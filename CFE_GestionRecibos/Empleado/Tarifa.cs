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
    public partial class Tarifa : Form
    {
        public int id_emp;
        TarifaClass tarifa;

        public Tarifa()
        {
            InitializeComponent();
        }

        private bool validar()
        {
            if (tbx_año.TextLength == 0)
            {
                MessageBox.Show("Escriba el año del periodo de facturación.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsOnlyNumerics(tbx_año.Text))
            {
                MessageBox.Show("El año no debe contener letras.", "Información inválida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (cbx_mes.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el mes del periodo de facturación.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_tarbas.TextLength == 0)
            {
                MessageBox.Show("Capture la tarifa básica.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsDecimalNumber(tbx_tarbas.Text))
            {
                MessageBox.Show("La tarifa básica no debe contener letras.", "Información inválida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_tarint.TextLength == 0)
            {
                MessageBox.Show("Capture la tarifa intermedia.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsDecimalNumber(tbx_tarint.Text))
            {
                MessageBox.Show("La tarifa intermedia no debe contener letras.", "Información inválida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_tarexc.TextLength == 0)
            {
                MessageBox.Show("Capture la tarifa excedente.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsDecimalNumber(tbx_tarexc.Text))
            {
                MessageBox.Show("La tarifa excedente no debe contener letras.", "Información inválida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            tarifa = new TarifaClass(
                    id_emp,
                    Convert.ToBoolean(cbx_tiposerv.SelectedIndex),
                    Convert.ToInt32(tbx_año.Text),
                    Convert.ToByte(cbx_mes.Text),
                    Convert.ToDouble(tbx_tarbas.Text),
                    Convert.ToDouble(tbx_tarint.Text),
                    Convert.ToDouble(tbx_tarexc.Text)
            );

            return true;
        }

        private void Tarifa_Load(object sender, EventArgs e)
        {
            cbx_tiposerv.SelectedIndex = 0;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                EnlaceDB link = new EnlaceDB();
                switch (link.AgregarTarifa(tarifa))
                {
                    case 0:
                        MessageBox.Show("Tarifa registrada.", "Información");
                        break;
                    case 1:
                        MessageBox.Show("No se pudo registrar la tarifa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    default:
                        break;
                }
                tbx_año.Text = "";
                tbx_tarbas.Text = "";
                tbx_tarexc.Text = "";
                tbx_tarint.Text = "";
                cbx_mes.SelectedIndex = -1;
            }
        }
    }
}
