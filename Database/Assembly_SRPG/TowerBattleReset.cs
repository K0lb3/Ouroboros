namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0, "初期化（実行）", 0, 0), Pin(100, "初期化（終了）", 1, 100)]
    public class TowerBattleReset : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_INITIALIZE_BEGIN = 0;
        private const int OUTPUT_INITIALIZE_END = 100;
        [SerializeField]
        private GameObject TollRecovery;
        [SerializeField]
        private GameObject FreeRecovery;
        [SerializeField]
        private GameObject HaveItemObject;
        [SerializeField]
        private Text HaveItemNum;
        [SerializeField, HeaderBar("▼ミッション表示エリアのタイトル")]
        private Text m_MissionListTitle;
        [SerializeField]
        private Text m_MissionListTitleNoItem;
        [HeaderBar("▼ミッション表示リストの親"), SerializeField]
        private RectTransform m_MissionListParent;
        [HeaderBar("▼ミッション表示用テンプレート"), SerializeField]
        private QuestMissionItem m_MissionItemTemplate;
        private List<GameObject> m_MissionListItems;

        public TowerBattleReset()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0014;
            }
            this.InitializeMissionItems();
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_0014:
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

        private void InitializeMissionItems()
        {
            QuestParam param;
            param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            this.CreateResetMissionItems(param);
            if (this.m_MissionListItems == null)
            {
                goto Label_0044;
            }
            if (this.m_MissionListItems.Count <= 0)
            {
                goto Label_0044;
            }
            GameUtility.SetGameObjectActive(this.m_MissionListTitle, 1);
            goto Label_005C;
        Label_0044:
            GameUtility.SetGameObjectActive(this.m_MissionListTitleNoItem, 1);
            GameUtility.SetGameObjectActive(this.m_MissionListParent, 0);
        Label_005C:
            return;
        }

        private unsafe void Start()
        {
            TowerParam param;
            Text text;
            int num;
            GameUtility.SetGameObjectActive(this.m_MissionItemTemplate, 0);
            GameUtility.SetGameObjectActive(this.m_MissionListTitle, 0);
            GameUtility.SetGameObjectActive(this.m_MissionListTitleNoItem, 0);
            GameUtility.SetGameObjectActive(this.m_MissionListParent, 0);
            param = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
            if (param != null)
            {
                goto Label_0047;
            }
            return;
        Label_0047:
            if (param.floor_reset_coin <= 0)
            {
                goto Label_00BE;
            }
            this.TollRecovery.SetActive(1);
            this.FreeRecovery.SetActive(0);
            this.TollRecovery.GetComponent<Text>().set_text(string.Format(LocalizedText.Get("sys.TOWER_BATTLE_RESET_DESCRIPTION_02"), &param.floor_reset_coin.ToString()));
            this.HaveItemNum.set_text(&MonoSingleton<GameManager>.Instance.Player.Coin.ToString());
            goto Label_00E2;
        Label_00BE:
            this.TollRecovery.SetActive(0);
            this.FreeRecovery.SetActive(1);
            this.HaveItemObject.SetActive(0);
        Label_00E2:
            return;
        }
    }
}

