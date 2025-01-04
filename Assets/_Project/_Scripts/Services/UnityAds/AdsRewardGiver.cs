using System;

public class AdsRewardGiver 
{
    public event Action OnGiveSecondChance;

    public void GiveReward(int rewardId)
    {
        switch (rewardId)
        {
            case 0:
                GiveSecondChance();
                break;
            default:
                break;
        }
    }

    private void GiveSecondChance()
    {
        OnGiveSecondChance?.Invoke();
    }
}
