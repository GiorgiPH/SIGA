using Guna.UI2.WinForms;
using PV.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PV.Clases.Polizas
{
    class DBPolizas
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        public static string ObtenerCn()
        {
            return Settings.Default.ControlCondominiosConnectionString;
        }
        public DBPolizas()
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
        //----------------------------- obtiene el consecutivo de condiminios
        public void Catalogo_DefPoliza(string TipoPoliza, Label clave)
        {
            try
            {
                //clave.Items.Clear();
                cmd = new SqlCommand("select top 1 Case Count(Clave) when  null then 0 else Count(Clave) end as conse from DEFPOLIZA where TipoPolzOpcion='Definiciones Condominios'", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //clave.Items.Add(dr[0].ToString());
                    clave.Text = dr["conse"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }

        //----------------------------- obtiene el consecutivo de gasto p
        public void Catalogo_DefPolizaGP(string TipoPoliza, Label clave)
        {
            try
            {
                //clave.Items.Clear();
                cmd = new SqlCommand("select top 1 Case Count(Clave) when  null then 0 else Count(Clave) end as conse from DEFPOLIZA where TipoPolzOpcion='Definiciones Gastos Personales'", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //clave.Items.Add(dr[0].ToString());
                    clave.Text = dr["conse"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }


        public void Catalogo_DefPolizaInventarios(string TipoPoliza, Label clave)
        {
            try
            {
                //clave.Items.Clear();
                cmd = new SqlCommand("select top 1 Case Count(Clave) when  null then 0 else Count(Clave) end as conse from DEFPOLIZA where TipoPolzOpcion='Definiciones Inventarios'", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //clave.Items.Add(dr[0].ToString());
                    clave.Text = dr["conse"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        public void Catalogo_DefPolizaCompras(string TipoPoliza, Label clave)
        {
            try
            {
                //clave.Items.Clear();
                cmd = new SqlCommand("select top 1 Case Count(Clave) when  null then 0 else Count(Clave) end as conse from DEFPOLIZA where TipoPolzOpcion='Definiciones Compras'", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //clave.Items.Add(dr[0].ToString());
                    clave.Text = dr["conse"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }



        //----------------------------- obtiene el tipo de definicion de poliza
        public void Catalogo_DefPoliza1(string TipoPoliza, ComboBox clave)
        {
            try
            {
                //clave.Items.Clear();
                cmd = new SqlCommand("select clave from Catalogo_DefPoliza  where TipoPoliza like '%" + TipoPoliza + "%'", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //clave.Items.Add(dr[0].ToString());
                    clave.Text = dr["clave"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }


        //----------------------------- obtiene el tipo de definicion de poliza
        public void Catalogo_DefPoliza2(int clave, string TipoPoliza, ComboBox txtdescripcion)
        {
            try
            {
                cmd = new SqlCommand("select * from Catalogo_DefPoliza  where Clave = '" + clave + "' and TipoPoliza like '%" + TipoPoliza + "%'", cn);
                //cmd = new SqlCommand("select * from  Catalogo_DefPoliza  where Clave = '" + clave + "' and TipoPoliza='" + TipoPoliza + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtdescripcion.Text = dr["Descripcion"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }

        //-----------------------------obtiene ls tipos de cargos
        public void Catalogo_cargosyabonos(ComboBox cmbcargosyabono)
        {
            try
            {

                cmbcargosyabono.Items.Clear();
                cmd = new SqlCommand("select Descripcion from Catalogo_CargosYAbonos where Grupo='Condiminios'", cn);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmbcargosyabono.Items.Add(dr[0].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }

        //------------------------
        public void Catalogo_cargosyabonosGP(ComboBox cmbcargosyabono)
        {
            try
            {

                cmbcargosyabono.Items.Clear();
                cmd = new SqlCommand("select Descripcion from Catalogo_CargosYAbonos where Grupo='Gastos Personales'", cn);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmbcargosyabono.Items.Add(dr[0].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }

        public void Catalogo_cargosyabonosCompras(ComboBox cmbcargosyabono)
        {
            try
            {

                cmbcargosyabono.Items.Clear();
                cmd = new SqlCommand("select Descripcion from Catalogo_CargosYAbonos where Grupo='Compras'", cn);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmbcargosyabono.Items.Add(dr[0].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        public void Catalogo_cargosyabonosInventarios(ComboBox cmbcargosyabono)
        {
            try
            {

                cmbcargosyabono.Items.Clear();
                cmd = new SqlCommand("select Descripcion from Catalogo_CargosYAbonos where Grupo='Inventarios'", cn);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmbcargosyabono.Items.Add(dr[0].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }


        public void Catalogo_cargosyabonos2(string Descripcion, TextBox Clave)
        {
            try
            {
                cmd = new SqlCommand("select Clave from Catalogo_CargosYAbonos where Descripcion like '%" + Descripcion + "%' ", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Clave.Text = dr["Clave"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                //  MessageBox.Show("errror" + ex.ToString());
            }
        }
        public void Catalogo_cargosyabonos3(string clave, ComboBox Descripcion)
        {
            try
            {
                Descripcion.Items.Clear();
                cmd = new SqlCommand("select Descripcion from Catalogo_CargosYAbonos where Clave = '" + clave + "'", cn);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Descripcion.Items.Add(dr[0].ToString());
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }


        public string RegistroDefPoliza(string clave, string nombre, string Estatus, string separador, string TipoSeparador, string TipoPoliza, string DiarioPoliza, string TipoCargo1, string Cargo1, string TipoCargo2, string Cargo2, string TipoCargo3, string Cargo3, string TipoCargo4, string Cargo4, string TipoCargo5, string Cargo5, string TipoCargo6, string Cargo6, string TipoCargo7, string Cargo7, string TipoAbono1, string Abono1, string TipoAbono2, string Abono2, string TipoAbono3, string Abono3, string TipoAbono4, string Abono4, string Notas, string TipoPolzOpcion)
        {
            string mensaje = "";


            try
            {


                //    cmd = new SqlCommand("Insert into Anticipo (Folio, ClavePropietario, Caja, Fecha, FormaPago, Concepto, Referencia, CuentaBancaria, NumeroOperacion, Importe, Saldo, Divisa, TipoCambio ) values ('" + txtfolio + "', '" + txtClavePropietario + "',  '" + txtCaja + "',  '" + txtFecha + "', '" + txtFormaPago + "',  '" + txtConcepto + "',  '" + txtReferencia + "', '" + txtCuentaBancaria + "',  '" + txtNumeroOperacion + "',  " + importe + ", " + importe + ", '" + Divisa + "', '" + TipoCambio + "')", cn);
                cmd = new SqlCommand("insert into DEFPOLIZA(Clave, Nombre, Estatus, Separador, TipoSeparador, TipoPoliza, DiarioPoliza, TipoCargo1, Cargo1, TipoCargo2, Cargo2, TipoCargo3, Cargo3, TipoCargo4, Cargo4, TipoCargo5, Cargo5,TipoCargo6, Cargo6,TipoCargo7, Cargo7,TipoAbono1, Abono1, TipoAbono2, Abono2, TipoAbono3, Abono3, TipoAbono4, Abono4, Notas, TipoPolzOpcion) values('" + clave + "', '" + nombre + "', '" + Estatus + "','" + separador + "','" + TipoSeparador + "','" + TipoPoliza + "','" + DiarioPoliza + "','" + TipoCargo1 + "','" + Cargo1 + "','" + TipoCargo2 + "','" + Cargo2 + "','" + TipoCargo3 + "','" + Cargo3 + "','" + TipoCargo4 + "','" + Cargo4 + "','" + TipoCargo5 + "','" + Cargo5 + "','" + TipoCargo6 + "','" + Cargo6 + "','" + TipoCargo7 + "','" + Cargo7 + "','" + TipoAbono1 + "','" + Abono1 + "','" + TipoAbono2 + "','" + Abono2 + "','" + TipoAbono3 + "','" + Abono3 + "','" + TipoAbono4 + "','" + Abono4 + "','" + Notas + "','" + TipoPolzOpcion + "')", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro guardado.";



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //-----------Condiminios

        public void Consultar_DefinicionesPoliza(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select Consecutivo,Clave,Nombre from DEFPOLIZA as DP where TipoPolzOpcion='Definiciones Condominios' ", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Nombre"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar las definiciones");
            }
        }
        //------------gastos personales

        public void Consultar_DefinicionesPolizaGP(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select Consecutivo,Clave,Nombre from DEFPOLIZA as DP where TipoPolzOpcion='Definiciones Gastos Personales'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Nombre"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar las definiciones");
            }
        }
        //------------gastos personales

        public void Consultar_DefinicionesPolizaCompras(DataGridView dgv, string TipoPoliza)
        {
            try
            {
                if (TipoPoliza == "Compras")
                {


                    dgv.Rows.Clear();
                    da = new SqlDataAdapter("select Consecutivo,Clave,Nombre from DEFPOLIZA as DP where TipoPolzOpcion='Definiciones Compras' and nombre in ('Define Póliza Compras Almacén','Define Póliza Compras Gastos','Define Póliza Egresos ','Define Póliza Anticipos Proveedores','Define Póliza Aplicación Anticipos Proveedores')", cn);
                    dt = new DataTable();
                    da.Fill(dt);
                }
                if (TipoPoliza == "Egresos")
                {


                    dgv.Rows.Clear();
                    da = new SqlDataAdapter("select Consecutivo,Clave,Nombre from DEFPOLIZA as DP where TipoPolzOpcion='Definiciones Compras' and nombre in ('Define Póliza Egresos ','Define Póliza Anticipos','Define Póliza Aplicación Anticipos') ", cn);
                    dt = new DataTable();
                    da.Fill(dt);
                }

                if (TipoPoliza == "Salida Almacen")
                {


                    dgv.Rows.Clear();
                    da = new SqlDataAdapter("select Consecutivo,Clave,Nombre from DEFPOLIZA as DP where TipoPolzOpcion='Definiciones Compras' and nombre in ('Define Póliza Salidas Almacén') ", cn);
                    dt = new DataTable();
                    da.Fill(dt);
                }

                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Nombre"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar las definiciones");
            }
        }
        public void Consultar_DefinicionesPolizaInv(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select Consecutivo,Clave,Nombre from DEFPOLIZA as DP where TipoPolzOpcion='Definiciones Inventarios'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Nombre"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar las definiciones");
            }
        }
        public void consultarDefeniciónes(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select Consecutivo,Clave,Nombre from DEFPOLIZA as DP", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[0].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar las definiciones");
            }
        }
        public string EditarDefPoliza(string clave, string nombre, string Estatus, string separador, string TipoSeparador, string TipoPoliza, string DiarioPoliza, string TipoCargo1, string Cargo1, string TipoCargo2, string Cargo2, string TipoCargo3, string Cargo3, string TipoCargo4, string Cargo4, string TipoCargo5, string Cargo5, string TipoCargo6, string Cargo6, string TipoCargo7, string Cargo7, string TipoAbono1, string Abono1, string TipoAbono2, string Abono2, string TipoAbono3, string Abono3, string TipoAbono4, string Abono4, string Notas, string consecutivo, string TipoPolz)
        {
            string mensaje = "";
            try
            {
                cmd = new SqlCommand("update DEFPOLIZA set Estatus='" + Estatus + "',Nombre='" + nombre + "' , Separador='" + separador + "', TipoSeparador='" + TipoSeparador + "', TipoPoliza='" + TipoPoliza + "', DiarioPoliza='" + DiarioPoliza + "', TipoCargo1='" + TipoCargo1 + "', Cargo1='" + Cargo1 + "', TipoCargo2='" + TipoCargo2 + "', Cargo2='" + Cargo2 + "', TipoCargo3='" + TipoCargo3 + "', Cargo3='" + Cargo3 + "', TipoCargo4='" + TipoCargo4 + "', Cargo4='" + Cargo4 + "', TipoCargo5='" + TipoCargo5 + "', Cargo5='" + Cargo5 + "', TipoCargo6='" + TipoCargo6 + "', Cargo6='" + Cargo6 + "', TipoCargo7='" + TipoCargo7 + "', Cargo7='" + Cargo7 + "',TipoAbono1='" + TipoAbono1 + "', Abono1='" + Abono1 + "', TipoAbono2='" + TipoAbono2 + "', Abono2='" + Abono2 + "', TipoAbono3='" + TipoAbono3 + "', Abono3='" + Abono3 + "', TipoAbono4='" + TipoAbono4 + "', Abono4='" + Abono4 + "', Notas ='" + Notas + "' where clave='" + clave + "' and TipoPolzOpcion='" + TipoPolz + "'", cn);
                cmd.ExecuteNonQuery();
                mensaje = "Registro guardado.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }



        ////APARTIR LA CLASE QUE SE OCUPA PARA FORMULARIO GENERAR POLIZAS
        ///
           //----------------------------- obtiene el tipo de definicion de poliza
        public void Catalogo_DefPolizaGen(string TipoPoliza, ComboBox clave)
        {
            try
            {

                clave.Items.Clear();
                cmd = new SqlCommand("select clave from Catalogo_DefPoliza  where TipoPoliza like '%" + TipoPoliza + "%'", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    clave.Items.Add(dr[0].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        //----------------------------- obtiene el tipo de definicion de poliza
        public void Catalogo_DefPolizaGen2(int clave, string TipoPoliza, Guna2TextBox txtdescripcion, Guna2TextBox Estatus, Guna2TextBox TipoCargo1, Guna2TextBox TipoCargo2, Guna2TextBox TipoCargo3, Guna2TextBox TipoCargo4, Guna2TextBox TipoCargo5, Guna2TextBox TipoAbono1, Guna2TextBox TipoAbono2, Guna2TextBox TipoAbono3, Guna2TextBox TipoAbono4, ComboBox tipopoliza, Guna2TextBox DiarioP, Guna2TextBox TipoSeparador)
        {
            try
            {
                cmd = new SqlCommand("select Nombre,Estatus, Cargo1,Cargo2,Cargo3,Cargo4,Cargo5,Abono1,Abono2,Abono3,Abono4,TipoPoliza,DiarioPoliza,TipoSeparador from DEFPOLIZA where clave='" + clave + "' and TipoPolzOpcion like '%" + TipoPoliza + "%'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    txtdescripcion.Text = dr["Nombre"].ToString();
                    Estatus.Text = dr["Estatus"].ToString();
                    TipoCargo1.Text = dr["Cargo1"].ToString();
                    TipoCargo2.Text = dr["Cargo2"].ToString();
                    TipoCargo3.Text = dr["Cargo3"].ToString();
                    TipoCargo4.Text = dr["Cargo4"].ToString();
                    TipoCargo5.Text = dr["Cargo5"].ToString();
                    TipoAbono1.Text = dr["Abono1"].ToString();
                    TipoAbono2.Text = dr["Abono2"].ToString();
                    TipoAbono3.Text = dr["Abono3"].ToString();
                    TipoAbono4.Text = dr["Abono4"].ToString();
                    tipopoliza.Text = dr["TipoPoliza"].ToString();
                    DiarioP.Text = dr["DiarioPoliza"].ToString();
                    TipoSeparador.Text = dr["TipoSeparador"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        //----------------------------- 
        public string RegistroGeneracionPoliza(string Clave, string Nombre, string Estatus, string Cargo1, string Cargo2, string Cargo3, string Cargo4, string Cargo5, string Abono1, string Abono2, string Abono3, string Abono4, string Periodo, string Diainicio, string DiaFin, string NoPoliza, string TipoPoliza, string DiarioPoliza, string Resumida, string PorDias, string Documento, string Concepto, string Exportar, string Separador, string TipoPolzOpcion)
        {
            string mensaje = "";
            try
            {
                cmd = new SqlCommand("insert into REGPOLIZA (Clave,Nombre,Estatus,Cargo1,Cargo2,Cargo3,Cargo4,Cargo5,Abono1,Abono2,Abono3,Abono4,Periodo,Diainicio,DiaFin,NoPoliza,TipoPoliza,DiarioPoliza,Resumida,PorDias,Documento,Concepto,Exportar,Separador,TipoPolzOpcion) Values ('" + Clave + "','" + Nombre + "','" + Estatus + "','" + Cargo1 + "','" + Cargo2 + "','" + Cargo3 + "','" + Cargo4 + "','" + Cargo5 + "','" + Abono1 + "','" + Abono2 + "','" + Abono3 + "','" + Abono4 + "','" + Periodo + "','" + Diainicio + "','" + DiaFin + "','" + NoPoliza + "','" + TipoPoliza + "','" + DiarioPoliza + "','" + Resumida + "','" + PorDias + "','" + Documento + "','" + Concepto + "','" + Exportar + "','" + Separador + "','" + TipoPolzOpcion + "')", cn);
                cmd.ExecuteNonQuery();
                //             mensaje = "Registro guardado.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }
        //----------------------------- o
        public void ConsultaDefPoliza(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select Consecutivo,Clave, Nombre, Estatus, TipoPoliza  from DEFPOLIZA where TipoPolzOpcion='Definiciones Condominios'", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Estatus"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["TipoPoliza"].ToString();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar las definiciones");
            }
        }
        //----------------------------- o
        public void ConsultaDefPolizaGP(DataGridView dgv)
        {
            try
            {
                dgv.Rows.Clear();
                da = new SqlDataAdapter("select Consecutivo,Clave, Nombre, Estatus, TipoPoliza  from DEFPOLIZA  where TipoPolzOpcion='Definiciones Gastos Personales' ", cn);
                dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Estatus"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["TipoPoliza"].ToString();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar las definiciones");
            }
        }
        public void ConsultaDefPolizaInventarios(DataGridView dgv)
        {
            try
            {



                dgv.Rows.Clear();
                da = new SqlDataAdapter("select  Consecutivo,Clave, Nombre, Estatus, TipoPoliza from DEFPOLIZA as DP where TipoPolzOpcion='Definiciones Inventarios' and nombre in ('Define Póliza Entradas Inventariables','Define Póliza Salidas Inventariables','Define Póliza Traspasos Inventariables')", cn);
                dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Estatus"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["TipoPoliza"].ToString();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar las definiciones");

            }
        }
        public void ConsultaDefPolizaCompras(DataGridView dgv, string TipoPoliza)
        {
            try
            {


                if (TipoPoliza == "Polizas Compras")
                {


                    dgv.Rows.Clear();
                    da = new SqlDataAdapter("select  Consecutivo,Clave, Nombre, Estatus, TipoPoliza from DEFPOLIZA as DP where TipoPolzOpcion='Definiciones Compras' and nombre in ('Define Póliza Compras Almacén','Define Póliza Compras Gastos','Define Póliza Egresos ','Define Póliza Anticipos Proveedores','Define Póliza Aplicación Anticipos Proveedores')", cn);
                    dt = new DataTable();
                    da.Fill(dt);
                }
                //if (TipoPoliza == "Egresos")
                //{


                //    dgv.Rows.Clear();
                //    da = new SqlDataAdapter("select  Consecutivo,Clave, Nombre, Estatus, TipoPoliza from DEFPOLIZA as DP where TipoPolzOpcion='Definiciones Compras' and nombre in ('Define Póliza Egresos ','Define Póliza Anticipos','Define Póliza Aplicación Anticipos') ", cn);
                //    dt = new DataTable();
                //    da.Fill(dt);
                //}

                else
                {


                    dgv.Rows.Clear();
                    da = new SqlDataAdapter("select  Consecutivo,Clave, Nombre, Estatus, TipoPoliza from DEFPOLIZA as DP where TipoPolzOpcion='Definiciones Compras' and nombre in ('Define Póliza Salidas Almacén') ", cn);
                    dt = new DataTable();
                    da.Fill(dt);
                }
                foreach (DataRow item in dt.Rows)
                {
                    int n = dgv.Rows.Add();
                    dgv.Rows[n].Cells[0].Value = item["Consecutivo"].ToString();
                    dgv.Rows[n].Cells[1].Value = item["Clave"].ToString();
                    dgv.Rows[n].Cells[2].Value = item["Nombre"].ToString();
                    dgv.Rows[n].Cells[3].Value = item["Estatus"].ToString();
                    dgv.Rows[n].Cells[4].Value = item["TipoPoliza"].ToString();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar las definiciones");

            }
        }


        //-----------------------
        public void CargarInfoPoliza(string Conse, string clave, string Nombre, Guna2TextBox C1, Guna2TextBox C2, Guna2TextBox C3, Guna2TextBox C4, Guna2TextBox C5, Guna2TextBox abono1, Guna2TextBox abono2, Guna2TextBox abono3, Guna2TextBox abono4, Guna2TextBox tiposeparador, ComboBox TipoPoliza, Guna2TextBox DiarioP, string TipoPolzOpcion)
        {
            try
            {
                cmd = new SqlCommand("select clave,nombre,Estatus,Cargo1,Cargo2,Cargo3,Cargo4,Cargo5,Abono1,Abono2,Abono3,Abono4,TipoSeparador,TipoPoliza,DiarioPoliza from DEFPOLIZA where TipoPolzOpcion='Definiciones Condominios' and Consecutivo = '" + Conse + "' and  Clave = '" + clave + "' and Nombre = '" + Nombre + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    C1.Text = dr["Cargo1"].ToString();
                    C2.Text = dr["Cargo2"].ToString();
                    C3.Text = dr["Cargo3"].ToString();
                    C4.Text = dr["Cargo4"].ToString();
                    C5.Text = dr["Cargo5"].ToString();
                    abono1.Text = dr["abono1"].ToString();
                    abono2.Text = dr["abono2"].ToString();
                    abono3.Text = dr["abono3"].ToString();
                    abono4.Text = dr["abono4"].ToString();
                    tiposeparador.Text = dr["TipoSeparador"].ToString();
                    TipoPoliza.Items.Add(dr["TipoPoliza"].ToString());
                    DiarioP.Text = dr["DiarioPoliza"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        //----------------------------------------------------------
        public void CargarInfoPolizaGP(string Conse, string clave, string Nombre, Guna2TextBox C1, Guna2TextBox C2, Guna2TextBox C3, Guna2TextBox C4, Guna2TextBox C5, Guna2TextBox abono1, Guna2TextBox abono2, Guna2TextBox abono3, Guna2TextBox abono4, Guna2TextBox tiposeparador, ComboBox TipoPoliza, Guna2TextBox DiarioP, string TipoPolzOpcion)
        {
            try
            {
                cmd = new SqlCommand("select clave,nombre,Estatus,Cargo1,Cargo2,Cargo3,Cargo4,Cargo5,Abono1,Abono2,Abono3,Abono4,TipoSeparador,TipoPoliza,DiarioPoliza from DEFPOLIZA where TipoPolzOpcion='Definiciones Gastos Personales' and  Consecutivo = '" + Conse + "' and  Clave = '" + clave + "' and  Nombre = '" + Nombre + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    C1.Text = dr["Cargo1"].ToString();
                    C2.Text = dr["Cargo2"].ToString();
                    C3.Text = dr["Cargo3"].ToString();
                    C4.Text = dr["Cargo4"].ToString();
                    C5.Text = dr["Cargo5"].ToString();
                    abono1.Text = dr["abono1"].ToString();
                    abono2.Text = dr["abono2"].ToString();
                    abono3.Text = dr["abono3"].ToString();
                    abono4.Text = dr["abono4"].ToString();
                    tiposeparador.Text = dr["TipoSeparador"].ToString();
                    TipoPoliza.Items.Add(dr["TipoPoliza"].ToString());
                    DiarioP.Text = dr["DiarioPoliza"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        public void CargarInfoPolizaInventarios(string Conse, string clave, string Nombre, Guna2TextBox C1, Guna2TextBox C2, Guna2TextBox C3, Guna2TextBox C4, Guna2TextBox C5, Guna2TextBox abono1, Guna2TextBox abono2, Guna2TextBox abono3, Guna2TextBox abono4, Guna2TextBox tiposeparador, ComboBox TipoPoliza, Guna2TextBox DiarioP, string TipoPolzOpcion)
        {
            try
            {
                cmd = new SqlCommand("select clave,nombre,Estatus,Cargo1,Cargo2,Cargo3,Cargo4,Cargo5,Abono1,Abono2,Abono3,Abono4,TipoSeparador,TipoPoliza,DiarioPoliza from DEFPOLIZA where TipoPolzOpcion='Definiciones Inventarios' and  Consecutivo = '" + Conse + "' and  Clave = '" + clave + "' and  Nombre = '" + Nombre + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    C1.Text = dr["Cargo1"].ToString();
                    C2.Text = dr["Cargo2"].ToString();
                    C3.Text = dr["Cargo3"].ToString();
                    C4.Text = dr["Cargo4"].ToString();
                    C5.Text = dr["Cargo5"].ToString();
                    abono1.Text = dr["abono1"].ToString();
                    abono2.Text = dr["abono2"].ToString();
                    abono3.Text = dr["abono3"].ToString();
                    abono4.Text = dr["abono4"].ToString();
                    tiposeparador.Text = dr["TipoSeparador"].ToString();
                    TipoPoliza.Items.Add(dr["TipoPoliza"].ToString());
                    DiarioP.Text = dr["DiarioPoliza"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }

        //----------------------------------------------------------
        public void CargarInfoPolizaCompras(string Conse, string clave, string Nombre, Guna2TextBox C1, Guna2TextBox C2, Guna2TextBox C3, Guna2TextBox C4, Guna2TextBox C5, Guna2TextBox C6, Guna2TextBox C7, Guna2TextBox abono1, Guna2TextBox abono2, Guna2TextBox abono3, Guna2TextBox abono4, Guna2TextBox tiposeparador, ComboBox TipoPoliza, Guna2TextBox DiarioP, string TipoPolzOpcion)
        {
            try
            {
                cmd = new SqlCommand("select clave,nombre,Estatus,Cargo1,Cargo2,Cargo3,Cargo4,Cargo5,Cargo6,Cargo7,Abono1,Abono2,Abono3,Abono4,TipoSeparador,TipoPoliza,DiarioPoliza from DEFPOLIZA where TipoPolzOpcion='Definiciones Compras' and  Consecutivo = '" + Conse + "' and  Clave = '" + clave + "' and  Nombre = '" + Nombre + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    C1.Text = dr["Cargo1"].ToString();
                    C2.Text = dr["Cargo2"].ToString();
                    C3.Text = dr["Cargo3"].ToString();
                    C4.Text = dr["Cargo4"].ToString();
                    C5.Text = dr["Cargo5"].ToString();
                    C6.Text = dr["Cargo6"].ToString();
                    C7.Text = dr["Cargo7"].ToString();
                    abono1.Text = dr["abono1"].ToString();
                    abono2.Text = dr["abono2"].ToString();
                    abono3.Text = dr["abono3"].ToString();
                    abono4.Text = dr["abono4"].ToString();
                    tiposeparador.Text = dr["TipoSeparador"].ToString();
                    TipoPoliza.Items.Add(dr["TipoPoliza"].ToString());
                    TipoPoliza.Text = dr["TipoPoliza"].ToString();
                    DiarioP.Text = dr["DiarioPoliza"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        //----------------------------------------------------------
        //-----------------------
        public void ConsultarInfoPoliza(string Conse, string clave, string Nombre, TextBox C1, TextBox C2, TextBox C3, TextBox C4, TextBox C5, TextBox C6, TextBox C7, ComboBox TC1, ComboBox TC2, ComboBox TC3, ComboBox TC4, ComboBox TC5, ComboBox TC6, ComboBox TC7, TextBox abono1, TextBox abono2, TextBox abono3, TextBox abono4, ComboBox Tabono1, ComboBox Tabono2, ComboBox Tabono3, ComboBox Tabono4, TextBox tiposeparador, ComboBox Tipopoliza, Guna2TextBox DiarioPoliza, Guna2TextBox notas)
        {
            try
            {
                cmd = new SqlCommand("select clave,nombre,Estatus, Cargo1,Cargo2,Cargo3,Cargo4,Cargo5,Cargo6,Cargo7,TipoCargo1,TipoCargo2,TipoCargo3,TipoCargo4,TipoCargo5,TipoCargo6,TipoCargo7,Abono1,Abono2,Abono3,Abono4,TipoAbono1,TipoAbono2,TipoAbono3,TipoAbono4,TipoSeparador,TipoPoliza,DiarioPoliza,Notas  from DEFPOLIZA where Consecutivo = '" + Conse + "' and  Clave = '" + clave + "' and Nombre = '" + Nombre + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    //C1.Text = dr["TipoCargo1"].ToString();
                    //C2.Text = dr["TipoCargo2"].ToString();
                    //C3.Text = dr["TipoCargo3"].ToString();
                    //C4.Text = dr["TipoCargo4"].ToString();
                    //C5.Text = dr["TipoCargo5"].ToString();
                    //C6.Text = dr["TipoCargo6"].ToString();
                    //C7.Text = dr["TipoCargo7"].ToString();
                    //TC1.Items.Add(dr["Cargo1"].ToString());
                    //TC2.Items.Add(dr["Cargo2"].ToString());
                    //TC3.Items.Add(dr["Cargo3"].ToString());
                    //TC4.Items.Add(dr["Cargo4"].ToString());
                    //TC5.Items.Add(dr["Cargo5"].ToString());
                    //TC6.Items.Add(dr["Cargo6"].ToString());
                    //TC7.Items.Add(dr["Cargo7"].ToString());

                    //Tabono1.Items.Add(dr["abono1"].ToString());

                    //Tabono2.Items.Add(dr["abono2"].ToString());
                    //Tabono3.Items.Add(dr["abono3"].ToString());
                    //Tabono4.Items.Add(dr["abono4"].ToString());
                    //abono1.Text = dr["Tipoabono1"].ToString();
                    //abono2.Text = dr["Tipoabono2"].ToString();
                    //abono3.Text = dr["Tipoabono3"].ToString();
                    //abono4.Text = dr["Tipoabono4"].ToString();
                    //tiposeparador.Text = dr["TipoSeparador"].ToString();
                    //  Tipopoliza.Text = dr["Tipopoliza"].ToString();
                    //DiarioPoliza.Text = dr["Diariopoliza"].ToString();
                    //notas.Text = dr["notas"].ToString();



                    C1.Text = dr["TipoCargo1"].ToString();
                    C2.Text = dr["TipoCargo2"].ToString();
                    C3.Text = dr["TipoCargo3"].ToString();
                    C4.Text = dr["TipoCargo4"].ToString();
                    C5.Text = dr["TipoCargo5"].ToString();
                    C6.Text = dr["TipoCargo6"].ToString();
                    C7.Text = dr["TipoCargo7"].ToString();
                    TC1.Text = dr["Cargo1"].ToString();
                    TC2.Text = dr["Cargo2"].ToString();
                    TC3.Text = dr["Cargo3"].ToString();
                    TC4.Text = dr["Cargo4"].ToString();
                    TC5.Text = dr["Cargo5"].ToString();
                    TC6.Text = dr["Cargo6"].ToString();
                    TC7.Text = dr["Cargo7"].ToString();

                    Tabono1.Text = dr["abono1"].ToString();
                    Tabono2.Text = dr["abono2"].ToString();
                    Tabono3.Text = dr["abono3"].ToString();
                    Tabono4.Text = dr["abono4"].ToString();
                    abono1.Text = dr["Tipoabono1"].ToString();
                    abono2.Text = dr["Tipoabono2"].ToString();
                    abono3.Text = dr["Tipoabono3"].ToString();
                    abono4.Text = dr["Tipoabono4"].ToString();
                    tiposeparador.Text = dr["TipoSeparador"].ToString();
                    Tipopoliza.Text = dr["Tipopoliza"].ToString();
                    DiarioPoliza.Text = dr["Diariopoliza"].ToString();
                    notas.Text = dr["notas"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("1:" + ex.ToString());

            }
        }
        //----------------------------------------------------------

        public void TipoDocumento(ComboBox clave)
        {
            try
            {

                clave.Items.Clear();
                cmd = new SqlCommand("select Clave from Documento where TipoDocumento in ('Venta') and Clase in ('Remision','Factura')", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    clave.Items.Add(dr[0].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        //----------------------------------------------------------
        public void clavesDocumento(Label clave)
        {
            try
            {


                cmd = new SqlCommand("select STRING_AGG('-'+Clave +'-' ,',' ) as clave from Documento where TipoDocumento in ('Venta') and Clase in ('Remision','Factura')", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    clave.Text = dr["clave"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        //----------------------------------------------------------



        public void TipoTorre(ComboBox clave)
        {
            try
            {

                clave.Items.Clear();
                clave.Items.Add("TODOS");
                cmd = new SqlCommand("select Descripcion from  Condominio", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    clave.Items.Add(dr[0].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        //----------------------------------------------------------
        public void ClaveTipoTorre(string descripcion, Label clave)
        {
            try
            {


                cmd = new SqlCommand("select ClaveCondominio from  Condominio where Descripcion='" + descripcion + "'", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    clave.Text = dr["ClaveCondominio"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        //----------------------------------------------------------


        public void TipoDocumentoCompras(ComboBox clave)
        {
            try
            {

                clave.Items.Clear();
                cmd = new SqlCommand("select Distinct(Clave) from Documento where  TipoDocumento in ('Compra') and Clase in ('Compra')", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    clave.Items.Add(dr[0].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }

        //----------------------------------------------------------
        public void DescripcionDocumento(string clave, Guna2TextBox Nombre)
        {

            try
            {
                cmd = new SqlCommand("select Nombre from Documento where TipoDocumento in ('Venta') and Clase in ('Remision','Factura') and Clave = '" + clave + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Nombre.Text = dr["Nombre"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error:" + ex.ToString());
            }
        }
        //----------------------------------------------------------
        public void DescripcionDocumentoCompras(string clave, Guna2TextBox Nombre)
        {

            try
            {
                cmd = new SqlCommand("select Nombre from Documento where TipoDocumento in ('Compra') and Clase in ('Compra') and Clave = '" + clave + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Nombre.Text = dr["Nombre"].ToString();

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error:" + ex.ToString());
            }
        }


        public void ConsultaNumPoliza(string TipoPoliza, Label clave)
        {
            try
            {
                //clave.Items.Clear();
                cmd = new SqlCommand("select top 1 Consecutivo from DEFPOLIZA order by consecutivo desc ", cn);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //clave.Items.Add(dr[0].ToString());
                    clave.Text = dr["Consecutivo"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        //-----------------
        public void consultaCuentaBanco(Label Cuenta)
        {
            try
            {
                cmd = new SqlCommand("select top 1 Case  CuentaContable When '' Then 'Num de cuenta no agregado' else CuentaContable end  from CuentasBancarias", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Cuenta.Text = dr["CuentaContable"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        public void consultaCuentaConceptosIngresos(Label Cuenta)
        {
            try
            {
                cmd = new SqlCommand("select top 1 Case CuentaContable  When '' Then 'Num de cuenta no agregado' else CuentaContable end from ConceptosIngreso", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Cuenta.Text = dr["CuentaContable"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        public void consultaCuentaDocumento(Label Cuenta, string Clave)
        {
            try
            {
                cmd = new SqlCommand("select Case  Cuenta  When '' Then 'Num de cuenta no agregado' else Cuenta end from Documento where Clave='" + Clave + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Cuenta.Text = dr["Cuenta"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        public string RegistroCuentaContable(string Clave, string Nombre, string Cargo1, string Abono1, string NoPoliza, string CuentaContable, string Grupo)
        {
            string mensaje = "";
            try
            {
                cmd = new SqlCommand("insert into RegPOLIZACuentasContables (Nopoliza,Clave,Nombre,Cargo,Abono,CuentaContable,Grupo) Values ('" + NoPoliza + "','" + Clave + "','" + Nombre + "','" + Cargo1 + "','" + Abono1 + "','" + CuentaContable + "','" + Grupo + "')", cn);

                cmd.ExecuteNonQuery();
                //             mensaje = "Registro guardado.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error." + ex.ToString());
            }
            return mensaje;

        }

        public void ConcatenaCuentasContables(Label Cuenta, string nopoliza, string clave, string nombre, string Separador, string Grupo)
        {
            try
            {
                //cmd = new SqlCommand("select (Cargo1 + Cargo2+Cargo3 + Cargo4 + Cargo5) as CuentaContable1 from DEFPOLIZA  where  cargo1 <> 'Cuenta Contable Propietario'   and  cargo1<>('Cuenta Contable Documento') and  cargo1<>(' Cuenta Contable Concepto Ingreso') and   cargo1<>('Cuenta Contable Bancos') and  cargo1<>('Catalogo Gastos')   and cargo2 <> 'Cuenta Contable Propietario'   and  cargo2<>('Cuenta Contable Documento') and  cargo2<>(' Cuenta Contable Concepto Ingreso') and   cargo2<>('Cuenta Contable Bancos') and  cargo2<>('Catalogo Gastos') and cargo3 <> 'Cuenta Contable Propietario' and  cargo3<>('Cuenta Contable Documento') and  cargo3<>(' Cuenta Contable Concepto Ingreso') and   cargo3<>('Cuenta Contable Bancos') and  cargo3<>('Catalogo Gastos')   and cargo4 <> 'Cuenta Contable Propietario'   and cargo4<>('Cuenta Contable Documento') and  cargo4<>(' Cuenta Contable Concepto Ingreso') and  cargo4<>('Cuenta Contable Bancos') and cargo4<>('Catalogo Gastos')   and  cargo5 <> 'Cuenta Contable Propietario'   and  cargo5<>('Cuenta Contable Documento') and  cargo5<>(' Cuenta Contable Concepto Ingreso') and   cargo5<>('Cuenta Contable Bancos') and  cargo5<>('Catalogo Gastos')    and Nombre like '%" + nombre + "%' and TipoPolzOpcion='" + Grupo + "' ", cn);
                cmd = new SqlCommand("select(Cargo1 + Cargo2 + Cargo3 + Cargo4 + Cargo5) as CuentaContable1 from DEFPOLIZA  where cargo1 <> 'Cuenta Contable Propietario'   and cargo1<>('Cuenta Contable Documento') and cargo1<>(' Cuenta Contable Concepto Ingreso') and cargo1<>('Cuenta Contable Bancos') and cargo1<>('Catalogo Gastos')   and cargo2<> 'Cuenta Contable Propietario'   and cargo2<>('Cuenta Contable Documento') and cargo2<>(' Cuenta Contable Concepto Ingreso') and cargo2<>('Cuenta Contable Bancos') and cargo2<>('Catalogo Gastos') and cargo3<> 'Cuenta Contable Propietario' and cargo3<>('Cuenta Contable Documento') and cargo3<>(' Cuenta Contable Concepto Ingreso') and cargo3<>('Cuenta Contable Bancos') and cargo3<>('Catalogo Gastos')   and cargo4<> 'Cuenta Contable Propietario'   and cargo4<>('Cuenta Contable Documento') and cargo4<>(' Cuenta Contable Concepto Ingreso') and cargo4<>('Cuenta Contable Bancos') and cargo4<>('Catalogo Gastos')   and cargo5<> 'Cuenta Contable Propietario'   and cargo5<>('Cuenta Contable Documento') and cargo5<>(' Cuenta Contable Concepto Ingreso') and cargo5<>('Cuenta Contable Bancos') and cargo5<>('Catalogo Gastos')     and cargo1<>('Cuenta Contable Almacenes') and cargo1<>('Cuenta Contable Documentos')   and cargo1<>('Cuenta Contable Movimientos Inventarios')  and cargo1<>('Cuenta Contable de Proveedores') and cargo1<>('Cuenta Contable Productos y Servicios')  and cargo1<>('Cuenta Contable Conceptos Globales') and cargo1<>('Cuenta Contable Centro de Costo')  and cargo1<>('Cuenta Contable Propietarios') and cargo1<>('Cuenta Contable Bancos')  and cargo2<>('Cuenta Contable Almacenes') and cargo2<>('Cuenta Contable Documentos')   and cargo2<>('Cuenta Contable Movimientos Inventarios')  and cargo2<>('Cuenta Contable de Proveedores') and cargo2<>('Cuenta Contable Productos y Servicios')  and cargo2<>('Cuenta Contable Conceptos Globales') and cargo2<>('Cuenta Contable Centro de Costo')  and cargo2<>('Cuenta Contable Propietarios') and cargo2<>('Cuenta Contable Bancos')  and cargo3<>('Cuenta Contable Almacenes') and cargo3<>('Cuenta Contable Documentos')   and cargo3<>('Cuenta Contable Movimientos Inventarios')  and cargo3<>('Cuenta Contable de Proveedores') and cargo3<>('Cuenta Contable Productos y Servicios')  and cargo3<>('Cuenta Contable Conceptos Globales') and cargo3<>('Cuenta Contable Centro de Costo')  and cargo3<>('Cuenta Contable Propietarios') and cargo3<>('Cuenta Contable Bancos')  and cargo4<>('Cuenta Contable Almacenes') and cargo4<>('Cuenta Contable Documentos')   and cargo4<>('Cuenta Contable Movimientos Inventarios')  and cargo4<>('Cuenta Contable de Proveedores') and cargo4<>('Cuenta Contable Productos y Servicios')  and cargo4<>('Cuenta Contable Conceptos Globales') and cargo4<>('Cuenta Contable Centro de Costo')  and cargo4<>('Cuenta Contable Propietarios') and cargo4<>('Cuenta Contable Bancos')  and cargo5<>('Cuenta Contable Almacenes') and cargo5<>('Cuenta Contable Documentos')   and cargo5<>('Cuenta Contable Movimientos Inventarios')  and cargo5<>('Cuenta Contable de Proveedores') and cargo5<>('Cuenta Contable Productos y Servicios')  and cargo5<>('Cuenta Contable Conceptos Globales') and cargo5<>('Cuenta Contable Centro de Costo')  and cargo5<>('Cuenta Contable Propietarios') and cargo5<>('Cuenta Contable Bancos') and Nombre like '%" + nombre + "%' and TipoPolzOpcion = '" + Grupo + "'", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Cuenta.Text = dr["CuentaContable1"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
        public void ConcatenaCuentasContablesA(Label Cuenta, string nopoliza, string clave, string nombre, string Separador, string Grupo)
        {
            try
            {
                //cmd = new SqlCommand("select STRING_AGG(RPCC.abono,'" + Separador + "') as CuentaContable1 From RegPOLIZACuentasContables  AS RPCC where abono <> '' and abono <> 'Cuenta Contable Propietario'  and abono<>('Cuenta Contable Documento') and abono<>(' Cuenta Contable Concepto Ingreso') and  abono<>('Cuenta Contable Bancos') and abono <> 'Catalogo Gastos' and Nopoliza='" + nopoliza + "' and Clave='" + clave + "' and Nombre='" + nombre + "' and Grupo='" + Grupo + "' ", cn);
                //cmd = new SqlCommand("select (abono1 + abono2+abono3 + abono4 ) as CuentaContable1 from DEFPOLIZA  where  Abono1 <> 'Cuenta Contable Propietario'   and  Abono1<>('Cuenta Contable Documento') and  Abono1<>(' Cuenta Contable Concepto Ingreso') and   Abono1<>('Cuenta Contable Bancos') and  Abono1<>('Catalogo Gastos')   and Abono2 <> 'Cuenta Contable Propietario'   and  Abono2<>('Cuenta Contable Documento') and  Abono2<>(' Cuenta Contable Concepto Ingreso') and   Abono2<>('Cuenta Contable Bancos') and  Abono2<>('Catalogo Gastos') and Abono3 <> 'Cuenta Contable Propietario' and  Abono3<>('Cuenta Contable Documento') and  Abono3<>(' Cuenta Contable Concepto Ingreso') and   Abono3<>('Cuenta Contable Bancos') and  Abono3<>('Catalogo Gastos')   and Abono4 <> 'Cuenta Contable Propietario'   and Abono4<>('Cuenta Contable Documento') and  Abono4<>(' Cuenta Contable Concepto Ingreso') and  Abono4<>('Cuenta Contable Bancos') and Abono4<>('Catalogo Gastos')   and Nombre like '%" + nombre+"%' and TipoPolzOpcion='"+Grupo+"' ", cn);
                cmd = new SqlCommand("select (abono1 + abono2+abono3 + abono4 ) as CuentaContable1 from DEFPOLIZA  where  Abono1 <> 'Cuenta Contable Propietario'   and  Abono1<>('Cuenta Contable Documento') and  Abono1<>(' Cuenta Contable Concepto Ingreso') and   Abono1<>('Cuenta Contable Bancos') and  Abono1<>('Catalogo Gastos')   and Abono2 <> 'Cuenta Contable Propietario'   and  Abono2<>('Cuenta Contable Documento') and  Abono2<>(' Cuenta Contable Concepto Ingreso') and   Abono2<>('Cuenta Contable Bancos') and  Abono2<>('Catalogo Gastos') and Abono3 <> 'Cuenta Contable Propietario' and  Abono3<>('Cuenta Contable Documento') and  Abono3<>(' Cuenta Contable Concepto Ingreso') and   Abono3<>('Cuenta Contable Bancos') and  Abono3<>('Catalogo Gastos')   and Abono4 <> 'Cuenta Contable Propietario'   and Abono4<>('Cuenta Contable Documento') and  Abono4<>(' Cuenta Contable Concepto Ingreso') and  Abono4<>('Cuenta Contable Bancos') and Abono4<>('Catalogo Gastos')    and Abono1<>('Cuenta Contable Almacenes') and Abono1<>('Cuenta Contable Documentos')   and Abono1<>('Cuenta Contable Movimientos Inventarios')   and Abono1<>('Cuenta Contable de Proveedores') and Abono1<>('Cuenta Contable Productos y Servicios')  and Abono1<>('Cuenta Contable Conceptos Globales') and Abono1<>('Cuenta Contable Centro de Costo')  and Abono1<>('Cuenta Contable Propietarios') and Abono1<>('Cuenta Contable Bancos')  and Abono2<>('Cuenta Contable Almacenes') and Abono2<>('Cuenta Contable Documentos')   and Abono2<>('Cuenta Contable Movimientos Inventarios')  and Abono2<>('Cuenta Contable de Proveedores') and Abono2<>('Cuenta Contable Productos y Servicios')  and Abono2<>('Cuenta Contable Conceptos Globales') and Abono2<>('Cuenta Contable Centro de Costo')  and Abono2<>('Cuenta Contable Propietarios') and Abono2<>('Cuenta Contable Bancos')  and Abono3<>('Cuenta Contable Almacenes') and Abono3<>('Cuenta Contable Documentos')   and Abono3<>('Cuenta Contable Movimientos Inventarios')  and Abono3<>('Cuenta Contable de Proveedores') and Abono3<>('Cuenta Contable Productos y Servicios')  and Abono3<>('Cuenta Contable Conceptos Globales') and Abono3<>('Cuenta Contable Centro de Costo')  and Abono3<>('Cuenta Contable Propietarios') and Abono3<>('Cuenta Contable Bancos')  and Abono4<>('Cuenta Contable Almacenes') and Abono4<>('Cuenta Contable Documentos')   and Abono4<>('Cuenta Contable Movimientos Inventarios')  and Abono4<>('Cuenta Contable de Proveedores') and Abono4<>('Cuenta Contable Productos y Servicios')  and Abono4<>('Cuenta Contable Conceptos Globales') and Abono4<>('Cuenta Contable Centro de Costo')  and Abono4<>('Cuenta Contable Propietarios') and Abono4<>('Cuenta Contable Bancos')  and Nombre like '%" + nombre + "%' and TipoPolzOpcion='" + Grupo + "' ", cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Cuenta.Text = dr["CuentaContable1"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("1:" + ex.ToString());
            }
        }
    }
}




