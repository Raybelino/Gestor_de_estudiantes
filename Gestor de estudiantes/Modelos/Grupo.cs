namespace Gestor_de_estudiantes.Modelos
{
    public class Grupo
    {
        public string? Nombre { get; set; }
        public string? AsignaturaNombre { get; set; }
        public List<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
    }

}
