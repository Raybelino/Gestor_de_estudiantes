using Gestor_de_estudiantes.Modelos;

namespace Gestor_de_estudiantes.Interfaces
{
    public interface IEstudianteService
    {
        OperationResult CrearGrupo(string nombre, string asignatura);
        OperationResult AgregarEstudiante(string nombreGrupo, string nombre, string matricula, string tipo);
        OperationResult RegistrarCalificacion(string matricula, double notaExamen, double notaPractica);
        OperationResult ObtenerGrupos();
        OperationResult ObtenerEstudiantes(string nombreGrupo);
    }
}
