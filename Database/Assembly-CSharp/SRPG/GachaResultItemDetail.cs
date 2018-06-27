// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "Close", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refreshed", FlowNode.PinTypes.Output, 2)]
  public class GachaResultItemDetail : MonoBehaviour, IFlowInterface
  {
    private readonly int OUT_CLOSE_DETAIL;
    public GameObject ItemInfo;
    public GameObject Bg;
    private ItemData mCurrentItem;
    [SerializeField]
    private Button CloseBtn;

    public GachaResultItemDetail()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.CloseBtn, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.CloseBtn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCloseDetail)));
    }

    private void OnEnable()
    {
      Animator component1 = (Animator) ((Component) this).GetComponent<Animator>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        component1.SetBool("close", false);
      if (Object.op_Inequality((Object) this.Bg, (Object) null))
      {
        CanvasGroup component2 = (CanvasGroup) this.Bg.GetComponent<CanvasGroup>();
        if (Object.op_Inequality((Object) component2, (Object) null))
        {
          component2.set_interactable(true);
          component2.set_blocksRaycasts(true);
        }
      }
      this.Refresh();
    }

    private void OnCloseDetail()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, this.OUT_CLOSE_DETAIL);
    }

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.ItemInfo, (Object) null))
        return;
      int index = int.Parse(FlowNode_Variable.Get("GachaResultDataIndex"));
      ItemParam itemParam = GachaResultData.drops[index].item;
      int num = GachaResultData.drops[index].num;
      if (itemParam == null)
        return;
      this.mCurrentItem = new ItemData();
      this.mCurrentItem.Setup(0L, itemParam, num);
      DataSource.Bind<ItemData>(this.ItemInfo, this.mCurrentItem);
      GameParameter.UpdateAll(this.ItemInfo);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }
  }
}
