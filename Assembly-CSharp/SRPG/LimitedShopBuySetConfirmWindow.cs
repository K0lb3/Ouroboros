// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopBuySetConfirmWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class LimitedShopBuySetConfirmWindow : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    public GameObject ItemParent;
    private List<LimitedShopSetItemListElement> limited_shop_item_set_list;

    public LimitedShopBuySetConfirmWindow()
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
      LimitedShopItem data = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData().items[GlobalVars.ShopBuyIndex];
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(data.iname);
      this.limited_shop_item_set_list.Clear();
      if (data.IsSet)
      {
        for (int index = 0; index < data.children.Length; ++index)
        {
          GameObject gameObject = index >= this.limited_shop_item_set_list.Count ? (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate) : ((Component) this.limited_shop_item_set_list[index]).get_gameObject();
          if (Object.op_Inequality((Object) gameObject, (Object) null))
          {
            gameObject.SetActive(true);
            Vector3 localScale = gameObject.get_transform().get_localScale();
            gameObject.get_transform().SetParent(this.ItemParent.get_transform());
            gameObject.get_transform().set_localScale(localScale);
            LimitedShopSetItemListElement component = (LimitedShopSetItemListElement) gameObject.GetComponent<LimitedShopSetItemListElement>();
            ItemData itemData = new ItemData();
            itemData.Setup(0L, data.children[index].iname, data.children[index].num);
            StringBuilder stringBuilder = GameUtility.GetStringBuilder();
            if (itemData != null)
              stringBuilder.Append(itemData.Param.name);
            stringBuilder.Append(" × ");
            stringBuilder.Append(data.children[index].num.ToString());
            component.itemName.set_text(stringBuilder.ToString());
            component.itemData = itemData;
            this.limited_shop_item_set_list.Add(component);
          }
        }
      }
      DataSource.Bind<LimitedShopItem>(((Component) this).get_gameObject(), data);
      DataSource.Bind<ItemData>(((Component) this).get_gameObject(), itemDataByItemId);
      DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), MonoSingleton<GameManager>.Instance.GetItemParam(data.iname));
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
