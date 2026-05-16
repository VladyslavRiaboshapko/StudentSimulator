using StudentSimulator.Works;
using StudentSimulator.University.Exams;
using StudentSimulator.University.Lectures;
using StudentSimulator.University.Practises;
using StudentSimulator.Works.Repo;
using StudentSimulator.Domain.Core.User;
using System.Security.Cryptography.X509Certificates;
using StudentSimulator.Shops;
using StudentSimulator.GameLogic.SaveLogic;
using StudentSimulator.Data.PayloadData;
using StudentSimulator.Items.Drinks;
using StudentSimulator.GameLogic.Launcher;
public class Program
{
    public static void Main(string[] args)
    {
        GameLauncher gameLauncher = new GameLauncher();
        gameLauncher.Start();
    }
}