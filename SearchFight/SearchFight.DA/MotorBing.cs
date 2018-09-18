using System.Configuration;

namespace SearchFight.DA
{
    public class MotorBing : MotorBusqueda
    {
        public MotorBing()
        {
            Nombre = "Bing";
            Url = ConfigurationManager.AppSettings["URLBing"];
            APIKey = ConfigurationManager.AppSettings["APIKeyBing"];
            HeaderNameAPIkey = ConfigurationManager.AppSettings["HeaderNameAPIKeyBing"];
        }
    }
}
