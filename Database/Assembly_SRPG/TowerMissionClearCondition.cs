namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0, "表示内容更新", 0, 0)]
    public class TowerMissionClearCondition : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_REFRESH_MISSION = 0;
        [SerializeField, HeaderBar("▼ウィンドウのタイトル")]
        private Text TowerName;
        [SerializeField]
        private Text TowerFloorNum;
        [HeaderBar("▼吹き出し内のオブジェクト"), SerializeField]
        private Text ClearConditionText01;
        [SerializeField]
        private Text ClearConditionText02;
        [SerializeField]
        private ImageArray ClearConditionImage01;
        [SerializeField]
        private ImageArray ClearConditionImage02;
        [HeaderBar("▼ミッション表示エリアのタイトル"), SerializeField]
        private Text m_MissionListTitle;
        [SerializeField]
        private Text m_MissionListTitleNoItem;
        [SerializeField, HeaderBar("▼ミッション表示リストの親")]
        private RectTransform m_MissionListParent;
        [HeaderBar("▼ミッション表示用テンプレート"), SerializeField]
        private QuestMissionItem m_MissionItemTemplate;
        private List<GameObject> m_MissionListItems;

        public TowerMissionClearCondition()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            if (pinID == null)
            {
                goto Label_000D;
            }
            goto Label_0018;
        Label_000D:
            this.Refresh();
            goto Label_001D;
        Label_0018:;
        Label_001D:
            return;
        }

        private QuestMissionItem CreateMissionItem(QuestBonusObjective bonusObjective)
        {
            QuestMissionItem item;
            GameObject obj2;
            if (bonusObjective != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            item = null;
            obj2 = Object.Instantiate<GameObject>(this.m_MissionItemTemplate.get_gameObject());
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.m_MissionListParent.get_transform(), 0);
            return obj2.GetComponent<QuestMissionItem>();
        }

        private void CreateResetMissionItems(QuestParam questParam)
        {
            int num;
            QuestMissionItem item;
            QuestBonusObjective objective;
            this.DeleteMissionItems();
            if (questParam != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (questParam.bonusObjective != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            if ((this.m_MissionListParent == null) == null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            if ((this.m_MissionItemTemplate == null) == null)
            {
                goto Label_003D;
            }
            return;
        Label_003D:
            if (this.m_MissionListItems != null)
            {
                goto Label_0053;
            }
            this.m_MissionListItems = new List<GameObject>();
        Label_0053:
            DataSource.Bind<QuestParam>(base.get_gameObject(), questParam);
            num = 0;
            goto Label_00C1;
        Label_0066:
            item = null;
            objective = questParam.bonusObjective[num];
            if (questParam.IsMissionClear(num) == null)
            {
                goto Label_0082;
            }
            goto Label_00BD;
        Label_0082:
            if (objective.IsProgressMission() != null)
            {
                goto Label_0092;
            }
            goto Label_00BD;
        Label_0092:
            item = this.CreateMissionItem(objective);
            item.SetGameParameterIndex(num);
            this.m_MissionListItems.Add(item.get_gameObject());
            GameParameter.UpdateAll(item.get_gameObject());
        Label_00BD:
            num += 1;
        Label_00C1:
            if (num < ((int) questParam.bonusObjective.Length))
            {
                goto Label_0066;
            }
            return;
        }

        private void DeleteMissionItems()
        {
            int num;
            if (this.m_MissionListItems == null)
            {
                goto Label_0050;
            }
            num = 0;
            goto Label_0034;
        Label_0012:
            Object.Destroy(this.m_MissionListItems[num]);
            this.m_MissionListItems[num] = null;
            num += 1;
        Label_0034:
            if (num < this.m_MissionListItems.Count)
            {
                goto Label_0012;
            }
            this.m_MissionListItems.Clear();
        Label_0050:
            return;
        }

        private void Refresh()
        {
            TowerParam param;
            TowerFloorParam param2;
            QuestParam param3;
            int num;
            int num2;
            param = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
            if (param != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            param2 = MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID);
            if (param2 != null)
            {
                goto Label_002E;
            }
            return;
        Label_002E:
            param3 = param2.GetQuestParam();
            if (param3 != null)
            {
                goto Label_003C;
            }
            return;
        Label_003C:
            if (param3.bonusObjective != null)
            {
                goto Label_0048;
            }
            return;
        Label_0048:
            num = param3.GetClearMissionNum();
            num2 = (int) param3.bonusObjective.Length;
            if (param3.IsMissionCompleteALL() == null)
            {
                goto Label_009C;
            }
            this.ClearConditionText01.set_text(string.Format(LocalizedText.Get("sys.TOWER_CLEAR_CINDITION_01_CLEAR"), (int) num, (int) num2));
            this.ClearConditionImage01.ImageIndex = 0;
            goto Label_00CF;
        Label_009C:
            this.ClearConditionText01.set_text(string.Format(LocalizedText.Get("sys.TOWER_CLEAR_CINDITION_01"), (int) num, (int) num2));
            this.ClearConditionImage01.ImageIndex = 1;
        Label_00CF:
            if (param3.state != 2)
            {
                goto Label_0101;
            }
            this.ClearConditionText02.set_text(LocalizedText.Get("sys.TOWER_CLEAR_CINDITION_02_CLEAR"));
            this.ClearConditionImage02.ImageIndex = 0;
            goto Label_0122;
        Label_0101:
            this.ClearConditionText02.set_text(LocalizedText.Get("sys.TOWER_CLEAR_CINDITION_02"));
            this.ClearConditionImage02.ImageIndex = 1;
        Label_0122:
            if ((this.TowerName != null) == null)
            {
                goto Label_0144;
            }
            this.TowerName.set_text(param.name);
        Label_0144:
            if ((this.TowerFloorNum != null) == null)
            {
                goto Label_0166;
            }
            this.TowerFloorNum.set_text(param2.name);
        Label_0166:
            this.CreateResetMissionItems(param3);
            if (this.m_MissionListItems == null)
            {
                goto Label_019A;
            }
            if (this.m_MissionListItems.Count <= 0)
            {
                goto Label_019A;
            }
            GameUtility.SetGameObjectActive(this.m_MissionListTitle, 1);
            goto Label_01B2;
        Label_019A:
            GameUtility.SetGameObjectActive(this.m_MissionListTitleNoItem, 1);
            GameUtility.SetGameObjectActive(this.m_MissionListParent, 0);
        Label_01B2:
            return;
        }

        private void Start()
        {
            GameUtility.SetGameObjectActive(this.m_MissionItemTemplate, 0);
            GameUtility.SetGameObjectActive(this.m_MissionListTitle, 0);
            GameUtility.SetGameObjectActive(this.m_MissionListTitleNoItem, 0);
            this.Refresh();
            return;
        }
    }
}

