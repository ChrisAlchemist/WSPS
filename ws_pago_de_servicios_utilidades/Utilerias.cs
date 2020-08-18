using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
using System.ServiceModel;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Web;

namespace ws_pago_de_servicios_utilidades
{
    public static class Utilerias
    {
        public static string ExcepcionDebug(Exception ex)
        {
            return string.Empty;
        }
        public static string Enmascarar(String cadena)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(cadena))
            {
                string[] mascara = cadena.Split(' ');
                try
                {
                    foreach (string s in mascara)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            if (s.Length <= 2)
                                result += new String('*', s.Length) + ' ';
                            else
                                result += s[0] + new String('*', s.Length - 1) + ' ';
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            return result;

        }
        public static bool validaUsuario(string usuario, string contrasena)
        {
            if (string.Compare(usuario, "CMVF1nZ4S") == 0 && string.Compare(contrasena, "8DED40B6E19F14D1651FDDF063CDD2") == 0)
                return true;
            else
            {
                throw new FaultException("Usuario o contraseña incorrectos", new FaultCode("1000"));
            }
        }
        public static string EscribirLog(string mensaje)
        {
            string resultado = "todo bien";
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "LogBancaCMV");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                System.IO.File.WriteAllText(Path.Combine(folder, @"Log_" + Guid.NewGuid() + ".txt"), mensaje);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;
        }
        public static string GenerarContraseña()
        {
            int length = 10;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890)(/&%$#";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
 
    }
  
}