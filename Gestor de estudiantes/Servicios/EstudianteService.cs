using Gestor_de_estudiantes.Data;
using Gestor_de_estudiantes.Interfaces;
using Gestor_de_estudiantes.Modelos;

namespace Gestor_de_estudiantes.Servicios
{
    public class EstudianteService : IEstudianteService
    {
        public OperationResult CrearGrupo(string? nombre, string? asignatura)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(asignatura))
                    return OperationResult.Failure("Nombre y asignatura son obligatorios");

                if (DataStorage.BuscarGrupo(nombre) != null)
                    return OperationResult.Failure("El grupo ya existe");

                var grupo = new Grupo
                {
                    Nombre = nombre,
                    AsignaturaNombre = asignatura
                };

                DataStorage.Grupos.Add(grupo);
                return OperationResult.SuccessResult(grupo, $"Grupo '{nombre}' creado exitosamente");
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error al crear grupo: {ex.Message}");
            }
        }
        public OperationResult AgregarEstudiante(string? nombreGrupo, string? nombre, string? matricula, string? tipo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreGrupo) || string.IsNullOrWhiteSpace(nombre) ||
                    string.IsNullOrWhiteSpace(matricula) || string.IsNullOrWhiteSpace(tipo))
                    return OperationResult.Failure("Todos los campos son obligatorios");

                var grupo = DataStorage.BuscarGrupo(nombreGrupo);
                if (grupo == null)
                    return OperationResult.Failure("Grupo no encontrado");

                if (DataStorage.BuscarEstudiante(matricula) != null)
                    return OperationResult.Failure("Ya existe un estudiante con esa matrícula");

                Estudiante estudiante = tipo.ToLower() == "presencial"
                    ? new EstudiantePresencial()
                    : new EstudianteDistancia();

                estudiante.Nombre = nombre;
                estudiante.Matricula = matricula;

                grupo.Estudiantes.Add(estudiante);
                DataStorage.Estudiantes.Add(estudiante);

                return OperationResult.SuccessResult(estudiante, $"Estudiante '{nombre}' agregado al grupo '{nombreGrupo}'");
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error al agregar estudiante: {ex.Message}");
            }
        }

        public OperationResult RegistrarCalificacion(string matricula, double notaExamen, double notaPractica)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(matricula))
                    return OperationResult.Failure("Matrícula es obligatoria");

                if (notaExamen < 0 || notaExamen > 40)
                    return OperationResult.Failure("La nota del examen tiene que ser entre 0 a 40");
                if (notaPractica < 0 || notaPractica > 60)
                    return OperationResult.Failure("La nota de practica tiene que ser entre 0 a 60");

                var estudiante = DataStorage.BuscarEstudiante(matricula);
                if (estudiante == null)
                    return OperationResult.Failure("Estudiante no encontrado");

                estudiante.NotaExamen = notaExamen;
                estudiante.NotaPractica = notaPractica;
                return OperationResult.SuccessResult(null, $"Calificación: Nota de examen: {notaExamen} y nota de practica: {notaPractica} registrada para {estudiante.Nombre}");
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error al registrar calificación: {ex.Message}");
            }
        }

        public OperationResult ObtenerGrupos()
        {
            try
            {
                return OperationResult.SuccessResult(DataStorage.Grupos, "Grupos obtenidos");
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error al obtener grupos: {ex.Message}");
            }
        }

        public OperationResult ObtenerEstudiantes(string nombreGrupo)
        {
            try
            {
                var grupo = DataStorage.BuscarGrupo(nombreGrupo);
                if (grupo == null)
                    return OperationResult.Failure("Grupo no encontrado");

                return OperationResult.SuccessResult(grupo.Estudiantes, "Estudiantes obtenidos");
            }
            catch (Exception ex)
            {
                return OperationResult.Failure($"Error al obtener estudiantes: {ex.Message}");
            }
        }
    }
}

