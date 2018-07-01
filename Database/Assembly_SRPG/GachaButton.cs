// Decompiled with JetBrains decompiler
// Type: SRPG.GachaButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
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
    private int DisplayCostNum;
    private StateMachine<GachaButton> mState;
    private bool mUpdateTrigger;
    private GachaButton.GachaCostType mCostType;
    private GachaButton.GachaCategoryType mCategoryType;
    private int mCost;
    private int mStepIndex;
    private int mStepMax;
    private int mTicketNum;
    private string mButtonText;
    private string mCategory;
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
    private bool mStarted;
    private GameManager gm;
    private Coroutine mUpdateCoroutine;
    private float mNextUpdateTime;

    public GachaButton()
    {
      base.\u002Ector();
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
      }
    }

    public bool IsUpdateTrigger
    {
      get
      {
        return this.mUpdateTrigger;
      }
    }

    private bool IsFreeCoin()
    {
      if (this.Category == "coin_1")
        return this.gm.Player.CheckFreeGachaCoin();
      return false;
    }

    private bool IsFreeGold()
    {
      if (this.Category == "gold_1" && this.gm.Player.CheckFreeGachaGold())
        return !this.gm.Player.CheckFreeGachaGoldMax();
      return false;
    }

    public GachaButton.GachaCostType CostType
    {
      get
      {
        return this.mCostType;
      }
      set
      {
        this.mCostType = value;
      }
    }

    public bool IsSetCostType
    {
      get
      {
        if (this.mCostType != GachaButton.GachaCostType.NONE)
          return this.mCostType != GachaButton.GachaCostType.ALL;
        return false;
      }
    }

    public GachaButton.GachaCategoryType CategoryType
    {
      get
      {
        return this.mCategoryType;
      }
      set
      {
        this.mCategoryType = value;
      }
    }

    public bool IsSetCategoryType
    {
      get
      {
        if (this.mCategoryType != GachaButton.GachaCategoryType.NONE)
          return this.mCategoryType != GachaButton.GachaCategoryType.ALL;
        return false;
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
      }
    }

    private void Start()
    {
      this.gm = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.optionLayout, (UnityEngine.Object) null))
        this.optionLayout.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.optionLayout2, (UnityEngine.Object) null))
        this.optionLayout2.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.paidOption, (UnityEngine.Object) null))
        this.paidOption.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.timerOption, (UnityEngine.Object) null))
        this.timerOption.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.haveOption, (UnityEngine.Object) null))
        this.haveOption.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.stepupOption, (UnityEngine.Object) null))
        this.stepupOption.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GachaBtn, (UnityEngine.Object) null))
        this.GachaBtn.SetActive(false);
      this.mState = new StateMachine<GachaButton>(this);
      this.mState.GotoState<GachaButton.State_Init>();
    }

    private void Update()
    {
      if (this.mState == null)
        return;
      this.mState.Update();
    }

    private void OnEnable()
    {
      if (this.mUpdateCoroutine != null)
      {
        this.StopCoroutine(this.mUpdateCoroutine);
        this.mUpdateCoroutine = (Coroutine) null;
      }
      if (!this.mStarted)
        return;
      this.RefreshTimerOption();
    }

    private bool IsObjectActivated(GameObject gobj)
    {
      return !UnityEngine.Object.op_Equality((UnityEngine.Object) gobj, (UnityEngine.Object) null) && gobj.get_activeInHierarchy();
    }

    public bool SetGachaButtonEvent(UnityAction action)
    {
      if (action != null)
      {
        Button component = (Button) this.GachaBtn.GetComponent<Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          ((UnityEventBase) component.get_onClick()).RemoveAllListeners();
          ((UnityEvent) component.get_onClick()).AddListener(action);
          return true;
        }
      }
      return false;
    }

    public bool SetGachaButtonUIParameter(GachaButton.GachaCostType cost_type = GachaButton.GachaCostType.NONE, GachaButton.GachaCategoryType category_type = GachaButton.GachaCategoryType.NONE)
    {
      if (cost_type == GachaButton.GachaCostType.NONE || cost_type == GachaButton.GachaCostType.ALL || (category_type == GachaButton.GachaCategoryType.NONE || category_type == GachaButton.GachaCategoryType.ALL))
        return false;
      this.mCostType = cost_type;
      this.mCategoryType = category_type;
      this.mUpdateTrigger = true;
      return true;
    }

    public bool UpdateUI()
    {
      this.UpdateCostBG(this.mCostType);
      this.UpdateOptionObject(this.mCategoryType, this.mCostType);
      this.UpdateButtonImage(this.mCostType);
      this.UpdateButtonText(this.mButtonText);
      this.UpdateCostNumber(this.mCost);
      this.UpdatePaidCost();
      this.GachaBtn.SetActive(true);
      ((Component) this).get_gameObject().SetActive(true);
      this.mStarted = true;
      this.RefreshTimerOption();
      return true;
    }

    private bool UpdateCostBG(GachaButton.GachaCostType cost = GachaButton.GachaCostType.NONE)
    {
      if (cost == GachaButton.GachaCostType.NONE || cost == GachaButton.GachaCostType.ALL || UnityEngine.Object.op_Equality((UnityEngine.Object) this.CostBG, (UnityEngine.Object) null))
        return false;
      Transform parent = this.costBG.get_transform().get_parent();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) parent, (UnityEngine.Object) null))
        return false;
      if (cost == GachaButton.GachaCostType.TICKET)
        ((Component) parent).get_gameObject().SetActive(false);
      else
        ((Component) parent).get_gameObject().SetActive(true);
      ((ImageArray) this.CostBG.GetComponent<ImageArray>()).ImageIndex = cost == GachaButton.GachaCostType.COIN || cost == GachaButton.GachaCostType.COIN_P ? 0 : 1;
      return true;
    }

    private bool UpdateOptionObject(GachaButton.GachaCategoryType category = GachaButton.GachaCategoryType.NONE, GachaButton.GachaCostType cost = GachaButton.GachaCostType.NONE)
    {
      if (category == GachaButton.GachaCategoryType.NONE || category == GachaButton.GachaCategoryType.ALL || (cost == GachaButton.GachaCostType.NONE || cost == GachaButton.GachaCostType.ALL))
        return false;
      this.optionLayout.SetActive(false);
      this.optionLayout2.SetActive(false);
      this.paidOption.SetActive(false);
      this.timerOption.SetActive(false);
      this.stepupOption.SetActive(false);
      if (cost == GachaButton.GachaCostType.COIN_P)
      {
        this.optionLayout2.SetActive(true);
        this.paidOption.SetActive(true);
      }
      if (cost == GachaButton.GachaCostType.TICKET)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TicketState, (UnityEngine.Object) null))
        {
          Text component = (Text) this.TicketState.GetComponent<Text>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            component.set_text(this.TicketNum.ToString());
            this.optionLayout.SetActive(true);
          }
        }
      }
      else if (category == GachaButton.GachaCategoryType.STEPUP)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StepupState, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentStepup10, (UnityEngine.Object) null))
          {
            Transform child = this.StepupState.get_transform().FindChild("current_10");
            this.mCurrentStepup10 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null) ? (GameObject) null : ((Component) child).get_gameObject();
          }
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentStepup1, (UnityEngine.Object) null))
          {
            Transform child = this.StepupState.get_transform().FindChild("current_1");
            this.mCurrentStepup1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null) ? (GameObject) null : ((Component) child).get_gameObject();
          }
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMaxStepup10, (UnityEngine.Object) null))
          {
            Transform child = this.StepupState.get_transform().FindChild("max_10");
            this.mMaxStepup10 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null) ? (GameObject) null : ((Component) child).get_gameObject();
          }
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMaxStepup1, (UnityEngine.Object) null))
          {
            Transform child = this.StepupState.get_transform().FindChild("max_1");
            this.mMaxStepup1 = !UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null) ? (GameObject) null : ((Component) child).get_gameObject();
          }
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentStepup1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentStepup10, (UnityEngine.Object) null) || (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMaxStepup1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mMaxStepup10, (UnityEngine.Object) null)))
            return false;
          int num1 = (this.StepIndex + 1) % 10;
          int num2 = (this.StepIndex + 1) / 10;
          int num3 = this.StepMax % 10;
          int num4 = this.StepMax / 10;
          ((ImageArray) this.mCurrentStepup1.GetComponent<ImageArray>()).ImageIndex = num1;
          ((ImageArray) this.mCurrentStepup10.GetComponent<ImageArray>()).ImageIndex = num2;
          ((ImageArray) this.mMaxStepup10.GetComponent<ImageArray>()).ImageIndex = num4;
          ((ImageArray) this.mMaxStepup1.GetComponent<ImageArray>()).ImageIndex = num3;
          this.mCurrentStepup10.SetActive(num2 > 0);
          this.mMaxStepup10.SetActive(num4 > 0);
          this.optionLayout.SetActive(true);
          this.stepupOption.SetActive(true);
        }
      }
      else if ((this.Category == "coin_1" || this.Category == "gold_1") && (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TimerObject, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LimitObject, (UnityEngine.Object) null)))
      {
        bool flag = true;
        if (this.mCostType == GachaButton.GachaCostType.FREE_COIN || this.mCostType == GachaButton.GachaCostType.COIN)
        {
          this.UpdateTimer(this.gm.Player.GetNextFreeGachaCoinCoolDownSec());
          this.UpdateLimit(1 - this.gm.Player.FreeGachaCoin.num);
          flag = this.gm.Player.CheckFreeGachaCoin();
        }
        else if (this.mCostType == GachaButton.GachaCostType.FREE_GOLD || this.mCostType == GachaButton.GachaCostType.GOLD)
        {
          this.UpdateTimer(this.gm.Player.GetNextFreeGachaGoldCoolDownSec());
          this.UpdateLimit((int) this.gm.MasterParam.FixParam.FreeGachaGoldMax - this.gm.Player.FreeGachaGold.num);
          flag = this.gm.Player.CheckFreeGachaGold();
        }
        this.TimerObject.SetActive(!flag);
        this.LimitObject.SetActive(flag);
        this.optionLayout.SetActive(true);
        this.timerOption.SetActive(true);
      }
      return true;
    }

    private bool UpdateTimer(long sec = 0)
    {
      int num1 = (int) (sec / 3600L);
      int num2 = (int) (sec % 3600L / 60L);
      int num3 = (int) (sec % 60L);
      int num4 = num1 / 10;
      int num5 = num1 % 10;
      int num6 = num2 / 10;
      int num7 = num2 % 10;
      int num8 = num3 / 10;
      int num9 = num3 % 10;
      this.TimerH10.ImageIndex = num4 >= 10 ? 0 : num4;
      this.TimerH1.ImageIndex = num5 >= 10 ? 0 : num5;
      this.TimerM10.ImageIndex = num6 >= 10 ? 0 : num6;
      this.TimerM1.ImageIndex = num7 >= 10 ? 0 : num7;
      this.TimerS10.ImageIndex = num8 >= 10 ? 0 : num8;
      this.TimerS1.ImageIndex = num9 >= 10 ? 0 : num9;
      return true;
    }

    private bool UpdateLimit(int num)
    {
      if (num <= 0)
      {
        this.Limit10.ImageIndex = 0;
        this.Limit1.ImageIndex = 0;
        return false;
      }
      int num1 = num / 10;
      int num2 = num % 10;
      this.Limit10.ImageIndex = num1 >= 10 ? 0 : num1;
      ((Component) this.Limit10).get_gameObject().SetActive(num1 > 0);
      this.Limit1.ImageIndex = num2 >= 10 ? 0 : num2;
      return true;
    }

    private bool UpdateButtonImage(GachaButton.GachaCostType cost = GachaButton.GachaCostType.NONE)
    {
      if (cost == GachaButton.GachaCostType.NONE || cost == GachaButton.GachaCostType.ALL)
        return false;
      ((ImageArray) this.gachaBtn.GetComponent<ImageArray>()).ImageIndex = cost != GachaButton.GachaCostType.COIN_P ? 0 : 1;
      return true;
    }

    private bool UpdateButtonText(string update_text = "")
    {
      if (!string.IsNullOrEmpty(update_text))
      {
        Transform transform = this.GachaBtn.get_transform().Find("text");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
        {
          LText component = (LText) ((Component) transform).GetComponent<LText>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            component.set_text(update_text);
            return true;
          }
        }
      }
      return false;
    }

    private bool UpdateCostNumber(int cost = 0)
    {
      Transform child1 = this.CostBG.get_transform().FindChild("num");
      Transform child2 = this.CostBG.get_transform().FindChild("num_free");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) child1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) child2, (UnityEngine.Object) null))
        return false;
      ((Component) child1).get_gameObject().SetActive(false);
      ((Component) child2).get_gameObject().SetActive(false);
      if (this.IsFreeCoin() || this.IsFreeGold())
        ((Component) child2).get_gameObject().SetActive(true);
      else
        ((Component) child1).get_gameObject().SetActive(true);
      if (cost <= 0)
        return false;
      this.RefreshCostNum(cost);
      return true;
    }

    private bool UpdatePaidCost()
    {
      if (this.CostType != GachaButton.GachaCostType.COIN_P)
        return false;
      this.RefreshPaidCost(this.gm.Player.PaidCoin);
      return true;
    }

    private bool RefreshCostNum(int cost = 0)
    {
      if (cost <= 0)
        return false;
      int num1 = (int) Math.Log10(cost <= 0 ? 1.0 : (double) cost) + 1;
      int num2 = cost;
      for (int displayCostNum = this.DisplayCostNum; displayCostNum > 0; --displayCostNum)
      {
        Transform child = this.CostBG.get_transform().FindChild("num/value_" + Mathf.Pow(10f, (float) (displayCostNum - 1)).ToString());
        if (num1 < displayCostNum)
        {
          ((Component) child).get_gameObject().SetActive(false);
        }
        else
        {
          int num3 = (int) Mathf.Pow(10f, (float) (displayCostNum - 1));
          int num4 = num2 / num3;
          ((Component) child).get_gameObject().SetActive(true);
          ((ImageArray) ((Component) child).GetComponent<ImageArray>()).ImageIndex = num4;
          num2 %= num3;
        }
      }
      return true;
    }

    private bool RefreshPaidCost(int cost = 0)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.PaidCoin, (UnityEngine.Object) null))
        return false;
      int childCount = this.PaidCoin.get_childCount();
      int num1 = (int) Math.Log10(cost <= 0 ? 1.0 : (double) cost) + 1;
      int num2 = cost;
      for (int index = childCount; index > 0; --index)
      {
        Transform child = this.PaidCoin.FindChild("coin" + Mathf.Pow(10f, (float) (index - 1)).ToString());
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) child, (UnityEngine.Object) null))
        {
          if (num1 < index)
          {
            ((Component) child).get_gameObject().SetActive(false);
          }
          else
          {
            int num3 = (int) Mathf.Pow(10f, (float) (index - 1));
            int num4 = num2 / num3;
            ((Component) child).get_gameObject().SetActive(true);
            ((ImageArray) ((Component) child).GetComponent<ImageArray>()).ImageIndex = num4;
            num2 %= num3;
          }
        }
      }
      return true;
    }

    [DebuggerHidden]
    private IEnumerator UpdateOption()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaButton.\u003CUpdateOption\u003Ec__IteratorF0() { \u003C\u003Ef__this = this };
    }

    private void SetUpdateOption(float interval)
    {
      if (!((Component) this).get_gameObject().get_activeInHierarchy())
        return;
      if ((double) interval <= 0.0)
      {
        if (this.mUpdateCoroutine == null)
          return;
        this.StopCoroutine(this.mUpdateCoroutine);
      }
      else
      {
        this.mNextUpdateTime = Time.get_time() + interval;
        if (this.mUpdateCoroutine != null)
          return;
        this.mUpdateCoroutine = this.StartCoroutine(this.UpdateOption());
      }
    }

    private bool RefreshTimerOption()
    {
      if (this.mCategory != "coin_1" && this.mCategory != "gold_1")
      {
        this.TimerObject.SetActive(false);
        this.LimitObject.SetActive(false);
        if (this.mUpdateCoroutine != null)
        {
          this.StopCoroutine(this.mUpdateCoroutine);
          this.mUpdateCoroutine = (Coroutine) null;
        }
        return true;
      }
      bool flag = false;
      if (this.mCategory == "coin_1")
      {
        flag = this.gm.Player.CheckFreeGachaCoin();
        this.UpdateTimer(this.gm.Player.GetNextFreeGachaCoinCoolDownSec());
        this.UpdateLimit(!flag ? 0 : 1);
      }
      else if (this.mCategory == "gold_1")
      {
        flag = this.gm.Player.CheckFreeGachaGold();
        if (this.gm.Player.FreeGachaGold.num == (int) this.gm.MasterParam.FixParam.FreeGachaGoldMax)
          flag = true;
        this.UpdateTimer(this.gm.Player.GetNextFreeGachaGoldCoolDownSec());
        this.UpdateLimit((int) this.gm.MasterParam.FixParam.FreeGachaGoldMax - this.gm.Player.FreeGachaGold.num);
      }
      this.UpdateCostNumber(this.mCost);
      this.TimerObject.SetActive(!flag);
      this.LimitObject.SetActive(flag);
      this.SetUpdateOption(1f);
      return true;
    }

    public enum GachaCostType
    {
      NONE,
      COIN,
      COIN_P,
      GOLD,
      TICKET,
      FREE_COIN,
      FREE_GOLD,
      ALL,
    }

    public enum GachaNumType
    {
      NONE,
      SINGLE,
      MULTI,
      ALL,
    }

    public enum GachaCategoryType
    {
      NONE,
      RARE,
      NORMAL,
      STEPUP,
      ALL,
    }

    private class State_Init : State<GachaButton>
    {
      public override void Begin(GachaButton self)
      {
        base.Begin(self);
        self.mState.GotoState<GachaButton.State_Wait>();
      }
    }

    private class State_Wait : State<GachaButton>
    {
      public override void Begin(GachaButton self)
      {
        base.Begin(self);
      }

      public override void Update(GachaButton self)
      {
        base.Update(self);
        if (!self.UpdateTrigger)
          return;
        self.mState.GotoState<GachaButton.State_UpdateUI>();
      }
    }

    private class State_UpdateUI : State<GachaButton>
    {
      public override void Begin(GachaButton self)
      {
        base.Begin(self);
        self.UpdateUI();
        self.mUpdateTrigger = false;
        self.mState.GotoState<GachaButton.State_Wait>();
      }
    }
  }
}
