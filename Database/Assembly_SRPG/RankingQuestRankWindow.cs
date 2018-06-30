namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(200, "ランキングリスト更新完了", 1, 200), Pin(300, "リストが選択された", 1, 300), Pin(100, "ランキングリスト更新", 0, 100)]
    public class RankingQuestRankWindow : MonoBehaviour, IFlowInterface
    {
        public const int INPUT_LIST_UPDATE = 100;
        public const int OUTPUT_LIST_UPDATED = 200;
        public const int OUTPUT_LIST_SELECTED = 300;
        [SerializeField]
        private RankingQuestRankList m_TargetList;
        [SerializeField]
        private ScrollListController m_ScrollListController;
        [SerializeField]
        private GameObject m_RootObject;
        [SerializeField]
        private Text m_WindowTitleText;
        [SerializeField]
        private GameObject m_OwnRankBanner;
        [SerializeField]
        private GameObject m_NotRegisteredText;
        [SerializeField]
        private GameObject m_NotSummaryData;
        private RankingQuestParam m_RankingQuestParam;

        public RankingQuestRankWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            RankingQuestParam param;
            QuestParam param2;
            if (pinID != 100)
            {
                goto Label_0086;
            }
            param = GlobalVars.SelectedRankingQuestParam;
            if (GlobalVars.SelectedRankingQuestParam != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(param.iname);
            if (param2 == null)
            {
                goto Label_0052;
            }
            if ((this.m_WindowTitleText != null) == null)
            {
                goto Label_0052;
            }
            this.m_WindowTitleText.set_text(param2.name);
        Label_0052:
            this.m_RankingQuestParam = param;
            this.m_ScrollListController.set_enabled(1);
            this.m_TargetList.OnSetUpItems();
            this.m_ScrollListController.Refresh();
            FlowNode_GameObject.ActivateOutputLinks(this, 200);
        Label_0086:
            return;
        }

        public void OnItemSelect(GameObject go)
        {
            RankingQuestUserData data;
            ReqQuestRankingPartyData data2;
            data = DataSource.FindDataOfClass<RankingQuestUserData>(go, null);
            if (data == null)
            {
                goto Label_0076;
            }
            data2 = new ReqQuestRankingPartyData();
            data2.m_ScheduleID = this.m_RankingQuestParam.schedule_id;
            data2.m_RankingType = this.m_RankingQuestParam.type;
            data2.m_QuestID = this.m_RankingQuestParam.iname;
            data2.m_TargetUID = data.m_UID;
            DataSource.Bind<RankingQuestUserData>(this.m_RootObject, data);
            DataSource.Bind<ReqQuestRankingPartyData>(this.m_RootObject, data2);
            FlowNode_GameObject.ActivateOutputLinks(this, 300);
        Label_0076:
            return;
        }

        public void SetData(RankingQuestUserData[] data)
        {
            this.m_TargetList.SetData(data);
            if ((this.m_NotSummaryData != null) == null)
            {
                goto Label_0037;
            }
            this.m_NotSummaryData.SetActive((data == null) ? 1 : (((int) data.Length) < 1));
        Label_0037:
            return;
        }

        public void SetData(RankingQuestUserData[] data, RankingQuestUserData ownData)
        {
            this.m_TargetList.SetData(data);
            this.SetOwnRankingData(ownData);
            if ((this.m_NotSummaryData != null) == null)
            {
                goto Label_003E;
            }
            this.m_NotSummaryData.SetActive((data == null) ? 1 : (((int) data.Length) < 1));
        Label_003E:
            return;
        }

        private void SetOwnRankingData(RankingQuestUserData ownData)
        {
            bool flag;
            RankingQuestInfo info;
            flag = (ownData == null) == 0;
            if ((this.m_OwnRankBanner != null) == null)
            {
                goto Label_00A0;
            }
            if ((this.m_NotRegisteredText != null) == null)
            {
                goto Label_00A0;
            }
            if (flag == null)
            {
                goto Label_0088;
            }
            this.m_OwnRankBanner.SetActive(1);
            this.m_NotRegisteredText.SetActive(0);
            DataSource.Bind<RankingQuestUserData>(this.m_OwnRankBanner, ownData);
            DataSource.Bind<UnitData>(this.m_OwnRankBanner, ownData.m_UnitData);
            info = this.m_OwnRankBanner.GetComponent<RankingQuestInfo>();
            if ((info != null) == null)
            {
                goto Label_00A0;
            }
            info.UpdateValue();
            goto Label_00A0;
        Label_0088:
            this.m_OwnRankBanner.SetActive(0);
            this.m_NotRegisteredText.SetActive(1);
        Label_00A0:
            return;
        }

        private void Start()
        {
            ListItemEvents events;
            if ((this.m_OwnRankBanner != null) == null)
            {
                goto Label_003B;
            }
            events = this.m_OwnRankBanner.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_003B;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        Label_003B:
            return;
        }
    }
}

