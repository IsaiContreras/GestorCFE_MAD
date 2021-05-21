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
    public partial class GeneradorRecibo : Form
    {
        public int num_emp;

        public GeneradorRecibo()
        {
            InitializeComponent();
        }

        public bool validar()
        {
            if (tbx_año.TextLength == 0)
            {
                MessageBox.Show("Capture el año del periodo de facturación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (!RegexUtilities.IsOnlyNumerics(tbx_año.Text))
            {
                MessageBox.Show("El año no debe contener letras.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (cbx_mes.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el mes del periodo de facturación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            return true;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            // GENERAR RECIBOS
            if (validar())
            {
                EnlaceDB link = new EnlaceDB();
                if (link.GenerarRecibos(Convert.ToInt32(tbx_año.Text), Convert.ToSByte(cbx_mes.Text), Convert.ToByte(cbx_tiposerv.SelectedIndex), num_emp))
                {
                    string msg = "Recibos de " + cbx_mes.Text + " de " + tbx_año.Text + " del tipo " + cbx_tiposerv.Text + " generados.";
                    MessageBox.Show(msg, "Información");
                    Close();
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GeneradorRecibo_Load(object sender, EventArgs e)
        {
            cbx_tiposerv.SelectedIndex = 0;
        }
    }
}
