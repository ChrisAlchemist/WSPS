using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using ws_pago_de_servicios_entidades;
using ws_pago_de_servicios_entidades.Requests;
using ws_pago_de_servicios_utilidades;

namespace TestPago
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConexionAPI.ObtenerConexion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtResult.Text = string.Empty;
                RequestPagarServicios requestPagarServicios = new RequestPagarServicios() { IdProducto = 582, IdServicio = 133, Telefono = "4433740472", NumeroReferencia = "718931201466", Monto = 1243 };
                //this.textData.Text = string.IsNullOrEmpty(this.textData.Text)

                this.textData.Text = JsonConvert.SerializeObject(requestPagarServicios);
                //this.txtResult.Text= Utilerias.CMVCifrarAES256(this.textData.Text);
                string texto = Cifrado.CifradoTexto(this.textData.Text);
                this.txtResult.Text = "Texto Cifrado " + texto;

                this.txtResult.Text += "\nTexto Decifrado " + Cifrado.DescifradoTexto(texto);

            }
            catch (Exception ex)
            {
                this.txtResult.Text = "Error :" + ex.Message;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtResult.Text = string.Empty;
                this.txtResult.Text += "\nTexto Decifrado " + Cifrado.DescifradoTexto(this.textData.Text);
            }
            catch (Exception ex)
            {
                this.txtResult.Text = "Error :" + ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ResponseGeneral r = SerializerManager<ResponseGeneral>.DeseralizarStringXMLToObject(this.textData.Text);
                this.txtResult.Text = r.ResponseAbonar.MENSAJE.legend;
            }
            catch (Exception ex)
            {

                this.txtResult.Text = "Error :" + ex.Message;
            }
        }
    }
}
