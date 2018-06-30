namespace SRPG
{
    using GR;
    using System;
    using UnityEngine.UI;

    [Pin(1, "Success", 1, 1), Pin(2, "NoTrophy", 1, 2), Pin(0, "Request", 0, 0), NodeType("Trophy/ReqAchievedQuest", 0x7fe5)]
    public class FlowNode_ReqTrophyAchievedQuest : FlowNode_Network
    {
        public Text Quests;
        public Toggle toggle;

        public FlowNode_ReqTrophyAchievedQuest()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            TrophyParam param;
            if (pinID != null)
            {
                goto Label_0062;
            }
            param = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_0030;
            }
            if (TrophyConditionTypesEx.IsExtraClear(param.Objectives[0].type) != null)
            {
                goto Label_0045;
            }
        Label_0030:
            this.toggle.set_isOn(1);
            base.ActivateOutputLinks(2);
            return;
        Label_0045:
            base.ExecRequest(new ReqTrophyAchievedQuest(param.iname, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0062:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<TrophyQuests> response;
            string[] strArray;
            GameManager manager;
            int num;
            QuestParam param;
            if (Network.IsError == null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<TrophyQuests>>(&www.text);
            if ((this.Quests != null) == null)
            {
                goto Label_00BA;
            }
            strArray = response.body.histories;
            if (strArray == null)
            {
                goto Label_0044;
            }
            if (((int) strArray.Length) > 0)
            {
                goto Label_0067;
            }
        Label_0044:
            this.Quests.set_text(LocalizedText.Get("sys.TROPHY_NOT_ACHIEVEDQUEST"));
            Network.RemoveAPI();
            base.ActivateOutputLinks(1);
            return;
        Label_0067:
            manager = MonoSingleton<GameManager>.Instance;
            num = 0;
            goto Label_00B1;
        Label_0074:
            param = manager.FindQuest(strArray[num]);
            if (param != null)
            {
                goto Label_008B;
            }
            goto Label_00AD;
        Label_008B:
            this.Quests.set_text(this.Quests.get_text() + param.name + "\n");
        Label_00AD:
            num += 1;
        Label_00B1:
            if (num < ((int) strArray.Length))
            {
                goto Label_0074;
            }
        Label_00BA:
            Network.RemoveAPI();
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

