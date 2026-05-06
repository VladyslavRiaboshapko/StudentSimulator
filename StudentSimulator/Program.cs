using StudentSimulator.Works;
using StudentSimulator.University.Exams;
using StudentSimulator.University.Lectures;
using StudentSimulator.University.Practises;
using StudentSimulator.Works.Repo;
using StudentSimulator.Domain.Core.User;
using System.Security.Cryptography.X509Certificates;
using StudentSimulator.Shops;
public class Program
{
    public static void Main(string[] args)
    {
        Supermarket supermarket = SupermarketFactory.CreateSupermarket();

        supermarket.ShowProducts();
    }
}