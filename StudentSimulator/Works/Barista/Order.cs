namespace StudentSimulator.Works.Barista
{
    public record Order
    {
        public int Id {get; set;}
        public int Duration {get; set;}
        public int Clients {get; set;}
        public int CafeLevel {get; set;}
    }
}