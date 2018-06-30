namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class MissionDetail : MonoBehaviour
    {
        [SerializeField]
        private QuestMissionItem ItemTemplate;
        [SerializeField]
        private QuestMissionItem UnitTemplate;
        [SerializeField]
        private QuestMissionItem ArtifactTemplate;
        [SerializeField]
        private QuestMissionItem ConceptCardTemplate;
        [SerializeField]
        private QuestMissionItem NothingRewardTemplate;
        [SerializeField]
        private GameObject ContentsParent;
        [SerializeField]
        private GameObject Window;
        [SerializeField]
        private UnityEngine.UI.ScrollRect ScrollRect;
        [SerializeField]
        private GameObject Scrollbar;

        public MissionDetail()
        {
            base..ctor();
            return;
        }

        private unsafe void Awake()
        {
            QuestParam param;
            RectTransform transform;
            Vector2 vector;
            Vector2 vector2;
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_003D;
            }
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_003D;
            }
            param = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
        Label_003D:
            if (param != null)
            {
                goto Label_0044;
            }
            return;
        Label_0044:
            if (param.bonusObjective != null)
            {
                goto Label_0050;
            }
            return;
        Label_0050:
            if (((int) param.bonusObjective.Length) <= 3)
            {
                goto Label_0105;
            }
            if ((this.Scrollbar != null) == null)
            {
                goto Label_007B;
            }
            this.Scrollbar.SetActive(1);
        Label_007B:
            if ((this.ScrollRect != null) == null)
            {
                goto Label_00A4;
            }
            this.ScrollRect.set_horizontal(0);
            this.ScrollRect.set_vertical(1);
        Label_00A4:
            if ((this.Window == null) == null)
            {
                goto Label_00B6;
            }
            return;
        Label_00B6:
            transform = this.Window.get_transform() as RectTransform;
            if ((transform != null) == null)
            {
                goto Label_014B;
            }
            transform.set_sizeDelta(new Vector2(&transform.get_sizeDelta().x, &transform.get_sizeDelta().y + 120f));
            goto Label_014B;
        Label_0105:
            if ((this.Scrollbar != null) == null)
            {
                goto Label_0122;
            }
            this.Scrollbar.SetActive(0);
        Label_0122:
            if ((this.ScrollRect != null) == null)
            {
                goto Label_014B;
            }
            this.ScrollRect.set_horizontal(0);
            this.ScrollRect.set_vertical(0);
        Label_014B:
            this.RefreshQuestMissionReward(param);
            return;
        }

        private void RefreshQuestMissionReward(QuestParam questParam)
        {
            int num;
            QuestMissionItem item;
            QuestBonusObjective objective;
            ConceptCardIcon icon;
            ConceptCardData data;
            ItemParam param;
            if (questParam.bonusObjective != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_020F;
        Label_0013:
            item = null;
            objective = questParam.bonusObjective[num];
            if (objective.itemType != 3)
            {
                goto Label_0045;
            }
            item = Object.Instantiate<GameObject>(this.ArtifactTemplate.get_gameObject()).GetComponent<QuestMissionItem>();
            goto Label_011E;
        Label_0045:
            if (objective.itemType != 6)
            {
                goto Label_0099;
            }
            item = Object.Instantiate<GameObject>(this.ConceptCardTemplate.get_gameObject()).GetComponent<QuestMissionItem>();
            icon = item.get_gameObject().GetComponent<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_011E;
            }
            data = ConceptCardData.CreateConceptCardDataForDisplay(objective.item);
            icon.Setup(data);
            goto Label_011E;
        Label_0099:
            if (objective.itemType != 100)
            {
                goto Label_00C1;
            }
            item = Object.Instantiate<GameObject>(this.NothingRewardTemplate.get_gameObject()).GetComponent<QuestMissionItem>();
            goto Label_011E;
        Label_00C1:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(objective.item);
            if (param != null)
            {
                goto Label_00DF;
            }
            goto Label_020B;
        Label_00DF:
            if (param.type != 0x10)
            {
                goto Label_0108;
            }
            item = Object.Instantiate<GameObject>(this.UnitTemplate.get_gameObject()).GetComponent<QuestMissionItem>();
            goto Label_011E;
        Label_0108:
            item = Object.Instantiate<GameObject>(this.ItemTemplate.get_gameObject()).GetComponent<QuestMissionItem>();
        Label_011E:
            if ((item == null) == null)
            {
                goto Label_012F;
            }
            goto Label_020B;
        Label_012F:
            if ((item.Star != null) == null)
            {
                goto Label_014C;
            }
            item.Star.Index = num;
        Label_014C:
            if ((item.FrameParam != null) == null)
            {
                goto Label_0169;
            }
            item.FrameParam.Index = num;
        Label_0169:
            if ((item.IconParam != null) == null)
            {
                goto Label_0186;
            }
            item.IconParam.Index = num;
        Label_0186:
            if ((item.NameParam != null) == null)
            {
                goto Label_01A3;
            }
            item.NameParam.Index = num;
        Label_01A3:
            if ((item.AmountParam != null) == null)
            {
                goto Label_01C0;
            }
            item.AmountParam.Index = num;
        Label_01C0:
            if ((item.ObjectigveParam != null) == null)
            {
                goto Label_01DD;
            }
            item.ObjectigveParam.Index = num;
        Label_01DD:
            item.get_gameObject().SetActive(1);
            item.get_transform().SetParent(this.ContentsParent.get_transform(), 0);
            GameParameter.UpdateAll(item.get_gameObject());
        Label_020B:
            num += 1;
        Label_020F:
            if (num < ((int) questParam.bonusObjective.Length))
            {
                goto Label_0013;
            }
            return;
        }
    }
}

