// Decompiled with JetBrains decompiler
// Type: SRPG.ShopBuyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ShopBuyList : MonoBehaviour
  {
    public GameObject amount;
    public GameObject day_reset;
    public GameObject limit;
    public GameObject icon_set;
    public GameObject sold_count;
    public GameObject no_limited_price;
    public Text amount_text;
    public GameObject discount_low;
    public GameObject discount_middle;
    public GameObject discount_high;
    [HeaderBar("▼アイテム表示用")]
    public GameObject item_name;
    public GameObject item_icon;
    [HeaderBar("▼武具表示用")]
    public GameObject artifact_name;
    public GameObject artifact_icon;
    [HeaderBar("▼真理念装表示用")]
    public GameObject conceptCard_name;
    public ConceptCardIcon conceptCard_icon;
    private ShopItem mShopItem;

    public ShopBuyList()
    {
      base.\u002Ector();
    }

    public ShopItem ShopItem
    {
      set
      {
        this.mShopItem = value;
        GameUtility.SetGameObjectActive(this.day_reset, this.mShopItem.is_reset);
        GameUtility.SetGameObjectActive(this.limit, !this.mShopItem.is_reset);
        GameUtility.SetGameObjectActive(this.icon_set, this.mShopItem.saleType == ESaleType.Coin_P);
        GameUtility.SetGameObjectActive(this.sold_count, !this.mShopItem.IsNotLimited);
        GameUtility.SetGameObjectActive(this.no_limited_price, this.mShopItem.IsNotLimited);
        GameUtility.SetGameObjectActive(this.amount, !this.mShopItem.IsSet);
        if (this.mShopItem.IsItem || this.mShopItem.IsSet)
        {
          GameUtility.SetGameObjectActive(this.item_name, true);
          GameUtility.SetGameObjectActive(this.item_icon, true);
          GameUtility.SetGameObjectActive(this.artifact_name, false);
          GameUtility.SetGameObjectActive(this.artifact_icon, false);
          GameUtility.SetGameObjectActive(this.conceptCard_name, false);
          GameUtility.SetGameObjectActive((Component) this.conceptCard_icon, false);
        }
        else if (this.mShopItem.IsArtifact)
        {
          GameUtility.SetGameObjectActive(this.item_name, false);
          GameUtility.SetGameObjectActive(this.item_icon, false);
          GameUtility.SetGameObjectActive(this.artifact_name, true);
          GameUtility.SetGameObjectActive(this.artifact_icon, true);
          GameUtility.SetGameObjectActive(this.conceptCard_name, false);
          GameUtility.SetGameObjectActive((Component) this.conceptCard_icon, false);
        }
        else if (this.mShopItem.IsConceptCard)
        {
          GameUtility.SetGameObjectActive(this.item_name, false);
          GameUtility.SetGameObjectActive(this.item_icon, false);
          GameUtility.SetGameObjectActive(this.artifact_name, false);
          GameUtility.SetGameObjectActive(this.artifact_icon, false);
          GameUtility.SetGameObjectActive(this.conceptCard_name, true);
          GameUtility.SetGameObjectActive((Component) this.conceptCard_icon, true);
        }
        if (!Object.op_Inequality((Object) this.amount_text, (Object) null))
          return;
        this.amount_text.set_text(this.mShopItem.remaining_num.ToString());
      }
      get
      {
        return this.mShopItem;
      }
    }

    public void SetupDiscountUI()
    {
      if (this.mShopItem == null)
        return;
      int discount = this.mShopItem.discount;
      ShopBuyList.DiscountGrade discountGrade = ShopBuyList.DiscountGrade.None;
      if (discount > 0)
      {
        discountGrade = ShopBuyList.DiscountGrade.Low;
        if (discount >= 40 && discount < 70)
          discountGrade = ShopBuyList.DiscountGrade.Middle;
        else if (discount >= 70)
          discountGrade = ShopBuyList.DiscountGrade.High;
      }
      GameObject gameObject = (GameObject) null;
      if (Object.op_Inequality((Object) this.discount_low, (Object) null))
      {
        bool flag = discountGrade == ShopBuyList.DiscountGrade.Low;
        this.discount_low.SetActive(flag);
        if (flag)
          gameObject = this.discount_low;
      }
      if (Object.op_Inequality((Object) this.discount_middle, (Object) null))
      {
        bool flag = discountGrade == ShopBuyList.DiscountGrade.Middle;
        this.discount_middle.SetActive(flag);
        if (flag)
          gameObject = this.discount_middle;
      }
      if (Object.op_Inequality((Object) this.discount_high, (Object) null))
      {
        bool flag = discountGrade == ShopBuyList.DiscountGrade.High;
        this.discount_high.SetActive(flag);
        if (flag)
          gameObject = this.discount_high;
      }
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      SerializeValueBehaviour component = (SerializeValueBehaviour) gameObject.GetComponent<SerializeValueBehaviour>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      Text uiLabel = component.list.GetUILabel("text");
      if (!Object.op_Inequality((Object) uiLabel, (Object) null))
        return;
      uiLabel.set_text(discount.ToString());
    }

    public void SetupConceptCard(ConceptCardData conceptCardData)
    {
      if (Object.op_Equality((Object) this.conceptCard_icon, (Object) null))
        return;
      this.conceptCard_icon.Setup(conceptCardData);
    }

    public enum DiscountGrade : byte
    {
      None,
      Low,
      Middle,
      High,
    }
  }
}
