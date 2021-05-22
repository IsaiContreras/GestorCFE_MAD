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
    public partial class Recibos : Form
    {
        public long id_rec;

        public Recibos()
        {
            InitializeComponent();
        }

        private void btn_pagar_Click(object sender, EventArgs e)
        {
            var res = DialogResult.Cancel;
            if (rdb_efectivo.Checked == true)
            {
                PagoEfectivo dialogPE = new PagoEfectivo();
                dialogPE.id_rec = id_rec;
                res = dialogPE.ShowDialog();
            }
            else if (rdb_tarjeta.Checked == true)
            {
                PagoTarjeta dialogPT = new PagoTarjeta();
                dialogPT.id_rec = id_rec;
                res = dialogPT.ShowDialog();
            }
            else
            {
                PagoTransf dialogPTr = new PagoTransf();
                dialogPTr.id_rec = id_rec;
                res = dialogPTr.ShowDialog();
            }
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Pago realizado con éxito.", "Información");
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void Recibos_Load(object sender, EventArgs e)
        {
            EnlaceDB link = new EnlaceDB();
            DataRow recibo = link.DatosRecibo(id_rec);
            st_username.Text = "Nombre cliente: " + Convert.ToString(recibo.ItemArray[0]);
            st_identity.Text = "ID: " + Convert.ToString(recibo.ItemArray[1]);
            st_medidor.Text = "Medidor: " + Convert.ToString(recibo.ItemArray[2]);
            st_tiposerv.Text = "Tipo de servicio: " + Convert.ToString(recibo.ItemArray[3]);
            Domicilio show = new Domicilio(Convert.ToString(recibo.ItemArray[4]));
            st_domicilio.Text = "Domicilio: " + show.getFormat();
            st_periodo.Text = "Periodo: " + recibo.ItemArray[5].ToString() + "/" + recibo.ItemArray[6].ToString();
            st_consbas.Text = recibo.ItemArray[7].ToString() + " kW";
            st_consint.Text = recibo.ItemArray[8].ToString() + " kW";
            st_consexced.Text = recibo.ItemArray[9].ToString() + " kW";
            st_constot.Text = recibo.ItemArray[10].ToString() + " kW";
            st_prebas.Text = "$" + string.Format("{0:0.00}", recibo.ItemArray[11]);
            st_preint.Text = "$" + string.Format("{0:0.00}", recibo.ItemArray[12]);
            st_preexc.Text = "$" + string.Format("{0:0.00}", recibo.ItemArray[13]);
            st_pabas.Text = "$" + string.Format("{0:0.00}", recibo.ItemArray[14]);
            st_paint.Text = "$" + string.Format("{0:0.00}", recibo.ItemArray[15]);
            st_paexc.Text = "$" + string.Format("{0:0.00}", recibo.ItemArray[16]);
            st_pretot.Text = "$" + string.Format("{0:0.00}", recibo.ItemArray[17]);
            st_papend.Text = "$" + string.Format("{0:0.00}", recibo.ItemArray[18]);
            st_paiva.Text = "$" + string.Format("{0:0.00}", recibo.ItemArray[19]);
            st_patot.Text = "$" + string.Format("{0:0.00}", recibo.ItemArray[20]);
            bool pagado = Convert.ToBoolean(recibo.ItemArray[21]);
            if (pagado) st_pagado.Text = "**PAGADO**";
            else st_pagado.Text = "";
            rdb_efectivo.Checked = true;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Recibos_FormClosing(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
