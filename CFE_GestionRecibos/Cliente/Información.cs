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
    public partial class Información : Form
    {
        public long id_cl;

        public Información()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Información_Load(object sender, EventArgs e)
        {
            EnlaceDB link = new EnlaceDB();
            DateTime tm;
            DataRow cliente = link.ObtenerCliente(id_cl);
            st_identity.Text = "ID: " + Convert.ToString(cliente.ItemArray[0]);
            st_username.Text = "Nombre de usuario: " + Convert.ToString(cliente.ItemArray[1]);
            tm = (DateTime)cliente.ItemArray[2];
            st_fecnac.Text = "Fecha de nacimiento: " + string.Format(tm.ToShortDateString(), "dd/mm/aaaa");
            st_edad.Text = "Edad: " + Convert.ToString(cliente.ItemArray[3]);
            Domicilio display = new Domicilio(Convert.ToString(cliente.ItemArray[4]));
            st_domicilio.Text = "Domicilio: " + display.getFormat();
            st_curp.Text = "CURP: " + Convert.ToString(cliente.ItemArray[5]);
            st_email.Text = "Correo electrónico: " + Convert.ToString(cliente.ItemArray[6]);
            tm = (DateTime)cliente.ItemArray[7];
            st_fechaalta.Text = "Fecha de alta: " + string.Format(tm.ToShortDateString(), "dd/mm/aaaa");
        }
    }
}
