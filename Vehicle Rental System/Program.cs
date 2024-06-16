namespace Vehicle_Rental_System
{
    using Vehicle_Rental_System.Classes;

    public class Program
    {
        static void Main()
        {
            // Example Car rental
            Car car = new Car
            {
                Brand = "Mitsubishi",
                Model = "Mirage",
                Value = 15000,
                SafetyRating = 3
            };
            Invoice carInvoice = new Invoice(
                car, 10, 10, new DateTime(2024, 6, 3), new DateTime(2024, 6, 13), "John Doe");
            carInvoice.GenerateInvoice();

            Console.WriteLine();

            // Example Motorcycle rental
            Motorcycle motorcycle = new Motorcycle
            {
                Brand = "Triumph",
                Model = "Tiger Sport 660",
                Value = 10000,
                RiderAge = 20
            };
            Invoice motorcycleInvoice = new Invoice(
                motorcycle, 10, 10, new DateTime(2024, 6, 3), new DateTime(2024, 6, 13), "Mary Johnson");
            motorcycleInvoice.GenerateInvoice();

            Console.WriteLine();

            // Example CargoVan rental
            CargoVan cargoVan = new CargoVan
            {
                Brand = "Citroen",
                Model = "Jumper",
                Value = 20000,
                DriverExperience = 8
            };
            Invoice cargoVanInvoice = new Invoice(
                cargoVan, 15, 10, new DateTime(2024, 6, 3), new DateTime(2024, 6, 13), "John Markson");
            cargoVanInvoice.GenerateInvoice();
        }
    }
}