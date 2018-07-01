// Decompiled with JetBrains decompiler
// Type: SRPG.ItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class ItemData
  {
    private long mUniqueID;
    private ItemParam mItemParam;
    private RarityParam mRarityParam;
    private SkillData mSkill;
    private ItemData.ItemFlags mFlags;
    protected int mNum;
    private bool mIsNew;

    public int No
    {
      get
      {
        return this.mItemParam.no;
      }
    }

    public long UniqueID
    {
      get
      {
        return this.mUniqueID;
      }
    }

    public ItemParam Param
    {
      get
      {
        return this.mItemParam;
      }
    }

    public string ItemID
    {
      get
      {
        if (this.mItemParam != null)
          return this.mItemParam.iname;
        return (string) null;
      }
    }

    public int Num
    {
      get
      {
        if (this.mItemParam != null)
          return Math.Min(this.mNum, this.mItemParam.cap);
        return this.mNum;
      }
    }

    public int NumNonCap
    {
      get
      {
        return this.mNum;
      }
    }

    public SkillData Skill
    {
      get
      {
        return this.mSkill;
      }
    }

    public bool IsUsed
    {
      get
      {
        return this.mNum > 0;
      }
    }

    public EItemType ItemType
    {
      get
      {
        if (this.mItemParam != null)
          return this.mItemParam.type;
        return EItemType.Used;
      }
    }

    public int Rarity
    {
      get
      {
        if (this.mItemParam != null)
          return this.mItemParam.rare;
        return 0;
      }
    }

    public RarityParam RarityParam
    {
      get
      {
        return this.mRarityParam;
      }
    }

    public int HaveCap
    {
      get
      {
        if (this.mItemParam != null)
          return this.mItemParam.cap;
        return 0;
      }
    }

    public int InventoryCap
    {
      get
      {
        if (this.mItemParam != null)
          return this.mItemParam.invcap;
        return 0;
      }
    }

    public int Buy
    {
      get
      {
        if (this.mItemParam != null)
          return this.mItemParam.buy;
        return 0;
      }
    }

    public int Sell
    {
      get
      {
        if (this.mItemParam != null)
          return this.mItemParam.sell;
        return 0;
      }
    }

    public RecipeParam Recipe
    {
      get
      {
        if (this.mItemParam != null)
          return this.mItemParam.Recipe;
        return (RecipeParam) null;
      }
    }

    public void SetFlag(ItemData.ItemFlags flag)
    {
      this.mFlags |= flag;
    }

    public void ResetFlag(ItemData.ItemFlags flag)
    {
      this.mFlags &= ~flag;
    }

    public void ResetAllFlag(ItemData.ItemFlags flag)
    {
      this.mFlags = (ItemData.ItemFlags) 0;
    }

    public bool GetFlag(ItemData.ItemFlags flag)
    {
      return (ItemData.ItemFlags) 0 != (this.mFlags & flag);
    }

    public bool IsNew
    {
      get
      {
        return this.GetFlag(ItemData.ItemFlags.NewItem);
      }
      set
      {
        if (value)
          this.SetFlag(ItemData.ItemFlags.NewItem);
        else
          this.ResetFlag(ItemData.ItemFlags.NewItem);
      }
    }

    public bool IsNewSkin
    {
      get
      {
        return this.GetFlag(ItemData.ItemFlags.NewSkin);
      }
      set
      {
        if (value)
          this.SetFlag(ItemData.ItemFlags.NewSkin);
        else
          this.ResetFlag(ItemData.ItemFlags.NewSkin);
      }
    }

    public bool Deserialize(Json_Item json)
    {
      if (json != null)
        return this.Setup(json.iid, json.iname, json.num);
      return false;
    }

    public bool Setup(long iid, string iname, int num)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      this.mItemParam = instanceDirect.GetItemParam(iname);
      DebugUtility.Assert(this.mItemParam != null, "Failed ItemParam iname \"" + iname + "\" not found.");
      this.mRarityParam = instanceDirect.GetRarityParam(this.mItemParam.rare);
      this.mUniqueID = iid;
      this.mNum = num;
      if (!string.IsNullOrEmpty(this.mItemParam.skill))
      {
        int rank = 1;
        this.mSkill = new SkillData();
        this.mSkill.Setup(this.mItemParam.skill, rank, this.GetRankCap(), (MasterParam) null);
      }
      return true;
    }

    public bool Setup(long iid, ItemParam itemParam, int num)
    {
      this.mItemParam = itemParam;
      this.mUniqueID = iid;
      this.mNum = num;
      if (!string.IsNullOrEmpty(this.mItemParam.skill))
      {
        int rank = 1;
        this.mSkill = new SkillData();
        this.mSkill.Setup(this.mItemParam.skill, rank, this.GetRankCap(), (MasterParam) null);
      }
      return true;
    }

    public void Gain(int num)
    {
      this.mNum = Math.Max(this.mNum + num, 0);
    }

    public void Used(int num)
    {
      this.mNum = Math.Max(this.mNum - num, 0);
    }

    public void SetNum(int num)
    {
      this.mNum = Math.Max(num, 0);
    }

    public override string ToString()
    {
      return (this.mItemParam == null ? "None" : this.mItemParam.name) + this.GetType().FullName;
    }

    public bool CheckEquipEnhanceMaterial()
    {
      if (this.mItemParam != null)
        return this.mItemParam.CheckEquipEnhanceMaterial();
      return false;
    }

    public int GetRankCap()
    {
      if (this.mRarityParam != null)
        return (int) this.mRarityParam.EquipEnhanceParam.rankcap;
      return 1;
    }

    [Flags]
    public enum ItemFlags
    {
      NewItem = 1,
      NewSkin = 2,
    }
  }
}
