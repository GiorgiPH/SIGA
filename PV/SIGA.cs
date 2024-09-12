using System;
using System.Data;
using System.Windows.Forms;

namespace PuntoVentas
{
    public partial class PuntoVentas : Form
    {
        public static int Opcion = 0;

        public PuntoVentas()
        {
            InitializeComponent();
        }

        private void btAcceso_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void PuntoVentas_Activated(object sender, EventArgs e)
        {
            if (Opcion == 1)
            {
                this.Hide();
            }
        }

        private void PuntoVentas_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void PuntoVentas_Load(object sender, EventArgs e)
        {

        }

        internal class PuntoVentaDataSet
        {
            public SchemaSerializationMode SchemaSerializationMode { get; internal set; }
            public string DataSetName { get; internal set; }
            public object DatosTienda { get; internal set; }
            public object Opera1 { get; internal set; }
        }

        internal class PuntoVentaDataSetTableAdapters
        {
            internal class DatosTiendaTableAdapter
            {
                public bool ClearBeforeFill { get; internal set; }

                internal void Fill(object datosTienda)
                {
                    throw new NotImplementedException();
                }
            }

            internal class Opera1TableAdapter
            {
                public bool ClearBeforeFill { get; internal set; }

                internal void Fill(object opera1)
                {
                    throw new NotImplementedException();
                }
            }
        }

        internal class PuntoVentaDataSet1
        {
            public PuntoVentaDataSet1()
            {
            }

            public string DataSetName { get; internal set; }
            public SchemaSerializationMode SchemaSerializationMode { get; internal set; }
            public object Opera1 { get; internal set; }
            public object Opera2 { get; internal set; }
            public object DatosEmpresa { get; internal set; }
        }

        internal class PuntoVentaDataSet1TableAdapters
        {
            internal class Opera1TableAdapter
            {
                public bool ClearBeforeFill { get; internal set; }

                internal void Fill(object opera1)
                {
                    throw new NotImplementedException();
                }
            }

            internal class Opera2TableAdapter
            {
                public bool ClearBeforeFill { get; internal set; }

                internal void Fill(object opera2)
                {
                    throw new NotImplementedException();
                }
            }

            internal class DatosEmpresaTableAdapter
            {
                public bool ClearBeforeFill { get; internal set; }

                internal void Fill(object datosEmpresa)
                {
                    throw new NotImplementedException();
                }
            }
        }

        internal class PuntoVentaDataSet2
        {
            public SchemaSerializationMode SchemaSerializationMode { get; internal set; }
            public string DataSetName { get; internal set; }
            public object DatosEmpresa { get; internal set; }
            public object Opera1 { get; internal set; }
            public object Opera2 { get; internal set; }
        }

        internal class PuntoVentaDataSet2TableAdapters
        {
            internal class DatosEmpresaTableAdapter
            {
                public bool ClearBeforeFill { get; internal set; }

                internal void Fill(object datosEmpresa)
                {
                    throw new NotImplementedException();
                }
            }

            internal class Opera1TableAdapter
            {
                public bool ClearBeforeFill { get; internal set; }

                internal void Fill(object opera1)
                {
                    throw new NotImplementedException();
                }
            }

            internal class Opera2TableAdapter
            {
                public bool ClearBeforeFill { get; internal set; }

                internal void Fill(object opera2)
                {
                    throw new NotImplementedException();
                }
            }
        }

        internal class PuntoVentaDataSet3
        {
        }

        internal class PuntoVentaDataSet3TableAdapters
        {
        }
    }
}
