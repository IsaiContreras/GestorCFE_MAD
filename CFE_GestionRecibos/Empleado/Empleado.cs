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
    public partial class Empleado : Form
    {
        public int id = 001;
        public string username = "Default Employee";

        public Empleado()
        {
            InitializeComponent();
        }

        private void UpdateClientesDgv()//ACTUALIZA DGVS
        {
            EnlaceDB link = new EnlaceDB();
            dgv_clientes.DataSource = null;
            dgv_servicios.DataSource = null;
            dgv_clientes.Columns.Clear();
            dgv_clientes.Rows.Clear();
            dgv_servicios.Columns.Clear();
            dgv_servicios.Rows.Clear();
            dgv_clientes.DataSource = link.LlenarClientes();
            if (dgv_clientes.Rows.Count > 0)
            {
                btn_modclient.Enabled = true;
                btn_elimclient.Enabled = true;
                btn_infocliente.Enabled = true;
                btn_agrserv.Enabled = true;
                btn_showserv.Enabled = true;
            }
            else
            {
                btn_modclient.Enabled = false;
                btn_elimclient.Enabled = false;
                btn_infocliente.Enabled = false;
                btn_agrserv.Enabled = false;
                btn_showserv.Enabled = false;
                btn_modserv.Enabled = false;
                btn_elimserv.Enabled = false;
                btn_conshist.Enabled = false;
            }
        }

        private void UpdateServiciosDgv()//ACTUALIZA DGV SERVICIOS
        {
            EnlaceDB link = new EnlaceDB();
            dgv_servicios.DataSource = null;
            dgv_servicios.Rows.Clear();
            dgv_servicios.Columns.Clear();
            long idcliente = Convert.ToInt64(dgv_clientes.SelectedRows[0].Cells[0].Value);
            dgv_servicios.DataSource = link.LlenarServicios(idcliente);
            for (int i = 0; i < dgv_servicios.Rows.Count; i++)
            {
                Domicilio capture;
                string domic = Convert.ToString(dgv_servicios.Rows[i].Cells[2].Value);
                capture = new Domicilio(domic);
                string formated = capture.getFormat();
                dgv_servicios.Rows[i].Cells[2].Value = formated;
            }
            dgv_servicios.AutoResizeColumns();
            if (dgv_servicios.Rows.Count > 0)
            {
                btn_modserv.Enabled = true;
                btn_elimserv.Enabled = true;
                btn_conshist.Enabled = true;
            }
            else
            {
                btn_modserv.Enabled = false;
                btn_elimserv.Enabled = false;
                btn_conshist.Enabled = false;
            }
        }

        private void btn_agrclient_Click(object sender, EventArgs e)//AGREGA CLIENTE
        {
            AgregarCliente dialogAC = new AgregarCliente();
            dialogAC.id_emp = id;
            if (dialogAC.ShowDialog() == DialogResult.OK)
            {
                UpdateClientesDgv();
            }
        }

        private void btn_modclient_Click(object sender, EventArgs e)//MODIFICA CLIENTE
        {
            AgregarCliente dialogMC = new AgregarCliente();
            dialogMC.id_emp = id;
            dialogMC.id_client_mod = Convert.ToInt64(dgv_clientes.SelectedRows[0].Cells[0].Value);
            dialogMC.Text = "Modificar cliente";
            if (dialogMC.ShowDialog() == DialogResult.OK)
            {
                UpdateClientesDgv();
            }
        }

        private void btn_infoempl_Click(object sender, EventArgs e)//INFO DE EMPLEADO
        {
            Información dialogInfo = new Información();
            dialogInfo.id_emp = Convert.ToInt32(id);
            dialogInfo.ShowDialog();
        }

        private void st_agrserv_Click(object sender, EventArgs e)//AGREGA SERVICIO
        {
            AgregarServicio dialogAS = new AgregarServicio();
            dialogAS.id = Convert.ToInt64(dgv_clientes.SelectedRows[0].Cells[0].Value);
            dialogAS.username = Convert.ToString(dgv_clientes.SelectedRows[0].Cells[1].Value);
            dialogAS.num_emp = id;
            if (dialogAS.ShowDialog() == DialogResult.OK)
            {
                UpdateServiciosDgv();
            }
        }

        private void btn_modserv_Click(object sender, EventArgs e)//MODIFICA SERVICIO
        {
            AgregarServicio dialogMS = new AgregarServicio();
            dialogMS.id = Convert.ToInt64(dgv_clientes.SelectedRows[0].Cells[0].Value);
            dialogMS.username = Convert.ToString(dgv_clientes.SelectedRows[0].Cells[1].Value);
            dialogMS.num_emp = id;
            dialogMS.id_serv_mod = Convert.ToInt64(dgv_servicios.SelectedRows[0].Cells[3].Value);
            dialogMS.Text = "Modificar servicio";
            if (dialogMS.ShowDialog() == DialogResult.OK)
            {
                UpdateServiciosDgv();
            }
        }

        private void btn_reptaf_Click(object sender, EventArgs e)//REPORTE TARIFA
        {
            ReporteTarifas dialogRT = new ReporteTarifas();
            dialogRT.ShowDialog();
        }

        private void btn_repcons_Click(object sender, EventArgs e)//REPORTE CONSUMO
        {
            ReporteConsumos dialogRC = new ReporteConsumos();
            dialogRC.ShowDialog();
        }

        private void btn_repgen_Click(object sender, EventArgs e)//REPORTE GENERAL
        {
            ReporteGeneral dialogRG = new ReporteGeneral();
            dialogRG.ShowDialog();
        }

        private void btn_conshist_Click(object sender, EventArgs e)//CONSUMO HISTORICO
        {
            ConsumoHistórico dialogCH = new ConsumoHistórico();
            dialogCH.medidor = Convert.ToInt64(dgv_servicios.SelectedRows[0].Cells[0].Value);
            dialogCH.ShowDialog();
        }

        private void btn_elimclient_Click(object sender, EventArgs e)//ELIMINA CLIENTE
        {
            var result = MessageBox.Show("¿Seguro que desea dar de baja este cliente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                EnlaceDB link = new EnlaceDB();
                int id_cl = Convert.ToInt32(dgv_clientes.SelectedRows[0].Cells[0].Value);
                if (link.EliminarCliente(id_cl))
                {
                    MessageBox.Show("Cliente eliminado con éxito", "Información");
                    UpdateClientesDgv();
                }
            }
        }

        private void btn_elimserv_Click(object sender, EventArgs e)//ELIMINA SERVICIO
        {
            var result = MessageBox.Show("¿Seguro que desea dar de baja este servicio?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                EnlaceDB link = new EnlaceDB();
                long id_serv = Convert.ToInt64(dgv_servicios.SelectedRows[0].Cells[3].Value);
                if (link.EliminarServicio(id_serv))
                {
                    MessageBox.Show("Eliminado con éxito", "Información");
                    UpdateServiciosDgv();
                }
            }
        }

        private void btn_infocliente_Click(object sender, EventArgs e)//INFO DE CLIENTE
        {
            Cliente.Información dialogIC = new Cliente.Información();
            dialogIC.id_cl = Convert.ToInt64(dgv_clientes.SelectedRows[0].Cells[0].Value);
            dialogIC.ShowDialog();
        }

        private void Empleado_Load(object sender, EventArgs e)//INICIALIZACIÓN
        {
            st_identity.Text = "ID: " + id.ToString();
            st_username.Text = "Usuario: " + username;
            UpdateClientesDgv();
        }

        private void btn_genrec_Click(object sender, EventArgs e)//GENERA RECIBO
        {
            GeneradorRecibo dialogGR = new GeneradorRecibo();
            dialogGR.num_emp = id;
            dialogGR.Show();
        }

        private void btn_regtaf_Click(object sender, EventArgs e)//AGREGA TARIFA
        {
            Tarifa dialogTar = new Tarifa();
            dialogTar.id_emp = id;
            dialogTar.ShowDialog();
        }

        private void btn_regcons_Click(object sender, EventArgs e)//AGREGA CONSUMO
        {
            Consumo dialogCon = new Consumo();
            dialogCon.id_emp = id;
            dialogCon.ShowDialog();
        }

        private void btn_showserv_Click(object sender, EventArgs e)//MOSTRAR SERVICIOS
        {
            UpdateServiciosDgv();
        }

        private void dgv_clientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_servicios.DataSource = null;
            btn_modserv.Enabled = false;
            btn_elimserv.Enabled = false;
            btn_conshist.Enabled = false;
        }

        private void dgv_clientes_SelectionChanged(object sender, EventArgs e)
        {
            dgv_servicios.DataSource = null;
            btn_modserv.Enabled = false;
            btn_elimserv.Enabled = false;
            btn_conshist.Enabled = false;
        }
    }
}
