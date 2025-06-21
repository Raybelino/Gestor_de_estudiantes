namespace Gestor_de_estudiantes.Modelos
{
    public class EstudianteDistancia : Estudiante
    {
        public override double CalcularNotaFinal() => NotaExamen + NotaPractica;
        public override string TipoEstudiante() => "Distancia";
    }
}
