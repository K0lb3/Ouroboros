// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopSetItemListElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventShopSetItemListElement : MonoBehaviour
  {
    public Text itemName;
    public GameObject ItemIcon;
    public GameObject ArtifactIcon;
    private ItemData mItemData;
    private ArtifactParam mArtifactParam;
    private GameObject mDetailWindow;
    public GameObject ArtifactDetailWindow;
    public GameObject ItemDetailWindow;
    private EventShopItem mShopItem;

    public EventShopSetItemListElement()
    {
      base.\u002Ector();
    }

    public ItemData itemData
    {
      set
      {
        DataSource.Bind<ItemData>(((Component) this).get_gameObject(), value);
        this.mItemData = value;
      }
      get
      {
        return this.mItemData;
      }
    }

    public ArtifactParam ArtifactParam
    {
      set
      {
        DataSource.Bind<ArtifactParam>(((Component) this).get_gameObject(), value);
        this.mArtifactParam = value;
      }
      get
      {
        return this.mArtifactParam;
      }
    }

    public void SetShopItemDesc(Json_ShopItemDesc item)
    {
      this.ItemIcon.SetActive(!item.IsArtifact);
      this.ArtifactIcon.SetActive(item.IsArtifact);
      if (this.mShopItem == null)
        this.mShopItem = new EventShopItem();
      this.mShopItem.num = item.num;
      this.mShopItem.iname = item.iname;
    }

    public void OnClickDetailArtifact()
    {
      if (Object.op_Inequality((Object) this.mDetailWindow, (Object) null))
        return;
      this.mDetailWindow = (GameObject) Object.Instantiate<GameObject>((M0) this.ArtifactDetailWindow);
      ArtifactData data = new ArtifactData();
      data.Deserialize(new Json_Artifact()
      {
        iname = this.mArtifactParam.iname,
        rare = this.mArtifactParam.rareini
      });
      DataSource.Bind<ArtifactData>(this.mDetailWindow, data);
    }

    public void OnClickDetailItem()
    {
      if (Object.op_Inequality((Object) this.mDetailWindow, (Object) null))
        return;
      this.mDetailWindow = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemDetailWindow);
      DataSource.Bind<ItemData>(this.mDetailWindow, MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mItemData.Param.iname));
      DataSource.Bind<ItemParam>(this.mDetailWindow, MonoSingleton<GameManager>.Instance.GetItemParam(this.mItemData.Param.iname));
      DataSource.Bind<EventShopItem>(this.mDetailWindow, this.mShopItem);
    }
  }
}
