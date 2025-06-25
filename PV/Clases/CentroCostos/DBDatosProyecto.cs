using PV.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Security.Cryptography;
using Condominios;
using System.Reflection;

namespace PV.Clases.CentroCostos
{
    internal class DBDatosProyecto
    {

        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static int Eliminado = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBDatosProyecto()
        {
            try
            {
                cn = new SqlConnection(ObtenerCn());
                cn.Open();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de Conexion" + ex.ToString());
            }
        }


        public void SeleccionarCentroCostos(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select Nombre from CentroCostos", cn);
           // cmd = new SqlCommand("select * from CentroCostos where Clave='" + txtClave + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }



        public void SeleccionarUsuario(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select Nombre from Usuarios", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }

        public void SeleccionarFormaPAgo2(ComboBox cb)
        {
            cb.Items.Clear();
           // cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select Descripcion from FormasPago", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        public void SeleccionarCatConceptosGlobales(ComboBox cb)
        {
            cb.Items.Clear();
            // cb.Items.Add("TODOS");
            cmd = new SqlCommand("select Clave from ConceptosGlobales", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }

        public void ConsultConceptoSeleccionado(string Clave, Guna2TextBox Importe)
        {
            try
            {
                cmd = new SqlCommand("Select Importe from ConceptosGlobales where Clave='" + Clave + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Importe.Text = dr["Importe"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        public void SeleccionarFormaPAgo2(Guna2TextBox clave,string formapago)
        {
          
            // cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select ClaveFormasPago from FormasPago where Descripcion='"+ formapago + "'", cn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                clave.Text = dr["ClaveFormasPago"].ToString();
            }
            dr.Close();
            
        }


        public void ConsultaFolio(Guna2TextBox Folio)
        {
            try
            {
                //cmd = new SqlCommand("select top 1 Folio from DatosProyecto order by folio desc", cn);
                cmd = new SqlCommand("SELECT ISNULL((SELECT TOP 1 Folio FROM DatosProyecto ORDER BY Folio DESC), 0) + 1 AS Folio;", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    Folio.Text = dr["Folio"].ToString();
                 /*   if (Folio.Text == string.Empty || Folio.Text == "")
                    {
                        Folio.Text = "1";
                    }
                    else {
                        int ultimofolio = Convert.ToInt16(Folio.Text);
                        Folio.Text = Convert.ToString(ultimofolio + 1);
                    }
                 */
                   // MessageBox.Show("Folio: " + Folio.Text); 
                    
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        // registrar forma pago 
        public string RegistroFormaPagoproyecto(string Id,string Folio,string txtclave, string txtformapago, string txtreferencia, string txtdescripcionR)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from FormaPagoProyecto where Id='"+Id+"'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into FormaPagoProyecto (Folio,IdFormaPago,DescripcionFormaPago,Referencia,DescripcionReferencia) values ('" + Folio + "', '" + txtclave + "',  '" + txtformapago + "',  '" + txtreferencia + "', '" + txtdescripcionR +"')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de Formas de Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("Update FormaPagoProyecto set IdFormaPago='"+txtclave+ "',DescripcionFormaPago='"+ txtformapago + "',Referencia='" + txtreferencia + "', DescripcionReferencia='" + txtdescripcionR + "' where Id='"+Id+"'", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro modificado.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        public DataTable ObtenerFormasPagoProyectoo(string folio)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT * from FormaPagoProyecto 
                      
                      WHERE Folio = @Folio 
                      ", cn);

                da.SelectCommand.Parameters.AddWithValue("@Folio", folio);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                // Puedes manejar mejor el error o lanzarlo para la capa superior
                throw new Exception("Error al obtener partidas de reembolso", ex);
            }
        }

        public string RegistroDatosproyecto(string Folio, string centrocosto, string proyecto, string Caracteristicas, string Area,string Encargado, string iva, string CuentaContable)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from DatosProyecto where centrocostos='" + centrocosto + "' and proyecto='"+ proyecto + "' ", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {

                    cmd = new SqlCommand("Insert into DatosProyecto (Folio,CentroCostos,Proyecto,Caracteristicas,Area,Encargado,Iva,CuentaContable) values ('" + Folio + "', '" + centrocosto + "',  '" + proyecto + "',  '" + Caracteristicas + "', '" + Area + "', '" + Encargado + "', '" + iva + "', '" + CuentaContable + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Datos de Formas de Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cmd = new SqlCommand("Update DatosProyecto set Caracteristicas='" + Caracteristicas + "', Area='" + Area + "',Encargado='" + Encargado + "', Iva='" + iva + "', CuentaContable='" + CuentaContable + "'", cn);
                        cmd.ExecuteNonQuery();
                        mensaje = "Registro modificado.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }

        //Mostrar Usuario seleccionado
        public void ConsultaproyectoSeleccionada(string centrocostos,string proyecto,string folio, Guna2TextBox txtproyecto, Guna2TextBox txtcaracteristicas, Guna2TextBox Area, Guna2TextBox Encargado, Guna2TextBox idformapago , Guna2TextBox descripcionformapago, Guna2TextBox RBreferencia, Guna2TextBox descripcionReferencia, Guna2TextBox IVA, Guna2TextBox Cuentacontable)
        {
            try
            {
                cmd = new SqlCommand("select  D.Proyecto, D.Caracteristicas, D.Area, D.Encargado, f.IdFormaPago, F.DescripcionFormaPago, Referencia, DescripcionReferencia,D.Iva,D.CuentaContable from DatosProyecto D inner join FormaPagoProyecto F on D.Folio = F.Folio where D.CentroCostos = '" + centrocostos+"'  and D.Proyecto = '"+proyecto+"' and D.Folio = '"+folio+"'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtproyecto.Text = dr["Proyecto"].ToString();
                    txtcaracteristicas.Text = dr["Caracteristicas"].ToString();
                    Area.Text = dr["Area"].ToString();
                    Encargado.Text = dr["Encargado"].ToString();
                    idformapago.Text = dr["IdFormaPago"].ToString();
                    descripcionformapago.Text = dr["DescripcionFormaPago"].ToString();
                    RBreferencia.Text  = dr["Referencia"].ToString();                
                    descripcionReferencia.Text = dr["DescripcionReferencia"].ToString();
                    IVA.Text = dr["Iva"].ToString();
                    Cuentacontable.Text = dr["CuentaContable"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        public void Cargardatosproyectos(DataGridView dgv, string centrocostos)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select * from DatosProyecto D where D.CentroCostos = '" + centrocostos+"' ", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[1].Value = item["Proyecto"].ToString();
                  
                    dgv.Rows[n].Cells[2].Value = item["Folio"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        public DataTable CargarDatosProyectos(string centrocostos)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Proyecto, Folio FROM DatosProyecto WHERE CentroCostos = @centrocostos and estatus=1", cn))
                {
                    cmd.Parameters.AddWithValue("@centrocostos", centrocostos);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }

            return dt;
        }
        public bool ActualizarEstatusProyecto(string folio)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE DatosProyecto SET Estatus = 0 WHERE Folio = @folio", cn))
                {
                    cmd.Parameters.AddWithValue("@folio", folio);
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    return filasAfectadas > 0; // true si se actualizó al menos una fila
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar estatus: " + ex.Message);
                return false;
            }
        }

        //_____________________________________________________________________________________________________


    }
}


