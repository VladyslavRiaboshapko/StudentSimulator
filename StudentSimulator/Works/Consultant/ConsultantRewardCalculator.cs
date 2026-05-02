namespace StudentSimulator.Works.Consultant;

public static class ConsultantRewardCalculator
{
    public static Reward Calculate(ConsultingSession session)
    {
        double typeMultiplier;
        double money;
        double stamina;
        double mood;

        switch(session.ClientType)
        {
            case ClientType.Student:
            typeMultiplier = 1;
            break;

            case ClientType.SmallBusiness:
            typeMultiplier = 2.5;
            break;

            case ClientType.Corporation:
            typeMultiplier = 4;
            break;
        }

        bool isSuccess = SuccessRandomizer.IsSuccess(session.Complexity / 10);

        if(isSuccess)
        {
            money = session.MinProfit * session.Duration * session.Complexity;
            stamina = session.Duration * session.Complexity * JobMultipliers.ConsultantStamina;
            mood = session.Complexity - session.MinProfit / (session.Complexity * session.Duration);
        }
        else
        {
            money = session.MinProfit * JobMultipliers.Lose;
            stamina = session.Duration * session.Complexity * JobMultipliers.ConsultantStamina;
            mood = session.MinProfit / (session.Duration * session.Complexity);
        }

        return new Reward(money, stamina, mood);
    }
}