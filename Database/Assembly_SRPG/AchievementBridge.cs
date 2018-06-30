namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class AchievementBridge : MonoBehaviour
    {
        public AchievementBridge()
        {
            base..ctor();
            return;
        }

        public void OnClick()
        {
            GameManager manager;
            if (GameCenterManager.IsAuth() == null)
            {
                goto Label_0031;
            }
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_0027;
            }
            manager.Player.UpdateAchievementTrophyStates();
        Label_0027:
            GameCenterManager.ShowAchievement();
            goto Label_0036;
        Label_0031:
            GameCenterManager.ReAuth();
        Label_0036:
            return;
        }
    }
}

