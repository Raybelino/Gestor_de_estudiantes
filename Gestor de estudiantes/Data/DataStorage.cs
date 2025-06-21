using Gestor_de_estudiantes.Modelos;

namespace Gestor_de_estudiantes.Data
{
    public static class DataStorage
    {
        public static List<Grupo> Grupos = new List<Grupo>();
        public static List<Estudiante> Estudiantes = new List<Estudiante>();
        public static Grupo? BuscarGrupo(string nombre)
        {
            return Grupos.FirstOrDefault(g => g.Nombre != null && g.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        public static Estudiante? BuscarEstudiante(string matricula)
        {
            return Estudiantes.FirstOrDefault(e => e.Matricula != null && e.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
        }
    }
}
