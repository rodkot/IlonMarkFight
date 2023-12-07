using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using DataLib;
using DataLib.Cards;
using DataLib.Exceptions;
using Microsoft.Extensions.Logging;

namespace Core.Client;

public class HttpOpponentAsker : IHttpAskerOpponent
{
    private readonly Uri _uri;
 

    public HttpOpponentAsker(Uri uri)
    {
        _uri = uri;
    }


    private static IEnumerable<CardDto> ConvertCards(IEnumerable<Card> cards)
    {
        return cards.Select(x => new CardDto(x));
    }

    public async Task<Card> Ask(IEnumerable<Card> cards)
    {
        var httpClient = new HttpClient();
        var postContent = JsonContent.Create(ConvertCards(cards));
        var response = await httpClient.PostAsync(_uri, postContent);
        var stream = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<CardDto>(stream) ??
                     throw new InvalidOperationException();
        switch (response.StatusCode)
        {
            case HttpStatusCode.OK:
            {
                return result.ToCard();
            }
            case HttpStatusCode.BadRequest:
                throw new BadRequestException($"incorrect request, errors: {string.Join(", ", result.Errors)}");
            case HttpStatusCode.InternalServerError:
                throw new ServerErrorException("server error");
            default:
                throw new UnexpectedHttpStatusCodeException($"unexpected status code {response.StatusCode}");
        }
    }
}

// public class CardChoiceDto
// {
//     [JsonPropertyName("name")]
//     [JsonRequired]
//     public string Name { get; set; }
//
//     [JsonPropertyName("cardNumber")]
//     [JsonRequired]
//     public int CardNumber { get; set; }
//
//     [JsonPropertyName("errors")] public IEnumerable<string> Errors { get; set; }
// }

public class CardDto
{
    [JsonPropertyName("Color")]
    [JsonRequired]
    public Color Color { get; set; }

    [JsonPropertyName("Number")]
    [JsonRequired]
    public int Number { get; set; }


    [JsonPropertyName("errors")] public IEnumerable<string> Errors { get; set; }

    public CardDto(Card card)
    {
        Color = card.Color;
        Number = card.Number;
    }

    public Card ToCard()
    {
        return new Card(Color, Number);
    }
}