using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public long id_admin;
        public string nom;
        public string ape;
        public DateTime fec_nac;
        public string rfc;
        public string curp;
        public List<string> tel;
        public string correo_e;
        public string contra;
        public bool activo;
    }

    public class ClienteClass
    {
        public long id_cl;
        public string nombres;
        public string apellidos;
        public DateTime fec_nac;
        public string domicilio;
        public List<string> telefonos;
        public string correo_e;
        public string contra;
        public bool activo;
    }

    public class ServicioClass
    {
        public long id_cl;
        public long id_ser;
        public long medidor;
        public bool tipo_ser;
        public string domicilio;
        public bool activo;
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
}
