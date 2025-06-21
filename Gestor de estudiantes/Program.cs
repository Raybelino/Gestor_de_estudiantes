using Gestor_de_estudiantes.Interfaces;
using Gestor_de_estudiantes.Modelos;
using Gestor_de_estudiantes.Servicios;

class Program
{
    // Declaración de servicios usando interfaces
    // Esto permite flexibilidad y facilita las pruebas unitarias
    private static IEstudianteService estudianteService = new EstudianteService();
    private static IReporteService reporteService = new ReporteService();

    static void Main(string[] args)
    {
        // Título del sistema
        Console.WriteLine("=== SISTEMA DE GESTIÓN DE ESTUDIANTES ===");

        bool continuar = true;

        while (continuar)
        {
            MostrarMenu();

            string? opcion = Console.ReadLine();

            // Evaluar la opción seleccionada y ejecutar la función correspondiente
            switch (opcion)
            {
                case "1": CrearGrupo(); break;           // Crear un nuevo grupo
                case "2": AgregarEstudiante(); break;    // Agregar estudiante a un grupo
                case "3": RegistrarCalificacion(); break; // Registrar calificaciones
                case "4": MostrarListado(); break;       // Mostrar listado de estudiantes
                case "5": MostrarPorcentaje(); break;    // Mostrar porcentaje de aprobados
                case "6": MostrarGruposDisponibles(); break; // Ver todos los grupos
                case "7": continuar = false; break;      // Salir del programa
                default:
                    Console.WriteLine(" Opción inválida. Intente de nuevo.");
                    break;
            }

            // Si el usuario no eligió salir, pausar antes de continuar
            if (continuar)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();      // Esperar entrada del usuario
                Console.Clear();        // Limpiar pantalla
            }
        }

        Console.WriteLine("¡Gracias por usar el sistema!");
    }

    /// <summary>
    /// Muestra el menú principal con todas las opciones disponibles
    /// </summary>
    static void MostrarMenu()
    {
        Console.WriteLine("");
        Console.WriteLine("=== MENÚ PRINCIPAL ===");
        Console.WriteLine("1. Crear grupo");
        Console.WriteLine("2. Agregar estudiante");
        Console.WriteLine("3. Registrar calificación");
        Console.WriteLine("4. Mostrar listado de grupo");
        Console.WriteLine("5. Mostrar porcentaje de aprobados");
        Console.WriteLine("6. Ver grupos disponibles");
        Console.WriteLine("7. Salir");
        Console.WriteLine("");
        Console.Write("Seleccione una opción: ");
    }

    /// <summary>
    /// Función para crear un nuevo grupo de estudiantes
    /// </summary>
    static void CrearGrupo()
    {
        try
        {
            Console.WriteLine("\n--- CREAR GRUPO ---");

            Console.Write("Nombre del grupo: ");
            string? nombre = Console.ReadLine();
            Console.Write("Nombre de la asignatura: ");
            string? asignatura = Console.ReadLine();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(asignatura))
            {
                Console.WriteLine("El nombre y asignatura no puede estar vacío.");
                return;
            }


            var resultado = estudianteService.CrearGrupo(nombre, asignatura);

            // Mostrar el resultado de la operación
            if (resultado.Success)
                Console.WriteLine($" {resultado.Message}");
            else
                Console.WriteLine($" {resultado.Message}");
        }
        catch (Exception ex)
        {
            // Manejar errores inesperados
            Console.WriteLine($" Error inesperado al crear grupo: {ex.Message}");
        }
    }

    /// <summary>
    /// Función para agregar un estudiante a un grupo existente
    /// </summary>
    static void AgregarEstudiante()
    {

        Console.WriteLine("\n--- AGREGAR ESTUDIANTE ---");


        Console.Write("Nombre del grupo: ");
        string? grupo = Console.ReadLine();
        Console.Write("Nombre del estudiante: ");
        string? nombre = Console.ReadLine();
        Console.Write("Matrícula: ");
        string? matricula = Console.ReadLine();
        Console.Write("Tipo (presencial/distancia): ");
        string? tipo = Console.ReadLine();

        // Validar que todos los campos estén completos
        if (string.IsNullOrWhiteSpace(grupo) || string.IsNullOrWhiteSpace(nombre) ||
            string.IsNullOrWhiteSpace(matricula) || string.IsNullOrWhiteSpace(tipo))
        {
            Console.WriteLine("No pueden estar vacios nunguno de los campos.");
            return;
        }


        var resultado = estudianteService.AgregarEstudiante(grupo, nombre, matricula, tipo);

        // Mostrar el resultado de la operación
        if (resultado.Success)
            Console.WriteLine($" {resultado.Message}");
        else
            Console.WriteLine($" {resultado.Message}");
    }

    /// <summary>
    /// Función para registrar calificaciones de un estudiante
    /// </summary>
    static void RegistrarCalificacion()
    {

        double notaExamen = 0;
        double notaPractica = 0;


        Console.WriteLine("\n--- REGISTRAR CALIFICACIÓN ---");

        Console.Write("Matrícula del estudiante: ");
        string? matricula = Console.ReadLine();

        // Validar que la matrícula no esté vacía
        if (string.IsNullOrWhiteSpace(matricula))
        {
            Console.WriteLine("Matrícula no puede estar vacía.");
            return;
        }

        // Solicitar y validar nota de examen (0-40 puntos)
        Console.Write("Nota Examen (0-40): ");
        if (!double.TryParse(Console.ReadLine(), out notaExamen))
        {
            // Si no se puede convertir a número, mostrar error
            if (notaExamen > 40)
                Console.WriteLine("Nota de examen inválida");
            return;
        }

        // Solicitar y validar nota de práctica (0-60 puntos)
        Console.Write("Nota Practica (0-60): ");
        if (!double.TryParse(Console.ReadLine(), out notaPractica))
        {
            // Si no se puede convertir a número, mostrar error
            if (notaPractica > 60)
                Console.WriteLine("Nota de practica inválida");
            return;
        }

        var resultado = estudianteService.RegistrarCalificacion(matricula, notaExamen, notaPractica);

        // Mostrar el resultado de la operación
        if (resultado.Success)
            Console.WriteLine($" {resultado.Message}");
        else
            Console.WriteLine($" {resultado.Message}");
    }

    /// <summary>
    /// Función para mostrar el listado de estudiantes de un grupo
    /// </summary>
    static void MostrarListado()
    {
        Console.WriteLine("\n--- LISTADO DE GRUPO ---");

        Console.Write("Nombre del grupo: ");
        string? grupo = Console.ReadLine();

        // Validar que el nombre del grupo no esté vacío
        if (string.IsNullOrWhiteSpace(grupo))
        {
            Console.WriteLine("Nombre del grupo no puede estar vacía.");
            return;
        }

        var resultado = reporteService.MostrarListadoGrupo(grupo);

        if (!resultado.Success)
            Console.WriteLine($" {resultado.Message}");
    }

    /// <summary>
    /// Función para mostrar el porcentaje de estudiantes aprobados en un grupo
    /// </summary>
    static void MostrarPorcentaje()
    {
        Console.WriteLine("\n--- PORCENTAJE DE APROBADOS ---");

        Console.Write("Nombre del grupo: ");
        string? grupo = Console.ReadLine();

        // Validar que el nombre del grupo no esté vacío
        if (string.IsNullOrWhiteSpace(grupo))
        {
            Console.WriteLine("Nombre del grupo no puede estar vacía.");
            return;
        }

        var resultado = reporteService.CalcularPorcentajeAprobados(grupo);

        // Si hay error, mostrar mensaje
        if (!resultado.Success)
            Console.WriteLine($" {resultado.Message}");
    }

    /// <summary>
    /// Función para mostrar todos los grupos disponibles en el sistema
    /// </summary>
    static void MostrarGruposDisponibles()
    {
        Console.WriteLine("\n--- GRUPOS DISPONIBLES ---");

        var resultado = estudianteService.ObtenerGrupos();

        // Si la operación fue exitosa
        if (resultado.Success)
        {
            // Convertir los datos a lista de grupos
            var grupos = (List<Grupo>?)resultado.Data;

            // Verificar si hay grupos disponibles
            if (grupos == null || grupos.Count == 0)
            {
                Console.WriteLine("No hay grupos creados");
            }
            else
            {
                // Iterar y mostrar cada grupo con su información
                foreach (var grupo in grupos)
                {
                    Console.WriteLine($" {grupo.Nombre} - {grupo.AsignaturaNombre} ({grupo.Estudiantes.Count} estudiantes)");
                }
            }
        }
        else
        {
            // Si hubo error, mostrar mensaje
            Console.WriteLine($" {resultado.Message}");
        }
    }
}