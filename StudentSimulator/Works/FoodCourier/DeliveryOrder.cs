namespace StudentSimulator.Works.FoodCourier
{
    public record DeliveryOrder
    {
        public int Id {get; set;}
        public int Distance {get; set;}
        public string ClientName {get; set;}
        public List<string> Items {get; set;}
    }
}