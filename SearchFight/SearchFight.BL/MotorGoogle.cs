using System;
using System.Net;
using System.Web.Script.Serialization;
using SearchFight.DA;

namespace SearchFight.BL
{
    public class MotorGoogle
    {
        public DA.MotorGoogle motor = new DA.MotorGoogle();

        public Resultado Search(Query query)
        {
            ValidarQuery(query);

            RespuestaGoogle respuesta = new RespuestaGoogle();
            Resultado resultado = new Resultado();
            string url;

            if (query.Texto.Split(' ').Length == 1) // una palabra
            {
                url = string.Format(motor.Url, motor.APIKey, motor.CEKey, query.Texto);
            }
            else // dos o más palabras
            {
                url = string.Format(motor.UrlFraseExacta, motor.APIKey, motor.CEKey, query.Texto);
            }

            using (WebClient wc = new WebClient())
            {
                string resultadoJSON = wc.DownloadString(url);
                resultadoJSON = resultadoJSON.Replace("\n", "");

                JavaScriptSerializer jss = new JavaScriptSerializer();
                respuesta = jss.Deserialize<RespuestaGoogle>(resultadoJSON);
                
                resultado.MotorBusqueda = motor;
                resultado.query = query;
                resultado.CantidadResultados = long.Parse(respuesta.SearchInformation.TotalResults);
            }

            return resultado;
        }

        private void ValidarQuery(Query q)
        {
            if (q == null) throw new ArgumentNullException(nameof(q));
            if (string.IsNullOrWhiteSpace(q.Texto)) throw new ArgumentNullException(nameof(q.Texto));
        }
    }
}
