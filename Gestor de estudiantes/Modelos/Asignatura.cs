namespace Gestor_de_estudiantes.Modelos
{
    public class Asignatura
    {
        public string? NombreAsignatura { get; set; }
        public List<Grupo> Grupos { get; set; } = new List<Grupo>();
    }
}
