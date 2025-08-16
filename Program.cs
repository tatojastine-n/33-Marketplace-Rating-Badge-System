using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Seller
{
    public string Name { get; set; }
    public double[] Ratings { get; set; }
    public int Sales { get; set; }
    public int Cancellations { get; set; }
    public string Badge { get; private set; }

    public Seller(string name, double[] ratings, int sales, int cancellations)
    {
        Name = name;
        Ratings = ratings;
        Sales = sales;
        Cancellations = cancellations;
        Badge = AssignBadge();
    }

    public double AverageRating()
    {
        return Ratings.Length > 0 ? Ratings.Average() : 0;
    }

    public double CancellationRate()
    {
        return Sales > 0 ? (double)Cancellations / Sales : 0;
    }

    public string SalesTier()
    {
        if (Sales >= 100) return "Platinum";
        if (Sales >= 50) return "Gold";
        if (Sales >= 20) return "Silver";
        return "Bronze";
    }

    private string AssignBadge()
    {
        double avgRating = AverageRating();
        double cancellationRate = CancellationRate();
        string salesTier = SalesTier();

        switch (salesTier)
        {
            case "Platinum":
                return avgRating >= 4.5 && cancellationRate < 0.05 ? "Top Seller" : "Platinum Seller";
            case "Gold":
                return avgRating >= 4.0 && cancellationRate < 0.1 ? "Gold Seller" : "Regular Seller";
            case "Silver":
                return avgRating >= 3.5 ? "Silver Seller" : "Regular Seller";
            default:
                return "New Seller";
        }
    }
}
namespace Marketplace_Rating___Badge_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Seller> sellers = new List<Seller>
        {
            new Seller("Alice", new double[] { 4.5, 4.7, 4.8 }, 120, 2),
            new Seller("Bob", new double[] { 4.0, 4.1, 4.2 }, 60, 5),
            new Seller("Charlie", new double[] { 3.0, 3.5, 4.0 }, 30, 10),
            new Seller("David", new double[] { 4.9, 4.8, 4.7 }, 200, 1),
            new Seller("Eve", new double[] { 2.5, 3.0, 2.0 }, 10, 3)
        };

            var leaderboard = sellers
                .OrderByDescending(s => s.Sales)
                .ThenByDescending(s => s.AverageRating())
                .ToList();

            Console.WriteLine("Leaderboard:");
            foreach (var seller in leaderboard)
            {
                Console.WriteLine($"{seller.Name}: Badge = {seller.Badge}, Avg Rating = {seller.AverageRating():F2}, Sales = {seller.Sales}, Cancellations = {seller.Cancellations}");
            }
        }
    }
}
