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
    public partial class ReporteTarifas : Form
    {
        public ReporteTarifas()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            int año = Convert.ToInt32(tbx_año.Text);
            EnlaceDB link = new EnlaceDB();
            dgv_reporte.DataSource = link.ReporteTarifas(año);
            dgv_reporte.AutoResizeColumns();
        }
    }
}
