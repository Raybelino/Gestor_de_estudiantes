using Gestor_de_estudiantes.Data;
using Gestor_de_estudiantes.Interfaces;
using Gestor_de_estudiantes.Modelos;

namespace Gestor_de_estudiantes.Servicios
{
    public class GrupoService : IGrupoService
    {
        public OperationResult ListarCalificaciones(Grupo grupo)
        {
            var listado = grupo.Estudiantes.Select(e => new
            {
                e.Matricula,
                e.Nombre,
                Tipo = e.TipoEstudiante(),
                NotaFinal = e.CalcularNotaFinal()
            }).ToList();

            return OperationResult.SuccessResult(listado, "Listado generado");
        }

        public OperationResult CalcularPorcentajeAprobados(string nombreGrupo)
        {
            try
            {
                var grupo = DataStorage.BuscarGrupo(nombreGrupo);
                if (grupo == null)
                    return OperationResult.Failure("Grupo no encontrado");

                if (grupo.Estudiantes.Count == 0)
                    return OperationResult.Failure("El grupo no tiene estudiantes");

                int aprobados = grupo.Estudiantes.Count(e => e.CalcularNotaFinal() > 70 ? true : false);
                double porcentaje = (aprobados * 100.0) / grupo.Estudiantes.Count;

                Console.WriteLine($"\nESTADÍSTICAS DEL GRUPO {grupo.Nombre}:");
                Console.WriteLine($"Total estudiantes: {grupo.Estudiantes.Count}");
                Console.WriteLine($"Aprobados: {aprobados}");
                Console.WriteLine($"Reprobados: {grupo.Estudiantes.Count - aprobados}");
                Console.WriteLine($"Porcentaje de aprobación: {porcentaje:F1}%");

                return OperationResult.SuccessResult(porcentaje, "Estadísticas calculadas");
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error al calcular porcentaje: {ex.Message}");
            }
        }
    }
}
