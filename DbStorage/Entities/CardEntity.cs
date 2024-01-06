using DataLib.Cards;

namespace DbStorage.Entities;

public class CardEntity
{
    public int Id { get; set; }
    public Color Color { get; set; }
    public int Number { get; set; }

    public CardEntity(Color color, int number)
    {
        Color = color;
        Number = number;
    }
    
    public CardEntity(Card card)
    {
        Color = card.Color;
        Number = card.Number;
    }

    public static CardEntity FromCard(Card card)
    {
        return new CardEntity(card);
    }

    public Card ToCard()
    {
        return new Card(Color, Number);
    }
}