// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilityListItemEvents
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitAbilityListItemEvents : ListItemEvents
  {
    private UnitAbilityListItemEvents.ListItemTouchController mTouchController;
    public ListItemEvents.ListItemEvent OnRankUp;
    public ListItemEvents.ListItemEvent OnRankUpBtnPress;
    public ListItemEvents.ListItemEvent OnRankUpBtnUp;
    [HelpBox("アビリティをランクアップ可能であればこのゲームオブジェクトを選択可能にします。")]
    public Selectable RankupButton;
    public RectTransform AbilityPoint;
    public GameObject LabelLevel;
    public GameObject LabelLevelMax;
    private float mLastUpdateTime;

    public UnitAbilityListItemEvents.ListItemTouchController ItemTouchController
    {
      get
      {
        return this.mTouchController;
      }
    }

    private void Start()
    {
      this.UpdateItemStates();
      if (!Object.op_Inequality((Object) this.RankupButton, (Object) null))
        return;
      this.mTouchController = (UnitAbilityListItemEvents.ListItemTouchController) ((Component) this.RankupButton).get_gameObject().AddComponent<UnitAbilityListItemEvents.ListItemTouchController>();
      this.mTouchController.OnPointerDownFunc = new UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerDown(this.RankUpPress);
      this.mTouchController.OnPointerUpFunc = new UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerUp(this.RankUpUp);
      this.mTouchController.RankUpFunc = new UnitAbilityListItemEvents.ListItemTouchController.DelegateRankUp(this.RankUp);
    }

    private void OnEnable()
    {
      MonoSingleton<GameManager>.Instance.OnAbilityRankUpCountChange += new GameManager.RankUpCountChangeEvent(this.OnRankUpCountChange);
    }

    private void OnDisable()
    {
      if (!Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
        return;
      MonoSingleton<GameManager>.Instance.OnAbilityRankUpCountChange -= new GameManager.RankUpCountChangeEvent(this.OnRankUpCountChange);
    }

    private void OnRankUpCountChange(int count)
    {
      this.UpdateItemStates();
    }

    private void RankUpPress(UnitAbilityListItemEvents.ListItemTouchController controller)
    {
      if (this.OnRankUpBtnPress == null)
        return;
      this.OnRankUpBtnPress(((Component) this).get_gameObject());
    }

    private void RankUpUp(UnitAbilityListItemEvents.ListItemTouchController controller)
    {
      if (this.OnRankUpBtnUp == null)
        return;
      this.OnRankUpBtnUp(((Component) this).get_gameObject());
    }

    public void RankUp(UnitAbilityListItemEvents.ListItemTouchController controller)
    {
      if (this.OnRankUp == null)
        return;
      this.OnRankUp(((Component) this).get_gameObject());
    }

    private void UpdateItemStates()
    {
      AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(((Component) this).get_gameObject(), (AbilityData) null);
      if (dataOfClass == null)
        return;
      bool flag = dataOfClass.Rank >= dataOfClass.GetRankMaxCap();
      if (Object.op_Inequality((Object) this.LabelLevel, (Object) null))
        this.LabelLevel.SetActive(!flag);
      if (Object.op_Inequality((Object) this.LabelLevelMax, (Object) null))
        this.LabelLevelMax.SetActive(flag);
      if (Object.op_Inequality((Object) this.RankupButton, (Object) null))
      {
        ((Component) this.RankupButton).get_gameObject().SetActive(dataOfClass.Rank < dataOfClass.GetRankCap());
        this.RankupButton.set_interactable(true & MonoSingleton<GameManager>.Instance.Player.CheckRankUpAbility(dataOfClass));
      }
      if (!Object.op_Inequality((Object) this.AbilityPoint, (Object) null))
        return;
      ((Component) this.AbilityPoint).get_gameObject().SetActive(dataOfClass.Rank < dataOfClass.GetRankCap());
    }

    public class ListItemTouchController : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
    {
      private static readonly float FirstSpan = 0.3f;
      private static readonly float SecondSpanMax = 1f;
      private static readonly float SecondSpanMin = 0.2f;
      private static readonly float SecondSpanOffset = 0.1f;
      public UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerDown OnPointerDownFunc;
      public UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerUp OnPointerUpFunc;
      public UnitAbilityListItemEvents.ListItemTouchController.DelegateRankUp RankUpFunc;
      public float HoldDuration;
      public float HoldSpan;
      public bool Holding;
      public bool IsFirstDownFunc;
      private Vector2 mDragStartPos;

      public ListItemTouchController()
      {
        base.\u002Ector();
      }

      public void OnPointerDown(PointerEventData eventData)
      {
        if (this.OnPointerDownFunc == null)
          return;
        this.StatusReset();
        this.OnPointerDownFunc(this);
        this.Holding = true;
        this.mDragStartPos = eventData.get_position();
      }

      public void OnPointerUp()
      {
        if (this.OnPointerUpFunc != null)
          this.OnPointerUpFunc(this);
        this.StatusReset();
      }

      public void OnDestroy()
      {
        this.StatusReset();
        if (this.OnPointerDownFunc != null)
          this.OnPointerDownFunc = (UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerDown) null;
        if (this.OnPointerUpFunc == null)
          return;
        this.OnPointerUpFunc = (UnitAbilityListItemEvents.ListItemTouchController.DelegateOnPointerUp) null;
      }

      public void UpdatePress(float deltaTime)
      {
        if (this.Holding && !Input.GetMouseButton(0))
        {
          if ((double) this.HoldDuration < (double) this.HoldSpan)
            this.RankUpFunc(this);
          this.OnPointerUp();
        }
        else
        {
          if (!this.Holding)
            return;
          this.HoldDuration += deltaTime;
          if ((double) this.HoldDuration < (double) this.HoldSpan)
            return;
          this.HoldDuration -= this.HoldSpan;
          if (!this.IsFirstDownFunc)
          {
            this.IsFirstDownFunc = true;
            this.HoldSpan = UnitAbilityListItemEvents.ListItemTouchController.SecondSpanMax;
          }
          else
          {
            this.HoldSpan -= UnitAbilityListItemEvents.ListItemTouchController.SecondSpanOffset;
            this.HoldSpan = Mathf.Max(UnitAbilityListItemEvents.ListItemTouchController.SecondSpanMin, this.HoldSpan);
          }
          this.RankUpFunc(this);
        }
      }

      public void StatusReset()
      {
        this.HoldDuration = 0.0f;
        this.Holding = false;
        this.HoldSpan = UnitAbilityListItemEvents.ListItemTouchController.FirstSpan;
        this.IsFirstDownFunc = false;
        // ISSUE: explicit reference operation
        ((Vector2) @this.mDragStartPos).Set(0.0f, 0.0f);
      }

      public delegate void DelegateOnPointerDown(UnitAbilityListItemEvents.ListItemTouchController controller);

      public delegate void DelegateOnPointerUp(UnitAbilityListItemEvents.ListItemTouchController controller);

      public delegate void DelegateRankUp(UnitAbilityListItemEvents.ListItemTouchController controller);
    }
  }
}
