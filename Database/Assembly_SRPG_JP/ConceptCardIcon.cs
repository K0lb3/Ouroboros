// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardIcon : MonoBehaviour
  {
    [SerializeField]
    private RawImage mIconImage;
    [SerializeField]
    private Text mNameText;
    [SerializeField]
    private Text mLevelTitleText;
    [SerializeField]
    private Text mLevelText;
    [SerializeField]
    private Text mLevelCapText;
    [SerializeField]
    private Image mRarityImage;
    [SerializeField]
    private ImageArray mRarityFrame;
    [SerializeField]
    private Text mTrustText;
    [SerializeField]
    private GameObject mFavorite;
    [SerializeField]
    private GameObject mOwner;
    [SerializeField]
    private Image mOwnerIcon;
    [SerializeField]
    private GameObject mSameCardIcon;
    [SerializeField]
    private GameObject mExistSwitchOn;
    [SerializeField]
    private GameObject mExistSwitchOff;
    [SerializeField]
    private Text mCardNum;
    [SerializeField]
    private GameObject mNotSale;
    private ConceptCardData mConceptCard;

    public ConceptCardIcon()
    {
      base.\u002Ector();
    }

    public ConceptCardData ConceptCard
    {
      get
      {
        return this.mConceptCard;
      }
    }

    public void Setup(ConceptCardData card)
    {
      this.mConceptCard = card;
      if (card != null)
        this.Refresh();
      else
        this.ResetIcon();
    }

    public void ResetIcon()
    {
      this.mConceptCard = (ConceptCardData) null;
      this.mIconImage.set_texture((Texture) null);
      this.Refresh();
    }

    public void Refresh()
    {
      DataSource.Bind<ConceptCardData>(((Component) this).get_gameObject(), this.mConceptCard);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      this.RefreshExistImage();
      this.RefreshIconImage();
      this.RefreshIconParam();
    }

    public UnitData GetOwner()
    {
      if (this.mConceptCard == null)
        return (UnitData) null;
      return this.mConceptCard.GetOwner();
    }

    private void RefreshExistImage()
    {
      bool flag = this.mConceptCard != null;
      if (Object.op_Inequality((Object) this.mExistSwitchOn, (Object) null))
        this.mExistSwitchOn.SetActive(flag);
      if (!Object.op_Inequality((Object) this.mExistSwitchOff, (Object) null))
        return;
      this.mExistSwitchOff.SetActive(!flag);
    }

    private void RefreshIconImage()
    {
      if (this.mConceptCard == null || Object.op_Equality((Object) this.mIconImage, (Object) null))
        MonoSingleton<GameManager>.Instance.CancelTextureLoadRequest(this.mIconImage);
      else
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mIconImage, AssetPath.ConceptCardIcon(this.mConceptCard.Param));
    }

    private void RefreshIconParam()
    {
      if (Object.op_Inequality((Object) this.mRarityFrame, (Object) null))
        ((Component) this.mRarityFrame).get_gameObject().SetActive(false);
      if (this.mConceptCard == null)
      {
        if (Object.op_Inequality((Object) this.mLevelTitleText, (Object) null))
          ((Component) this.mLevelTitleText).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.mLevelText, (Object) null))
          ((Component) this.mLevelText).get_gameObject().SetActive(false);
        if (!Object.op_Inequality((Object) this.mLevelCapText, (Object) null))
          return;
        ((Component) this.mLevelCapText).get_gameObject().SetActive(false);
      }
      else
      {
        this.SetNameText(this.mConceptCard.Param.name);
        this.SetLevelText((int) this.mConceptCard.Lv);
        this.SetLevelCapText((int) this.mConceptCard.CurrentLvCap);
        this.SetTrustText((int) this.mConceptCard.Trust);
        this.SetRarityImaget(this.mConceptCard.Param.rare);
        this.SetFavorite(this.mConceptCard.Favorite);
        this.SetRarityFrame((int) this.mConceptCard.Rarity);
        if (Object.op_Inequality((Object) this.mOwner, (Object) null))
        {
          UnitData owner = this.GetOwner();
          if (owner != null)
          {
            this.mOwner.SetActive(true);
            this.SetOwnerIcon(this.mOwnerIcon, owner);
          }
          else
            this.mOwner.SetActive(false);
        }
        this.SetSameCardIcon();
      }
    }

    public void SetNameText(string name)
    {
      if (Object.op_Equality((Object) this.mNameText, (Object) null))
        return;
      this.mNameText.set_text(name.ToString());
    }

    public void SetLevelText(int lv)
    {
      if (Object.op_Equality((Object) this.mLevelText, (Object) null))
        return;
      this.mLevelText.set_text(lv.ToString());
      ((Component) this.mLevelText).get_gameObject().SetActive(true);
      if (!Object.op_Inequality((Object) this.mLevelTitleText, (Object) null))
        return;
      ((Component) this.mLevelTitleText).get_gameObject().SetActive(true);
    }

    public void SetLevelCapText(int lvcap)
    {
      if (Object.op_Equality((Object) this.mLevelCapText, (Object) null))
        return;
      this.mLevelCapText.set_text(lvcap.ToString());
      ((Component) this.mLevelCapText).get_gameObject().SetActive(true);
      if (!Object.op_Inequality((Object) this.mLevelTitleText, (Object) null))
        return;
      ((Component) this.mLevelTitleText).get_gameObject().SetActive(true);
    }

    public void SetTrustText(int trust)
    {
      if (Object.op_Equality((Object) this.mTrustText, (Object) null))
        return;
      ConceptCardManager.SubstituteTrustFormat(this.mConceptCard, this.mTrustText, trust, false);
    }

    public void SetNoRewardTrustText()
    {
      if (Object.op_Equality((Object) this.mTrustText, (Object) null))
        return;
      this.mTrustText.set_text("---");
    }

    public void SetRarityImaget(int rare)
    {
      if (Object.op_Equality((Object) this.mRarityImage, (Object) null))
        return;
      this.mRarityImage.set_sprite((Sprite) null);
      GameSettings instance = GameSettings.Instance;
      if (!Object.op_Inequality((Object) instance, (Object) null) || instance.ConceptCardIcon_Rarity.Length <= 0)
        return;
      this.mRarityImage.set_sprite(instance.ConceptCardIcon_Rarity[rare]);
    }

    public void SetFavorite(bool favorite)
    {
      if (Object.op_Equality((Object) this.mFavorite, (Object) null))
        return;
      this.mFavorite.SetActive(favorite);
    }

    public void SetRarityFrame(int rarity)
    {
      if (Object.op_Equality((Object) this.mRarityFrame, (Object) null))
        return;
      ((Component) this.mRarityFrame).get_gameObject().SetActive(true);
      GameSettings instance = GameSettings.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      this.mRarityFrame.set_sprite(instance.GetConceptCardFrame(rarity));
    }

    public void SetOwnerIcon(Image OwnerIcon, UnitData ownerUnit)
    {
      if (Object.op_Equality((Object) OwnerIcon, (Object) null))
        return;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(ownerUnit.UnitParam.piece);
      OwnerIcon.set_sprite(spriteSheet.GetSprite(itemParam.icon));
    }

    public void SetSameCardIcon()
    {
      if (Object.op_Equality((Object) this.mSameCardIcon, (Object) null))
        return;
      this.mSameCardIcon.SetActive(false);
      if (Object.op_Equality((Object) ConceptCardManager.Instance, (Object) null) || ConceptCardManager.Instance.SelectedConceptCardData == null || !ConceptCardManager.Instance.IsEnhanceListActive)
        return;
      this.mSameCardIcon.SetActive(this.mConceptCard.Param.iname == ConceptCardManager.Instance.SelectedConceptCardData.Param.iname);
    }

    public void SetCardNum(int num)
    {
      if (Object.op_Equality((Object) this.mCardNum, (Object) null))
        return;
      this.mCardNum.set_text(num.ToString());
    }

    public void SetNotSellFlag(bool flag)
    {
      if (Object.op_Equality((Object) this.mNotSale, (Object) null))
        return;
      this.mNotSale.SetActive(flag);
    }
  }
}
