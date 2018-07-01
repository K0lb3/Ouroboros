// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopGiftWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class EventShopGiftWindow : MonoBehaviour
  {
    [SerializeField]
    private GameObject ItemParent;
    [SerializeField]
    private GameObject ItemTemplate;
    private List<GameObject> mItems;

    public EventShopGiftWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        this.ItemTemplate.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects(this.mItems);
      this.mItems.Clear();
      EventShopData eventShopData = MonoSingleton<GameManager>.Instance.Player.GetEventShopData();
      if (eventShopData == null || eventShopData.items.Count <= 0 || Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      int shopBuyIndex = GlobalVars.ShopBuyIndex;
      Transform parent = !Object.op_Inequality((Object) this.ItemParent, (Object) null) ? this.ItemTemplate.get_transform().get_parent() : this.ItemParent.get_transform();
      EventShopItem eventShopItem = eventShopData.items[shopBuyIndex];
      if (eventShopItem.IsArtifact)
      {
        eventShopItem.children = new Json_ShopItemDesc[1];
        eventShopItem.children[0] = new Json_ShopItemDesc();
        eventShopItem.children[0].iname = eventShopItem.iname;
        eventShopItem.children[0].num = eventShopItem.num;
      }
      if (eventShopItem.children != null && eventShopItem.children.Length > 0)
      {
        foreach (Json_ShopItemDesc child in eventShopItem.children)
        {
          string empty = string.Empty;
          GameObject gameObject;
          string name;
          if (child.IsArtifact)
          {
            ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(child.iname);
            if (artifactParam != null)
            {
              gameObject = this.InstantiateItem<ArtifactParam>(this.ItemTemplate, parent, artifactParam);
              name = artifactParam.name;
            }
            else
              continue;
          }
          else
          {
            ItemData itemData = new ItemData();
            if (itemData.Setup(0L, child.iname, child.num))
            {
              gameObject = this.InstantiateItem<ItemData>(this.ItemTemplate, parent, itemData);
              name = itemData.Param.name;
            }
            else
              continue;
          }
          EventShopGiftItem component = (EventShopGiftItem) gameObject.GetComponent<EventShopGiftItem>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.SetShopItemInfo(child, name);
        }
      }
      GameParameter.UpdateAll(((Component) parent).get_gameObject());
    }

    public GameObject InstantiateItem<BindType>(GameObject template, Transform parent, BindType item)
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) template);
      gameObject.get_transform().SetParent(parent, false);
      DataSource.Bind<BindType>(gameObject, item);
      this.mItems.Add(gameObject);
      gameObject.SetActive(true);
      return gameObject;
    }
  }
}
