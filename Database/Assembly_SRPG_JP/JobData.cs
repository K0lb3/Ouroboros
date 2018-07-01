// Decompiled with JetBrains decompiler
// Type: SRPG.JobData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class JobData
  {
    public static EAbilitySlot[] ABILITY_SLOT_TYPES = new EAbilitySlot[5]
    {
      EAbilitySlot.Action,
      EAbilitySlot.Action,
      EAbilitySlot.Reaction,
      EAbilitySlot.Support,
      EAbilitySlot.Support
    };
    public static ArtifactTypes[] ARTIFACT_SLOT_TYPES = new ArtifactTypes[3]
    {
      ArtifactTypes.Arms,
      ArtifactTypes.Armor,
      ArtifactTypes.Accessory
    };
    private OInt mRank = (OInt) 0;
    private SkillData mNormalAttackSkill = new SkillData();
    private EquipData[] mEquips = new EquipData[6];
    private List<AbilityData> mLearnAbilitys = new List<AbilityData>();
    private long[] mAbilitySlots = new long[5];
    private long[] mArtifacts = new long[3];
    private ArtifactData[] mArtifactDatas = new ArtifactData[3];
    public const int MAX_RANKUP_EQUIPS = 6;
    public const int MAX_LARNING_ABILITY = 8;
    public const int MAX_ABILITY_SLOT = 5;
    public const int MAX_ARTIFACT_SLOT = 3;
    public const int FIXED_ABILITY_SLOT_INDEX = 0;
    private UnitData mOwner;
    private long mUniqueID;
    private JobParam mJobParam;
    private SkillData mJobMaster;
    private string mSelectSkin;
    private ArtifactData mSelectSkinData;

    public JobData()
    {
      for (int index = 0; index < this.mEquips.Length; ++index)
        this.mEquips[index] = new EquipData();
    }

    public static int GetArtifactSlotIndex(ArtifactTypes type)
    {
      for (int index = 0; index < JobData.ARTIFACT_SLOT_TYPES.Length; ++index)
      {
        if (type == JobData.ARTIFACT_SLOT_TYPES[index])
          return index;
      }
      return -1;
    }

    public UnitData Owner
    {
      get
      {
        return this.mOwner;
      }
    }

    public JobParam Param
    {
      get
      {
        return this.mJobParam;
      }
    }

    public long UniqueID
    {
      get
      {
        return this.mUniqueID;
      }
      set
      {
        this.mUniqueID = value;
      }
    }

    public string JobID
    {
      get
      {
        if (this.Param != null)
          return this.Param.iname;
        return (string) null;
      }
    }

    public string Name
    {
      get
      {
        if (this.Param != null)
          return this.Param.name;
        return (string) null;
      }
    }

    public int Rank
    {
      get
      {
        return (int) this.mRank;
      }
    }

    public JobTypes JobType
    {
      get
      {
        if (this.Param != null)
          return this.Param.type;
        return JobTypes.Attacker;
      }
    }

    public RoleTypes RoleType
    {
      get
      {
        if (this.Param != null)
          return this.Param.role;
        return RoleTypes.Zenei;
      }
    }

    public string JobResourceID
    {
      get
      {
        if (this.Param != null)
          return this.Param.model;
        return (string) null;
      }
    }

    public EquipData[] Equips
    {
      get
      {
        return this.mEquips;
      }
    }

    public List<AbilityData> LearnAbilitys
    {
      get
      {
        return this.mLearnAbilitys;
      }
    }

    public long[] AbilitySlots
    {
      get
      {
        return this.mAbilitySlots;
      }
    }

    public long[] Artifacts
    {
      get
      {
        return this.mArtifacts;
      }
    }

    public ArtifactData[] ArtifactDatas
    {
      get
      {
        return this.mArtifactDatas;
      }
    }

    public string SelectedSkin
    {
      get
      {
        return this.mSelectSkin;
      }
      set
      {
        this.mSelectSkin = value;
      }
    }

    public bool IsActivated
    {
      get
      {
        return this.Rank > 0;
      }
    }

    public SkillData JobMaster
    {
      get
      {
        if (this.CheckJobMaster())
          return this.mJobMaster;
        return (SkillData) null;
      }
    }

    public void Deserialize(UnitData owner, Json_Job json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.mJobParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(json.iname);
      this.mUniqueID = json.iid;
      this.mRank = (OInt) json.rank;
      this.mOwner = owner;
      this.mSelectSkin = json.cur_skin;
      for (int index = 0; index < this.mEquips.Length; ++index)
        this.mEquips[index].Setup(this.mJobParam.GetRankupItemID((int) this.mRank, index));
      if (json.equips != null)
      {
        for (int index = 0; index < json.equips.Length; ++index)
          this.mEquips[index].Equip(json.equips[index]);
      }
      if (!string.IsNullOrEmpty(this.Param.atkskill[0]))
        this.mNormalAttackSkill.Setup(this.Param.atkskill[0], 1, 1, (MasterParam) null);
      else
        this.mNormalAttackSkill.Setup(this.Param.atkskill[(int) owner.UnitParam.element], 1, 1, (MasterParam) null);
      if (!string.IsNullOrEmpty(this.Param.master) && MonoSingleton<GameManager>.Instance.MasterParam.FixParam.IsJobMaster)
      {
        if (this.mJobMaster == null)
          this.mJobMaster = new SkillData();
        this.mJobMaster.Setup(this.Param.master, 1, 1, (MasterParam) null);
      }
      if (json.abils != null)
      {
        Array.Sort<Json_Ability>(json.abils, (Comparison<Json_Ability>) ((src, dsc) => (int) (src.iid - dsc.iid)));
        for (int index = 0; index < json.abils.Length; ++index)
        {
          AbilityData abilityData = new AbilityData();
          string iname = json.abils[index].iname;
          long iid = json.abils[index].iid;
          int exp = json.abils[index].exp;
          abilityData.Setup(this.mOwner, iid, iname, exp, 0);
          this.mLearnAbilitys.Add(abilityData);
        }
      }
      Array.Clear((Array) this.mAbilitySlots, 0, this.mAbilitySlots.Length);
      if (json.select != null && json.select.abils != null)
      {
        for (int index = 0; index < json.select.abils.Length && index < this.mAbilitySlots.Length; ++index)
          this.mAbilitySlots[index] = json.select.abils[index];
      }
      for (int index = 0; index < this.mArtifactDatas.Length; ++index)
        this.mArtifactDatas[index] = (ArtifactData) null;
      Array.Clear((Array) this.mArtifacts, 0, this.mArtifacts.Length);
      if (json.select != null && json.select.artifacts != null)
      {
        for (int index = 0; index < json.select.artifacts.Length && index < this.mArtifacts.Length; ++index)
          this.mArtifacts[index] = json.select.artifacts[index];
      }
      if (json.artis != null)
      {
        for (int index1 = 0; index1 < json.artis.Length; ++index1)
        {
          if (json.artis[index1] != null)
          {
            int index2 = Array.IndexOf<long>(this.mArtifacts, json.artis[index1].iid);
            if (index2 >= 0)
            {
              ArtifactData artifactData = new ArtifactData();
              artifactData.Deserialize(json.artis[index1]);
              this.mArtifactDatas[index2] = artifactData;
            }
          }
        }
      }
      if (string.IsNullOrEmpty(json.cur_skin))
        return;
      ArtifactData artifactData1 = new ArtifactData();
      artifactData1.Deserialize(new Json_Artifact()
      {
        iname = json.cur_skin
      });
      this.mSelectSkinData = artifactData1;
    }

    public void UnlockSkillAll()
    {
      for (int index = 0; index < this.LearnAbilitys.Count; ++index)
        this.LearnAbilitys[index].UpdateLearningsSkill(false, (List<SkillData>) null);
    }

    public SkillData GetAttackSkill()
    {
      return this.mNormalAttackSkill;
    }

    public int GetJobRankAvoidRate()
    {
      return this.GetJobRankAvoidRate(this.Rank);
    }

    public int GetJobRankAvoidRate(int rank)
    {
      if (this.Param != null)
        return this.Param.GetJobRankAvoidRate(rank);
      return 0;
    }

    public int GetJobRankInitJewelRate()
    {
      return this.GetJobRankInitJewelRate(this.Rank);
    }

    public int GetJobRankInitJewelRate(int rank)
    {
      if (this.Param != null)
        return this.Param.GetJobRankInitJewelRate(rank);
      return 0;
    }

    public StatusParam GetJobRankStatus()
    {
      return this.GetJobRankStatus(this.Rank);
    }

    public StatusParam GetJobRankStatus(int rank)
    {
      if (this.Param != null)
        return this.Param.GetJobRankStatus(rank);
      return (StatusParam) null;
    }

    public BaseStatus GetJobTransfarStatus(EElement element)
    {
      return this.GetJobTransfarStatus(this.Rank, element);
    }

    public BaseStatus GetJobTransfarStatus(int rank, EElement element)
    {
      if (this.Param != null)
        return this.Param.GetJobTransfarStatus(rank, element);
      return (BaseStatus) null;
    }

    public OString[] GetLearningAbilitys(int rank)
    {
      if (this.Param != null)
        return this.Param.GetLearningAbilitys(rank);
      return (OString[]) null;
    }

    public int GetJobChangeCost(int rank)
    {
      if (this.Param != null)
        return this.Param.GetJobChangeCost(rank);
      return 0;
    }

    public string[] GetJobChangeItems(int rank)
    {
      if (this.Param != null)
        return this.Param.GetJobChangeItems(rank);
      return (string[]) null;
    }

    public int[] GetJobChangeItemNums(int rank)
    {
      if (this.Param != null)
        return this.Param.GetJobChangeItemNums(rank);
      return (int[]) null;
    }

    public int GetRankupCost(int rank)
    {
      if (this.Param != null)
        return this.Param.GetRankupCost(rank);
      return 0;
    }

    public string[] GetRankupItems(int rank)
    {
      if (this.Param != null)
        return this.Param.GetRankupItems(rank);
      return (string[]) null;
    }

    public string GetRankupItemID(int rank, int index)
    {
      if (this.Param != null)
        return this.Param.GetRankupItemID(rank, index);
      return (string) null;
    }

    public int FindEquipSlotByItemID(string iname)
    {
      int num = -1;
      for (int index = 0; index < 6; ++index)
      {
        string rankupItemId = this.GetRankupItemID(this.Rank, index);
        if (!string.IsNullOrEmpty(rankupItemId) && !(rankupItemId != iname))
        {
          num = index;
          break;
        }
      }
      return num;
    }

    public bool CheckEnableEquipSlot(int index)
    {
      return index >= 0 && index < this.mEquips.Length && (this.mEquips[index] != null && !this.mEquips[index].IsEquiped()) && (this.Owner == null || this.GetEnableEquipUnitLevel(index) <= this.Owner.Lv);
    }

    public bool Equip(int index)
    {
      if (!this.CheckEnableEquipSlot(index))
        return false;
      string rankupItemId = this.GetRankupItemID(this.Rank, index);
      this.mEquips[index].Setup(rankupItemId);
      this.mEquips[index].Equip((Json_Equip) null);
      return true;
    }

    public void JobRankUp()
    {
      ++this.mRank;
      for (int index = 0; index < this.mEquips.Length; ++index)
        this.mEquips[index].Setup(this.GetRankupItemID(this.Rank, index));
      if ((int) this.mRank == 1 && !string.IsNullOrEmpty(this.mJobParam.fixed_ability) && this.mLearnAbilitys.Find((Predicate<AbilityData>) (p => p.AbilityID == this.mJobParam.fixed_ability)) == null)
      {
        AbilityData ability = new AbilityData();
        List<AbilityData> learnedAbilities = this.mOwner.GetAllLearnedAbilities(false);
        string fixedAbility = this.mJobParam.fixed_ability;
        long val1 = 0;
        int exp = 0;
        for (int index = 0; index < learnedAbilities.Count; ++index)
          val1 = Math.Max(val1, learnedAbilities[index].UniqueID);
        long iid = val1 + 1L;
        try
        {
          ability.Setup(this.mOwner, iid, fixedAbility, exp, 0);
          this.mLearnAbilitys.Add(ability);
          this.SetAbilitySlot(0, ability);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
      OString[] learningAbilitys = this.GetLearningAbilitys((int) this.mRank);
      if (learningAbilitys == null)
        return;
      for (int index1 = 0; index1 < learningAbilitys.Length; ++index1)
      {
        string abilityID = (string) learningAbilitys[index1];
        if (!string.IsNullOrEmpty(abilityID) && this.mLearnAbilitys.Find((Predicate<AbilityData>) (p => p.AbilityID == abilityID)) == null)
        {
          AbilityData abilityData = new AbilityData();
          string iname = abilityID;
          List<AbilityData> learnedAbilities = this.mOwner.GetAllLearnedAbilities(false);
          long val1 = 0;
          int exp = 0;
          for (int index2 = 0; index2 < learnedAbilities.Count; ++index2)
            val1 = Math.Max(val1, learnedAbilities[index2].UniqueID);
          long iid = val1 + 1L;
          try
          {
            abilityData.Setup(this.mOwner, iid, iname, exp, 0);
            this.mLearnAbilitys.Add(abilityData);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
    }

    public int GetJobRankCap(UnitData self)
    {
      return JobParam.GetJobRankCap(self.Rarity);
    }

    public bool CheckJobRankUp(UnitData self, bool canCreate = false, bool useCommon = true)
    {
      if (this.Param == null || this.Rank >= this.GetJobRankCap(self))
        return false;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      NeedEquipItemList needEquipItemList = new NeedEquipItemList();
      if (canCreate)
      {
        if (!player.CheckEnableCreateEquipItemAll(self, this.mEquips, !useCommon ? (NeedEquipItemList) null : needEquipItemList) && !needEquipItemList.IsEnoughCommon())
          return false;
      }
      else
      {
        for (int index = 0; index < 6; ++index)
        {
          if (this.mEquips[index] == null || !this.mEquips[index].IsEquiped())
            return false;
        }
      }
      return true;
    }

    public bool CanAllEquip(ref int cost, ref Dictionary<string, int> equips, ref Dictionary<string, int> consumes, ref int target_rank, ref bool can_jobmaster, ref bool can_jobmax, NeedEquipItemList item_list = null, bool all = false)
    {
      if (all)
        return MonoSingleton<GameManager>.Instance.Player.CheckEnable2(this.Owner, this.Equips, ref consumes, ref cost, ref target_rank, ref can_jobmaster, ref can_jobmax, (NeedEquipItemList) null);
      return MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.Owner, this.Equips, ref consumes, ref cost, item_list);
    }

    public bool CanAllEquip(ref int cost, ref Dictionary<string, int> equips, ref Dictionary<string, int> consumes, NeedEquipItemList item_list = null)
    {
      return MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.Owner, this.Equips, ref consumes, ref cost, item_list);
    }

    public int GetEnableEquipUnitLevel(int slot)
    {
      if (this.mEquips[slot] == null || this.mEquips[slot].ItemParam == null)
        return 0;
      return this.mEquips[slot].ItemParam.equipLv;
    }

    public void SetAbilitySlot(int slot, AbilityData ability)
    {
      DebugUtility.Assert(slot >= 0 && slot < this.mAbilitySlots.Length, "EquipAbility Out of Length");
      long num = 0;
      if (ability != null)
      {
        AbilityParam abilityParam = ability.Param;
        if (abilityParam.slot != JobData.ABILITY_SLOT_TYPES[slot])
        {
          DebugUtility.LogError("指定スロットに対応するアビリティではない");
          return;
        }
        if (slot != 0 && abilityParam.is_fixed)
        {
          DebugUtility.LogError("指定スロットには固定アビリティは装備不可能");
          return;
        }
        num = ability.UniqueID;
      }
      this.mAbilitySlots[slot] = num;
    }

    public bool SetEquipArtifact(int slot, ArtifactData artifact)
    {
      DebugUtility.Assert(slot >= 0 && slot < this.mArtifacts.Length, "SetArtifact Out of Length");
      long num = 0;
      if (artifact != null)
      {
        if (!this.CheckEquipArtifact(slot, artifact))
          return false;
        num = (long) artifact.UniqueID;
      }
      this.mArtifacts[slot] = num;
      this.mArtifactDatas[slot] = artifact;
      return true;
    }

    public bool CheckEquipArtifact(int slot, ArtifactData artifact)
    {
      if (artifact == null || this.mArtifacts == null || (slot < 0 || slot >= this.mArtifacts.Length) || this.Owner == null)
        return false;
      int jobIndex = Array.IndexOf<JobData>(this.Owner.Jobs, this);
      if (jobIndex < 0 || !artifact.CheckEnableEquip(this.Owner, jobIndex))
        return false;
      ArtifactParam artifactParam = artifact.ArtifactParam;
      if (artifactParam.type == ArtifactTypes.Accessory || artifactParam.type == JobData.ARTIFACT_SLOT_TYPES[slot])
        return true;
      DebugUtility.LogError("ArtifactSlot mismatch");
      return false;
    }

    public ArtifactData GetSelectedSkinData()
    {
      if (this.mSelectSkinData != null && this.mSelectSkin == this.mSelectSkinData.ArtifactParam.iname)
        return this.mSelectSkinData;
      ArtifactParam artifactParam = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (a => a.iname == this.mSelectSkin));
      if (artifactParam == null)
        return (ArtifactData) null;
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iname = artifactParam.iname
      });
      this.mSelectSkinData = artifactData;
      return this.mSelectSkinData;
    }

    public bool CheckJobMaster()
    {
      if (!MonoSingleton<GameManager>.Instance.MasterParam.FixParam.IsJobMaster || this.Equips == null || this.Rank < JobParam.MAX_JOB_RANK)
        return false;
      bool flag = true;
      for (int index = 0; index < this.Equips.Length; ++index)
      {
        EquipData equip = this.Equips[index];
        if (equip == null || !equip.IsValid() || !equip.IsEquiped())
        {
          flag = false;
          break;
        }
      }
      return flag;
    }
  }
}
