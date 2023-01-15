public class PlayerModel
{
    public float Speed { get; set; } = 2f;

    public Inventory Inventory { get; }
    public Currency Currency { get; }
}