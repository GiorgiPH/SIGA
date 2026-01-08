using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PuntoVentas.Clases.Usuarios;

namespace PV
{
    public partial class UsuarioPermisos : Form
    {
        private PermisosManager permisosManager;
        string usuario; // TODO: Obtener del usuario logueado

        public UsuarioPermisos(string usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void UsuarioPermisos_Load(object sender, EventArgs e)
        {
            // Inicializar el gestor de permisos
            permisosManager = new PermisosManager(guna2TabControl1, this.usuario);
            
            // Cargar permisos desde la base de datos
            permisosManager.CargarPermisos();
            
            // Configurar eventos para switches secundarios
            permisosManager.ConfigurarEventosSwitchesSecundarios();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            // Guardar permisos
            string resultado = permisosManager.GuardarPermisos();
            MessageBox.Show(resultado, "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            // Cerrar conexión a la base de datos
            permisosManager?.CerrarConexion();
            this.Close();
        }

        // Los siguientes métodos se mantienen por compatibilidad pero ya no son necesarios
        // El sistema genérico maneja toda la lógica

        private void PARAMETROS_Click(object sender, EventArgs e)
        {
            // Evento de clic en pestaña PARAMETROS
        }

        private void CATALOGOS_Click(object sender, EventArgs e)
        {
            // Evento de clic en pestaña CATALOGOS
        }

        private void COMPRAS_Click(object sender, EventArgs e)
        {
            // Evento de clic en pestaña COMPRAS
        }

        private void VENTAS_Click(object sender, EventArgs e)
        {
            // Evento de clic en pestaña VENTAS
        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {
            // Evento de clic en label
        }

        private void guna2ToggleSwitch35_CheckedChanged(object sender, EventArgs e)
        {
            // Evento para switch de TESORERIA (manejado por el sistema genérico)
        }

        private void CatSwitch_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Los siguientes métodos ya no son necesarios ya que el sistema genérico maneja
        // toda la lógica de switches. Se mantienen comentados por referencia:

        /*
        private void ParamSwitch_CheckedChanged(object sender, EventArgs e) { }
        private void ParamSwitch1_CheckedChanged(object sender, EventArgs e) { }
        private void ParamSwitch2_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch1_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch2_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch3_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch4_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch5_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch6_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch7_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch8_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch9_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch10_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch11_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch12_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch13_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch14_CheckedChanged(object sender, EventArgs e) { }
        private void CatSwitch15_CheckedChanged(object sender, EventArgs e) { }
        private void InvSwitch1_CheckedChanged(object sender, EventArgs e) { }
        private void InvSwitch3_CheckedChanged(object sender, EventArgs e) { }
        private void InvSwitch4_CheckedChanged(object sender, EventArgs e) { }
        private void InvSwitch5_CheckedChanged(object sender, EventArgs e) { }
        private void InvSwitch6_CheckedChanged(object sender, EventArgs e) { }
        private void InvSwitch7_CheckedChanged(object sender, EventArgs e) { }
        private void InvSwitch8_CheckedChanged(object sender, EventArgs e) { }
        private void CompSwitch1_CheckedChanged(object sender, EventArgs e) { }
        private void CompSwitch2_CheckedChanged(object sender, EventArgs e) { }
        private void CompSwitch3_CheckedChanged(object sender, EventArgs e) { }
        private void CompSwitch4_CheckedChanged(object sender, EventArgs e) { }
        private void CompSwitch5_CheckedChanged(object sender, EventArgs e) { }
        private void CompSwitch6_CheckedChanged(object sender, EventArgs e) { }
        private void CompSwitch7_CheckedChanged(object sender, EventArgs e) { }
        private void CompSwitch8_CheckedChanged(object sender, EventArgs e) { }
        private void CompSwitch9_CheckedChanged(object sender, EventArgs e) { }
        private void venSwitch_CheckedChanged(object sender, EventArgs e) { }
        */
    }
}
