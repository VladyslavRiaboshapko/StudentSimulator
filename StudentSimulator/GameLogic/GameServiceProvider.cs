using StudentSimulator.Data.PayloadData;
using StudentSimulator.GameLogic;
using StudentSimulator.GameLogic.SaveLogic;
using StudentSimulator.Works.Repo;
using StudentSimulator.Works;

namespace StudentSimulator.GameLogic;
public static class GameServiceProvider
{
    private const string JobsJsonPath = "Data/JobsData.json";

    public static GameEngine CreateEngine(GameState state, SaveManager saveManager)
    {
        var jobsRepository = new JobsRepository(JobsJsonPath);
        var jobsEngine = new JobsEngine();

        var jobManager = new JobManager(jobsRepository, jobsEngine, state.Player);

        return new GameEngine(state, saveManager, jobManager);
    }
}