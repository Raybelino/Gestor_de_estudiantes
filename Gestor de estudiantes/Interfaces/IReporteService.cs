using Gestor_de_estudiantes.Modelos;

namespace Gestor_de_estudiantes.Interfaces
{
    public interface IReporteService
    {
        OperationResult MostrarListadoGrupo(string nombreGrupo);
        OperationResult CalcularPorcentajeAprobados(string nombreGrupo);
        OperationResult ObtenerEstadisticas(string nombreGrupo);
    }
}
