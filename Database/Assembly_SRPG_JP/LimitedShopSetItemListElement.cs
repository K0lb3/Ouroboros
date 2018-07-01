// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopSetItemListElement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LimitedShopSetItemListElement : MonoBehaviour
  {
    public Text itemName;
    public GameObject ItemIcon;
    public GameObject ItemDetailWindow;
    public GameObject ArtifactIcon;
    public GameObject ArtifactDetailWindow;
    public GameObject ConceptCard;
    public GameObject ConceptCardDetailWindow;
    private GameObject mDetailWindow;
    private LimitedShopItem mLimitedShopItem;
    private ItemData mItemData;
    private ArtifactParam mArtifactParam;
    private ConceptCardData mConceptCardData;

    public LimitedShopSetItemListElement()
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

    public void SetupConceptCard(ConceptCardData conceptCardData)
    {
      this.mConceptCardData = conceptCardData;
      if (Object.op_Equality((Object) this.ConceptCard, (Object) null))
      {
        DebugUtility.LogError("ConceptCard == null");
      }
      else
      {
        ConceptCardIcon componentInChildren = (ConceptCardIcon) this.ConceptCard.GetComponentInChildren<ConceptCardIcon>();
        if (!Object.op_Inequality((Object) componentInChildren, (Object) null))
          return;
        componentInChildren.Setup(conceptCardData);
      }
    }

    public void SetShopItemDesc(Json_ShopItemDesc item)
    {
      this.ItemIcon.SetActive(false);
      this.ArtifactIcon.SetActive(false);
      this.ConceptCard.SetActive(false);
      if (item.IsItem)
        this.ItemIcon.SetActive(true);
      else if (item.IsArtifact)
        this.ArtifactIcon.SetActive(true);
      else if (item.IsConceptCard)
        this.ConceptCard.SetActive(true);
      else
        DebugUtility.LogError(string.Format("不明な商品タイプが設定されています (item.iname({0}) => {1})", (object) item.iname, (object) item.itype));
      if (this.mLimitedShopItem == null)
        this.mLimitedShopItem = new LimitedShopItem();
      this.mLimitedShopItem.num = item.num;
      this.mLimitedShopItem.iname = item.iname;
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
      DataSource.Bind<LimitedShopItem>(this.mDetailWindow, this.mLimitedShopItem);
    }

    public void OnClickDetailConceptCard()
    {
      if (Object.op_Inequality((Object) this.mDetailWindow, (Object) null))
        return;
      GlobalVars.SelectedConceptCardData.Set(this.mConceptCardData);
      this.mDetailWindow = (GameObject) Object.Instantiate<GameObject>((M0) this.ConceptCardDetailWindow);
    }
  }
}
