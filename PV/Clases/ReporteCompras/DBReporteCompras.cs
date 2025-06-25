using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using PV.Properties;


namespace PV.Clases.ReporteCompras
{
    class DBReporteCompras
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;

        public static int Folio = 0;
        public static string MatriculaC = string.Empty;
        public static decimal Total = 0;
        public static decimal Saldo = 0;

        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }
        public void CerrarConexion()
        {
            try
            {
                cn.Close();

            }
            catch (Exception ex)
            {

            }
        }
        public DBReporteCompras()
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
        //___________________________________________________________________________________________
        public void SeleccionarProveedor(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select RazonSocial from Proveedor", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarPropietario(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select RazonSocial from Propietarios order by RazonSocial", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarCondominio(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select Descripcion from Condominio order by Descripcion", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarProveedor2(ComboBox cb)
        {
            cb.Items.Clear();
 
            cmd = new SqlCommand("Select RazonSocial from Proveedor", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarCentroCosto(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select Nombre from CentroCostos", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //___________________________________________________________________________________________
        public void SeleccionarCuentasBancarias(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("select Nombre from CuentasBancarias", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //_____________________________________________________________________________________________________
        //Mostrar empresa seleccionado
        public void totalesFecha(string ClavePropietario, string fecha2)
        {
            try
            {
                cmd = new SqlCommand("Select sum(RP.Total + RG.Total + NG.Total) as Total, sum(RP.Saldo + RP.Saldo + NG.Saldo) as Saldo from RecepcionProducto as RP, RegistroGastos as RG, NotasGasto as NG,  Proveedor as P where P.IdProveedor= RP.ClaveProveedor and P.IdProveedor=RG.ClaveProveedor and P.IdProveedor=NG.ClaveProveedor and P.IdProveedor= '" + ClavePropietario + "' and RP.Fecha <= '" + fecha2 + "' and RG.Fecha <= '" + fecha2 + "' and NG.Fecha <= '" + fecha2 + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Total = Convert.ToDecimal(dr["Total"].ToString());
                    Saldo = Convert.ToDecimal(dr["Saldo"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar empresa seleccionado
        public void totales(string ClavePropietario)
        {
            try
            {
                cmd = new SqlCommand("Select sum(RP.Total + RG.Total + NG.Total) as Total, sum(RP.Saldo + RP.Saldo + NG.Saldo) as Saldo from RecepcionProducto as RP, RegistroGastos as RG, NotasGasto as NG,  Proveedor as P where P.IdProveedor= RP.ClaveProveedor and P.IdProveedor=RG.ClaveProveedor and P.IdProveedor=NG.ClaveProveedor and P.IdProveedor= '" + ClavePropietario + "' ", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Total = Convert.ToDecimal(dr["Total"].ToString());
                    Saldo = Convert.ToDecimal(dr["Saldo"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        public void totales2(string ClavePropietario)
        {
            try
            {
                cmd = new SqlCommand("Select sum(RP.Total + RG.Total + NG.Total) as Total, sum(RP.Saldo + RP.Saldo + NG.Saldo) as Saldo from RecepcionProducto as RP, RegistroGastos as RG, NotasGasto as NG,  Proveedor as P where P.IdProveedor= RP.ClaveProveedor and P.IdProveedor=RG.ClaveProveedor and P.IdProveedor=NG.ClaveProveedor and P.RazonSocial= '" + ClavePropietario + "' ", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Total = Convert.ToDecimal(dr["Total"].ToString());
                    Saldo = Convert.ToDecimal(dr["Saldo"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
    }
}
