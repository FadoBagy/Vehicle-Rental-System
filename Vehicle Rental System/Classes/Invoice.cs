namespace Vehicle_Rental_System.Classes
{
    using System.Text;

    public class Invoice
    {
        public Vehicle Vehicle { get; set; }
        public int RentalPeriod { get; set; } // in days
        public int ActualRentalPeriod { get; set; } // in days
        public DateTime StartDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CustomerName { get; set; }

        public Invoice(
            Vehicle vehicle, 
            int rentalPeriod, 
            int actualRentalPeriod, 
            DateTime startDate, 
            DateTime returnDate, 
            string customerName)
        {
            Vehicle = vehicle;
            RentalPeriod = rentalPeriod;
            ActualRentalPeriod = actualRentalPeriod;
            StartDate = startDate;
            ReturnDate = returnDate;
            CustomerName = customerName;
        }

        public void GenerateInvoice()
        {
            decimal rentalCost = CalculateRentalCost(out decimal dailyRentalCost);
            decimal insuranceCost = CalculateInsuranceCost(
                out decimal dailyInsuranceCost, 
                out decimal initialDailyInsuranceCost, 
                out decimal insuranceAdjustment);
            decimal totalCost = rentalCost + insuranceCost;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("XXXXXXXXXX");
            sb.AppendLine($"Date: {DateTime.Now:yyyy-MM-dd}");
            sb.AppendLine($"Customer Name: {CustomerName}");
            sb.AppendLine($"Rented Vehicle: {Vehicle.Brand} {Vehicle.Model}");
            sb.AppendLine();
            sb.AppendLine($"Reservation start date: {StartDate:yyyy-MM-dd}");
            sb.AppendLine($"Reservation end date: {StartDate.AddDays(RentalPeriod):yyyy-MM-dd}");
            sb.AppendLine($"Reserved rental days: {RentalPeriod} days");
            sb.AppendLine();
            sb.AppendLine($"Actual return date: {ReturnDate:yyyy-MM-dd}");
            sb.AppendLine($"Actual rental days: {ActualRentalPeriod} days");
            sb.AppendLine();
            sb.AppendLine($"Rental cost per day: ${dailyRentalCost:F2}");
            if (Vehicle is Car)
            {
                sb.AppendLine($"Insurance per day: ${dailyInsuranceCost:F2}");
            }
            else if (Vehicle is Motorcycle)
            {
                sb.AppendLine($"Initial insurance per day: ${initialDailyInsuranceCost:F2}");
                sb.AppendLine($"Insurance addition per day: ${insuranceAdjustment:F2}");
                sb.AppendLine($"Insurance per day: ${dailyInsuranceCost:F2}");
            }
            else if (Vehicle is CargoVan)
            {
                sb.AppendLine($"Initial insurance per day: ${initialDailyInsuranceCost:F2}");
                sb.AppendLine($"Insurance discount per day: ${insuranceAdjustment:F2}");
                sb.AppendLine($"Insurance per day: ${dailyInsuranceCost:F2}");
            }
            sb.AppendLine();
            if (ActualRentalPeriod < RentalPeriod)
            {
                decimal earlyReturnDiscount = ((RentalPeriod - ActualRentalPeriod) * dailyRentalCost / 2);
                sb.AppendLine($"Early return discount for rent: ${earlyReturnDiscount:F2}");
                decimal earlyInsuranceDiscount = ((RentalPeriod - ActualRentalPeriod) * dailyInsuranceCost);
                sb.AppendLine($"Early return discount for insurance: ${earlyInsuranceDiscount:F2}");
            }
            sb.AppendLine($"Total rent: ${rentalCost:F2}");
            sb.AppendLine($"Total insurance: ${insuranceCost:F2}");
            sb.AppendLine($"Total: ${totalCost:F2}");
            sb.AppendLine("XXXXXXXXXX");

            Console.WriteLine(sb.ToString());
        }

        private decimal CalculateRentalCost(out decimal dailyRentalCost)
        {
            dailyRentalCost = 0;
            if (Vehicle is Car)
            {
                dailyRentalCost = RentalPeriod <= 7 ? 20 : 15;
            }
            else if (Vehicle is Motorcycle)
            {
                dailyRentalCost = RentalPeriod <= 7 ? 15 : 10;
            }
            else if (Vehicle is CargoVan)
            {
                dailyRentalCost = RentalPeriod <= 7 ? 50 : 40;
            }

            decimal rentalCost = dailyRentalCost * ActualRentalPeriod;
            if (ActualRentalPeriod < RentalPeriod)
            {
                rentalCost += (RentalPeriod - ActualRentalPeriod) * (dailyRentalCost / 2);
            }

            return rentalCost;
        }

        private decimal CalculateInsuranceCost(
            out decimal dailyInsuranceCost, 
            out decimal initialDailyInsuranceCost, 
            out decimal insuranceAdjustment)
        {
            dailyInsuranceCost = 0;
            initialDailyInsuranceCost = 0;
            insuranceAdjustment = 0;

            if (Vehicle is Car car)
            {
                initialDailyInsuranceCost = 0.0001m * car.Value; // 0.01% of car value
                if (car.SafetyRating >= 4)
                {
                    dailyInsuranceCost = initialDailyInsuranceCost * 0.9m; // 10% discount for high safety rating
                }
                else
                {
                    dailyInsuranceCost = initialDailyInsuranceCost;
                }
            }
            else if (Vehicle is Motorcycle motorcycle)
            {
                initialDailyInsuranceCost = 0.0002m * motorcycle.Value; // 0.02% of motorcycle value
                if (motorcycle.RiderAge < 25)
                {
                    insuranceAdjustment = initialDailyInsuranceCost * 0.2m; // 20% increase for riders under 25
                    dailyInsuranceCost = initialDailyInsuranceCost + insuranceAdjustment;
                }
                else
                {
                    dailyInsuranceCost = initialDailyInsuranceCost;
                }
            }
            else if (Vehicle is CargoVan cargoVan)
            {
                initialDailyInsuranceCost = 0.0003m * cargoVan.Value; // 0.03% of cargo van value
                if (cargoVan.DriverExperience > 5)
                {
                    insuranceAdjustment = initialDailyInsuranceCost * 0.15m; // 15% discount for drivers with more than 5 years experience
                    dailyInsuranceCost = initialDailyInsuranceCost * 0.85m;
                }
                else
                {
                    dailyInsuranceCost = initialDailyInsuranceCost;
                }
            }

            decimal insuranceCost = dailyInsuranceCost * ActualRentalPeriod;
            return insuranceCost;
        }
    }
}
