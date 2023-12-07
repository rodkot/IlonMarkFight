using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using DataLib.Cards;

namespace OpponentsWebApp.Dto;

public class CardDto
{
    [JsonPropertyName("Color")]
    [JsonRequired]
    public Color Color { get; set; }

    [JsonPropertyName("Number")]
    [JsonRequired]
    public int Number { get; set; }
    
    public static CardDto ToCardDto(Card card)
    {
        return new CardDto
        {
            Color = card.Color,
            Number = card.Number
        };
    }


    public Card ToCard()
    {
        return new Card(Color, Number);
    }
}