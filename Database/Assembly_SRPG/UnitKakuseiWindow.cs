namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(100, "ユニットが覚醒した", 1, 100), Pin(0, "表示を更新", 0, 0)]
    public class UnitKakuseiWindow : MonoBehaviour, IFlowInterface
    {
        public KakuseiWindowEvent OnKakuseiAccept;
        public UnitData Unit;
        public JobParam UnlockJobParam;
        public Button KakuseiButton;
        public Text KakeraMsg;
        public Text KakeraCharaMsg;
        public Text KakeraElementMsg;
        public Text KakeraCommonMsg;
        public GameObject JobUnlock;
        private UnitData mCurrentUnit;
        private ItemParam mElementKakera;
        private ItemParam mCommonKakera;

        public UnitKakuseiWindow()
        {
            base..ctor();
            return;
        }

        private void AcceptCommonKakera(GameObject go)
        {
            this.KakuseiAccept();
            return;
        }

        private void AcceptElementKakera(GameObject go)
        {
            string str;
            if (this.mCommonKakera == null)
            {
                goto Label_0040;
            }
            UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.KAKUSEI_CONFIRM_COMMON_KAKERA"), this.mCommonKakera.name), new UIUtility.DialogResultEvent(this.AcceptCommonKakera), null, null, 0, -1, null, null);
            return;
        Label_0040:
            this.AcceptCommonKakera(null);
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_000C;
            }
            this.Refresh();
        Label_000C:
            return;
        }

        private void KakuseiAccept()
        {
            if (this.OnKakuseiAccept == null)
            {
                goto Label_0017;
            }
            this.OnKakuseiAccept();
            return;
        Label_0017:
            MonoSingleton<GameManager>.Instance.Player.AwakingUnit(this.mCurrentUnit);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void OnKakuseiClick()
        {
            string str;
            if (this.mElementKakera == null)
            {
                goto Label_0040;
            }
            UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.KAKUSEI_CONFIRM_ELEMENT_KAKERA"), this.mElementKakera.name), new UIUtility.DialogResultEvent(this.AcceptElementKakera), null, null, 0, -1, null, null);
            return;
        Label_0040:
            this.AcceptElementKakera(null);
            return;
        }

        public void Refresh()
        {
            int num;
            int num2;
            ItemParam param;
            int num3;
            int num4;
            string str;
            int num5;
            int num6;
            int num7;
            string str2;
            ItemParam param2;
            int num8;
            int num9;
            string str3;
            ItemParam param3;
            int num10;
            int num11;
            string str4;
            bool flag;
            this.mCurrentUnit = (this.Unit == null) ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID) : this.Unit;
            if (this.mCurrentUnit != null)
            {
                goto Label_0041;
            }
            return;
        Label_0041:
            num = this.mCurrentUnit.AwakeLv;
            if (this.mCurrentUnit.GetAwakeLevelCap() > num)
            {
                goto Label_0061;
            }
            return;
        Label_0061:
            DataSource.Bind<UnitData>(base.get_gameObject(), this.mCurrentUnit);
            param = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentUnit.UnitParam.piece);
            num3 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentUnit.UnitParam.piece);
            num4 = this.mCurrentUnit.GetAwakeNeedPieces();
            if ((this.KakuseiButton != null) == null)
            {
                goto Label_00E1;
            }
            this.KakuseiButton.set_interactable(this.mCurrentUnit.CheckUnitAwaking());
        Label_00E1:
            if ((this.KakeraMsg != null) == null)
            {
                goto Label_0123;
            }
            str = LocalizedText.Get("sys.CONFIRM_KAKUSEI");
            this.KakeraMsg.set_text(string.Format(str, param.name, (int) num4, (int) num3));
        Label_0123:
            num5 = num4;
            num6 = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentUnit.UnitParam.piece);
            num7 = (num6 < num4) ? num6 : num4;
            if ((this.KakeraCharaMsg != null) == null)
            {
                goto Label_01CE;
            }
            if (num7 <= 0)
            {
                goto Label_01BD;
            }
            str2 = LocalizedText.Get("sys.KAKUSEI_KAKERA_CHARA");
            this.KakeraCharaMsg.set_text(string.Format(str2, param.name, (int) num7, (int) num6));
            this.KakeraCharaMsg.get_gameObject().SetActive(1);
            goto Label_01CE;
        Label_01BD:
            this.KakeraCharaMsg.get_gameObject().SetActive(0);
        Label_01CE:
            num5 = Mathf.Max(0, num5 - num7);
            param2 = this.mCurrentUnit.GetElementPieceParam();
            num8 = this.mCurrentUnit.GetElementPieces();
            num9 = (num8 < num5) ? num8 : num5;
            this.mElementKakera = null;
            if (((this.KakeraElementMsg != null) == null) || (param2 == null))
            {
                goto Label_0292;
            }
            if (num9 <= 0)
            {
                goto Label_0281;
            }
            str3 = LocalizedText.Get("sys.KAKUSEI_KAKERA_ELEMENT");
            this.KakeraElementMsg.set_text(string.Format(str3, param2.name, (int) num9, (int) num8));
            this.KakeraElementMsg.get_gameObject().SetActive(1);
            this.mElementKakera = param2;
            goto Label_0292;
        Label_0281:
            this.KakeraElementMsg.get_gameObject().SetActive(0);
        Label_0292:
            num5 = Mathf.Max(0, num5 - num9);
            param3 = this.mCurrentUnit.GetCommonPieceParam();
            num10 = this.mCurrentUnit.GetCommonPieces();
            num11 = (num10 < num5) ? num10 : num5;
            this.mCommonKakera = null;
            if ((this.KakeraCommonMsg != null) == null)
            {
                goto Label_0356;
            }
            if (param3 == null)
            {
                goto Label_0356;
            }
            if (num11 <= 0)
            {
                goto Label_0345;
            }
            str4 = LocalizedText.Get("sys.KAKUSEI_KAKERA_COMMON");
            this.KakeraCommonMsg.set_text(string.Format(str4, param3.name, (int) num11, (int) num10));
            this.KakeraCommonMsg.get_gameObject().SetActive(1);
            this.mCommonKakera = param3;
            goto Label_0356;
        Label_0345:
            this.KakeraCommonMsg.get_gameObject().SetActive(0);
        Label_0356:
            num5 = Mathf.Max(0, num5 - num11);
            if ((this.JobUnlock != null) == null)
            {
                goto Label_03A3;
            }
            flag = 0;
            if (this.UnlockJobParam == null)
            {
                goto Label_0396;
            }
            DataSource.Bind<JobParam>(this.JobUnlock, this.UnlockJobParam);
            flag = 1;
        Label_0396:
            this.JobUnlock.SetActive(flag);
        Label_03A3:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void Start()
        {
            this.Refresh();
            if ((this.KakuseiButton != null) == null)
            {
                goto Label_0033;
            }
            this.KakuseiButton.get_onClick().AddListener(new UnityAction(this, this.OnKakuseiClick));
        Label_0033:
            return;
        }

        public delegate void KakuseiWindowEvent();
    }
}

