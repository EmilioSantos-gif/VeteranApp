using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuerraIraqApp
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            GuerraIraqAppDBEntities appDBCtxt = new GuerraIraqAppDBEntities();

            string cedula, nombres, apellidos, rango, estado;
            DateTime fechaNacimiento;

            cedula = textCedula.Text;
            nombres = txtNombres.Text;
            apellidos = txtApellidos.Text;
            rango = txtRango.Text;
            estado = txtEstado.Text;
            fechaNacimiento = dtpFechaNacimiento.Value;

            try
            {
                int cantRegBefore= appDBCtxt.Veterano.Count();

                appDBCtxt.SPInsertVeterano(cedula, nombres, apellidos, fechaNacimiento, rango, estado);
                
                int cantRegAfter = appDBCtxt.Veterano.Count();

                if (cantRegAfter > cantRegBefore)
                {
                    MessageBox.Show("Registro guardado existosamente.");

                    VetWS.VetWebServiceSoapClient vetWSClient = new VetWS.VetWebServiceSoapClient();

                    VetWS.VeteranoWS veterano = new VetWS.VeteranoWS
                    {
                        Cedula = cedula,
                        Nombres = nombres,
                        Apellidos = apellidos,
                        Rango = rango,
                        Estado = estado,
                        FechaNacimiento = fechaNacimiento
                    };

                    VetWS.Respuesta WSRespuesta= new VetWS.Respuesta();

                    WSRespuesta = vetWSClient.RegistrarVeterano(veterano);

                    MessageBox.Show($"Code: {WSRespuesta.codigo}\nResponse: {WSRespuesta.mensaje}");
                } else
                {
                    MessageBox.Show("No se pudo grabar el registro.");
                }

                //TODO: Hacer un clear luego de guardar

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
