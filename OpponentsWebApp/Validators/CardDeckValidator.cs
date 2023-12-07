using DataLib.Cards;
using OpponentsWebApp.Dto;
using OpponentsWebApp.Exceptions;

namespace OpponentsWebApp.Validators;

public class CardDeckValidator
{
    public static int CardsCount { get; } = 18;
    
    public static IList<Card> ValidateAndReturn(IList<CardDto> dtos)
    {
        var desk = dtos.Select(dto => dto.ToCard()).ToList();
        if (desk.Count != CardsCount)
        {
            throw new BadDeckLength();
        }

        return desk;
    }
}