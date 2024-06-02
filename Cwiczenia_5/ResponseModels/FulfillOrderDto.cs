namespace Cwiczenia_5.RequestModels;

public class FulfillOrderDto
{
    public int IdProduct { set; get; }
    public int IdWarehouse { set; get; }
    public int Amount { set; get; }
    public DateTime CreatedAt { set; get; }
}