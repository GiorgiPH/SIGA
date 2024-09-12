using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using PV.Properties;


namespace PV.Clases.Presupuesto
{
    class DBPresupuesto
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

        public DBPresupuesto()
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
        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string RegistroPeriodo(string txtClave, string cmbMes, string dtFechaInicio, string dtFechaFinal)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Periodo where Clave='" + txtClave + "' and Mes='" + cmbMes + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into Periodo (Clave, Mes, FechaInicia, FechaFinal) values ('" + txtClave + "', '" + cmbMes + "',  '" + dtFechaInicio + "',  '" + dtFechaFinal + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Periodo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update Periodo set FechaInicia='" + dtFechaInicio + "', FechaFinal='" + dtFechaFinal + "' where Clave= '" + txtClave + "' and Mes='" + cmbMes + "'", cn);
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
        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string RegistroConcepto(string txtClave, string descripcion, string clase, string vincularA)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from ConceptoPresupuesto where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into ConceptoPresupuesto (Clave, Descripcion, Clase, VincularA) values ('" + txtClave + "', '" + descripcion + "',  '" + clase + "',  '" + vincularA + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Conceptos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update ConceptoPresupuesto set Descripcion='" + descripcion + "', Clase='" + clase + "', VincularA='" + vincularA + "' where Clave= '" + txtClave + "'", cn);
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
        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string EliminarConcepto(string condominio, string ejercicio, string concepto, string tipo)
        {
            string mensaje = "";
            int contador = 0;

            try
            {

                cmd = new SqlCommand("Delete Presupuesto where Condominio='" + condominio + "' and  Ejercicio='" + ejercicio + "' and  Concepto= '" + concepto + "' and Tipo='"+tipo+"'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro eliminado.";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string RegistroPresupuesto(string Condominio, string ejercicio, string Tipo, string concepto, decimal enero, decimal febrero, decimal marzo, decimal abril, decimal mayo, decimal junio, decimal julio, decimal agosto, decimal septiembre, decimal octubre, decimal noviembre, decimal diciembre, decimal total)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from Presupuesto where Condominio='" + Condominio + "' and Ejercicio='" + ejercicio + "' and Tipo='" + Tipo + "' and Concepto='" + concepto + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into Presupuesto (Condominio, Ejercicio, Tipo, Concepto, Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre, Total) values ('" + Condominio + "', '" + ejercicio + "', '" + Tipo + "', '" + concepto + "', " + enero + ", " + febrero + ", " + marzo + ", " + abril + ", " + mayo + ", " + junio + ", " + julio + ", " + agosto + ", " + septiembre + ", " + octubre + ", " + noviembre + ", " + diciembre + ", " + total + ")", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    if (MessageBox.Show("¿Desea actualizar el registro actual?", "Presupuesto", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        cmd = new SqlCommand("Update Presupuesto set Enero= " + enero + ", Febrero= " + febrero + ", Marzo= " + marzo + ", Abril=" + abril + ", Mayo=" + mayo + ", Junio= " + junio + ", Julio=" + julio + ", Agosto=" + agosto + ", Septiembre=" + septiembre + ", Octubre=" + octubre + ", Noviembre=" + noviembre + ", Diciembre=" + diciembre + ", Total=" + total + " where Condominio='" + Condominio + "' and Ejercicio='" + ejercicio + "' and Tipo='" + Tipo + "' and Concepto='" + concepto + "'", cn);
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
        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string RegistroConcepto2(string txtClave, string descripcion, string clase, string vincularA)
        {
            string mensaje = "";
            int contador = 0;

            try
            {
                cmd = new SqlCommand("select * from ConceptoPresupuesto where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    contador++;
                }
                dr.Close();

                if (contador <= 0)
                {
                    cmd = new SqlCommand("Insert into ConceptoPresupuesto (Clave, Descripcion, Clase, VincularA) values ('" + txtClave + "', '" + descripcion + "',  '" + clase + "',  '" + vincularA + "')", cn);
                    cmd.ExecuteNonQuery();
                    mensaje = "Registro guardado.";

                }

                else if (contador > 0)
                {
                    cmd = new SqlCommand("Update ConceptoPresupuesto set Descripcion='" + descripcion + "', Clase='" + clase + "', VincularA='" + vincularA + "' where Clave= '" + txtClave + "'", cn);
                    cmd.ExecuteNonQuery();

                    mensaje = "Registro modificado.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string RegistroProducto(string txtClave, string producto)
        {
            string mensaje = "";
            int contador = 0;

            try
            {

                cmd = new SqlCommand("Insert into Concepto_Producto (Concepto, ProductoServicio) values ('" + txtClave + "', '" + producto + "')", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro guardado.";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string Eliminarproductoconcepto(string concepto, string producto)
        {
            string mensaje = "";
            int contador = 0;

            try
            {

                cmd = new SqlCommand("delete Concepto_Producto where Concepto='" + concepto + "' and ProductoServicio='" + producto + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro eliminado.";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //_________________________________________________________________________________________________________________________--
        // registrar forma pago 
        public string Eliminarproductoconcepto2(string concepto)
        {
            string mensaje = "";
            int contador = 0;

            try
            {

                cmd = new SqlCommand("delete Concepto_Producto where Concepto='" + concepto + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro eliminado.";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //________________________________________________________________________________________________
        //formas pago Registrados
        public void CargarPeriodo(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from Periodo", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Mes"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //formas pago Registrados
        public void CargarConcepto(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from ConceptoPresupuesto", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Clase"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //formas pago Registrados
        public void CargarConceptoFiltro(DataGridView dgv, string clave)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from ConceptoPresupuesto where Clave like '%" + clave + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Clase"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //formas pago Registrados
        public void CargarConceptoFiltroClase(DataGridView dgv, string clave)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from ConceptoPresupuesto where Clase like '%" + clave + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Clase"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //formas pago Registrados
        public void CargarConceptoFiltroClaseConcep(DataGridView dgv, string clave, string clase)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select * from ConceptoPresupuesto where Clave like '%" + clave + "%' and Clase like '%" + clase + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Clase"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //formas pago Registrados
        public void CargarPresupuesto(DataGridView dgv, string Tipo)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select Distinct P.*, CP.Descripcion, iif (P.Condominio='GLOBAL', 'GLOBAL', C.Descripcion ) as Cond from presupuesto as P, ConceptoPresupuesto as CP, Condominio as C where P.Concepto=CP.Clave and (P.Condominio=(convert(varchar,C.ClaveCondominio)) or P.Condominio='GLOBAL') and P.Tipo='" + Tipo + "' order by P.Ejercicio ", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Ejercicio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Periodo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Concepto"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Cond"].ToString();
                    dgv.Rows[n].Cells[5].Value = item["Condominio"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //formas pago Registrados
        public void CargarPresupuestoFiltro(DataGridView dgv, string Tipo, string condominio, string ejercicio, string concepto)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select P.*, CP.Descripcion, P.Concepto, C.Descripcion as Cond, P.Condominio as ClaveCondominio from Presupuesto as P, ConceptoPresupuesto as CP, Condominio as C where P.Concepto=CP.Clave and P.Condominio=C.ClaveCondominio and P.Tipo='" + Tipo + "' and C.Descripcion like '%" + condominio + "%' and CP.Descripcion like '%" + concepto + "%' and P.Ejercicio like '%" + ejercicio + "%'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Ejercicio"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Periodo"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Concepto"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["Cond"].ToString();
                    dgv.Rows[n].Cells[5].Value = item["ClaveCondominio"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //________________________________________________________________________________________________
        //formas pago Registrados
        public void CargarConceptoPresupuesto(DataGridView dgv, string concepto)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("Select CP.Concepto, PS.Descripcion, CP.ProductoServicio from Concepto_Producto as CP, ProductosServicios as PS where CP.ProductoServicio=PS.ClaveProducto and CP.Concepto='" + concepto + "'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Concepto"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["ProductoServicio"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar formas seleccionado
        public void ConsultaPeriodoSeleccionado(string txtClave, string mes, ComboBox cmbMes, Guna2DateTimePicker dtFechaInicio,Guna2DateTimePicker dtFechaFinal)
        {
            try
            {
                cmd = new SqlCommand("Select * from Periodo where Clave='" + txtClave + "' and Mes='" + mes + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cmbMes.Text = dr["Mes"].ToString();
                    dtFechaInicio.Text = dr["FechaInicia"].ToString();
                    dtFechaFinal.Text = dr["FechaFinal"].ToString();
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
        //Mostrar formas seleccionado
        public void ConsultaPresupuestoSeleccionado(string txtEjercicio, string Concepto, string condominio, TextBox txtConcepto, TextBox enero, TextBox febrero, TextBox marzo, TextBox abril, TextBox mayo, TextBox junio, TextBox julio, TextBox agosto, TextBox septiembre, TextBox octubre, TextBox noviembre, TextBox diciembre, TextBox total)
        {
            try
            {
                cmd = new SqlCommand("Select * from Presupuesto where Condominio='" + condominio + "' and Ejercicio='" + txtEjercicio + "' and Concepto='" + Concepto + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtConcepto.Text = dr["Concepto"].ToString();
                    enero.Text = dr["Enero"].ToString();
                    febrero.Text = dr["Febrero"].ToString();
                    marzo.Text = dr["Marzo"].ToString();
                    abril.Text = dr["Abril"].ToString();
                    mayo.Text = dr["Mayo"].ToString();
                    junio.Text = dr["Junio"].ToString();
                    julio.Text = dr["Julio"].ToString();
                    agosto.Text = dr["Agosto"].ToString();
                    septiembre.Text = dr["Septiembre"].ToString();
                    octubre.Text = dr["Octubre"].ToString();
                    noviembre.Text = dr["Noviembre"].ToString();
                    diciembre.Text = dr["Diciembre"].ToString();
                    total.Text = dr["Total"].ToString();
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
        //Mostrar formas seleccionado
        public void ConsultaConceptoSeleccionado(string txtClave, TextBox descripcion, ComboBox clase, TextBox concepto)
        {
            try
            {
                cmd = new SqlCommand("Select * from ConceptoPresupuesto where Clave='" + txtClave + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    descripcion.Text = dr["Descripcion"].ToString();
                    clase.Text = dr["Clase"].ToString();
                    concepto.Text = dr["VincularA"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarConceptos(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select CI.* from ConceptosIngreso as CI where not exists (select VincularA from ConceptoPresupuesto as CP where CI.Clave=CP.VincularA)", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarCondomini(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select Descripcion from Condominio", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarCondominiCerrar(ComboBox cb, string Tipo, string Ejercicio)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select C.Descripcion from Condominio as C, Presupuesto as P where convert(varchar,C.CLaveCOndominio)=P.Condominio and P.Tipo='" + Tipo + "' and P.Ejercicio='" + Ejercicio + "' group by C.Descripcion", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarConceptoCerrar(ComboBox cb, string Tipo, string Ejercicio, string ClaveCond)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select CP.Descripcion from ConceptoPresupuesto as CP, Condominio as C, Presupuesto as P where convert(varchar,C.CLaveCOndominio)=P.Condominio and CP.CLave=P.Concepto and C.ClaveCondominio='" + ClaveCond + "' and P.Tipo='" + Tipo + "' and P.Ejercicio='" + Ejercicio + "' group by CP.Descripcion", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarCondomini2(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("GLOBAL");
            cmd = new SqlCommand("Select Descripcion from Condominio", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarCondomini3(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select Descripcion from Condominio", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarConceptos2(ComboBox cb, string Concepto)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select CI.* from ConceptosIngreso as CI where Clave='" + Concepto + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
            cmd = new SqlCommand("Select CI.* from ConceptosIngreso as CI where not exists (select VincularA from ConceptoPresupuesto as CP where CI.Clave=CP.VincularA)", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[1].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarPeriodo(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("select (Convert(varchar, P.Clave) + '-' + P.Mes) as Periodo from Periodo as P", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarConcepto(ComboBox cb, string Ejercicio, string condominio)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select CP.Descripcion from ConceptoPresupuesto as CP where CP.Clase='Ingreso' and not exists (select P.Concepto from Presupuesto as P where CP.Clave=P.Concepto and P.Tipo='Ingreso' and P.Ejercicio='" + Ejercicio + "' and P.Condominio='" + condominio + "')", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarConcepto2(ComboBox cb, string Ejercicio, string condominio)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select CP.Descripcion from ConceptoPresupuesto as CP where CP.Clase='Egreso' and not exists (select P.Concepto from Presupuesto as P where CP.Clave=P.Concepto and P.Tipo='Egreso' and P.Ejercicio='" + Ejercicio + "' and P.Condominio='" + condominio + "')", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarConcepto3(ComboBox cb, string Ejercicio, string concepto)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select CP.Descripcion from ConceptoPresupuesto as CP where CP.Clase='Ingreso' and CP.Clave='" + concepto + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cmd = new SqlCommand("Select CP.Descripcion from ConceptoPresupuesto as CP where CP.Clase='Ingreso' and not exists (select P.Concepto from Presupuesto as P where CP.Clave=P.Concepto and P.Tipo='Ingreso' and P.Ejercicio='" + Ejercicio + "')", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarConcepto4(ComboBox cb, string Ejercicio, string concepto)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select CP.Descripcion from ConceptoPresupuesto as CP where CP.Clase='Egreso' and  CP.Clave='" + concepto + "'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cmd = new SqlCommand("Select CP.Descripcion from ConceptoPresupuesto as CP where CP.Clase='Egreso' and not exists (select P.Concepto from Presupuesto as P where CP.Clave=P.Concepto and P.Tipo='Egreso' and P.Ejercicio='" + Ejercicio + "')", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarProducto(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select PS.* from ProductosServicios as PS where not exists (select ProductoServicio from Concepto_Producto as CP where PS.ClaveProducto=CP.ProductoServicio)", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[2].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarEjercicio(ComboBox cb)
        {
            cb.Items.Clear();
            cmd = new SqlCommand("Select Clave from Periodo group by Clave", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //____________________________________________________________________________________________________________________________________________
        public void SeleccionarEjercicio2(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("TODOS");
            cmd = new SqlCommand("Select Clave from Periodo group by Clave", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cb.Items.Add(dr[0].ToString());
            }
            dr.Close();
        }
        //______________________________________________________________________________________________________________________________
        public string[] InformacionConcepto(string Nombre)
        {
            cmd = new SqlCommand("Select Clave from ConceptosIngreso where Descripcion = '" + Nombre + "'", cn);
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
        //______________________________________________________________________________________________________________________________
        public string[] InformacionCondominio(string Nombre)
        {
            cmd = new SqlCommand("Select ClaveCondominio from Condominio where Descripcion = '" + Nombre + "'", cn);
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
        //______________________________________________________________________________________________________________________________
        public string[] InformacionCondominio2(string Nombre)
        {
            cmd = new SqlCommand("Select Descripcion from Condominio where ClaveCondominio = '" + Nombre + "'", cn);
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
        //______________________________________________________________________________________________________________________________
        public string[] InformacionConceptoPresupuesto(string Nombre)
        {
            cmd = new SqlCommand("Select Clave from ConceptoPresupuesto where Descripcion = '" + Nombre + "'", cn);
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
        //______________________________________________________________________________________________________________________________
        public string[] InformacionConceptoPresupuesto2(string Nombre)
        {
            cmd = new SqlCommand("Select Descripcion from ConceptoPresupuesto where Clave = '" + Nombre + "'", cn);
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
        //______________________________________________________________________________________________________________________________
        public string[] InformacionPrductos(string Nombre)
        {
            cmd = new SqlCommand("Select ClaveProducto from ProductosServicios where Descripcion = '" + Nombre + "'", cn);
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
        //______________________________________________________________________________________________________________________________
        public string[] InformacionConcepto2(string Nombre)
        {
            cmd = new SqlCommand("Select Descripcion from ConceptosIngreso where Clave = '" + Nombre + "'", cn);
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

        //___________________________________________________________________________________________________________________
        public void Monto(KeyPressEventArgs e)
        {
            try
            {
                if (char.IsNumber(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (char.IsSeparator(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                    e.Handled = true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //_____________________________________________________________________________________________________
        //Mostrar formas seleccionado
        public void ConsultaPresupuesto2(string ejercicio, string concepto, string Tipo, string Condominio, TextBox txtDescripcion)
        {
            try
            {
                cmd = new SqlCommand("select P.Total, CP.* from Presupuesto as P, ConceptoPresupuesto as CP where P.Concepto=CP.Clave and CP.Clave='" + concepto + "' and P.Ejercicio='" + ejercicio + "' and P.Tipo='" + Tipo + "' and P.Condominio='" + Condominio + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Total"].ToString();

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
        //Mostrar formas seleccionado
        public void ConsultaConcepto(TextBox txtDescripcion)
        {
            try
            {
                cmd = new SqlCommand("Select Concepto from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["Concepto"].ToString();

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
        //Mostrar formas seleccionado
        public void ConsultaConceptoE(TextBox txtDescripcion)
        {
            try
            {
                cmd = new SqlCommand("Select ConceptoExt from DatosEmpresa", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtDescripcion.Text = dr["ConceptoExt"].ToString();

                }
                dr.Close();

            }
            catch (Exception ex)
            {
                dr.Close();
                MessageBox.Show("Error" + ex.ToString());
            }
        }
        //___________________________________________________________
        public string RegistroFormaPago2(string txtClave, decimal Importe)
        {
            string mensaje = string.Empty;

            try
            {
                cmd = new SqlCommand("Update Condominio set  Importe=" + Importe + ", Metodo='Manual' where ClaveCondominio= '" + txtClave + "'", cn);
                cmd.ExecuteNonQuery();

                mensaje = "Presupuesto Cerrado.";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //___________________________________________________________
        public string RegistroFormaPago3(string txtClave, decimal Importe)
        {
            string mensaje = string.Empty;

            try
            {
                cmd = new SqlCommand("Update Condominio set ImporteE=" + Importe + ", MetodoE='Manual' where ClaveCondominio= '" + txtClave + "'", cn);
                cmd.ExecuteNonQuery();

                mensaje = "Presupuesto Cerrado.";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;
        }
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string EliminarDivisa(string txtClaveDivisa)
        {
            string mensaje = string.Empty;
            try
            {
                cmd = new SqlCommand("Delete Periodo where Clave='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
            }
            catch (Exception)
            {
                mensaje = "El registro esta en uso, no es posible eliminar";
            }
            return mensaje;
        }
        //_________________________________________________________________________________________________________________________--
        // registrar divisa 
        public string EliminarDivisa2(string txtClaveDivisa)
        {
            string mensaje = string.Empty;
            try
            {
                cmd = new SqlCommand("Delete ConceptoPresupuesto where Clave='" + txtClaveDivisa + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro Eliminado";
            }
            catch (Exception)
            {
                mensaje = "El registro esta en uso, no es posible eliminar";
            }
            return mensaje;
        }
    }
}
