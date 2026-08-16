using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PV.Clases
{
    public static class ComboUtil
    {
        public static void LlenarComboBox(
       ComboBox comboBox,
       DataTable dataSource,
       string displayMember,
       string valueMember)
        {
            comboBox.DataSource = null;
            comboBox.Items.Clear();

            if (dataSource == null || dataSource.Rows.Count == 0)
            {
                return;
            }

            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.DataSource = dataSource;

            comboBox.SelectedIndex = -1;
        }
        public static string ObtenerSelectedValue(ComboBox combo)
        {
            if (combo == null)
                return null;

            if (combo.SelectedValue == null ||
                combo.SelectedValue == DBNull.Value ||
                combo.SelectedValue is DataRowView)
            {
                return null;
            }

            string valor = combo.SelectedValue.ToString();

            return string.IsNullOrWhiteSpace(valor)
                ? null
                : valor;
        }
    }
}
