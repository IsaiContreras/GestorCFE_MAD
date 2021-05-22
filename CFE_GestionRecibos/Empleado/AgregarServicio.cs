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
    public partial class AgregarServicio : Form
    {
        public long id = 001;
        public string username = "Default Costumer";

        ServicioClass serv;
        ServicioClass servMod;
        public int num_emp;

        public long id_serv_mod;

        public AgregarServicio()
        {
            InitializeComponent();
        }

        private bool validar()
        {
            if (tbx_medidor.TextLength == 0)
            {
                MessageBox.Show("Llene el medidor.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsOnlyNumerics(tbx_medidor.Text))
            {
                MessageBox.Show("El medidor no debe contener letras.", "Información inválida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_calle.TextLength == 0) { 

                MessageBox.Show("Llene la calle de su domicilio.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tbx_numext.TextLength == 0)
            {
                MessageBox.Show("Llene el número externo.", "Información incompleta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            else if (!RegexUtilities.IsOnlyNumerics(tbx_numext.Text))
            {
                MessageBox.Show("El número externo no debe contener letras.", "Información inválida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

            serv = new ServicioClass(
                id,
                num_emp,
                Convert.ToInt64(tbx_medidor.Text),
                new Domicilio(tbx_calle.Text, tbx_numext.Text, tbx_numint.Text, tbx_col.Text, tbx_estado.Text, tbx_munic.Text, tbx_cp.Text),
                Convert.ToBoolean(cbx_servicio.SelectedIndex)
            );

            return true;
        }

        private void llenarInfo()
        {
            EnlaceDB link = new EnlaceDB();
            DataRow dato = link.DatosServicio(id_serv_mod);
            servMod = new ServicioClass(dato, id_serv_mod);

            tbx_medidor.Text = Convert.ToString(servMod.med);
            tbx_calle.Text = servMod.dom.calle;
            tbx_numext.Text = servMod.dom.numext;
            tbx_numint.Text = servMod.dom.numint;
            tbx_col.Text = servMod.dom.col;
            tbx_munic.Text = servMod.dom.munic;
            tbx_estado.Text = servMod.dom.estado;
            tbx_cp.Text = servMod.dom.cp;
            if (servMod.tipo_ser) cbx_servicio.SelectedIndex = 1;
            else cbx_servicio.SelectedIndex = 0;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if(Text == "Modificar servicio")
            {
                if (validar())
                {
                    EnlaceDB link = new EnlaceDB();
                    switch(link.ModificarServicio(serv, servMod))
                    {
                        case 0:
                            MessageBox.Show("Servicio editado con éxito.", "Información");
                            DialogResult = DialogResult.OK;
                            Close();
                            break;
                        case 1:
                            MessageBox.Show("No se pudo editar servicio.", "Información");
                            DialogResult = DialogResult.Cancel;
                            Close();
                            break;
                    }
                }
            }
            else
            {
                if (validar())
                {
                    EnlaceDB link = new EnlaceDB();
                    switch (link.AgregarServicio(serv))
                    {
                        case 0:
                            MessageBox.Show("Servicio agregado con éxito.", "Aviso");
                            DialogResult = DialogResult.OK;
                            Close();
                            break;
                        case 1:
                            MessageBox.Show("No se pudo agregar el servicio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            DialogResult = DialogResult.Cancel;
                            Close();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void AgregarServicio_Load(object sender, EventArgs e)
        {
            st_identity.Text = "ID: " + id.ToString();
            st_username.Text = "Usuario: " + username;
            cbx_servicio.SelectedIndex = 0;
            if (Text == "Modificar servicio")
            {
                llenarInfo();
                btn_ok.Text = "Aceptar";
            }
        }
    }
}
