using StudentSimulator.Domain.Core.User;

namespace StudentSimulator.Data.PayloadData;
public class GameState
{
    public User Player { get; set; }
    public int CurrentDay { get; set; }
}