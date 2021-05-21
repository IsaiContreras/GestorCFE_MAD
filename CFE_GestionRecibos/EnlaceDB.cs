using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace CFE_GestionRecibos
{
    public class EnlaceDB
    {
        static private string _aux { set; get; }
        static private SqlConnection _conexion;
        static private SqlDataAdapter _adaptador = new SqlDataAdapter();
        static private SqlCommand _comandosql = new SqlCommand();
        static private DataTable _tabla = new DataTable();
        static private DataSet _DS = new DataSet();

        public DataTable obtenertabla
        {
            get
            {
                return _tabla;
            }
        }

        private static void conectar()
        {
            //string cnn = ConfigurationManager.AppSettings["desarrollo1"];
            string cnn = ConfigurationManager.ConnectionStrings["GestorCFE"].ToString();
            _conexion = new SqlConnection(cnn);
            _conexion.Open();
        }
        private static void desconectar()
        {
            _conexion.Close();
        }

        // LOG_IN
        public byte Autentificar(ref Login param, char type)
        {
            DataTable table = new DataTable();
            try
            {
                conectar();
                string qry = "sp_Logins";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;
                var parametro1 = _comandosql.Parameters.Add("@type", SqlDbType.Char);
                parametro1.Value = type;
                var parametro2 = _comandosql.Parameters.Add("@correo_e", SqlDbType.VarChar, 60);
                parametro2.Value = param.correo_e;
                var parametro3 = _comandosql.Parameters.Add("@contra", SqlDbType.Char, 8);
                parametro3.Value = param.contra;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(table);

                DataRow s = table.Rows[0];

                if (Convert.ToInt16(table.Rows[0].ItemArray[0]) == -1)
                    return 1;
                else if (Convert.ToInt16(table.Rows[0].ItemArray[0]) == -2)
                    return 2;
                else if (Convert.ToInt16(table.Rows[0].ItemArray[0]) == -3)
                    return 3;
                else
                {
                    param.id = Convert.ToInt64(table.Rows[0].ItemArray[0]);
                    param.username = (string)table.Rows[0].ItemArray[1];
                }
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 10;
            }
            finally
            {
                desconectar();
            }
            return 0;
        }
        public bool BloquearUsuario(string correo, char type, byte bloq)
        {
            try
            {
                conectar();
                string qry = "sp_Bloq";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@type", SqlDbType.Char, 1);
                parametro1.Value = type;
                var parametro2 = _comandosql.Parameters.Add("@correo_e", SqlDbType.VarChar, 60);
                parametro2.Value = correo;
                var parametro3 = _comandosql.Parameters.Add("@lock", SqlDbType.Bit);
                parametro3.Value = bloq;

                _adaptador.UpdateCommand = _comandosql;
                _comandosql.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
            return true;
        }
        public DataTable ObtenerRecordar(char type)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Recordar_contra";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                parametro1.Value = "showbytype";
                var parametro2 = _comandosql.Parameters.Add("@tip_us", SqlDbType.Char, 1);
                parametro2.Value = type;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }

        }
        public bool RecordarUsuario(ref Login param, char type)
        {
            try
            {
                conectar();
                string qry = "sp_Recordar_contra";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                parametro1.Value = "insert";
                var parametro2 = _comandosql.Parameters.Add("@correo_e", SqlDbType.VarChar, 60);
                parametro2.Value = param.correo_e;
                var parametro3 = _comandosql.Parameters.Add("@contra", SqlDbType.Char, 8);
                parametro3.Value = param.contra;
                var parametro4 = _comandosql.Parameters.Add("@tip_us", SqlDbType.Char, 1);
                parametro4.Value = type;

                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
            return true;
        }
        public bool ElimRecordUser(short id)
        {
            try
            {
                conectar();
                string qry = "sp_Recordar_contra";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                parametro1.Value = "delete";
                var parametro2 = _comandosql.Parameters.Add("@id_rc", SqlDbType.SmallInt);
                parametro2.Value = id;

                _adaptador.DeleteCommand = _comandosql;
                _comandosql.ExecuteNonQuery();
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
            return true;
        }

        // ADMINISTRADOR
        public DataTable LlenarEmpleados()
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Empleados";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var proc_par = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                proc_par.Value = "showall";

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataRow ObtenerEmpleado(int id_emp)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Empleados";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parámetro1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                parámetro1.Value = "searchbyid";
                var parámetro2 = _comandosql.Parameters.Add("@num_emp", SqlDbType.Int);
                parámetro2.Value = id_emp;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt.Rows[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataRow DatosEmpleado(int id_emp)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Empleados";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parámetro1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                parámetro1.Value = "searchtomod";
                var parámetro2 = _comandosql.Parameters.Add("@num_emp", SqlDbType.Int);
                parámetro2.Value = id_emp;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt.Rows[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataTable LlenarRegistroAct(int id_emp)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_RegistroActividad";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                parametro1.Value = "showbyemp";
                var parametro2 = _comandosql.Parameters.Add("@num_emp", SqlDbType.Int);
                parametro2.Value = id_emp;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataRow ObtenerRegistroAct(long id)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_RegistroActividad";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                parametro1.Value = "searchbyid";
                var parametro2 = _comandosql.Parameters.Add("@clave", SqlDbType.BigInt);
                parametro2.Value = id;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt.Rows[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public byte AgregarEmpleado(EmpleadoClass emp)
        {
            try
            {
                conectar();
                string qry = "sp_Empleados";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "insert";
                var param2 = _comandosql.Parameters.Add("@id_adm", SqlDbType.SmallInt);
                param2.Value = emp.id_admin;
                var param3 = _comandosql.Parameters.Add("@nom", SqlDbType.VarChar, 60);
                param3.Value = emp.nom;
                var param4 = _comandosql.Parameters.Add("@ape", SqlDbType.VarChar, 60);
                param4.Value = emp.ape;
                var param5 = _comandosql.Parameters.Add("@fec_nac", SqlDbType.Date);
                param5.Value = emp.fec_nac;
                var param6 = _comandosql.Parameters.Add("@curp", SqlDbType.Char, 18);
                param6.Value = emp.curp;
                var param7 = _comandosql.Parameters.Add("@rfc", SqlDbType.Char, 13);
                param7.Value = emp.rfc;
                var param8 = _comandosql.Parameters.Add("@correo_e", SqlDbType.VarChar, 60);
                param8.Value = emp.correo_e;
                var param9 = _comandosql.Parameters.Add("@contra", SqlDbType.Char, 8);
                param9.Value = emp.contra;

                _adaptador.InsertCommand = _comandosql;
                if (_comandosql.ExecuteNonQuery() == 0)
                {
                    return 1;
                }

                return 0;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 10;
            }
            finally
            {
                desconectar();
            }
        }
        public byte ModificarEmpleado(EmpleadoClass newemp, EmpleadoClass oldemp)
        {
            try
            {
                conectar();
                string qry = "sp_Empleados";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "update";
                var param2 = _comandosql.Parameters.Add("@num_emp", SqlDbType.Int);
                param2.Value = oldemp.num_emp;
                if (newemp.nom != oldemp.nom)
                {
                    var param3 = _comandosql.Parameters.Add("@nom", SqlDbType.VarChar, 60);
                    param3.Value = newemp.nom;
                }
                if (newemp.ape != oldemp.ape)
                {
                    var param4 = _comandosql.Parameters.Add("@ape", SqlDbType.VarChar, 60);
                    param4.Value = newemp.ape;
                }
                if (newemp.fec_nac != oldemp.fec_nac)
                {
                    var param5 = _comandosql.Parameters.Add("@fec_nac", SqlDbType.Date);
                    param5.Value = newemp.fec_nac;
                }
                if (newemp.rfc != oldemp.rfc)
                {
                    var param6 = _comandosql.Parameters.Add("@rfc", SqlDbType.Char, 13);
                    param6.Value = newemp.rfc;
                }
                if (newemp.curp != oldemp.curp)
                {
                    var param7 = _comandosql.Parameters.Add("@curp", SqlDbType.Char, 18);
                    param7.Value = newemp.curp;
                }
                if (newemp.correo_e != oldemp.correo_e)
                {
                    var param8 = _comandosql.Parameters.Add("@correo_e", SqlDbType.VarChar, 60);
                    param8.Value = newemp.correo_e;
                }
                if (newemp.contra != oldemp.contra)
                {
                    var param9 = _comandosql.Parameters.Add("@contra", SqlDbType.Char, 8);
                    param9.Value = newemp.contra;
                }

                _adaptador.UpdateCommand = _comandosql;
                if (_comandosql.ExecuteNonQuery() == 0)
                {
                    return 1;
                }
                return 0;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 10;
            }
            finally
            {
                desconectar();
            }
        }
        public bool EliminarEmpleado(int id_emp)
        {
            try
            {
                conectar();
                string qry = "sp_Empleados";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                parametro1.Value = "delete";
                var parametro2 = _comandosql.Parameters.Add("@num_emp", SqlDbType.Int);
                parametro2.Value = id_emp;

                _adaptador.DeleteCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }

        // EMPLEADO
        public DataTable LlenarClientes()
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Clientes";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var proc_par = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                proc_par.Value = "showall";

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataRow ObtenerCliente(long id_cl)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Clientes";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "searchbyid";
                var param2 = _comandosql.Parameters.Add("@id_cl", SqlDbType.BigInt);
                param2.Value = id_cl;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt.Rows[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataRow DatosCliente(long id_cl)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Clientes";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "searchtomod";
                var param2 = _comandosql.Parameters.Add("@id_cl", SqlDbType.BigInt);
                param2.Value = id_cl;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt.Rows[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataTable LlenarServicios(long id_cl)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Servicios";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "searchbycl";
                var param2 = _comandosql.Parameters.Add("@id_cl", SqlDbType.BigInt);
                param2.Value = id_cl;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataRow DatosServicio(long id_serv)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Servicios";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "searchtomod";
                var param2 = _comandosql.Parameters.Add("@id_ser", SqlDbType.BigInt);
                param2.Value = id_serv;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt.Rows[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataTable LlenarRecibos(long id_serv)
        {
            DataTable dt = new DataTable();
            conectar();
            string qry = "sp_Recibos";
            _comandosql = new SqlCommand(qry, _conexion);
            _comandosql.CommandType = CommandType.StoredProcedure;
            _comandosql.CommandTimeout = 9000;

            var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
            param1.Value = "searchbyserv";
            var param2 = _comandosql.Parameters.Add("@id_ser", SqlDbType.BigInt);
            param2.Value = id_serv;

            _adaptador.SelectCommand = _comandosql;
            _adaptador.Fill(dt);
            return dt;
        }
        public DataRow DatosRecibo(long id_rec)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Recibos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "searchbyid";
                var param2 = _comandosql.Parameters.Add("@id_rec", SqlDbType.BigInt);
                param2.Value = id_rec;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt.Rows[0];
            }
            catch(SqlException e){
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataTable NivelesKilowatts()
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Kilowatts";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "showall";

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataTable ReporteTarifas(int año)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Tarifas";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "searchbyyear";
                var param2 = _comandosql.Parameters.Add("@año", SqlDbType.Int);
                param2.Value = año;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt;
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataTable ReporteConsumos(int año)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Consumos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "searchbyyear";
                var param2 = _comandosql.Parameters.Add("@año", SqlDbType.Int);
                param2.Value = año;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }
        public DataTable ConsumoHistorico(int año, long medidor)
        {
            try
            {
                DataTable dt = new DataTable();
                conectar();
                string qry = "sp_Consumos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "historic";
                var param2 = _comandosql.Parameters.Add("@año", SqlDbType.Int);
                param2.Value = año;
                var param3 = _comandosql.Parameters.Add("@med", SqlDbType.BigInt);
                param3.Value = medidor;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(dt);

                return dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                desconectar();
            }
        }

        public byte AgregarCliente(ClienteClass client)
        {
            try
            {
                conectar();
                string qry = "sp_Clientes";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "insert";
                var param2 = _comandosql.Parameters.Add("@num_emp", SqlDbType.Int);
                param2.Value = client.num_emp;
                var param3 = _comandosql.Parameters.Add("@nom", SqlDbType.VarChar, 60);
                param3.Value = client.nom;
                var param4 = _comandosql.Parameters.Add("@ape", SqlDbType.VarChar, 60);
                param4.Value = client.ape;
                var param5 = _comandosql.Parameters.Add("@fec_nac", SqlDbType.Date);
                param5.Value = client.fec_nac;
                var param6 = _comandosql.Parameters.Add("@curp", SqlDbType.Char, 18);
                param6.Value = client.curp;
                var param7 = _comandosql.Parameters.Add("@correo_e", SqlDbType.VarChar, 60);
                param7.Value = client.correo_e;
                var param8 = _comandosql.Parameters.Add("@contra", SqlDbType.Char, 8);
                param8.Value = client.contra;
                var param9 = _comandosql.Parameters.Add("@dom", SqlDbType.VarChar, 128);
                param9.Value = client.dom.getAssembled();

                _adaptador.InsertCommand = _comandosql;
                if(_comandosql.ExecuteNonQuery() == 0)
                {
                    return 1;
                }
                return 0;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 10;
            }
            finally
            {
                desconectar();
            }
        }
        public byte ModificarCliente(ClienteClass newclient, ClienteClass oldclient)
        {
            try
            {
                conectar();
                string qry = "sp_Clientes";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "update";
                var param2 = _comandosql.Parameters.Add("@id_cl", SqlDbType.BigInt);
                param2.Value = oldclient.id_cl;
                if (newclient.nom != oldclient.nom)
                {
                    var param3 = _comandosql.Parameters.Add("@nom", SqlDbType.VarChar, 60);
                    param3.Value = newclient.nom;
                }
                if (newclient.ape != oldclient.ape)
                {
                    var param4 = _comandosql.Parameters.Add("@ape", SqlDbType.VarChar, 60);
                    param4.Value = newclient.ape;
                }
                if (newclient.fec_nac != oldclient.fec_nac)
                {
                    var param5 = _comandosql.Parameters.Add("@fec_nac", SqlDbType.Date);
                    param5.Value = newclient.fec_nac;
                }
                if (newclient.curp != oldclient.curp)
                {
                    var param6 = _comandosql.Parameters.Add("@curp", SqlDbType.Char, 18);
                    param6.Value = newclient.curp;
                }
                if (newclient.dom != oldclient.dom)
                {
                    var param7 = _comandosql.Parameters.Add("@dom", SqlDbType.VarChar, 128);
                    param7.Value = newclient.dom.getAssembled();
                }
                if (newclient.correo_e != oldclient.correo_e)
                {
                    var param8 = _comandosql.Parameters.Add("@correo_e", SqlDbType.VarChar, 60);
                    param8.Value = newclient.correo_e;
                }
                if (newclient.contra != oldclient.contra)
                {
                    var param9 = _comandosql.Parameters.Add("@contra", SqlDbType.Char, 8);
                    param9.Value = newclient.contra;
                }

                _adaptador.UpdateCommand = _comandosql;
                if (_comandosql.ExecuteNonQuery() == 0)
                {
                    return 1;
                }
                return 0;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 10;
            }
            finally
            {
                desconectar();
            }
        }
        public bool EliminarCliente(long id_cl)
        {
            try
            {
                conectar();
                string qry = "sp_Clientes";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                parametro1.Value = "delete";
                var parametro2 = _comandosql.Parameters.Add("@id_cl", SqlDbType.BigInt);
                parametro2.Value = id_cl;

                _adaptador.DeleteCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }
        public byte AgregarServicio(ServicioClass serv)
        {
            try
            {
                conectar();
                string qry = "sp_Servicios";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "insert";
                var param2 = _comandosql.Parameters.Add("@id_cl", SqlDbType.BigInt);
                param2.Value = serv.id_cl;
                var param3 = _comandosql.Parameters.Add("@num_emp", SqlDbType.Int);
                param3.Value = serv.num_emp;
                var param4 = _comandosql.Parameters.Add("@med", SqlDbType.BigInt);
                param4.Value = serv.med;
                var param5 = _comandosql.Parameters.Add("@tip_ser", SqlDbType.Bit);
                param5.Value = serv.tipo_ser;
                var param6 = _comandosql.Parameters.Add("@dom", SqlDbType.VarChar, 128);
                param6.Value = serv.dom.getAssembled();

                _adaptador.InsertCommand = _comandosql;
                if (_comandosql.ExecuteNonQuery() == 0)
                {
                    return 1;
                }
                return 0;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 10;
            }
            finally
            {
                desconectar();
            }
        }
        public byte ModificarServicio(ServicioClass newserv, ServicioClass oldserv)
        {
            try
            {
                conectar();
                string qry = "sp_Servicios";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "update";
                var param2 = _comandosql.Parameters.Add("@id_ser", SqlDbType.BigInt);
                param2.Value = oldserv.id_ser;
                if (newserv.med != oldserv.med)
                {
                    var param3 = _comandosql.Parameters.Add("@med", SqlDbType.BigInt);
                    param3.Value = newserv.med;
                }
                if (newserv.tipo_ser != oldserv.tipo_ser)
                {
                    var param4 = _comandosql.Parameters.Add("@tip_ser", SqlDbType.Bit);
                    param4.Value = newserv.tipo_ser;
                }
                if (newserv.dom != oldserv.dom)
                {
                    var param5 = _comandosql.Parameters.Add("@dom", SqlDbType.VarChar, 128);
                    param5.Value = newserv.dom.getAssembled();
                }

                _adaptador.UpdateCommand = _comandosql;
                if (_comandosql.ExecuteNonQuery() == 0)
                {
                    return 1;
                }
                return 0;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 10;
            }
            finally
            {
                desconectar();
            }
        }
        public bool EliminarServicio(long id_serv)
        {
            try
            {
                conectar();
                string qry = "sp_Servicios";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var parametro1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                parametro1.Value = "delete";
                var parametro2 = _comandosql.Parameters.Add("@id_serv", SqlDbType.BigInt);
                parametro2.Value = id_serv;

                _adaptador.DeleteCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }

        public byte AgregarTarifa(TarifaClass tarifa)
        {
            try
            {
                conectar();
                string qry = "sp_Tarifas";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "insert";
                var param2 = _comandosql.Parameters.Add("@num_emp", SqlDbType.Int);
                param2.Value = tarifa.num_emp;
                var param3 = _comandosql.Parameters.Add("@año", SqlDbType.Int);
                param3.Value = tarifa.año;
                var param4 = _comandosql.Parameters.Add("@mes", SqlDbType.TinyInt);
                param4.Value = tarifa.mes;
                var param5 = _comandosql.Parameters.Add("@tip_ser", SqlDbType.Bit);
                param5.Value = tarifa.tipo_serv;
                var param6 = _comandosql.Parameters.Add("@tar_b", SqlDbType.Decimal, 7);
                param6.Value = tarifa.tar_bas;
                var param7 = _comandosql.Parameters.Add("@tar_i", SqlDbType.Decimal, 7);
                param7.Value = tarifa.tar_inter;
                var param8 = _comandosql.Parameters.Add("@tar_e", SqlDbType.Decimal, 7);
                param8.Value = tarifa.tar_exced;

                _adaptador.InsertCommand = _comandosql;
                if (_comandosql.ExecuteNonQuery() == 0)
                {
                    return 1;
                }
                return 0;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 10;
            }
            finally
            {
                desconectar();
            }
        }
        public byte AgregarConsumo(ConsumoClass consumo)
        {
            try
            {
                conectar();
                string qry = "sp_Consumos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "insert";
                var param2 = _comandosql.Parameters.Add("@num_emp", SqlDbType.Int);
                param2.Value = consumo.num_emp;
                var param3 = _comandosql.Parameters.Add("@id_kw", SqlDbType.Int);
                param3.Value = consumo.id_kw;
                var param4 = _comandosql.Parameters.Add("@año", SqlDbType.Int);
                param4.Value = consumo.año;
                var param5 = _comandosql.Parameters.Add("@mes", SqlDbType.TinyInt);
                param5.Value = consumo.mes;
                var param6 = _comandosql.Parameters.Add("@med", SqlDbType.BigInt);
                param6.Value = consumo.medidor;
                var param7 = _comandosql.Parameters.Add("@kw_tot", SqlDbType.Int);
                param7.Value = consumo.consumotot;

                _adaptador.InsertCommand = _comandosql;
                if (_comandosql.ExecuteNonQuery() == 0)
                {
                    return 1;
                }
                return 0;
            }
            catch(SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 10;
            }
            finally
            {
                desconectar();
            }
        }
        public bool GenerarRecibos(int año, sbyte mes, byte tipo_serv, int num_emp)
        {
            try
            {
                conectar();
                string qry = "sp_Recibos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "generate";
                var param2 = _comandosql.Parameters.Add("@año", SqlDbType.Int);
                param2.Value = año;
                var param3 = _comandosql.Parameters.Add("@mes", SqlDbType.TinyInt);
                param3.Value = mes;
                var param4 = _comandosql.Parameters.Add("@tip_ser", SqlDbType.Bit);
                param4.Value = tipo_serv;
                var param5 = _comandosql.Parameters.Add("@num_emp", SqlDbType.Int);
                param5.Value = num_emp;
                
                _adaptador.InsertCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            finally
            {
                desconectar();
            }
        }

        // CLIENTE
        public bool Pago(long id_rec, double pago)
        {
            try
            {
                conectar();
                string qry = "sp_Recibos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;

                var param1 = _comandosql.Parameters.Add("@proc", SqlDbType.VarChar, 16);
                param1.Value = "payment";
                var param2 = _comandosql.Parameters.Add("@id_rec", SqlDbType.BigInt);
                param2.Value = id_rec;
                var param3 = _comandosql.Parameters.Add("@pago", SqlDbType.Money);
                param3.Value = pago;

                _adaptador.UpdateCommand = _comandosql;
                _comandosql.ExecuteNonQuery();

                return true;
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                desconectar();
            }
        }

        // REGISTRO ACTIVIDAD
        public void RegistrarActividad(string action, int num_emp, long id_inserted)
        {
            try
            {
                conectar();
                string qry = "sp_RegistroActividad";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 9000;
                
            }
            catch (SqlException e)
            {

            }
            finally
            {

            }
        }

    }
}
