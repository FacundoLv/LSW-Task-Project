public class Currency
{
    public int Amount { get; private set; }

    public void Add(int amount)
    {
        Amount += amount;
    }

    public void Spend(int amount)
    {
        if (CanSpend(amount)) Amount -= amount;
    }

    public bool CanSpend(int amount) => Amount >= amount;
}