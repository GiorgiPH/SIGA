using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PV.Properties;


namespace PV.Clases.FiltroMovimiento
{
    class DBFiltroMovimientos
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }

        public DBFiltroMovimientos()
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
        //____________________________________________________________________________________________________________________________________________
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarDocumento(ComboBox cb, string Documento)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select * from TipoMovimiento where TipoMovimiento = '" + Documento + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarTipoDocumento(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select TipoMovimiento from TipoMovimiento group by TipoMovimiento", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarProductoServ(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select (Descripcion + '-' + Marca) as Producto from ProductosServicios", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarAlmacen(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (convert(varchar, Clave) + ' - ' + Nombre) Almacen from almacenes", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void Categorias(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select Nombre from Categorias", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionCategoria(string Nombre)
        {
            cmd = new SqlCommand("Select ClaveCategoria from Categorias where Nombre = '" + Nombre + "'", cn);
            dr = cmd.ExecuteReader();
            string[] resultado = null;
            while (dr.Read())
            {
                string[] valores =
                {
                    dr[0].ToString(),
                };
                resultado = valores;
            }
            dr.Close();
            return resultado;
        }

    }
}
