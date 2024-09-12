using System;
using System.Net.Mail;
using System.Windows.Forms;
using MEDCON.Clases.Avisos;
using PuntoVentas.Clases.Login;

namespace MEDCON
{
    public partial class Avisos : Form
    {
        DBAvisos c = new DBAvisos();

        string clave = "";

        public Avisos()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        void GenerarClave()
        {
            c.ClaveAvisosiguiente();
            if (DBAvisos.Clave == 0)
            {
                DBAvisos.Clave = 1;
            }
            else
            {
                DBAvisos.Clave = DBAvisos.Clave + 1;
            }
            txtClave.Text = Convert.ToString(DBAvisos.Clave);
        }

        void Limpiar()
        {
            txtClave.Clear();
            txtNombre.Clear();
            dpFecha.ResetText();
            txtDescripcion.Clear();
            txtClave2.Clear();
            txtNombre2.Clear();
            txtDescripcion2.Clear();
            txtNombre.Enabled = false;
            dpFecha.Enabled = false;
            txtDescripcion.Enabled = false;
            Ocultar();
        }

        private void Avisos_Load(object sender, EventArgs e)
        {
            DBAvisos.Clave = 0;
            cmbRegistradoPor.Text = DBLogin.usuario;
            c.CargarAvisos(dataGridView2);
            c.CargarAvisos(dataGridView3);
            c.Correos(dataGridView1);
            c.CargarPacientes(dataGridView4);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Limpiar();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        void Ocultar()
        {

            PanelAvisos.Visible = false;
            PanelAviso2.Visible = false;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (PanelAvisos.Visible == false)
            {
                PanelAvisos.Visible = true;
            }
            else if (PanelAvisos.Visible == true)
            {
                PanelAvisos.Visible = false;
            }

            if (PanelAviso2.Visible == false)
            {
                PanelAviso2.Visible = true;
            }
            else if (PanelAviso2.Visible == true)
            {
                PanelAviso2.Visible = false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                MessageBox.Show("Genere nuevo registro");
            }
            else if (txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese el nombre del aviso");
                txtNombre.Focus();
                return;
            }
            else if (txtDescripcion.Text == "")
            {
                MessageBox.Show("Ingrese la descripcion del aviso");
                txtDescripcion.Focus();
                return;
            }
            else if (cmbRegistradoPor.Text == "")
            {
                MessageBox.Show("Ingrese quien registra el aviso");
                cmbRegistradoPor.Focus();
                return;
            }
            else
            {
                MessageBox.Show(c.RegistroAvisos(txtClave.Text, txtNombre.Text, dpFecha.Text, txtDescripcion.Text, cmbRegistradoPor.Text));
                Limpiar();
                c.CargarAvisos(dataGridView2);
                c.CargarAvisos(dataGridView3);
                c.Correos(dataGridView1);
            }
        }

        private void txtFiltroClave2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFiltroNombre2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Clave = dataGridView2.Rows[e.RowIndex].Cells["ClaveAviso"].Value.ToString();
                clave = Clave;
            }
            else
            {
                return;
            }
            Limpiar();
            txtClave.Text = clave;
            txtClave.Enabled = false;
            c.ConsultaAvisoSeleccionado(txtClave.Text, txtNombre, dpFecha, txtDescripcion, cmbRegistradoPor, txtNombre2, txtClave2, txtDescripcion2);
            txtNombre.Enabled = true;
            dpFecha.Enabled = true;
            txtDescripcion.Enabled = true;
            PanelAvisos.Visible = false;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            Ocultar();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Ocultar();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Ocultar();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Ocultar();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Ocultar();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Ocultar();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            PanelAvisos.Visible = false;
            PanelAviso2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Mail = "";
            c.CorreoContra();

            if (txtClave2.Text != string.Empty)
            {
                if (DBAvisos.Servidor == "hotmail.com")
                {
                    if (MessageBox.Show("Si: Enviar Avisos a todos los Propietarios. No: Enviar Avisos a los Propietarios Seleccionados. ¿Enviar aviso?", "Enviar Avisos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int rowcont = dataGridView1.Rows.Count;
                        for (int i = 0; i < rowcont; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value != null)
                            {
                                Mail = dataGridView1.Rows[i].Cells[2].Value.ToString();

                                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                mmsg.To.Add(Mail);
                                mmsg.Subject = txtNombre.Text;
                                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                mmsg.Body = txtDescripcion.Text;
                                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                mmsg.IsBodyHtml = false;

                                if (txtArchivo.Text != string.Empty)
                                {
                                    mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                }

                                mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@hotmail.com");


                                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@hotmail.com", DBAvisos.Contraseña);
                                cliente.Port = 587;
                                cliente.EnableSsl = true;
                                cliente.Host = "smtp.live.com";

                                try
                                {
                                    cliente.Send(mmsg);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("Error" + ex.ToString());
                                }
                            }
                        }

                        c.Correos2(dataGridView1);
                        int rowcont2 = dataGridView1.Rows.Count;
                        for (int i = 0; i < rowcont2; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value != null)
                            {
                                Mail = dataGridView1.Rows[i].Cells[2].Value.ToString();

                                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                mmsg.To.Add(Mail);
                                mmsg.Subject = txtNombre.Text;
                                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                mmsg.Body = txtDescripcion.Text;
                                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                mmsg.IsBodyHtml = false;

                                if (txtArchivo.Text != string.Empty)
                                {
                                    mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                }

                                mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@hotmail.com");


                                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@hotmail.com", DBAvisos.Contraseña);
                                cliente.Port = 587;
                                cliente.EnableSsl = true;
                                cliente.Host = "smtp.live.com";

                                try
                                {
                                    cliente.Send(mmsg);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("Error" + ex.ToString());
                                }
                            }
                        }

                        c.CorreosClientes(dataGridView1);
                        int rowcont3 = dataGridView1.Rows.Count;
                        for (int i = 0; i < rowcont3; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value != null)
                            {
                                Mail = dataGridView1.Rows[i].Cells[2].Value.ToString();

                                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                mmsg.To.Add(Mail);
                                mmsg.Subject = txtNombre.Text;
                                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                mmsg.Body = txtDescripcion.Text;
                                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                mmsg.IsBodyHtml = false;

                                if (txtArchivo.Text != string.Empty)
                                {
                                    mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                }

                                mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@hotmail.com");


                                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@hotmail.com", DBAvisos.Contraseña);
                                cliente.Port = 587;
                                cliente.EnableSsl = true;
                                cliente.Host = "smtp.live.com";

                                try
                                {
                                    cliente.Send(mmsg);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("Error" + ex.ToString());
                                }
                            }
                        }

                        c.CorreosClientes2(dataGridView1);
                        int rowcont4 = dataGridView1.Rows.Count;
                        for (int i = 0; i < rowcont4; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value != null)
                            {
                                Mail = dataGridView1.Rows[i].Cells[2].Value.ToString();

                                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                mmsg.To.Add(Mail);
                                mmsg.Subject = txtNombre.Text;
                                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                mmsg.Body = txtDescripcion.Text;
                                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                mmsg.IsBodyHtml = false;

                                if (txtArchivo.Text != string.Empty)
                                {
                                    mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                }

                                mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@hotmail.com");


                                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@hotmail.com", DBAvisos.Contraseña);
                                cliente.Port = 587;
                                cliente.EnableSsl = true;
                                cliente.Host = "smtp.live.com";

                                try
                                {
                                    cliente.Send(mmsg);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("Error" + ex.ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow item2 in dataGridView4.Rows)
                        {
                            if (item2.Cells[0].Value != null)
                            {
                                if (item2.Cells[1].Value != null)
                                {
                                    Mail = item2.Cells[4].Value.ToString();

                                    System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                    mmsg.To.Add(Mail);
                                    mmsg.Subject = txtNombre.Text;
                                    mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                    mmsg.Body = txtDescripcion.Text;
                                    mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                    mmsg.IsBodyHtml = false;

                                    if (txtArchivo.Text != string.Empty)
                                    {
                                        mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                    }

                                    mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@hotmail.com");


                                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                    cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@hotmail.com", DBAvisos.Contraseña);
                                    cliente.Port = 587;
                                    cliente.EnableSsl = true;
                                    cliente.Host = "smtp.live.com";

                                    try
                                    {
                                        cliente.Send(mmsg);
                                    }
                                    catch (Exception ex)
                                    {

                                        MessageBox.Show("Error" + ex.ToString());
                                    }
                                }
                            }
                        }

                        foreach (DataGridViewRow item2 in dataGridView4.Rows)
                        {
                            if (item2.Cells[0].Value != null)
                            {
                                if (item2.Cells[1].Value != null)
                                {
                                    if (item2.Cells[5].Value.ToString() != string.Empty)
                                    {
                                        Mail = item2.Cells[5].Value.ToString();

                                        System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                        mmsg.To.Add(Mail);
                                        mmsg.Subject = txtNombre.Text;
                                        mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                        mmsg.Body = txtDescripcion.Text;
                                        mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                        mmsg.IsBodyHtml = false;

                                        if (txtArchivo.Text != string.Empty)
                                        {
                                            mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                        }

                                        mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@hotmail.com");


                                        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                        cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@hotmail.com", DBAvisos.Contraseña);
                                        cliente.Port = 587;
                                        cliente.EnableSsl = true;
                                        cliente.Host = "smtp.live.com";

                                        try
                                        {
                                            cliente.Send(mmsg);
                                        }
                                        catch (Exception ex)
                                        {

                                            MessageBox.Show("Error" + ex.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                    c.Correos(dataGridView1);
                    MessageBox.Show("Avisos Enviados");
                }
                else if (DBAvisos.Servidor == "gmail.com")
                {
                    if (MessageBox.Show("Si: Enviar Avisos a todos los Propietarios. No: Enviar Avisos a los Propietarios Seleccionados. ¿Enviar aviso?", "Enviar Avisos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int rowcont = dataGridView1.Rows.Count;
                        for (int i = 0; i < rowcont; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value != null)
                            {
                                Mail = dataGridView1.Rows[i].Cells[2].Value.ToString();

                                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                mmsg.To.Add(Mail);
                                mmsg.Subject = txtNombre.Text;
                                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                mmsg.Body = txtDescripcion.Text;
                                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                mmsg.IsBodyHtml = false;

                                if (txtArchivo.Text != string.Empty)
                                {
                                    mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                }

                                mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@" + DBAvisos.Servidor);


                                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@" + DBAvisos.Servidor, DBAvisos.Contraseña);
                                cliente.Port = 587;
                                cliente.EnableSsl = true;
                                cliente.Host = "smtp.gmail.com";

                                try
                                {
                                    cliente.Send(mmsg);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("Error" + ex.ToString());
                                }
                            }
                        }

                        c.Correos2(dataGridView1);
                        int rowcont2 = dataGridView1.Rows.Count;
                        for (int i = 0; i < rowcont2; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value != null)
                            {
                                Mail = dataGridView1.Rows[i].Cells[2].Value.ToString();

                                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                mmsg.To.Add(Mail);
                                mmsg.Subject = txtNombre.Text;
                                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                mmsg.Body = txtDescripcion.Text;
                                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                mmsg.IsBodyHtml = false;

                                if (txtArchivo.Text != string.Empty)
                                {
                                    mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                }

                                mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@" + DBAvisos.Servidor);


                                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@" + DBAvisos.Servidor, DBAvisos.Contraseña);
                                cliente.Port = 587;
                                cliente.EnableSsl = true;
                                cliente.Host = "smtp.gmail.com";

                                try
                                {
                                    cliente.Send(mmsg);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("Error" + ex.ToString());
                                }
                            }
                        }

                        c.CorreosClientes(dataGridView1);
                        int rowcont3 = dataGridView1.Rows.Count;
                        for (int i = 0; i < rowcont3; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value != null)
                            {
                                Mail = dataGridView1.Rows[i].Cells[2].Value.ToString();

                                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                mmsg.To.Add(Mail);
                                mmsg.Subject = txtNombre.Text;
                                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                mmsg.Body = txtDescripcion.Text;
                                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                mmsg.IsBodyHtml = false;

                                if (txtArchivo.Text != string.Empty)
                                {
                                    mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                }

                                mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@" + DBAvisos.Servidor);


                                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@" + DBAvisos.Servidor, DBAvisos.Contraseña);
                                cliente.Port = 587;
                                cliente.EnableSsl = true;
                                cliente.Host = "smtp.gmail.com";

                                try
                                {
                                    cliente.Send(mmsg);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("Error" + ex.ToString());
                                }
                            }
                        }

                        c.CorreosClientes2(dataGridView1);
                        int rowcont4 = dataGridView1.Rows.Count;
                        for (int i = 0; i < rowcont4; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value != null)
                            {
                                Mail = dataGridView1.Rows[i].Cells[2].Value.ToString();

                                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                mmsg.To.Add(Mail);
                                mmsg.Subject = txtNombre.Text;
                                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                mmsg.Body = txtDescripcion.Text;
                                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                mmsg.IsBodyHtml = false;

                                if (txtArchivo.Text != string.Empty)
                                {
                                    mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                }

                                mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@" + DBAvisos.Servidor);


                                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@" + DBAvisos.Servidor, DBAvisos.Contraseña);
                                cliente.Port = 587;
                                cliente.EnableSsl = true;
                                cliente.Host = "smtp.gmail.com";

                                try
                                {
                                    cliente.Send(mmsg);
                                }
                                catch (Exception ex)
                                {

                                    MessageBox.Show("Error" + ex.ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow item2 in dataGridView4.Rows)
                        {
                            if (item2.Cells[0].Value != null)
                            {
                                if (item2.Cells[1].Value != null)
                                {
                                    Mail = item2.Cells[4].Value.ToString();

                                    System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                    mmsg.To.Add(Mail);
                                    mmsg.Subject = txtNombre.Text;
                                    mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                    mmsg.Body = txtDescripcion.Text;
                                    mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                    mmsg.IsBodyHtml = false;

                                    if (txtArchivo.Text != string.Empty)
                                    {
                                        mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                    }

                                    mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@" + DBAvisos.Servidor);


                                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                    cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@" + DBAvisos.Servidor, DBAvisos.Contraseña);
                                    cliente.Port = 587;
                                    cliente.EnableSsl = true;
                                    cliente.Host = "smtp.gmail.com";

                                    try
                                    {
                                        cliente.Send(mmsg);
                                    }
                                    catch (Exception ex)
                                    {

                                        MessageBox.Show("Error" + ex.ToString());
                                    }
                                }
                            }
                        }

                        foreach (DataGridViewRow item2 in dataGridView4.Rows)
                        {
                            if (item2.Cells[0].Value != null)
                            {
                                if (item2.Cells[1].Value != null)
                                {
                                    if (item2.Cells[5].Value.ToString() != string.Empty)
                                    {
                                        Mail = item2.Cells[5].Value.ToString();

                                        System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                                        mmsg.To.Add(Mail);
                                        mmsg.Subject = txtNombre.Text;
                                        mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                                        mmsg.Body = txtDescripcion.Text;
                                        mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                                        mmsg.IsBodyHtml = false;

                                        if (txtArchivo.Text != string.Empty)
                                        {
                                            mmsg.Attachments.Add(new Attachment(txtArchivo.Text));
                                        }

                                        mmsg.From = new System.Net.Mail.MailAddress(DBAvisos.Correo + "@" + DBAvisos.Servidor);


                                        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();


                                        cliente.Credentials = new System.Net.NetworkCredential(DBAvisos.Correo + "@" + DBAvisos.Servidor, DBAvisos.Contraseña);
                                        cliente.Port = 25;
                                        cliente.EnableSsl = true;
                                        cliente.Host = "smtp.gmail.com";

                                        try
                                        {
                                            cliente.Send(mmsg);
                                        }
                                        catch (Exception ex)
                                        {

                                            MessageBox.Show("Error" + ex.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                    c.Correos(dataGridView1);
                    MessageBox.Show("Avisos Enviados");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un aviso para continuar");
            }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string Clave = dataGridView3.Rows[e.RowIndex].Cells["ClaveAviso2"].Value.ToString();
                clave = Clave;
            }
            else
            {
                return;
            }
            Limpiar();
            txtClave.Text = clave;
            txtClave.Enabled = false;
            c.ConsultaAvisoSeleccionado(txtClave.Text, txtNombre, dpFecha, txtDescripcion, cmbRegistradoPor, txtNombre2, txtClave2, txtDescripcion2);
            txtNombre.Enabled = true;
            dpFecha.Enabled = true;
            txtDescripcion.Enabled = true;
            PanelAvisos.Visible = false;
            PanelAviso2.Visible = false;
        }

        private void txtFiltroClave_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            Ocultar();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelAvisos.Visible = false;
            PanelAviso2.Visible = false;
        }

        private void Avisos_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (PanelPacientes2.Visible == false)
            {
                PanelPacientes2.Visible = true;
            }
            else if (PanelPacientes2.Visible == true)
            {
                PanelPacientes2.Visible = false;
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //ReporteAvisos rep = new ReporteAvisos();
            //rep.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == string.Empty)
            {
                GenerarClave();
                txtNombre.Enabled = true;
                dpFecha.Enabled = true;
                txtDescripcion.Enabled = true;
            }
            else
            {
                Limpiar();
                GenerarClave();
                txtNombre.Enabled = true;
                dpFecha.Enabled = true;
                txtDescripcion.Enabled = true;
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog Abrir = new OpenFileDialog();

            Abrir.Filter = "Archivos PDF(* .PDF) |*.pdf";
            Abrir.InitialDirectory = "C:/";

            if (Abrir.ShowDialog() == DialogResult.OK)
            {
                string Dir = Abrir.FileName;
                txtArchivo.Text = Dir;
            }
        }
    }
}
