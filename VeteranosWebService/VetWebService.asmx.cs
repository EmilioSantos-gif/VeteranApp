using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace VeteranosWebService
{
    /// <summary>
    /// Summary description for VetWebService
    /// </summary>
    [WebService(Namespace = "http://emilio.dev/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VetWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int Sumar(int a, int b)
        {
            return a + b;
        }

        [WebMethod] 
        public Respuesta RegistrarVeterano(VeteranoWS veterano)
        {
            string cedula, nombres, apellidos, rango, estado;
            DateTime fechaNacimiento;

            cedula = veterano.Cedula;
            nombres = veterano.Nombres;
            apellidos = veterano.Apellidos;
            rango = veterano.Rango;
            estado = veterano.Estado;
            fechaNacimiento = veterano.FechaNacimiento;

            VeteranWSDBEntities1 veteranWSDBCtxt = new VeteranWSDBEntities1();

            try
            {
                int cantRegBefore = veteranWSDBCtxt.VeteranoWS.Count();
                veteranWSDBCtxt.SPInsertVeteranoWS(cedula, nombres, apellidos, fechaNacimiento, rango, estado);
                int cantRegAfter = veteranWSDBCtxt.VeteranoWS.Count();
                
                if (cantRegAfter > cantRegBefore)
                {
                    return new Respuesta {codigo = 1, mensaje = "Grabado exitosamente en base de datos remota."};
                }
            } catch (Exception e)
            {
                return new Respuesta { codigo = -1, mensaje = $"Error: {e.Message }" };
            }

            return new Respuesta { codigo = 0, mensaje = "No se pudo grabar en la base de datos remota."};
        }
    }
}
