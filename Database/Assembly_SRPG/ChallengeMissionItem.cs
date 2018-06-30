namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class ChallengeMissionItem : MonoBehaviour
    {
        public ButtonObject ButtonNormal;
        public ButtonObject ButtonHighlight;
        public ButtonObject ButtonSecret;
        public Image ClearBadge;
        public UnityAction OnClick;

        public ChallengeMissionItem()
        {
            base..ctor();
            return;
        }

        public unsafe void Refresh()
        {
            GameManager manager;
            TrophyParam param;
            TrophyState state;
            State state2;
            ButtonObject obj2;
            ItemParam param2;
            ConceptCardParam param3;
            ConceptCardData data;
            base.get_gameObject().SetActive(1);
            this.ClearBadge.get_gameObject().SetActive(0);
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            param = DataSource.FindDataOfClass<TrophyParam>(base.get_gameObject(), null);
            if ((manager == null) != null)
            {
                goto Label_0042;
            }
            if (param != null)
            {
                goto Label_0085;
            }
        Label_0042:
            this.ButtonHighlight.button.get_gameObject().SetActive(0);
            this.ButtonNormal.button.get_gameObject().SetActive(0);
            this.ButtonSecret.button.get_gameObject().SetActive(1);
            return;
        Label_0085:
            state = ChallengeMission.GetTrophyCounter(param);
            state2 = 0;
            if (state.IsEnded == null)
            {
                goto Label_00A0;
            }
            state2 = 2;
            goto Label_00AD;
        Label_00A0:
            if (state.IsCompleted == null)
            {
                goto Label_00AD;
            }
            state2 = 1;
        Label_00AD:
            obj2 = null;
            if (state2 != 1)
            {
                goto Label_0106;
            }
            this.ButtonHighlight.button.get_gameObject().SetActive(1);
            this.ButtonNormal.button.get_gameObject().SetActive(0);
            this.ButtonSecret.button.get_gameObject().SetActive(0);
            obj2 = this.ButtonHighlight;
            goto Label_0150;
        Label_0106:
            this.ButtonHighlight.button.get_gameObject().SetActive(0);
            this.ButtonNormal.button.get_gameObject().SetActive(1);
            this.ButtonSecret.button.get_gameObject().SetActive(0);
            obj2 = this.ButtonNormal;
        Label_0150:
            if ((this.ClearBadge != null) == null)
            {
                goto Label_0175;
            }
            this.ClearBadge.get_gameObject().SetActive(state2 == 2);
        Label_0175:
            if (obj2 == null)
            {
                goto Label_01A0;
            }
            if ((obj2.title != null) == null)
            {
                goto Label_01A0;
            }
            obj2.title.set_text(param.Name);
        Label_01A0:
            if (obj2 == null)
            {
                goto Label_01F4;
            }
            if ((obj2.button != null) == null)
            {
                goto Label_01F4;
            }
            obj2.button.get_onClick().RemoveAllListeners();
            obj2.button.get_onClick().AddListener(this.OnClick);
            obj2.button.set_interactable((state2 == 2) == 0);
        Label_01F4:
            if (obj2 == null)
            {
                goto Label_049B;
            }
            if ((obj2.reward != null) == null)
            {
                goto Label_049B;
            }
            if (param.Gold == null)
            {
                goto Label_025D;
            }
            obj2.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (int) param.Gold));
            GameUtility.SetGameObjectActive(obj2.icon, 1);
            GameUtility.SetGameObjectActive(obj2.conceptCardIcon, 0);
            goto Label_049B;
        Label_025D:
            if (param.Exp == null)
            {
                goto Label_02AD;
            }
            obj2.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (int) param.Exp));
            GameUtility.SetGameObjectActive(obj2.icon, 1);
            GameUtility.SetGameObjectActive(obj2.conceptCardIcon, 0);
            goto Label_049B;
        Label_02AD:
            if (param.Coin == null)
            {
                goto Label_02FD;
            }
            obj2.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) param.Coin));
            GameUtility.SetGameObjectActive(obj2.icon, 1);
            GameUtility.SetGameObjectActive(obj2.conceptCardIcon, 0);
            goto Label_049B;
        Label_02FD:
            if (param.Stamina == null)
            {
                goto Label_034D;
            }
            obj2.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (int) param.Stamina));
            GameUtility.SetGameObjectActive(obj2.icon, 1);
            GameUtility.SetGameObjectActive(obj2.conceptCardIcon, 0);
            goto Label_049B;
        Label_034D:
            if (param.Items == null)
            {
                goto Label_03DD;
            }
            if (((int) param.Items.Length) <= 0)
            {
                goto Label_03DD;
            }
            GameUtility.SetGameObjectActive(obj2.icon, 1);
            GameUtility.SetGameObjectActive(obj2.conceptCardIcon, 0);
            param2 = manager.GetItemParam(&(param.Items[0]).iname);
            if (param2 == null)
            {
                goto Label_049B;
            }
            obj2.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_ITEM"), param2.name, (int) &(param.Items[0]).Num));
            goto Label_049B;
        Label_03DD:
            if (param.ConceptCards == null)
            {
                goto Label_049B;
            }
            if (((int) param.ConceptCards.Length) <= 0)
            {
                goto Label_049B;
            }
            GameUtility.SetGameObjectActive(obj2.icon, 0);
            GameUtility.SetGameObjectActive(obj2.conceptCardIcon, 1);
            param3 = manager.MasterParam.GetConceptCardParam(&(param.ConceptCards[0]).iname);
            if (param3 == null)
            {
                goto Label_046D;
            }
            obj2.reward.set_text(string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_CONCEPT_CARD"), param3.name, (int) &(param.ConceptCards[0]).Num));
        Label_046D:
            if ((obj2.conceptCardIcon != null) == null)
            {
                goto Label_049B;
            }
            data = ConceptCardData.CreateConceptCardDataForDisplay(param3.iname);
            obj2.conceptCardIcon.Setup(data);
        Label_049B:
            if ((obj2.icon != null) == null)
            {
                goto Label_04B9;
            }
            obj2.icon.UpdateValue();
        Label_04B9:
            return;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }

        [Serializable]
        public class ButtonObject
        {
            public Button button;
            public Text title;
            public Text reward;
            public GameParameter icon;
            public ConceptCardIcon conceptCardIcon;

            public ButtonObject()
            {
                base..ctor();
                return;
            }
        }

        private enum State
        {
            Challenge,
            Clear,
            Ended
        }
    }
}

