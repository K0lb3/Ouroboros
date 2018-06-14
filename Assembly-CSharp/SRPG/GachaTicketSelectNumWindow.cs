// Decompiled with JetBrains decompiler
// Type: SRPG.GachaTicketSelectNumWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(104, "所持数が0orアイテムデータが存在しない", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(103, "チケットのinameが指定されていない", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(102, "キャンセル", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(101, "チケット枚数を決定", FlowNode.PinTypes.Output, 101)]
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
    [SerializeField]
    private Button BtnMax;
    private int mSaveUseNum;
    private int mMaxNum;
    private GachaManager gacham;

    public GachaTicketSelectNumWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
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
      if (Object.op_Inequality((Object) this.BtnMax, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnMax.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnMaxNum)));
      }
      string iname = FlowNode_Variable.Get("USE_TICKET_INAME");
      FlowNode_Variable.Set("USE_TICKET_INAME", string.Empty);
      if (string.IsNullOrEmpty(iname))
      {
        DebugUtility.LogError("不正なアイテムが指定されました");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      }
      else
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(iname);
        if (itemDataByItemId == null || itemDataByItemId.Num < 0)
        {
          DebugUtility.LogError("所持していないアイテムが指定されました");
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
        }
        else
        {
          if (Object.op_Equality((Object) this.gacham, (Object) null))
            this.gacham = MonoSingleton<GachaManager>.Instance;
          this.Refresh(itemDataByItemId);
        }
      }
    }

    public void Refresh(ItemData data)
    {
      this.mMaxNum = Mathf.Min(data.Num, 10);
      if (Object.op_Inequality((Object) this.WindowTitle, (Object) null))
        this.WindowTitle.set_text(LocalizedText.Get("sys.GACHA_TICKET_SELECT_TITLE", new object[1]
        {
          (object) data.Param.name
        }));
      if (Object.op_Inequality((Object) this.AmountTicket, (Object) null))
      {
        DataSource.Bind<ItemData>(this.AmountTicket, data);
        GameParameter.UpdateAll(this.AmountTicket);
      }
      if (Object.op_Inequality((Object) this.TicketNumSlider, (Object) null))
      {
        ((UnityEventBase) this.TicketNumSlider.get_onValueChanged()).RemoveAllListeners();
        this.TicketNumSlider.set_minValue(1f);
        this.TicketNumSlider.set_maxValue((float) this.mMaxNum);
        // ISSUE: method pointer
        ((UnityEvent<float>) this.TicketNumSlider.get_onValueChanged()).AddListener(new UnityAction<float>((object) this, __methodptr(OnUseNumChanged)));
        this.TicketNumSlider.set_value(this.TicketNumSlider.get_minValue());
      }
      if (Object.op_Inequality((Object) this.BtnPlus, (Object) null))
        ((Selectable) this.BtnPlus).set_interactable((double) this.TicketNumSlider.get_value() + 1.0 <= (double) this.TicketNumSlider.get_maxValue());
      if (Object.op_Inequality((Object) this.BtnMinus, (Object) null))
        ((Selectable) this.BtnMinus).set_interactable((double) this.TicketNumSlider.get_value() - 1.0 >= (double) this.TicketNumSlider.get_minValue());
      this.UsedNum.text = this.TicketNumSlider.get_value().ToString();
      this.gacham.UseTicketNum = (int) this.TicketNumSlider.get_value();
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
      this.UsedNum.text = ((int) value).ToString();
      this.gacham.UseTicketNum = (int) value;
      if (Object.op_Inequality((Object) this.BtnPlus, (Object) null))
        ((Selectable) this.BtnPlus).set_interactable((double) this.TicketNumSlider.get_value() + 1.0 <= (double) this.TicketNumSlider.get_maxValue());
      if (!Object.op_Inequality((Object) this.BtnMinus, (Object) null))
        return;
      ((Selectable) this.BtnMinus).set_interactable((double) this.TicketNumSlider.get_value() - 1.0 >= (double) this.TicketNumSlider.get_minValue());
    }

    private void OnMaxNum()
    {
      if (!Object.op_Inequality((Object) this.TicketNumSlider, (Object) null))
        return;
      this.TicketNumSlider.set_value(this.TicketNumSlider.get_maxValue());
    }
  }
}
