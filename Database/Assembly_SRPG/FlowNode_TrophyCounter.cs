namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(0, "RequestReviewURL", 0, 0), NodeType("Trophy/TrophyCounter", 0x7fe5), Pin(100, "output", 1, 100)]
    public class FlowNode_TrophyCounter : FlowNode
    {
        public string ReviewURL_Android;
        public string ReviewURL_iOS;
        public string ReviewURL_Generic;
        public string ReviewURL_Twitter;

        public FlowNode_TrophyCounter()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            PlayerData data;
            TrophyObjective[] objectiveArray;
            int num;
            if (pinID != null)
            {
                goto Label_0061;
            }
            str = null;
            str = this.ReviewURL_Generic;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0020;
            }
            Application.OpenURL(str);
        Label_0020:
            data = MonoSingleton<GameManager>.Instance.Player;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x11);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0051;
        Label_0043:
            data.AddTrophyCounter(objectiveArray[num], 1);
            num -= 1;
        Label_0051:
            if (num >= 0)
            {
                goto Label_0043;
            }
            base.ActivateOutputLinks(100);
        Label_0061:
            return;
        }
    }
}

