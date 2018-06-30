namespace SRPG
{
    using System;

    public class PlayerLevelUpInfo
    {
        public int LevelPrev;
        public int LevelNext;
        public int StaminaNext;
        public int StaminaMaxPrev;
        public int StaminaMaxNext;
        public int MaxFriendNumPrev;
        public int MaxFriendNumNext;
        public string[] UnlockInfo;

        public PlayerLevelUpInfo()
        {
            base..ctor();
            return;
        }
    }
}

