// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftUnitParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class VersusDraftUnitParam
  {
    private long mId;
    private long mDraftUnitId;
    private int mWeight;
    private long mDummyIID;
    private string mUnitIName;
    private int mRare;
    private int mAwake;
    private int mLevel;
    private int mSelectJobIndex;
    private List<VersusDraftUnitJob> mVersusDraftUnitJobs;
    private Dictionary<string, VersusDraftUnitAbility> mAbilities;
    private List<VersusDraftUnitArtifact> mVersusDraftUnitArtifacts;
    private string mConceptCardIName;
    private int mConceptCardLevel;
    private List<VersusDraftUnitDoor> mVersusDraftUnitDoors;
    private string mMasterAbilityIName;
    private string mSkinIName;

    public bool IsHidden { get; set; }

    public long Id
    {
      get
      {
        return this.mId;
      }
    }

    public long DraftUnitId
    {
      get
      {
        return this.mDraftUnitId;
      }
    }

    public int Weight
    {
      get
      {
        return this.mWeight;
      }
    }

    public bool Deserialize(long dummy_iid, JSON_VersusDraftUnitParam param)
    {
      if (dummy_iid <= 0L || param == null)
        return false;
      this.mDummyIID = dummy_iid;
      this.mId = param.id;
      this.mDraftUnitId = param.draft_unit_id;
      this.mWeight = param.weight;
      this.mUnitIName = param.unit_iname;
      this.mRare = param.rare;
      this.mAwake = param.awake;
      this.mLevel = param.lv;
      this.mSelectJobIndex = param.select_job_idx;
      this.mVersusDraftUnitJobs = new List<VersusDraftUnitJob>();
      if (!string.IsNullOrEmpty(param.job1_iname))
        this.mVersusDraftUnitJobs.Add(new VersusDraftUnitJob()
        {
          mIName = param.job1_iname,
          mRank = param.job1_lv,
          mEquip = param.job1_equip == 1
        });
      if (!string.IsNullOrEmpty(param.job2_iname))
        this.mVersusDraftUnitJobs.Add(new VersusDraftUnitJob()
        {
          mIName = param.job2_iname,
          mRank = param.job2_lv,
          mEquip = param.job2_equip == 1
        });
      if (!string.IsNullOrEmpty(param.job3_iname))
        this.mVersusDraftUnitJobs.Add(new VersusDraftUnitJob()
        {
          mIName = param.job3_iname,
          mRank = param.job3_lv,
          mEquip = param.job3_equip == 1
        });
      this.mAbilities = new Dictionary<string, VersusDraftUnitAbility>();
      int num1 = 0;
      if (!string.IsNullOrEmpty(param.abil1_iname))
      {
        this.mAbilities.Add(param.abil1_iname, new VersusDraftUnitAbility()
        {
          mIName = param.abil1_iname,
          mLevel = param.abil1_lv,
          mIID = this.mDummyIID * 10L + (long) num1
        });
        ++num1;
      }
      if (!string.IsNullOrEmpty(param.abil2_iname))
      {
        this.mAbilities.Add(param.abil2_iname, new VersusDraftUnitAbility()
        {
          mIName = param.abil2_iname,
          mLevel = param.abil2_lv,
          mIID = this.mDummyIID * 10L + (long) num1
        });
        ++num1;
      }
      if (!string.IsNullOrEmpty(param.abil3_iname))
      {
        this.mAbilities.Add(param.abil3_iname, new VersusDraftUnitAbility()
        {
          mIName = param.abil3_iname,
          mLevel = param.abil3_lv,
          mIID = this.mDummyIID * 10L + (long) num1
        });
        ++num1;
      }
      if (!string.IsNullOrEmpty(param.abil4_iname))
      {
        this.mAbilities.Add(param.abil4_iname, new VersusDraftUnitAbility()
        {
          mIName = param.abil4_iname,
          mLevel = param.abil4_lv,
          mIID = this.mDummyIID * 10L + (long) num1
        });
        ++num1;
      }
      if (!string.IsNullOrEmpty(param.abil5_iname))
      {
        this.mAbilities.Add(param.abil5_iname, new VersusDraftUnitAbility()
        {
          mIName = param.abil5_iname,
          mLevel = param.abil5_lv,
          mIID = this.mDummyIID * 10L + (long) num1
        });
        int num2 = num1 + 1;
      }
      this.mVersusDraftUnitArtifacts = new List<VersusDraftUnitArtifact>();
      if (!string.IsNullOrEmpty(param.arti1_iname))
        this.mVersusDraftUnitArtifacts.Add(new VersusDraftUnitArtifact()
        {
          mIName = param.arti1_iname,
          mRare = param.arti1_rare,
          mLevel = param.arti1_lv
        });
      if (!string.IsNullOrEmpty(param.arti2_iname))
        this.mVersusDraftUnitArtifacts.Add(new VersusDraftUnitArtifact()
        {
          mIName = param.arti2_iname,
          mRare = param.arti2_rare,
          mLevel = param.arti2_lv
        });
      if (!string.IsNullOrEmpty(param.arti3_iname))
        this.mVersusDraftUnitArtifacts.Add(new VersusDraftUnitArtifact()
        {
          mIName = param.arti3_iname,
          mRare = param.arti3_rare,
          mLevel = param.arti3_lv
        });
      this.mConceptCardIName = !string.IsNullOrEmpty(param.card_iname) ? param.card_iname : string.Empty;
      this.mConceptCardLevel = param.card_lv;
      this.mVersusDraftUnitDoors = new List<VersusDraftUnitDoor>();
      if (param.door1_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Envy,
          mLevel = param.door1_lv
        });
      if (param.door2_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Wrath,
          mLevel = param.door2_lv
        });
      if (param.door3_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Sloth,
          mLevel = param.door3_lv
        });
      if (param.door4_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Lust,
          mLevel = param.door4_lv
        });
      if (param.door5_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Gluttony,
          mLevel = param.door5_lv
        });
      if (param.door6_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Greed,
          mLevel = param.door6_lv
        });
      if (param.door7_lv > 0)
        this.mVersusDraftUnitDoors.Add(new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.Pride,
          mLevel = param.door7_lv
        });
      if (this.mVersusDraftUnitDoors.Count > 0)
        this.mVersusDraftUnitDoors.Insert(0, new VersusDraftUnitDoor()
        {
          mCategory = TobiraParam.Category.START,
          mLevel = 1
        });
      this.mMasterAbilityIName = param.master_abil;
      this.mSkinIName = param.skin;
      return true;
    }

    public Json_Unit GetJson_Unit()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return (Json_Unit) null;
      if (instance.GetUnitParam(this.mUnitIName) == null)
        return (Json_Unit) null;
      Json_Unit jsonUnit = new Json_Unit();
      jsonUnit.iid = this.mDraftUnitId;
      jsonUnit.iname = this.mUnitIName;
      jsonUnit.rare = this.mRare;
      jsonUnit.plus = this.mAwake;
      jsonUnit.exp = instance.MasterParam.GetUnitLevelExp(this.mLevel);
      jsonUnit.lv = this.mLevel;
      jsonUnit.fav = 0;
      jsonUnit.elem = 0;
      jsonUnit.select = new Json_UnitSelectable();
      jsonUnit.jobs = new Json_Job[this.mVersusDraftUnitJobs.Count];
      for (int index1 = 0; index1 < this.mVersusDraftUnitJobs.Count; ++index1)
      {
        JobParam jobParam = instance.GetJobParam(this.mVersusDraftUnitJobs[index1].mIName);
        if (jobParam != null && jobParam.ranks.Length > this.mVersusDraftUnitJobs[index1].mRank)
        {
          JobRankParam rank = jobParam.ranks[this.mVersusDraftUnitJobs[index1].mRank];
          Json_Job jsonJob = new Json_Job();
          jsonJob.iid = this.mDummyIID * 10L + (long) index1;
          jsonJob.iname = this.mVersusDraftUnitJobs[index1].mIName;
          jsonJob.rank = this.mVersusDraftUnitJobs[index1].mRank;
          jsonJob.equips = new Json_Equip[JobRankParam.MAX_RANKUP_EQUIPS];
          if (this.mVersusDraftUnitJobs[index1].mEquip)
          {
            for (int index2 = 0; index2 < JobRankParam.MAX_RANKUP_EQUIPS; ++index2)
              jsonJob.equips[index2] = new Json_Equip()
              {
                iid = jsonJob.iid * 10L + (long) index2,
                iname = rank.equips[index2]
              };
          }
          jsonJob.select = new Json_JobSelectable();
          jsonJob.select.abils = new long[5];
          jsonJob.select.artifacts = new long[3];
          List<Json_Ability> jsonAbilityList = new List<Json_Ability>();
          List<string> stringList = new List<string>();
          stringList.Add(jobParam.fixed_ability);
          for (int index2 = 1; index2 <= jsonJob.rank; ++index2)
          {
            if (jobParam.ranks.Length >= index2 && jobParam.ranks[index2] != null && jobParam.ranks[index2].learnings != null)
            {
              for (int index3 = 0; index3 < jobParam.ranks[index2].learnings.Length; ++index3)
                stringList.Add((string) jobParam.ranks[index2].learnings[index3]);
            }
          }
          for (int index2 = 0; index2 < stringList.Count; ++index2)
          {
            Json_Ability jsonAbility = new Json_Ability();
            jsonAbility.iid = jsonJob.iid * 10L + (long) index2;
            jsonAbility.iname = stringList[index2];
            jsonAbility.exp = 0;
            jsonAbilityList.Add(jsonAbility);
            if (this.mAbilities.ContainsKey(jsonAbility.iname))
            {
              jsonAbility.exp = this.mAbilities[jsonAbility.iname].mLevel - 1;
              jsonAbility.iid = this.mAbilities[jsonAbility.iname].mIID;
            }
          }
          jsonJob.abils = jsonAbilityList.ToArray();
          if (index1 == this.mSelectJobIndex)
          {
            jsonUnit.select.job = jsonJob.iid;
            jsonJob.artis = new Json_Artifact[3];
            for (int index2 = 0; index2 < this.mVersusDraftUnitArtifacts.Count; ++index2)
            {
              Json_Artifact jsonArtifact = new Json_Artifact();
              jsonArtifact.iid = jsonJob.iid * 100L + (long) index2;
              jsonArtifact.iname = this.mVersusDraftUnitArtifacts[index2].mIName;
              jsonArtifact.rare = this.mVersusDraftUnitArtifacts[index2].mRare;
              jsonArtifact.exp = ArtifactData.StaticCalcExpFromLevel(this.mVersusDraftUnitArtifacts[index2].mLevel);
              jsonJob.artis[index2] = jsonArtifact;
              jsonJob.select.artifacts[index2] = jsonArtifact.iid;
            }
            int index3 = 0;
            using (Dictionary<string, VersusDraftUnitAbility>.ValueCollection.Enumerator enumerator = this.mAbilities.Values.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                VersusDraftUnitAbility current = enumerator.Current;
                jsonJob.select.abils[index3] = current.mIID;
                ++index3;
              }
            }
            jsonJob.cur_skin = this.mSkinIName;
          }
          jsonUnit.jobs[index1] = jsonJob;
        }
      }
      if (!string.IsNullOrEmpty(this.mMasterAbilityIName))
      {
        jsonUnit.abil = new Json_MasterAbility();
        jsonUnit.abil.iid = this.mDummyIID;
        jsonUnit.abil.iname = this.mMasterAbilityIName;
        jsonUnit.abil.exp = 0;
      }
      ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(this.mConceptCardIName);
      if (conceptCardParam != null)
      {
        RarityParam rarityParam = instance.GetRarityParam(conceptCardParam.rare);
        jsonUnit.concept_card = new JSON_ConceptCard();
        jsonUnit.concept_card.iname = this.mConceptCardIName;
        jsonUnit.concept_card.iid = this.mDummyIID;
        jsonUnit.concept_card.plus = (int) rarityParam.ConceptCardAwakeCountMax;
        jsonUnit.concept_card.exp = instance.MasterParam.GetConceptCardLevelExp(conceptCardParam.rare, this.mConceptCardLevel);
        jsonUnit.concept_card.trust = 0;
        jsonUnit.concept_card.trust_bonus = 0;
        jsonUnit.concept_card.fav = 0;
      }
      jsonUnit.doors = new Json_Tobira[this.mVersusDraftUnitDoors.Count];
      List<Json_Ability> jsonAbilityList1 = new List<Json_Ability>();
      for (int index1 = 0; index1 < this.mVersusDraftUnitDoors.Count; ++index1)
      {
        Json_Tobira jsonTobira = new Json_Tobira();
        jsonTobira.category = (int) this.mVersusDraftUnitDoors[index1].mCategory;
        jsonTobira.lv = this.mVersusDraftUnitDoors[index1].mLevel;
        jsonUnit.doors[index1] = jsonTobira;
        TobiraParam tobiraParam = instance.MasterParam.GetTobiraParam(this.mUnitIName, this.mVersusDraftUnitDoors[index1].mCategory);
        if (tobiraParam != null)
        {
          for (int index2 = 0; index2 < tobiraParam.LeanAbilityParam.Length; ++index2)
          {
            TobiraLearnAbilityParam learnAbilityParam = tobiraParam.LeanAbilityParam[index2];
            if (learnAbilityParam.Level <= jsonTobira.lv)
            {
              switch (learnAbilityParam.AbilityAddType)
              {
                case TobiraLearnAbilityParam.AddType.JobOverwrite:
                  for (int index3 = 0; index3 < jsonUnit.jobs.Length; ++index3)
                  {
                    for (int index4 = 0; index4 < jsonUnit.jobs[index3].abils.Length; ++index4)
                    {
                      if (jsonUnit.jobs[index3].abils[index4].iname == learnAbilityParam.AbilityOverwrite)
                      {
                        jsonUnit.jobs[index3].abils[index4].iname = learnAbilityParam.AbilityIname;
                        if (this.mAbilities.ContainsKey(learnAbilityParam.AbilityIname))
                        {
                          jsonUnit.jobs[index3].abils[index4].iid = this.mAbilities[learnAbilityParam.AbilityIname].mIID;
                          jsonUnit.jobs[index3].abils[index4].exp = this.mAbilities[learnAbilityParam.AbilityIname].mLevel - 1;
                        }
                      }
                    }
                  }
                  continue;
                case TobiraLearnAbilityParam.AddType.MasterAdd:
                  jsonAbilityList1.Add(new Json_Ability()
                  {
                    iid = this.mDummyIID * 100L + (long) (index1 * 10) + (long) index2,
                    iname = learnAbilityParam.AbilityIname,
                    exp = 0
                  });
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      jsonUnit.door_abils = jsonAbilityList1.ToArray();
      return jsonUnit;
    }
  }
}
