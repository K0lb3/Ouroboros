namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(5, "全ミッションコンプリート", 1, 5), Pin(0, "更新", 0, 0), Pin(1, "完了", 1, 3), Pin(4, "継続", 0, 1), Pin(3, "コンプリート報酬", 1, 2), Pin(2, "ミッション報酬", 1, 1)]
    public class ChallengeMissionReward : MonoBehaviour, IFlowInterface
    {
        public GameObject PanelNormal;
        public GameObject PanelComplete;
        public RawImage ImageItem;
        public RawImage ImageExp;
        public RawImage ImageGold;
        public RawImage ImageStamina;
        public ConceptCardIcon ConceptCardObject;
        public RawImage ImageReward;
        public Text TextMessage;
        private bool isAllMissionCompleteMessageShown;
        private TrophyParam mTrophy;

        public ChallengeMissionReward()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 4)
            {
                goto Label_0041;
            }
            if (this.isAllMissionCompleteMessageShown == null)
            {
                goto Label_0034;
            }
            if ((this.mTrophy.iname == "CHALLENGE_06") == null)
            {
                goto Label_0034;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 1);
            return;
        Label_0034:
            base.StartCoroutine(this.showRewardMessage());
        Label_0041:
            return;
        }

        private void Awake()
        {
            if ((this.PanelNormal != null) == null)
            {
                goto Label_001D;
            }
            this.PanelNormal.SetActive(0);
        Label_001D:
            if ((this.PanelComplete != null) == null)
            {
                goto Label_003A;
            }
            this.PanelComplete.SetActive(0);
        Label_003A:
            return;
        }

        private unsafe string GetAllRewardText(TrophyParam trophy)
        {
            object[] objArray2;
            object[] objArray1;
            StringBuilder builder;
            bool flag;
            TrophyParam.RewardItem item;
            TrophyParam.RewardItem[] itemArray;
            int num;
            ItemParam param;
            UnitParam param2;
            string str;
            TrophyParam.RewardItem item2;
            TrophyParam.RewardItem[] itemArray2;
            int num2;
            ConceptCardParam param3;
            builder = new StringBuilder();
            flag = 0;
            if (trophy.Items == null)
            {
                goto Label_0149;
            }
            if (((int) trophy.Items.Length) <= 0)
            {
                goto Label_0149;
            }
            itemArray = this.mTrophy.Items;
            num = 0;
            goto Label_013F;
        Label_0035:
            item = *(&(itemArray[num]));
            param = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(&item.iname);
            if (param == null)
            {
                goto Label_0139;
            }
            if (param.type != 1)
            {
                goto Label_009A;
            }
            builder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_PIECE"), param.name, (int) &item.Num));
            flag = 1;
            goto Label_0139;
        Label_009A:
            if (param.type != 0x10)
            {
                goto Label_0110;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetUnitParam(param.iname);
            if (param2 == null)
            {
                goto Label_0139;
            }
            objArray1 = new object[] { (int) (param2.rare + 1), param2.name };
            str = LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT", objArray1);
            objArray2 = new object[] { str };
            builder.AppendLine(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_GET", objArray2));
            goto Label_0139;
        Label_0110:
            builder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD"), param.name, (int) &item.Num));
        Label_0139:
            num += 1;
        Label_013F:
            if (num < ((int) itemArray.Length))
            {
                goto Label_0035;
            }
        Label_0149:
            if (trophy.Gold == null)
            {
                goto Label_017A;
            }
            builder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (int) trophy.Gold));
            goto Label_02A0;
        Label_017A:
            if (trophy.Exp == null)
            {
                goto Label_01AB;
            }
            builder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (int) trophy.Exp));
            goto Label_02A0;
        Label_01AB:
            if (trophy.Coin == null)
            {
                goto Label_01DC;
            }
            builder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) trophy.Coin));
            goto Label_02A0;
        Label_01DC:
            if (trophy.Stamina == null)
            {
                goto Label_020D;
            }
            builder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (int) trophy.Stamina));
            goto Label_02A0;
        Label_020D:
            if (trophy.ConceptCards == null)
            {
                goto Label_02A0;
            }
            itemArray2 = trophy.ConceptCards;
            num2 = 0;
            goto Label_0295;
        Label_0228:
            item2 = *(&(itemArray2[num2]));
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(&item2.iname);
            if (param3 != null)
            {
                goto Label_0266;
            }
            Debug.LogError("GetConceptCardParam == null");
            goto Label_028F;
        Label_0266:
            builder.AppendLine(string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_CONCEPT_CARD"), param3.name, (int) &item2.Num));
        Label_028F:
            num2 += 1;
        Label_0295:
            if (num2 < ((int) itemArray2.Length))
            {
                goto Label_0228;
            }
        Label_02A0:
            if (flag == null)
            {
                goto Label_02C3;
            }
            builder.AppendLine(string.Empty);
            builder.AppendLine(LocalizedText.Get("sys.CHALLENGE_REWARD_NOTE"));
        Label_02C3:
            return builder.ToString();
        }

        [DebuggerHidden]
        private IEnumerator showRewardMessage()
        {
            <showRewardMessage>c__IteratorEC rec;
            rec = new <showRewardMessage>c__IteratorEC();
            rec.<>f__this = this;
            return rec;
        }

        private void Start()
        {
            if ((this.TextMessage == null) == null)
            {
                goto Label_0019;
            }
            base.set_enabled(0);
            return;
        Label_0019:
            if (string.IsNullOrEmpty(GlobalVars.SelectedChallengeMissionTrophy) == null)
            {
                goto Label_0030;
            }
            base.set_enabled(0);
            return;
        Label_0030:
            this.mTrophy = ChallengeMission.GetTrophy(GlobalVars.SelectedChallengeMissionTrophy);
            if (this.mTrophy != null)
            {
                goto Label_0053;
            }
            base.set_enabled(0);
            return;
        Label_0053:
            if (this.mTrophy.IsChallengeMissionRoot == null)
            {
                goto Label_0087;
            }
            this.PanelNormal.SetActive(0);
            this.PanelComplete.SetActive(1);
            FlowNode_GameObject.ActivateOutputLinks(this, 3);
            goto Label_00A6;
        Label_0087:
            this.PanelNormal.SetActive(1);
            this.PanelComplete.SetActive(0);
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
        Label_00A6:
            this.UpdateReward(this.mTrophy);
            return;
        }

        private unsafe void UpdateReward(TrophyParam trophy)
        {
            object[] objArray1;
            string str;
            string str2;
            bool flag;
            bool flag2;
            bool flag3;
            bool flag4;
            bool flag5;
            string str3;
            ItemParam param;
            UnitParam param2;
            string str4;
            ConceptCardParam param3;
            string str5;
            ConceptCardData data;
            if (trophy != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            str = LocalizedText.Get("sys.CHALLENGE_MSG_REWARD_ITEM");
            str2 = LocalizedText.Get("sys.CHALLENGE_MSG_REWARD_OTHER");
            flag = 0;
            flag2 = 0;
            flag3 = 0;
            flag4 = 0;
            flag5 = 0;
            str3 = string.Empty;
            param = null;
            if (trophy.Gold == null)
            {
                goto Label_006D;
            }
            flag3 = 1;
            str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_GOLD"), (int) trophy.Gold);
            str3 = string.Format(str2, str3);
            goto Label_0297;
        Label_006D:
            if (trophy.Exp == null)
            {
                goto Label_00A5;
            }
            flag2 = 1;
            str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_EXP"), (int) trophy.Exp);
            str3 = string.Format(str2, str3);
            goto Label_0297;
        Label_00A5:
            if (trophy.Coin == null)
            {
                goto Label_00EE;
            }
            flag = 1;
            str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (int) trophy.Coin);
            str3 = string.Format(str2, str3);
            param = MonoSingleton<GameManager>.Instance.GetItemParam("$COIN");
            goto Label_0297;
        Label_00EE:
            if (trophy.Stamina == null)
            {
                goto Label_0127;
            }
            flag4 = 1;
            str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_STAMINA"), (int) trophy.Stamina);
            str3 = string.Format(str2, str3);
            goto Label_0297;
        Label_0127:
            if (trophy.Items == null)
            {
                goto Label_01F4;
            }
            if (((int) trophy.Items.Length) <= 0)
            {
                goto Label_01F4;
            }
            flag = 1;
            param = MonoSingleton<GameManager>.Instance.GetItemParam(&(trophy.Items[0]).iname);
            if (param == null)
            {
                goto Label_0297;
            }
            if (param.type != 0x10)
            {
                goto Label_01CA;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetUnitParam(param.iname);
            if (param2 == null)
            {
                goto Label_0297;
            }
            objArray1 = new object[] { (int) (param2.rare + 1), param2.name };
            str4 = LocalizedText.Get("sys.CHALLENGE_DETAIL_REWARD_UNIT_BR", objArray1);
            str3 = string.Format(str2, str4);
            goto Label_01EF;
        Label_01CA:
            str3 = string.Format(str, param.name, (int) &(trophy.Items[0]).Num);
        Label_01EF:
            goto Label_0297;
        Label_01F4:
            if (trophy.ConceptCards == null)
            {
                goto Label_0297;
            }
            if (((int) trophy.ConceptCards.Length) <= 0)
            {
                goto Label_0297;
            }
            flag5 = 1;
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(&(trophy.ConceptCards[0]).iname);
            if (param3 == null)
            {
                goto Label_0297;
            }
            str3 = string.Format(LocalizedText.Get("sys.CHALLENGE_MSG_REWARD_CONCEPT_CARD"), param3.name, (int) &(trophy.ConceptCards[0]).Num);
            if ((this.ConceptCardObject != null) == null)
            {
                goto Label_0297;
            }
            data = ConceptCardData.CreateConceptCardDataForDisplay(param3.iname);
            this.ConceptCardObject.Setup(data);
        Label_0297:
            if ((this.ImageItem != null) == null)
            {
                goto Label_02B9;
            }
            this.ImageItem.get_gameObject().SetActive(flag);
        Label_02B9:
            if ((this.ImageExp != null) == null)
            {
                goto Label_02DB;
            }
            this.ImageExp.get_gameObject().SetActive(flag2);
        Label_02DB:
            if ((this.ImageGold != null) == null)
            {
                goto Label_02FE;
            }
            this.ImageGold.get_gameObject().SetActive(flag3);
        Label_02FE:
            if ((this.ImageStamina != null) == null)
            {
                goto Label_0321;
            }
            this.ImageStamina.get_gameObject().SetActive(flag4);
        Label_0321:
            if ((this.ConceptCardObject != null) == null)
            {
                goto Label_0344;
            }
            this.ConceptCardObject.get_gameObject().SetActive(flag5);
        Label_0344:
            if (param == null)
            {
                goto Label_0358;
            }
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
        Label_0358:
            if ((this.TextMessage != null) == null)
            {
                goto Label_0376;
            }
            this.TextMessage.set_text(str3);
        Label_0376:
            return;
        }

        [CompilerGenerated]
        private sealed class <showRewardMessage>c__IteratorEC : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string <message>__0;
            internal bool <processing>__1;
            internal int $PC;
            internal object $current;
            internal ChallengeMissionReward <>f__this;

            public <showRewardMessage>c__IteratorEC()
            {
                base..ctor();
                return;
            }

            internal void <>m__2AC(GameObject go)
            {
                this.<processing>__1 = 0;
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0096;
                }
                goto Label_010A;
            Label_0021:
                if (this.<>f__this.mTrophy.IsChallengeMissionRoot == null)
                {
                    goto Label_00F7;
                }
                this.<message>__0 = this.<>f__this.GetAllRewardText(this.<>f__this.mTrophy);
                this.<processing>__1 = 1;
                UIUtility.SystemMessage(LocalizedText.Get("sys.CHALLENGE_REWARD_TITLE"), this.<message>__0, new UIUtility.DialogResultEvent(this.<>m__2AC), null, 0, -1);
                goto Label_0096;
            Label_0083:
                this.$current = null;
                this.$PC = 1;
                goto Label_010C;
            Label_0096:
                if (this.<processing>__1 != null)
                {
                    goto Label_0083;
                }
                if ((this.<>f__this.mTrophy.iname == "CHALLENGE_06") == null)
                {
                    goto Label_00F7;
                }
                this.<>f__this.TextMessage.set_text(LocalizedText.Get("sys.CHALLENGE_MSG_ALL_CLEAR"));
                this.<>f__this.isAllMissionCompleteMessageShown = 1;
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 5);
                goto Label_010A;
            Label_00F7:
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 1);
                this.$PC = -1;
            Label_010A:
                return 0;
            Label_010C:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }
    }
}

