using Gestor_de_estudiantes.Modelos;

namespace Gestor_de_estudiantes.Interfaces
{
    public interface IGrupoService
    {
        OperationResult ListarCalificaciones(Grupo grupo);
        OperationResult CalcularPorcentajeAprobados(string nombreGrupo);
    }
}
