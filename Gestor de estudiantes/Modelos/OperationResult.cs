namespace Gestor_de_estudiantes.Modelos
{
    public class OperationResult
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public object? Data { get; set; }

        public static OperationResult SuccessResult(object? data = null, string? message = "OK") 
            => new OperationResult { Success = true, Data = data, Message = message };

        public static OperationResult Failure(string? message)
            => new OperationResult { Success = false, Message = message };
    }
}
