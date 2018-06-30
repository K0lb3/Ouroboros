namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(1, "Success", 1, 1), Pin(0, "Start", 0, 0), NodeType("Multi/MultiTowerShowDetail", 0x7fe5)]
    public class FlowNode_MultiTowerShowDetail : FlowNode
    {
        [SerializeField]
        private GameObject DetailObject;

        public FlowNode_MultiTowerShowDetail()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0014;
            }
            this.OnClickDetail();
            base.ActivateOutputLinks(1);
        Label_0014:
            return;
        }

        public void OnClickDetail()
        {
            QuestParam param;
            MultiTowerFloorParam param2;
            GameObject obj2;
            QuestCampaignData[] dataArray;
            MultiTowerQuestInfo info;
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            param2 = DataSource.FindDataOfClass<MultiTowerFloorParam>(base.get_gameObject(), null);
            if (param2 != null)
            {
                goto Label_0030;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetMTFloorParam(GlobalVars.SelectedQuestID);
        Label_0030:
            if (((this.DetailObject != null) == null) || (param == null))
            {
                goto Label_00A5;
            }
            obj2 = Object.Instantiate<GameObject>(this.DetailObject);
            DataSource.Bind<QuestParam>(obj2, param);
            dataArray = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(param);
            DataSource.Bind<QuestCampaignData[]>(obj2, (((int) dataArray.Length) != null) ? dataArray : null);
            DataSource.Bind<QuestParam>(obj2, param);
            DataSource.Bind<MultiTowerFloorParam>(obj2, param2);
            info = obj2.GetComponent<MultiTowerQuestInfo>();
            if ((info != null) == null)
            {
                goto Label_00A5;
            }
            info.Refresh();
        Label_00A5:
            return;
        }
    }
}

