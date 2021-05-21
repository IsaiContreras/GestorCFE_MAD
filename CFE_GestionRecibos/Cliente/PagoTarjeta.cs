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
    public partial class PagoTarjeta : Form
    {
        public long id_rec;

        public PagoTarjeta()
        {
            InitializeComponent();
        }

        private bool validar()
        {
            if (tbx_numtarj.TextLength == 0)
            {
                return false;
            }
            else if (!RegexUtilities.IsOnlyNumerics(tbx_numtarj.Text))
            {
                return false;
            }
            if (tbx_mes.TextLength == 0)
            {
                return false;
            }
            else if (!RegexUtilities.IsOnlyNumerics(tbx_mes.Text))
            {
                return false;
            }
            if (tbx_año.TextLength == 0)
            {
                return false;
            }
            else if (!RegexUtilities.IsOnlyNumerics(tbx_año.Text))
            {
                return false;
            }
            if (tbx_cvv.TextLength == 0)
            {
                return false;
            }
            else if (!RegexUtilities.IsOnlyNumerics(tbx_cvv.Text))
            {
                return false;
            }
            if (tbx_cantidad.TextLength == 0)
            {
                return false;
            }
            else if (!RegexUtilities.IsDecimalNumber(tbx_cantidad.Text))
            {
                return false;
            }
            return true;
        }

        private void PagoTarjeta_FormClosing(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btn_pagar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                EnlaceDB link = new EnlaceDB();
                if (link.Pago(id_rec, Convert.ToDouble(tbx_cantidad.Text)))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
