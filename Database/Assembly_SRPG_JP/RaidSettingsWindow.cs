// Decompiled with JetBrains decompiler
// Type: SRPG.RaidSettingsWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Close", FlowNode.PinTypes.Output, 1)]
  public class RaidSettingsWindow : MonoBehaviour, IFlowInterface
  {
    public RaidSettingsWindow.RaidSettingsEvent OnAccept;
    public string DebugQuestID;
    public SRPG_Button AddButton;
    public SRPG_Button SubButton;
    public Slider Slider;
    public Text CountText;
    public Text APText;
    public Text CostText;
    public SRPG_Button OKButton;
    public GameObject Ticket;
    private QuestParam mQuest;
    private int mCount;
    private int mCountMax;
    private int mLimit;
    private bool mStarted;
    private int mLastTicketCount;
    private bool mInsideRefresh;

    public RaidSettingsWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
    }

    public void Setup(QuestParam quest, int count, int max)
    {
      this.mQuest = quest;
      this.mLimit = max;
      if (count >= 0)
        this.mCount = Mathf.Max(1, count);
      if (!this.mStarted)
        return;
      this.Refresh();
    }

    public int Count
    {
      get
      {
        return this.mCount;
      }
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.AddButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.AddButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnAddClick)));
      }
      if (Object.op_Inequality((Object) this.SubButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.SubButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnSubClick)));
      }
      if (Object.op_Inequality((Object) this.Slider, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<float>) this.Slider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnSliderChange)));
        this.Slider.set_minValue(1f);
      }
      if (Object.op_Inequality((Object) this.OKButton, (Object) null))
        this.OKButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnOKClick));
      MonoSingleton<GameManager>.Instance.OnStaminaChange += new GameManager.StaminaChangeEvent(this.OnPlayerStaminaChange);
      this.mStarted = true;
      if (this.mQuest == null)
        return;
      this.Refresh();
    }

    private void OnOKClick(SRPG_Button button)
    {
      if (this.OnAccept == null)
        return;
      this.OnAccept(this);
    }

    private void OnPlayerStaminaChange()
    {
      if (Object.op_Equality((Object) this, (Object) null))
        MonoSingleton<GameManager>.Instance.OnStaminaChange -= new GameManager.StaminaChangeEvent(this.OnPlayerStaminaChange);
      else
        this.CountChanged();
    }

    private int GetTicketNum()
    {
      if (this.mQuest != null && !string.IsNullOrEmpty(this.mQuest.ticket))
        return MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mQuest.ticket);
      return -1;
    }

    private void Update()
    {
      int ticketNum = this.GetTicketNum();
      if (this.mLastTicketCount == ticketNum)
        return;
      this.mLastTicketCount = ticketNum;
      this.Refresh();
    }

    private void OnAddClick()
    {
      if (this.mCount >= this.mCountMax)
        return;
      ++this.mCount;
      if (!Object.op_Inequality((Object) this.Slider, (Object) null))
        return;
      this.Slider.set_value((float) this.mCount);
    }

    private void OnSubClick()
    {
      if (this.mCount <= 1)
        return;
      --this.mCount;
      if (!Object.op_Inequality((Object) this.Slider, (Object) null))
        return;
      this.Slider.set_value((float) this.mCount);
    }

    private void OnSliderChange(float value)
    {
      if (this.mInsideRefresh)
        return;
      this.mCount = Mathf.Clamp(Mathf.FloorToInt(value), 1, this.mCountMax);
      this.CountChanged();
    }

    private void CountChanged()
    {
      if (Object.op_Inequality((Object) this.AddButton, (Object) null))
        ((Selectable) this.AddButton).set_interactable(this.mCount < this.mCountMax);
      if (Object.op_Inequality((Object) this.SubButton, (Object) null))
        ((Selectable) this.SubButton).set_interactable(this.mCount > 1);
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool flag = true;
      if (Object.op_Inequality((Object) this.APText, (Object) null))
        this.APText.set_text(player.Stamina.ToString());
      if (this.mQuest != null)
      {
        int num = this.mQuest.RequiredApWithPlayerLv(player.Lv, true) * this.mCount;
        flag &= player.Stamina >= num;
        if (Object.op_Inequality((Object) this.CostText, (Object) null))
        {
          this.CostText.set_text(num.ToString());
          Selectable component = (Selectable) ((Component) this.CostText).GetComponent<Selectable>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.set_interactable(flag);
        }
      }
      if (!Object.op_Inequality((Object) this.CountText, (Object) null))
        return;
      this.CountText.set_text(this.mCount.ToString());
      Selectable component1 = (Selectable) ((Component) this.CountText).GetComponent<Selectable>();
      if (!Object.op_Inequality((Object) component1, (Object) null))
        return;
      component1.set_interactable(flag);
    }

    public void Refresh()
    {
      if (this.mQuest == null || string.IsNullOrEmpty(this.mQuest.ticket))
        return;
      this.mInsideRefresh = true;
      ItemParam data = string.IsNullOrEmpty(this.mQuest.ticket) ? (ItemParam) null : MonoSingleton<GameManager>.Instance.GetItemParam(this.mQuest.ticket);
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(data);
      int num = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
      this.mLastTicketCount = num;
      this.mCountMax = Mathf.Min(num, this.mLimit);
      if (this.mQuest.GetChallangeLimit() > 0)
        this.mCountMax = Mathf.Min(this.mCountMax, this.mQuest.GetChallangeLimit() - this.mQuest.GetChallangeCount());
      this.mCount = Mathf.Min(this.mCount, this.mCountMax);
      if (Object.op_Inequality((Object) this.Ticket, (Object) null))
      {
        DataSource.Bind<ItemData>(this.Ticket, itemDataByItemParam);
        DataSource.Bind<ItemParam>(this.Ticket, data);
      }
      if (Object.op_Inequality((Object) this.Slider, (Object) null))
      {
        this.Slider.set_maxValue((float) this.mCountMax);
        if (Mathf.FloorToInt(this.Slider.get_value()) != this.mCount)
          this.Slider.set_value((float) this.mCount);
      }
      this.CountChanged();
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      this.mInsideRefresh = false;
    }

    public void Close()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    public delegate void RaidSettingsEvent(RaidSettingsWindow settings);
  }
}
