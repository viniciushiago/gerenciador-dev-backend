using FluentValidation;

namespace API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Erro de validação");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage });

                await context.Response.WriteAsJsonAsync(new
                {
                    Status = 400,
                    Message = "Erro de validação",
                    Errors = errors
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    Status = 500,
                    Message = "Ocorreu um erro interno. Tente novamente mais tarde."
                });
            }
        }
    }
}
