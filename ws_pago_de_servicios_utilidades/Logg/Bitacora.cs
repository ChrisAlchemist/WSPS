
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws_pago_de_servicios_utilidades.Logg
{
   public class Bitacora<T> where T : class
    {
        public Bitacora()
        {

        }
        public Bitacora(String numeroSocioBitacora , T value ,string Metodo="", string  ex = "")
        {
            this.FechaBitacora=System.DateTime.Now.ToShortDateString();
            this.HoraBitacora =System.DateTime.Now.ToShortTimeString();
            this.numeroSocioBitacora = numeroSocioBitacora;
            this.Parametros = value;
            this.Metodo = Metodo;
            //this.ex = ex;
        }

        public Bitacora(T value, string Metodo = "", string ex = "")
        {
            this.FechaBitacora = System.DateTime.Now.ToShortDateString();
            this.HoraBitacora = System.DateTime.Now.ToShortTimeString();
            this.numeroSocioBitacora = numeroSocioBitacora;
            this.Parametros = value;
            this.Metodo = Metodo;
            //this.ex = ex;
        }

        public Bitacora(T value )
        {
            this.FechaBitacora = System.DateTime.Now.ToShortDateString();
            this.HoraBitacora = System.DateTime.Now.ToShortTimeString();
            this.numeroSocioBitacora = numeroSocioBitacora;
            this.Parametros = value;
            this.Metodo = Metodo;
            //this.ex = ex;
        }

        public String FechaBitacora { get; set; }

        public String HoraBitacora { get; set; }
        public  string numeroSocioBitacora { get; set; }
        //public string  ex { get; set; }
        public T Parametros { get; set; }
        public string Metodo { get; set; }


    }

}
