// Decompiled with JetBrains decompiler
// Type: SRPG.GachaResultItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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

    public void Setup(int _index)
    {
      ItemParam itemParam = GachaResultData.drops[_index].item;
      int num = GachaResultData.drops[_index].num;
      if (itemParam == null)
        return;
      ItemData _data = new ItemData();
      _data.Setup(0L, itemParam, num);
      this.Setup(_data);
    }

    public void Setup(ItemData _data)
    {
      this.mCurrentItem = _data;
    }

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.ItemInfo, (Object) null))
        return;
      DataSource.Bind<ItemData>(this.ItemInfo, this.mCurrentItem);
      GameParameter.UpdateAll(this.ItemInfo);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }
  }
}
