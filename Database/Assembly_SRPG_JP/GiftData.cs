// Decompiled with JetBrains decompiler
// Type: SRPG.GiftData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class GiftData
  {
    public string iname;
    public int num;
    public int gold;
    public int coin;
    public int arenacoin;
    public int multicoin;
    public int kakeracoin;
    public GiftData.GiftConceptCard conceptCard;
    public long giftTypes;
    public int rarity;

    public string ConceptCardIname
    {
      get
      {
        if (this.conceptCard == null)
          return string.Empty;
        return this.conceptCard.iname;
      }
    }

    public int ConceptCardNum
    {
      get
      {
        if (this.conceptCard == null)
          return 0;
        return this.conceptCard.num;
      }
    }

    public bool IsGetConceptCardUnit
    {
      get
      {
        if (this.conceptCard == null)
          return false;
        return !string.IsNullOrEmpty(this.conceptCard.get_unit);
      }
    }

    public string ConceptCardGetUnitIname
    {
      get
      {
        if (this.conceptCard == null)
          return string.Empty;
        return this.conceptCard.get_unit;
      }
    }

    public bool IsValidRarity
    {
      get
      {
        return this.rarity != -1;
      }
    }

    public bool NotSet
    {
      get
      {
        return this.giftTypes == 0L;
      }
    }

    public bool CheckGiftTypeIncluded(GiftTypes flag)
    {
      return ((GiftTypes) this.giftTypes & flag) != (GiftTypes) 0;
    }

    public void SetGiftTypeIncluded(GiftTypes flag)
    {
      this.giftTypes |= (long) flag;
    }

    public void UpdateGiftTypes()
    {
      if (this.gold > 0)
        this.SetGiftTypeIncluded(GiftTypes.Gold);
      if (this.coin > 0)
        this.SetGiftTypeIncluded(GiftTypes.Coin);
      if (this.arenacoin > 0)
        this.SetGiftTypeIncluded(GiftTypes.ArenaCoin);
      if (this.multicoin > 0)
        this.SetGiftTypeIncluded(GiftTypes.MultiCoin);
      if (this.kakeracoin > 0)
        this.SetGiftTypeIncluded(GiftTypes.KakeraCoin);
      if (!string.IsNullOrEmpty(this.ConceptCardIname))
        this.SetGiftTypeIncluded(GiftTypes.ConceptCard);
      if (string.IsNullOrEmpty(this.iname))
        return;
      if (this.iname.StartsWith("AF_"))
      {
        if (this.num <= 0)
          return;
        this.SetGiftTypeIncluded(GiftTypes.Artifact);
      }
      else if (this.iname.StartsWith("IT_STS_"))
      {
        if (this.num <= 0)
          return;
        this.SetGiftTypeIncluded(GiftTypes.SelectConceptCardItem);
      }
      else if (this.iname.StartsWith("IT_SU_"))
      {
        if (this.num <= 0)
          return;
        this.SetGiftTypeIncluded(GiftTypes.SelectUnitItem);
      }
      else if (this.iname.StartsWith("IT_SI_"))
      {
        if (this.num <= 0)
          return;
        this.SetGiftTypeIncluded(GiftTypes.SelectItem);
      }
      else if (this.iname.StartsWith("IT_SA_"))
      {
        if (this.num <= 0)
          return;
        this.SetGiftTypeIncluded(GiftTypes.SelectArtifactItem);
      }
      else if (this.iname.StartsWith("IT_"))
      {
        if (this.num <= 0)
          return;
        this.SetGiftTypeIncluded(GiftTypes.Item);
      }
      else if (this.iname.StartsWith("UN_"))
      {
        if (this.num <= 0)
          return;
        this.SetGiftTypeIncluded(GiftTypes.Unit);
      }
      else
      {
        if (!this.iname.StartsWith("AWARD_") || this.num <= 0)
          return;
        this.SetGiftTypeIncluded(GiftTypes.Award);
      }
    }

    public ArtifactData CreateArtifactData()
    {
      if (!this.CheckGiftTypeIncluded(GiftTypes.Artifact))
      {
        DebugUtility.LogError("このギフトは武具ではありません");
        return (ArtifactData) null;
      }
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.iname);
      if (artifactParam == null)
        return (ArtifactData) null;
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        exp = 0,
        iname = artifactParam.iname,
        fav = 0,
        rare = !this.IsValidRarity ? artifactParam.rareini : this.rarity
      });
      return artifactData;
    }

    public class GiftConceptCard
    {
      public string iname;
      public int num;
      public string get_unit;
    }
  }
}
