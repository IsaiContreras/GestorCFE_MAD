using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CFE_GestionRecibos
{
    public class Login
    {
        public string correo_e;
        public string contra;
        public long id;
        public string username;
    }

    public class LogRem
    {
        public LogRem() { }
        public LogRem(short id, string corr, string contra, char type)
        {
            this.id = id;
            Correo_electronico = corr;
            Contrasena = contra;
            Tipo = type;
        }
        public short id { get; set; }
        public char Tipo { get; set; }
        public string Correo_electronico { get; set; }
        public string Contrasena { get; set; }
    }

    public class AdminClass
    {
        public long id_adm;
        public string nom;
    }

    public class EmpleadoClass
    {
        public EmpleadoClass()
        {

        }
        public EmpleadoClass(DataRow dato, int numero)
        {
            num_emp = numero;
            nom = Convert.ToString(dato.ItemArray[0]);
            ape = Convert.ToString(dato.ItemArray[1]);
            fec_nac = (DateTime)dato.ItemArray[2];
            curp = Convert.ToString(dato.ItemArray[3]);
            rfc = Convert.ToString(dato.ItemArray[4]);
            correo_e = Convert.ToString(dato.ItemArray[5]);
            contra = Convert.ToString(dato.ItemArray[6]);
        }
        public EmpleadoClass(short admin, string nombres, string apellidos, DateTime fechaNac, string curp, string rfc, string email, string password)
        {
            id_admin = admin;
            nom = nombres;
            ape = apellidos;
            fec_nac = fechaNac;
            this.curp = curp;
            this.rfc = rfc;
            correo_e = email;
            contra = password;
        }
        public int num_emp;
        public string nom;
        public string ape;
        public DateTime fec_nac;
        public string rfc;
        public string curp;
        public List<string> tel;
        public string correo_e;
        public string contra;
        public bool activo;
        public short id_admin;
    }

    public class ClienteClass
    {
        public ClienteClass()
        {

        }
        public ClienteClass(DataRow dato, long id)
        {
            id_cl = id;
            nom = Convert.ToString(dato.ItemArray[0]);
            ape = Convert.ToString(dato.ItemArray[1]);
            fec_nac = (DateTime)dato.ItemArray[2];
            string domic = Convert.ToString(dato.ItemArray[3]);
            dom = new Domicilio(domic);
            curp = Convert.ToString(dato.ItemArray[4]);
            correo_e = Convert.ToString(dato.ItemArray[5]);
            contra = Convert.ToString(dato.ItemArray[6]);
        }
        public ClienteClass(int numero_emp, string nombres, string apellidos, DateTime fechaNac, Domicilio domic, string curp, string email, string password)
        {
            num_emp = numero_emp;
            nom = nombres;
            ape = apellidos;
            fec_nac = fechaNac;
            dom = domic;
            this.curp = curp;
            correo_e = email;
            contra = password;
        }
        public long id_cl;
        public string nom;
        public string ape;
        public DateTime fec_nac;
        public string curp;
        public Domicilio dom;
        public List<string> telefonos;
        public string correo_e;
        public string contra;
        public bool activo;
        public int num_emp;
    }

    public class ServicioClass
    {
        public ServicioClass()
        {

        }
        public ServicioClass(DataRow dato, long id)
        {
            id_ser = id;
            med = Convert.ToInt64(dato.ItemArray[0]);
            string domic = Convert.ToString(dato.ItemArray[1]);
            dom = new Domicilio(domic);
            if (Convert.ToString(dato.ItemArray[2]) == "Industrial")
                tipo_ser = true;
            else tipo_ser = false;
        }
        public ServicioClass(long id_cl, int num_emp, long medidor, Domicilio domic, bool tipo_servicio)
        {
            this.id_cl = id_cl;
            this.num_emp = num_emp;
            med = medidor;
            dom = domic;
            tipo_ser = tipo_servicio;
        }
        public long id_ser;
        public long med;
        public DateTime fecha_alta;
        public bool tipo_ser;
        public Domicilio dom;
        public bool activo;
        public long id_cl;
        public int num_emp;
    }

    public class TarifaClass
    {
        public TarifaClass()
        {

        }
        public TarifaClass(int numemp, bool tipo, int año, byte mes, double t_bas, double t_int, double t_exc)
        {
            tipo_serv = tipo;
            this.año = año;
            this.mes = mes;
            tar_bas = t_bas;
            tar_inter = t_int;
            tar_exced = t_exc;
            num_emp = numemp;
        }
        public bool tipo_serv;
        public int año;
        public byte mes;
        public double tar_bas;
        public double tar_inter;
        public double tar_exced;
        public int num_emp;
    }

    public class ConsumoClass
    {
        public ConsumoClass()
        {

        }
        public ConsumoClass(int numemp, int id_kw, long medidor, int año, byte mes, int consumo)
        {
            num_emp = numemp;
            this.id_kw = id_kw;
            this.medidor = medidor;
            this.año = año;
            this.mes = mes;
            consumotot = consumo;
        }
        public long medidor;
        public int año;
        public byte mes;
        public int consumotot;
        public int num_emp;
        public int id_kw;
    }

    public class RecibosClass
    {
        public long id_ser;
        public long id_rec;
        public string nombre_cl;
        public int year;
        public byte month;
        public string domicilio_cl;
        public bool tipo_ser;
        public long medidor;
        public DateTime fec_venc;
        public string domicilio;
        public bool pagado;
        public double pago_total;
        public double cargo_iva;
        public double precio_bas;
        public double precio_inter;
        public double precio_exced;
        public double importe_pago;
        public double pendiente_pago;
    }

    public class Domicilio
    {
        public Domicilio()
        {

        }
        public Domicilio(string dom)
        {
            string[] parts = dom.Split(',');
            if (parts.Length == 7)
            {
                calle = parts[0];
                numext = parts[1];
                numint = parts[2];
                col = parts[3];
                munic = parts[4];
                estado = parts[5];
                cp = parts[6];
            }
        }
        public Domicilio(string calle, string numext, string numint, string col, string munic, string estado, string cp)
        {
            this.calle = calle;
            this.numext = numext;
            this.numint = numint;
            this.col = col;
            this.munic = munic;
            this.estado = estado;
            this.cp = cp;
        }
        public string calle { get; set; }
        public string numext { get; set; }
        public string numint { get; set; }
        public string col { get; set; }
        public string munic { get; set; }
        public string estado { get; set; }
        public string cp { get; set; }
        public string getAssembled()
        {
            return calle + ',' + numext + ',' + numint + ',' + col + ',' + munic + ',' + estado + ',' + cp;
        }
        public string getFormat()
        {
            string numinterstr = "";
            if (numint.Length > 0)
            {
                numinterstr = ", #" + numint;
            }
            return calle + " #" + numext + numinterstr + ", Col. " + col + ", " + munic + ", " + estado + ". " + cp;
        }
    }

    public class Telefono_cl
    {
        public int id;
        public string telefono;
        public long id_cl;
    }

    public class Telefono_emp
    {
        public int id;
        public string telefono;
        public long id_cl;
    }

    class RegexUtilities
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        public static bool IsDecimalNumber(string text)
        {
            Regex r = new Regex(@"^\d+\.?\d*$");
            if (r.IsMatch(text))
                return true;
            else
                return false;
        }
        public static bool IsOnlyNumerics(string text)
        {
            Regex r = new Regex("^[0-9]+$");
            if (r.IsMatch(text))
                return true;
            else
                return false;
        }
        public static bool IsOnlyAlphas(string text)
        {
            Regex r = new Regex("^[a-zA-Z]+$");
            if (r.IsMatch(text))
                return true;
            else
                return false;
        }
    }
}
