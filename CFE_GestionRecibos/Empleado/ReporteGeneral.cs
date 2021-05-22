using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFE_GestionRecibos.Empleado
{
    public partial class ReporteGeneral : Form
    {
        public ReporteGeneral()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            if (tbx_año.TextLength == 0)
            {
                MessageBox.Show("Capture año.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (!RegexUtilities.IsOnlyNumerics(tbx_año.Text))
            {
                MessageBox.Show("El año no debe contener letras.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (cbx_mes.SelectedIndex == -1)
            {
                MessageBox.Show("Capture mes.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            EnlaceDB link = new EnlaceDB();
            int year = Convert.ToInt32(tbx_año.Text);
            byte month;
            byte tipo;
            if (cbx_mes.Text == "Todos") month = 0;
            else month = Convert.ToByte(cbx_mes.Text);
            if (cbx_tipo.Text == "Ambos") tipo = 2;
            else tipo = Convert.ToByte(cbx_tipo.SelectedIndex - 1);
            dgv_reporte.DataSource = link.ReporteGeneral(year, month, tipo);
        }

        private void ReporteGeneral_Load(object sender, EventArgs e)
        {
            cbx_tipo.SelectedIndex = 0;
        }
    }
}
