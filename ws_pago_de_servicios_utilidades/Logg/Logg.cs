
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ws_pago_de_servicios_utilidades.Logg
{
    public class Logg
    {
        EventLog m_EventLog = null;
        private String NumeroSocio = string.Empty;
        public Logg()
        {
            try
            {
                if (m_EventLog == null)
                    m_EventLog = new EventLog("GestoPago CMV");
                m_EventLog.Source = "GestoPago CMV";
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear Event Log"); ;
            }
            
        }

        private string Fuente { get; set; }

        public void Info(string mensaje )
        {
            try
            {
                string fechaHora ="Fecha :"+ System.DateTime.Now.ToShortDateString() + "\n"+"Hora :"+System.DateTime.Now.ToShortTimeString()+"\n"+mensaje;
                fechaHora = fechaHora.Length > 32766 ? fechaHora.Substring(0, 32766) : fechaHora;
                m_EventLog.WriteEntry(fechaHora, EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                 Utilerias.EscribirLog("fecha: " + System.DateTime.Now.ToShortDateString() + "Hora: "+System.DateTime.Now.ToShortTimeString()+"\n Error: " + "No se puede escribir en EventVier \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void Warning(string mensaje )
        {
            try
            {
                string fechaHora = "Fecha :" + System.DateTime.Now.ToShortDateString() + "\n" + "Hora :" + System.DateTime.Now.ToShortTimeString() + "\n" + mensaje;
                fechaHora = fechaHora.Length > 32766 ? fechaHora.Substring(0, 32766) : fechaHora;
                m_EventLog.WriteEntry(fechaHora, EventLogEntryType.Warning);
            }
            catch (Exception ex)
            {
                Utilerias.EscribirLog("fecha: " + System.DateTime.Now.ToShortDateString() + "Hora: " + System.DateTime.Now.ToShortTimeString() + "\n Error: " + "No se puede escribir en EventVier \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void Error(string mensaje  )
        {
            try
            {
                string fechaHora = "Fecha :" + System.DateTime.Now.ToShortDateString() + "\n" + "Hora :" + System.DateTime.Now.ToShortTimeString() + "\n" + mensaje;
                fechaHora = fechaHora.Length > 32766 ? fechaHora.Substring(0, 32750) : fechaHora;
                m_EventLog.WriteEntry(fechaHora, EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                Utilerias.EscribirLog("fecha: " + System.DateTime.Now.ToShortDateString() + "Hora: " + System.DateTime.Now.ToShortTimeString() + "\n Error: " + "No se puede escribir en EventVier \n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void gitIgnore()
        {
        }
    }
}
