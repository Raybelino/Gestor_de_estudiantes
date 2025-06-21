namespace Gestor_de_estudiantes.Modelos
{
    public class EstudiantePresencial : Estudiante
    {
        public override double CalcularNotaFinal() => NotaExamen + NotaPractica;
        public override string TipoEstudiante() => "Presencial";
    }

}
