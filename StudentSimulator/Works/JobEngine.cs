using System;
using StudentSimulator.Data.PayloadData;
using StudentSimulator.Works;

namespace StudentSimulator.Works
{
    public class JobsEngine
    {
        public Reward RunJob<TTask, TSession>(
            TSession session, 
            TTask task, 
            MiniGameHandler<TSession> gameLogic, 
            RewardCalculator<TTask> rewardCalculator,
            double failPenalty = 0.2)
        {
            bool success = gameLogic(session);

            Reward totalReward = rewardCalculator(task);

            if (!success)
            {
                totalReward.Money *= failPenalty;
                Console.WriteLine($"Ви не впоралися. Отримано лише частина оплати: {totalReward.Money}грн");
            }
            else
            {
                Console.WriteLine($"Успіх! Ви заробили: {totalReward.Money}грн");
            }

            return totalReward;
        }
    }
}