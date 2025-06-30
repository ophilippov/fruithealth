using FruitHealth.Fruits.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FruitHealth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FruitsController : ControllerBase
{
    private readonly ILogger<FruitsController> _logger;
    private readonly IMediator _mediator;

    public FruitsController(ILogger<FruitsController> logger, IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [Route("healthiest")]
    [HttpGet(Name = "GetHealthiestFruits")]
    public async Task<IActionResult> GetHealthiestFruits(
        [FromQuery] double? sugarMin,
        [FromQuery] double? sugarMax,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetFruitsQuery
        {
            SugarMin = sugarMin,
            SugarMax = sugarMax
        }, cancellationToken);
        
        return Ok(result);
    }

}
