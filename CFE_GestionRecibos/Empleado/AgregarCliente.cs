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
    public partial class AgregarCliente : Form
    {
        ClienteClass client;
        ClienteClass clientMod;
        public int id_emp;

        public long id_client_mod;

        public AgregarCliente()
        {
            InitializeComponent();
        }

        private bool validar()
        {
            if (tbx_nombres.TextLength == 0)
            {
                MessageBox.Show("Llene el campo Nombres.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsOnlyAlphas(tbx_nombres.Text))
            {
                MessageBox.Show("El nombre no debe contener caracteres numéricos.", "Información inválida.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_apellidos.TextLength == 0)
            {
                MessageBox.Show("Llene el campo Apellidos.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsOnlyAlphas(tbx_apellidos.Text))
            {
                MessageBox.Show("El apellido no debe contener caracteres numéricos.", "Información inválida.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (cbx_mesnac.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione su mes de nacimiento.", "Información incompleta.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (cbx_dianac.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione su día de nacimiento.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_añonac.TextLength == 0)
            {
                MessageBox.Show("Escriba su año de nacimiento.", "Información incompleta.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsOnlyNumerics(tbx_añonac.Text))
            {
                MessageBox.Show("El año no debe contener letras.", "Información inválida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_calle.TextLength == 0)
            {

            }
            if (tbx_numext.TextLength == 0)
            {
                MessageBox.Show("Llene el número externo.", "Información inválida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsOnlyNumerics(tbx_numext.Text))
            {
                MessageBox.Show("El número externo contiene letras.", "Información inválida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_col.TextLength == 0)
            {
                MessageBox.Show("Llene la colonia de su domicilio.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_estado.TextLength == 0)
            {
                MessageBox.Show("Llene el estado de su domicilio.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_munic.TextLength == 0)
            {
                MessageBox.Show("Llene el municipio de su domicilio.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_cp.TextLength == 0)
            {
                MessageBox.Show("Llene el código postal de su domicilio.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsOnlyNumerics(tbx_cp.Text))
            {
                MessageBox.Show("El código postal no debe contener letras.", "Información inválida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_curp.TextLength != 18)
            {
                MessageBox.Show("El CURP debe contener 18 caracteres.", "Información incompleta.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (!RegexUtilities.IsValidEmail(tbx_email.Text))
            {
                MessageBox.Show("El correo electrónico no es válido.", "Información inválida.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_password.TextLength != 8)
            {
                MessageBox.Show("La contraseña debe contener 8 caracteres.", "Información incompleta.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            client = new ClienteClass(
                id_emp,
                tbx_nombres.Text,
                tbx_apellidos.Text,
                new DateTime(Convert.ToInt32(tbx_añonac.Text), Convert.ToInt32(cbx_mesnac.Text), Convert.ToInt32(cbx_dianac.Text)),
                new Domicilio(tbx_calle.Text, tbx_numext.Text, tbx_numint.Text, tbx_col.Text, tbx_munic.Text, tbx_estado.Text, tbx_cp.Text),
                tbx_curp.Text,
                tbx_email.Text,
                tbx_password.Text
            );

            return true;
        }

        private void llenarInfo()
        {
            EnlaceDB link = new EnlaceDB();
            DataRow dato = link.DatosCliente(id_client_mod);
            clientMod = new ClienteClass(dato, id_client_mod);
            
            tbx_nombres.Text = clientMod.nom;
            tbx_apellidos.Text = clientMod.ape;
            cbx_mesnac.SelectedIndex = clientMod.fec_nac.Month - 1;
            cbx_dianac.SelectedIndex = clientMod.fec_nac.Day - 1;
            tbx_añonac.Text = Convert.ToString(clientMod.fec_nac.Year);
            tbx_calle.Text = clientMod.dom.calle;
            tbx_numext.Text = clientMod.dom.numext;
            tbx_numint.Text = clientMod.dom.numint;
            tbx_col.Text = clientMod.dom.col;
            tbx_munic.Text = clientMod.dom.munic;
            tbx_estado.Text = clientMod.dom.estado;
            tbx_cp.Text = clientMod.dom.cp;
            tbx_curp.Text = clientMod.curp;
            tbx_email.Text = clientMod.correo_e;
            tbx_password.Text = clientMod.contra;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if(Text == "Modificar cliente")
            {
                // MODIFICACIÓN Cliente
                if (validar())
                {
                    EnlaceDB link = new EnlaceDB();
                    switch (link.ModificarCliente(client, clientMod))
                    {
                        case 0:
                            MessageBox.Show("Cliente editado con éxito.", "Información");
                            DialogResult = DialogResult.OK;
                            Close();
                            break;
                        case 1:
                            MessageBox.Show("No se pudo modificar el cliente. Quizá el correo electrónico ya esté usado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            DialogResult = DialogResult.Cancel;
                            Close();
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                // ALTA Cliente
                if (validar())
                {
                    // ALTA
                    EnlaceDB link = new EnlaceDB();
                    switch (link.AgregarCliente(client))
                    {
                        case 0:
                            MessageBox.Show("Empleado agregado con éxito.", "Aviso");
                            DialogResult = DialogResult.OK;
                            Close();
                            break;
                        case 1:
                            MessageBox.Show("No se pudo agregar el empleado. Quizá el correo electrónico ya esté usado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            DialogResult = DialogResult.Cancel;
                            Close();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void AgregarCliente_Load(object sender, EventArgs e)
        {
            if(Text == "Modificar cliente")
            {
                llenarInfo();
                btn_ok.Text = "Aceptar";
            }
        }

        private void btn_addtel_Click(object sender, EventArgs e)
        {
            Teléfono dialogT = new Teléfono();
            dialogT.ShowDialog();
        }

        private void btn_deltel_Click(object sender, EventArgs e)
        {
            //QUITA TELEFONO
        }
    }
}
