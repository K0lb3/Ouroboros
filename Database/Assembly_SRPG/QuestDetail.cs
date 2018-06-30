namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(100, "表示更新(完了)", 1, 100), Pin(10, "表示更新", 0, 10)]
    public class QuestDetail : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_REFRESH_UI = 10;
        private const int OUTPUT_REFRESH_UI = 100;
        [HeaderBar("▼ミッション報酬表示用オブジェクトの親"), SerializeField]
        private RectTransform m_ContentRoot;
        [SerializeField, HeaderBar("▼ミッション報酬表示用テンプレート")]
        private QuestMissionItem m_RewardItemTemplate;
        [SerializeField]
        private QuestMissionItem m_RewardUnitTemplate;
        [SerializeField]
        private QuestMissionItem m_RewardArtifactTemplate;
        [SerializeField]
        private QuestMissionItem m_RewardCardTemplate;
        [SerializeField]
        private QuestMissionItem m_RewardNothingTemplate;
        [HeaderBar("▼ミッション報酬が何も設定されていない時に表示するオブジェクト"), SerializeField]
        private GameObject m_NoMissionText;
        [SerializeField]
        private bool m_RefreshOnStart;
        [HeaderBar("▼ミッションの表示順(デフォルト=ソートしない)"), SerializeField]
        private SortOrder m_SortOrder;
        private List<ViewParam> m_ListItems;

        public QuestDetail()
        {
            this.m_RefreshOnStart = 1;
            this.m_ListItems = new List<ViewParam>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private int <SortItems>m__3CB(ViewParam item1, ViewParam item2)
        {
            return this.Compare_AchievemedToUnachieved(item1, item2);
        }

        [CompilerGenerated]
        private int <SortItems>m__3CC(ViewParam item1, ViewParam item2)
        {
            return this.Compare_UnachievedToAchievemed(item1, item2);
        }

        public void Activated(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_0016;
            }
            this.Refresh();
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_0016:
            return;
        }

        private unsafe int Compare_AchievemedToUnachieved(ViewParam viewParam1, ViewParam viewParam2)
        {
            int num;
            if (viewParam1 != null)
            {
                goto Label_0008;
            }
            return -1;
        Label_0008:
            if (viewParam2 != null)
            {
                goto Label_0010;
            }
            return 1;
        Label_0010:
            if (viewParam1 != viewParam2)
            {
                goto Label_0019;
            }
            return 0;
        Label_0019:
            if (viewParam1.IsAchieved != viewParam2.IsAchieved)
            {
                goto Label_003F;
            }
            return &viewParam1.MissionIndex.CompareTo(viewParam2.MissionIndex);
        Label_003F:
            if (viewParam1.IsAchieved == null)
            {
                goto Label_004C;
            }
            return -1;
        Label_004C:
            return 1;
        }

        private unsafe int Compare_UnachievedToAchievemed(ViewParam viewParam1, ViewParam viewParam2)
        {
            int num;
            if (viewParam1 != null)
            {
                goto Label_0008;
            }
            return -1;
        Label_0008:
            if (viewParam2 != null)
            {
                goto Label_0010;
            }
            return 1;
        Label_0010:
            if (viewParam1 != viewParam2)
            {
                goto Label_0019;
            }
            return 0;
        Label_0019:
            if (viewParam1.IsAchieved != viewParam2.IsAchieved)
            {
                goto Label_003F;
            }
            return &viewParam1.MissionIndex.CompareTo(viewParam2.MissionIndex);
        Label_003F:
            if (viewParam1.IsAchieved != null)
            {
                goto Label_004C;
            }
            return -1;
        Label_004C:
            return 1;
        }

        private void CreateItems(QuestParam questParam)
        {
            int num;
            QuestMissionItem item;
            QuestBonusObjective objective;
            ConceptCardIcon icon;
            ConceptCardData data;
            ViewParam param;
            if (questParam.bonusObjective != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_00A7;
        Label_0013:
            item = null;
            objective = questParam.bonusObjective[num];
            item = this.CreateRewardItem(objective);
            if (objective.itemType != 6)
            {
                goto Label_005F;
            }
            icon = item.get_gameObject().GetComponentInChildren<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_005F;
            }
            data = ConceptCardData.CreateConceptCardDataForDisplay(objective.item);
            icon.Setup(data);
        Label_005F:
            item.SetGameParameterIndex(num);
            param = new ViewParam();
            param.ListItem = item;
            param.MissionIndex = num;
            param.IsAchieved = questParam.IsMissionClear(num);
            this.m_ListItems.Add(param);
            GameParameter.UpdateAll(item.get_gameObject());
            num += 1;
        Label_00A7:
            if (num < ((int) questParam.bonusObjective.Length))
            {
                goto Label_0013;
            }
            return;
        }

        private QuestMissionItem CreateRewardItem(QuestBonusObjective bonusObjective)
        {
            QuestMissionItem item;
            ItemParam param;
            GameObject obj2;
            if (bonusObjective != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            item = null;
            if (bonusObjective.itemType != 3)
            {
                goto Label_0022;
            }
            item = this.m_RewardArtifactTemplate;
            goto Label_008C;
        Label_0022:
            if (bonusObjective.itemType != 6)
            {
                goto Label_003A;
            }
            item = this.m_RewardCardTemplate;
            goto Label_008C;
        Label_003A:
            if (bonusObjective.itemType != 100)
            {
                goto Label_0053;
            }
            item = this.m_RewardNothingTemplate;
            goto Label_008C;
        Label_0053:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(bonusObjective.item);
            if (param != null)
            {
                goto Label_006C;
            }
            return null;
        Label_006C:
            if (param.type != 0x10)
            {
                goto Label_0085;
            }
            item = this.m_RewardUnitTemplate;
            goto Label_008C;
        Label_0085:
            item = this.m_RewardItemTemplate;
        Label_008C:
            obj2 = Object.Instantiate<GameObject>(item.get_gameObject());
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.m_ContentRoot.get_transform(), 0);
            return obj2.GetComponent<QuestMissionItem>();
        }

        private void Refresh()
        {
            QuestParam param;
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_003C;
            }
            if (param.HasMission() != null)
            {
                goto Label_002F;
            }
            GameUtility.SetGameObjectActive(this.m_NoMissionText, 1);
            goto Label_003C;
        Label_002F:
            this.CreateItems(param);
            this.SortItems();
        Label_003C:
            return;
        }

        private void SortItems()
        {
            int num;
            if (this.m_SortOrder != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.m_SortOrder != 1)
            {
                goto Label_0034;
            }
            this.m_ListItems.Sort(new Comparison<ViewParam>(this.<SortItems>m__3CB));
            goto Label_0057;
        Label_0034:
            if (this.m_SortOrder != 2)
            {
                goto Label_0057;
            }
            this.m_ListItems.Sort(new Comparison<ViewParam>(this.<SortItems>m__3CC));
        Label_0057:
            num = 0;
            goto Label_009F;
        Label_005E:
            if ((this.m_ListItems[num].ListItem == null) == null)
            {
                goto Label_007F;
            }
            goto Label_009B;
        Label_007F:
            this.m_ListItems[num].ListItem.get_transform().SetSiblingIndex(num);
        Label_009B:
            num += 1;
        Label_009F:
            if (num < this.m_ListItems.Count)
            {
                goto Label_005E;
            }
            return;
        }

        private void Start()
        {
            GameUtility.SetGameObjectActive(this.m_RewardItemTemplate, 0);
            GameUtility.SetGameObjectActive(this.m_RewardUnitTemplate, 0);
            GameUtility.SetGameObjectActive(this.m_RewardArtifactTemplate, 0);
            GameUtility.SetGameObjectActive(this.m_RewardCardTemplate, 0);
            GameUtility.SetGameObjectActive(this.m_RewardNothingTemplate, 0);
            GameUtility.SetGameObjectActive(this.m_NoMissionText, 0);
            if (this.m_RefreshOnStart == null)
            {
                goto Label_0059;
            }
            this.Refresh();
        Label_0059:
            return;
        }

        public enum SortOrder
        {
            None,
            AchievemedToUnachieved,
            UnachievedToAchievemed
        }

        private class ViewParam
        {
            private QuestMissionItem m_ListItem;
            private int m_MissionIndex;
            private bool m_IsAchieved;

            public ViewParam()
            {
                base..ctor();
                return;
            }

            public QuestMissionItem ListItem
            {
                get
                {
                    return this.m_ListItem;
                }
                set
                {
                    this.m_ListItem = value;
                    return;
                }
            }

            public int MissionIndex
            {
                get
                {
                    return this.m_MissionIndex;
                }
                set
                {
                    this.m_MissionIndex = value;
                    return;
                }
            }

            public bool IsAchieved
            {
                get
                {
                    return this.m_IsAchieved;
                }
                set
                {
                    this.m_IsAchieved = value;
                    return;
                }
            }
        }
    }
}

