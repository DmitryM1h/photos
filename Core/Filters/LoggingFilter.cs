using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Filters;

public class LoggingFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Logging Filter OnActionExecuting");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("Logging Filter OnActionExecuted");
    }
}