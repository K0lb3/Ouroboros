namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(1, "Matching", 0, 1)]
    public class RankMatchMatchingInfo : MonoBehaviour, IFlowInterface
    {
        public RankMatchMatchingInfo()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000C;
            }
            ProgressWindow.OpenRankMatchLoadScreen();
        Label_000C:
            return;
        }

        public void Start()
        {
            MonoSingleton<GameManager>.Instance.AudienceMode = 0;
            return;
        }
    }
}

