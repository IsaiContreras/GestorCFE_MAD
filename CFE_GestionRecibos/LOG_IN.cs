using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFE_GestionRecibos
{
    public partial class LOG_IN : Form
    {
        private byte tries = 0;
        private string mailused = "";
        List<LogRem> rem_logs = new List<LogRem>();

        public LOG_IN()
        {
            InitializeComponent();
        }

        private void FillRememberList(char type)
        {
            EnlaceDB link = new EnlaceDB();
            DataTable remLog = new DataTable();
            remLog = link.ObtenerRecordar(type);
            rem_logs.Clear();
            foreach (DataRow row in remLog.Rows)
            {
                rem_logs.Add(new LogRem(Convert.ToInt16(row.ItemArray[0]), Convert.ToString(row.ItemArray[1]), Convert.ToString(row.ItemArray[2]), Convert.ToChar(row.ItemArray[3])));
            }
            cbx_email.DataSource = null;
            cbx_email.Items.Clear();
            cbx_email.SelectedIndex = -1;
            cbx_email.DisplayMember = "Correo_electronico";
            cbx_email.ValueMember = "id";
            cbx_email.DataSource = rem_logs;
            cbx_email.SelectedIndex = -1;
            tbx_password.Text = "";
            chb_rememberme.Checked = false;
        }

        private void LOG_IN_Load(object sender, EventArgs e)
        {
            cbx_usertype.SelectedIndex = 0;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (cbx_email.Text.Length == 0)
            {
                MessageBox.Show("Capture el correo electrónico.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (tbx_password.TextLength != 8)
            {
                MessageBox.Show("La contraseña debe contener 8 caracteres.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            switch (cbx_usertype.SelectedIndex)
            {
                case 0: // Cliente
                    {
                        Login log = new Login();
                        log.correo_e = cbx_email.Text;
                        log.contra = tbx_password.Text;
                        EnlaceDB link = new EnlaceDB();
                        switch (link.Autentificar(ref log, 'C'))
                        {
                            case 0:
                                if (chb_rememberme.Checked == true & cbx_email.SelectedIndex == -1)
                                {
                                    link.RecordarUsuario(ref log, 'C');
                                }
                                else if (chb_rememberme.Checked == false & cbx_email.SelectedIndex != -1)
                                {
                                    link.ElimRecordUser(Convert.ToInt16(cbx_email.SelectedValue));
                                }
                                Cliente.Cliente dialogCli = new Cliente.Cliente();
                                dialogCli.id = log.id;
                                dialogCli.username = log.username;
                                Hide();
                                dialogCli.ShowDialog();
                                Close();
                                break;
                            case 1:
                                if (mailused == cbx_email.Text) tries++;
                                else tries = 0;
                                if (tries == 2)
                                {
                                    link.BloquearUsuario(cbx_email.Text, 'C', 1);
                                    MessageBox.Show("Se ha bloqueado el usuario por demasiados intentos fallidos.", "Dato globito.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    MessageBox.Show("Contraseña incorrecta.", "Dato globito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    mailused = cbx_email.Text;
                                }
                                break;
                            case 2:
                                MessageBox.Show("Usuario bloqueado.", "Dato globito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            case 3:
                                MessageBox.Show("El correo electrónico no existe.", "Dato globito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                        break;
                    }
                case 1: // Empleado
                    {
                        Login log = new Login();
                        log.correo_e = cbx_email.Text;
                        log.contra = tbx_password.Text;
                        EnlaceDB link = new EnlaceDB();
                        switch (link.Autentificar(ref log, 'E'))
                        {
                            case 0:
                                if (chb_rememberme.Checked == true & cbx_email.SelectedIndex == -1)
                                {
                                    link.RecordarUsuario(ref log, 'E');
                                }
                                else if (chb_rememberme.Checked == false & cbx_email.SelectedIndex != -1)
                                {
                                    link.ElimRecordUser(Convert.ToInt16(cbx_email.SelectedValue));
                                }
                                Empleado.Empleado dialogEmp = new Empleado.Empleado();
                                dialogEmp.id = Convert.ToInt32(log.id);
                                dialogEmp.username = log.username;
                                Hide();
                                dialogEmp.ShowDialog();
                                Close();
                                break;
                            case 1:
                                if (mailused == cbx_email.Text) tries++;
                                else tries = 0;
                                if (tries == 2)
                                {
                                    link.BloquearUsuario(cbx_email.Text, 'E', 1);
                                    MessageBox.Show("Se ha bloqueado el usuario por demasiados intentos fallidos.", "Dato globito.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    MessageBox.Show("Contraseña incorrecta.", "Dato globito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    mailused = cbx_email.Text;
                                }
                                break;
                            case 2:
                                MessageBox.Show("Usuario bloqueado.", "Dato globito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            case 3:
                                MessageBox.Show("El correo electrónico no existe.", "Dato globito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                        break;
                    }
                case 2: // Administrador
                    {
                        Login log = new Login();
                        log.correo_e = cbx_email.Text;
                        log.contra = tbx_password.Text;
                        EnlaceDB link = new EnlaceDB();
                        switch (link.Autentificar(ref log, 'A'))
                        {
                            case 0:
                                if (chb_rememberme.Checked == true & cbx_email.SelectedIndex == -1)
                                {
                                    link.RecordarUsuario(ref log, 'A');
                                }
                                else if (chb_rememberme.Checked == false & cbx_email.SelectedIndex != -1)
                                {
                                    link.ElimRecordUser(Convert.ToInt16(cbx_email.SelectedValue));
                                }
                                Administrador.Administrador dialogAdmin = new Administrador.Administrador();
                                dialogAdmin.id = Convert.ToInt16(log.id);
                                dialogAdmin.username = log.username;
                                Hide();
                                dialogAdmin.ShowDialog();
                                Close();
                                break;
                            case 1:
                                MessageBox.Show("Contraseña incorrecta.", "Dato globito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            case 3:
                                MessageBox.Show("El correo electrónico no existe.", "Dato globito", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void cbx_usertype_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnlaceDB link = new EnlaceDB();
            switch (cbx_usertype.SelectedIndex)
            {
                case 2: // ADMIN
                    FillRememberList('A');
                    break;
                case 1: // EMPLEADO
                    FillRememberList('E');
                    break;
                case 0: // CLIENTE
                    FillRememberList('C');
                    break;
            }
        }

        private void cbx_email_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_email.SelectedIndex != -1)
            {
                LogRem look = new LogRem();
                look.id = Convert.ToInt16(cbx_email.SelectedValue);
                tbx_password.Text = rem_logs.Find(x => x.id == Convert.ToInt16(cbx_email.SelectedValue)).Contrasena;
                chb_rememberme.Checked = true;
            }
        }

        private void cbx_email_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbx_email.SelectedIndex != -1)
            {
                if (e.KeyChar == 8)
                {
                    chb_rememberme.Checked = false;
                    cbx_email.SelectedIndex = -1;
                    tbx_password.Text = "";
                    chb_rememberme.Checked = false;
                }
            }
        }
    }
}
