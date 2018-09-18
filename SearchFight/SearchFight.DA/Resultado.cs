namespace SearchFight.DA
{
    public class Resultado
    {
        public Query query { get; set; }
        public MotorBusqueda MotorBusqueda { get; set; }
        public long CantidadResultados { get; set; }
    }
}
