namespace WebApp.Models.Cars
{
    public class IndexCarAllViewModel 
    {
        public string Id { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }

        public string PlateNumber { get; set; }

        public int Year { get; set; }

        public string PictureUrl { get; set; }

        public int FinishedRepairs { get; set; }

        public int AllCars { get; set; }

        public int AllClients { get; set; }
        
    }
}
