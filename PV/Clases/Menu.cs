using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PV.Clases.Menu
{
    /// <summary>
    /// Representa un módulo raíz del menú: el par de botones visuales que lo activan
    /// (por ejemplo BtnParametros1 / BtnParametros2) y, opcionalmente, el panel de
    /// "Grupo" que dicho módulo despliega.
    ///
    /// Esta clase reemplaza la información que antes vivía implícita y repetida
    /// dentro de ~9 métodos distintos (uno por módulo) del formulario del menú.
    /// </summary>
    public sealed class ModuloMenuItem
    {
        /// <summary>Identificador único del módulo (ej. "Parametros", "Ventas").</summary>
        public string Clave { get; }

        /// <summary>Botón del módulo en su primera presentación visual (ej. modo icono).</summary>
        public Control Boton1 { get; }

        /// <summary>Botón del módulo en su segunda presentación visual (ej. modo texto).</summary>
        public Control Boton2 { get; }

        /// <summary>Posición Y base del módulo cuando ningún grupo está abierto encima de él.</summary>
        public int PosicionYBase { get; }

        /// <summary>Panel de "Grupo" que este módulo despliega. Puede ser null si el módulo no tiene grupo.</summary>
        public Control PanelGrupo { get; }

        public ModuloMenuItem(string clave, Control boton1, Control boton2, int posicionYBase, Control panelGrupo = null)
        {
            Clave = clave ?? throw new ArgumentNullException(nameof(clave));
            Boton1 = boton1 ?? throw new ArgumentNullException(nameof(boton1));
            Boton2 = boton2 ?? throw new ArgumentNullException(nameof(boton2));
            PosicionYBase = posicionYBase;
            PanelGrupo = panelGrupo;
        }
    }

    /// <summary>
    /// Controlador de navegación del menú principal.
    ///
    /// Responsabilidad única: decidir qué grupo raíz está visible y recalcular,
    /// de forma genérica, la posición de todos los botones raíz en consecuencia.
    ///
    /// Sustituye a los handlers guna2GradientButton4_Click, guna2GradientButton6_Click,
    /// guna2GradientButton21_Click, guna2GradientButton4_Click_1, btnventas2_Click,
    /// btnCompras2_Click, btnPresupuesto2_Click, btnTesoreria2_Click y btnUtilerias2_Click,
    /// que repetían ~40-50 líneas casi idénticas cada uno.
    ///
    /// Aplica la Regla 1 (abrir un grupo cierra los demás) de forma uniforme para
    /// todos los módulos, sin excepción.
    /// </summary>
    public sealed class MenuNavigationManager
    {
        /// <summary>Separación vertical (px) entre el panel de grupo y el botón que sigue debajo.</summary>
        private const int EspaciadoDespuesDeGrupo = 10;

        private readonly Control _contenedor;
        private readonly List<ModuloMenuItem> _modulos;
        private readonly int _xBoton1;
        private readonly int _xBoton2;

        /// <param name="contenedor">Panel que hospeda visualmente los paneles de grupo (ej. guna2Panel5).</param>
        /// <param name="modulosEnOrden">Módulos del menú en el orden visual en que deben apilarse.</param>
        /// <param name="xBoton1">Posición X fija de la primera columna de botones.</param>
        /// <param name="xBoton2">Posición X fija de la segunda columna de botones.</param>
        public MenuNavigationManager(Control contenedor, List<ModuloMenuItem> modulosEnOrden, int xBoton1, int xBoton2)
        {
            _contenedor = contenedor ?? throw new ArgumentNullException(nameof(contenedor));
            _modulos = modulosEnOrden ?? throw new ArgumentNullException(nameof(modulosEnOrden));
            _xBoton1 = xBoton1;
            _xBoton2 = xBoton2;
        }

        /// <summary>
        /// Deja el menú en su estado inicial: todos los grupos cerrados y los
        /// botones raíz en su posición base. Debe llamarse una vez al iniciar el formulario.
        /// </summary>
        public void Inicializar()
        {
            CerrarTodosLosGrupos();
            Reacomodar();
        }

        /// <summary>
        /// Abre el grupo asociado a <paramref name="claveModulo"/>. Si ya estaba abierto, lo cierra.
        /// Cualquier otro grupo que estuviera abierto se cierra automáticamente (Regla 1).
        /// </summary>
        public void AlternarGrupo(string claveModulo)
        {
            ModuloMenuItem modulo = _modulos.FirstOrDefault(m => m.Clave == claveModulo);
            if (modulo == null || modulo.PanelGrupo == null)
            {
                return;
            }

            bool yaEstabaAbierto = modulo.PanelGrupo.Visible;

            CerrarTodosLosGrupos();

            if (!yaEstabaAbierto)
            {
                AbrirGrupo(modulo);
            }

            Reacomodar();
        }

        /// <summary>
        /// Cierra todos los grupos raíz sin excepción. Se usa tanto internamente
        /// (antes de abrir un grupo distinto) como desde el formulario al entrar
        /// a una tarea (Regla 3).
        /// </summary>
        public void CerrarTodosLosGrupos()
        {
            foreach (ModuloMenuItem modulo in _modulos.Where(m => m.PanelGrupo != null))
            {
                modulo.PanelGrupo.Visible = false;

                if (_contenedor.Controls.Contains(modulo.PanelGrupo))
                {
                    _contenedor.Controls.Remove(modulo.PanelGrupo);
                }
            }

            Reacomodar();
        }

        private void AbrirGrupo(ModuloMenuItem modulo)
        {
            if (!_contenedor.Controls.Contains(modulo.PanelGrupo))
            {
                _contenedor.Controls.Add(modulo.PanelGrupo);
            }

            modulo.PanelGrupo.Visible = true;
        }

        /// <summary>
        /// Recorre los módulos en orden y calcula la posición Y de cada botón raíz
        /// (y del panel de grupo abierto, si lo hay) de forma acumulativa, usando la
        /// altura real de los controles en tiempo de ejecución.
        ///
        /// Esto sustituye los cientos de "new Point(x, y)" hardcodeados que existían
        /// antes, uno distinto por cada combinación posible de grupo abierto/cerrado.
        /// Agregar un módulo nuevo a la lista de <see cref="_modulos"/> es todo lo que
        /// se necesita; no hay coordenadas que recalcular a mano.
        /// </summary>
        private void Reacomodar()
        {
            if (_modulos.Count == 0)
            {
                return;
            }

            int y = _modulos[0].PosicionYBase;

            foreach (ModuloMenuItem modulo in _modulos)
            {
                modulo.Boton1.Location = new Point(_xBoton1, y);
                modulo.Boton2.Location = new Point(_xBoton2, y);

                int alturaFila = Math.Max(modulo.Boton1.Height, modulo.Boton2.Height);
                y += alturaFila;

                if (modulo.PanelGrupo != null && modulo.PanelGrupo.Visible)
                {
                    modulo.PanelGrupo.Location = new Point(_xBoton1, y);
                    y += modulo.PanelGrupo.Height + EspaciadoDespuesDeGrupo;
                }
            }
        }
    }

    /// <summary>
    /// Controlador de subgrupos (tercer nivel del menú).
    ///
    /// Responsabilidad única: garantizar que solo un subgrupo esté visible a la vez
    /// (Regla 2), sin importar de qué módulo provenga.
    ///
    /// Sustituye a los handlers BtnMovimientos_Click, btnReportesMovimientosInventarios_Click,
    /// btnReportesCompras_Click, guna2GradientButton56_Click, guna2GradientButton36_Click,
    /// guna2GradientButton35_Click, guna2GradientButton20_Click, guna2GradientButton18_Click
    /// y btnReportesTesoreria_Click, que antes solo cerraban a "algunos" subgrupos vecinos
    /// conocidos en vez de a todos.
    /// </summary>
    public sealed class SubgrupoManager
    {
        private readonly List<Control> _subgrupos;

        public SubgrupoManager(IEnumerable<Control> subgrupos)
        {
            _subgrupos = subgrupos?.ToList() ?? throw new ArgumentNullException(nameof(subgrupos));
        }

        /// <summary>
        /// Muestra <paramref name="subgrupo"/> en <paramref name="posicion"/> y oculta
        /// cualquier otro subgrupo que estuviera visible.
        /// </summary>
        public void Abrir(Control subgrupo, Point posicion)
        {
            CerrarTodos();
            subgrupo.Location = posicion;
            subgrupo.Visible = true;
        }

        /// <summary>Oculta todos los subgrupos sin excepción (Regla 2 y Regla 3).</summary>
        public void CerrarTodos()
        {
            foreach (Control subgrupo in _subgrupos)
            {
                subgrupo.Visible = false;
            }
        }
    }
}