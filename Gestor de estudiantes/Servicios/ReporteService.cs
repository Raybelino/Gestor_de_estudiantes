using Gestor_de_estudiantes.Data;
using Gestor_de_estudiantes.Interfaces;
using Gestor_de_estudiantes.Modelos;

namespace Gestor_de_estudiantes.Servicios
{
    public class ReporteService : IReporteService
    {
        public OperationResult MostrarListadoGrupo(string nombreGrupo)
        {
            try
            {
                var grupo = DataStorage.BuscarGrupo(nombreGrupo);
                if (grupo == null)
                    return OperationResult.Failure("Grupo no encontrado");

                Console.WriteLine($"\n=== GRUPO: {grupo.Nombre} - {grupo.AsignaturaNombre} ===");
                Console.WriteLine("Matrícula\tNombre\t\tTipo\t\tEstado");
                Console.WriteLine("".PadRight(70, '-'));

                foreach (var estudiante in grupo.Estudiantes)
                {
                    Console.WriteLine($"{estudiante.Matricula} \t{estudiante.Nombre} \t\t{estudiante.TipoEstudiante()} \t{(estudiante.CalcularNotaFinal() > 70 ? "APROBADO" : "DESAPROBADO")}");
                }

                return OperationResult.SuccessResult(grupo, "Listado mostrado correctamente");
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error al mostrar listado: {ex.Message}");
            }
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

        public OperationResult ObtenerEstadisticas(string nombreGrupo)
        {
            try
            {
                var grupo = DataStorage.BuscarGrupo(nombreGrupo);
                if (grupo == null)
                    return OperationResult.Failure("Grupo no encontrado");

                var stats = new
                {
                    TotalEstudiantes = grupo.Estudiantes.Count,
                    Aprobados = grupo.Estudiantes.Count(e => e.CalcularNotaFinal() > 70 ? true : false),
                    Reprobados = grupo.Estudiantes.Count(e => e.CalcularNotaFinal() < 70 ? true : false),
                };

                return OperationResult.SuccessResult(stats, "Estadísticas obtenidas");
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error al obtener estadísticas: {ex.Message}");
            }
        }
    }
}
