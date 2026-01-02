using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Condominios.Clases.CentroCostos;
using Condominios.Clases.ConceptosGlobales;
using ControlAcademico;
using Guna.UI2.WinForms;
using PV.Clases;
using PV.Clases.Inventario;
using PV.Clases.OrdenCompra;

namespace PV
{
    public partial class ConceptosGlobalesPartidaGastos : Form
    {
        DBOrdenCompra c = new DBOrdenCompra();
        DBRegistroReembolso r = new DBRegistroReembolso();
        DBConceptosGlobales conceptosGlobales = new DBConceptosGlobales();
        string recibo = string.Empty;
        string reciboCol = string.Empty;
        string impuesto = string.Empty;
        int Cantidadimpuesto = 0;
        int contadorimpuesto = 0;
        private string[] datosconceptos;
        DataTable partidas;
        decimal precio1 = 0;
        decimal cantidad = 0m;
        decimal descuento = 0m;
        decimal RetencionP = 0m;
        decimal importeImp = 0m;
        int partidas_btnsiguiente = 0;
        int numeroDePartidas = 0;
        decimal subtotalBase = 0m;

        public ConceptosGlobalesPartidaGastos(string Folio, string Recibo, string ReciboCol, string [] datosConceptosG)
        {
            InitializeComponent();
            TxtFolio.Text = Folio;
            recibo = Recibo;
            reciboCol = ReciboCol;
            partidas_btnsiguiente = 1;
        }
        private void LlenarComboConceptos()
        {
            try
            {
                DataTable menus = conceptosGlobales.CargarConceptos();


                cmbConcepto.DropDownStyle = ComboBoxStyle.DropDown; // Cambiar a DropDown
                cmbConcepto.DataSource = menus;
                cmbConcepto.DisplayMember = "Nombre"; // Campo visible
                cmbConcepto.ValueMember = "Clave";   // Campo interno
                cmbConcepto.SelectedIndex = -1;     // Ningún elemento seleccionado al inicio
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConceptosGlobalesPartidaGastos_Load(object sender, EventArgs e)
        {
            try { 
            Limpiar();
             //r.ObtenerPartidasReembolso(TxtFolio.Text,dataGridView1);
            r.ObtenerPartidasReembolso2(TxtFolio.Text, dataGridView2);
            cmbConcepto.SelectedIndexChanged -= cmbConcepto_SelectedIndexChanged;
            LlenarComboConceptos();
            cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;
            /*
            var subtotalesUnicos = partidas.AsEnumerable()
            .GroupBy(row => row.Field<int>("Partida")) // o Field<int> si es numérica
            .Select(g => g.First()) // una sola fila por partida
            .Sum(row => row.Field<decimal>("Subtotal"));

            txtSubtotal.Text = subtotalesUnicos.ToString("N2");*/

            /*  var primerRegistro = partidas.AsEnumerable().FirstOrDefault();

              // Perform the calculation only if a record was found
              decimal totalCalculado = 0;
              if (primerRegistro != null)
              {
                  // Ensure that these fields exist in your DataTable and have appropriate types
                  decimal cantidad = primerRegistro.Field<decimal>("Cantidad");
                  decimal precio = primerRegistro.Field<decimal>("Precio");
                  decimal descuento = primerRegistro.Field<decimal>("Descuento"); // Assuming 'Descuento' field exists

                  totalCalculado = (cantidad * precio) - descuento;
              }*/
            //   txtSubtotal.Text = totalCalculado.ToString("N2");

             numeroDePartidas = dataGridView2.RowCount;

            decimal Subtotal = 0m;
            decimal clase = 0m;
            string Tipo = string.Empty;
            decimal importe = 0m;
            

            if (dataGridView2.Rows.Count > 0)
            {
                DataGridViewRow firstRow = dataGridView2.Rows[0];
                // decimal.TryParse(firstRow.Cells[4].Value.ToString(), out cantidad);
                Tipo = (firstRow.Cells[5].Value.ToString());
                txtclase.Text= (firstRow.Cells[1].Value.ToString());
                decimal.TryParse(firstRow.Cells[2].Value.ToString(), out Subtotal);
                decimal.TryParse(firstRow.Cells[3].Value.ToString(), out importeImp);           
                decimal.TryParse(firstRow.Cells[4].Value.ToString(), out RetencionP);              
            }

                cmbConcepto.SelectedValue = Tipo;
                cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;
         
            numeroDePartidas = dataGridView2.RowCount;
            ///////////////////////////             
            txtSubtotal.Text = Subtotal.ToString("N2");
            ActualizarCalculos();
            if (numeroDePartidas == 2)
            {
                guna2Button3.Enabled = true;
            }
            else if (numeroDePartidas > 2)
            { 
                guna2Button3.Enabled = false;
                guna2Button8.Enabled = true;
                guna2Button8.Visible = true;
            }
            }
            catch {
                MessageBox.Show("Ocurrio un error al tratar de mostrar los impuestos Globales");
            }   




        }
        private void cmbConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cmbConcepto.Text))
            {
               /*try
                {*/
                    

                     //Obtener valores
                    string[] valores = conceptosGlobales.InformacionReciboConceptoGlobal(cmbConcepto.SelectedValue.ToString());                    txtClave.Text = valores[0];
                    txtConcepto.Text = valores[1];
                    txtclase.Text = valores[2];
                    txtTipo.Text = valores[3];
                    txtPorcentaje.Text = valores[6];
                    string xtIncluyeIva = valores[7];
                    if (Convert.ToBoolean(xtIncluyeIva) == true)
                    {
                        txtIncluyeIva.Text = "1";
                    }
                    else {
                        txtIncluyeIva.Text = "0";
                    }

                    txtNivel.Text = valores[7];
                   
                    if (txtNivel.Text == "Partida")
                    {
                        txtDescuento1.Enabled = false;
                    }
                    else
                    {
                        txtDescuento1.Enabled = true;
                    }

                        // Calcular y actualizar los valores

//ActualizarCalculos();
             //   MessageBox.Show("actualizacion desde combo:");
                /*   }
                   catch (Exception ex)
                   {
                       MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }*/
            }
        }
  
        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            c.Monto(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbConcepto.Text))
            {
                MessageBox.Show("Registre el concepto para continuar");
                return;
            }

            if (decimal.TryParse(txtTotal.Text, out decimal total) && total < 0)
            {
                MessageBox.Show("No es posible continuar con un Total menor a 0");
                return;
            }

            decimal sumaSubtotal16 = 0m;
            decimal sumaImpuesto16 = 0m;

            decimal subtotal = Convert.ToDecimal(txtSubtotal.Text);
            decimal descuento = Convert.ToDecimal(txtDescuento1.Text);
            decimal impuesto = Convert.ToDecimal(txtDescuento1.Text);
            int folio = Convert.ToInt32(TxtFolio.Text);
            string clase = txtclase.Text;
            
            switch (clase)
            {
                case "Cargo":
                case "Descuento":
            
                    c.InsertarReciboConceptoGlobalGasto(txtClave.Text, TxtFolio.Text, subtotal, descuento, total, clase, txtIncluyeIva.Text);
                    c.ActualizarReciboConceptoGlobalGastos(folio, total);

                    break;

                case "Impuesto":
                    if (impuesto == 0)
                    {
                        c.InsertarReciboConceptoGlobalGasto(txtClave.Text, TxtFolio.Text, subtotal, descuento, total, clase, txtIncluyeIva.Text);
                        c.ActualizarReciboConceptoGlobalGastos(folio, total);
            
                    }
                    else
                    {
                        subtotal -= impuesto;
                        c.InsertarReciboConceptoGlobalGasto(txtClave.Text, TxtFolio.Text, subtotal, descuento, total, clase, txtIncluyeIva.Text);
                       c.ActualizarReciboConceptoGlobalGastos(folio, total);
            
                    }
                    break;

                default:
                    MessageBox.Show("Clase no reconocida");
                    return;
            }
            
            //// Operaciones comunes después del switch
            
            c.ActualizarPartidaReciboConceptoGlobalGasto((TxtFolio.Text), Convert.ToDecimal(txtPorcentaje.Text), txtclase.Text);            
            //  c.ActualizarPartidaReciboConceptoGlobalRemision(folio, Convert.ToDecimal(txtPorcentaje.Text), clase, txtIncluyeIva.Text);
            c.EliminarReciboConceptoGlobalRemision3(TxtFolio.Text);
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)

        {

            try
            {
                if (partidas_btnsiguiente == numeroDePartidas)
                {
                    guna2Button8.Enabled = false;
                    guna2Button3.Enabled = true;
                }
                if (string.IsNullOrWhiteSpace(cmbConcepto.Text))
                {
                    MessageBox.Show("Registre el concepto para continuar");
                    return;
                }

                if (!decimal.TryParse(txtTotal.Text, out decimal total) || total < 0)
                {
                    MessageBox.Show("No es posible continuar con un Total menor a 0");
                    return;
                }
           //     MessageBox.Show(dataGridView2.Rows.Count.ToString());
           //     MessageBox.Show(partidas_btnsiguiente.ToString());

                if (partidas_btnsiguiente >= 0 && partidas_btnsiguiente < dataGridView2.Rows.Count)
                {
                    var fila = dataGridView2.Rows[partidas_btnsiguiente];
                    // Obtener valores de la fila actual
                    string Tipo = fila.Cells["Tipo"].Value?.ToString();
                    subtotalBase = Convert.ToDecimal(fila.Cells["SubtotalCalculado"].Value ?? 0);
                    decimal retenciones = Convert.ToDecimal(fila.Cells["ImporteRetencion"].Value ?? 0);
                    importeImp = 0m;
                    
                    importeImp = Convert.ToDecimal(fila.Cells["ImpuestoImporte"].Value ?? 0);
                    //txtDescuento1.Text = importeImp.ToString();
                    string clase = fila.Cells["ClaseConceptoAgrupada"].Value?.ToString();

               //     MessageBox.Show("Tipo:" + Tipo);
               //     MessageBox.Show("importeImp:" + importeImp);

                    cmbConcepto.SelectedValue = Tipo;
                  //  cmbConcepto.SelectedIndexChanged += cmbConcepto_SelectedIndexChanged;
                  //  MessageBox.Show("actualizacion combo:");

                    txtSubtotal.Text = subtotalBase.ToString("N2");
                    txtclase.Text = clase;
                    // Guardar en variables globales si es necesario
                    //this.importeImp = IVA;
                    this.RetencionP = retenciones;
                    this.subtotalBase = subtotalBase;
                    // Incrementar contador
                    
                    partidas_btnsiguiente++;

                    if (partidas_btnsiguiente + 1 == dataGridView2.Rows.Count)
                    {
                        guna2Button3.Enabled = true;
                        guna2Button8.Enabled = false;
                    }
                    ActualizarCalculos();
                }

                else

                {

                    MessageBox.Show("Índice fuera de rango.");

                }
 

                decimal subtotalFinal = Convert.ToDecimal(txtSubtotal.Text);
                string claseFinal = txtclase.Text;
                string incluyeIva = txtIncluyeIva.Text;

                c.InsertarReciboConceptoGlobalGasto(txtClave.Text, TxtFolio.Text, subtotalFinal, descuento, total, claseFinal, incluyeIva);

//                cmbConcepto.SelectedIndex = -1;
                //Limpiar();
                           }

            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }


        /*
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (partidas_btnsiguiente == numeroDePartidas)
                {
                    guna2Button8.Enabled = false;
                    guna2Button3.Enabled = true;
                }
                if (string.IsNullOrWhiteSpace(cmbConcepto.Text))
                {
                    MessageBox.Show("Registre el concepto para continuar");
                    return;
                }

                if (!decimal.TryParse(txtTotal.Text, out decimal total) || total < 0)
                {
                    MessageBox.Show("No es posible continuar con un Total menor a 0");
                    return;
                }

                if (partidas_btnsiguiente >= 0 && partidas_btnsiguiente < dataGridView2.Rows.Count)
                {
                    var fila = dataGridView2.Rows[partidas_btnsiguiente];
                    // Validación individual
                    
                    // object valorPrecio = fila.Cells["precio"].Value;
                    object valorSubtotal = fila.Cells["ImpuestoPorcentaje"].Value;
                    object valorImpuesto = fila.Cells["importeimpuesto"].Value;
                    object valorRetencion = fila.Cells["retneciones"].Value;


                    // Conversión y almacenamiento en variables
                     importeImp = Convert.ToDecimal(valorImpuesto);
                     RetencionP = Convert.ToDecimal(valorRetencion);
                     subtotalBase = Convert.ToDecimal(valorRetencion);
                    //RetencionP = Convert.ToDecimal(valorRetencion);

                    // Puedes usar aquí tus variables
                   // Console.WriteLine($"Precio: {precio}, Descuento: {descuento}, Cantidad: {cantidad}");
                    partidas_btnsiguiente++; // avanzar al siguiente para la próxima vez
                    
                    if (partidas_btnsiguiente+1 == dataGridView2.Rows.Count)
                    {
                        guna2Button3.Enabled = true;  // Activar botón 3
                        guna2Button8.Enabled = false; // Desactivar botón 8
                    }
                }
                else
                {
                    MessageBox.Show("Índice fuera de rango.");
                }


                ActualizarCalculos();
                 decimal subtotal = Convert.ToDecimal(txtSubtotal.Text);
                string clase = txtclase.Text;
                string incluyeIva = txtIncluyeIva.Text;
                c.InsertarReciboConceptoGlobalGasto(txtClave.Text, TxtFolio.Text, subtotal, descuento, total, clase, incluyeIva);
               
                Limpiar();
                cmbConcepto.SelectedIndex = -1;
            }
            catch (Exception ex) {
                MessageBox.Show("error: "+ ex);

            }
        }
        */
        void Limpiar()
        {
        //    cmbConcepto.Text = null;
          //  txtConcepto.Clear();
            txtclase.Clear();
            txtTipo.Clear();
            txtClave.Clear();
            txtConcepto.Clear();
            txtclase.Clear();
            txtTipo.Clear();
            txtDescuento1.Text = "0.00";
            txtTotal.Text = "0.00";
            txtImpuesto.Text = "0.00";
            txtIncluyeIva.Text = string.Empty;
            txtDivisa.Text = "MXN";
            txtTipoCambio.Text = "1.00";
            txtNivel.Text = string.Empty;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSubtotal_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtSubtotal);
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtDescuento1);

            if (txtDescuento1.Text != string.Empty)
            {
                if (txtclase.Text == "Cargo" || txtclase.Text == "Impuesto")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) + Convert.ToDecimal(txtDescuento1.Text)).ToString("N2");
                }
                else if (txtclase.Text == "Descuento")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtSubtotal.Text) - Convert.ToDecimal(txtDescuento1.Text)).ToString("N2");
                }
            }
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtTotal);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private decimal CalcularDescuento(string tipo, decimal porcentaje, decimal subtotal)
        {
            switch (tipo)
            {
                case "Importe":
                    return porcentaje;

                case "Porcentaje":
                    return (porcentaje / 100) * subtotal;

                default:
                    return 0;
            }
        }

        private decimal CalcularTotal(string clase, decimal subtotal, decimal descuento)
        {
            switch (clase)
            {
                case "Cargo":
                case "Impuesto":
                    return subtotal + descuento;

                case "Descuento":
                    return subtotal - descuento;

                default:
                    return subtotal; // Por defecto, retornar el subtotal
            }
        }
        private decimal CalcularIva(decimal total, string iva)
        {
            switch (iva)
            {
                case "1":

                    return (total * 0.16m);

                case "0":
                    return total;

                default:
                    return total; // Por defecto, retornar el subtotal
            }
        }
        private bool _calculando = false;

        private void ActualizarCalculos()
        {
  
            decimal descuentoValue = 0m; // Renamed to avoid confusion with the textbox
            decimal porcentajeValue = 0m;
            decimal impuestoCalculado = 0m;
            decimal totalFinal = 0m;
        //    decimal retencion1 =Convert.ToDecimal(txtRetencion.Text);

            //MessageBox.Show(txtclase.Text);
            
            if (txtclase.Text == "Impuesto")
            {
                txtDescuento1.Text = importeImp.ToString("N2");
                //    porcentajeValue = Convert.ToDecimal(txtPorcentaje.Text);
                subtotalBase = Convert.ToDecimal(txtSubtotal.Text);
                
              //  MessageBox.Show("txtDescuento1_1:" + txtDescuento1.Text);
                
                totalFinal = subtotalBase + importeImp;
                txtTotal.Text = totalFinal.ToString("N2");
                //MessageBox.Show("totalFinal_1:" + totalFinal);
            }
            else if (txtclase.Text == "Descuento") // For any other 'Clase' (e.g., 'Descuento', 'Cargo', etc.)
            {
                
                txtDescuento1.Text = RetencionP.ToString("N2");
                subtotalBase = Convert.ToDecimal(txtSubtotal.Text);
               // MessageBox.Show("txtDescuento1_2|:" + txtDescuento1.Text);

                totalFinal = subtotalBase - RetencionP;
                txtTotal.Text = totalFinal.ToString("N2");
              //  MessageBox.Show("totalFinal_2:" + totalFinal);
            

            }
            


            //if (_calculando) return;


            /*
                        try
                        {
                            _calculando = true;

                            decimal totalDescuento = 0;
                            decimal totalImpuesto = 0;
                            decimal totalIEPS = 0;
                            decimal subtotalGeneral = 0;
                            string clave1 = string.Empty;
                            foreach (DataRow row in partidas.Rows)
                            {
                                if (row.RowState == DataRowState.Deleted) continue;

                                decimal subtotalPartida = row.IsNull("Subtotal") ? 0 : Convert.ToDecimal(row["Subtotal"]);
                                subtotalGeneral += subtotalPartida;

                                string clase = row.IsNull("Clase") ? "" : row["Clase"].ToString();
                                string clave = row.IsNull("Clave") ? "" : row["Clave"].ToString();
                                clave1 = clase;
                                string tipo = row.IsNull("Tipo") ? "" : row["Tipo"].ToString();
                                decimal valorDescuento = row.IsNull("DescuentoConcepto") ? 0 : Convert.ToDecimal(row["DescuentoConcepto"]);
                                decimal valorCargo = row.IsNull("CargoConcepto") ? 0 : Convert.ToDecimal(row["CargoConcepto"]);
                                decimal valorIEPS = row.IsNull("IEPS") ? 0 : Convert.ToDecimal(row["IEPS"]);

                                if (clave=="IEPS")
                                {
                                    totalIEPS += Convert.ToDecimal(row["IEPS"]);
                                    if (clave == cmbConcepto.SelectedValue.ToString())
                                        txtDescuento.Text = totalIEPS.ToString("N2");
                                }


                                else if (clase == "Descuento")
                                {
                                    if (tipo == "Porcentaje")
                                        totalDescuento += (valorDescuento  / 100m) * (subtotalPartida + valorDescuento);
                                    else
                                        totalDescuento += valorDescuento;
                                    if(clave == cmbConcepto.SelectedValue.ToString())
                                        txtDescuento.Text = totalDescuento.ToString("N2");
                                }

                                else if (clase == "Impuesto")
                                {
                                    if (tipo == "Porcentaje")
                                        totalImpuesto += (valorCargo / 100m) * subtotalPartida;
                                    else
                                        totalImpuesto += valorCargo;

                                    if (clave == cmbConcepto.SelectedValue.ToString())
                                        txtDescuento.Text = totalImpuesto.ToString("N2");
                                }


                            }

                            // Asignar valores a cajas de texto
                            //txtSubtotal.Text = subtotalGeneral.ToString("N2");

                           decimal totalFinal = Convert.ToDecimal(txtSubtotal.Text) - totalDescuento + totalImpuesto + totalIEPS;

                      //      txtTotal.Text = totalFinal.ToString("N2");


                            if (clave1 == "Descuento")
                            {
                                txtTotal.Text = (Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtDescuento.Text)).ToString();
                            }
                            else if (clave1 == "Impuesto") { txtTotal.Text = totalFinal.ToString("N2"); }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("error aqui");
                            MessageBox.Show($"Error al calcular: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            _calculando = false;
                        }*/
            /*      try

                  {

                      _calculando = true;

                      decimal totalDescuento = 0;

                      decimal totalImpuesto = 0;

                      decimal totalIEPS = 0;

                      decimal subtotalGeneral = 0;

                      string conceptoClaveSeleccionado = cmbConcepto.SelectedValue.ToString();

                      string claseSeleccionada = "";

                      // Intentar tomar el subtotal actual del formulario si ya está definido

                      if (decimal.TryParse(txtSubtotal.Text, out decimal subtotalActual))

                      {

                          subtotalGeneral = subtotalActual;

                      }

                      else

                      {

                          subtotalGeneral = 0; // En caso de error, iniciar desde cero

                      }

                      foreach (DataRow row in partidas.Rows)

                      {

                          if (row.RowState == DataRowState.Deleted) continue;

                          decimal subtotalPartida = row.IsNull("Subtotal") ? 0 : Convert.ToDecimal(row["Subtotal"]);

                          // Solo sumar al subtotal si no se está usando el fijo

                          if (!decimal.TryParse(txtSubtotal.Text, out _))

                          {

                              subtotalGeneral += subtotalPartida;

                          }

                          string clase = row.IsNull("Clase") ? "" : row["Clase"].ToString();

                          string clave = row.IsNull("Clave") ? "" : row["Clave"].ToString();

                          string tipo = row.IsNull("Tipo") ? "" : row["Tipo"].ToString();

                          decimal valorDescuento = row.IsNull("DescuentoConcepto") ? 0 : Convert.ToDecimal(row["DescuentoConcepto"]);

                          decimal valorCargo = row.IsNull("CargoConcepto") ? 0 : Convert.ToDecimal(row["CargoConcepto"]);

                          decimal valorIEPS = row.IsNull("IEPS") ? 0 : Convert.ToDecimal(row["IEPS"]);

                          // Guardar clase del concepto seleccionado

                          if (clave == conceptoClaveSeleccionado)

                              claseSeleccionada = clase;

                          if (clave == "IEPS")

                          {

                              totalIEPS += valorIEPS;

                              if (clave == conceptoClaveSeleccionado)

                                  txtDescuento.Text = totalIEPS.ToString("N2");

                          }

                          else if (clase == "Descuento")

                          {

                              if (tipo == "Porcentaje")

                                  totalDescuento += (valorDescuento / 100m) * (subtotalPartida + valorDescuento);

                              else

                                  totalDescuento += valorDescuento;

                              if (clave == conceptoClaveSeleccionado)

                                  txtDescuento.Text = totalDescuento.ToString("N2");

                          }

                          else if (clase == "Impuesto")

                          {

                              if (tipo == "Porcentaje")

                                  totalImpuesto += (valorCargo / 100m) * subtotalPartida;

                              else

                                  totalImpuesto += valorCargo;

                              if (clave == conceptoClaveSeleccionado)

                                  txtDescuento.Text = totalImpuesto.ToString("N2");

                          }

                      }

                      // Si no hay subtotal definido, asignar el calculado

                      if (!decimal.TryParse(txtSubtotal.Text, out _))

                      {

                          txtSubtotal.Text = subtotalGeneral.ToString("N2");

                      }

                      // Determinar total con base en la clase del concepto seleccionado

                      if (claseSeleccionada == "Descuento")

                      {

                          txtTotal.Text = (subtotalGeneral - totalDescuento).ToString("N2");

                      }

                      else if (claseSeleccionada == "Impuesto")

                      {

                          txtTotal.Text = (subtotalGeneral + totalImpuesto).ToString("N2");

                      }

                      else

                      {

                          decimal totalFinal = subtotalGeneral - totalDescuento + totalImpuesto + totalIEPS;

                          txtTotal.Text = totalFinal.ToString("N2");

                      }

                  }
                  catch (Exception ex)
                  {
                                      MessageBox.Show("error aqui");
                      MessageBox.Show($"Error al calcular: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }

                  finally

                  {

                      _calculando = false;

                  }
            */


        }



        private void txtDescuento_TextChanged_1(object sender, EventArgs e)
        {
            Utilerias.Moneda2(ref txtDescuento1);
           // ActualizarCalculos();
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtPorcentaje_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
