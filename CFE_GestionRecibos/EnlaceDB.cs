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
                var parametro1 = _comandosql.Parameters.Add("@type", SqlDbType.Char, 1);
                parametro1.Value = type;
                var parametro2 = _comandosql.Parameters.Add("@correo_e", SqlDbType.VarChar, 20);
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

                _adaptador.InsertCommand = _comandosql;
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

        public DataTable get_Users()
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "Select Nombre, email, Fecha_modif from Usuarios where Activo = 0;";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.Text;
                _comandosql.CommandTimeout = 1200;

                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }

        public DataTable get_Deptos(string opc)
        {
            var msg = "";
            DataTable tabla = new DataTable();
            try
            {
                conectar();
                string qry = "sp_Gestiona_Deptos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Opc", SqlDbType.Char, 1);
                parametro1.Value = opc;


                _adaptador.SelectCommand = _comandosql;
                _adaptador.Fill(tabla);

            }
            catch (SqlException e)
            {
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();
            }

            return tabla;
        }
        public bool Add_Deptos(string opc, string depto)
        {
            var msg = "";
            var add = true;
            try
            {
                conectar();
                string qry = "sp_Gestiona_Deptos";
                _comandosql = new SqlCommand(qry, _conexion);
                _comandosql.CommandType = CommandType.StoredProcedure;
                _comandosql.CommandTimeout = 1200;

                var parametro1 = _comandosql.Parameters.Add("@Opc", SqlDbType.Char, 1);
                parametro1.Value = opc;
                var parametro2 = _comandosql.Parameters.Add("@Nombre", SqlDbType.VarChar, 20);
                parametro2.Value = depto;

                _adaptador.InsertCommand = _comandosql;
                
                _comandosql.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                add = false;
                msg = "Excepción de base de datos: \n";
                msg += e.Message;
                MessageBox.Show(msg, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                desconectar();                
            }

            return add;
        }

    }
}
