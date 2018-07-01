// Decompiled with JetBrains decompiler
// Type: SRPG.EquipData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class EquipData
  {
    private long mUniqueID;
    private ItemParam mItemParam;
    private RarityParam mRarityParam;
    private int mExp;
    private int mRank;
    private SkillData mSkill;
    private bool mEquiped;

    public long UniqueID
    {
      get
      {
        return this.mUniqueID;
      }
    }

    public ItemParam ItemParam
    {
      get
      {
        return this.mItemParam;
      }
    }

    public RarityParam RarityParam
    {
      get
      {
        return this.mRarityParam;
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

    public int Rank
    {
      get
      {
        return this.mRank;
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

    public int Exp
    {
      get
      {
        return this.mExp;
      }
    }

    public SkillData Skill
    {
      get
      {
        return this.mSkill;
      }
    }

    public bool Setup(string item_iname)
    {
      this.Reset();
      if (string.IsNullOrEmpty(item_iname))
        return false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.mItemParam = instance.GetItemParam(item_iname);
      this.mRarityParam = instance.GetRarityParam(this.mItemParam.rare);
      if (!string.IsNullOrEmpty(this.mItemParam.skill))
      {
        int rank = this.CalcRank();
        this.mSkill = new SkillData();
        this.mSkill.Setup(this.mItemParam.skill, rank, (int) this.mRarityParam.EquipEnhanceParam.rankcap, (MasterParam) null);
      }
      return true;
    }

    public void Reset()
    {
      this.mUniqueID = 0L;
      this.mItemParam = (ItemParam) null;
      this.mExp = 0;
      this.mRank = 1;
      this.mSkill = (SkillData) null;
      this.mEquiped = false;
    }

    public void Equip(Json_Equip json)
    {
      if (json == null)
        return;
      this.Equip(json.iname, json.iid, json.exp);
    }

    public void Equip(string iname, long iid, int exp)
    {
      if (!this.IsValid() || this.mItemParam.iname != iname || iid == 0L)
        return;
      this.mUniqueID = iid;
      this.mExp = exp;
      this.mRank = this.CalcRank();
      this.mEquiped = true;
      if (this.mSkill == null && !string.IsNullOrEmpty(this.mItemParam.skill))
        this.mSkill = new SkillData();
      if (this.mSkill == null)
        return;
      this.mSkill.Setup(this.mItemParam.skill, this.mRank, this.GetRankCap(), (MasterParam) null);
    }

    public bool IsValid()
    {
      return this.mItemParam != null;
    }

    public bool IsEquiped()
    {
      return this.mEquiped;
    }

    public int GetRankCap()
    {
      if (this.mRarityParam != null)
        return (int) this.RarityParam.EquipEnhanceParam.rankcap;
      return 1;
    }

    public int GetNextExp(int rank)
    {
      RarityEquipEnhanceParam equipEnhanceParam = this.RarityParam == null ? (RarityEquipEnhanceParam) null : this.RarityParam.EquipEnhanceParam;
      DebugUtility.Assert((rank <= 0 ? 0 : (rank <= (int) equipEnhanceParam.rankcap ? 1 : 0)) != 0, "アイテムのレアリティ" + (object) this.mItemParam.rare + "には指定ランク" + (object) rank + "の情報に存在しない。");
      int index = rank - 1;
      if (index < (int) equipEnhanceParam.rankcap)
        return (int) equipEnhanceParam.ranks[index].need_point;
      return 0;
    }

    public int GetNeedExp(int rank)
    {
      RarityEquipEnhanceParam equipEnhanceParam = this.RarityParam == null ? (RarityEquipEnhanceParam) null : this.RarityParam.EquipEnhanceParam;
      DebugUtility.Assert((rank <= 0 ? 0 : (rank <= (int) equipEnhanceParam.rankcap ? 1 : 0)) != 0, "アイテムのレアリティ" + (object) this.mItemParam.rare + "には指定ランク" + (object) rank + "の情報に存在しない。");
      int num = 0;
      for (int index = 0; index < rank; ++index)
        num += (int) equipEnhanceParam.ranks[index].need_point;
      return num;
    }

    public int CalcRank()
    {
      return this.CalcRankFromExp(this.Exp);
    }

    public int CalcRankFromExp(int current)
    {
      int rankCap = this.GetRankCap();
      int num = 0;
      int val1 = 0;
      for (int index = 0; index < rankCap; ++index)
      {
        num += this.GetNextExp(index + 1);
        if (num <= current)
          ++val1;
      }
      return Math.Min(Math.Max(val1, 1), rankCap);
    }

    public void UpdateParam()
    {
      if (this.mSkill == null)
        return;
      this.mSkill.UpdateParam();
    }

    public int GetExp()
    {
      return this.GetExpFromExp(this.Exp);
    }

    public int GetExpFromExp(int current)
    {
      int needExp = this.GetNeedExp(this.CalcRankFromExp(current));
      return current - needExp;
    }

    public int GetNextExp()
    {
      return this.GetNextExpFromExp(this.Exp);
    }

    public int GetNextExpFromExp(int current)
    {
      int rankCap = this.GetRankCap();
      int num = 0;
      for (int index = 0; index < rankCap; ++index)
      {
        num += this.GetNextExp(index + 1);
        if (num > current)
          return num - current;
      }
      return 0;
    }

    public void GainExp(int exp)
    {
      this.mExp += exp;
      this.mRank = this.CalcRank();
      if (this.mSkill == null || this.ItemParam == null)
        return;
      this.mSkill.Setup(this.ItemParam.skill, this.mRank, this.GetRankCap(), (MasterParam) null);
    }

    public int GetEnhanceCostScale()
    {
      if (this.RarityParam == null || this.RarityParam.EquipEnhanceParam == null)
        return 0;
      return (int) this.RarityParam.EquipEnhanceParam.cost_scale;
    }

    public List<ItemData> GetReturnItemList()
    {
      if (!this.IsValid() || !this.IsEquiped())
        return (List<ItemData>) null;
      RarityEquipEnhanceParam equipEnhanceParam = this.RarityParam == null ? (RarityEquipEnhanceParam) null : this.RarityParam.EquipEnhanceParam;
      if (equipEnhanceParam == null || equipEnhanceParam.ranks == null)
        return (List<ItemData>) null;
      RarityEquipEnhanceParam.RankParam rankParam = equipEnhanceParam.GetRankParam(this.Rank);
      if (rankParam == null || rankParam.return_item == null)
        return (List<ItemData>) null;
      ReturnItem[] returnItem = rankParam.return_item;
      List<ItemData> itemDataList = new List<ItemData>();
      for (int index = 0; index < returnItem.Length; ++index)
      {
        if (!string.IsNullOrEmpty(returnItem[index].iname) && (int) returnItem[index].num > 0)
        {
          ItemData itemData = new ItemData();
          itemData.Setup(0L, returnItem[index].iname, (int) returnItem[index].num);
          itemDataList.Add(itemData);
        }
      }
      return itemDataList;
    }

    public override string ToString()
    {
      return string.Format("ItemParam=[{0}] ({1})", (object) this.ItemParam, (object) this.GetType().Name);
    }
  }
}
