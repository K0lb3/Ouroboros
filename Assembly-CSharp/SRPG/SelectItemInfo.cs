// Decompiled with JetBrains decompiler
// Type: SRPG.SelectItemInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class SelectItemInfo : MonoBehaviour, IFlowInterface
  {
    public SelectItemInfo()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
    }

    private void Start()
    {
      this.Refresh();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.ItemSelectListItemData.iiname);
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(itemParam.iname);
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), itemParam);
      DataSource.Bind<ItemSelectListItemData>(((Component) this).get_gameObject(), GlobalVars.ItemSelectListItemData);
      DataSource.Bind<ItemData>(((Component) this).get_gameObject(), itemDataByItemId);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
