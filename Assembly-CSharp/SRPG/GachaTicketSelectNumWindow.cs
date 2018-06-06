// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTicketSelectNumWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(101, "チケット枚数を決定", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(104, "所持数が0orアイテムデータが存在しない", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(103, "チケットのinameが指定されていない", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(102, "キャンセル", FlowNode.PinTypes.Output, 102)]
  public class GachaTicketSelectNumWindow : MonoBehaviour, IFlowInterface
  {
    private const int DefaultMaxNum = 10;
    [SerializeField]
    private Text WindowTitle;
    [SerializeField]
    private BitmapText UsedNum;
    [SerializeField]
    private Slider TicketNumSlider;
    [SerializeField]
    private GameObject AmountTicket;
    [SerializeField]
    private Button BtnDecide;
    [SerializeField]
    private Button BtnCancel;
    [SerializeField]
    private Button BtnPlus;
    [SerializeField]
    private Button BtnMinus;
    private int mSaveUseNum;
    private int mMaxNum;
    private ItemData mCurrentItemData;
    private GachaManager gacham;

    public GachaTicketSelectNumWindow()
    {
      base.\u002Ector();
    }

    public ItemData CurrentItemData
    {
      get
      {
        return this.mCurrentItemData;
      }
    }

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      string iname = FlowNode_Variable.Get("USE_TICKET_INAME");
      FlowNode_Variable.Set("USE_TICKET_INAME", string.Empty);
      if (string.IsNullOrEmpty(iname))
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      }
      else
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (instance.Player.GetItemAmount(iname) <= 0)
        {
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
        }
        else
        {
          this.mCurrentItemData = instance.Player.FindItemDataByItemID(iname);
          if (Object.op_Inequality((Object) this.BtnPlus, (Object) null))
          {
            // ISSUE: method pointer
            ((UnityEvent) this.BtnPlus.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnAddNum)));
          }
          if (Object.op_Inequality((Object) this.BtnMinus, (Object) null))
          {
            // ISSUE: method pointer
            ((UnityEvent) this.BtnMinus.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnRemoveNum)));
          }
          if (Object.op_Equality((Object) this.gacham, (Object) null))
            this.gacham = MonoSingleton<GachaManager>.Instance;
          this.Refresh();
        }
      }
    }

    public void Refresh()
    {
      int num = this.mCurrentItemData.Num;
      this.mMaxNum = num <= 10 ? num : 10;
      this.mSaveUseNum = 1;
      this.gacham.UseTicketNum = this.mSaveUseNum;
      if (Object.op_Inequality((Object) this.WindowTitle, (Object) null))
        this.WindowTitle.set_text(LocalizedText.Get("sys.GACHA_TICKET_SELECT_TITLE", new object[1]
        {
          (object) this.mCurrentItemData.Param.name
        }));
      if (Object.op_Inequality((Object) this.AmountTicket, (Object) null))
        DataSource.Bind<ItemData>(this.AmountTicket, this.CurrentItemData);
      if (Object.op_Inequality((Object) this.TicketNumSlider, (Object) null))
      {
        ((UnityEventBase) this.TicketNumSlider.get_onValueChanged()).RemoveAllListeners();
        this.TicketNumSlider.set_minValue(1f);
        this.TicketNumSlider.set_maxValue((float) this.mMaxNum);
        // ISSUE: method pointer
        ((UnityEvent<float>) this.TicketNumSlider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnUseNumChanged)));
        this.TicketNumSlider.set_value((float) this.mSaveUseNum);
      }
      this.UsedNum.text = this.mSaveUseNum.ToString();
      GameParameter.UpdateAll(this.AmountTicket);
    }

    private void OnAddNum()
    {
      if (!Object.op_Inequality((Object) this.TicketNumSlider, (Object) null) || (double) this.TicketNumSlider.get_maxValue() <= (double) this.TicketNumSlider.get_value())
        return;
      Slider ticketNumSlider = this.TicketNumSlider;
      ticketNumSlider.set_value(ticketNumSlider.get_value() + 1f);
    }

    private void OnRemoveNum()
    {
      if (!Object.op_Inequality((Object) this.TicketNumSlider, (Object) null) || (double) this.TicketNumSlider.get_minValue() >= (double) this.TicketNumSlider.get_value())
        return;
      Slider ticketNumSlider = this.TicketNumSlider;
      ticketNumSlider.set_value(ticketNumSlider.get_value() - 1f);
    }

    private void OnUseNumChanged(float value)
    {
      this.mSaveUseNum = (int) value;
      this.UsedNum.text = this.mSaveUseNum.ToString();
      this.gacham.UseTicketNum = this.mSaveUseNum;
    }
  }
}
