// Decompiled with JetBrains decompiler
// Type: SRPG.GetItemWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "アイテム更新", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "アイテム選択", FlowNode.PinTypes.Output, 100)]
  public class GetItemWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    private List<GameObject> ItemSelectItem;

    public GetItemWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.get_activeInHierarchy())
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
    }

    public void Activated(int pinID)
    {
    }

    public void Refresh(ItemSelectListItemData[] shopdata)
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < this.ItemSelectItem.Count; ++index)
        this.ItemSelectItem[index].get_gameObject().SetActive(false);
      int length = shopdata.Length;
      for (int index = 0; index < length; ++index)
      {
        ItemSelectListItemData data = shopdata[index];
        if (index >= this.ItemSelectItem.Count)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent((Transform) this.ItemLayoutParent, false);
          this.ItemSelectItem.Add(gameObject);
        }
        GameObject gameObject1 = this.ItemSelectItem[index];
        DataSource.Bind<ItemSelectListItemData>(gameObject1, data);
        ItemData itemDataByItemId = player.FindItemDataByItemID(data.iiname);
        DataSource.Bind<ItemData>(gameObject1, itemDataByItemId);
        DataSource.Bind<ItemParam>(gameObject1, MonoSingleton<GameManager>.Instance.GetItemParam(data.iiname));
        ListItemEvents component = (ListItemEvents) gameObject1.GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        gameObject1.SetActive(true);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnSelect(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      GlobalVars.ItemSelectListItemData = DataSource.FindDataOfClass<ItemSelectListItemData>(go, (ItemSelectListItemData) null);
    }
  }
}
