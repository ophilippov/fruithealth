using System.Net.Mime;
using FluentValidation;
using FruitHealth.Abstractions.Exceptions;
using FruitHealth.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FruitHealth.Api.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
  private readonly ProblemDetailsFactory _problemDetailsFactory;

  public ExceptionHandlerMiddleware(ProblemDetailsFactory problemDetailsFactory)
  {
    _problemDetailsFactory = problemDetailsFactory ?? throw new ArgumentNullException(nameof(problemDetailsFactory));
  }
  
  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(context, ex);
    }
  }

  private Task HandleExceptionAsync(HttpContext context, Exception ex)
  {
    switch (ex)
    {
      case ValidationException validationException:
        // TODO: Log the exception (not implemented)
        var modelState = new ModelStateDictionary();
        foreach (var error in validationException.Errors)
        {
          modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
        var validationProblemDetails = _problemDetailsFactory.CreateValidationProblemDetails(
          context, 
          modelState, 
          statusCode: StatusCodes.Status400BadRequest,
          instance: context.Request.Path,
          title: "Request Validation Failed"
        );
        return WriteProblemAsync(context, validationProblemDetails);

      case InvalidModelException invalidModelException:
        // TODO: Log the exception (not implemented)
        var invalidModelProblemDetails = _problemDetailsFactory.CreateProblemDetails(
          context, 
          statusCode: StatusCodes.Status422UnprocessableEntity, 
          title: "The model is invalid, further processing is not possible.",
          detail: $"The request contained an invalid model or resulted in an invalid state. Please try again later.",
          instance: context.Request.Path
        );
        return WriteProblemAsync(context, invalidModelProblemDetails);

      case ExternalResourceUnavailableException:
        // TODO: Log the exception (not implemented)
        var externalResourceProblemDetails = _problemDetailsFactory.CreateProblemDetails(
          context,
          statusCode: StatusCodes.Status502BadGateway,
          title: "External Resource Unavailable",
          detail: "Something went wrong when accessing the external resource. Please try again later.",
          instance: context.Request.Path
        );
        return WriteProblemAsync(context, externalResourceProblemDetails);

      case ExternalResourceNotFoundException:
        // TODO: Log the exception (not implemented)
        var notFoundProblemDetails = _problemDetailsFactory.CreateProblemDetails(
          context, 
          statusCode: StatusCodes.Status404NotFound, 
          title: "External Resource Not Found",
          detail: "Something went wrong when accessing the external resource. The resource was not found. Please try again later.",
          instance: context.Request.Path
        );
        return WriteProblemAsync(context, notFoundProblemDetails);  

      default:
        // TODO: Log the exception (not implemented)
        var problemDetails = _problemDetailsFactory.CreateProblemDetails(
          context, 
          statusCode: StatusCodes.Status500InternalServerError, 
          title: "An unexpected error occurred", 
          detail: "Something went wrong. Please contact support if the issue persists.",
          instance: context.Request.Path
        );
        return WriteProblemAsync(context, problemDetails);
    }
  }

  private static async Task WriteProblemAsync<TProblemDetails>(HttpContext context, TProblemDetails problemDetails) 
    where TProblemDetails : ProblemDetails
  {
    context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
    context.Response.ContentType = MediaTypeNames.Application.ProblemJson;
    await context.Response.WriteAsJsonAsync(problemDetails);
  }
}
