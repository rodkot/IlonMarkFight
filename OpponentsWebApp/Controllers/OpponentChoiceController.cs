using DataLib.Opponents;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OpponentsWebApp.Dto;
using OpponentsWebApp.Validators;

namespace OpponentsWebApp.Controllers;

[ApiController]
[Route("opponent")]
public class OpponentChoiceController
{
    private readonly Opponent _opponent;

    public OpponentChoiceController(Opponent opponent)
    {
        _opponent = opponent;
    }

    [ProducesResponseType(typeof(CardDto), 200)]
    [ProducesResponseType(typeof(ErrorDto), 400)]
    [ProducesResponseType(typeof(ErrorDto), 500)]
    [HttpPost(Name = "choose")]
    public IResult Choose([FromBody] IList<CardDto> dtos)
    {
        var dropDesk = CardDeckValidator.ValidateAndReturn(dtos);

        return Results.Ok(CardDto.ToCardDto(_opponent.Choose(dropDesk)));
    }
}