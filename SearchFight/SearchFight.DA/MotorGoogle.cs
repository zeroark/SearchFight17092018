using System.Configuration;

namespace SearchFight.DA
{
    public class MotorGoogle : MotorBusqueda
    {
        public MotorGoogle()
        {
            Nombre = "Google";
            Url = ConfigurationManager.AppSettings["URLGoogle"];
            UrlFraseExacta = ConfigurationManager.AppSettings["URLGoogleExactTerms"];
            APIKey = ConfigurationManager.AppSettings["APIKeyGoogle"];
            CEKey = ConfigurationManager.AppSettings["CEKeyGoogle"];
        }
    }
}
