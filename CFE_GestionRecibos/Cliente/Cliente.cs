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
    public partial class Cliente : Form
    {
        public long id = 001;
        public string username = "Default Costumer";

        List<ServicioClass> servicios;

        public Cliente()
        {
            InitializeComponent();
        }

        private void btn_info_Click(object sender, EventArgs e)
        {
            Información dialogInfo = new Información();
            dialogInfo.id_cl = id;
            dialogInfo.ShowDialog();
        }

        private void btn_recibo_Click(object sender, EventArgs e)
        {
            Recibos dialogR = new Recibos();
            dialogR.id_rec = Convert.ToInt64(dgv_recibos.SelectedRows[0].Cells[0].Value);
            if (dialogR.ShowDialog() == DialogResult.OK)
            {
                EnlaceDB link = new EnlaceDB();
                dgv_recibos.DataSource = link.LlenarRecibos(Convert.ToInt64(cbx_servicios.SelectedValue));
            }
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            st_identity.Text = "ID: " + id.ToString();
            st_username.Text = "Usuario: " + username;
            EnlaceDB link = new EnlaceDB();
            servicios = new List<ServicioClass>();
            DataTable dt = link.LlenarServicios(id);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                servicios.Add(new ServicioClass(dt.Rows[i], Convert.ToInt64(dt.Rows[i].ItemArray[3])));
            }
            cbx_servicios.SelectedIndex = -1;
            cbx_servicios.ValueMember = "id_ser";
            cbx_servicios.DisplayMember = "med";
            cbx_servicios.DataSource = servicios;
        }

        private void cbx_servicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_servicios.SelectedIndex != -1)
            {
                EnlaceDB link = new EnlaceDB();
                dgv_recibos.DataSource = link.LlenarRecibos(Convert.ToInt64(cbx_servicios.SelectedValue));
                if (dgv_recibos.Rows.Count != 0)
                {
                    btn_recibo.Enabled = true;
                }
                else
                {
                    btn_recibo.Enabled = false;
                }
            }
            else
            {
                dgv_recibos.DataSource = null;
            }
        }
    }
}
