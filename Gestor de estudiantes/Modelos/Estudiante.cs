namespace Gestor_de_estudiantes.Modelos
{
    public abstract class Estudiante
    {
        public string? Matricula { get; set; }
        public string? Nombre { get; set; }
        public double NotaExamen { get; set; }
        public double NotaPractica { get; set; }

        public abstract double CalcularNotaFinal();
        public abstract string TipoEstudiante();
    }
}
