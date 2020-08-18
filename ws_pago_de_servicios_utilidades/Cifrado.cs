using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ws_pago_de_servicios_utilidades
{
    public static class Cifrado
    {

        // se define el tamaño del key y del vector

        private static readonly int tamanyoClave = 32;
        private static readonly int tamanyoVector = 16;

        

        // se convierte el vector y la key a bytes

        public static byte[] Key = null;
        public static byte[] IV = null;



        public static string CifradoTexto(String txtPlano)

        {
            string strKey = ConexionAPI.secretKey.Trim().Replace(" ", "+");
             string strIV = ConexionAPI.IV.Trim().Replace(" ", "+");


            Key = Convert.FromBase64String(FormarBase64(strKey));
            IV = Convert.FromBase64String(FormarBase64(strIV));

            Array.Resize(ref Key, tamanyoClave);
            Array.Resize(ref IV, tamanyoVector);

            // se instancia el Rijndael

            Rijndael RijndaelAlg = Rijndael.Create();
            RijndaelAlg.Mode = CipherMode.CBC;
            RijndaelAlg.Padding = PaddingMode.PKCS7;
            // se establece cifrado

            MemoryStream memoryStream = new MemoryStream();

            // se crea el flujo de datos de cifrado

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                RijndaelAlg.CreateEncryptor(Key, IV),
                CryptoStreamMode.Write);

            // se obtine la información a cifrar

            byte[] txtPlanoBytes = UTF8Encoding.UTF8.GetBytes(txtPlano);

            // se cifran los datos

            cryptoStream.Write(txtPlanoBytes, 0, txtPlanoBytes.Length);

            cryptoStream.FlushFinalBlock();

            // se obtinen los datos cifrados

            byte[] cipherMessageBytes = memoryStream.ToArray();

            // se cierra todo

            memoryStream.Close();
            cryptoStream.Close();

            // Se devuelve la cadena cifrada

            return Convert.ToBase64String(cipherMessageBytes);
        }

        /**
		 * Descifra una cadena texto con el algoritmo de Rijndael
		 * 
		 * @param	txtCifrado	mensaje cifrado
		 * @return	string				texto descifrado (plano)
		 */
        public static Byte[]  FormaB2(string campo) {
            try
            {
                return Convert.FromBase64CharArray(campo.ToCharArray(),0,campo.Length);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static string FormarBase64(string campo)
        {
            try
            {
                //string strKey = ConexionAPI.secretKey.Trim().Replace(" ", "+");
                //byte[] textAsBytes = Encoding.UTF8.GetBytes(campo);
                //campo = System.Convert.ToBase64String(textAsBytes);
                if (campo.Length % 4 > 0)
                    campo = campo.PadRight(campo.Length + 4 - campo.Length % 4, '=');
                return campo;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string DescifradoTexto(String txtCifrado)
        {
            string strKey = ConexionAPI.secretKey.Trim().Replace(" ", "+");
            string strIV = ConexionAPI.IV.Trim().Replace(" ", "+");


            Key = Convert.FromBase64String(FormarBase64(strKey));
            IV = Convert.FromBase64String(FormarBase64(strIV));

            Array.Resize(ref Key, tamanyoClave);
            Array.Resize(ref IV, tamanyoVector);

            // se obtienen los bytes para el cifrado

            byte[] cipherTextBytes = Convert.FromBase64String(txtCifrado);

            // se almacenan los datos descifrados

            byte[] plainTextBytes = new byte[cipherTextBytes.Length]; 
			// se crea una instancia del Rijndael			
 
			Rijndael RijndaelAlg = Rijndael.Create();

            // se crean los datos cifrados

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // se descifran los datos

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                RijndaelAlg.CreateDecryptor(Key, IV),
                CryptoStreamMode.Read);

            // se obtienen datos descifrados

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            // se cierra todo

            memoryStream.Close();
            cryptoStream.Close();

            // se devuelve los datos descifrados

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
