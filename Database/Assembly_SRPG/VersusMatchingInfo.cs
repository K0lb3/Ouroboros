namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(2, "DraftMatching", 0, 2), Pin(1, "Matching", 0, 1)]
    public class VersusMatchingInfo : MonoBehaviour, IFlowInterface
    {
        public VersusMatchingInfo()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0017;
            }
            GlobalVars.IsVersusDraftMode = 0;
            ProgressWindow.OpenVersusLoadScreen();
            goto Label_0029;
        Label_0017:
            if (pinID != 2)
            {
                goto Label_0029;
            }
            GlobalVars.IsVersusDraftMode = 1;
            ProgressWindow.OpenVersusDraftLoadScreen();
        Label_0029:
            return;
        }

        public void Start()
        {
            MonoSingleton<GameManager>.Instance.AudienceMode = 0;
            GlobalVars.IsVersusDraftMode = 0;
            return;
        }
    }
}

