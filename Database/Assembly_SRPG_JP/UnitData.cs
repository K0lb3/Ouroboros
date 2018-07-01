// Decompiled with JetBrains decompiler
// Type: SRPG.UnitData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitData
  {
    private static BaseStatus UnitScaleStatus = new BaseStatus();
    private static BaseStatus WorkScaleStatus = new BaseStatus();
    private static readonly Dictionary<string, string[]> CONDITIONS_TARGET_NAMES = new Dictionary<string, string[]>()
    {
      {
        "UNIT_JOB_MASTER1",
        new string[3]{ "voice_0017", "voice_0018", "voice_0019" }
      },
      {
        "UNIT_LEVEL85",
        new string[10]
        {
          "chara_0002",
          "chara_0003",
          "chara_0004",
          "chara_0005",
          "chara_0006",
          "chara_0007",
          "chara_0008",
          "chara_0009",
          "chara_0010",
          "chara_0011"
        }
      },
      {
        "UNIT_LEVEL75",
        new string[8]
        {
          "sys_0001",
          "sys_0002",
          "sys_0003",
          "sys_0033",
          "sys_0035",
          "sys_0046",
          "sys_0047",
          "sys_0050"
        }
      },
      {
        "UNIT_LEVEL65",
        new string[9]
        {
          "sys_0009",
          "sys_0013",
          "sys_0015",
          "sys_0017",
          "sys_0019",
          "sys_0023",
          "sys_0024",
          "sys_0026",
          "sys_0028"
        }
      }
    };
    private BaseStatus mStatus = new BaseStatus();
    private OInt mLv = (OInt) 1;
    private OInt mExp = (OInt) 0;
    private OInt mRarity = (OInt) 0;
    private OInt mAwakeLv = (OInt) 0;
    private OInt mElement = (OInt) 0;
    private OInt mJobIndex = (OInt) 0;
    private List<ArtifactParam> mUnlockedSkins = new List<ArtifactParam>();
    private List<AbilityData> mLearnAbilitys = new List<AbilityData>();
    private List<AbilityData> mBattleAbilitys = new List<AbilityData>(11);
    private List<SkillData> mBattleSkills = new List<SkillData>(11);
    private List<AbilityData> mTobiraMasterAbilitys = new List<AbilityData>();
    private List<SRPG.TobiraData> mTobiraData = new List<SRPG.TobiraData>();
    private OInt mUnlockTobiraNum = (OInt) 0;
    public const int MAX_JOB = 4;
    public const int MAX_MASTER_ABILITY = 1;
    public const int MAX_JOB_ABILITY = 5;
    public const int MAX_EQUIP_ABILITY = 5;
    public const int MAX_USED_ABILITY = 11;
    private long mUniqueID;
    private UnitParam mUnitParam;
    public float LastSyncTime;
    private bool mFavorite;
    private EElement mSupportElement;
    private JobData[] mJobs;
    private long[] mPartyJobs;
    public UnitData.TemporaryFlags TempFlags;
    private SkillData mLeaderSkill;
    private AbilityData mMasterAbility;
    private AbilityData mCollaboAbility;
    private AbilityData mMapEffectAbility;
    private SkillData mNormalAttackSkill;
    private QuestClearUnlockUnitDataParam mUnlockedLeaderSkill;
    private List<QuestClearUnlockUnitDataParam> mUnlockedAbilitys;
    private List<QuestClearUnlockUnitDataParam> mUnlockedSkills;
    private List<QuestClearUnlockUnitDataParam> mSkillUnlocks;
    private Dictionary<string, UnitJobOverwriteParam> mUnitJobOverwriteParams;
    private ConceptCardData mConceptCard;
    private Dictionary<string, SkillAbilityDeriveData> mJobSkillAbilityDeriveData;
    private SkillAbilityDeriveData mSkillAbilityDeriveData;
    private int mNumJobsAvailable;
    public UnitBadgeTypes BadgeState;
    private List<QuestParam> mCharacterQuests;

    public List<QuestClearUnlockUnitDataParam> SkillUnlocks
    {
      get
      {
        return this.mSkillUnlocks;
      }
    }

    public long UniqueID
    {
      get
      {
        return this.mUniqueID;
      }
    }

    public UnitParam UnitParam
    {
      get
      {
        return this.mUnitParam;
      }
    }

    public string UnitID
    {
      get
      {
        if (this.UnitParam != null)
          return this.UnitParam.iname;
        return (string) null;
      }
    }

    public BaseStatus Status
    {
      get
      {
        return this.mStatus;
      }
    }

    public int Lv
    {
      get
      {
        return (int) this.mLv;
      }
    }

    public int Exp
    {
      get
      {
        return (int) this.mExp;
      }
    }

    public int Rarity
    {
      get
      {
        return (int) this.mRarity;
      }
    }

    public int AwakeLv
    {
      get
      {
        return (int) this.mAwakeLv;
      }
    }

    public EquipData[] CurrentEquips
    {
      get
      {
        if (this.CurrentJob != null)
          return this.CurrentJob.Equips;
        return (EquipData[]) null;
      }
    }

    public SkillData LeaderSkill
    {
      get
      {
        return this.mLeaderSkill;
      }
    }

    public AbilityData MasterAbility
    {
      get
      {
        return this.mMasterAbility;
      }
    }

    public AbilityData CollaboAbility
    {
      get
      {
        return this.mCollaboAbility;
      }
    }

    public AbilityData MapEffectAbility
    {
      get
      {
        return this.mMapEffectAbility;
      }
    }

    public List<AbilityData> TobiraMasterAbilitys
    {
      get
      {
        return this.mTobiraMasterAbilitys;
      }
    }

    public long[] CurrentAbilitySlots
    {
      get
      {
        if (this.CurrentJob != null)
          return this.CurrentJob.AbilitySlots;
        return (long[]) null;
      }
    }

    public List<AbilityData> LearnAbilitys
    {
      get
      {
        return this.mLearnAbilitys;
      }
    }

    public List<AbilityData> BattleAbilitys
    {
      get
      {
        return this.mBattleAbilitys;
      }
    }

    public List<SkillData> BattleSkills
    {
      get
      {
        return this.mBattleSkills;
      }
    }

    public EElement Element
    {
      get
      {
        return (EElement) (int) this.mElement;
      }
    }

    public JobTypes JobType
    {
      get
      {
        if (this.CurrentJob != null)
          return this.CurrentJob.JobType;
        if (this.UnitParam.no_job_status != null)
          return this.UnitParam.no_job_status.jobtype;
        return JobTypes.None;
      }
    }

    public RoleTypes RoleType
    {
      get
      {
        if (this.CurrentJob != null)
          return this.CurrentJob.RoleType;
        if (this.UnitParam.no_job_status != null)
          return this.UnitParam.no_job_status.role;
        return RoleTypes.None;
      }
    }

    public ConceptCardData ConceptCard
    {
      get
      {
        return this.mConceptCard;
      }
      set
      {
        this.mConceptCard = value;
      }
    }

    public int NumJobsAvailable
    {
      get
      {
        return this.mNumJobsAvailable;
      }
    }

    public bool IsJobAvailable(int jobNo)
    {
      if (0 <= jobNo)
        return jobNo < this.mNumJobsAvailable;
      return false;
    }

    public JobData CurrentJob
    {
      get
      {
        if (this.mJobs == null || (int) this.mJobIndex < 0 || this.mJobs.Length <= (int) this.mJobIndex)
          return (JobData) null;
        return this.mJobs[(int) this.mJobIndex];
      }
    }

    public string CurrentJobId
    {
      get
      {
        if (this.CurrentJob != null)
          return this.CurrentJob.JobID;
        return string.Empty;
      }
    }

    public bool IsUnlockTobira
    {
      get
      {
        SRPG.TobiraData tobiraData = this.mTobiraData.Find((Predicate<SRPG.TobiraData>) (tobira =>
        {
          if (tobira.Param == null)
            return false;
          return tobira.Param.TobiraCategory == TobiraParam.Category.START;
        }));
        if (tobiraData != null)
          return tobiraData.IsUnlocked;
        return false;
      }
    }

    public List<SRPG.TobiraData> TobiraData
    {
      get
      {
        return this.mTobiraData;
      }
    }

    public int UnlockTobriaNum
    {
      get
      {
        return (int) this.mUnlockTobiraNum;
      }
    }

    public JobData GetBaseJob(string jobID)
    {
      JobSetParam classChangeBase2 = this.FindClassChangeBase2(jobID);
      if (classChangeBase2 == null)
        return (JobData) null;
      for (int index = 0; index < this.mNumJobsAvailable; ++index)
      {
        if (this.mJobs[index].JobID == classChangeBase2.job)
          return this.mJobs[index];
      }
      return (JobData) null;
    }

    public JobData FindJobDataBySkillData(SkillParam param)
    {
      if (param == null || this.mJobs == null)
        return (JobData) null;
      string hitAbliId = string.Empty;
      for (int index = 0; index < this.BattleAbilitys.Count; ++index)
      {
        if (Array.FindIndex<SkillData>(this.BattleAbilitys[index].Skills.ToArray(), (Predicate<SkillData>) (p => p.SkillID == param.iname)) != -1)
          hitAbliId = this.BattleAbilitys[index].AbilityID;
      }
      if (!string.IsNullOrEmpty(hitAbliId))
      {
        for (int index = 0; index < this.mJobs.Length; ++index)
        {
          JobData mJob = this.mJobs[index];
          if (mJob != null)
          {
            for (int rank = 0; rank < JobParam.MAX_JOB_RANK; ++rank)
            {
              OString[] learningAbilitys = mJob.GetLearningAbilitys(rank);
              if (learningAbilitys != null && Array.FindIndex<OString>(learningAbilitys, (Predicate<OString>) (name => (string) name == hitAbliId)) != -1)
                return mJob;
            }
            if (mJob.Param != null && mJob.Param.fixed_ability == hitAbliId)
              return mJob;
          }
        }
      }
      if (!string.IsNullOrEmpty(param.job) && this.mMasterAbility != null && this.mMasterAbility.LearningSkills != null)
      {
        for (int index1 = 0; index1 < this.mMasterAbility.LearningSkills.Length; ++index1)
        {
          LearningSkill learningSkill = this.mMasterAbility.LearningSkills[index1];
          if (learningSkill.iname != null && learningSkill.iname == param.iname)
          {
            for (int index2 = 0; index2 < this.mJobs.Length; ++index2)
            {
              if (this.mJobs[index2] != null && this.mJobs[index2].JobID == param.job)
                return this.mJobs[index2];
            }
          }
        }
      }
      if (!string.IsNullOrEmpty(param.job) && this.mMapEffectAbility != null && this.mMapEffectAbility.LearningSkills != null)
      {
        foreach (LearningSkill learningSkill in this.mMapEffectAbility.LearningSkills)
        {
          if (learningSkill.iname != null && learningSkill.iname == param.iname)
          {
            for (int index = 0; index < this.mJobs.Length; ++index)
            {
              if (this.mJobs[index] != null && this.mJobs[index].JobID == param.job)
                return this.mJobs[index];
            }
          }
        }
      }
      return (JobData) null;
    }

    public ArtifactData FindArtifactDataBySkillParam(SkillParam param)
    {
      if (param == null || this.mJobs == null)
        return (ArtifactData) null;
      for (int index1 = 0; index1 < this.mJobs.Length; ++index1)
      {
        JobData mJob = this.mJobs[index1];
        if (mJob != null && mJob.ArtifactDatas != null)
        {
          for (int index2 = 0; index2 < mJob.ArtifactDatas.Length; ++index2)
          {
            ArtifactData artifactData = mJob.ArtifactDatas[index2];
            if (artifactData != null && artifactData.LearningAbilities != null)
            {
              for (int index3 = 0; index3 < artifactData.LearningAbilities.Count; ++index3)
              {
                AbilityData learningAbility = artifactData.LearningAbilities[index3];
                if (learningAbility != null && learningAbility.LearningSkills != null)
                {
                  for (int index4 = 0; index4 < learningAbility.LearningSkills.Length; ++index4)
                  {
                    if (learningAbility.LearningSkills[index4].iname == param.iname)
                      return artifactData;
                  }
                }
              }
            }
          }
        }
      }
      return (ArtifactData) null;
    }

    public JobData[] Jobs
    {
      get
      {
        return this.mJobs;
      }
    }

    public int JobCount
    {
      get
      {
        return this.mJobs.Length;
      }
    }

    public int JobIndex
    {
      get
      {
        return (int) this.mJobIndex;
      }
    }

    public string SexPrefix
    {
      get
      {
        return this.UnitParam.SexPrefix;
      }
    }

    public bool IsFavorite
    {
      set
      {
        this.mFavorite = value;
      }
      get
      {
        return this.mFavorite;
      }
    }

    public EElement SupportElement
    {
      get
      {
        return this.mSupportElement;
      }
    }

    public bool IsIntoUnit
    {
      get
      {
        if (this.UnitParam != null)
          return this.UnitParam.IsStopped();
        return false;
      }
    }

    public bool IsSkinUnlocked()
    {
      return this.mUnlockedSkins.Count + this.GetEnableConceptCardSkins(-1).Length >= 1;
    }

    public bool IsSetSkin(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      if (this.mJobs != null && this.mJobs[this.JobIndex] != null)
        return !string.IsNullOrEmpty(this.mJobs[this.JobIndex].SelectedSkin);
      return false;
    }

    public ArtifactParam GetSelectedSkinData(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      if (this.mJobs == null || this.mJobs[jobIndex] == null || string.IsNullOrEmpty(this.mJobs[jobIndex].SelectedSkin))
        return (ArtifactParam) null;
      return Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (p => p.iname == this.mJobs[jobIndex].SelectedSkin));
    }

    public ArtifactParam GetSelectedSkin(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      return this.GetSelectedSkinData(jobIndex);
    }

    public ArtifactParam[] GetSelectedSkins()
    {
      if (this.Jobs == null && this.Jobs.Length < 1)
        return new ArtifactParam[0];
      ArtifactParam[] artifactParamArray = new ArtifactParam[this.Jobs.Length];
      for (int jobIndex = 0; jobIndex < artifactParamArray.Length; ++jobIndex)
        artifactParamArray[jobIndex] = this.GetSelectedSkinData(jobIndex);
      return artifactParamArray;
    }

    public void SetJobSkin(string afName, int jobIndex = -1, bool is_need_check = true)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      if (string.IsNullOrEmpty(afName))
      {
        this.mJobs[jobIndex].SelectedSkin = (string) null;
      }
      else
      {
        ArtifactParam artifactParam = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (p => p.iname == afName));
        if (artifactParam == null)
          return;
        if (this.mConceptCard != null && this.mConceptCard.GetEnableEquipEffects(this, this.mJobs[jobIndex]).FindIndex((Predicate<ConceptCardEquipEffect>) (effect => effect.Skin == afName)) >= 0)
        {
          this.mJobs[jobIndex].SelectedSkin = afName;
        }
        else
        {
          if (is_need_check && (!artifactParam.CheckEnableEquip(this, jobIndex) || !this.CheckUsedSkin(artifactParam.iname)))
            return;
          this.mJobs[jobIndex].SelectedSkin = afName;
        }
      }
    }

    public void SetJobSkinAll(string afName)
    {
      if (string.IsNullOrEmpty(afName))
      {
        this.ResetJobSkinAll();
      }
      else
      {
        ArtifactParam artifactParam = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (p => p.iname == afName));
        if (artifactParam == null)
        {
          this.ResetJobSkinAll();
        }
        else
        {
          if (!this.CheckUsedSkin(artifactParam.iname))
            return;
          for (int jobIndex = 0; jobIndex < this.mJobs.Length; ++jobIndex)
            this.mJobs[jobIndex].SelectedSkin = !artifactParam.CheckEnableEquip(this, jobIndex) ? (string) null : afName;
        }
      }
    }

    public void ResetJobSkin(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      this.mJobs[jobIndex].SelectedSkin = (string) null;
    }

    public void ResetJobSkinAll()
    {
      for (int index = 0; index < this.mJobs.Length; ++index)
        this.mJobs[index].SelectedSkin = (string) null;
    }

    public ArtifactParam[] GetSelectableSkins(JobParam jobParam)
    {
      if (jobParam == null)
        return (ArtifactParam[]) null;
      int index = Array.FindIndex<JobData>(this.mJobs, (Predicate<JobData>) (j => j.Param.iname == jobParam.iname));
      if (index == -1)
        return (ArtifactParam[]) null;
      return this.GetSelectableSkins(index);
    }

    public ArtifactParam[] GetSelectableSkins(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
      for (int index = 0; index < this.mUnlockedSkins.Count; ++index)
      {
        ArtifactParam mUnlockedSkin = this.mUnlockedSkins[index];
        if (mUnlockedSkin != null && mUnlockedSkin.CheckEnableEquip(this, this.JobIndex))
          artifactParamList.Add(mUnlockedSkin);
      }
      if (artifactParamList.Count >= 1)
        return artifactParamList.ToArray();
      return new ArtifactParam[0];
    }

    public ArtifactParam[] GetAllSkins(int jobIndex = -1)
    {
      if (this.mUnitParam.skins == null)
        return new ArtifactParam[0];
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
      ArtifactParam[] array = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray();
      for (int i = 0; i < this.mUnitParam.skins.Length; ++i)
      {
        ArtifactParam artifactParam = Array.Find<ArtifactParam>(array, (Predicate<ArtifactParam>) (af => af.iname == this.mUnitParam.skins[i]));
        if (artifactParam != null && artifactParam.CheckEnableEquip(this, this.JobIndex))
          artifactParamList.Add(artifactParam);
      }
      if (artifactParamList.Count >= 1)
        return artifactParamList.ToArray();
      return new ArtifactParam[0];
    }

    public ArtifactParam[] GetEnableConceptCardSkins(int jobIndex = -1)
    {
      if (this.mConceptCard == null)
        return new ArtifactParam[0];
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
      List<ConceptCardEquipEffect> equip_effects = this.mConceptCard.GetEnableEquipEffects(this, this.mJobs[jobIndex]);
      for (int i = 0; i < equip_effects.Count; ++i)
      {
        if (!string.IsNullOrEmpty(equip_effects[i].Skin))
        {
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.Find((Predicate<ArtifactParam>) (art => art.iname == equip_effects[i].Skin));
          if (artifactParam != null)
            artifactParamList.Add(artifactParam);
        }
      }
      return artifactParamList.ToArray();
    }

    public QuestClearUnlockUnitDataParam[] UnlockedSkills
    {
      get
      {
        if (this.mUnlockedSkills != null)
          return this.mUnlockedSkills.ToArray();
        return (QuestClearUnlockUnitDataParam[]) null;
      }
    }

    public string UnlockedSkillIds()
    {
      string empty = string.Empty;
      if (this.mUnlockedLeaderSkill != null)
        empty += this.mUnlockedLeaderSkill.iname;
      if (this.mUnlockedAbilitys != null)
      {
        for (int index = 0; index < this.mUnlockedAbilitys.Count; ++index)
          empty += string.IsNullOrEmpty(empty) ? this.mUnlockedAbilitys[index].iname : "," + this.mUnlockedAbilitys[index].iname;
      }
      if (this.mUnlockedSkills != null)
      {
        for (int index = 0; index < this.mUnlockedSkills.Count; ++index)
          empty += string.IsNullOrEmpty(empty) ? this.mUnlockedSkills[index].iname : "," + this.mUnlockedSkills[index].iname;
      }
      return empty;
    }

    public QuestClearUnlockUnitDataParam[] UnlockedSkillDiff(string oldIds)
    {
      string str1 = this.UnlockedSkillIds();
      string str2 = !string.IsNullOrEmpty(oldIds) ? oldIds : string.Empty;
      string str3 = !string.IsNullOrEmpty(str1) ? str1 : string.Empty;
      if (str2.Length >= str3.Length)
        return new QuestClearUnlockUnitDataParam[0];
      string[] array = str2.Split(',');
      string[] newUnlocks = str3.Split(',');
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>();
      for (int i = 0; i < newUnlocks.Length; ++i)
      {
        if (Array.FindIndex<string>(array, (Predicate<string>) (p => p == newUnlocks[i])) == -1)
        {
          QuestClearUnlockUnitDataParam unlockUnitData = masterParam.GetUnlockUnitData(newUnlocks[i]);
          if (unlockUnitData != null)
            unlockUnitDataParamList.Add(unlockUnitData);
        }
      }
      return unlockUnitDataParamList.ToArray();
    }

    public bool IsQuestClearUnlocked(string id, QuestClearUnlockUnitDataParam.EUnlockTypes type)
    {
      if (!string.IsNullOrEmpty(id))
      {
        QuestClearUnlockUnitDataParam unlockUnitDataParam = (QuestClearUnlockUnitDataParam) null;
        switch (type)
        {
          case QuestClearUnlockUnitDataParam.EUnlockTypes.Skill:
            if (this.mUnlockedSkills != null)
            {
              unlockUnitDataParam = this.mUnlockedSkills.Find((Predicate<QuestClearUnlockUnitDataParam>) (p =>
              {
                if (p.new_id == id)
                  return p.type == type;
                return false;
              }));
              break;
            }
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability:
          case QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility:
            if (this.mUnlockedAbilitys != null)
            {
              unlockUnitDataParam = this.mUnlockedAbilitys.Find((Predicate<QuestClearUnlockUnitDataParam>) (p =>
              {
                if (p.new_id == id)
                  return p.type == type;
                return false;
              }));
              break;
            }
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.LeaderSkill:
            if (this.mUnlockedLeaderSkill != null)
            {
              unlockUnitDataParam = this.mUnlockedLeaderSkill;
              break;
            }
            break;
        }
        if (unlockUnitDataParam != null)
          return true;
      }
      return false;
    }

    public List<string> GetQuestUnlockConditions(QuestParam quest)
    {
      if (this.mSkillUnlocks == null)
        return (List<string>) null;
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.mSkillUnlocks.Count; ++index)
      {
        QuestClearUnlockUnitDataParam mSkillUnlock = this.mSkillUnlocks[index];
        if (!(mSkillUnlock.uid != this.UnitID) && Array.FindIndex<string>(mSkillUnlock.qids, (Predicate<string>) (p => p == quest.iname)) != -1)
        {
          string condText = this.mSkillUnlocks[index].GetCondText(this.mUnitParam);
          if (!string.IsNullOrEmpty(condText))
            stringList.Add(condText);
        }
      }
      return stringList;
    }

    private Json_Job[] AppendUnlockedJobs(Json_Job[] jobs)
    {
      if (jobs == null || this.mUnitParam.jobsets == null)
        return jobs;
      List<Json_Job> jsonJobList = new List<Json_Job>((IEnumerable<Json_Job>) jobs);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      long num = 0;
      for (int index = 0; index < jobs.Length; ++index)
      {
        if (jobs[index].iid > num)
          num = jobs[index].iid;
      }
      List<JobSetParam> jobSetParamList = new List<JobSetParam>();
      for (int index = 0; index < this.mUnitParam.jobsets.Length; ++index)
      {
        JobSetParam jobSetParam = instanceDirect.GetJobSetParam(this.mUnitParam.jobsets[index]);
        if (jobSetParam != null)
          jobSetParamList.Add(jobSetParam);
      }
      JobSetParam[] changeJobSetParam = instanceDirect.GetClassChangeJobSetParam(this.mUnitParam.iname);
      if (changeJobSetParam != null)
      {
        for (int index = 0; index < changeJobSetParam.Length; ++index)
          jobSetParamList.Add(changeJobSetParam[index]);
      }
      for (int index1 = 0; index1 < jobSetParamList.Count; ++index1)
      {
        JobSetParam jobSetParam = jobSetParamList[index1];
        bool flag = true;
        int index2 = -1;
        for (int index3 = 0; index3 < jobs.Length; ++index3)
        {
          if (jobs[index3] != null && jobSetParam.job == jobs[index3].iname)
          {
            index2 = index3;
            flag = false;
            break;
          }
        }
        if (!flag)
        {
          if (!string.IsNullOrEmpty(jobSetParam.jobchange))
          {
            JobSetParam before = instanceDirect.GetJobSetParam(jobSetParam.jobchange);
            if (before != null && jobs[index2].rank > 0)
            {
              int index3 = jsonJobList.FindIndex((Predicate<Json_Job>) (p => p.iname == before.job));
              if (index3 >= 0)
                jsonJobList.RemoveAt(index3);
            }
          }
        }
        else
        {
          Json_Job jsonJob = new Json_Job();
          jsonJob.iname = jobSetParam.job;
          jsonJob.iid = num + 1L;
          ++num;
          jsonJobList.Add(jsonJob);
        }
      }
      return jsonJobList.ToArray();
    }

    private void SetSkinLockedJob(Json_Job[] jobs)
    {
      if (jobs == null)
        return;
      string str = string.Empty;
      for (int index = 0; index < jobs.Length; ++index)
      {
        if (jobs[index] != null && !string.IsNullOrEmpty(jobs[index].cur_skin))
        {
          str = jobs[index].cur_skin;
          break;
        }
      }
      if (string.IsNullOrEmpty(str))
        return;
      for (int index = 0; index < jobs.Length; ++index)
      {
        if (jobs[index] != null)
          jobs[index].cur_skin = str;
      }
    }

    public bool CheckUsedSkin(string afName)
    {
      if (string.IsNullOrEmpty(afName) || this.mUnlockedSkins == null || this.mUnlockedSkins.Count < 0)
        return false;
      bool flag = false;
      for (int index = 0; index < this.mUnlockedSkins.Count; ++index)
      {
        ArtifactParam mUnlockedSkin = this.mUnlockedSkins[index];
        if (mUnlockedSkin != null && mUnlockedSkin.iname == afName)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public void Deserialize(Json_Unit json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.UpdateSyncTime();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      this.mUnitParam = instanceDirect.GetUnitParam(json.iname);
      this.mUniqueID = json.iid;
      this.mRarity = (OInt) Math.Min(Math.Max(json.rare, (int) this.mUnitParam.rare), (int) this.mUnitParam.raremax);
      this.mAwakeLv = (OInt) json.plus;
      this.mUnlockTobiraNum = (OInt) TobiraUtility.GetUnlockTobiraNum(json.doors);
      this.mElement = (OInt) ((int) this.mUnitParam.element);
      this.mExp = (OInt) json.exp;
      this.mLv = (OInt) this.CalcLevel();
      this.mJobs = (JobData[]) null;
      this.mJobIndex = (OInt) 0;
      this.mFavorite = json.fav == 1;
      this.mSupportElement = (EElement) json.elem;
      this.mPartyJobs = (long[]) null;
      if (json.select.quests != null)
      {
        this.mPartyJobs = new long[11];
        for (int index = 0; index < json.select.quests.Length; ++index)
        {
          string qtype = json.select.quests[index].qtype;
          if (!string.IsNullOrEmpty(qtype))
            this.mPartyJobs[(int) PartyData.GetPartyTypeFromString(qtype)] = json.select.quests[index].jiid;
        }
      }
      this.mUnlockedLeaderSkill = (QuestClearUnlockUnitDataParam) null;
      this.mUnlockedAbilitys = (List<QuestClearUnlockUnitDataParam>) null;
      this.mUnlockedSkills = (List<QuestClearUnlockUnitDataParam>) null;
      this.mSkillUnlocks = (List<QuestClearUnlockUnitDataParam>) null;
      if (json.quest_clear_unlocks != null && json.quest_clear_unlocks.Length >= 1)
      {
        QuestClearUnlockUnitDataParam[] unlockUnitDataParamArray = new QuestClearUnlockUnitDataParam[json.quest_clear_unlocks.Length];
        for (int index = 0; index < json.quest_clear_unlocks.Length; ++index)
        {
          QuestClearUnlockUnitDataParam unlockUnitData = instanceDirect.MasterParam.GetUnlockUnitData(json.quest_clear_unlocks[index]);
          if (unlockUnitData != null)
            unlockUnitDataParamArray[index] = unlockUnitData;
        }
        if (unlockUnitDataParamArray.Length >= 1)
        {
          List<int> intList = new List<int>();
          for (int index1 = 0; index1 < unlockUnitDataParamArray.Length; ++index1)
          {
            if (unlockUnitDataParamArray[index1] != null && !unlockUnitDataParamArray[index1].add)
            {
              for (int index2 = 0; index2 < unlockUnitDataParamArray.Length; ++index2)
              {
                if (unlockUnitDataParamArray[index2] != null && index1 != index2 && unlockUnitDataParamArray[index1].old_id == unlockUnitDataParamArray[index2].new_id)
                {
                  if (unlockUnitDataParamArray[index2].add)
                  {
                    unlockUnitDataParamArray[index2] = (QuestClearUnlockUnitDataParam) null;
                    break;
                  }
                  intList.Add(index2);
                  break;
                }
              }
            }
          }
          for (int index = 0; index < intList.Count; ++index)
            unlockUnitDataParamArray[intList[index]] = (QuestClearUnlockUnitDataParam) null;
          for (int index = 0; index < unlockUnitDataParamArray.Length; ++index)
          {
            if (unlockUnitDataParamArray[index] != null)
            {
              if (unlockUnitDataParamArray[index].type == QuestClearUnlockUnitDataParam.EUnlockTypes.LeaderSkill)
              {
                if (this.mUnlockedLeaderSkill == null)
                  this.mUnlockedLeaderSkill = new QuestClearUnlockUnitDataParam();
                this.mUnlockedLeaderSkill = unlockUnitDataParamArray[index];
              }
              else if (unlockUnitDataParamArray[index].type == QuestClearUnlockUnitDataParam.EUnlockTypes.Ability || unlockUnitDataParamArray[index].type == QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility)
              {
                if (this.mUnlockedAbilitys == null)
                  this.mUnlockedAbilitys = new List<QuestClearUnlockUnitDataParam>();
                this.mUnlockedAbilitys.Add(unlockUnitDataParamArray[index]);
              }
              else if (unlockUnitDataParamArray[index].type == QuestClearUnlockUnitDataParam.EUnlockTypes.Skill)
              {
                if (this.mUnlockedSkills == null)
                  this.mUnlockedSkills = new List<QuestClearUnlockUnitDataParam>();
                this.mUnlockedSkills.Add(unlockUnitDataParamArray[index]);
              }
            }
          }
        }
      }
      QuestClearUnlockUnitDataParam[] allUnlockUnitDatas = instanceDirect.MasterParam.GetAllUnlockUnitDatas();
      if (allUnlockUnitDatas != null)
      {
        this.mSkillUnlocks = new List<QuestClearUnlockUnitDataParam>();
        for (int index = 0; index < allUnlockUnitDatas.Length; ++index)
        {
          if (allUnlockUnitDatas[index].uid == this.UnitID)
            this.mSkillUnlocks.Add(allUnlockUnitDatas[index]);
        }
      }
      if (this.UnitParam.skins != null)
      {
        this.mUnlockedSkins.Clear();
        ArtifactParam[] array = instanceDirect.MasterParam.Artifacts.ToArray();
        for (int index = 0; index < this.UnitParam.skins.Length; ++index)
        {
          string skinName = this.UnitParam.skins[index];
          ArtifactParam artifactParam = Array.Find<ArtifactParam>(array, (Predicate<ArtifactParam>) (af => af.iname == skinName));
          if (artifactParam != null && instanceDirect.Player.ItemEntryExists(artifactParam.kakera))
            this.mUnlockedSkins.Add(artifactParam);
        }
      }
      if (json.jobs != null)
        json.jobs = this.AppendUnlockedJobs(json.jobs);
      if (json.jobs != null)
        this.SetSkinLockedJob(json.jobs);
      this.mMapEffectAbility = (AbilityData) null;
      if (json.jobs != null)
      {
        this.mJobs = new JobData[json.jobs.Length];
        int newSize = 0;
        Json_Job[] jsonJobArray = new Json_Job[json.jobs.Length];
        int num = 0;
        JobSetParam[] changeJobSetParam = instanceDirect.GetClassChangeJobSetParam(this.mUnitParam.iname);
        List<JobSetParam> jobSetParamList = (List<JobSetParam>) null;
        if (changeJobSetParam != null && changeJobSetParam.Length > 0)
          jobSetParamList = new List<JobSetParam>((IEnumerable<JobSetParam>) changeJobSetParam);
        for (int index1 = 0; index1 < 2; ++index1)
        {
          for (int index2 = 0; index2 < this.mUnitParam.jobsets.Length; ++index2)
          {
            JobSetParam jobset = instanceDirect.GetJobSetParam(this.mUnitParam.jobsets[index2]);
            JobSetParam jobSetParam = (JobSetParam) null;
            if (jobSetParamList != null && jobSetParamList.Count > 0)
            {
              int index3 = jobSetParamList.FindIndex((Predicate<JobSetParam>) (p => p.jobchange == jobset.iname));
              if (index3 >= 0)
                jobSetParam = changeJobSetParam[index3];
            }
            do
            {
              bool flag = false;
              for (int index3 = 0; index3 < json.jobs.Length; ++index3)
              {
                if (json.jobs[index3] != null)
                {
                  if (jobset.job == json.jobs[index3].iname)
                  {
                    jsonJobArray[num++] = json.jobs[index3];
                    json.jobs[index3] = (Json_Job) null;
                    flag = true;
                    break;
                  }
                  if (jobSetParam != null && jobSetParam.job == json.jobs[index3].iname)
                  {
                    jsonJobArray[num++] = json.jobs[index3];
                    json.jobs[index3] = (Json_Job) null;
                    flag = true;
                    break;
                  }
                }
              }
              if (!flag)
                jobset = string.IsNullOrEmpty(jobset.jobchange) ? (JobSetParam) null : instanceDirect.GetJobSetParam(jobset.jobchange);
              else
                break;
            }
            while (jobset != null);
          }
        }
        for (int index = 0; index < json.jobs.Length; ++index)
        {
          if (json.jobs[index] != null)
          {
            jsonJobArray[num++] = json.jobs[index];
            DebugUtility.LogError("JOB_DATA InputError :: INAME => " + json.jobs[index].iname);
            json.jobs[index] = (Json_Job) null;
          }
        }
        json.jobs = jsonJobArray;
        for (int index = 0; index < json.jobs.Length; ++index)
        {
          JobData jobData = new JobData();
          jobData.Deserialize(this, json.jobs[index]);
          this.mJobs[newSize] = jobData;
          ++newSize;
        }
        if (newSize != this.mJobs.Length)
          Array.Resize<JobData>(ref this.mJobs, newSize);
        for (int index = 0; index < this.mJobs.Length; ++index)
        {
          if (json.select != null && this.mJobs[index].UniqueID == json.select.job)
          {
            this.mJobIndex = (OInt) index;
            break;
          }
        }
        if (!string.IsNullOrEmpty(this.mJobs[(int) this.mJobIndex].Param.MapEffectAbility))
        {
          string mapEffectAbility = this.mJobs[(int) this.mJobIndex].Param.MapEffectAbility;
          this.mMapEffectAbility = new AbilityData();
          this.mMapEffectAbility.Setup(this, -1L, mapEffectAbility, 0, 0);
          this.mMapEffectAbility.UpdateLearningsSkill(false, (List<SkillData>) null);
          this.mMapEffectAbility.IsNoneCategory = true;
        }
      }
      else
      {
        this.mNormalAttackSkill = new SkillData();
        this.mNormalAttackSkill.Setup(this.UnitParam.no_job_status == null ? (string) null : this.UnitParam.no_job_status.default_skill, 1, 1, (MasterParam) null);
      }
      this.mConceptCard = (ConceptCardData) null;
      if (json.concept_card != null && json.concept_card.iid > 0L)
      {
        this.mConceptCard = new ConceptCardData();
        this.mConceptCard.Deserialize(json.concept_card);
      }
      this.mTobiraData.Clear();
      if (json.doors != null)
      {
        for (int index = 0; index < json.doors.Length; ++index)
        {
          Json_Tobira door = json.doors[index];
          SRPG.TobiraData tobiraData = new SRPG.TobiraData();
          tobiraData.Setup(json.iname, (TobiraParam.Category) door.category, door.lv);
          this.mTobiraData.Add(tobiraData);
        }
      }
      this.mMasterAbility = (AbilityData) null;
      if (json.abil != null && !string.IsNullOrEmpty(json.abil.iname))
      {
        this.mMasterAbility = new AbilityData();
        string iname = json.abil.iname;
        long iid = json.abil.iid;
        int exp = json.abil.exp;
        this.mMasterAbility.Setup(this, iid, iname, exp, 0);
        this.mMasterAbility.IsNoneCategory = true;
      }
      this.mCollaboAbility = (AbilityData) null;
      if (json.c_abil != null && !string.IsNullOrEmpty(json.c_abil.iname))
      {
        this.mCollaboAbility = new AbilityData();
        this.mCollaboAbility.Setup(this, json.c_abil.iid, json.c_abil.iname, json.c_abil.exp, 0);
        this.mCollaboAbility.IsNoneCategory = true;
        this.mCollaboAbility.UpdateLearningsSkillCollabo(json.c_abil.skills);
      }
      this.mTobiraMasterAbilitys.Clear();
      if (json.door_abils != null)
      {
        for (int index = 0; index < json.door_abils.Length; ++index)
        {
          Json_Ability doorAbil = json.door_abils[index];
          if (doorAbil != null && !string.IsNullOrEmpty(doorAbil.iname))
          {
            AbilityData abilityData = new AbilityData();
            abilityData.Setup(this, doorAbil.iid, doorAbil.iname, doorAbil.exp, 0);
            abilityData.IsNoneCategory = true;
            this.mTobiraMasterAbilitys.Add(abilityData);
          }
        }
      }
      string overwriteLeaderSkill = TobiraUtility.GetOverwriteLeaderSkill(this.mTobiraData);
      string iname1 = this.mUnlockedLeaderSkill == null ? this.GetLeaderSkillIname((int) this.mRarity) : this.mUnlockedLeaderSkill.new_id;
      if (!string.IsNullOrEmpty(overwriteLeaderSkill))
        iname1 = overwriteLeaderSkill;
      this.mLeaderSkill = (SkillData) null;
      if (!string.IsNullOrEmpty(iname1))
      {
        this.mLeaderSkill = new SkillData();
        this.mLeaderSkill.Setup(iname1, 1, 1, (MasterParam) null);
      }
      this.mUnitJobOverwriteParams = instanceDirect.MasterParam.GetUnitJobOverwriteParamsForUnit(this.UnitID);
      this.mJobSkillAbilityDeriveData = instanceDirect.MasterParam.CreateSkillAbilityDeriveDataWithArtifacts(this.mJobs);
      this.UpdateAvailableJobs();
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
      this.ResetCharacterQuestParams();
      this.FindCharacterQuestParams();
    }

    public string Serialize()
    {
      string str1 = string.Empty + "{\"iid\":" + (object) this.UniqueID + ",\"iname\":\"" + this.UnitParam.iname + "\"" + ",\"rare\":" + (object) this.Rarity + ",\"plus\":" + (object) this.AwakeLv + ",\"lv\":" + (object) this.Lv + ",\"exp\":" + (object) this.Exp + ",\"fav\":" + (!this.IsFavorite ? "0" : "1");
      if (this.MasterAbility != null)
        str1 = str1 + ",\"abil\":{\"iid\":" + (object) this.MasterAbility.UniqueID + ",\"iname\":\"" + this.MasterAbility.Param.iname + "\",\"exp\":" + (object) this.MasterAbility.Exp + "}";
      if (this.CollaboAbility != null)
      {
        string str2 = str1 + ",\"c_abil\":{\"iid\":" + (object) this.CollaboAbility.UniqueID + ",\"iname\":\"" + this.CollaboAbility.Param.iname + "\",\"exp\":" + (object) this.CollaboAbility.Exp + ",\"skills\":[";
        for (int index = 0; index < this.CollaboAbility.Skills.Count; ++index)
        {
          SkillData skill = this.CollaboAbility.Skills[index];
          if (skill != null)
            str2 = str2 + (index == 0 ? string.Empty : ",") + "{\"iname\":\"" + skill.SkillParam.iname + "\"}";
        }
        str1 = str2 + "]}";
      }
      string str3 = string.Empty;
      if (this.mUnlockedAbilitys != null)
      {
        for (int index = 0; index < this.mUnlockedAbilitys.Count; ++index)
          str3 = str3 + (index <= 0 ? string.Empty : ",") + "\"" + this.mUnlockedAbilitys[index].iname + "\"";
      }
      if (this.mUnlockedSkills != null)
      {
        if (!string.IsNullOrEmpty(str3))
          str3 += ",";
        for (int index = 0; index < this.mUnlockedSkills.Count; ++index)
          str3 = str3 + (index <= 0 ? string.Empty : ",") + "\"" + this.mUnlockedSkills[index].iname + "\"";
      }
      if (this.mUnlockedLeaderSkill != null)
        str3 = str3 + (string.IsNullOrEmpty(str3) ? string.Empty : ",") + "\"" + this.mUnlockedLeaderSkill.iname + "\"";
      if (!string.IsNullOrEmpty(str3))
        str1 = str1 + ",\"quest_clear_unlocks\":[" + str3 + "]";
      if (this.Jobs != null && this.Jobs.Length > 0)
      {
        string str2 = str1 + ",\"jobs\":[";
        for (int index1 = 0; index1 < this.Jobs.Length; ++index1)
        {
          JobData job = this.Jobs[index1];
          if (index1 > 0)
            str2 += ",";
          string str4 = str2 + "{\"iid\":" + (object) job.UniqueID + ",\"iname\":\"" + job.JobID + "\",\"rank\":" + (object) job.Rank;
          if (job.Equips != null && job.Equips.Length > 0)
          {
            string str5 = str4 + ",\"equips\":[";
            for (int index2 = 0; index2 < job.Equips.Length; ++index2)
            {
              EquipData equip = job.Equips[index2];
              if (index2 > 0)
                str5 += ",";
              str5 = str5 + "{\"iid\":" + (object) equip.UniqueID + ",\"iname\":\"" + equip.ItemID + "\",\"exp\":" + (object) equip.Exp + "}";
            }
            str4 = str5 + "]";
          }
          if (job.LearnAbilitys != null && job.LearnAbilitys.Count > 0)
          {
            string str5 = str4 + ",\"abils\":[";
            for (int index2 = 0; index2 < job.LearnAbilitys.Count; ++index2)
            {
              AbilityData learnAbility = job.LearnAbilitys[index2];
              if (index2 > 0)
                str5 += ",";
              str5 = str5 + "{\"iid\":" + (object) learnAbility.UniqueID + ",\"iname\":\"" + learnAbility.Param.iname + "\",\"exp\":" + (object) learnAbility.Exp + "}";
            }
            str4 = str5 + "]";
          }
          if (job.AbilitySlots != null || job.Artifacts != null)
          {
            string str5 = str4 + ",\"select\":{";
            bool flag = false;
            if (job.AbilitySlots != null)
            {
              string str6 = str5 + "\"abils\":[";
              for (int index2 = 0; index2 < job.AbilitySlots.Length; ++index2)
              {
                if (index2 > 0)
                  str6 += ",";
                str6 += (string) (object) job.AbilitySlots[index2];
              }
              str5 = str6 + "]";
              flag = true;
            }
            if (job.Artifacts != null)
            {
              if (flag)
                str5 += ",";
              string str6 = str5 + "\"artifacts\":[";
              for (int index2 = 0; index2 < job.Artifacts.Length; ++index2)
              {
                if (index2 > 0)
                  str6 += ",";
                str6 += (string) (object) job.Artifacts[index2];
              }
              str5 = str6 + "]";
            }
            str4 = str5 + "}";
          }
          if (job.ArtifactDatas != null)
          {
            string str5 = str4 + ",\"artis\":[";
            for (int index2 = 0; index2 < job.ArtifactDatas.Length; ++index2)
            {
              ArtifactData artifactData = job.ArtifactDatas[index2];
              if (index2 > 0)
                str5 += ",";
              str5 = artifactData != null ? str5 + "{\"iid\":" + (object) artifactData.UniqueID + ",\"iname\":\"" + artifactData.ArtifactParam.iname + "\"" + ",\"exp\":" + (object) artifactData.Exp + ",\"rare\":" + (object) artifactData.Rarity + ",\"fav\":" + (object) (!artifactData.IsFavorite ? 0 : 1) + "}" : str5 + "null";
            }
            str4 = str5 + "]";
          }
          if (!string.IsNullOrEmpty(job.SelectedSkin))
            str4 = str4 + ",\"cur_skin\":\"" + job.SelectedSkin + "\"";
          str2 = str4 + "}";
        }
        str1 = str2 + "]";
      }
      long num1 = this.CurrentJob == null ? 0L : this.CurrentJob.UniqueID;
      if (num1 != 0L)
      {
        string str2 = str1 + ",\"select\":{";
        if (num1 != 0L)
        {
          str2 = str2 + "\"job\":" + (object) num1;
          if (this.mPartyJobs != null && this.mPartyJobs.Length > 0)
          {
            string str4 = str2 + ",\"quests\":[";
            int num2 = 0;
            for (int index = 0; index < this.mPartyJobs.Length; ++index)
            {
              if (this.mPartyJobs[index] != 0L)
              {
                if (num2 > 0)
                  str4 += (string) (object) ',';
                str4 = str4 + "{\"qtype\":\"" + PartyData.GetStringFromPartyType((PlayerPartyTypes) index) + "\",\"jiid\":" + (object) this.mPartyJobs[index] + "}";
                ++num2;
              }
            }
            str2 = str4 + "]";
          }
        }
        str1 = str2 + "}";
      }
      if (this.mTobiraData != null && this.mTobiraData.Count > 0)
      {
        str1 = str1 + "," + TobiraUtility.ToJsonString(this.mTobiraData);
        if (this.mTobiraMasterAbilitys.Count > 0)
        {
          string str2 = str1 + ",\"door_abils\":[";
          for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
          {
            if (index > 0)
              str2 += ",";
            str2 = str2 + "{" + "\"iid\":" + (object) this.mTobiraMasterAbilitys[index].UniqueID + ",\"exp\":" + (object) this.mTobiraMasterAbilitys[index].Exp + ",\"iname\":\"" + this.mTobiraMasterAbilitys[index].Param.iname + "\"" + "}";
          }
          str1 = str2 + "]";
        }
      }
      if (this.mConceptCard != null)
        str1 = str1 + ",\"concept_card\":{" + "\"iid\":" + (object) this.mConceptCard.UniqueID + ",\"iname\":\"" + this.mConceptCard.Param.iname + "\"" + ",\"exp\":" + (object) this.mConceptCard.Exp + ",\"trust\":" + (object) this.mConceptCard.Trust + ",\"fav\":" + (object) (!this.mConceptCard.Favorite ? 0 : 1) + ",\"is_new\":0" + ",\"trust_bonus\":" + (object) (!this.mConceptCard.TrustBonus ? 0 : 1) + ",\"plus\":" + (object) this.mConceptCard.AwakeCount + "}";
      return str1 + "}";
    }

    public string Serialize2()
    {
      string str1 = string.Empty + "{\"iid\":" + (object) this.UniqueID + ",\"iname\":\"" + this.UnitParam.iname + "\"" + ",\"rare\":" + (object) this.Rarity + ",\"plus\":" + (object) this.AwakeLv + ",\"lv\":" + (object) this.Lv + ",\"exp\":" + (object) this.Exp;
      if (this.MasterAbility != null)
        str1 = str1 + ",\"abil\":{\"iid\":" + (object) this.MasterAbility.UniqueID + ",\"iname\":\"" + this.MasterAbility.Param.iname + "\",\"exp\":" + (object) this.MasterAbility.Exp + "}";
      if (this.CollaboAbility != null)
      {
        string str2 = str1 + ",\"c_abil\":{\"iid\":" + (object) this.CollaboAbility.UniqueID + ",\"iname\":\"" + this.CollaboAbility.Param.iname + "\",\"exp\":" + (object) this.CollaboAbility.Exp + ",\"skills\":[";
        for (int index = 0; index < this.CollaboAbility.Skills.Count; ++index)
        {
          SkillData skill = this.CollaboAbility.Skills[index];
          if (skill != null)
            str2 = str2 + (index == 0 ? string.Empty : ",") + "{\"iname\":\"" + skill.SkillParam.iname + "\"}";
        }
        str1 = str2 + "]}";
      }
      string str3 = string.Empty;
      if (this.mUnlockedAbilitys != null)
      {
        for (int index = 0; index < this.mUnlockedAbilitys.Count; ++index)
          str3 = str3 + (index <= 0 ? string.Empty : ",") + "\"" + this.mUnlockedAbilitys[index].iname + "\"";
      }
      if (this.mUnlockedSkills != null)
      {
        if (!string.IsNullOrEmpty(str3))
          str3 += ",";
        for (int index = 0; index < this.mUnlockedSkills.Count; ++index)
          str3 = str3 + (index <= 0 ? string.Empty : ",") + "\"" + this.mUnlockedSkills[index].iname + "\"";
      }
      if (this.mUnlockedLeaderSkill != null)
        str3 = str3 + (string.IsNullOrEmpty(str3) ? string.Empty : ",") + "\"" + this.mUnlockedLeaderSkill.iname + "\"";
      if (!string.IsNullOrEmpty(str3))
        str1 = str1 + ",\"quest_clear_unlocks\":[" + str3 + "]";
      long num1 = this.CurrentJob == null ? 0L : this.CurrentJob.UniqueID;
      if (this.Jobs != null && this.Jobs.Length > 0)
      {
        string str2 = str1 + ",\"jobs\":[";
        for (int index1 = 0; index1 < this.Jobs.Length; ++index1)
        {
          JobData job = this.Jobs[index1];
          if (index1 > 0)
            str2 += ",";
          string str4 = str2 + "{\"iid\":" + (object) job.UniqueID + ",\"iname\":\"" + job.JobID + "\",\"rank\":" + (object) job.Rank;
          if (job.Equips != null && job.Equips.Length > 0)
          {
            string str5 = str4 + ",\"equips\":[";
            for (int index2 = 0; index2 < job.Equips.Length; ++index2)
            {
              EquipData equip = job.Equips[index2];
              if (index2 > 0)
                str5 += ",";
              str5 = str5 + "{\"iid\":" + (object) equip.UniqueID + ",\"iname\":\"" + equip.ItemID + "\",\"exp\":" + (object) equip.Exp + "}";
            }
            str4 = str5 + "]";
          }
          if (job.LearnAbilitys != null && job.LearnAbilitys.Count > 0)
          {
            string str5 = str4 + ",\"abils\":[";
            for (int index2 = 0; index2 < job.LearnAbilitys.Count; ++index2)
            {
              AbilityData learnAbility = job.LearnAbilitys[index2];
              if (index2 > 0)
                str5 += ",";
              str5 = str5 + "{\"iid\":" + (object) learnAbility.UniqueID + ",\"iname\":\"" + learnAbility.Param.iname + "\",\"exp\":" + (object) learnAbility.Exp + "}";
            }
            str4 = str5 + "]";
          }
          if ((job.AbilitySlots != null || job.Artifacts != null) && num1 == job.UniqueID)
          {
            string str5 = str4 + ",\"select\":{";
            bool flag = false;
            if (job.AbilitySlots != null)
            {
              string str6 = str5 + "\"abils\":[";
              for (int index2 = 0; index2 < job.AbilitySlots.Length; ++index2)
              {
                if (index2 > 0)
                  str6 += ",";
                str6 += (string) (object) job.AbilitySlots[index2];
              }
              str5 = str6 + "]";
              flag = true;
            }
            if (job.Artifacts != null)
            {
              if (flag)
                str5 += ",";
              string str6 = str5 + "\"artifacts\":[";
              for (int index2 = 0; index2 < job.Artifacts.Length; ++index2)
              {
                if (index2 > 0)
                  str6 += ",";
                str6 += (string) (object) job.Artifacts[index2];
              }
              str5 = str6 + "]";
            }
            str4 = str5 + "}";
          }
          if (job.ArtifactDatas != null)
          {
            string str5 = str4 + ",\"artis\":[";
            if (job.UniqueID == num1)
            {
              for (int index2 = 0; index2 < job.ArtifactDatas.Length; ++index2)
              {
                ArtifactData artifactData = job.ArtifactDatas[index2];
                if (index2 > 0)
                  str5 += ",";
                str5 = artifactData != null ? str5 + "{\"iid\":" + (object) artifactData.UniqueID + ",\"iname\":\"" + artifactData.ArtifactParam.iname + "\"" + ",\"exp\":" + (object) artifactData.Exp + ",\"rare\":" + (object) artifactData.Rarity + "}" : str5 + "null";
              }
            }
            else
            {
              for (int index2 = 0; index2 < job.ArtifactDatas.Length; ++index2)
              {
                ArtifactData artifactData = job.ArtifactDatas[index2];
                if (index2 > 0)
                  str5 += ",";
                str5 = artifactData == null || artifactData != null && artifactData.ArtifactParam.type != ArtifactTypes.Arms ? str5 + "null" : str5 + "{\"iid\":" + (object) artifactData.UniqueID + ",\"iname\":\"" + artifactData.ArtifactParam.iname + "\"" + ",\"exp\":" + (object) artifactData.Exp + ",\"rare\":" + (object) artifactData.Rarity + ",\"fav\":" + (object) (!artifactData.IsFavorite ? 0 : 1) + "}";
              }
            }
            str4 = str5 + "]";
          }
          if (!string.IsNullOrEmpty(job.SelectedSkin) && num1 == job.UniqueID)
            str4 = str4 + ",\"cur_skin\":\"" + job.SelectedSkin + "\"";
          str2 = str4 + "}";
        }
        str1 = str2 + "]";
      }
      if (num1 != 0L)
      {
        string str2 = str1 + ",\"select\":{";
        if (num1 != 0L)
        {
          str2 = str2 + "\"job\":" + (object) num1;
          if (this.mPartyJobs != null && this.mPartyJobs.Length > 0)
          {
            string str4 = str2 + ",\"quests\":[";
            int num2 = 0;
            for (int index = 0; index < this.mPartyJobs.Length; ++index)
            {
              if (this.mPartyJobs[index] != 0L)
              {
                if (num2 > 0)
                  str4 += (string) (object) ',';
                str4 = str4 + "{\"qtype\":\"" + PartyData.GetStringFromPartyType((PlayerPartyTypes) index) + "\",\"jiid\":" + (object) this.mPartyJobs[index] + "}";
                ++num2;
              }
            }
            str2 = str4 + "]";
          }
        }
        str1 = str2 + "}";
      }
      if (this.mTobiraData != null && this.mTobiraData.Count > 0)
      {
        str1 = str1 + "," + TobiraUtility.ToJsonString(this.mTobiraData);
        if (this.mTobiraMasterAbilitys.Count > 0)
        {
          string str2 = str1 + ",\"door_abils\":[";
          for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
          {
            if (index > 0)
              str2 += ",";
            str2 = str2 + "{" + "\"iid\":" + (object) this.mTobiraMasterAbilitys[index].UniqueID + ",\"exp\":" + (object) this.mTobiraMasterAbilitys[index].Exp + ",\"iname\":\"" + this.mTobiraMasterAbilitys[index].Param.iname + "\"" + "}";
          }
          str1 = str2 + "]";
        }
      }
      if (this.mConceptCard != null)
        str1 = str1 + ",\"concept_card\":{" + "\"iid\":" + (object) this.mConceptCard.UniqueID + ",\"iname\":\"" + this.mConceptCard.Param.iname + "\"" + ",\"exp\":" + (object) this.mConceptCard.Exp + ",\"trust\":" + (object) this.mConceptCard.Trust + ",\"fav\":" + (object) (!this.mConceptCard.Favorite ? 0 : 1) + ",\"is_new\":0" + ",\"trust_bonus\":" + (object) (!this.mConceptCard.TrustBonus ? 0 : 1) + ",\"plus\":" + (object) this.mConceptCard.AwakeCount + "}";
      return str1 + "}";
    }

    public bool Setup(string unit_iname, int exp, int rare, int awakeLv, string job_iname = null, int jobrank = 1, EElement elem = EElement.None, int unlockTobiraNum = 0)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      this.mUnitParam = instanceDirect.GetUnitParam(unit_iname);
      DebugUtility.Assert(this.mUnitParam != null, "Failed UnitParam iname \"" + unit_iname + "\" not found.");
      this.mRarity = (OInt) Math.Min(Math.Max(rare, (int) this.mUnitParam.rare), (int) this.mUnitParam.raremax);
      this.mAwakeLv = (OInt) Math.Min(awakeLv, this.GetAwakeLevelCap());
      this.mUnlockTobiraNum = (OInt) unlockTobiraNum;
      this.mElement = (OInt) ((int) elem);
      this.mExp = (OInt) exp;
      this.mLv = (OInt) this.CalcLevel();
      this.mJobs = (JobData[]) null;
      this.mJobIndex = (OInt) 0;
      if (this.mUnitParam.jobsets != null && this.mUnitParam.jobsets.Length > 0 && !string.IsNullOrEmpty(this.mUnitParam.jobsets[0]))
      {
        this.mJobs = new JobData[this.mUnitParam.jobsets.Length];
        int index = 0;
        int num = 0;
        for (; index < this.mUnitParam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = instanceDirect.GetJobSetParam(this.mUnitParam.jobsets[index]);
          if (jobSetParam != null && !string.IsNullOrEmpty(jobSetParam.job))
          {
            JobData jobData = new JobData();
            Json_Job json = new Json_Job();
            json.iname = jobSetParam.job;
            json.rank = 1;
            if (json.iname == job_iname)
            {
              json.rank = jobrank;
              this.mJobIndex = (OInt) num;
            }
            try
            {
              jobData.Deserialize(this, json);
              this.mJobs[num++] = jobData;
            }
            catch (Exception ex)
            {
              DebugUtility.LogException(ex);
            }
          }
        }
      }
      else
      {
        this.mNormalAttackSkill = new SkillData();
        this.mNormalAttackSkill.Setup(this.UnitParam.no_job_status == null ? (string) null : this.UnitParam.no_job_status.default_skill, 1, 1, (MasterParam) null);
      }
      this.UpdateAvailableJobs();
      if (this.mJobs != null)
      {
        for (int index = 0; index < this.mJobs.Length; ++index)
          this.mJobs[index].UnlockSkillAll();
      }
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      for (int index1 = 0; index1 < this.BattleAbilitys.Count; ++index1)
      {
        this.BattleAbilitys[index1].Setup(this, (long) (index1 + 1), this.BattleAbilitys[index1].AbilityID, this.BattleAbilitys[index1].Exp, 0);
        for (int index2 = 0; index2 < this.BattleAbilitys[index1].Skills.Count; ++index2)
        {
          SkillData skill = this.BattleAbilitys[index1].Skills[index2];
          if (skill != null && !this.mBattleSkills.Contains(skill))
            this.mBattleSkills.Add(skill);
        }
      }
      this.mLeaderSkill = (SkillData) null;
      string leaderSkillIname = this.GetLeaderSkillIname((int) this.mRarity);
      if (!string.IsNullOrEmpty(leaderSkillIname))
      {
        this.mLeaderSkill = new SkillData();
        this.mLeaderSkill.Setup(leaderSkillIname, 1, 1, (MasterParam) null);
      }
      this.CalcStatus();
      return true;
    }

    public void Setup(UnitData src)
    {
      this.Deserialize(JSONParser.parseJSONObject<Json_Unit>(src.Serialize()));
    }

    public void Release()
    {
      this.mJobs = (JobData[]) null;
      this.mStatus = (BaseStatus) null;
    }

    public void ShowTooltip(GameObject targetGO, bool allowJobChange = false, UnitJobDropdown.ParentObjectEvent updateValue = null)
    {
      PlayerPartyTypes dataOfClass = DataSource.FindDataOfClass<PlayerPartyTypes>(targetGO, PlayerPartyTypes.Max);
      GameObject root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>("UI/UnitTooltip_1"));
      UnitData data = new UnitData();
      data.Setup(this);
      data.TempFlags = this.TempFlags;
      DataSource.Bind<UnitData>(root, data);
      DataSource.Bind<PlayerPartyTypes>(root, dataOfClass);
      UnitJobDropdown componentInChildren1 = (UnitJobDropdown) root.GetComponentInChildren<UnitJobDropdown>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
      {
        bool flag = (data.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && allowJobChange && dataOfClass != PlayerPartyTypes.Max;
        ((Component) componentInChildren1).get_gameObject().SetActive(true);
        componentInChildren1.UpdateValue = updateValue;
        Selectable component1 = (Selectable) ((Component) componentInChildren1).get_gameObject().GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          component1.set_interactable(flag);
        Image component2 = (Image) ((Component) componentInChildren1).get_gameObject().GetComponent<Image>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          ((Graphic) component2).set_color(!flag ? new Color(0.5f, 0.5f, 0.5f) : Color.get_white());
      }
      ArtifactSlots componentInChildren2 = (ArtifactSlots) root.GetComponentInChildren<ArtifactSlots>();
      AbilitySlots componentInChildren3 = (AbilitySlots) root.GetComponentInChildren<AbilitySlots>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren3, (UnityEngine.Object) null))
      {
        bool enable = (data.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && allowJobChange && dataOfClass != PlayerPartyTypes.Max;
        componentInChildren2.Refresh(enable);
        componentInChildren3.Refresh(enable);
      }
      ConceptCardSlots componentInChildren4 = (ConceptCardSlots) root.GetComponentInChildren<ConceptCardSlots>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren4, (UnityEngine.Object) null))
      {
        bool enable = (data.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && allowJobChange && dataOfClass != PlayerPartyTypes.Max;
        componentInChildren4.Refresh(enable);
      }
      GameParameter.UpdateAll(root);
    }

    public int GetRarityCap()
    {
      return (int) this.UnitParam.raremax;
    }

    public int GetRarityLevelCap(int rarity)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      GrowParam growParam = masterParam.GetGrowParam((string) this.UnitParam.grow);
      int val1 = growParam == null ? 0 : growParam.GetLevelCap();
      RarityParam rarityParam = masterParam.GetRarityParam(rarity);
      if (rarityParam != null)
        val1 = Math.Min(val1, (int) rarityParam.UnitLvCap);
      return val1;
    }

    public int GetLevelCap(bool bPlayerLvCap = false)
    {
      int val1 = this.GetRarityLevelCap(this.Rarity) + this.AwakeLv + TobiraUtility.GetAdditionalUnitLevelCapWithUnlockNum(this.UnlockTobriaNum);
      if (bPlayerLvCap)
        val1 = Math.Min(val1, MonoSingleton<GameManager>.Instance.Player.Lv);
      return val1;
    }

    public int CalcLevel()
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.CalcUnitLevel((int) this.mExp, this.GetLevelCap(false));
    }

    public int GetExp()
    {
      return (int) this.mExp - MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitLevelExp((int) this.mLv);
    }

    public int GetNextLevel()
    {
      return Math.Min(this.Lv + 1, this.GetLevelCap(false));
    }

    public int GetNextExp()
    {
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      int levelCap = this.GetLevelCap(false);
      int num = 0;
      for (int index = 0; index < levelCap; ++index)
      {
        num += masterParam.GetUnitNextExp(index + 1);
        if (num > (int) this.mExp)
          return num - (int) this.mExp;
      }
      return 0;
    }

    public bool CheckGainExp()
    {
      return this.Exp < this.GetGainExpCap();
    }

    public int GetGainExpCap(int playerLv)
    {
      int levelCap = this.GetLevelCap(false);
      return levelCap <= playerLv ? MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitLevelExp(levelCap) : MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitLevelExp(playerLv + 1) - 1;
    }

    public int GetGainExpCap()
    {
      return this.GetGainExpCap(MonoSingleton<GameManager>.Instance.Player.Lv);
    }

    public void GainExp(int exp, int playerLv)
    {
      int gainExpCap = this.GetGainExpCap(playerLv);
      int mLv = (int) this.mLv;
      this.mExp = (OInt) Math.Min((int) this.mExp + exp, gainExpCap);
      this.mLv = (OInt) this.CalcLevel();
      if (mLv == (int) this.mLv)
        return;
      this.CalcStatus();
      MonoSingleton<GameManager>.GetInstanceDirect().Player.OnUnitLevelChange(this.UnitID, (int) this.mLv - mLv, (int) this.mLv, false);
    }

    public void CalcStatus()
    {
      this.mLv = (OInt) this.CalcLevel();
      this.CalcStatus((int) this.mLv, (int) this.mJobIndex, ref this.mStatus, -1);
    }

    public void CalcStatus(int lv, int jobNo, ref BaseStatus status, int disableJobMasterJobNo = -1)
    {
      UnitData.UnitScaleStatus.Clear();
      UnitData.WorkScaleStatus.Clear();
      UnitParam.CalcUnitLevelStatus(this.UnitParam, lv, ref status);
      JobData jobData = this.GetJobData(jobNo);
      if (jobData != null)
      {
        StatusParam statusParam1 = status.param;
        statusParam1.mov = (OShort) ((int) statusParam1.mov + (int) jobData.Param.mov);
        StatusParam statusParam2 = status.param;
        statusParam2.jmp = (OShort) ((int) statusParam2.jmp + (int) jobData.Param.jmp);
        UnitJobOverwriteParam jobOverwriteParam = this.GetUnitJobOverwriteParam(jobData.JobID);
        if (jobOverwriteParam != null)
          status.AddRate(jobOverwriteParam.mStatus);
        else
          status.AddRate(jobData.GetJobRankStatus());
        status.Add(jobData.GetJobTransfarStatus(this.Element));
        foreach (EquipData equip in jobData.Equips)
        {
          if (equip != null && equip.IsValid() && equip.IsEquiped())
          {
            UnitData.WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(equip.Skill, this.Element, ref status, ref UnitData.WorkScaleStatus);
            UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
          }
        }
        if (jobData.Artifacts != null && jobData.Artifacts.Length != 0)
        {
          BaseStatus src1 = new BaseStatus();
          BaseStatus src2 = new BaseStatus();
          BaseStatus comp1 = new BaseStatus();
          BaseStatus comp2 = new BaseStatus();
          src1.Clear();
          src2.Clear();
          for (int index = 0; index < jobData.Artifacts.Length; ++index)
          {
            if (jobData.Artifacts[index] != 0L)
            {
              ArtifactData artifactData = jobData.ArtifactDatas[index];
              if (artifactData != null && artifactData.EquipSkill != null)
              {
                comp1.Clear();
                comp2.Clear();
                UnitData.WorkScaleStatus.Clear();
                SkillData.GetHomePassiveBuffStatus(artifactData.EquipSkill, this.Element, ref comp1, ref comp2, ref comp2, ref comp1, ref UnitData.WorkScaleStatus);
                src1.ReplaceHighest(comp1);
                src2.ReplaceLowest(comp2);
                UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
              }
            }
          }
          status.Add(src1);
          status.Add(src2);
        }
        if (!string.IsNullOrEmpty(jobData.SelectedSkin))
        {
          ArtifactData selectedSkinData = jobData.GetSelectedSkinData();
          if (selectedSkinData != null && selectedSkinData.EquipSkill != null)
          {
            UnitData.WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(selectedSkinData.EquipSkill, this.Element, ref status, ref UnitData.WorkScaleStatus);
            UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
          }
        }
        if (this.mConceptCard != null)
        {
          List<ConceptCardEquipEffect> enableEquipEffects = this.mConceptCard.GetEnableEquipEffects(this, jobData);
          for (int index = 0; index < enableEquipEffects.Count; ++index)
          {
            UnitData.WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(enableEquipEffects[index].EquipSkill, this.Element, ref status, ref UnitData.WorkScaleStatus);
            UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
          }
        }
      }
      else
      {
        StatusParam statusParam1 = status.param;
        statusParam1.mov = (OShort) ((int) statusParam1.mov + (this.mUnitParam.no_job_status == null ? 0 : (int) this.mUnitParam.no_job_status.mov));
        StatusParam statusParam2 = status.param;
        statusParam2.jmp = (OShort) ((int) statusParam2.jmp + (this.mUnitParam.no_job_status == null ? 0 : (int) this.mUnitParam.no_job_status.jmp));
      }
      if (this.mTobiraData != null)
      {
        List<SkillData> parameterBuffSkills = TobiraUtility.GetParameterBuffSkills(this.mTobiraData);
        for (int index = 0; index < parameterBuffSkills.Count; ++index)
        {
          UnitData.WorkScaleStatus.Clear();
          SkillData.GetHomePassiveBuffStatus(parameterBuffSkills[index], this.Element, ref status, ref UnitData.WorkScaleStatus);
          UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
        }
      }
      if (this.Jobs != null)
      {
        for (int index = 0; index < this.Jobs.Length; ++index)
        {
          SkillData jobMaster = this.Jobs[index].JobMaster;
          if (jobMaster != null && disableJobMasterJobNo != index)
          {
            UnitData.WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(jobMaster, this.Element, ref status, ref UnitData.WorkScaleStatus);
            UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
          }
        }
      }
      UnitParam.CalcUnitElementStatus(this.Element, ref status);
      for (int index = 0; index < this.BattleSkills.Count; ++index)
      {
        UnitData.WorkScaleStatus.Clear();
        SkillData.GetHomePassiveBuffStatus(this.BattleSkills[index], this.Element, ref status, ref UnitData.WorkScaleStatus);
        UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
      }
      status.AddRate(UnitData.UnitScaleStatus);
      status.param.ApplyMinVal();
    }

    public int GetCombination()
    {
      return 7 + this.AwakeLv;
    }

    public int GetCombinationRange()
    {
      if (this.AwakeLv > 20)
        return 3;
      return this.AwakeLv > 10 ? 2 : 1;
    }

    public string GetLeaderSkillIname(int rarity)
    {
      if (rarity < 0 || this.mUnitParam.leader_skills.Length <= rarity)
        return (string) null;
      return this.mUnitParam.leader_skills[rarity];
    }

    public SkillData GetAttackSkill()
    {
      return this.GetAttackSkill((int) this.mJobIndex);
    }

    public SkillData GetAttackSkill(int jobNo)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData != null)
        return jobData.GetAttackSkill();
      return this.mNormalAttackSkill;
    }

    public int GetAttackRangeMin(SkillData skill)
    {
      return this.GetAttackRangeMin((int) this.mJobIndex, skill);
    }

    public int GetAttackRangeMin(int jobNo, SkillData skill)
    {
      SkillData skillData = skill;
      if (skillData == null || skillData.IsReactionSkill() && skillData.IsBattleSkill())
      {
        skillData = this.GetAttackSkill(jobNo);
        if (skillData == null)
          return 0;
      }
      return skillData.RangeMin;
    }

    public int GetAttackRangeMax(SkillData skill)
    {
      return this.GetAttackRangeMax((int) this.mJobIndex, skill);
    }

    public int GetAttackRangeMax(int jobNo, SkillData skill)
    {
      SkillData skillData = skill;
      if (skillData == null || skillData.IsReactionSkill() && skillData.IsBattleSkill())
      {
        skillData = this.GetAttackSkill(jobNo);
        if (skillData == null)
          return 0;
      }
      return skillData.RangeMax;
    }

    public int GetAttackScope(SkillData skill)
    {
      return this.GetAttackScope((int) this.mJobIndex, skill);
    }

    public int GetAttackScope(int jobNo, SkillData skill)
    {
      SkillData skillData = skill;
      if (skillData == null || skillData.IsReactionSkill() && skillData.IsBattleSkill())
      {
        skillData = this.GetAttackSkill(jobNo);
        if (skillData == null)
          return 0;
      }
      return skillData.Scope;
    }

    public int GetAttackHeight(SkillData skill = null, bool is_range = false)
    {
      return this.GetAttackHeight((int) this.mJobIndex, skill, is_range);
    }

    public int GetAttackHeight(int jobNo, SkillData skill, bool is_range)
    {
      SkillData skillData = skill;
      if (skillData == null || skillData.IsReactionSkill() && skillData.IsBattleSkill())
      {
        skillData = this.GetAttackSkill(jobNo);
        if (skillData == null)
          return 0;
      }
      int num = skillData.EnableAttackGridHeight;
      if (is_range && skill.TeleportType != eTeleportType.None)
        num = skillData.TeleportHeight;
      return num;
    }

    public int GetMoveCount()
    {
      return (int) this.mStatus.param.mov;
    }

    public int GetMoveHeight()
    {
      return (int) this.mStatus.param.jmp;
    }

    public RecipeParam GetRarityUpRecipe()
    {
      return this.GetRarityUpRecipe(this.Rarity);
    }

    public RecipeParam GetRarityUpRecipe(int rarity)
    {
      if (this.UnitParam != null)
        return this.UnitParam.GetRarityUpRecipe(rarity);
      return (RecipeParam) null;
    }

    public bool UnitRarityUp()
    {
      this.mRarity = (OInt) Math.Min((int) (++this.mRarity), this.GetRarityCap());
      string leaderSkillIname = this.GetLeaderSkillIname((int) this.mRarity);
      if (!string.IsNullOrEmpty(leaderSkillIname))
      {
        if (this.mLeaderSkill == null)
          this.mLeaderSkill = new SkillData();
        this.mLeaderSkill.Setup(leaderSkillIname, 1, 1, (MasterParam) null);
      }
      this.CalcStatus();
      return true;
    }

    public bool CheckUnitRarityUp()
    {
      if (this.GetRarityLevelCap(this.Rarity) > this.Lv || this.GetRarityCap() <= this.Rarity)
        return false;
      RecipeParam rarityUpRecipe = this.GetRarityUpRecipe();
      if (rarityUpRecipe == null)
        return false;
      for (int index = 0; index < rarityUpRecipe.items.Length; ++index)
      {
        RecipeItem recipeItem = rarityUpRecipe.items[index];
        if (recipeItem != null && !string.IsNullOrEmpty(recipeItem.iname))
        {
          ItemData itemDataByItemId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemID(recipeItem.iname);
          if (itemDataByItemId == null || itemDataByItemId.Num < recipeItem.num)
            return false;
        }
      }
      return true;
    }

    public int GetRarityUpCost()
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(this.Rarity);
      if (rarityParam != null)
        return (int) rarityParam.UnitRarityUpCost;
      return 0;
    }

    public bool CanUnlockTobira()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.CanUnlockTobira(this.UnitID);
    }

    public bool MeetsTobiraConditions(TobiraParam.Category category)
    {
      List<UnitData.TobiraConditioError> errors;
      return this.MeetsTobiraConditions(category, out errors);
    }

    public bool MeetsTobiraConditions(TobiraParam.Category category, out List<UnitData.TobiraConditioError> errors)
    {
      errors = new List<UnitData.TobiraConditioError>();
      TobiraConditionParam[] conditionsForUnit = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraConditionsForUnit(this.UnitID, category);
      if (conditionsForUnit == null)
        return false;
      foreach (TobiraConditionParam tobiraConditionParam in conditionsForUnit)
      {
        TobiraConditionParam cond = tobiraConditionParam;
        switch (cond.CondType)
        {
          case TobiraConditionParam.ConditionType.Unit:
            UnitData unitData = this;
            if (!string.IsNullOrEmpty(cond.CondUnit.UnitIname))
            {
              unitData = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(cond.CondUnit.UnitIname);
              if (unitData == null)
              {
                errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.UnitNone));
                break;
              }
            }
            if (cond.CondUnit.Level > unitData.Lv)
              errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.UnitLevel));
            if (cond.CondUnit.AwakeLevel > unitData.AwakeLv)
              errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.UnitAwakeLevel));
            if (category != TobiraParam.Category.START)
            {
              if (!string.IsNullOrEmpty(cond.CondUnit.JobIname) && cond.CondUnit.JobLevel > unitData.GetJobLevelByJobID(cond.CondUnit.JobIname))
                errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.UnitJobLevel));
              SRPG.TobiraData tobiraData = unitData.TobiraData.Find((Predicate<SRPG.TobiraData>) (tobira => tobira.Param.TobiraCategory == cond.CondUnit.TobiraCategory));
              if (tobiraData == null || cond.CondUnit.TobiraLv > tobiraData.Lv)
              {
                errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.UnitTobiraLevel));
                break;
              }
              break;
            }
            break;
          case TobiraConditionParam.ConditionType.Quest:
            if (category != TobiraParam.Category.START && !MonoSingleton<GameManager>.Instance.Player.IsQuestCleared(cond.CondIname))
            {
              errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.Quest));
              break;
            }
            break;
        }
      }
      return errors.Count == 0;
    }

    public bool UnitAwaking()
    {
      this.mAwakeLv = (OInt) Math.Min((int) (++this.mAwakeLv), this.GetAwakeLevelCap());
      return true;
    }

    public bool CheckUnitAwaking()
    {
      return this.GetAwakeLevelCap() > (int) this.mAwakeLv && this.GetAwakeNeedPieces() <= this.GetPieces() + this.GetElementPieces() + this.GetCommonPieces();
    }

    public bool CheckUnitAwaking(int awakelv)
    {
      int awakeLevelCap = this.GetAwakeLevelCap();
      if (awakeLevelCap < (int) this.mAwakeLv || awakeLevelCap < awakelv)
        return false;
      int num = this.GetPieces() + this.GetElementPieces() + this.GetCommonPieces();
      for (int mAwakeLv = (int) this.mAwakeLv; mAwakeLv < awakelv; ++mAwakeLv)
      {
        int awakeNeedPieces = MonoSingleton<GameManager>.Instance.MasterParam.GetAwakeNeedPieces(mAwakeLv);
        if (awakeNeedPieces > num)
          return false;
        num -= awakeNeedPieces;
      }
      return true;
    }

    public int GetPieces()
    {
      UnitParam unitParam = this.UnitParam;
      if (string.IsNullOrEmpty(unitParam.piece))
        return 0;
      ItemData itemDataByItemId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemID(unitParam.piece);
      if (itemDataByItemId == null)
        return 0;
      return itemDataByItemId.Num;
    }

    public ItemParam GetElementPieceParam()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      string key = string.Empty;
      switch (this.Element)
      {
        case EElement.Fire:
          key = (string) fixParam.CommonPieceFire;
          break;
        case EElement.Water:
          key = (string) fixParam.CommonPieceWater;
          break;
        case EElement.Wind:
          key = (string) fixParam.CommonPieceWind;
          break;
        case EElement.Thunder:
          key = (string) fixParam.CommonPieceThunder;
          break;
        case EElement.Shine:
          key = (string) fixParam.CommonPieceShine;
          break;
        case EElement.Dark:
          key = (string) fixParam.CommonPieceDark;
          break;
      }
      if (string.IsNullOrEmpty(key))
        return (ItemParam) null;
      return MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(key);
    }

    public ItemData GetElementPieceData()
    {
      ItemParam elementPieceParam = this.GetElementPieceParam();
      if (elementPieceParam == null)
        return (ItemData) null;
      return MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemParam(elementPieceParam);
    }

    public int GetElementPieces()
    {
      ItemData elementPieceData = this.GetElementPieceData();
      if (elementPieceData == null)
        return 0;
      return elementPieceData.Num;
    }

    public ItemParam GetCommonPieceParam()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (string.IsNullOrEmpty((string) fixParam.CommonPieceAll))
        return (ItemParam) null;
      return MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam((string) fixParam.CommonPieceAll);
    }

    public ItemData GetCommonPieceData()
    {
      ItemParam commonPieceParam = this.GetCommonPieceParam();
      if (commonPieceParam == null)
        return (ItemData) null;
      return MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemParam(commonPieceParam);
    }

    public int GetCommonPieces()
    {
      ItemData commonPieceData = this.GetCommonPieceData();
      if (commonPieceData == null)
        return 0;
      return commonPieceData.Num;
    }

    public string GetUnlockTobiraElementID()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      switch (this.Element)
      {
        case EElement.Fire:
          return (string) fixParam.TobiraUnlockElem[0];
        case EElement.Water:
          return (string) fixParam.TobiraUnlockElem[1];
        case EElement.Wind:
          return (string) fixParam.TobiraUnlockElem[2];
        case EElement.Thunder:
          return (string) fixParam.TobiraUnlockElem[3];
        case EElement.Shine:
          return (string) fixParam.TobiraUnlockElem[4];
        case EElement.Dark:
          return (string) fixParam.TobiraUnlockElem[5];
        default:
          return string.Empty;
      }
    }

    public string GetUnlockTobiraBirthID()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (this.mUnitParam.birthID == 0 || this.mUnitParam.birthID == 100)
        return string.Empty;
      int index = this.mUnitParam.birthID - 1;
      if (index >= 0)
        return (string) fixParam.TobiraUnlockBirth[index];
      DebugUtility.LogError("インデックスが有効範囲内にありません");
      return string.Empty;
    }

    public int GetChangePieces()
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(this.Rarity);
      string s = FlowNode_Variable.Get("UNIT_SELECT_TICKET");
      if (!string.IsNullOrEmpty(s) && int.Parse(s) == 1)
        return (int) rarityParam.UnitSelectChangePieceNum;
      if (rarityParam != null)
        return (int) rarityParam.UnitChangePieceNum;
      return 0;
    }

    public int GetAwakeNeedPieces()
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetAwakeNeedPieces(this.AwakeLv);
    }

    public int GetAwakeCost()
    {
      return 0;
    }

    public int GetAwakeLevelCap()
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(this.Rarity);
      if (rarityParam != null)
        return (int) rarityParam.UnitAwakeLvCap;
      return 0;
    }

    public JobParam GetJobParam(int jobNo)
    {
      return this.GetJobData(jobNo)?.Param;
    }

    public JobData GetJobData(int jobNo)
    {
      JobData[] jobs = this.Jobs;
      if (jobs == null || jobNo < 0 || jobs.Length <= jobNo)
        return (JobData) null;
      return jobs[jobNo];
    }

    public JobData GetClassChangeJobData(int jobNo)
    {
      JobData[] jobs = this.Jobs;
      if (jobs == null || jobNo < 0 || this.mNumJobsAvailable <= jobNo)
        return (JobData) null;
      JobParam classChangeJobParam = this.GetClassChangeJobParam(jobNo);
      for (int numJobsAvailable = this.mNumJobsAvailable; numJobsAvailable < jobs.Length; ++numJobsAvailable)
      {
        if (jobs[numJobsAvailable].Param == classChangeJobParam)
          return jobs[numJobsAvailable];
      }
      return (JobData) null;
    }

    public EquipData[] GetRankupEquips(int jobNo)
    {
      return this.GetJobData(jobNo)?.Equips;
    }

    public EquipData GetRankupEquipData(int jobNo, int slot)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData == null || jobData.Equips == null)
        return (EquipData) null;
      if (slot < 0 || slot >= jobData.Equips.Length)
        return (EquipData) null;
      return jobData.Equips[slot];
    }

    public bool CheckEnableEquipSlot(int jobNo, int slot)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData != null)
        return jobData.CheckEnableEquipSlot(slot);
      return false;
    }

    public bool CheckEnableEnhanceEquipment()
    {
      for (int index1 = 0; index1 < this.Jobs.Length; ++index1)
      {
        JobData job = this.Jobs[index1];
        if (job != null && job.IsActivated)
        {
          EquipData[] equips = job.Equips;
          if (equips != null)
          {
            for (int index2 = 0; index2 < equips.Length; ++index2)
            {
              if (equips[index2] != null && equips[index2].IsValid() && equips[index2].IsEquiped())
              {
                int rank = equips[index2].Rank;
                int rankCap = equips[index2].GetRankCap();
                if (rankCap > 1 && rank < rankCap)
                  return true;
              }
            }
          }
        }
      }
      return false;
    }

    public JobSetParam GetJobSetParam(int jobNo)
    {
      if (jobNo < 0 || jobNo >= this.mJobs.Length)
        return (JobSetParam) null;
      return this.GetJobSetParam2(this.mJobs[jobNo].JobID);
    }

    public JobSetParam GetJobSetParam(string jobID)
    {
      if (this.UnitParam.jobsets == null)
        return (JobSetParam) null;
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      for (int index = 0; index < this.UnitParam.jobsets.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.UnitParam.jobsets[index]))
        {
          for (JobSetParam jobSetParam = masterParam.GetJobSetParam(this.UnitParam.jobsets[index]); jobSetParam != null; jobSetParam = masterParam.GetJobSetParam(jobSetParam.jobchange))
          {
            if (jobSetParam.job == jobID)
              return jobSetParam;
          }
        }
      }
      return (JobSetParam) null;
    }

    public JobSetParam GetJobSetParam2(string jobID)
    {
      if (this.UnitParam.jobsets == null)
        return (JobSetParam) null;
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.UnitParam.jobsets.Length; ++index)
        stringList.Add(this.UnitParam.jobsets[index]);
      JobSetParam[] changeJobSetParam = masterParam.GetClassChangeJobSetParam(this.UnitParam.iname);
      if (changeJobSetParam != null && changeJobSetParam.Length > 0)
      {
        for (int index = 0; index < changeJobSetParam.Length; ++index)
          stringList.Add(changeJobSetParam[index].iname);
      }
      for (int index = 0; index < stringList.Count; ++index)
      {
        if (!string.IsNullOrEmpty(stringList[index]))
        {
          for (JobSetParam jobSetParam = masterParam.GetJobSetParam(stringList[index]); jobSetParam != null; jobSetParam = masterParam.GetJobSetParam(jobSetParam.jobchange))
          {
            if (jobSetParam.job == jobID)
              return jobSetParam;
          }
        }
      }
      return (JobSetParam) null;
    }

    public int GetJobRankCap()
    {
      return JobParam.GetJobRankCap(this.Rarity);
    }

    public int GetJobLevelByJobID(string iname)
    {
      for (int index = 0; index < this.Jobs.Length; ++index)
      {
        if (this.Jobs[index].Param.iname == iname)
          return this.Jobs[index].Rank;
      }
      return 0;
    }

    public bool CheckJobUnlockable(int jobNo)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData == null)
        return false;
      if (jobData.IsActivated)
        return true;
      JobSetParam jobSetParam = this.GetJobSetParam(jobNo);
      if (jobSetParam == null || this.Rarity < jobSetParam.lock_rarity || this.AwakeLv < jobSetParam.lock_awakelv)
        return false;
      if (jobSetParam.lock_jobs != null)
      {
        for (int index1 = 0; index1 < jobSetParam.lock_jobs.Length; ++index1)
        {
          if (jobSetParam.lock_jobs[index1] != null)
          {
            string iname = jobSetParam.lock_jobs[index1].iname;
            bool flag = false;
            for (int index2 = 0; index2 < this.mJobs.Length; ++index2)
            {
              if (this.mJobs[index2] != null && this.mJobs[index2].JobID == iname)
              {
                flag = true;
                break;
              }
            }
            if (flag && jobSetParam.lock_jobs[index1].lv > this.GetJobLevelByJobID(iname))
              return false;
          }
        }
      }
      return true;
    }

    public bool CheckJobUnlock(int jobNo, bool canCreate)
    {
      if (!this.CheckJobUnlockable(jobNo))
        return false;
      return this.GetJobData(jobNo).CheckJobRankUp(this, canCreate, true);
    }

    public bool CheckJobRankUp()
    {
      return this.CheckJobRankUp((int) this.mJobIndex);
    }

    public bool CheckJobRankUp(int jobNo)
    {
      return this.CheckJobRankUpInternal(jobNo, false, true);
    }

    public bool CheckJobRankUpAllEquip()
    {
      return this.CheckJobRankUpAllEquip((int) this.mJobIndex, true);
    }

    public bool CheckJobRankUpAllEquip(int jobNo, bool useCommon = true)
    {
      return this.CheckJobRankUpInternal(jobNo, true, useCommon);
    }

    private bool CheckJobRankUpInternal(int jobNo, bool canCreate, bool useCommon = true)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData.IsActivated)
        return jobData.CheckJobRankUp(this, canCreate, useCommon);
      return this.CheckJobUnlock(jobNo, canCreate);
    }

    public JobParam GetClassChangeJobParam()
    {
      return this.GetClassChangeJobParam((int) this.mJobIndex);
    }

    public JobParam GetClassChangeJobParam(int jobNo)
    {
      JobSetParam classChangeJobSet = this.GetClassChangeJobSet(jobNo);
      if (classChangeJobSet != null)
        return MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(classChangeJobSet.job);
      return (JobParam) null;
    }

    public void ReserveTemporaryJobs()
    {
      for (int jobNo = 0; jobNo < this.mJobs.Length; ++jobNo)
      {
        JobSetParam classChangeJobSet = this.GetClassChangeJobSet(jobNo);
        if (classChangeJobSet != null)
        {
          bool flag = false;
          for (int index = 0; index < this.mJobs.Length; ++index)
          {
            if (this.mJobs[index].JobID == classChangeJobSet.job)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            JobData jobData = new JobData();
            jobData.Deserialize(this, new Json_Job()
            {
              iname = classChangeJobSet.job
            });
            Array.Resize<JobData>(ref this.mJobs, this.mJobs.Length + 1);
            this.mJobs[this.mJobs.Length - 1] = jobData;
          }
        }
      }
    }

    public JobSetParam GetClassChangeJobSet(int jobNo)
    {
      if (this.UnitParam.jobsets == null)
        return (JobSetParam) null;
      JobData jobData = this.GetJobData(jobNo);
      if (jobData == null || jobNo >= this.UnitParam.jobsets.Length)
        return (JobSetParam) null;
      for (JobSetParam jobSetParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(this.UnitParam.jobsets[jobNo]); jobSetParam != null; jobSetParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(jobSetParam.jobchange))
      {
        if (jobSetParam.job == jobData.JobID)
          return MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(jobSetParam.jobchange);
      }
      return (JobSetParam) null;
    }

    public bool CheckJobClassChange(int jobNo)
    {
      JobData jobData = this.GetJobData(jobNo);
      return jobData != null && this.CheckJobRankUpAllEquip(Array.IndexOf<JobData>(this.mJobs, jobData), true);
    }

    public bool CheckClassChangeJobExist()
    {
      return this.CheckClassChangeJobExist((int) this.mJobIndex);
    }

    public bool CheckClassChangeJobExist(int jobNo)
    {
      if (this.UnitParam.jobsets == null || this.GetJobData(jobNo) == null)
        return false;
      JobSetParam classChangeJobSet = this.GetClassChangeJobSet(jobNo);
      if (classChangeJobSet == null || this.Rarity < classChangeJobSet.lock_rarity || this.AwakeLv < classChangeJobSet.lock_awakelv)
        return false;
      if (classChangeJobSet.lock_jobs != null)
      {
        for (int index1 = 0; index1 < classChangeJobSet.lock_jobs.Length; ++index1)
        {
          if (classChangeJobSet.lock_jobs[index1] != null)
          {
            string iname = classChangeJobSet.lock_jobs[index1].iname;
            bool flag = false;
            for (int index2 = 0; index2 < this.mJobs.Length; ++index2)
            {
              if (this.mJobs[index2] != null && this.mJobs[index2].JobID == iname)
              {
                flag = true;
                break;
              }
            }
            if (flag && classChangeJobSet.lock_jobs[index1].lv > this.GetJobLevelByJobID(iname))
              return false;
          }
        }
      }
      return true;
    }

    public void SetJob(PlayerPartyTypes type)
    {
      JobData jobFor = this.GetJobFor(type);
      if (jobFor == this.CurrentJob)
        return;
      this.SetJob(jobFor);
    }

    public void SetJob(JobData job)
    {
      int jobNo = Array.IndexOf<JobData>(this.mJobs, job);
      if (0 > jobNo)
        return;
      this.SetJobIndex(jobNo);
    }

    public void SetJobIndex(int jobNo)
    {
      this.mJobIndex = (OInt) jobNo;
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
    }

    public void JobUnlock(int jobNo)
    {
      this.JobRankUp(jobNo);
    }

    public void JobClassChange(int jobNo)
    {
      JobData jobData1 = this.GetJobData(jobNo);
      JobData baseJob = this.GetBaseJob(jobData1.JobID);
      if (baseJob == null || jobData1 == null)
        return;
      jobData1.JobRankUp();
      JobData jobData2 = this.CurrentJob;
      List<JobData> jobDataList = new List<JobData>((IEnumerable<JobData>) this.mJobs);
      jobDataList.Remove(jobData1);
      jobDataList[jobDataList.IndexOf(baseJob)] = jobData1;
      this.mJobs = jobDataList.ToArray();
      if (jobData2 == baseJob)
        jobData2 = jobData1;
      this.mJobIndex = (OInt) Array.IndexOf<JobData>(this.mJobs, jobData2);
      this.ReserveTemporaryJobs();
      this.UpdateAvailableJobs();
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
    }

    public void JobRankUp(int jobNo)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData == null)
        return;
      jobData.JobRankUp();
      this.ReserveTemporaryJobs();
      this.UpdateAvailableJobs();
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
    }

    public List<ItemData> GetJobRankUpReturnItemData(int jobNo, bool ignoreEquiped = false)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData == null)
        return (List<ItemData>) null;
      List<ItemData> itemDataList = new List<ItemData>();
      for (int index = 0; index < jobData.Equips.Length; ++index)
      {
        EquipData equip = jobData.Equips[index];
        if (equip != null && equip.IsValid() && (equip.IsEquiped() || ignoreEquiped))
        {
          List<ItemData> returnItemList = equip.GetReturnItemList();
          if (returnItemList != null)
          {
            using (List<ItemData>.Enumerator enumerator = returnItemList.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                ItemData current = enumerator.Current;
                itemDataList.Add(current);
              }
            }
          }
        }
      }
      return itemDataList;
    }

    private void UpdateAvailableJobs()
    {
      this.mNumJobsAvailable = 0;
      if (this.mJobs == null)
        return;
      for (int index1 = 0; index1 < this.mJobs.Length; ++index1)
      {
        if (this.mJobs[index1] != null)
        {
          JobSetParam classChangeBase2 = this.FindClassChangeBase2(this.mJobs[index1].JobID);
          if (classChangeBase2 == null)
          {
            ++this.mNumJobsAvailable;
          }
          else
          {
            bool flag = false;
            for (int index2 = 0; index2 < this.mJobs.Length; ++index2)
            {
              if (this.mJobs[index2].JobID == classChangeBase2.job)
              {
                flag = true;
                break;
              }
            }
            if (!flag)
              ++this.mNumJobsAvailable;
          }
        }
      }
    }

    private JobSetParam FindClassChangeBase2(string jobID)
    {
      UnitParam unitParam = this.UnitParam;
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      JobSetParam[] changeJobSetParam = masterParam.GetClassChangeJobSetParam(unitParam.iname);
      if (changeJobSetParam == null)
        return (JobSetParam) null;
      for (int index1 = 0; index1 < changeJobSetParam.Length; ++index1)
      {
        JobSetParam jobSetParam1 = changeJobSetParam[index1];
        if (jobSetParam1.job == jobID)
        {
          JobSetParam jobSetParam2 = masterParam.GetJobSetParam(jobSetParam1.jobchange);
          if (jobSetParam2 != null)
          {
            for (int index2 = 0; index2 < unitParam.jobsets.Length; ++index2)
            {
              JobSetParam jobSetParam3 = masterParam.GetJobSetParam(unitParam.jobsets[index2]);
              if (jobSetParam3 != null && jobSetParam3.job == jobSetParam2.job)
                return jobSetParam3;
            }
            break;
          }
          break;
        }
      }
      return (JobSetParam) null;
    }

    private JobSetParam FindClassChangeBase(string jobID)
    {
      UnitParam unitParam = this.UnitParam;
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      for (int index = 0; index < unitParam.jobsets.Length; ++index)
      {
        if (!string.IsNullOrEmpty(unitParam.jobsets[index]))
        {
          JobSetParam jobSetParam1 = masterParam.GetJobSetParam(unitParam.jobsets[index]);
          while (jobSetParam1 != null && !string.IsNullOrEmpty(jobSetParam1.jobchange))
          {
            JobSetParam jobSetParam2 = jobSetParam1;
            jobSetParam1 = masterParam.GetJobSetParam(jobSetParam1.jobchange);
            if (jobSetParam1 != null)
            {
              if (jobSetParam1.job == jobID)
                return jobSetParam2;
            }
            else
              break;
          }
        }
      }
      return (JobSetParam) null;
    }

    public void SetEquipAbility(int slot, long iid)
    {
      this.SetEquipAbility((int) this.mJobIndex, slot, iid);
    }

    public void SetEquipAbility(int jobIndex, int slot, long iid)
    {
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
        jobData.SetAbilitySlot(slot, this.GetAbilityData(iid));
      this.UpdateUnitBattleAbilityAll(jobIndex);
      this.CalcStatus();
    }

    private void UpdateUnitLearnAbilityAll()
    {
      this.UpdateUnitLearnAbilityAll((int) this.mJobIndex);
    }

    private void UpdateUnitLearnAbilityAll(int jobIndex)
    {
      this.mLearnAbilitys.Clear();
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
      {
        for (int index = 0; index < jobData.LearnAbilitys.Count; ++index)
        {
          AbilityData learnAbility = jobData.LearnAbilitys[index];
          if (learnAbility != null)
            this.mLearnAbilitys.Add(learnAbility);
        }
      }
      if (this.mMasterAbility != null)
        this.mLearnAbilitys.Add(this.mMasterAbility);
      if (this.mMapEffectAbility != null)
        this.mLearnAbilitys.Add(this.mMapEffectAbility);
      if (this.mTobiraMasterAbilitys == null)
        return;
      for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
      {
        if (this.mTobiraMasterAbilitys[index] != null)
          this.mLearnAbilitys.Add(this.mTobiraMasterAbilitys[index]);
      }
    }

    private void UpdateUnitBattleAbilityAll()
    {
      this.UpdateUnitBattleAbilityAll((int) this.mJobIndex);
    }

    private void UpdateUnitBattleAbilityAll(int jobIndex)
    {
      this.mBattleAbilitys.ForEach((Action<AbilityData>) (ability => ability.ResetDeriveAbility()));
      this.mBattleAbilitys.Clear();
      this.mBattleSkills.Clear();
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
      {
        for (int index1 = 0; index1 < jobData.AbilitySlots.Length; ++index1)
        {
          AbilityData abilityData = this.GetAbilityData(jobData.AbilitySlots[index1]);
          if (abilityData != null && !this.mBattleAbilitys.Contains(abilityData))
          {
            this.mBattleAbilitys.Add(abilityData);
            if (abilityData.Skills != null)
            {
              for (int index2 = 0; index2 < abilityData.Skills.Count; ++index2)
              {
                SkillData skill = abilityData.Skills[index2];
                if (skill != null && !this.mBattleSkills.Contains(skill))
                  this.mBattleSkills.Add(skill);
              }
            }
          }
        }
        if (jobData.Artifacts != null)
        {
          for (int index1 = 0; index1 < jobData.Artifacts.Length; ++index1)
          {
            if (jobData.Artifacts[index1] != 0L)
            {
              ArtifactData artifactData = jobData.ArtifactDatas[index1];
              if (artifactData != null && artifactData.LearningAbilities != null)
              {
                for (int index2 = 0; index2 < artifactData.LearningAbilities.Count; ++index2)
                {
                  AbilityData learningAbility = artifactData.LearningAbilities[index2];
                  if (learningAbility != null && learningAbility.CheckEnableUseAbility(this, jobIndex) && !this.mBattleAbilitys.Contains(learningAbility))
                  {
                    this.mBattleAbilitys.Add(learningAbility);
                    if (learningAbility.Skills != null)
                    {
                      for (int index3 = 0; index3 < learningAbility.Skills.Count; ++index3)
                      {
                        SkillData skill = learningAbility.Skills[index3];
                        if (skill != null && !this.mBattleSkills.Contains(skill))
                          this.mBattleSkills.Add(skill);
                      }
                    }
                  }
                }
              }
            }
          }
        }
        if (!string.IsNullOrEmpty(jobData.SelectedSkin))
        {
          ArtifactData selectedSkinData = jobData.GetSelectedSkinData();
          if (selectedSkinData != null && selectedSkinData.LearningAbilities != null)
          {
            for (int index1 = 0; index1 < selectedSkinData.LearningAbilities.Count; ++index1)
            {
              AbilityData learningAbility = selectedSkinData.LearningAbilities[index1];
              if (learningAbility != null && learningAbility.CheckEnableUseAbility(this, jobIndex) && !this.mBattleAbilitys.Contains(learningAbility))
              {
                this.mBattleAbilitys.Add(learningAbility);
                if (learningAbility.Skills != null)
                {
                  for (int index2 = 0; index2 < learningAbility.Skills.Count; ++index2)
                  {
                    SkillData skill = learningAbility.Skills[index2];
                    if (skill != null && !this.mBattleSkills.Contains(skill))
                      this.mBattleSkills.Add(skill);
                  }
                }
              }
            }
          }
        }
      }
      if (this.mConceptCard != null)
      {
        List<ConceptCardEquipEffect> enableEquipEffects = this.mConceptCard.GetEnableEquipEffects(this, jobData);
        for (int index1 = 0; index1 < enableEquipEffects.Count; ++index1)
        {
          AbilityData ability = enableEquipEffects[index1].Ability;
          if (ability != null && !this.mBattleAbilitys.Contains(ability) && this.mBattleAbilitys.FindIndex((Predicate<AbilityData>) (btl_abil => btl_abil.Param.iname == ability.Param.iname)) < 0)
          {
            this.mBattleAbilitys.Add(ability);
            if (ability.Skills != null)
            {
              for (int index2 = 0; index2 < ability.Skills.Count; ++index2)
              {
                SkillData skill = ability.Skills[index2];
                if (skill != null && !this.mBattleSkills.Contains(skill) && this.mBattleSkills.FindIndex((Predicate<SkillData>) (btl_skill => btl_skill.SkillParam.iname == skill.SkillParam.iname)) < 0)
                  this.mBattleSkills.Add(skill);
              }
            }
          }
        }
      }
      if (this.mMasterAbility != null && !this.mBattleAbilitys.Contains(this.mMasterAbility))
      {
        this.mBattleAbilitys.Add(this.mMasterAbility);
        if (this.mMasterAbility.Skills != null)
        {
          for (int index = 0; index < this.mMasterAbility.Skills.Count; ++index)
          {
            SkillData skill = this.mMasterAbility.Skills[index];
            if (skill != null && !this.mBattleSkills.Contains(skill))
              this.mBattleSkills.Add(skill);
          }
        }
      }
      if (this.mCollaboAbility != null && !this.mBattleAbilitys.Contains(this.mCollaboAbility))
      {
        this.mBattleAbilitys.Add(this.mCollaboAbility);
        if (this.mCollaboAbility.Skills != null)
        {
          using (List<SkillData>.Enumerator enumerator = this.mCollaboAbility.Skills.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              SkillData current = enumerator.Current;
              if (current != null && !this.mBattleSkills.Contains(current))
                this.mBattleSkills.Add(current);
            }
          }
        }
      }
      if (this.mMapEffectAbility != null && !this.mBattleAbilitys.Contains(this.mMapEffectAbility))
      {
        this.mBattleAbilitys.Add(this.mMapEffectAbility);
        if (this.mMapEffectAbility.Skills != null)
        {
          using (List<SkillData>.Enumerator enumerator = this.mMapEffectAbility.Skills.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              SkillData current = enumerator.Current;
              if (current != null && !this.mBattleSkills.Contains(current))
                this.mBattleSkills.Add(current);
            }
          }
        }
      }
      if (this.mTobiraMasterAbilitys != null)
      {
        for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
        {
          AbilityData tobiraMasterAbility = this.mTobiraMasterAbilitys[index];
          if (tobiraMasterAbility != null && !this.mBattleAbilitys.Contains(tobiraMasterAbility))
          {
            this.mBattleAbilitys.Add(tobiraMasterAbility);
            if (tobiraMasterAbility.Skills != null)
            {
              using (List<SkillData>.Enumerator enumerator = tobiraMasterAbility.Skills.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  SkillData current = enumerator.Current;
                  if (current != null && !this.mBattleSkills.Contains(current))
                    this.mBattleSkills.Add(current);
                }
              }
            }
          }
        }
      }
      using (List<SkillData>.Enumerator enumerator = this.mBattleSkills.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillData current = enumerator.Current;
          if (!string.IsNullOrEmpty(current.ReplaceSkillId))
          {
            current.Setup(current.ReplaceSkillId, current.Rank, current.GetRankCap(), (MasterParam) null);
            current.ReplaceSkillId = (string) null;
          }
          string iname1 = current.SkillParam.iname;
          string iname2 = this.SearchReplacementSkill(iname1);
          if (iname2 != null)
          {
            current.Setup(iname2, current.Rank, current.GetRankCap(), (MasterParam) null);
            current.ReplaceSkillId = iname1;
          }
        }
      }
      if (jobData == null || this.mJobSkillAbilityDeriveData == null || !this.mJobSkillAbilityDeriveData.TryGetValue(jobData.Param.iname, out this.mSkillAbilityDeriveData))
        return;
      UnitData.RefrectionDeriveSkillAndAbility(this.mSkillAbilityDeriveData, this.mBattleAbilitys, this.mBattleSkills);
    }

    public string SearchReplacementSkill(string skill_id)
    {
      using (List<SkillData>.Enumerator enumerator = this.mBattleSkills.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillData current = enumerator.Current;
          if (current.SkillParam.effect_type == SkillEffectTypes.EffReplace && current.SkillParam.ReplaceTargetIdLists != null && current.SkillParam.ReplaceChangeIdLists != null)
          {
            for (int index = 0; index < current.SkillParam.ReplaceTargetIdLists.Count; ++index)
            {
              if (current.SkillParam.ReplaceTargetIdLists[index] == skill_id && index < current.SkillParam.ReplaceChangeIdLists.Count)
                return current.SkillParam.ReplaceChangeIdLists[index];
            }
          }
        }
      }
      return (string) null;
    }

    public string SearchAbilityReplacementSkill(string ability_id)
    {
      using (List<SkillData>.Enumerator enumerator = this.mBattleSkills.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillData current = enumerator.Current;
          if (current.SkillParam.effect_type == SkillEffectTypes.EffReplace && current.SkillParam.AbilityReplaceTargetIdLists != null && current.SkillParam.AbilityReplaceChangeIdLists != null)
          {
            for (int index = 0; index < current.SkillParam.AbilityReplaceTargetIdLists.Count; ++index)
            {
              if (current.SkillParam.AbilityReplaceTargetIdLists[index] == ability_id && index < current.SkillParam.AbilityReplaceChangeIdLists.Count)
                return current.SkillParam.AbilityReplaceChangeIdLists[index];
            }
          }
        }
      }
      return (string) null;
    }

    public List<AbilityData> SearchDerivedAbilitys(AbilityData baseAbility)
    {
      List<AbilityData> abilityDataList = new List<AbilityData>();
      if (this.CurrentJob != null && this.mJobSkillAbilityDeriveData != null)
      {
        SkillAbilityDeriveData abilityDeriveData = (SkillAbilityDeriveData) null;
        if (this.mJobSkillAbilityDeriveData.TryGetValue(this.CurrentJob.Param.iname, out abilityDeriveData))
        {
          List<AbilityDeriveParam> abilityDeriveParams = abilityDeriveData.GetAvailableAbilityDeriveParams();
          List<SkillDeriveParam> skillDeriveParams = abilityDeriveData.GetAvailableSkillDeriveParams();
          List<AbilityDeriveParam> all = abilityDeriveParams.FindAll((Predicate<AbilityDeriveParam>) (abilityDeriveParam => abilityDeriveParam.BaseAbilityIname == baseAbility.Param.iname));
          List<SkillData> skillDataList = new List<SkillData>();
          using (List<AbilityDeriveParam>.Enumerator enumerator1 = all.GetEnumerator())
          {
            while (enumerator1.MoveNext())
            {
              AbilityDeriveParam current = enumerator1.Current;
              AbilityData deriveAbility = baseAbility.CreateDeriveAbility(current);
              abilityDataList.Add(deriveAbility);
              if (deriveAbility.Skills != null)
              {
                using (List<SkillData>.Enumerator enumerator2 = deriveAbility.Skills.GetEnumerator())
                {
                  while (enumerator2.MoveNext())
                  {
                    SkillData derivedAbilitySkill = enumerator2.Current;
                    if (skillDataList.Find((Predicate<SkillData>) (skill => skill.SkillParam.iname == derivedAbilitySkill.SkillParam.iname)) == null)
                      skillDataList.Add(derivedAbilitySkill);
                  }
                }
              }
            }
          }
          using (List<SkillDeriveParam>.Enumerator enumerator = skillDeriveParams.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              SkillDeriveParam skillDeriveParam = enumerator.Current;
              SkillData skillData = skillDataList.Find((Predicate<SkillData>) (battleSkill => battleSkill.SkillParam.iname == skillDeriveParam.m_BaseParam.iname));
              if (skillData != null)
              {
                SkillData deriveSkill = skillData.CreateDeriveSkill(skillDeriveParam);
                if (skillData.OwnerAbiliy != null)
                  skillData.OwnerAbiliy.AddDeriveSkill(deriveSkill);
              }
            }
          }
        }
      }
      return abilityDataList;
    }

    public List<SkillData> SearchDerivedSkills(SkillData baseSkill)
    {
      List<SkillData> skillDataList = new List<SkillData>();
      if (this.CurrentJob != null && this.mJobSkillAbilityDeriveData != null)
      {
        SkillAbilityDeriveData abilityDeriveData = (SkillAbilityDeriveData) null;
        if (this.mJobSkillAbilityDeriveData.TryGetValue(this.CurrentJob.Param.iname, out abilityDeriveData))
        {
          List<SkillDeriveParam> skillDeriveParams = abilityDeriveData.GetAvailableSkillDeriveParams();
          using (List<SkillDeriveParam>.Enumerator enumerator = skillDeriveParams.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              SkillDeriveParam skillDeriveParam = enumerator.Current;
              if (!(baseSkill.SkillParam.iname != skillDeriveParam.BaseSkillIname) && !skillDataList.Exists((Predicate<SkillData>) (skill => skill.SkillParam.iname == skillDeriveParam.DeriveSkillIname)))
              {
                SkillData deriveSkill = baseSkill.CreateDeriveSkill(skillDeriveParam);
                skillDataList.Add(deriveSkill);
              }
            }
          }
        }
      }
      return skillDataList;
    }

    public SkillAbilityDeriveData GetSkillAbilityDeriveData(JobData target_job = null, ArtifactTypes replace_target_slot = ArtifactTypes.None, ArtifactParam new_artifact = null)
    {
      JobData job = target_job != null ? target_job : this.CurrentJob;
      int artifactSlotIndex = JobData.GetArtifactSlotIndex(replace_target_slot);
      List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
      for (int slot = 0; slot < 3; ++slot)
      {
        ArtifactParam artifactParam = this.GetEquipArtifactParam(slot, job);
        if (new_artifact != null && slot == artifactSlotIndex)
          artifactParam = new_artifact;
        if (artifactParam != null)
          artifactParamList.Add(artifactParam);
      }
      string[] artifactInames = new string[artifactParamList.Count];
      for (int index = 0; index < artifactParamList.Count; ++index)
        artifactInames[index] = artifactParamList[index].iname;
      return MonoSingleton<GameManager>.Instance.MasterParam.CreateSkillAbilityDeriveDataWithArtifacts(artifactInames);
    }

    private static void RefrectionDeriveSkillAndAbility(SkillAbilityDeriveData skillAbilityDeriveData, List<AbilityData> refAbilitys, List<SkillData> refSkills)
    {
      if (skillAbilityDeriveData == null || refAbilitys == null || refSkills == null)
        return;
      List<AbilityDeriveParam> abilityDeriveParams = skillAbilityDeriveData.GetAvailableAbilityDeriveParams();
      List<SkillDeriveParam> skillDeriveParams = skillAbilityDeriveData.GetAvailableSkillDeriveParams();
      List<AbilityData> abilityDataList = new List<AbilityData>();
      List<SkillData> skillDataList = new List<SkillData>();
      using (List<AbilityDeriveParam>.Enumerator enumerator = abilityDeriveParams.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          AbilityDeriveParam abilityDeriveParam = enumerator.Current;
          AbilityData abilityData = (AbilityData) null;
          int index1 = refAbilitys.FindIndex((Predicate<AbilityData>) (battleAbility => battleAbility.Param.iname == abilityDeriveParam.m_BaseParam.iname));
          if (index1 != -1)
            abilityData = refAbilitys[index1];
          if (abilityData != null)
          {
            AbilityData deriveAbility = abilityData.CreateDeriveAbility(abilityDeriveParam);
            abilityData.AddDeriveAbility(deriveAbility);
            if (abilityData.Skills != null)
              abilityData.Skills.ForEach((Action<SkillData>) (skill => refSkills.Remove(skill)));
            if (deriveAbility.Skills != null)
            {
              for (int index2 = 0; index2 < deriveAbility.Skills.Count; ++index2)
              {
                SkillData skill = deriveAbility.Skills[index2];
                if (skill != null && !refSkills.Contains(skill) && refSkills.FindIndex((Predicate<SkillData>) (btl_skill => btl_skill.SkillParam.iname == skill.SkillParam.iname)) < 0)
                  refSkills.Add(skill);
              }
            }
            if (!abilityDataList.Contains(abilityData))
              abilityDataList.Add(abilityData);
            refAbilitys.Insert(index1, deriveAbility);
          }
        }
      }
      using (List<SkillDeriveParam>.Enumerator enumerator = skillDeriveParams.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillDeriveParam skillDeriveParam = enumerator.Current;
          SkillData skillData = (SkillData) null;
          int index = refSkills.FindIndex((Predicate<SkillData>) (battleSkill => battleSkill.SkillParam.iname == skillDeriveParam.m_BaseParam.iname));
          if (index != -1)
            skillData = refSkills[index];
          if (skillData != null)
          {
            SkillData deriveSkill = skillData.CreateDeriveSkill(skillDeriveParam);
            if (skillData.OwnerAbiliy != null)
              skillData.OwnerAbiliy.AddDeriveSkill(deriveSkill);
            if (!skillDataList.Contains(skillData))
              skillDataList.Add(skillData);
            refSkills.Insert(index, deriveSkill);
          }
        }
      }
      abilityDataList.ForEach((Action<AbilityData>) (ability => refAbilitys.Remove(ability)));
      skillDataList.ForEach((Action<SkillData>) (skill => refSkills.Remove(skill)));
    }

    public void UpdateAbilityRankUp()
    {
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
    }

    public List<AbilityData> CreateLearnAbilitys()
    {
      return this.CreateLearnAbilitys((int) this.mJobIndex);
    }

    public List<AbilityData> CreateLearnAbilitys(int jobIndex)
    {
      List<AbilityData> abilityDataList = new List<AbilityData>();
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
      {
        for (int index = 0; index < jobData.LearnAbilitys.Count; ++index)
        {
          if (!abilityDataList.Contains(jobData.LearnAbilitys[index]))
            abilityDataList.Add(jobData.LearnAbilitys[index]);
        }
      }
      if (this.mMasterAbility != null && !abilityDataList.Contains(this.mMasterAbility))
        abilityDataList.Add(this.mMasterAbility);
      if (this.mMapEffectAbility != null && !abilityDataList.Contains(this.mMapEffectAbility))
        abilityDataList.Add(this.mMapEffectAbility);
      if (this.mTobiraMasterAbilitys != null)
      {
        for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
        {
          AbilityData tobiraMasterAbility = this.mTobiraMasterAbilitys[index];
          if (tobiraMasterAbility != null && !abilityDataList.Contains(tobiraMasterAbility))
            abilityDataList.Add(tobiraMasterAbility);
        }
      }
      return abilityDataList;
    }

    public List<AbilityData> GetAllLearnedAbilities(bool enableDerive = false)
    {
      List<AbilityData> refAbilitys = new List<AbilityData>(32);
      for (int index1 = 0; index1 < this.mJobs.Length; ++index1)
      {
        JobData mJob = this.mJobs[index1];
        if (mJob != null && mJob.IsActivated)
        {
          for (int index2 = 0; index2 < mJob.LearnAbilitys.Count; ++index2)
          {
            AbilityData learnAbility = mJob.LearnAbilitys[index2];
            if (learnAbility != null && learnAbility.IsValid() && !refAbilitys.Contains(learnAbility))
              refAbilitys.Add(learnAbility);
          }
        }
      }
      if (this.mMasterAbility != null && !refAbilitys.Contains(this.mMasterAbility))
        refAbilitys.Add(this.mMasterAbility);
      if (this.mMapEffectAbility != null && !refAbilitys.Contains(this.mMapEffectAbility))
        refAbilitys.Add(this.mMapEffectAbility);
      if (this.mTobiraMasterAbilitys != null)
      {
        for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
        {
          AbilityData tobiraMasterAbility = this.mTobiraMasterAbilitys[index];
          if (tobiraMasterAbility != null && !refAbilitys.Contains(tobiraMasterAbility))
            refAbilitys.Add(tobiraMasterAbility);
        }
      }
      if (enableDerive && this.mJobSkillAbilityDeriveData != null)
      {
        JobData currentJob = this.CurrentJob;
        SkillAbilityDeriveData skillAbilityDeriveData = (SkillAbilityDeriveData) null;
        if (this.mJobSkillAbilityDeriveData.TryGetValue(currentJob.Param.iname, out skillAbilityDeriveData))
        {
          List<SkillData> refSkills = new List<SkillData>();
          using (List<AbilityData>.Enumerator enumerator1 = refAbilitys.GetEnumerator())
          {
            while (enumerator1.MoveNext())
            {
              AbilityData current1 = enumerator1.Current;
              if (current1.Skills != null)
              {
                using (List<SkillData>.Enumerator enumerator2 = current1.Skills.GetEnumerator())
                {
                  while (enumerator2.MoveNext())
                  {
                    SkillData current2 = enumerator2.Current;
                    if (current2 != null && !refSkills.Contains(current2))
                      refSkills.Add(current2);
                  }
                }
              }
            }
          }
          UnitData.RefrectionDeriveSkillAndAbility(skillAbilityDeriveData, refAbilitys, refSkills);
        }
      }
      return refAbilitys;
    }

    public AbilityData[] CreateEquipAbilitys()
    {
      return this.CreateEquipAbilitys((int) this.mJobIndex);
    }

    public AbilityData[] CreateEquipAbilitys(int jobIndex)
    {
      AbilityData[] abilityDataArray = new AbilityData[5];
      Array.Clear((Array) abilityDataArray, 0, abilityDataArray.Length);
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
      {
        for (int index = 0; index < jobData.AbilitySlots.Length; ++index)
        {
          long abilitySlot = jobData.AbilitySlots[index];
          abilityDataArray[index] = this.GetAbilityData(abilitySlot);
        }
      }
      return abilityDataArray;
    }

    public List<SkillData> CreateEquipSkills()
    {
      return this.CreateEquipSkills((int) this.mJobIndex);
    }

    public List<SkillData> CreateEquipSkills(int jobIndex)
    {
      List<SkillData> skillDataList = new List<SkillData>();
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
      {
        for (int index1 = 0; index1 < jobData.AbilitySlots.Length; ++index1)
        {
          AbilityData abilityData = this.GetAbilityData(jobData.AbilitySlots[index1]);
          if (abilityData != null)
          {
            for (int index2 = 0; index2 < abilityData.Skills.Count; ++index2)
            {
              SkillData skill = abilityData.Skills[index2];
              if (skill != null && !skillDataList.Contains(skill))
                skillDataList.Add(skill);
            }
          }
        }
      }
      return skillDataList;
    }

    public AbilityData GetAbilityData(long iid)
    {
      if (iid <= 0L)
        return (AbilityData) null;
      for (int jobNo = 0; jobNo < this.mJobs.Length; ++jobNo)
      {
        JobData jobData = this.GetJobData(jobNo);
        if (jobData != null)
        {
          for (int index = 0; index < jobData.LearnAbilitys.Count; ++index)
          {
            AbilityData learnAbility = jobData.LearnAbilitys[index];
            if (learnAbility != null && learnAbility.IsValid() && learnAbility.UniqueID == iid)
              return learnAbility;
          }
        }
      }
      if (this.mMasterAbility != null && this.mMasterAbility.UniqueID == iid)
        return this.mMasterAbility;
      if (this.mMapEffectAbility != null && this.mMapEffectAbility.UniqueID == iid)
        return this.mMapEffectAbility;
      if (this.mTobiraMasterAbilitys != null)
      {
        for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
        {
          AbilityData tobiraMasterAbility = this.mTobiraMasterAbilitys[index];
          if (tobiraMasterAbility != null && tobiraMasterAbility.UniqueID == iid)
            return tobiraMasterAbility;
        }
      }
      return (AbilityData) null;
    }

    public SkillData GetSkillData(string iname)
    {
      int jobIndex = 0;
      return this.GetSkillData(iname, ref jobIndex);
    }

    public SkillData GetSkillData(string iname, ref int jobIndex)
    {
      for (int jobNo = 0; jobNo < this.mJobs.Length; ++jobNo)
      {
        JobData jobData = this.GetJobData(jobNo);
        if (jobData != null)
        {
          for (int index1 = 0; index1 < jobData.LearnAbilitys.Count; ++index1)
          {
            AbilityData learnAbility = jobData.LearnAbilitys[index1];
            if (learnAbility != null && learnAbility.IsValid())
            {
              for (int index2 = 0; index2 < learnAbility.Skills.Count; ++index2)
              {
                SkillData skill = learnAbility.Skills[index2];
                if (skill != null && skill.IsValid())
                {
                  if (!(skill.SkillID != iname))
                    return skill;
                  string str = this.SearchReplacementSkill(iname);
                  if (!string.IsNullOrEmpty(str) && skill.SkillID == str || skill.IsDerivedSkill && skill.m_BaseSkillIname == iname)
                    return skill;
                }
              }
            }
          }
        }
      }
      if (this.mMasterAbility != null)
      {
        using (List<SkillData>.Enumerator enumerator = this.mMasterAbility.Skills.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SkillData current = enumerator.Current;
            if (current != null && current.IsValid() && !(current.SkillID != iname))
              return current;
          }
        }
        if (this.mMasterAbility.IsDeriveBaseAbility)
        {
          SkillData skillDataInSkills = this.mMasterAbility.DerivedAbility.FindSkillDataInSkills(iname);
          if (skillDataInSkills != null)
            return skillDataInSkills;
        }
      }
      if (this.mCollaboAbility != null)
      {
        using (List<SkillData>.Enumerator enumerator = this.mCollaboAbility.Skills.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SkillData current = enumerator.Current;
            if (current != null && current.IsValid() && !(current.SkillID != iname))
              return current;
          }
        }
        if (this.mCollaboAbility.IsDeriveBaseAbility)
        {
          SkillData skillDataInSkills = this.mCollaboAbility.FindSkillDataInSkills(iname);
          if (skillDataInSkills != null)
            return skillDataInSkills;
        }
      }
      if (this.mMapEffectAbility != null)
      {
        using (List<SkillData>.Enumerator enumerator = this.mMapEffectAbility.Skills.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SkillData current = enumerator.Current;
            if (current != null && current.IsValid() && !(current.SkillID != iname))
              return current;
          }
        }
        if (this.mMapEffectAbility.IsDeriveBaseAbility)
        {
          SkillData skillDataInSkills = this.mMapEffectAbility.FindSkillDataInSkills(iname);
          if (skillDataInSkills != null)
            return skillDataInSkills;
        }
      }
      if (this.mTobiraMasterAbilitys != null)
      {
        for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
        {
          AbilityData tobiraMasterAbility = this.mTobiraMasterAbilitys[index];
          if (tobiraMasterAbility != null)
          {
            using (List<SkillData>.Enumerator enumerator = tobiraMasterAbility.Skills.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SkillData current = enumerator.Current;
                if (current != null && current.IsValid() && !(current.SkillID != iname))
                  return current;
              }
            }
          }
        }
      }
      return (SkillData) null;
    }

    public int GetSkillUsedCost(SkillData skill)
    {
      return this.GetSkillUsedCost(skill.SkillParam);
    }

    public int GetSkillUsedCost(SkillParam skill)
    {
      int num1 = (int) skill.cost;
      if (skill.effect_type != SkillEffectTypes.GemsGift && skill.type != ESkillType.Attack && skill.type != ESkillType.Item)
      {
        int num2 = Math.Max(num1 + (int) this.Status[BattleBonus.UsedJewel], 0);
        num1 = num2 + num2 * (int) this.Status[BattleBonus.UsedJewelRate] / 100;
      }
      return num1;
    }

    public UnitJobOverwriteParam GetUnitJobOverwriteParam(string job_iname)
    {
      UnitJobOverwriteParam jobOverwriteParam = (UnitJobOverwriteParam) null;
      if (this.mUnitJobOverwriteParams != null)
        this.mUnitJobOverwriteParams.TryGetValue(job_iname, out jobOverwriteParam);
      return jobOverwriteParam;
    }

    public override string ToString()
    {
      return this.UnitParam.name + "(" + this.GetType().Name + ")";
    }

    public void SetVirtualAwakeLv(int target_awake_lv)
    {
      this.mAwakeLv = (OInt) target_awake_lv;
    }

    public bool CheckEnableEquipment(ItemParam item)
    {
      if (item == null || item.type != EItemType.Equip)
        return false;
      for (int jobNo = 0; jobNo < this.NumJobsAvailable; ++jobNo)
      {
        JobData jobData = this.GetJobData(jobNo);
        if (jobData != null && jobData.Equips != null)
        {
          for (int index = 0; index < jobData.Equips.Length; ++index)
          {
            EquipData equip = jobData.Equips[index];
            if (equip != null && equip.IsValid() && (!equip.IsEquiped() && equip.ItemParam == item) && equip.ItemParam.equipLv <= this.Lv)
              return true;
          }
        }
      }
      return false;
    }

    public void UpdateBadge()
    {
      this.SetBadgeState(UnitBadgeTypes.EnableAwaking, this.CheckUnitAwaking());
    }

    private void SetBadgeState(UnitBadgeTypes type, bool flag)
    {
      if (flag)
        this.BadgeState |= type;
      else
        this.BadgeState &= ~type;
    }

    private bool CheckEnableEquipmentBadge()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int jobNo = 0; jobNo < this.NumJobsAvailable; ++jobNo)
      {
        JobData jobData = this.GetJobData(jobNo);
        if (jobData != null && jobData.Equips != null && jobData.IsActivated)
        {
          for (int index = 0; index < jobData.Equips.Length; ++index)
          {
            EquipData equip = jobData.Equips[index];
            if (equip != null && equip.IsValid() && !equip.IsEquiped() && ((player.HasItem(equip.ItemID) || player.CheckEnableCreateItem(equip.ItemParam, true, 1, (NeedEquipItemList) null)) && equip.ItemParam.equipLv <= this.Lv))
              return true;
          }
        }
      }
      return false;
    }

    public ArtifactParam GetEquipArmArtifact()
    {
      return this.GetEquipArtifactParam(0, (JobData) null);
    }

    public ArtifactParam GetEquipArtifactParam(int slot, JobData job = null)
    {
      JobData job1 = job;
      if (job == null)
        job1 = this.CurrentJob;
      if (job != null)
      {
        ArtifactData equipArtifactData = this.GetEquipArtifactData(slot, job1);
        if (equipArtifactData != null)
          return equipArtifactData.ArtifactParam;
        if (slot == 0)
          return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(job1.Param.artifact);
      }
      return (ArtifactParam) null;
    }

    public ArtifactData GetEquipArtifactData(int slot, JobData job = null)
    {
      JobData jobData = job;
      if (job == null)
        jobData = this.CurrentJob;
      if (jobData != null && jobData.Artifacts != null && jobData.Artifacts.Length > slot)
        return this.GetSortedArtifactDatas(jobData.ArtifactDatas)[slot];
      return (ArtifactData) null;
    }

    public ArtifactData[] GetSortedArtifactDatas(ArtifactData[] artifacts)
    {
      if (artifacts == null || artifacts.Length <= 0)
        return new ArtifactData[3];
      Dictionary<int, List<ArtifactData>> dictionary = new Dictionary<int, List<ArtifactData>>();
      for (int index = 0; index < artifacts.Length; ++index)
      {
        if (artifacts[index] != null)
        {
          int type = (int) artifacts[index].ArtifactParam.type;
          if (!dictionary.ContainsKey(type))
            dictionary.Add(type, new List<ArtifactData>());
          dictionary[type].Add(artifacts[index]);
        }
      }
      List<ArtifactData> artifactDataList = new List<ArtifactData>();
      int length = Enum.GetNames(typeof (ArtifactTypes)).Length;
      for (int key = 1; key <= length; ++key)
      {
        if (dictionary.ContainsKey(key))
          artifactDataList.AddRange((IEnumerable<ArtifactData>) dictionary[key]);
      }
      int num = 3 - artifactDataList.Count;
      if (num > 0)
      {
        for (int index = 0; index < num; ++index)
          artifactDataList.Add((ArtifactData) null);
      }
      return artifactDataList.ToArray();
    }

    public bool SetEquipArtifactData(int slot, ArtifactData artifact)
    {
      return this.SetEquipArtifactData((int) this.mJobIndex, slot, artifact, true);
    }

    public bool SetEquipArtifactData(int job_index, int slot, ArtifactData artifact, bool is_calc = true)
    {
      JobData jobData = this.GetJobData(job_index);
      if (jobData == null || !jobData.SetEquipArtifact(slot, artifact))
        return false;
      this.UpdateArtifact(job_index, is_calc, false);
      return true;
    }

    public void UpdateArtifact(int job_index, bool is_calc = true, bool refreshSkillAbilityDeriveData = false)
    {
      if (refreshSkillAbilityDeriveData)
        this.mJobSkillAbilityDeriveData = MonoSingleton<GameManager>.Instance.MasterParam.CreateSkillAbilityDeriveDataWithArtifacts(this.mJobs);
      this.UpdateUnitLearnAbilityAll(job_index);
      this.UpdateUnitBattleAbilityAll(job_index);
      if (!is_calc)
        return;
      this.CalcStatus((int) this.mLv, job_index, ref this.mStatus, -1);
    }

    public void SetUniqueID(long uniqueID)
    {
      this.mUniqueID = uniqueID;
    }

    private void UpdateSyncTime()
    {
      this.LastSyncTime = Time.get_realtimeSinceStartup();
    }

    public string GetUnitSkinVoiceSheetName(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = this.JobIndex;
      ArtifactParam selectedSkinData = this.GetSelectedSkinData(jobIndex);
      JobData jobData = this.Jobs == null || this.Jobs[jobIndex] == null ? (JobData) null : this.Jobs[jobIndex];
      return AssetPath.UnitVoiceFileName(this.UnitParam, selectedSkinData, this.UnitParam.GetJobVoice(jobData == null ? string.Empty : jobData.JobID));
    }

    public string GetUnitSkinVoiceCueName(int jobIndex = -1)
    {
      return AssetPath.UnitVoiceFileName(this.UnitParam, (ArtifactParam) null, string.Empty);
    }

    public string GetUnitJobVoiceSheetName(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = this.JobIndex;
      JobData jobData = this.Jobs == null || this.Jobs[jobIndex] == null ? (JobData) null : this.Jobs[jobIndex];
      return AssetPath.UnitVoiceFileName(this.UnitParam, (ArtifactParam) null, this.UnitParam.GetJobVoice(jobData == null ? string.Empty : jobData.JobID));
    }

    public UnitData.UnitPlaybackVoiceData GetUnitPlaybackVoiceData()
    {
      string skinVoiceSheetName = this.GetUnitSkinVoiceSheetName(-1);
      if (string.IsNullOrEmpty(skinVoiceSheetName))
      {
        DebugUtility.LogError("UnitDataにボイス設定が存在しません");
        return (UnitData.UnitPlaybackVoiceData) null;
      }
      string sheetName = "VO_" + skinVoiceSheetName;
      string cueNamePrefix = this.GetUnitSkinVoiceCueName(-1) + "_";
      MySound.Voice _voice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix, false);
      CriAtomExAcb acb = _voice.FindAcb(sheetName);
      if (acb == null)
      {
        DebugUtility.LogError("Acbファイルが存在しません");
        return (UnitData.UnitPlaybackVoiceData) null;
      }
      CriAtomEx.CueInfo[] cueInfoList = acb.GetCueInfoList();
      if (cueInfoList == null || cueInfoList.Length <= 0)
      {
        DebugUtility.LogError("CueInfoが存在しません");
        return (UnitData.UnitPlaybackVoiceData) null;
      }
      UnitData.UnitPlaybackVoiceData playbackVoiceData = new UnitData.UnitPlaybackVoiceData();
      playbackVoiceData.Init(this, _voice, cueInfoList, skinVoiceSheetName);
      return playbackVoiceData;
    }

    public bool CheckUnlockPlaybackVoice()
    {
      return this.Rarity + 1 >= 5;
    }

    public UnitData.CharacterQuestUnlockProgress SaveUnlockProgress()
    {
      UnitData.CharacterQuestParam charaEpisodeData = this.GetCurrentCharaEpisodeData();
      if (charaEpisodeData == null)
        return (UnitData.CharacterQuestUnlockProgress) null;
      return new UnitData.CharacterQuestUnlockProgress()
      {
        Level = this.Lv,
        Rarity = this.Rarity,
        CondQuest = charaEpisodeData.Param,
        ClearUnlocksCond = this.IsChQuestParentUnlocked(charaEpisodeData.Param)
      };
    }

    public bool IsQuestUnlocked(UnitData.CharacterQuestUnlockProgress progress)
    {
      UnitData.CharacterQuestParam charaEpisodeData = this.GetCurrentCharaEpisodeData();
      bool flag = this.IsChQuestParentUnlocked(charaEpisodeData.Param);
      int ulvmin = charaEpisodeData.Param.EntryConditionCh.ulvmin;
      int num = charaEpisodeData.Param.EntryConditionCh.rmin - 1;
      return flag && ulvmin <= this.Lv && num <= this.Rarity && (progress == null || progress.CondQuest.iname == charaEpisodeData.Param.iname && (!progress.ClearUnlocksCond || ulvmin > progress.Level || num > progress.Rarity));
    }

    public int CharacterQuestRarity
    {
      get
      {
        return UnitParam.MASTER_QUEST_RARITY - 1;
      }
    }

    public bool IsOpenCharacterQuest()
    {
      OInt characterQuestRarity = (OInt) this.CharacterQuestRarity;
      OInt masterQuestLv = (OInt) UnitParam.MASTER_QUEST_LV;
      return (int) characterQuestRarity > -1 && (int) masterQuestLv > 0 && (this.Rarity >= (int) characterQuestRarity && this.Lv >= (int) masterQuestLv) && this.IsSetCharacterQuest();
    }

    public List<QuestParam> FindCondQuests()
    {
      return this.mCharacterQuests;
    }

    public UnitData.CharacterQuestParam GetCurrentCharaEpisodeData()
    {
      if (!this.IsSetCharacterQuest())
        return (UnitData.CharacterQuestParam) null;
      UnitData.CharacterQuestParam[] charaEpisodeList = this.GetCharaEpisodeList();
      if (charaEpisodeList == null)
        return (UnitData.CharacterQuestParam) null;
      for (int index = 0; index < charaEpisodeList.Length; ++index)
      {
        if (charaEpisodeList[index].Param.state != QuestStates.Cleared)
          return charaEpisodeList[index];
      }
      return (UnitData.CharacterQuestParam) null;
    }

    public UnitData.CharacterQuestParam[] GetCharaEpisodeList()
    {
      if (!this.IsSetCharacterQuest())
        return (UnitData.CharacterQuestParam[]) null;
      List<QuestParam> questParamList = new List<QuestParam>();
      List<QuestParam> condQuests = this.FindCondQuests();
      if (condQuests.Count <= 0)
        return (UnitData.CharacterQuestParam[]) null;
      UnitData.CharacterQuestParam[] characterQuestParamArray = new UnitData.CharacterQuestParam[condQuests.Count];
      for (int index = 0; index < condQuests.Count; ++index)
      {
        UnitData.CharacterQuestParam characterQuestParam = new UnitData.CharacterQuestParam();
        characterQuestParam.EpisodeNum = index + 1;
        characterQuestParam.Param = condQuests[index];
        characterQuestParam.EpisodeTitle = condQuests[index].name;
        characterQuestParam.IsNew = condQuests[index].state == QuestStates.New;
        string empty = string.Empty;
        characterQuestParam.IsAvailable = condQuests[index].IsEntryQuestConditionCh(this, ref empty);
        characterQuestParamArray[index] = characterQuestParam;
      }
      return characterQuestParamArray;
    }

    public bool OpenNewCharacterEpisodeOnLevelUp(int beforeLv, UnitData.CharacterQuestParam targetQuset = null)
    {
      if (beforeLv >= this.Lv)
        return false;
      UnitData.CharacterQuestParam characterQuestParam = targetQuset != null ? targetQuset : this.GetCurrentCharaEpisodeData();
      if (characterQuestParam == null || !this.IsChQuestParentUnlocked(characterQuestParam.Param) || characterQuestParam.Param.EntryConditionCh == null)
        return false;
      int ulvmin = characterQuestParam.Param.EntryConditionCh.ulvmin;
      if (ulvmin <= this.Lv && ulvmin > beforeLv)
        return characterQuestParam.IsAvailable;
      return false;
    }

    public bool OpenCharacterQuestOnLevelUp(int beforeLv)
    {
      if (beforeLv >= this.Lv)
        return false;
      OInt masterQuestLv = (OInt) UnitParam.MASTER_QUEST_LV;
      if ((int) masterQuestLv <= 0 || !this.IsSetCharacterQuest() || (int) masterQuestLv <= beforeLv)
        return false;
      return (int) masterQuestLv <= this.Lv;
    }

    public bool OpenCharacterQuestOnQuestResult(QuestParam startParam, int beforeLv)
    {
      UnitData.CharacterQuestParam charaEpisodeData = this.GetCurrentCharaEpisodeData();
      return charaEpisodeData != null && this.IsChQuestParentUnlocked(charaEpisodeData.Param) && (startParam.iname != charaEpisodeData.Param.iname && charaEpisodeData.IsAvailable || this.OpenNewCharacterEpisodeOnLevelUp(beforeLv, charaEpisodeData));
    }

    public bool OpenCharacterQuestOnRarityUp(int beforeRarity)
    {
      if (beforeRarity >= this.Rarity)
        return false;
      OInt oint = (OInt) (UnitParam.MASTER_QUEST_RARITY - 1);
      if (!this.IsSetCharacterQuest() || (int) oint <= beforeRarity)
        return false;
      return (int) oint <= this.Rarity;
    }

    public bool IsSetCharacterQuest()
    {
      if (this.mCharacterQuests != null)
        return this.mCharacterQuests.Count > 0;
      return false;
    }

    public void FindCharacterQuestParams()
    {
      if (this.mCharacterQuests != null)
        return;
      this.mCharacterQuests = new List<QuestParam>();
      string characterQuestSection = GameSettings.Instance.CharacterQuestSection;
      QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (quests[index].world == characterQuestSection && quests[index].ChapterID == this.UnitID)
          this.mCharacterQuests.Add(quests[index]);
      }
    }

    public void ResetCharacterQuestParams()
    {
      this.mCharacterQuests = (List<QuestParam>) null;
    }

    public bool IsChQuestParentUnlocked(QuestParam quest)
    {
      if (quest == null)
        return false;
      bool flag = true;
      List<AbilityData> learnedAbilities = this.GetAllLearnedAbilities(false);
      List<QuestClearUnlockUnitDataParam> all = this.SkillUnlocks.FindAll((Predicate<QuestClearUnlockUnitDataParam>) (p =>
      {
        if (p.qids != null)
          return -1 != Array.FindIndex<string>(p.qids, (Predicate<string>) (s => s == quest.iname));
        return false;
      }));
      if (all == null)
        return true;
      for (int index = 0; index < all.Count; ++index)
      {
        QuestClearUnlockUnitDataParam unlock = all[index];
        AbilityData abilityData = (AbilityData) null;
        if (!unlock.qcnd)
        {
          switch (unlock.type)
          {
            case QuestClearUnlockUnitDataParam.EUnlockTypes.Skill:
              abilityData = learnedAbilities.Find((Predicate<AbilityData>) (p => p.AbilityID == unlock.parent_id));
              flag = abilityData != null;
              break;
            case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability:
              flag = Array.Find<JobData>(this.Jobs, (Predicate<JobData>) (p =>
              {
                if (p.JobID == unlock.parent_id)
                  return p.IsActivated;
                return false;
              })) != null;
              break;
          }
          if (flag && !unlock.add)
          {
            switch (unlock.type)
            {
              case QuestClearUnlockUnitDataParam.EUnlockTypes.Skill:
                flag = Array.Find<LearningSkill>(abilityData.LearningSkills, (Predicate<LearningSkill>) (p => p.iname == unlock.old_id)) != null;
                break;
              case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability:
                flag = learnedAbilities.Find((Predicate<AbilityData>) (p => p.AbilityID == unlock.old_id)) != null;
                break;
              case QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility:
                flag = this.MasterAbility != null && this.MasterAbility.AbilityID == unlock.old_id;
                break;
            }
          }
          if (!flag)
            return false;
        }
      }
      return true;
    }

    public JobData GetJobFor(PlayerPartyTypes type)
    {
      if (this.mPartyJobs != null && type < (PlayerPartyTypes) this.mPartyJobs.Length && this.mPartyJobs[(int) type] != 0L)
      {
        long mPartyJob = this.mPartyJobs[(int) type];
        for (int index = this.mJobs.Length - 1; index >= 0; --index)
        {
          if (this.mJobs[index].UniqueID == mPartyJob)
            return this.mJobs[index];
        }
      }
      return this.CurrentJob;
    }

    public void SetJobFor(PlayerPartyTypes type, JobData job)
    {
      if (this.mPartyJobs == null)
        this.mPartyJobs = new long[11];
      else if (this.mPartyJobs.Length != 11)
        Array.Resize<long>(ref this.mPartyJobs, 11);
      this.mPartyJobs[(int) type] = job.UniqueID;
    }

    public bool CheckCommon(int index, int slot)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      JobData job = this.Jobs[index];
      string rankupItem = job.GetRankupItems(job.Rank)[slot];
      ItemData itemDataByItemId1 = instance.Player.FindItemDataByItemID(rankupItem);
      ItemParam itemParam = instance.GetItemParam(rankupItem);
      if (itemParam == null || !itemParam.IsCommon || itemDataByItemId1 != null && itemDataByItemId1.Num >= 1)
        return false;
      ItemParam commonEquip = instance.MasterParam.GetCommonEquip(itemParam, job.Rank == 0);
      if (commonEquip == null)
        return false;
      ItemData itemDataByItemId2 = instance.Player.FindItemDataByItemID(commonEquip.iname);
      if (itemDataByItemId2 == null || instance.MasterParam.FixParam.EquipCommonPieceNum == null)
        return false;
      int num1 = (int) instance.MasterParam.FixParam.EquipCommonPieceNum[itemParam.rare];
      int num2 = job.Rank <= 0 ? 1 : num1;
      return itemDataByItemId2 != null && itemDataByItemId2.Num >= num2;
    }

    public string UnlockedCollaboSkillIds()
    {
      string empty = string.Empty;
      if (this.mCollaboAbility != null)
      {
        for (int index = 0; index < this.mCollaboAbility.Skills.Count; ++index)
        {
          if (!string.IsNullOrEmpty(empty))
            empty += ",";
          empty += this.mCollaboAbility.Skills[index].SkillParam.iname;
        }
      }
      return empty;
    }

    public bool IsThrow
    {
      get
      {
        if (this.UnitParam != null)
          return this.UnitParam.IsThrow();
        return false;
      }
    }

    public static int CompareTo_Iname(UnitData unit1, UnitData unit2)
    {
      return unit2.UnitParam.iname.CompareTo(unit1.UnitParam.iname);
    }

    public static int CompareTo_Lv(UnitData unit1, UnitData unit2)
    {
      return unit2.Lv - unit1.Lv;
    }

    public static int CompareTo_JobRank(UnitData unit1, UnitData unit2)
    {
      return unit2.CurrentJob.Rank - unit1.CurrentJob.Rank;
    }

    public static int CompareTo_Rarity(UnitData unit1, UnitData unit2)
    {
      return unit2.Rarity - unit1.Rarity;
    }

    public static int CompareTo_RarityInit(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.UnitParam.rare - (int) unit1.UnitParam.rare;
    }

    public static int CompareTo_RarityMax(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.UnitParam.raremax - (int) unit1.UnitParam.raremax;
    }

    public bool IsKnockBack
    {
      get
      {
        if (this.UnitParam != null)
          return this.UnitParam.IsKnockBack();
        return false;
      }
    }

    public int CalcTotalParameter()
    {
      return 0 + (int) this.mStatus.param.atk + (int) this.mStatus.param.def + (int) this.mStatus.param.mag + (int) this.mStatus.param.mnd + (int) this.mStatus.param.spd + (int) this.mStatus.param.dex + (int) this.mStatus.param.cri + (int) this.mStatus.param.luk;
    }

    public SRPG.TobiraData GetTobiraData(TobiraParam.Category category)
    {
      return this.mTobiraData.Find((Predicate<SRPG.TobiraData>) (tobira =>
      {
        if (tobira.Param != null)
          return tobira.Param.TobiraCategory == category;
        return false;
      }));
    }

    public bool CheckTobiraIsUnlocked(TobiraParam.Category category)
    {
      SRPG.TobiraData tobiraData = this.GetTobiraData(category);
      if (tobiraData != null)
        return tobiraData.IsUnlocked;
      return false;
    }

    public void SetTobiraData(List<SRPG.TobiraData> tobiraData)
    {
      this.mTobiraData = tobiraData;
    }

    [Flags]
    public enum TemporaryFlags
    {
      TemporaryUnitData = 1,
      AllowJobChange = 2,
    }

    public class TobiraConditioError
    {
      public UnitData.TobiraConditioError.ErrorType Type;

      public TobiraConditioError(UnitData.TobiraConditioError.ErrorType type)
      {
        this.Type = type;
      }

      public enum ErrorType
      {
        None,
        Quest,
        UnitNone,
        UnitLevel,
        UnitAwakeLevel,
        UnitJobLevel,
        UnitTobiraLevel,
        UnitOpenAllJobs,
      }
    }

    public class Json_PlaybackVoiceData
    {
      public List<string> cue_names = new List<string>();
      public int playback_voice_unlocked;
    }

    public class UnitVoiceCueInfo
    {
      public CriAtomEx.CueInfo cueInfo;
      public string voice_name;
      public bool has_conditions;
      public bool is_locked;
      public bool is_new;
      public string unlock_conditions_text;
      public int number;
    }

    public class UnitPlaybackVoiceData
    {
      private UnitData unit;
      private MySound.Voice voice;
      private List<UnitData.UnitVoiceCueInfo> voice_cue_list;

      public MySound.Voice Voice
      {
        get
        {
          return this.voice;
        }
      }

      public List<UnitData.UnitVoiceCueInfo> VoiceCueList
      {
        get
        {
          return this.voice_cue_list;
        }
      }

      public void Init(UnitData _unit, MySound.Voice _voice, CriAtomEx.CueInfo[] _cueInfo, string _voice_name)
      {
        this.unit = _unit;
        this.voice = _voice;
        this.voice_cue_list = new List<UnitData.UnitVoiceCueInfo>();
        List<UnitData.UnitVoiceCueInfo> unitVoiceCueInfoList1 = new List<UnitData.UnitVoiceCueInfo>();
        List<UnitData.UnitVoiceCueInfo> unitVoiceCueInfoList2 = new List<UnitData.UnitVoiceCueInfo>();
        List<UnitData.UnitVoiceCueInfo> unitVoiceCueInfoList3 = new List<UnitData.UnitVoiceCueInfo>();
        List<UnitData.UnitVoiceCueInfo> unitVoiceCueInfoList4 = new List<UnitData.UnitVoiceCueInfo>();
        try
        {
          UnitData.Json_PlaybackVoiceData playbackVoiceData = (UnitData.Json_PlaybackVoiceData) JsonUtility.FromJson<UnitData.Json_PlaybackVoiceData>(PlayerPrefsUtility.GetString(this.unit.UniqueID.ToString(), string.Empty));
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          for (int index = 0; index < _cueInfo.Length; ++index)
          {
            string[] strArray = ((string) _cueInfo[index].name).Split('_');
            if (strArray.Length >= 2)
            {
              string str1 = strArray[strArray.Length - 2];
              string s = strArray[strArray.Length - 1];
              bool success = false;
              string _tmp_name = str1 + "_" + s;
              string str2 = LocalizedText.Get("playback_list." + _tmp_name.ToUpper(), ref success);
              if (success)
              {
                UnitData.UnitVoiceCueInfo _info = new UnitData.UnitVoiceCueInfo();
                _info.cueInfo = _cueInfo[index];
                _info.voice_name = str2;
                _info.is_locked = false;
                _info.is_new = false;
                _info.number = int.Parse(s);
                this.SetConditions(_tmp_name, ref _info);
                if (_info.has_conditions)
                {
                  _info.is_locked = !this.CheckConditions(this.unit, _tmp_name);
                  if (!_info.is_locked && (playbackVoiceData == null || playbackVoiceData.cue_names.Count <= 0 || !playbackVoiceData.cue_names.Contains((string) _cueInfo[index].name)))
                    _info.is_new = true;
                  if (playbackVoiceData != null && playbackVoiceData.cue_names.Contains((string) _cueInfo[index].name))
                    _info.is_locked = false;
                }
                string key = str1;
                if (key != null)
                {
                  // ISSUE: reference to a compiler-generated field
                  if (UnitData.UnitPlaybackVoiceData.\u003C\u003Ef__switch\u0024map15 == null)
                  {
                    // ISSUE: reference to a compiler-generated field
                    UnitData.UnitPlaybackVoiceData.\u003C\u003Ef__switch\u0024map15 = new Dictionary<string, int>(4)
                    {
                      {
                        "chara",
                        0
                      },
                      {
                        "voice",
                        1
                      },
                      {
                        "battle",
                        2
                      },
                      {
                        "sys",
                        3
                      }
                    };
                  }
                  int num;
                  // ISSUE: reference to a compiler-generated field
                  if (UnitData.UnitPlaybackVoiceData.\u003C\u003Ef__switch\u0024map15.TryGetValue(key, out num))
                  {
                    switch (num)
                    {
                      case 0:
                        unitVoiceCueInfoList1.Add(_info);
                        continue;
                      case 1:
                        unitVoiceCueInfoList1.Add(_info);
                        continue;
                      case 2:
                        unitVoiceCueInfoList2.Add(_info);
                        continue;
                      case 3:
                        unitVoiceCueInfoList3.Add(_info);
                        continue;
                    }
                  }
                }
                unitVoiceCueInfoList4.Add(_info);
              }
            }
          }
          unitVoiceCueInfoList1.Sort((Comparison<UnitData.UnitVoiceCueInfo>) ((a, b) => a.number - b.number));
          unitVoiceCueInfoList2.Sort((Comparison<UnitData.UnitVoiceCueInfo>) ((a, b) => a.number - b.number));
          unitVoiceCueInfoList3.Sort((Comparison<UnitData.UnitVoiceCueInfo>) ((a, b) => a.number - b.number));
          unitVoiceCueInfoList4.Sort((Comparison<UnitData.UnitVoiceCueInfo>) ((a, b) => a.number - b.number));
          this.voice_cue_list.AddRange((IEnumerable<UnitData.UnitVoiceCueInfo>) unitVoiceCueInfoList1);
          this.voice_cue_list.AddRange((IEnumerable<UnitData.UnitVoiceCueInfo>) unitVoiceCueInfoList2);
          this.voice_cue_list.AddRange((IEnumerable<UnitData.UnitVoiceCueInfo>) unitVoiceCueInfoList3);
          this.voice_cue_list.AddRange((IEnumerable<UnitData.UnitVoiceCueInfo>) unitVoiceCueInfoList4);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }

      public void Cleanup()
      {
        if (this.voice == null)
          return;
        this.voice.StopAll(1f);
        this.voice.Cleanup();
        this.voice = (MySound.Voice) null;
      }

      private void SetConditions(string _tmp_name, ref UnitData.UnitVoiceCueInfo _info)
      {
        _info.has_conditions = false;
        using (Dictionary<string, string[]>.KeyCollection.Enumerator enumerator = UnitData.CONDITIONS_TARGET_NAMES.Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            if (this.ContainsArray(UnitData.CONDITIONS_TARGET_NAMES[current], _tmp_name))
            {
              _info.has_conditions = true;
              _info.unlock_conditions_text = this.GetUnlockConditionsText(current);
              break;
            }
          }
        }
      }

      private string GetUnlockConditionsText(string _dictionary_key)
      {
        string key = _dictionary_key;
        if (key != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (UnitData.UnitPlaybackVoiceData.\u003C\u003Ef__switch\u0024map16 == null)
          {
            // ISSUE: reference to a compiler-generated field
            UnitData.UnitPlaybackVoiceData.\u003C\u003Ef__switch\u0024map16 = new Dictionary<string, int>(4)
            {
              {
                "UNIT_LEVEL85",
                0
              },
              {
                "UNIT_LEVEL75",
                1
              },
              {
                "UNIT_LEVEL65",
                2
              },
              {
                "UNIT_JOB_MASTER1",
                3
              }
            };
          }
          int num;
          // ISSUE: reference to a compiler-generated field
          if (UnitData.UnitPlaybackVoiceData.\u003C\u003Ef__switch\u0024map16.TryGetValue(key, out num))
          {
            switch (num)
            {
              case 0:
                return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_UNIT_LV"), (object) 85);
              case 1:
                return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_UNIT_LV"), (object) 75);
              case 2:
                return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_UNIT_LV"), (object) 65);
              case 3:
                return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_JOB_MASTER"));
            }
          }
        }
        return string.Empty;
      }

      private bool CheckConditions(UnitData _unit, string _tmp_name)
      {
        return _unit.CheckUnlockPlaybackVoice() && this.CheckConditionsUnitLevel(_tmp_name, 85) && (this.CheckConditionsUnitLevel(_tmp_name, 75) && this.CheckConditionsUnitLevel(_tmp_name, 65)) && this.CheckConditionsJobMaster1(_tmp_name);
      }

      private bool CheckConditionsUnitLevel(string _tmp_name, int _level)
      {
        return !this.ContainsArray(UnitData.CONDITIONS_TARGET_NAMES["UNIT_LEVEL" + _level.ToString()], _tmp_name) || this.unit.Lv >= _level;
      }

      private bool CheckConditionsJobMaster1(string _tmp_name)
      {
        if (!this.ContainsArray(UnitData.CONDITIONS_TARGET_NAMES["UNIT_JOB_MASTER1"], _tmp_name))
          return true;
        for (int index = 0; index < this.unit.Jobs.Length; ++index)
        {
          if (this.unit.Jobs[index].CheckJobMaster())
            return true;
        }
        return false;
      }

      private bool ContainsArray(string[] _array, string _target)
      {
        for (int index = 0; index < _array.Length; ++index)
        {
          if (_array[index] == _target)
            return true;
        }
        return false;
      }
    }

    public class CharacterQuestUnlockProgress
    {
      public int Level;
      public int Rarity;
      public QuestParam CondQuest;
      public bool ClearUnlocksCond;
    }

    public class CharacterQuestParam
    {
      public int EpisodeNum;
      public string EpisodeTitle;
      public bool IsNew;
      public bool IsAvailable;
      public QuestParam Param;
    }
  }
}
