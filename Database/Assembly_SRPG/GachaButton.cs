namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class GachaButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject optionLayout2;
        [SerializeField]
        private GameObject optionLayout;
        [SerializeField]
        private GameObject paidOption;
        [SerializeField]
        private GameObject timerOption;
        [SerializeField]
        private GameObject timerObject;
        [SerializeField]
        private GameObject limitObject;
        [SerializeField]
        private GameObject haveOption;
        [SerializeField]
        private GameObject ticketState;
        [SerializeField]
        private GameObject stepupOption;
        [SerializeField]
        private GameObject stepupState;
        [SerializeField]
        private GameObject gachaBtn;
        [SerializeField]
        private GameObject costBG;
        private GameObject mCurrentStepup10;
        private GameObject mCurrentStepup1;
        private GameObject mMaxStepup10;
        private GameObject mMaxStepup1;
        public ImageArray Limit10;
        public ImageArray Limit1;
        public ImageArray TimerH10;
        public ImageArray TimerH1;
        public ImageArray TimerM10;
        public ImageArray TimerM1;
        public ImageArray TimerS10;
        public ImageArray TimerS1;
        public Transform PaidCoin;
        private List<ImageArray> mPaidCoinNums;
        [SerializeField]
        private GameObject AppealOption;
        private ImageArray AppealBgs;
        private Text AppealMessageText;
        private bool mStarted;
        private GameManager gm;
        private int DisplayCostNum;
        private StateMachine<GachaButton> mState;
        private bool mUpdateTrigger;
        private GachaCategoryType mCategoryType;
        private GachaCostType mCostType;
        private int mCost;
        private int mStepIndex;
        private int mStepMax;
        private int mTicketNum;
        private string mButtonText;
        private string mCategory;
        private Coroutine mUpdateCoroutine;
        private float mNextUpdateTime;
        private GachaButtonParam m_GachaButtonParam;

        public GachaButton()
        {
            this.DisplayCostNum = 7;
            this.mButtonText = string.Empty;
            this.mCategory = string.Empty;
            base..ctor();
            return;
        }

        private bool IsFreeCoin()
        {
            return (((this.Category == "coin_1") == null) ? 0 : this.gm.Player.CheckFreeGachaCoin());
        }

        private bool IsFreeGold()
        {
            return ((((this.Category == "gold_1") == null) || (this.gm.Player.CheckFreeGachaGold() == null)) ? 0 : (this.gm.Player.CheckFreeGachaGoldMax() == 0));
        }

        private bool IsObjectActivated(GameObject gobj)
        {
        Label_0017:
            return ((((gobj == null) == null) && (gobj.get_activeInHierarchy() != null)) ? 1 : 0);
        }

        private void OnEnable()
        {
            if (this.mUpdateCoroutine == null)
            {
                goto Label_001E;
            }
            base.StopCoroutine(this.mUpdateCoroutine);
            this.mUpdateCoroutine = null;
        Label_001E:
            if (this.mStarted == null)
            {
                goto Label_0030;
            }
            this.RefreshTimerOption();
        Label_0030:
            return;
        }

        private unsafe bool RefreshCostNum(int cost)
        {
            int num;
            int num2;
            int num3;
            string str;
            Transform transform;
            int num4;
            int num5;
            float num6;
            if (cost > 0)
            {
                goto Label_0009;
            }
            return 0;
        Label_0009:
            num = ((int) Math.Log10((double) ((cost <= 0) ? 1 : cost))) + 1;
            num2 = cost;
            num3 = this.DisplayCostNum;
            goto Label_00B8;
        Label_002F:
            str = "num/value_" + &Mathf.Pow(10f, (float) (num3 - 1)).ToString();
            transform = this.CostBG.get_transform().FindChild(str);
            if (num >= num3)
            {
                goto Label_007D;
            }
            transform.get_gameObject().SetActive(0);
            goto Label_00B4;
        Label_007D:
            num4 = (int) Mathf.Pow(10f, (float) (num3 - 1));
            num5 = num2 / num4;
            transform.get_gameObject().SetActive(1);
            transform.GetComponent<ImageArray>().ImageIndex = num5;
            num2 = num2 % num4;
        Label_00B4:
            num3 -= 1;
        Label_00B8:
            if (num3 > 0)
            {
                goto Label_002F;
            }
            return 1;
        }

        private unsafe bool RefreshPaidCost(int cost)
        {
            int num;
            int num2;
            int num3;
            int num4;
            string str;
            Transform transform;
            int num5;
            int num6;
            float num7;
            if ((this.PaidCoin == null) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            num = this.PaidCoin.get_childCount();
            num2 = ((int) Math.Log10((double) ((cost <= 0) ? 1 : cost))) + 1;
            num3 = cost;
            num4 = num;
            goto Label_00D8;
        Label_0040:
            str = "coin" + &Mathf.Pow(10f, (float) (num4 - 1)).ToString();
            transform = this.PaidCoin.FindChild(str);
            if ((transform == null) == null)
            {
                goto Label_0084;
            }
            goto Label_00D4;
        Label_0084:
            if (num2 >= num4)
            {
                goto Label_009D;
            }
            transform.get_gameObject().SetActive(0);
            goto Label_00D4;
        Label_009D:
            num5 = (int) Mathf.Pow(10f, (float) (num4 - 1));
            num6 = num3 / num5;
            transform.get_gameObject().SetActive(1);
            transform.GetComponent<ImageArray>().ImageIndex = num6;
            num3 = num3 % num5;
        Label_00D4:
            num4 -= 1;
        Label_00D8:
            if (num4 > 0)
            {
                goto Label_0040;
            }
            return 1;
        }

        private bool RefreshTimerOption()
        {
            bool flag;
            long num;
            int num2;
            if (this.m_GachaButtonParam.Category == null)
            {
                goto Label_0020;
            }
            if (this.m_GachaButtonParam.IsNoUseFree == null)
            {
                goto Label_0058;
            }
        Label_0020:
            this.TimerObject.SetActive(0);
            this.LimitObject.SetActive(0);
            if (this.mUpdateCoroutine == null)
            {
                goto Label_0056;
            }
            base.StopCoroutine(this.mUpdateCoroutine);
            this.mUpdateCoroutine = null;
        Label_0056:
            return 1;
        Label_0058:
            flag = 1;
            num = 0L;
            num2 = 0;
            if (this.m_GachaButtonParam.Category != 1)
            {
                goto Label_00AF;
            }
            num = this.gm.Player.GetNextFreeGachaCoinCoolDownSec();
            num2 = 1 - this.gm.Player.FreeGachaCoin.num;
            flag = this.gm.Player.CheckFreeGachaCoin();
            goto Label_0113;
        Label_00AF:
            if (this.m_GachaButtonParam.Category != 2)
            {
                goto Label_0113;
            }
            num = this.gm.Player.GetNextFreeGachaGoldCoolDownSec();
            num2 = this.gm.MasterParam.FixParam.FreeGachaGoldMax - this.gm.Player.FreeGachaGold.num;
            flag = this.gm.Player.CheckFreeGachaGold();
        Label_0113:
            this.UpdateCostNumber(this.m_GachaButtonParam.Cost, this.m_GachaButtonParam.ExecNum, this.m_GachaButtonParam.CostType, this.m_GachaButtonParam.Category, this.m_GachaButtonParam.IsNoUseFree);
            this.UpdateTimer(num);
            this.UpdateLimit(num2);
            this.TimerObject.SetActive(flag == 0);
            this.LimitObject.SetActive(flag);
            this.SetUpdateOption(1f);
            return 1;
        }

        public bool SetGachaButtonEvent(UnityAction action)
        {
            Button button;
            if (action == null)
            {
                goto Label_0037;
            }
            button = this.GachaBtn.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0037;
            }
            button.get_onClick().RemoveAllListeners();
            button.get_onClick().AddListener(action);
            return 1;
        Label_0037:
            return 0;
        }

        public bool SetGachaButtonUIParameter(GachaCostType cost_type, GachaCategoryType category_type)
        {
            if (cost_type == null)
            {
                goto Label_0031;
            }
            if (cost_type == 7)
            {
                goto Label_0031;
            }
            if (category_type == null)
            {
                goto Label_0031;
            }
            if (category_type == 4)
            {
                goto Label_0031;
            }
            this.mCostType = cost_type;
            this.mCategoryType = category_type;
            this.mUpdateTrigger = 1;
            return 1;
        Label_0031:
            return 0;
        }

        private void SetUpdateOption(float interval)
        {
            if (base.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if (interval > 0f)
            {
                goto Label_0034;
            }
            if (this.mUpdateCoroutine == null)
            {
                goto Label_0033;
            }
            base.StopCoroutine(this.mUpdateCoroutine);
        Label_0033:
            return;
        Label_0034:
            this.mNextUpdateTime = Time.get_time() + interval;
            if (this.mUpdateCoroutine == null)
            {
                goto Label_004D;
            }
            return;
        Label_004D:
            this.mUpdateCoroutine = base.StartCoroutine(this.UpdateOption());
            return;
        }

        public bool SetupGachaButtonParam(GachaButtonParam _param)
        {
            if (_param != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.m_GachaButtonParam = _param;
            return 1;
        }

        private void Start()
        {
            this.gm = MonoSingleton<GameManager>.Instance;
            if ((this.optionLayout != null) == null)
            {
                goto Label_0028;
            }
            this.optionLayout.SetActive(0);
        Label_0028:
            if ((this.optionLayout2 != null) == null)
            {
                goto Label_0045;
            }
            this.optionLayout2.SetActive(0);
        Label_0045:
            if ((this.paidOption != null) == null)
            {
                goto Label_0062;
            }
            this.paidOption.SetActive(0);
        Label_0062:
            if ((this.timerOption != null) == null)
            {
                goto Label_007F;
            }
            this.timerOption.SetActive(0);
        Label_007F:
            if ((this.haveOption != null) == null)
            {
                goto Label_009C;
            }
            this.haveOption.SetActive(0);
        Label_009C:
            if ((this.stepupOption != null) == null)
            {
                goto Label_00B9;
            }
            this.stepupOption.SetActive(0);
        Label_00B9:
            if ((this.GachaBtn != null) == null)
            {
                goto Label_00D6;
            }
            this.GachaBtn.SetActive(0);
        Label_00D6:
            this.mState = new StateMachine<GachaButton>(this);
            this.mState.GotoState<State_Init>();
            return;
        }

        private void Update()
        {
            if (this.mState == null)
            {
                goto Label_0016;
            }
            this.mState.Update();
        Label_0016:
            return;
        }

        private bool UpdateAppeal(string message, int _appeal_type)
        {
            Text text;
            ImageArray array;
            if ((this.AppealMessageText == null) == null)
            {
                goto Label_003C;
            }
            text = this.AppealOption.GetComponentInChildren<Text>();
            if ((text == null) == null)
            {
                goto Label_0035;
            }
            DebugUtility.LogError("訴求文言を表示するオブジェクトがありません");
            return 0;
        Label_0035:
            this.AppealMessageText = text;
        Label_003C:
            if ((this.AppealBgs == null) == null)
            {
                goto Label_0078;
            }
            array = this.AppealOption.GetComponentInChildren<ImageArray>();
            if ((array == null) == null)
            {
                goto Label_0071;
            }
            DebugUtility.LogError("訴求吹き出し画像を表示するオブジェクトがありません");
            return 0;
        Label_0071:
            this.AppealBgs = array;
        Label_0078:
            if (_appeal_type >= ((int) this.AppealBgs.Images.Length))
            {
                goto Label_0097;
            }
            this.AppealBgs.ImageIndex = _appeal_type;
        Label_0097:
            this.AppealMessageText.set_text(message);
            return 1;
        }

        private bool UpdateButtonImage(GachaCostType cost)
        {
            ImageArray array;
            if ((cost != null) && (cost != 7))
            {
                goto Label_000F;
            }
            return 0;
        Label_000F:
            this.gachaBtn.GetComponent<ImageArray>().ImageIndex = (cost != 2) ? 0 : 1;
            return 1;
        }

        private bool UpdateButtonText(string _button_text)
        {
            Text text;
            if (string.IsNullOrEmpty(_button_text) == null)
            {
                goto Label_0017;
            }
            DebugUtility.LogError("ボタンテキストが指定されていません");
            return 0;
        Label_0017:
            if ((this.gachaBtn == null) == null)
            {
                goto Label_0034;
            }
            DebugUtility.LogError("表示するボタンの指定がありません");
            return 0;
        Label_0034:
            text = this.gachaBtn.GetComponentInChildren<Text>();
            if ((text == null) == null)
            {
                goto Label_0058;
            }
            DebugUtility.LogError("ボタンテキストを表示するTextコンポーネントがありません");
            return 0;
        Label_0058:
            text.set_text(_button_text);
            return 1;
        }

        private bool UpdateCostBG(GachaCostType _cost_type)
        {
            ImageArray array;
            if ((_cost_type != null) && (_cost_type != 7))
            {
                goto Label_0019;
            }
            DebugUtility.LogError("コストタイプが指定されていません");
            return 0;
        Label_0019:
            if ((this.costBG == null) == null)
            {
                goto Label_0036;
            }
            DebugUtility.LogError("コストアイコンを表示するオブジェクトが指定されていません");
            return 0;
        Label_0036:
            if (_cost_type != 4)
            {
                goto Label_005A;
            }
            this.costBG.get_transform().get_parent().get_gameObject().SetActive(0);
            return 1;
        Label_005A:
            this.costBG.get_transform().get_parent().get_gameObject().SetActive(1);
        Label_0090:
            this.costBG.GetComponent<ImageArray>().ImageIndex = ((_cost_type != 1) && (_cost_type != 2)) ? 1 : 0;
            return 1;
        }

        private bool UpdateCostNumber(int _cost, int _exec_num, GachaCostType _cost_type, GachaCategory _gacha_category, bool _is_nouse_free)
        {
            Transform transform;
            Transform transform2;
            bool flag;
            transform = this.costBG.get_transform().FindChild("num");
            transform2 = this.costBG.get_transform().FindChild("num_free");
            if (((transform == null) == null) && ((transform2 == null) == null))
            {
                goto Label_0050;
            }
            DebugUtility.LogError("消費コストを表示するオブジェクトが存在しません");
            return 0;
        Label_0050:
            transform.get_gameObject().SetActive(0);
            transform2.get_gameObject().SetActive(0);
            flag = 0;
            if (_gacha_category == null)
            {
                goto Label_00CD;
            }
            if (_gacha_category != 1)
            {
                goto Label_00AA;
            }
            flag = (((_exec_num != 1) || (_cost_type != 1)) || (_is_nouse_free != null)) ? 0 : this.gm.Player.CheckFreeGachaCoin();
            goto Label_00C8;
        Label_00AA:
            flag = (_exec_num != 1) ? 0 : this.gm.Player.CheckFreeGachaGold();
        Label_00C8:
            goto Label_00D5;
        Label_00CD:
            if (_cost != null)
            {
                goto Label_00D5;
            }
            flag = 1;
        Label_00D5:
            transform2.get_gameObject().SetActive(flag);
            transform.get_gameObject().SetActive(flag == 0);
            this.RefreshCostNum(_cost);
            return 1;
        }

        private bool UpdateLimit(int num)
        {
            int num2;
            int num3;
            if (num > 0)
            {
                goto Label_0021;
            }
            this.Limit10.ImageIndex = 0;
            this.Limit1.ImageIndex = 0;
            return 0;
        Label_0021:
            num2 = num / 10;
            num3 = num % 10;
            this.Limit10.ImageIndex = (num2 >= 10) ? 0 : num2;
            this.Limit10.get_gameObject().SetActive(num2 > 0);
            this.Limit1.ImageIndex = (num3 >= 10) ? 0 : num3;
            return 1;
        }

        [DebuggerHidden]
        private IEnumerator UpdateOption()
        {
            <UpdateOption>c__Iterator10C iteratorc;
            iteratorc = new <UpdateOption>c__Iterator10C();
            iteratorc.<>f__this = this;
            return iteratorc;
        }

        private bool UpdateOptionObject()
        {
            bool flag;
            long num;
            int num2;
            this.optionLayout.SetActive(0);
            this.optionLayout2.SetActive(0);
            this.paidOption.SetActive(0);
            this.timerOption.SetActive(0);
            this.stepupOption.SetActive(0);
            this.AppealOption.SetActive(0);
            if (this.m_GachaButtonParam.IsStepUp() == null)
            {
                goto Label_00A6;
            }
            if (this.m_GachaButtonParam.IsShowStepup == null)
            {
                goto Label_00A6;
            }
            if (this.UpdateStepup(this.m_GachaButtonParam.StepIndex, this.m_GachaButtonParam.StepMax) == null)
            {
                goto Label_021F;
            }
            this.optionLayout.SetActive(1);
            this.stepupOption.SetActive(1);
            goto Label_021F;
        Label_00A6:
            if (this.m_GachaButtonParam.Category == null)
            {
                goto Label_01EB;
            }
            if (this.m_GachaButtonParam.ExecNum != 1)
            {
                goto Label_01EB;
            }
            if (this.m_GachaButtonParam.CostType == 2)
            {
                goto Label_01EB;
            }
            if (this.m_GachaButtonParam.IsNoUseFree != null)
            {
                goto Label_021F;
            }
            flag = 1;
            num = 0L;
            num2 = 0;
            if (this.m_GachaButtonParam.Category != 1)
            {
                goto Label_013F;
            }
            num = this.gm.Player.GetNextFreeGachaCoinCoolDownSec();
            num2 = 1 - this.gm.Player.FreeGachaCoin.num;
            flag = this.gm.Player.CheckFreeGachaCoin();
            goto Label_01A3;
        Label_013F:
            if (this.m_GachaButtonParam.Category != 2)
            {
                goto Label_01A3;
            }
            num = this.gm.Player.GetNextFreeGachaGoldCoolDownSec();
            num2 = this.gm.MasterParam.FixParam.FreeGachaGoldMax - this.gm.Player.FreeGachaGold.num;
            flag = this.gm.Player.CheckFreeGachaGold();
        Label_01A3:
            this.UpdateTimer(num);
            this.UpdateLimit(num2);
            this.TimerObject.SetActive(flag == 0);
            this.LimitObject.SetActive(flag);
            this.optionLayout.SetActive(1);
            this.timerOption.SetActive(1);
            goto Label_021F;
        Label_01EB:
            if (this.m_GachaButtonParam.CostType != 2)
            {
                goto Label_021F;
            }
            if (this.UpdatePaidCost() == null)
            {
                goto Label_021F;
            }
            this.optionLayout2.SetActive(1);
            this.paidOption.SetActive(1);
        Label_021F:
            if (string.IsNullOrEmpty(this.m_GachaButtonParam.AppealText) != null)
            {
                goto Label_0261;
            }
            if (this.UpdateAppeal(this.m_GachaButtonParam.AppealText, this.m_GachaButtonParam.AppealType) == null)
            {
                goto Label_0261;
            }
            this.AppealOption.SetActive(1);
        Label_0261:
            return 1;
        }

        private bool UpdatePaidCost()
        {
            this.RefreshPaidCost(this.gm.Player.PaidCoin);
            return 1;
        }

        private bool UpdateStepup(int _index, int _max)
        {
            Transform transform;
            Transform transform2;
            Transform transform3;
            Transform transform4;
            int num;
            int num2;
            int num3;
            int num4;
            if ((this.StepupState == null) == null)
            {
                goto Label_001D;
            }
            DebugUtility.LogError("ステップアップ表示用のRootオブジェクトの指定がありません");
            return 0;
        Label_001D:
            if ((this.mCurrentStepup10 == null) == null)
            {
                goto Label_0062;
            }
            transform = this.StepupState.get_transform().FindChild("current_10");
            this.mCurrentStepup10 = ((transform != null) == null) ? null : transform.get_gameObject();
        Label_0062:
            if ((this.mCurrentStepup1 == null) == null)
            {
                goto Label_00A7;
            }
            transform2 = this.StepupState.get_transform().FindChild("current_1");
            this.mCurrentStepup1 = ((transform2 != null) == null) ? null : transform2.get_gameObject();
        Label_00A7:
            if ((this.mMaxStepup10 == null) == null)
            {
                goto Label_00EC;
            }
            transform3 = this.StepupState.get_transform().FindChild("max_10");
            this.mMaxStepup10 = ((transform3 != null) == null) ? null : transform3.get_gameObject();
        Label_00EC:
            if ((this.mMaxStepup1 == null) == null)
            {
                goto Label_0131;
            }
            transform4 = this.StepupState.get_transform().FindChild("max_1");
            this.mMaxStepup1 = ((transform4 != null) == null) ? null : transform4.get_gameObject();
        Label_0131:
            if ((this.mCurrentStepup1 == null) != null)
            {
                goto Label_0175;
            }
            if ((this.mCurrentStepup10 == null) != null)
            {
                goto Label_0175;
            }
            if ((this.mMaxStepup1 == null) != null)
            {
                goto Label_0175;
            }
            if ((this.mMaxStepup10 == null) == null)
            {
                goto Label_0181;
            }
        Label_0175:
            DebugUtility.LogError("ステップアップ表示に必要なオブジェクトがありません");
            return 0;
        Label_0181:
            num = (_index + 1) % 10;
            num2 = (_index + 1) / 10;
            num3 = _max % 10;
            num4 = _max / 10;
            this.mCurrentStepup1.GetComponent<ImageArray>().ImageIndex = num;
            this.mCurrentStepup10.GetComponent<ImageArray>().ImageIndex = num2;
            this.mMaxStepup10.GetComponent<ImageArray>().ImageIndex = num4;
            this.mMaxStepup1.GetComponent<ImageArray>().ImageIndex = num3;
            this.mCurrentStepup10.SetActive(num2 > 0);
            this.mMaxStepup10.SetActive(num4 > 0);
            return 1;
        }

        private bool UpdateTimer(long sec)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            num = (int) (sec / 0xe10L);
            num2 = (int) ((sec % 0xe10L) / 60L);
            num3 = (int) (sec % 60L);
            num4 = num / 10;
            num5 = num % 10;
            num6 = num2 / 10;
            num7 = num2 % 10;
            num8 = num3 / 10;
            num9 = num3 % 10;
            this.TimerH10.ImageIndex = (num4 >= 10) ? 0 : num4;
            this.TimerH1.ImageIndex = (num5 >= 10) ? 0 : num5;
            this.TimerM10.ImageIndex = (num6 >= 10) ? 0 : num6;
            this.TimerM1.ImageIndex = (num7 >= 10) ? 0 : num7;
            this.TimerS10.ImageIndex = (num8 >= 10) ? 0 : num8;
            this.TimerS1.ImageIndex = (num9 >= 10) ? 0 : num9;
            return 1;
        }

        public bool UpdateUI()
        {
            object[] objArray1;
            string str;
            this.UpdateCostBG(this.m_GachaButtonParam.CostType);
            this.UpdateButtonImage(this.m_GachaButtonParam.CostType);
            str = this.m_GachaButtonParam.ButtonText;
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_005F;
            }
            objArray1 = new object[] { (int) this.m_GachaButtonParam.ExecNum };
            str = LocalizedText.Get("sys.BTN_MULTI_GACHA", objArray1);
        Label_005F:
            this.UpdateButtonText(str);
            this.UpdateCostNumber(this.m_GachaButtonParam.Cost, this.m_GachaButtonParam.ExecNum, this.m_GachaButtonParam.CostType, this.m_GachaButtonParam.Category, this.m_GachaButtonParam.IsNoUseFree);
            this.UpdateOptionObject();
            this.gachaBtn.SetActive(1);
            base.get_gameObject().SetActive(1);
            this.mStarted = 1;
            this.RefreshTimerOption();
            return 1;
        }

        public GameObject OptionLayout2
        {
            get
            {
                return this.optionLayout2;
            }
        }

        public GameObject OptionLayout
        {
            get
            {
                return this.optionLayout;
            }
        }

        public GameObject PaidOption
        {
            get
            {
                return this.paidOption;
            }
        }

        public GameObject TimerOption
        {
            get
            {
                return this.timerOption;
            }
        }

        public GameObject TimerObject
        {
            get
            {
                return this.timerObject;
            }
        }

        public GameObject LimitObject
        {
            get
            {
                return this.limitObject;
            }
        }

        public GameObject HaveOption
        {
            get
            {
                return this.haveOption;
            }
        }

        public GameObject TicketState
        {
            get
            {
                return this.ticketState;
            }
        }

        public GameObject StepupOption
        {
            get
            {
                return this.stepupOption;
            }
        }

        public GameObject StepupState
        {
            get
            {
                return this.stepupState;
            }
        }

        public GameObject GachaBtn
        {
            get
            {
                return this.gachaBtn;
            }
        }

        public GameObject CostBG
        {
            get
            {
                return this.costBG;
            }
        }

        public bool UpdateTrigger
        {
            get
            {
                return this.mUpdateTrigger;
            }
            set
            {
                this.mUpdateTrigger = value;
                return;
            }
        }

        public bool IsUpdateTrigger
        {
            get
            {
                return this.mUpdateTrigger;
            }
        }

        public GachaCostType CostType
        {
            get
            {
                return this.mCostType;
            }
            set
            {
                this.mCostType = value;
                return;
            }
        }

        public bool IsSetCostType
        {
            get
            {
                return ((this.mCostType == null) ? 0 : ((this.mCostType == 7) == 0));
            }
        }

        public GachaCategoryType CategoryType
        {
            get
            {
                return this.mCategoryType;
            }
            set
            {
                this.mCategoryType = value;
                return;
            }
        }

        public bool IsSetCategoryType
        {
            get
            {
                return ((this.mCategoryType == null) ? 0 : ((this.mCategoryType == 4) == 0));
            }
        }

        public int Cost
        {
            get
            {
                return this.mCost;
            }
            set
            {
                this.mCost = value;
                return;
            }
        }

        public int StepIndex
        {
            get
            {
                return this.mStepIndex;
            }
            set
            {
                this.mStepIndex = value;
                return;
            }
        }

        public int StepMax
        {
            get
            {
                return this.mStepMax;
            }
            set
            {
                this.mStepMax = value;
                return;
            }
        }

        public int TicketNum
        {
            get
            {
                return this.mTicketNum;
            }
            set
            {
                this.mTicketNum = value;
                return;
            }
        }

        public string ButtonText
        {
            get
            {
                return this.mButtonText;
            }
            set
            {
                this.mButtonText = value;
                return;
            }
        }

        public string Category
        {
            get
            {
                return this.mCategory;
            }
            set
            {
                this.mCategory = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateOption>c__Iterator10C : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal GachaButton <>f__this;

            public <UpdateOption>c__Iterator10C()
            {
                base..ctor();
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
                        goto Label_0025;

                    case 1:
                        goto Label_003D;

                    case 2:
                        goto Label_0092;
                }
                goto Label_0099;
            Label_0025:
                goto Label_003D;
            Label_002A:
                this.$current = null;
                this.$PC = 1;
                goto Label_009B;
            Label_003D:
                if (Time.get_time() < this.<>f__this.mNextUpdateTime)
                {
                    goto Label_002A;
                }
                this.<>f__this.RefreshTimerOption();
                if (Time.get_time() < this.<>f__this.mNextUpdateTime)
                {
                    goto Label_003D;
                }
                this.<>f__this.mUpdateCoroutine = null;
                this.$current = null;
                this.$PC = 2;
                goto Label_009B;
            Label_0092:
                this.$PC = -1;
            Label_0099:
                return 0;
            Label_009B:
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

        public enum GachaCategoryType
        {
            NONE,
            RARE,
            NORMAL,
            STEPUP,
            ALL
        }

        public enum GachaNumType
        {
            NONE,
            SINGLE,
            MULTI,
            ALL
        }

        public enum GGachaCostType
        {
            NONE,
            COIN,
            COIN_P,
            GOLD,
            TICKET,
            FREE_COIN,
            FREE_GOLD,
            ALL
        }

        private class State_Init : State<GachaButton>
        {
            public State_Init()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaButton self)
            {
                base.Begin(self);
                self.mState.GotoState<GachaButton.State_Wait>();
                return;
            }
        }

        private class State_UpdateUI : State<GachaButton>
        {
            public State_UpdateUI()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaButton self)
            {
                base.Begin(self);
                self.UpdateUI();
                self.mUpdateTrigger = 0;
                self.mState.GotoState<GachaButton.State_Wait>();
                return;
            }
        }

        private class State_Wait : State<GachaButton>
        {
            public State_Wait()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaButton self)
            {
                base.Begin(self);
                return;
            }

            public override void Update(GachaButton self)
            {
                base.Update(self);
                if (self.UpdateTrigger == null)
                {
                    goto Label_001E;
                }
                self.mState.GotoState<GachaButton.State_UpdateUI>();
                return;
            Label_001E:
                return;
            }
        }
    }
}

