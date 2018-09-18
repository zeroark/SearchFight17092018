using System;
using System.Collections.Generic;
using SearchFight.DA;

namespace SearchFight.BL
{
    public class SearchFight
    {
        private MotorGoogle motorGoogle = new MotorGoogle();
        private MotorBing motorBing = new MotorBing();
        private List<Query> querys = new List<Query>();
        private List<Resultado> resultadosGoogle = new List<Resultado>();
        private List<Resultado> resultadosBing = new List<Resultado>();
        private Resultado winnerGoogle = new Resultado();
        private Resultado winnerBing = new Resultado();
        private Resultado winnerTotal = new Resultado();

        public void Search(string consulta)
        {
            querys = GetQuerys(consulta);
            Search();
        }

        public void Search(string[] consulta)
        {
            querys = GetQuerys(consulta);
            Search();
        }
        
        private void Search()
        {
            foreach (Query q in querys)
            {
                Resultado r = new Resultado(); 
                r = motorGoogle.Search(q);
                resultadosGoogle.Add(r);
                r = motorBing.Search(q);
                resultadosBing.Add(r);
            }
            winnerGoogle = GetWinner(resultadosGoogle);
            winnerBing = GetWinner(resultadosBing);
            winnerTotal = GetWinnerTotal();
        }

        private List<Query> GetQuerys(string consulta)
        {
            List<Query> querys = new List<Query>();
            consulta = consulta.Trim();

            ValidarConsulta(consulta);

            string[] consultaArray = consulta.Split(' ');
            bool comillas = false;
            string temp = "";
            foreach (string cons in consultaArray)
            {
                if (cons.IndexOf('"') == -1) // si no tiene comillas
                {
                    if (comillas) // si ya había encontrado comillas de inicio antes
                    {
                        temp = temp + " " + cons;
                    }
                    else // si no es parte de una frase con comillas, es una palabra sola
                    {
                        Query q = new Query(cons);
                        querys.Add(q);
                    }
                }
                if (cons.IndexOf('"') == 0) // si tiene comillas al principio de la palabra
                {
                    comillas = true;
                    temp = cons; // empieza a concatenar la frase con comillas
                    if (cons.IndexOf('"', 1) == cons.Length - 1) // si también tiene las comillas al final de la palabra
                    {
                        comillas = false;
                        temp = "";
                        DA.Query q = new DA.Query(cons.Replace("\"", "")); // es una palabra sola
                        querys.Add(q);
                    }
                }
                if (cons.IndexOf('"') == cons.Length - 1) // si tiene comillas al final de la palabra 
                {
                    if (comillas) // si ya había encontrado comillas de inicio antes
                    {
                        comillas = false;
                        temp = temp + " " + cons;
                        Query q = new Query(temp.Replace("\"", ""));
                        querys.Add(q);
                        temp = "";
                    }
                    else // si no tiene comillas de inicio
                    {
                        comillas = true;
                        temp = cons.Replace("\"", "");
                    }
                }
                if (cons.IndexOf('"') > 0 && cons.IndexOf('"') < cons.Length - 1) // si tiene comillas a mitad de palabra
                {
                    if (comillas) // si ya había encontrado comillas de inicio antes
                    {
                        temp = temp + " " + cons.Replace("\"", "");
                    }
                    else
                    {
                        comillas = true;
                        temp = cons.Replace("\"", "");
                    }
                }
            }
            if (comillas)
            {
                Query q = new Query(temp.Replace("\"", ""));
                querys.Add(q);
            }

            return querys;
        }

        private List<Query> GetQuerys(string[] consulta)
        {
            List<Query> querys = new List<Query>();
            foreach (string c in consulta)
            {
                Query q = new Query(c);
                querys.Add(q);
            }
            return querys;
        }

        private void ValidarConsulta(string consulta)
        {
            if (string.IsNullOrWhiteSpace(consulta)) throw new ArgumentNullException(nameof(consulta));
        }

        private Resultado GetWinner(List<Resultado> resultados)
        {
            Resultado ganador = new Resultado();
            long mayor = 0;
            foreach(Resultado r in resultados)
            {
                if(r.CantidadResultados > mayor)
                {
                    mayor = r.CantidadResultados;
                    ganador = r;
                }
            }

            return ganador;
        }
        
        private Resultado GetWinnerTotal()
        {
            Resultado ganador = new Resultado();
            ganador.MotorBusqueda = new MotorBusqueda();
            ganador.MotorBusqueda.Nombre = "Total";
            long suma = 0;
            long mayor = 0;
            foreach (Query q in querys)
            {
                suma = 0;
                foreach (Resultado r in resultadosGoogle)
                    if(r.query.Texto.Equals(q.Texto))
                    {
                        suma += r.CantidadResultados;
                        break;
                    }
                foreach (Resultado r in resultadosBing)
                    if (r.query.Texto.Equals(q.Texto))
                    {
                        suma += r.CantidadResultados;
                        break;
                    }
                if(suma > mayor)
                {
                    mayor = suma;
                    ganador.query = q;
                }
            }
            return ganador;
        }

        private string PrintWinner(Resultado res)
        {
            return res.MotorBusqueda.Nombre + " winner: " + res.query.Texto;
        }

        public string PrintWinnerGoogle()
        {
            return PrintWinner(winnerGoogle);
        }

        public string PrintWinnerBing()
        {
            return PrintWinner(winnerBing);
        }

        public string PrintWinnerTotal()
        {
            return PrintWinner(winnerTotal);
        }

        private string GetResult(Query query, MotorBusqueda motor, List<Resultado> resultados)
        {
            string result = motor.Nombre + ": ";
            foreach(Resultado r in resultados)
            {
                if(r.query.Texto.Equals(query.Texto) && r.MotorBusqueda.Nombre.Equals(motor.Nombre))
                {
                    result = result + r.CantidadResultados;
                }
            }
            return result;
        }

        public List<string> PrintResults()
        {
            List<string> result = new List<string>();
            string r = "";
            foreach(Query q in querys)
            {
                r = q.Texto + ": " + GetResult(q, motorGoogle.motor, resultadosGoogle) + " " + GetResult(q, motorBing.motor, resultadosBing);
                result.Add(r);
            }
            return result;
        }
    }
}
