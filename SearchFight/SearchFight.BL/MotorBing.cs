using System;
using System.Net;
using System.Web.Script.Serialization;
using SearchFight.DA;

namespace SearchFight.BL
{
    public class MotorBing
    {
        public DA.MotorBing motor = new DA.MotorBing();

        public Resultado Search(Query query)
        {
            ValidarQuery(query);

            RespuestaBing respuesta = new RespuestaBing();
            Resultado resultado = new Resultado();

            string url = string.Format(motor.Url, query.Texto);
            
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add(motor.HeaderNameAPIkey, motor.APIKey);
                string resultJSON = wc.DownloadString(url);
                resultJSON = resultJSON.Replace("\n", "");

                JavaScriptSerializer jss = new JavaScriptSerializer();
                respuesta = jss.Deserialize<RespuestaBing>(resultJSON);
                
                resultado.MotorBusqueda = motor;
                resultado.query = query;
                resultado.CantidadResultados = long.Parse(respuesta.WebPages.TotalEstimatedMatches);
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
