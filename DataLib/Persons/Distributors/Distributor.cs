using DataLib.Person.God;
using DataLib.Persons.Opponents;

namespace DataLib.Persons.Distributors;

public abstract class Distributor : Person, IManagingDestinies
{
    protected Distributor(string name) : base(name)
    {
    }

    public void Punish(Person person)
    {
        person.State = StateLive.Dead;
    }

    public void Spare(Person person)
    {
        person.State = StateLive.Alive;
    }
    

    public bool Judge(in Opponent firstOpponent, in Opponent secondOpponent)
    {
        return firstOpponent.ChooseCard.Color == secondOpponent.ChooseCard.Color;
    }
}