using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFE_GestionRecibos.Cliente
{
    public partial class PagoEfectivo : Form
    {
        public long id_rec;

        public PagoEfectivo()
        {
            InitializeComponent();
        }

        private void PagoEfectivo_FormClosing(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btn_pagar_Click(object sender, EventArgs e)
        {
            if (tbx_cantidad.TextLength == 0)
            {
                return;
            }
            if (!RegexUtilities.IsDecimalNumber(tbx_cantidad.Text))
            {
                return;
            }
            EnlaceDB link = new EnlaceDB();
            if (link.Pago(id_rec, Convert.ToDouble(tbx_cantidad.Text)))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
