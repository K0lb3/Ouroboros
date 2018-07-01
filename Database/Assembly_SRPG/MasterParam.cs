// Decompiled with JetBrains decompiler
// Type: SRPG.MasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class MasterParam
  {
    private FixParam mFixParam = new FixParam();
    private Dictionary<string, JobParam> mJobParamDict = new Dictionary<string, JobParam>();
    private List<UnitParam> mUnitParam;
    private List<SkillParam> mSkillParam;
    private List<BuffEffectParam> mBuffEffectParam;
    private List<CondEffectParam> mCondEffectParam;
    private List<AbilityParam> mAbilityParam;
    private List<ItemParam> mItemParam;
    private List<ArtifactParam> mArtifactParam;
    private List<WeaponParam> mWeaponParam;
    private List<RecipeParam> mRecipeParam;
    private List<JobParam> mJobParam;
    private List<QuestClearUnlockUnitDataParam> mUnlockUnitDataParam;
    private List<CollaboSkillParam> mCollaboSkillParam;
    private List<TrickParam> mTrickParam;
    private List<BreakObjParam> mBreakObjParam;
    private List<WeatherParam> mWeatherParam;
    private List<JobSetParam> mJobSetParam;
    private List<GrowParam> mGrowParam;
    private List<AIParam> mAIParam;
    private List<GeoParam> mGeoParam;
    private List<RarityParam> mRarityParam;
    private List<ShopParam> mShopParam;
    private PlayerParam[] mPlayerParamTbl;
    private OInt[] mPlayerExpTbl;
    private OInt[] mUnitExpTbl;
    private OInt[] mArtifactExpTbl;
    private OInt[] mAbilityExpTbl;
    private OInt[] mAwakePieceTbl;
    private LocalNotificationParam mLocalNotificationParam;
    private TrophyCategoryParam[] mTrophyCategory;
    private ChallengeCategoryParam[] mChallengeCategory;
    private TrophyParam[] mTrophy;
    public Dictionary<string, TrophyParam> mTrophyInameDict;
    private TrophyObjective[][] mTrophyDict;
    private TrophyParam[] mChallenge;
    private UnlockParam[] mUnlock;
    private VipParam[] mVip;
    private JSON_StreamingMovie[] mStreamingMovies;
    private BannerParam[] mBanner;
    private List<VersusMatchingParam> mVersusMatching;
    private List<VersusMatchCondParam> mVersusMatchCond;
    private List<TowerScoreParam> mTowerScore;
    private OInt[] mTowerRankTbl;
    public OInt[] mMultiLimitUnitLv;
    private Dictionary<string, UnitParam> mUnitDictionary;
    private Dictionary<string, SkillParam> mSkillDictionary;
    private Dictionary<string, AbilityParam> mAbilityDictionary;
    private Dictionary<string, ItemParam> mItemDictionary;
    private Dictionary<string, ArtifactParam> mArtifactDictionary;
    private List<AwardParam> mAwardParam;
    private Dictionary<string, AwardParam> mAwardDictionary;
    private Dictionary<string, List<JobSetParam>> mJobsetDictionary;
    private LoginInfoParam[] mLoginInfoParam;
    private Dictionary<string, FriendPresentItemParam> mFriendPresentItemParam;

    public bool Deserialize(string language, JSON_MasterParam json)
    {
      if (this.Loaded)
        return true;
      this.mLocalNotificationParam = (LocalNotificationParam) null;
      this.mFixParam.Deserialize(json.Fix[0]);
      if (json.Unit != null)
      {
        if (this.mUnitParam == null)
          this.mUnitParam = new List<UnitParam>(json.Unit.Length);
        if (this.mUnitDictionary == null)
          this.mUnitDictionary = new Dictionary<string, UnitParam>(json.Unit.Length);
        for (int index = 0; index < json.Unit.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey20C deserializeCAnonStorey20C = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey20C();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey20C.data = json.Unit[index];
          // ISSUE: reference to a compiler-generated method
          UnitParam unitParam = this.mUnitParam.Find(new Predicate<UnitParam>(deserializeCAnonStorey20C.\u003C\u003Em__181));
          if (unitParam == null)
          {
            unitParam = new UnitParam();
            this.mUnitParam.Add(unitParam);
          }
          // ISSUE: reference to a compiler-generated field
          unitParam.Deserialize(language, deserializeCAnonStorey20C.data);
          // ISSUE: reference to a compiler-generated field
          if (!this.mUnitDictionary.ContainsKey(deserializeCAnonStorey20C.data.iname))
          {
            // ISSUE: reference to a compiler-generated field
            this.mUnitDictionary.Add(deserializeCAnonStorey20C.data.iname, unitParam);
          }
        }
      }
      if (json.Skill != null)
      {
        if (this.mSkillParam == null)
          this.mSkillParam = new List<SkillParam>(json.Skill.Length);
        if (this.mSkillDictionary == null)
          this.mSkillDictionary = new Dictionary<string, SkillParam>(json.Skill.Length);
        for (int index = 0; index < json.Skill.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey20D deserializeCAnonStorey20D = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey20D();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey20D.data = json.Skill[index];
          // ISSUE: reference to a compiler-generated method
          SkillParam skillParam = this.mSkillParam.Find(new Predicate<SkillParam>(deserializeCAnonStorey20D.\u003C\u003Em__182));
          if (skillParam == null)
          {
            skillParam = new SkillParam();
            this.mSkillParam.Add(skillParam);
          }
          // ISSUE: reference to a compiler-generated field
          skillParam.Deserialize(language, deserializeCAnonStorey20D.data);
          // ISSUE: reference to a compiler-generated field
          if (!this.mSkillDictionary.ContainsKey(deserializeCAnonStorey20D.data.iname))
          {
            // ISSUE: reference to a compiler-generated field
            this.mSkillDictionary.Add(deserializeCAnonStorey20D.data.iname, skillParam);
          }
        }
      }
      if (json.Buff != null)
      {
        if (this.mBuffEffectParam == null)
          this.mBuffEffectParam = new List<BuffEffectParam>(json.Buff.Length);
        for (int index = 0; index < json.Buff.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey20E deserializeCAnonStorey20E = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey20E();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey20E.data = json.Buff[index];
          // ISSUE: reference to a compiler-generated method
          BuffEffectParam buffEffectParam = this.mBuffEffectParam.Find(new Predicate<BuffEffectParam>(deserializeCAnonStorey20E.\u003C\u003Em__183));
          if (buffEffectParam == null)
          {
            buffEffectParam = new BuffEffectParam();
            this.mBuffEffectParam.Add(buffEffectParam);
          }
          // ISSUE: reference to a compiler-generated field
          buffEffectParam.Deserialize(deserializeCAnonStorey20E.data);
        }
      }
      if (json.Cond != null)
      {
        if (this.mCondEffectParam == null)
          this.mCondEffectParam = new List<CondEffectParam>(json.Cond.Length);
        for (int index = 0; index < json.Cond.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey20F deserializeCAnonStorey20F = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey20F();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey20F.data = json.Cond[index];
          // ISSUE: reference to a compiler-generated method
          CondEffectParam condEffectParam = this.mCondEffectParam.Find(new Predicate<CondEffectParam>(deserializeCAnonStorey20F.\u003C\u003Em__184));
          if (condEffectParam == null)
          {
            condEffectParam = new CondEffectParam();
            this.mCondEffectParam.Add(condEffectParam);
          }
          // ISSUE: reference to a compiler-generated field
          condEffectParam.Deserialize(deserializeCAnonStorey20F.data);
        }
      }
      if (json.Ability != null)
      {
        if (this.mAbilityParam == null)
          this.mAbilityParam = new List<AbilityParam>(json.Ability.Length);
        if (this.mAbilityDictionary == null)
          this.mAbilityDictionary = new Dictionary<string, AbilityParam>(json.Ability.Length);
        for (int index = 0; index < json.Ability.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey210 deserializeCAnonStorey210 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey210();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey210.data = json.Ability[index];
          // ISSUE: reference to a compiler-generated method
          AbilityParam abilityParam = this.mAbilityParam.Find(new Predicate<AbilityParam>(deserializeCAnonStorey210.\u003C\u003Em__185));
          if (abilityParam == null)
          {
            abilityParam = new AbilityParam();
            this.mAbilityParam.Add(abilityParam);
          }
          // ISSUE: reference to a compiler-generated field
          abilityParam.Deserialize(language, deserializeCAnonStorey210.data);
          // ISSUE: reference to a compiler-generated field
          if (!this.mAbilityDictionary.ContainsKey(deserializeCAnonStorey210.data.iname))
          {
            // ISSUE: reference to a compiler-generated field
            this.mAbilityDictionary.Add(deserializeCAnonStorey210.data.iname, abilityParam);
          }
        }
      }
      if (json.Item != null)
      {
        if (this.mItemParam == null)
          this.mItemParam = new List<ItemParam>(json.Item.Length);
        if (this.mItemDictionary == null)
          this.mItemDictionary = new Dictionary<string, ItemParam>(json.Item.Length);
        for (int index = 0; index < json.Item.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey211 deserializeCAnonStorey211 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey211();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey211.data = json.Item[index];
          // ISSUE: reference to a compiler-generated method
          ItemParam itemParam = this.mItemParam.Find(new Predicate<ItemParam>(deserializeCAnonStorey211.\u003C\u003Em__186));
          if (itemParam == null)
          {
            itemParam = new ItemParam();
            this.mItemParam.Add(itemParam);
          }
          // ISSUE: reference to a compiler-generated field
          itemParam.Deserialize(language, deserializeCAnonStorey211.data);
          itemParam.no = index + 1;
          // ISSUE: reference to a compiler-generated field
          if (!this.mItemDictionary.ContainsKey(deserializeCAnonStorey211.data.iname))
          {
            // ISSUE: reference to a compiler-generated field
            this.mItemDictionary.Add(deserializeCAnonStorey211.data.iname, itemParam);
          }
        }
        this.AddUnitToItem();
      }
      if (json.Artifact != null)
      {
        if (this.mArtifactParam == null)
          this.mArtifactParam = new List<ArtifactParam>(json.Artifact.Length);
        if (this.mArtifactDictionary == null)
          this.mArtifactDictionary = new Dictionary<string, ArtifactParam>(json.Artifact.Length);
        for (int index = 0; index < json.Artifact.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey212 deserializeCAnonStorey212 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey212();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey212.data = json.Artifact[index];
          // ISSUE: reference to a compiler-generated field
          if (deserializeCAnonStorey212.data.iname != null)
          {
            // ISSUE: reference to a compiler-generated method
            ArtifactParam artifactParam = this.mArtifactParam.Find(new Predicate<ArtifactParam>(deserializeCAnonStorey212.\u003C\u003Em__187));
            if (artifactParam == null)
            {
              artifactParam = new ArtifactParam();
              this.mArtifactParam.Add(artifactParam);
            }
            // ISSUE: reference to a compiler-generated field
            artifactParam.Deserialize(language, deserializeCAnonStorey212.data);
            // ISSUE: reference to a compiler-generated field
            if (!this.mArtifactDictionary.ContainsKey(deserializeCAnonStorey212.data.iname))
            {
              // ISSUE: reference to a compiler-generated field
              this.mArtifactDictionary.Add(deserializeCAnonStorey212.data.iname, artifactParam);
            }
          }
        }
      }
      if (json.Weapon != null)
      {
        if (this.mWeaponParam == null)
          this.mWeaponParam = new List<WeaponParam>(json.Weapon.Length);
        for (int index = 0; index < json.Weapon.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey213 deserializeCAnonStorey213 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey213();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey213.data = json.Weapon[index];
          // ISSUE: reference to a compiler-generated method
          WeaponParam weaponParam = this.mWeaponParam.Find(new Predicate<WeaponParam>(deserializeCAnonStorey213.\u003C\u003Em__188));
          if (weaponParam == null)
          {
            weaponParam = new WeaponParam();
            this.mWeaponParam.Add(weaponParam);
          }
          // ISSUE: reference to a compiler-generated field
          weaponParam.Deserialize(deserializeCAnonStorey213.data);
        }
      }
      if (json.Recipe != null)
      {
        if (this.mRecipeParam == null)
          this.mRecipeParam = new List<RecipeParam>(json.Recipe.Length);
        for (int index = 0; index < json.Recipe.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey214 deserializeCAnonStorey214 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey214();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey214.data = json.Recipe[index];
          // ISSUE: reference to a compiler-generated method
          RecipeParam recipeParam = this.mRecipeParam.Find(new Predicate<RecipeParam>(deserializeCAnonStorey214.\u003C\u003Em__189));
          if (recipeParam == null)
          {
            recipeParam = new RecipeParam();
            this.mRecipeParam.Add(recipeParam);
          }
          // ISSUE: reference to a compiler-generated field
          recipeParam.Deserialize(deserializeCAnonStorey214.data);
        }
      }
      if (json.Job != null)
      {
        if (this.mJobParam == null)
          this.mJobParam = new List<JobParam>(json.Job.Length);
        for (int index = 0; index < json.Job.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey215 deserializeCAnonStorey215 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey215();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey215.data = json.Job[index];
          // ISSUE: reference to a compiler-generated method
          JobParam jobParam = this.mJobParam.Find(new Predicate<JobParam>(deserializeCAnonStorey215.\u003C\u003Em__18A));
          if (jobParam == null)
          {
            jobParam = new JobParam();
            this.mJobParam.Add(jobParam);
            // ISSUE: reference to a compiler-generated field
            this.mJobParamDict[deserializeCAnonStorey215.data.iname] = jobParam;
          }
          // ISSUE: reference to a compiler-generated field
          jobParam.Deserialize(language, deserializeCAnonStorey215.data);
        }
      }
      if (json.JobSet != null)
      {
        if (this.mJobSetParam == null)
          this.mJobSetParam = new List<JobSetParam>(json.JobSet.Length);
        if (this.mJobsetDictionary == null)
          this.mJobsetDictionary = new Dictionary<string, List<JobSetParam>>(json.Unit.Length);
        for (int index = 0; index < json.JobSet.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey216 deserializeCAnonStorey216 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey216();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey216.data = json.JobSet[index];
          // ISSUE: reference to a compiler-generated method
          JobSetParam jobSetParam = this.mJobSetParam.Find(new Predicate<JobSetParam>(deserializeCAnonStorey216.\u003C\u003Em__18B));
          if (jobSetParam == null)
          {
            jobSetParam = new JobSetParam();
            this.mJobSetParam.Add(jobSetParam);
          }
          // ISSUE: reference to a compiler-generated field
          jobSetParam.Deserialize(deserializeCAnonStorey216.data);
          if (!string.IsNullOrEmpty(jobSetParam.target_unit))
          {
            List<JobSetParam> jobSetParamList;
            if (this.mJobsetDictionary.ContainsKey(jobSetParam.target_unit))
            {
              jobSetParamList = this.mJobsetDictionary[jobSetParam.target_unit];
            }
            else
            {
              jobSetParamList = new List<JobSetParam>(3);
              this.mJobsetDictionary.Add(jobSetParam.target_unit, jobSetParamList);
            }
            jobSetParamList.Add(jobSetParam);
          }
        }
      }
      if (json.Grow != null)
      {
        if (this.mGrowParam == null)
          this.mGrowParam = new List<GrowParam>(json.Grow.Length);
        for (int index = 0; index < json.Grow.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey217 deserializeCAnonStorey217 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey217();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey217.data = json.Grow[index];
          // ISSUE: reference to a compiler-generated method
          GrowParam growParam = this.mGrowParam.Find(new Predicate<GrowParam>(deserializeCAnonStorey217.\u003C\u003Em__18C));
          if (growParam == null)
          {
            growParam = new GrowParam();
            this.mGrowParam.Add(growParam);
          }
          // ISSUE: reference to a compiler-generated field
          growParam.Deserialize(deserializeCAnonStorey217.data);
        }
      }
      if (json.AI != null)
      {
        if (this.mAIParam == null)
          this.mAIParam = new List<AIParam>(json.AI.Length);
        for (int index = 0; index < json.AI.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey218 deserializeCAnonStorey218 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey218();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey218.data = json.AI[index];
          // ISSUE: reference to a compiler-generated method
          AIParam aiParam = this.mAIParam.Find(new Predicate<AIParam>(deserializeCAnonStorey218.\u003C\u003Em__18D));
          if (aiParam == null)
          {
            aiParam = new AIParam();
            this.mAIParam.Add(aiParam);
          }
          // ISSUE: reference to a compiler-generated field
          aiParam.Deserialize(deserializeCAnonStorey218.data);
        }
      }
      if (json.Geo != null)
      {
        if (this.mGeoParam == null)
          this.mGeoParam = new List<GeoParam>(json.Geo.Length);
        for (int index = 0; index < json.Geo.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey219 deserializeCAnonStorey219 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey219();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey219.data = json.Geo[index];
          // ISSUE: reference to a compiler-generated method
          GeoParam geoParam = this.mGeoParam.Find(new Predicate<GeoParam>(deserializeCAnonStorey219.\u003C\u003Em__18E));
          if (geoParam == null)
          {
            geoParam = new GeoParam();
            this.mGeoParam.Add(geoParam);
          }
          // ISSUE: reference to a compiler-generated field
          geoParam.Deserialize(language, deserializeCAnonStorey219.data);
        }
      }
      if (json.Rarity != null)
      {
        if (this.mRarityParam == null)
          this.mRarityParam = new List<RarityParam>(json.Rarity.Length);
        for (int index = 0; index < json.Rarity.Length; ++index)
        {
          RarityParam rarityParam;
          if (this.mRarityParam.Count > index)
          {
            rarityParam = this.mRarityParam[index];
          }
          else
          {
            rarityParam = new RarityParam();
            this.mRarityParam.Add(rarityParam);
          }
          rarityParam.Deserialize(json.Rarity[index]);
        }
      }
      if (json.Shop != null)
      {
        if (this.mShopParam == null)
          this.mShopParam = new List<ShopParam>(json.Shop.Length);
        for (int index = 0; index < json.Shop.Length; ++index)
        {
          ShopParam shopParam;
          if (this.mShopParam.Count > index)
          {
            shopParam = this.mShopParam[index];
          }
          else
          {
            shopParam = new ShopParam();
            this.mShopParam.Add(shopParam);
          }
          shopParam.Deserialize(json.Shop[index]);
        }
      }
      if (json.Player != null)
      {
        this.mPlayerParamTbl = new PlayerParam[json.Player.Length];
        for (int index = 0; index < json.Player.Length; ++index)
        {
          JSON_PlayerParam json1 = json.Player[index];
          this.mPlayerParamTbl[index] = new PlayerParam();
          this.mPlayerParamTbl[index].Deserialize(json1);
        }
      }
      if (json.PlayerLvTbl != null)
      {
        this.mPlayerExpTbl = new OInt[json.PlayerLvTbl.Length];
        for (int index = 0; index < json.PlayerLvTbl.Length; ++index)
          this.mPlayerExpTbl[index] = (OInt) json.PlayerLvTbl[index];
      }
      if (json.UnitLvTbl != null)
      {
        this.mUnitExpTbl = new OInt[json.UnitLvTbl.Length];
        for (int index = 0; index < json.UnitLvTbl.Length; ++index)
          this.mUnitExpTbl[index] = (OInt) json.UnitLvTbl[index];
      }
      if (json.ArtifactLvTbl != null)
      {
        this.mArtifactExpTbl = new OInt[json.ArtifactLvTbl.Length];
        for (int index = 0; index < json.ArtifactLvTbl.Length; ++index)
          this.mArtifactExpTbl[index] = (OInt) json.ArtifactLvTbl[index];
      }
      if (json.AbilityRank != null)
      {
        this.mAbilityExpTbl = new OInt[json.AbilityRank.Length];
        for (int index = 0; index < json.AbilityRank.Length; ++index)
          this.mAbilityExpTbl[index] = (OInt) json.AbilityRank[index];
      }
      if (json.AwakePieceTbl != null)
      {
        this.mAwakePieceTbl = new OInt[json.AwakePieceTbl.Length];
        for (int index = 0; index < json.AwakePieceTbl.Length; ++index)
          this.mAwakePieceTbl[index] = (OInt) json.AwakePieceTbl[index];
      }
      this.mLocalNotificationParam = new LocalNotificationParam();
      if (json.LocalNotification != null)
      {
        this.mLocalNotificationParam.msg_stamina = json.LocalNotification[0].msg_stamina;
        this.mLocalNotificationParam.iOSAct_stamina = json.LocalNotification[0].iOSAct_stamina;
        this.mLocalNotificationParam.limitSec_stamina = json.LocalNotification[0].limitSec_stamina;
      }
      Dictionary<int, TrophyCategoryParam> dictionary = new Dictionary<int, TrophyCategoryParam>();
      if (json.TrophyCategory != null)
      {
        List<TrophyCategoryParam> trophyCategoryParamList = new List<TrophyCategoryParam>(json.TrophyCategory.Length);
        for (int index = 0; index < json.TrophyCategory.Length; ++index)
        {
          TrophyCategoryParam trophyCategoryParam = new TrophyCategoryParam();
          if (trophyCategoryParam.Deserialize(language, json.TrophyCategory[index]))
          {
            trophyCategoryParamList.Add(trophyCategoryParam);
            if (!dictionary.ContainsKey(trophyCategoryParam.hash_code))
              dictionary.Add(trophyCategoryParam.hash_code, trophyCategoryParam);
          }
        }
        this.mTrophyCategory = trophyCategoryParamList.ToArray();
      }
      if (json.Trophy != null && dictionary.Count > 0)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Trophy.Length);
        for (int index = 0; index < json.Trophy.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(language, json.Trophy[index]))
          {
            if (dictionary.ContainsKey(trophyParam.category_hash_code))
              trophyParam.CategoryParam = dictionary[trophyParam.category_hash_code];
            else
              DebugUtility.LogError(trophyParam.iname + " => 親カテゴリが未設定 or 入力ミス");
            trophyParamList.Add(trophyParam);
          }
        }
        this.mTrophy = trophyParamList.ToArray();
        this.mTrophyInameDict = new Dictionary<string, TrophyParam>();
        foreach (TrophyParam trophyParam in this.mTrophy)
          this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
      }
      if (json.ChallengeCategory != null)
      {
        List<ChallengeCategoryParam> challengeCategoryParamList = new List<ChallengeCategoryParam>(json.ChallengeCategory.Length);
        for (int index = 0; index < json.ChallengeCategory.Length; ++index)
        {
          ChallengeCategoryParam challengeCategoryParam = new ChallengeCategoryParam();
          if (challengeCategoryParam.Deserialize(json.ChallengeCategory[index]))
            challengeCategoryParamList.Add(challengeCategoryParam);
        }
        this.mChallengeCategory = challengeCategoryParamList.ToArray();
      }
      if (json.Challenge != null)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Challenge.Length);
        for (int index = 0; index < json.Challenge.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(language, json.Challenge[index]))
          {
            trophyParam.Challenge = 1;
            trophyParamList.Add(trophyParam);
          }
        }
        this.mChallenge = trophyParamList.ToArray();
        int length = this.mTrophy.Length;
        Array.Resize<TrophyParam>(ref this.mTrophy, length + this.mChallenge.Length);
        Array.Copy((Array) this.mChallenge, 0, (Array) this.mTrophy, length, this.mChallenge.Length);
        foreach (TrophyParam trophyParam in this.mChallenge)
          this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
      }
      this.CreateTrophyDict();
      if (json.Unlock != null)
      {
        List<UnlockParam> unlockParamList = new List<UnlockParam>(json.Unlock.Length);
        for (int index = 0; index < json.Unlock.Length; ++index)
        {
          UnlockParam unlockParam = new UnlockParam();
          if (unlockParam.Deserialize(json.Unlock[index]))
            unlockParamList.Add(unlockParam);
        }
        this.mUnlock = unlockParamList.ToArray();
      }
      if (json.Vip != null)
      {
        List<VipParam> vipParamList = new List<VipParam>(json.Vip.Length);
        for (int index = 0; index < json.Vip.Length; ++index)
        {
          VipParam vipParam = new VipParam();
          if (vipParam.Deserialize(json.Vip[index]))
            vipParamList.Add(vipParam);
        }
        this.mVip = vipParamList.ToArray();
      }
      if (json.Mov != null)
      {
        this.mStreamingMovies = new JSON_StreamingMovie[json.Mov.Length];
        for (int index = 0; index < json.Mov.Length; ++index)
        {
          this.mStreamingMovies[index] = new JSON_StreamingMovie();
          this.mStreamingMovies[index].alias = json.Mov[index].alias;
          this.mStreamingMovies[index].path = json.Mov[index].path;
        }
      }
      if (json.Banner != null)
      {
        List<BannerParam> bannerParamList = new List<BannerParam>(json.Banner.Length);
        for (int index = 0; index < json.Banner.Length; ++index)
        {
          BannerParam bannerParam = new BannerParam();
          if (bannerParam.Deserialize(json.Banner[index]))
            bannerParamList.Add(bannerParam);
        }
        this.mBanner = bannerParamList.ToArray();
      }
      for (int index = 0; index < this.mJobParam.Count; ++index)
        this.mJobParam[index].UpdateJobRankTransfarStatus(this);
      if (json.QuestClearUnlockUnitData != null)
      {
        List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>(json.QuestClearUnlockUnitData.Length);
        for (int index = 0; index < json.QuestClearUnlockUnitData.Length; ++index)
        {
          QuestClearUnlockUnitDataParam unlockUnitDataParam = new QuestClearUnlockUnitDataParam();
          unlockUnitDataParam.Deserialize(json.QuestClearUnlockUnitData[index]);
          unlockUnitDataParamList.Add(unlockUnitDataParam);
        }
        this.mUnlockUnitDataParam = unlockUnitDataParamList;
      }
      if (json.Award != null)
      {
        if (this.mAwardParam == null)
          this.mAwardParam = new List<AwardParam>(json.Award.Length);
        if (this.mAwardDictionary == null)
          this.mAwardDictionary = new Dictionary<string, AwardParam>(json.Award.Length);
        for (int index = 0; index < json.Award.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey21A deserializeCAnonStorey21A = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey21A();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey21A.data = json.Award[index];
          // ISSUE: reference to a compiler-generated field
          if (deserializeCAnonStorey21A.data.iname != null)
          {
            // ISSUE: reference to a compiler-generated method
            AwardParam awardParam = this.mAwardParam.Find(new Predicate<AwardParam>(deserializeCAnonStorey21A.\u003C\u003Em__18F));
            if (awardParam == null)
            {
              awardParam = new AwardParam();
              this.mAwardParam.Add(awardParam);
            }
            // ISSUE: reference to a compiler-generated field
            awardParam.Deserialize(language, deserializeCAnonStorey21A.data);
            if (!this.mAwardDictionary.ContainsKey(awardParam.iname))
              this.mAwardDictionary.Add(awardParam.iname, awardParam);
          }
        }
      }
      if (json.LoginInfo != null)
      {
        List<LoginInfoParam> loginInfoParamList = new List<LoginInfoParam>(json.LoginInfo.Length);
        for (int index = 0; index < json.LoginInfo.Length; ++index)
        {
          LoginInfoParam loginInfoParam = new LoginInfoParam();
          if (loginInfoParam.Deserialize(json.LoginInfo[index]))
            loginInfoParamList.Add(loginInfoParam);
        }
        this.mLoginInfoParam = loginInfoParamList.ToArray();
      }
      if (json.CollaboSkill != null)
      {
        List<CollaboSkillParam> collaboSkillParamList = new List<CollaboSkillParam>(json.CollaboSkill.Length);
        for (int index = 0; index < json.CollaboSkill.Length; ++index)
        {
          CollaboSkillParam collaboSkillParam = new CollaboSkillParam();
          collaboSkillParam.Deserialize(json.CollaboSkill[index]);
          collaboSkillParamList.Add(collaboSkillParam);
        }
        this.mCollaboSkillParam = collaboSkillParamList;
        CollaboSkillParam.UpdateCollaboSkill(this.mCollaboSkillParam);
      }
      if (json.Trick != null)
      {
        List<TrickParam> trickParamList = new List<TrickParam>(json.Trick.Length);
        for (int index = 0; index < json.Trick.Length; ++index)
        {
          TrickParam trickParam = new TrickParam();
          trickParam.Deserialize(language, json.Trick[index]);
          trickParamList.Add(trickParam);
        }
        this.mTrickParam = trickParamList;
      }
      if (json.BreakObj != null)
      {
        List<BreakObjParam> breakObjParamList = new List<BreakObjParam>(json.BreakObj.Length);
        for (int index = 0; index < json.BreakObj.Length; ++index)
        {
          BreakObjParam breakObjParam = new BreakObjParam();
          breakObjParam.Deserialize(language, json.BreakObj[index]);
          breakObjParamList.Add(breakObjParam);
        }
        this.mBreakObjParam = breakObjParamList;
      }
      if (json.VersusMatchKey != null)
      {
        this.mVersusMatching = new List<VersusMatchingParam>(json.VersusMatchKey.Length);
        for (int index = 0; index < json.VersusMatchKey.Length; ++index)
        {
          VersusMatchingParam versusMatchingParam = new VersusMatchingParam();
          versusMatchingParam.Deserialize(json.VersusMatchKey[index]);
          this.mVersusMatching.Add(versusMatchingParam);
        }
      }
      if (json.VersusMatchCond != null)
      {
        this.mVersusMatchCond = new List<VersusMatchCondParam>(json.VersusMatchCond.Length);
        for (int index = 0; index < json.VersusMatchCond.Length; ++index)
        {
          VersusMatchCondParam versusMatchCondParam = new VersusMatchCondParam();
          versusMatchCondParam.Deserialize(json.VersusMatchCond[index]);
          this.mVersusMatchCond.Add(versusMatchCondParam);
        }
      }
      if (json.TowerScore != null)
      {
        this.mTowerScore = new List<TowerScoreParam>(json.TowerScore.Length);
        for (int index = 0; index < json.TowerScore.Length; ++index)
        {
          TowerScoreParam towerScoreParam = new TowerScoreParam();
          if (towerScoreParam != null && towerScoreParam.Deserialize(json.TowerScore[index]))
            this.mTowerScore.Add(towerScoreParam);
        }
      }
      if (json.TowerRank != null)
      {
        this.mTowerRankTbl = new OInt[json.TowerRank.Length];
        for (int index = 0; index < json.TowerRank.Length; ++index)
          this.mTowerRankTbl[index] = (OInt) json.TowerRank[index];
      }
      if (json.MultilimitUnitLv != null)
      {
        this.mMultiLimitUnitLv = new OInt[json.MultilimitUnitLv.Length];
        for (int index = 0; index < json.MultilimitUnitLv.Length; ++index)
          this.mMultiLimitUnitLv[index] = (OInt) json.MultilimitUnitLv[index];
      }
      if (json.FriendPresentItem != null)
      {
        this.mFriendPresentItemParam = new Dictionary<string, FriendPresentItemParam>();
        for (int index = 0; index < json.FriendPresentItem.Length; ++index)
        {
          FriendPresentItemParam presentItemParam = new FriendPresentItemParam();
          presentItemParam.Deserialize(language, json.FriendPresentItem[index]);
          this.mFriendPresentItemParam.Add(presentItemParam.iname, presentItemParam);
        }
      }
      if (json.Weather != null)
      {
        List<WeatherParam> weatherParamList = new List<WeatherParam>(json.Weather.Length);
        for (int index = 0; index < json.Weather.Length; ++index)
        {
          WeatherParam weatherParam = new WeatherParam();
          weatherParam.Deserialize(language, json.Weather[index]);
          weatherParamList.Add(weatherParam);
        }
        this.mWeatherParam = weatherParamList;
      }
      this.Loaded = true;
      return true;
    }

    public bool Loaded { get; set; }

    public FixParam FixParam
    {
      get
      {
        return this.mFixParam;
      }
    }

    public LocalNotificationParam LocalNotificationParam
    {
      get
      {
        return this.mLocalNotificationParam;
      }
    }

    public List<ItemParam> Items
    {
      get
      {
        return this.mItemParam;
      }
    }

    public JobSetParam[] JobSets
    {
      get
      {
        return this.mJobSetParam.ToArray();
      }
    }

    public List<ArtifactParam> Artifacts
    {
      get
      {
        return this.mArtifactParam;
      }
    }

    public List<CollaboSkillParam> CollaboSkills
    {
      get
      {
        return this.mCollaboSkillParam;
      }
    }

    private void CreateTrophyDict()
    {
      List<TrophyObjective> trophyObjectiveList = new List<TrophyObjective>(this.mTrophy.Length);
      Array values = Enum.GetValues(typeof (TrophyConditionTypes));
      this.mTrophyDict = new TrophyObjective[values.Length][];
      for (int index1 = 0; index1 < values.Length; ++index1)
      {
        TrophyConditionTypes trophyConditionTypes = (TrophyConditionTypes) values.GetValue(index1);
        trophyObjectiveList.Clear();
        for (int index2 = 0; index2 < this.mTrophy.Length; ++index2)
        {
          TrophyParam trophyParam = this.mTrophy[index2];
          for (int index3 = 0; index3 < trophyParam.Objectives.Length; ++index3)
          {
            if (trophyParam.Objectives[index3].type == trophyConditionTypes)
              trophyObjectiveList.Add(trophyParam.Objectives[index3]);
          }
        }
        this.mTrophyDict[index1] = trophyObjectiveList.ToArray();
      }
    }

    private void AddUnitToItem()
    {
      if (this.mUnitDictionary.Count <= 0)
        return;
      using (Dictionary<string, UnitParam>.Enumerator enumerator = this.mUnitDictionary.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, UnitParam> current = enumerator.Current;
          if (current.Value != null && !string.IsNullOrEmpty((string) current.Value.piece))
          {
            ItemParam itemParam = new ItemParam();
            itemParam.iname = current.Value.iname;
            if (this.mItemDictionary.ContainsKey((string) current.Value.piece))
            {
              ItemParam mItem = this.mItemDictionary[(string) current.Value.piece];
              if (mItem != null)
              {
                itemParam.icon = mItem.icon;
                itemParam.type = EItemType.Unit;
                itemParam.cap = (OInt) 999;
                this.mItemDictionary.Add(itemParam.iname, itemParam);
              }
            }
          }
        }
      }
    }

    public bool Deserialize(JSON_MasterParam json)
    {
      if (this.Loaded)
        return true;
      DebugUtility.Verify((object) json, typeof (JSON_MasterParam));
      this.mLocalNotificationParam = (LocalNotificationParam) null;
      this.mFixParam.Deserialize(json.Fix[0]);
      if (json.Unit != null)
      {
        if (this.mUnitParam == null)
          this.mUnitParam = new List<UnitParam>(json.Unit.Length);
        if (this.mUnitDictionary == null)
          this.mUnitDictionary = new Dictionary<string, UnitParam>(json.Unit.Length);
        for (int index = 0; index < json.Unit.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey21B deserializeCAnonStorey21B = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey21B();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey21B.data = json.Unit[index];
          // ISSUE: reference to a compiler-generated method
          UnitParam unitParam = this.mUnitParam.Find(new Predicate<UnitParam>(deserializeCAnonStorey21B.\u003C\u003Em__190));
          if (unitParam == null)
          {
            unitParam = new UnitParam();
            this.mUnitParam.Add(unitParam);
          }
          // ISSUE: reference to a compiler-generated field
          unitParam.Deserialize(deserializeCAnonStorey21B.data);
          // ISSUE: reference to a compiler-generated field
          if (this.mUnitDictionary.ContainsKey(deserializeCAnonStorey21B.data.iname))
          {
            // ISSUE: reference to a compiler-generated field
            throw new Exception("重複エラー：Unit[" + deserializeCAnonStorey21B.data.iname + "]");
          }
          // ISSUE: reference to a compiler-generated field
          this.mUnitDictionary.Add(deserializeCAnonStorey21B.data.iname, unitParam);
        }
      }
      if (json.Skill != null)
      {
        if (this.mSkillParam == null)
          this.mSkillParam = new List<SkillParam>(json.Skill.Length);
        if (this.mSkillDictionary == null)
          this.mSkillDictionary = new Dictionary<string, SkillParam>(json.Skill.Length);
        for (int index = 0; index < json.Skill.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey21C deserializeCAnonStorey21C = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey21C();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey21C.data = json.Skill[index];
          // ISSUE: reference to a compiler-generated method
          SkillParam skillParam = this.mSkillParam.Find(new Predicate<SkillParam>(deserializeCAnonStorey21C.\u003C\u003Em__191));
          if (skillParam == null)
          {
            skillParam = new SkillParam();
            this.mSkillParam.Add(skillParam);
          }
          // ISSUE: reference to a compiler-generated field
          skillParam.Deserialize(deserializeCAnonStorey21C.data);
          // ISSUE: reference to a compiler-generated field
          if (this.mSkillDictionary.ContainsKey(deserializeCAnonStorey21C.data.iname))
          {
            // ISSUE: reference to a compiler-generated field
            throw new Exception("重複エラー：Skill[" + deserializeCAnonStorey21C.data.iname + "]");
          }
          // ISSUE: reference to a compiler-generated field
          this.mSkillDictionary.Add(deserializeCAnonStorey21C.data.iname, skillParam);
        }
        SkillParam.UpdateReplaceSkill(this.mSkillParam);
      }
      if (json.Buff != null)
      {
        if (this.mBuffEffectParam == null)
          this.mBuffEffectParam = new List<BuffEffectParam>(json.Buff.Length);
        for (int index = 0; index < json.Buff.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey21D deserializeCAnonStorey21D = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey21D();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey21D.data = json.Buff[index];
          // ISSUE: reference to a compiler-generated method
          BuffEffectParam buffEffectParam = this.mBuffEffectParam.Find(new Predicate<BuffEffectParam>(deserializeCAnonStorey21D.\u003C\u003Em__192));
          if (buffEffectParam == null)
          {
            buffEffectParam = new BuffEffectParam();
            this.mBuffEffectParam.Add(buffEffectParam);
          }
          // ISSUE: reference to a compiler-generated field
          buffEffectParam.Deserialize(deserializeCAnonStorey21D.data);
        }
      }
      if (json.Cond != null)
      {
        if (this.mCondEffectParam == null)
          this.mCondEffectParam = new List<CondEffectParam>(json.Cond.Length);
        for (int index = 0; index < json.Cond.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey21E deserializeCAnonStorey21E = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey21E();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey21E.data = json.Cond[index];
          // ISSUE: reference to a compiler-generated method
          CondEffectParam condEffectParam = this.mCondEffectParam.Find(new Predicate<CondEffectParam>(deserializeCAnonStorey21E.\u003C\u003Em__193));
          if (condEffectParam == null)
          {
            condEffectParam = new CondEffectParam();
            this.mCondEffectParam.Add(condEffectParam);
          }
          // ISSUE: reference to a compiler-generated field
          condEffectParam.Deserialize(deserializeCAnonStorey21E.data);
        }
      }
      if (json.Ability != null)
      {
        if (this.mAbilityParam == null)
          this.mAbilityParam = new List<AbilityParam>(json.Ability.Length);
        if (this.mAbilityDictionary == null)
          this.mAbilityDictionary = new Dictionary<string, AbilityParam>(json.Ability.Length);
        for (int index = 0; index < json.Ability.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey21F deserializeCAnonStorey21F = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey21F();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey21F.data = json.Ability[index];
          // ISSUE: reference to a compiler-generated method
          AbilityParam abilityParam = this.mAbilityParam.Find(new Predicate<AbilityParam>(deserializeCAnonStorey21F.\u003C\u003Em__194));
          if (abilityParam == null)
          {
            abilityParam = new AbilityParam();
            this.mAbilityParam.Add(abilityParam);
          }
          // ISSUE: reference to a compiler-generated field
          abilityParam.Deserialize(deserializeCAnonStorey21F.data);
          // ISSUE: reference to a compiler-generated field
          if (this.mAbilityDictionary.ContainsKey(deserializeCAnonStorey21F.data.iname))
          {
            // ISSUE: reference to a compiler-generated field
            throw new Exception("重複エラー：Ability[" + deserializeCAnonStorey21F.data.iname + "]");
          }
          // ISSUE: reference to a compiler-generated field
          this.mAbilityDictionary.Add(deserializeCAnonStorey21F.data.iname, abilityParam);
        }
      }
      if (json.Item != null)
      {
        if (this.mItemParam == null)
          this.mItemParam = new List<ItemParam>(json.Item.Length);
        if (this.mItemDictionary == null)
          this.mItemDictionary = new Dictionary<string, ItemParam>(json.Item.Length);
        for (int index = 0; index < json.Item.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey220 deserializeCAnonStorey220 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey220();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey220.data = json.Item[index];
          // ISSUE: reference to a compiler-generated method
          ItemParam itemParam = this.mItemParam.Find(new Predicate<ItemParam>(deserializeCAnonStorey220.\u003C\u003Em__195));
          if (itemParam == null)
          {
            itemParam = new ItemParam();
            this.mItemParam.Add(itemParam);
          }
          // ISSUE: reference to a compiler-generated field
          itemParam.Deserialize(deserializeCAnonStorey220.data);
          itemParam.no = index + 1;
          // ISSUE: reference to a compiler-generated field
          if (this.mItemDictionary.ContainsKey(deserializeCAnonStorey220.data.iname))
          {
            // ISSUE: reference to a compiler-generated field
            throw new Exception("重複エラー：Item[" + deserializeCAnonStorey220.data.iname + "]");
          }
          // ISSUE: reference to a compiler-generated field
          this.mItemDictionary.Add(deserializeCAnonStorey220.data.iname, itemParam);
        }
        this.AddUnitToItem();
      }
      if (json.Artifact != null)
      {
        if (this.mArtifactParam == null)
          this.mArtifactParam = new List<ArtifactParam>(json.Artifact.Length);
        if (this.mArtifactDictionary == null)
          this.mArtifactDictionary = new Dictionary<string, ArtifactParam>(json.Artifact.Length);
        for (int index = 0; index < json.Artifact.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey221 deserializeCAnonStorey221 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey221();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey221.data = json.Artifact[index];
          // ISSUE: reference to a compiler-generated field
          if (deserializeCAnonStorey221.data.iname != null)
          {
            // ISSUE: reference to a compiler-generated method
            ArtifactParam artifactParam = this.mArtifactParam.Find(new Predicate<ArtifactParam>(deserializeCAnonStorey221.\u003C\u003Em__196));
            if (artifactParam == null)
            {
              artifactParam = new ArtifactParam();
              this.mArtifactParam.Add(artifactParam);
            }
            // ISSUE: reference to a compiler-generated field
            artifactParam.Deserialize(deserializeCAnonStorey221.data);
            // ISSUE: reference to a compiler-generated field
            if (this.mArtifactDictionary.ContainsKey(deserializeCAnonStorey221.data.iname))
            {
              // ISSUE: reference to a compiler-generated field
              throw new Exception("重複エラー：Artifact[" + deserializeCAnonStorey221.data.iname + "]");
            }
            // ISSUE: reference to a compiler-generated field
            this.mArtifactDictionary.Add(deserializeCAnonStorey221.data.iname, artifactParam);
          }
        }
      }
      if (json.Weapon != null)
      {
        if (this.mWeaponParam == null)
          this.mWeaponParam = new List<WeaponParam>(json.Weapon.Length);
        for (int index = 0; index < json.Weapon.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey222 deserializeCAnonStorey222 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey222();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey222.data = json.Weapon[index];
          // ISSUE: reference to a compiler-generated method
          WeaponParam weaponParam = this.mWeaponParam.Find(new Predicate<WeaponParam>(deserializeCAnonStorey222.\u003C\u003Em__197));
          if (weaponParam == null)
          {
            weaponParam = new WeaponParam();
            this.mWeaponParam.Add(weaponParam);
          }
          // ISSUE: reference to a compiler-generated field
          weaponParam.Deserialize(deserializeCAnonStorey222.data);
        }
      }
      if (json.Recipe != null)
      {
        if (this.mRecipeParam == null)
          this.mRecipeParam = new List<RecipeParam>(json.Recipe.Length);
        for (int index = 0; index < json.Recipe.Length; ++index)
        {
          JSON_RecipeParam json1 = json.Recipe[index];
          RecipeParam recipeParam = new RecipeParam();
          this.mRecipeParam.Add(recipeParam);
          recipeParam.Deserialize(json1);
        }
      }
      if (json.Job != null)
      {
        if (this.mJobParam == null)
          this.mJobParam = new List<JobParam>(json.Job.Length);
        for (int index = 0; index < json.Job.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey223 deserializeCAnonStorey223 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey223();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey223.data = json.Job[index];
          // ISSUE: reference to a compiler-generated method
          JobParam jobParam = this.mJobParam.Find(new Predicate<JobParam>(deserializeCAnonStorey223.\u003C\u003Em__198));
          if (jobParam == null)
          {
            jobParam = new JobParam();
            this.mJobParam.Add(jobParam);
            // ISSUE: reference to a compiler-generated field
            this.mJobParamDict[deserializeCAnonStorey223.data.iname] = jobParam;
          }
          // ISSUE: reference to a compiler-generated field
          jobParam.Deserialize(deserializeCAnonStorey223.data);
        }
      }
      if (json.JobSet != null)
      {
        if (this.mJobSetParam == null)
          this.mJobSetParam = new List<JobSetParam>(json.JobSet.Length);
        if (this.mJobsetDictionary == null)
          this.mJobsetDictionary = new Dictionary<string, List<JobSetParam>>(json.Unit.Length);
        for (int index = 0; index < json.JobSet.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey224 deserializeCAnonStorey224 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey224();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey224.data = json.JobSet[index];
          // ISSUE: reference to a compiler-generated method
          JobSetParam jobSetParam = this.mJobSetParam.Find(new Predicate<JobSetParam>(deserializeCAnonStorey224.\u003C\u003Em__199));
          if (jobSetParam == null)
          {
            jobSetParam = new JobSetParam();
            this.mJobSetParam.Add(jobSetParam);
          }
          // ISSUE: reference to a compiler-generated field
          jobSetParam.Deserialize(deserializeCAnonStorey224.data);
          if (!string.IsNullOrEmpty(jobSetParam.target_unit))
          {
            List<JobSetParam> jobSetParamList;
            if (this.mJobsetDictionary.ContainsKey(jobSetParam.target_unit))
            {
              jobSetParamList = this.mJobsetDictionary[jobSetParam.target_unit];
            }
            else
            {
              jobSetParamList = new List<JobSetParam>(3);
              this.mJobsetDictionary.Add(jobSetParam.target_unit, jobSetParamList);
            }
            jobSetParamList.Add(jobSetParam);
          }
        }
      }
      if (json.Grow != null)
      {
        if (this.mGrowParam == null)
          this.mGrowParam = new List<GrowParam>(json.Grow.Length);
        for (int index = 0; index < json.Grow.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey225 deserializeCAnonStorey225 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey225();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey225.data = json.Grow[index];
          // ISSUE: reference to a compiler-generated method
          GrowParam growParam = this.mGrowParam.Find(new Predicate<GrowParam>(deserializeCAnonStorey225.\u003C\u003Em__19A));
          if (growParam == null)
          {
            growParam = new GrowParam();
            this.mGrowParam.Add(growParam);
          }
          // ISSUE: reference to a compiler-generated field
          growParam.Deserialize(deserializeCAnonStorey225.data);
        }
      }
      if (json.AI != null)
      {
        if (this.mAIParam == null)
          this.mAIParam = new List<AIParam>(json.AI.Length);
        for (int index = 0; index < json.AI.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey226 deserializeCAnonStorey226 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey226();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey226.data = json.AI[index];
          // ISSUE: reference to a compiler-generated method
          AIParam aiParam = this.mAIParam.Find(new Predicate<AIParam>(deserializeCAnonStorey226.\u003C\u003Em__19B));
          if (aiParam == null)
          {
            aiParam = new AIParam();
            this.mAIParam.Add(aiParam);
          }
          // ISSUE: reference to a compiler-generated field
          aiParam.Deserialize(deserializeCAnonStorey226.data);
        }
      }
      if (json.Geo != null)
      {
        if (this.mGeoParam == null)
          this.mGeoParam = new List<GeoParam>(json.Geo.Length);
        for (int index = 0; index < json.Geo.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey227 deserializeCAnonStorey227 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey227();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey227.data = json.Geo[index];
          // ISSUE: reference to a compiler-generated method
          GeoParam geoParam = this.mGeoParam.Find(new Predicate<GeoParam>(deserializeCAnonStorey227.\u003C\u003Em__19C));
          if (geoParam == null)
          {
            geoParam = new GeoParam();
            this.mGeoParam.Add(geoParam);
          }
          // ISSUE: reference to a compiler-generated field
          geoParam.Deserialize(deserializeCAnonStorey227.data);
        }
      }
      if (json.Rarity != null)
      {
        if (this.mRarityParam == null)
          this.mRarityParam = new List<RarityParam>(json.Rarity.Length);
        for (int index = 0; index < json.Rarity.Length; ++index)
        {
          RarityParam rarityParam;
          if (this.mRarityParam.Count > index)
          {
            rarityParam = this.mRarityParam[index];
          }
          else
          {
            rarityParam = new RarityParam();
            this.mRarityParam.Add(rarityParam);
          }
          rarityParam.Deserialize(json.Rarity[index]);
        }
      }
      if (json.Shop != null)
      {
        if (this.mShopParam == null)
          this.mShopParam = new List<ShopParam>(json.Shop.Length);
        for (int index = 0; index < json.Shop.Length; ++index)
        {
          ShopParam shopParam;
          if (this.mShopParam.Count > index)
          {
            shopParam = this.mShopParam[index];
          }
          else
          {
            shopParam = new ShopParam();
            this.mShopParam.Add(shopParam);
          }
          shopParam.Deserialize(json.Shop[index]);
        }
      }
      if (json.Player != null)
      {
        this.mPlayerParamTbl = new PlayerParam[json.Player.Length];
        for (int index = 0; index < json.Player.Length; ++index)
        {
          JSON_PlayerParam json1 = json.Player[index];
          this.mPlayerParamTbl[index] = new PlayerParam();
          this.mPlayerParamTbl[index].Deserialize(json1);
        }
      }
      if (json.PlayerLvTbl != null)
      {
        this.mPlayerExpTbl = new OInt[json.PlayerLvTbl.Length];
        for (int index = 0; index < json.PlayerLvTbl.Length; ++index)
          this.mPlayerExpTbl[index] = (OInt) json.PlayerLvTbl[index];
      }
      if (json.UnitLvTbl != null)
      {
        this.mUnitExpTbl = new OInt[json.UnitLvTbl.Length];
        for (int index = 0; index < json.UnitLvTbl.Length; ++index)
          this.mUnitExpTbl[index] = (OInt) json.UnitLvTbl[index];
      }
      if (json.ArtifactLvTbl != null)
      {
        this.mArtifactExpTbl = new OInt[json.ArtifactLvTbl.Length];
        for (int index = 0; index < json.ArtifactLvTbl.Length; ++index)
          this.mArtifactExpTbl[index] = (OInt) json.ArtifactLvTbl[index];
      }
      if (json.AbilityRank != null)
      {
        this.mAbilityExpTbl = new OInt[json.AbilityRank.Length];
        for (int index = 0; index < json.AbilityRank.Length; ++index)
          this.mAbilityExpTbl[index] = (OInt) json.AbilityRank[index];
      }
      if (json.AwakePieceTbl != null)
      {
        this.mAwakePieceTbl = new OInt[json.AwakePieceTbl.Length];
        for (int index = 0; index < json.AwakePieceTbl.Length; ++index)
          this.mAwakePieceTbl[index] = (OInt) json.AwakePieceTbl[index];
      }
      this.mLocalNotificationParam = new LocalNotificationParam();
      if (json.LocalNotification != null)
      {
        this.mLocalNotificationParam.msg_stamina = json.LocalNotification[0].msg_stamina;
        this.mLocalNotificationParam.iOSAct_stamina = json.LocalNotification[0].iOSAct_stamina;
        this.mLocalNotificationParam.limitSec_stamina = json.LocalNotification[0].limitSec_stamina;
      }
      Dictionary<int, TrophyCategoryParam> dictionary = new Dictionary<int, TrophyCategoryParam>();
      if (json.TrophyCategory != null)
      {
        List<TrophyCategoryParam> trophyCategoryParamList = new List<TrophyCategoryParam>(json.TrophyCategory.Length);
        for (int index = 0; index < json.TrophyCategory.Length; ++index)
        {
          TrophyCategoryParam trophyCategoryParam = new TrophyCategoryParam();
          if (trophyCategoryParam.Deserialize(json.TrophyCategory[index]))
          {
            trophyCategoryParamList.Add(trophyCategoryParam);
            if (!dictionary.ContainsKey(trophyCategoryParam.hash_code))
              dictionary.Add(trophyCategoryParam.hash_code, trophyCategoryParam);
          }
        }
        this.mTrophyCategory = trophyCategoryParamList.ToArray();
      }
      if (json.Trophy != null)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Trophy.Length);
        for (int index = 0; index < json.Trophy.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(json.Trophy[index]))
          {
            if (dictionary.ContainsKey(trophyParam.category_hash_code))
              trophyParam.CategoryParam = dictionary[trophyParam.category_hash_code];
            else
              DebugUtility.LogError(trophyParam.iname + " => 親カテゴリが未設定 or 入力ミス");
            if (trophyParam.IsPlanningToUse())
              trophyParamList.Add(trophyParam);
          }
        }
        this.mTrophy = trophyParamList.ToArray();
        this.mTrophyInameDict = new Dictionary<string, TrophyParam>();
        foreach (TrophyParam trophyParam in this.mTrophy)
          this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
      }
      if (json.ChallengeCategory != null)
      {
        List<ChallengeCategoryParam> challengeCategoryParamList = new List<ChallengeCategoryParam>(json.ChallengeCategory.Length);
        for (int index = 0; index < json.ChallengeCategory.Length; ++index)
        {
          ChallengeCategoryParam challengeCategoryParam = new ChallengeCategoryParam();
          if (challengeCategoryParam.Deserialize(json.ChallengeCategory[index]))
            challengeCategoryParamList.Add(challengeCategoryParam);
        }
        this.mChallengeCategory = challengeCategoryParamList.ToArray();
      }
      if (json.Challenge != null)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Challenge.Length);
        for (int index = 0; index < json.Challenge.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(json.Challenge[index]))
          {
            trophyParam.Challenge = 1;
            trophyParamList.Add(trophyParam);
          }
        }
        this.mChallenge = trophyParamList.ToArray();
        int length = this.mTrophy.Length;
        Array.Resize<TrophyParam>(ref this.mTrophy, length + this.mChallenge.Length);
        Array.Copy((Array) this.mChallenge, 0, (Array) this.mTrophy, length, this.mChallenge.Length);
        foreach (TrophyParam trophyParam in this.mChallenge)
          this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
      }
      this.CreateTrophyDict();
      if (json.Unlock != null)
      {
        List<UnlockParam> unlockParamList = new List<UnlockParam>(json.Unlock.Length);
        for (int index = 0; index < json.Unlock.Length; ++index)
        {
          UnlockParam unlockParam = new UnlockParam();
          if (unlockParam.Deserialize(json.Unlock[index]))
            unlockParamList.Add(unlockParam);
        }
        this.mUnlock = unlockParamList.ToArray();
      }
      if (json.Vip != null)
      {
        List<VipParam> vipParamList = new List<VipParam>(json.Vip.Length);
        for (int index = 0; index < json.Vip.Length; ++index)
        {
          VipParam vipParam = new VipParam();
          if (vipParam.Deserialize(json.Vip[index]))
            vipParamList.Add(vipParam);
        }
        this.mVip = vipParamList.ToArray();
      }
      if (json.Mov != null)
      {
        this.mStreamingMovies = new JSON_StreamingMovie[json.Mov.Length];
        for (int index = 0; index < json.Mov.Length; ++index)
        {
          this.mStreamingMovies[index] = new JSON_StreamingMovie();
          this.mStreamingMovies[index].alias = json.Mov[index].alias;
          this.mStreamingMovies[index].path = json.Mov[index].path;
        }
      }
      if (json.Banner != null)
      {
        List<BannerParam> bannerParamList = new List<BannerParam>(json.Banner.Length);
        for (int index = 0; index < json.Banner.Length; ++index)
        {
          BannerParam bannerParam = new BannerParam();
          if (bannerParam.Deserialize(json.Banner[index]))
            bannerParamList.Add(bannerParam);
        }
        this.mBanner = bannerParamList.ToArray();
      }
      for (int index = 0; index < this.mJobParam.Count; ++index)
        this.mJobParam[index].UpdateJobRankTransfarStatus(this);
      if (json.QuestClearUnlockUnitData != null)
      {
        List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>(json.QuestClearUnlockUnitData.Length);
        for (int index = 0; index < json.QuestClearUnlockUnitData.Length; ++index)
        {
          QuestClearUnlockUnitDataParam unlockUnitDataParam = new QuestClearUnlockUnitDataParam();
          unlockUnitDataParam.Deserialize(json.QuestClearUnlockUnitData[index]);
          unlockUnitDataParamList.Add(unlockUnitDataParam);
        }
        this.mUnlockUnitDataParam = unlockUnitDataParamList;
      }
      if (json.Award != null)
      {
        if (this.mAwardParam == null)
          this.mAwardParam = new List<AwardParam>(json.Award.Length);
        if (this.mAwardDictionary == null)
          this.mAwardDictionary = new Dictionary<string, AwardParam>(json.Award.Length);
        for (int index = 0; index < json.Award.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          MasterParam.\u003CDeserialize\u003Ec__AnonStorey228 deserializeCAnonStorey228 = new MasterParam.\u003CDeserialize\u003Ec__AnonStorey228();
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStorey228.data = json.Award[index];
          // ISSUE: reference to a compiler-generated field
          if (deserializeCAnonStorey228.data.iname != null)
          {
            // ISSUE: reference to a compiler-generated method
            AwardParam awardParam = this.mAwardParam.Find(new Predicate<AwardParam>(deserializeCAnonStorey228.\u003C\u003Em__19D));
            if (awardParam == null)
            {
              awardParam = new AwardParam();
              this.mAwardParam.Add(awardParam);
            }
            // ISSUE: reference to a compiler-generated field
            awardParam.Deserialize(deserializeCAnonStorey228.data);
            if (this.mAwardDictionary.ContainsKey(awardParam.iname))
              throw new Exception("Overlap : Award[" + awardParam.iname + "]");
            this.mAwardDictionary.Add(awardParam.iname, awardParam);
          }
        }
      }
      if (json.LoginInfo != null)
      {
        List<LoginInfoParam> loginInfoParamList = new List<LoginInfoParam>(json.LoginInfo.Length);
        for (int index = 0; index < json.LoginInfo.Length; ++index)
        {
          LoginInfoParam loginInfoParam = new LoginInfoParam();
          if (loginInfoParam.Deserialize(json.LoginInfo[index]))
            loginInfoParamList.Add(loginInfoParam);
        }
        this.mLoginInfoParam = loginInfoParamList.ToArray();
      }
      if (json.CollaboSkill != null)
      {
        List<CollaboSkillParam> collaboSkillParamList = new List<CollaboSkillParam>(json.CollaboSkill.Length);
        for (int index = 0; index < json.CollaboSkill.Length; ++index)
        {
          CollaboSkillParam collaboSkillParam = new CollaboSkillParam();
          collaboSkillParam.Deserialize(json.CollaboSkill[index]);
          collaboSkillParamList.Add(collaboSkillParam);
        }
        this.mCollaboSkillParam = collaboSkillParamList;
        CollaboSkillParam.UpdateCollaboSkill(this.mCollaboSkillParam);
      }
      if (json.Trick != null)
      {
        List<TrickParam> trickParamList = new List<TrickParam>(json.Trick.Length);
        for (int index = 0; index < json.Trick.Length; ++index)
        {
          TrickParam trickParam = new TrickParam();
          trickParam.Deserialize(json.Trick[index]);
          trickParamList.Add(trickParam);
        }
        this.mTrickParam = trickParamList;
      }
      if (json.BreakObj != null)
      {
        List<BreakObjParam> breakObjParamList = new List<BreakObjParam>(json.BreakObj.Length);
        for (int index = 0; index < json.BreakObj.Length; ++index)
        {
          BreakObjParam breakObjParam = new BreakObjParam();
          breakObjParam.Deserialize(json.BreakObj[index]);
          breakObjParamList.Add(breakObjParam);
        }
        this.mBreakObjParam = breakObjParamList;
      }
      if (json.VersusMatchKey != null)
      {
        this.mVersusMatching = new List<VersusMatchingParam>(json.VersusMatchKey.Length);
        for (int index = 0; index < json.VersusMatchKey.Length; ++index)
        {
          VersusMatchingParam versusMatchingParam = new VersusMatchingParam();
          versusMatchingParam.Deserialize(json.VersusMatchKey[index]);
          this.mVersusMatching.Add(versusMatchingParam);
        }
      }
      if (json.VersusMatchCond != null)
      {
        this.mVersusMatchCond = new List<VersusMatchCondParam>(json.VersusMatchCond.Length);
        for (int index = 0; index < json.VersusMatchCond.Length; ++index)
        {
          VersusMatchCondParam versusMatchCondParam = new VersusMatchCondParam();
          versusMatchCondParam.Deserialize(json.VersusMatchCond[index]);
          this.mVersusMatchCond.Add(versusMatchCondParam);
        }
      }
      if (json.TowerScore != null)
      {
        this.mTowerScore = new List<TowerScoreParam>(json.TowerScore.Length);
        for (int index = 0; index < json.TowerScore.Length; ++index)
        {
          TowerScoreParam towerScoreParam = new TowerScoreParam();
          if (towerScoreParam != null && towerScoreParam.Deserialize(json.TowerScore[index]))
            this.mTowerScore.Add(towerScoreParam);
        }
      }
      if (json.TowerRank != null)
      {
        this.mTowerRankTbl = new OInt[json.TowerRank.Length];
        for (int index = 0; index < json.TowerRank.Length; ++index)
          this.mTowerRankTbl[index] = (OInt) json.TowerRank[index];
      }
      if (json.MultilimitUnitLv != null)
      {
        this.mMultiLimitUnitLv = new OInt[json.MultilimitUnitLv.Length];
        for (int index = 0; index < json.MultilimitUnitLv.Length; ++index)
          this.mMultiLimitUnitLv[index] = (OInt) json.MultilimitUnitLv[index];
      }
      if (json.FriendPresentItem != null)
      {
        this.mFriendPresentItemParam = new Dictionary<string, FriendPresentItemParam>();
        for (int index = 0; index < json.FriendPresentItem.Length; ++index)
        {
          FriendPresentItemParam presentItemParam = new FriendPresentItemParam();
          presentItemParam.Deserialize(json.FriendPresentItem[index]);
          this.mFriendPresentItemParam.Add(presentItemParam.iname, presentItemParam);
        }
      }
      if (json.Weather != null)
      {
        List<WeatherParam> weatherParamList = new List<WeatherParam>(json.Weather.Length);
        for (int index = 0; index < json.Weather.Length; ++index)
        {
          WeatherParam weatherParam = new WeatherParam();
          weatherParam.Deserialize(json.Weather[index]);
          weatherParamList.Add(weatherParam);
        }
        this.mWeatherParam = weatherParamList;
      }
      this.Loaded = true;
      return true;
    }

    public void CacheReferences()
    {
      for (int index = 0; index < this.mUnitParam.Count; ++index)
      {
        if (this.mUnitParam[index] != null)
          this.mUnitParam[index].CacheReferences(this);
      }
    }

    public void DumpLoadedLog()
    {
    }

    public UnitParam[] GetAllUnits()
    {
      if (this.mUnitParam != null)
        return this.mUnitParam.ToArray();
      return new UnitParam[0];
    }

    public bool ContainsUnitID(string key)
    {
      return this.mUnitDictionary.ContainsKey(key);
    }

    public UnitParam GetUnitParam(string key)
    {
      try
      {
        return this.mUnitDictionary[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<UnitParam>(key);
      }
    }

    public SkillParam GetSkillParam(string key)
    {
      try
      {
        return this.mSkillDictionary[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<SkillParam>(key);
      }
    }

    public BuffEffectParam GetBuffEffectParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (BuffEffectParam) null;
      BuffEffectParam buffEffectParam = this.mBuffEffectParam.Find((Predicate<BuffEffectParam>) (p => p.iname == key));
      if (buffEffectParam == null)
        DebugUtility.LogError("Unknown BuffEffectParam \"" + key + "\"");
      return buffEffectParam;
    }

    public CondEffectParam GetCondEffectParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (CondEffectParam) null;
      CondEffectParam condEffectParam = this.mCondEffectParam.Find((Predicate<CondEffectParam>) (p => p.iname == key));
      if (condEffectParam == null)
        DebugUtility.LogError("Unknown CondEffectParam \"" + key + "\"");
      return condEffectParam;
    }

    public AbilityParam GetAbilityParam(string key)
    {
      try
      {
        return this.mAbilityDictionary[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<AbilityParam>(key);
      }
    }

    public ItemParam GetItemParam(string key)
    {
      try
      {
        return this.mItemDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown ItemParam \"" + key + "\"");
        return (ItemParam) null;
      }
    }

    public ArtifactParam GetArtifactParam(string key)
    {
      try
      {
        return this.mArtifactDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown ArtifactParam \"" + key + "\"");
        return (ArtifactParam) null;
      }
    }

    public WeaponParam GetWeaponParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (WeaponParam) null;
      WeaponParam weaponParam = this.mWeaponParam.Find((Predicate<WeaponParam>) (p => p.iname == key));
      if (weaponParam == null)
        DebugUtility.LogError("Unknown WeaponParam \"" + key + "\"");
      return weaponParam;
    }

    public RecipeParam GetRecipeParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (RecipeParam) null;
      RecipeParam recipeParam = this.mRecipeParam.Find((Predicate<RecipeParam>) (p => p.iname == key));
      if (recipeParam == null)
        DebugUtility.LogError("Unknown RecipeParam \"" + key + "\"");
      return recipeParam;
    }

    public JobParam GetJobParam(string key)
    {
      try
      {
        return this.mJobParamDict[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<JobParam>(key);
      }
    }

    public JobParam[] GetAllJobs()
    {
      if (this.mJobParam != null)
        return this.mJobParam.ToArray();
      return new JobParam[0];
    }

    public AbilityParam[] GetAllAbilities()
    {
      if (this.mAbilityParam != null)
        return this.mAbilityParam.ToArray();
      return new AbilityParam[0];
    }

    public SkillParam[] GetAllSkills()
    {
      if (this.mSkillParam != null)
        return this.mSkillParam.ToArray();
      return new SkillParam[0];
    }

    public QuestClearUnlockUnitDataParam[] GetAllUnlockUnitDatas()
    {
      if (this.mUnlockUnitDataParam != null)
        return this.mUnlockUnitDataParam.ToArray();
      return new QuestClearUnlockUnitDataParam[0];
    }

    public QuestClearUnlockUnitDataParam GetUnlockUnitData(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (QuestClearUnlockUnitDataParam) null;
      QuestClearUnlockUnitDataParam unlockUnitDataParam = this.mUnlockUnitDataParam.Find((Predicate<QuestClearUnlockUnitDataParam>) (p => p.iname == key));
      if (unlockUnitDataParam == null)
        throw new KeyNotFoundException<QuestClearUnlockUnitDataParam>(key);
      return unlockUnitDataParam;
    }

    public CollaboSkillParam GetCollaboSkillData(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return (CollaboSkillParam) null;
      if (this.mCollaboSkillParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetCollaboSkillData no data!</color>"));
        return (CollaboSkillParam) null;
      }
      CollaboSkillParam collaboSkillParam = this.mCollaboSkillParam.Find((Predicate<CollaboSkillParam>) (d => d.UnitIname == unit_iname));
      if (collaboSkillParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetCollaboSkillData data not found! unit_iname={0}</color>", (object) unit_iname));
      return collaboSkillParam;
    }

    public TrickParam GetTrickParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (TrickParam) null;
      if (this.mTrickParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTrickParam no data!</color>"));
        return (TrickParam) null;
      }
      TrickParam trickParam = this.mTrickParam.Find((Predicate<TrickParam>) (d => d.Iname == iname));
      if (trickParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTrickParam data not found! iname={0}</color>", (object) iname));
      return trickParam;
    }

    public BreakObjParam GetBreakObjParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (BreakObjParam) null;
      if (this.mBreakObjParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetBreakObjParam no data!</color>"));
        return (BreakObjParam) null;
      }
      BreakObjParam breakObjParam = this.mBreakObjParam.Find((Predicate<BreakObjParam>) (d => d.Iname == iname));
      if (breakObjParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetBreakObjParam data not found! iname={0}</color>", (object) iname));
      return breakObjParam;
    }

    public WeatherParam GetWeatherParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (WeatherParam) null;
      if (this.mWeatherParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetWeatherParam no data!</color>"));
        return (WeatherParam) null;
      }
      WeatherParam weatherParam = this.mWeatherParam.Find((Predicate<WeatherParam>) (d => d.Iname == iname));
      if (weatherParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetWeatherParam data not found! iname={0}</color>", (object) iname));
      return weatherParam;
    }

    public RecipeParam[] GetAllRecipes()
    {
      if (this.mRecipeParam != null)
        return this.mRecipeParam.ToArray();
      return new RecipeParam[0];
    }

    public JobSetParam GetJobSetParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (JobSetParam) null;
      JobSetParam jobSetParam = this.mJobSetParam.Find((Predicate<JobSetParam>) (p => p.iname == key));
      if (jobSetParam == null)
        throw new KeyNotFoundException<JobSetParam>(key);
      return jobSetParam;
    }

    public JobSetParam[] GetClassChangeJobSetParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (JobSetParam[]) null;
      try
      {
        return this.mJobsetDictionary[key].ToArray();
      }
      catch
      {
        return (JobSetParam[]) null;
      }
    }

    public GrowParam GetGrowParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (GrowParam) null;
      GrowParam growParam = this.mGrowParam.Find((Predicate<GrowParam>) (p => p.type == key));
      if (growParam == null)
        DebugUtility.LogError("Unknown GrowParam \"" + key + "\"");
      return growParam;
    }

    public AIParam GetAIParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (AIParam) null;
      AIParam aiParam = this.mAIParam.Find((Predicate<AIParam>) (p => p.iname == key));
      if (aiParam == null)
        DebugUtility.LogError("Failed AIParam iname \"" + key + "\" not found.");
      return aiParam;
    }

    public GeoParam GetGeoParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (GeoParam) null;
      GeoParam geoParam = this.mGeoParam.Find((Predicate<GeoParam>) (p => p.iname == key));
      if (geoParam == null)
        DebugUtility.LogError("Failed GeoParam iname \"" + key + "\" not found.");
      return geoParam;
    }

    public RarityParam GetRarityParam(int rarity)
    {
      if (this.mRarityParam != null && (rarity >= 0 || rarity < this.mRarityParam.Count))
        return this.mRarityParam[rarity];
      DebugUtility.LogError("mRarityParam Stack Overflow.");
      return (RarityParam) null;
    }

    public ShopParam GetShopParam(EShopType type)
    {
      int index1 = (int) type;
      if (type == EShopType.Event)
      {
        index1 = -1;
        for (int index2 = 0; index2 < this.mShopParam.Count; ++index2)
        {
          string[] strArray = GlobalVars.EventShopItem.shops.gname.Split('-');
          if (this.mShopParam[index2].iname.Equals(strArray[0]))
          {
            index1 = index2;
            break;
          }
        }
        if (index1 < 0)
        {
          DebugUtility.LogError("mShopParam Data Error. Not found: " + (object) GlobalVars.EventShopItem.shops.gname.Split('-'));
          return (ShopParam) null;
        }
      }
      if (type == EShopType.Limited)
      {
        index1 = -1;
        for (int index2 = 0; index2 < this.mShopParam.Count; ++index2)
        {
          string[] strArray = GlobalVars.LimitedShopItem.shops.gname.Split('-');
          if (this.mShopParam[index2].iname.Equals(strArray[0]))
          {
            index1 = index2;
            break;
          }
        }
        if (index1 < 0)
        {
          DebugUtility.LogError("mShopParam Data Error. Not found: " + (object) GlobalVars.LimitedShopItem.shops.gname.Split('-'));
          return (ShopParam) null;
        }
      }
      if (this.mShopParam != null && index1 >= 0 && index1 < this.mShopParam.Count)
        return this.mShopParam[index1];
      DebugUtility.LogError("mShopParam Stack Overflow.");
      return (ShopParam) null;
    }

    public int GetShopType(string iname)
    {
      int index = this.mShopParam.FindIndex((Predicate<ShopParam>) (p => p.iname == iname));
      if (index < 0)
        DebugUtility.LogError("Failed GetShopParam iname \"" + iname + "\" not found.");
      return index;
    }

    public PlayerParam GetPlayerParam(int lv)
    {
      if (lv > 0 && lv <= this.GetPlayerLevelCap())
        return this.mPlayerParamTbl[lv - 1];
      return (PlayerParam) null;
    }

    public int GetAbilityNextGold(int rank)
    {
      DebugUtility.Assert(rank > 0 && rank <= this.mAbilityExpTbl.Length, "指定ランク" + (object) rank + "がアビリティのランク範囲に存在しない。");
      return (int) this.mAbilityExpTbl[rank];
    }

    public int GetAwakeNeedPieces(int awakeLv)
    {
      DebugUtility.Assert(awakeLv >= 0 && awakeLv < this.mAwakePieceTbl.Length, "覚醒回数" + (object) awakeLv + "が覚醒可能な範囲に存在しない。");
      return (int) this.mAwakePieceTbl[awakeLv];
    }

    public int GetUnitNextExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mUnitExpTbl.Length, "指定レベル" + (object) lv + "がユニットのレベル範囲に存在しない。");
      return (int) this.mUnitExpTbl[lv - 1];
    }

    public int GetUnitLevelExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mUnitExpTbl.Length, "指定レベル" + (object) lv + "がユニットのレベル範囲に存在しない。");
      int num = 0;
      for (int index = 0; index < lv; ++index)
        num += (int) this.mUnitExpTbl[index];
      return num;
    }

    public int CalcUnitLevel(int totalExp, int levelCap)
    {
      int val2 = levelCap;
      int num = 0;
      int val1 = 0;
      for (int index = 0; index < val2; ++index)
      {
        num += this.GetUnitNextExp(index + 1);
        if (num <= totalExp)
          ++val1;
        else
          break;
      }
      return Math.Min(Math.Max(val1, 1), val2);
    }

    public int GetUnitMaxLevel()
    {
      return this.mUnitExpTbl.Length;
    }

    public int GetPlayerNextExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mPlayerExpTbl.Length, "指定レベル" + (object) lv + "がプレイヤーのレベル範囲に存在しない。");
      return (int) this.mPlayerExpTbl[lv - 1];
    }

    public int GetPlayerLevelExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mPlayerExpTbl.Length, "指定レベル" + (object) lv + "がプレイヤーのレベル範囲に存在しない。");
      int num = 0;
      for (int index = 0; index < lv; ++index)
        num += (int) this.mPlayerExpTbl[index];
      return num;
    }

    public int GetPlayerLevelCap()
    {
      Debug.Log((object) this.mPlayerExpTbl);
      return this.mPlayerExpTbl.Length;
    }

    public int GetVipArenaResetCount(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].ResetArenaNum;
    }

    public int GetVipRankNextPoint(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].NextRankNeedPoint;
    }

    public int GetVipRankTotalNeedPoint(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      int num = 0;
      for (int index = 0; index < rank; ++index)
        num += this.mVip[index].NextRankNeedPoint;
      return num;
    }

    public int GetVipBuyStaminaLimit(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].BuyStaminaNum;
    }

    public int GetVipBuyGoldLimit(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].BuyCoinNum;
    }

    public int GetVipRankCap()
    {
      if (this.mVip == null)
        return 0;
      return Math.Max(this.mVip.Length - 1, 0);
    }

    public TrophyCategoryParam[] TrophyCategories
    {
      get
      {
        return this.mTrophyCategory;
      }
    }

    public ChallengeCategoryParam[] ChallengeCategories
    {
      get
      {
        return this.mChallengeCategory;
      }
    }

    public TrophyParam[] Trophies
    {
      get
      {
        return this.mTrophy;
      }
    }

    public TrophyObjective[] GetTrophiesOfType(TrophyConditionTypes type)
    {
      return this.mTrophyDict[(int) type];
    }

    public TrophyParam GetTrophy(string iname)
    {
      if (this.mTrophy == null)
        return (TrophyParam) null;
      TrophyParam trophyParam;
      if (this.mTrophyInameDict.TryGetValue(iname, out trophyParam))
        return trophyParam;
      return (TrophyParam) null;
    }

    public UnlockParam[] Unlocks
    {
      get
      {
        return this.mUnlock;
      }
    }

    public UnlockParam GetUnlockParam(string iname)
    {
      for (int index = this.mUnlock.Length - 1; index >= 0; --index)
      {
        if (this.mUnlock[index].iname == iname)
          return this.mUnlock[index];
      }
      return (UnlockParam) null;
    }

    public UnitParam GetUnitParamForPiece(string key, bool doCheck = true)
    {
      if (string.IsNullOrEmpty(key))
        return (UnitParam) null;
      if (key == (string) this.FixParam.CommonPieceAll || key == (string) this.FixParam.CommonPieceDark || (key == (string) this.FixParam.CommonPieceFire || key == (string) this.FixParam.CommonPieceShine) || (key == (string) this.FixParam.CommonPieceThunder || key == (string) this.FixParam.CommonPieceWater || key == (string) this.FixParam.CommonPieceWind))
        return (UnitParam) null;
      UnitParam unitParam = this.mUnitParam.Find((Predicate<UnitParam>) (p => (string) p.piece == key));
      if (doCheck && unitParam == null)
        DebugUtility.LogError("Failed UnitParam iname \"" + key + "\" not found.");
      return unitParam;
    }

    public OInt[] GetArtifactExpTable()
    {
      return this.mArtifactExpTbl;
    }

    public string TranslateMoviePath(string path)
    {
      if (this.mStreamingMovies == null)
        return path;
      for (int index = 0; index < this.mStreamingMovies.Length; ++index)
      {
        if (this.mStreamingMovies[index].alias == path)
          return this.mStreamingMovies[index].path;
      }
      return path;
    }

    public ArtifactParam GetSkinParamFromItemId(string itemId)
    {
      return Array.Find<ArtifactParam>(this.mArtifactParam.ToArray(), (Predicate<ArtifactParam>) (s => s.kakera == itemId));
    }

    public bool IsSkinItem(string itemId)
    {
      return this.GetSkinParamFromItemId(itemId) != null;
    }

    public BannerParam[] Banners
    {
      get
      {
        return this.mBanner;
      }
    }

    public AwardParam GetAwardParam(string key)
    {
      try
      {
        return this.mAwardDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown AwardParam \"" + key + "\"");
        return (AwardParam) null;
      }
    }

    public AwardParam[] GetAllAwards()
    {
      if (this.mAwardParam != null)
        return this.mAwardParam.ToArray();
      return new AwardParam[0];
    }

    public LoginInfoParam[] GetAllLoginInfos()
    {
      if (this.mLoginInfoParam != null)
        return this.mLoginInfoParam;
      return new LoginInfoParam[0];
    }

    public LoginInfoParam[] GetActiveLoginInfos()
    {
      if (this.mLoginInfoParam == null)
        return (LoginInfoParam[]) null;
      List<LoginInfoParam> loginInfoParamList = new List<LoginInfoParam>();
      int player_level = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
      bool is_beginner = MonoSingleton<GameManager>.Instance.Player.IsBeginner();
      for (int index = 0; index < this.mLoginInfoParam.Length; ++index)
      {
        if (this.mLoginInfoParam[index].IsDisplayable(TimeManager.ServerTime, player_level, is_beginner))
          loginInfoParamList.Add(this.mLoginInfoParam[index]);
      }
      return loginInfoParamList.ToArray();
    }

    public VersusMatchingParam[] GetVersusMatchingParam()
    {
      return this.mVersusMatching.ToArray();
    }

    public VersusMatchCondParam[] GetVersusMatchingCondition()
    {
      return this.mVersusMatchCond.ToArray();
    }

    public TowerScoreParam[] TowerScore
    {
      get
      {
        return this.mTowerScore.ToArray();
      }
    }

    public OInt[] TowerRank
    {
      get
      {
        return this.mTowerRankTbl;
      }
    }

    public OInt[] GetMultiPlayLimitUnitLv()
    {
      return this.mMultiLimitUnitLv;
    }

    public bool Deserialize2(JSON_MasterParam json)
    {
      if (this.Loaded)
        return true;
      DebugUtility.Verify((object) json, typeof (JSON_MasterParam));
      this.mLocalNotificationParam = (LocalNotificationParam) null;
      this.mFixParam.Deserialize(json.Fix[0]);
      if (json.Unit != null)
      {
        if (this.mUnitParam == null)
          this.mUnitParam = new List<UnitParam>(json.Unit.Length);
        if (this.mUnitDictionary == null)
          this.mUnitDictionary = new Dictionary<string, UnitParam>(json.Unit.Length);
        for (int index = 0; index < json.Unit.Length; ++index)
        {
          JSON_UnitParam json1 = json.Unit[index];
          UnitParam unitParam = new UnitParam();
          this.mUnitParam.Add(unitParam);
          unitParam.Deserialize(json1);
          this.mUnitDictionary.Add(json1.iname, unitParam);
        }
      }
      if (json.Skill != null)
      {
        if (this.mSkillParam == null)
          this.mSkillParam = new List<SkillParam>(json.Skill.Length);
        if (this.mSkillDictionary == null)
          this.mSkillDictionary = new Dictionary<string, SkillParam>(json.Skill.Length);
        for (int index = 0; index < json.Skill.Length; ++index)
        {
          JSON_SkillParam json1 = json.Skill[index];
          SkillParam skillParam = new SkillParam();
          this.mSkillParam.Add(skillParam);
          skillParam.Deserialize(json1);
          this.mSkillDictionary.Add(json1.iname, skillParam);
        }
        SkillParam.UpdateReplaceSkill(this.mSkillParam);
      }
      if (json.Buff != null)
      {
        if (this.mBuffEffectParam == null)
          this.mBuffEffectParam = new List<BuffEffectParam>(json.Buff.Length);
        for (int index = 0; index < json.Buff.Length; ++index)
        {
          JSON_BuffEffectParam json1 = json.Buff[index];
          BuffEffectParam buffEffectParam = new BuffEffectParam();
          this.mBuffEffectParam.Add(buffEffectParam);
          buffEffectParam.Deserialize(json1);
        }
      }
      if (json.Cond != null)
      {
        if (this.mCondEffectParam == null)
          this.mCondEffectParam = new List<CondEffectParam>(json.Cond.Length);
        for (int index = 0; index < json.Cond.Length; ++index)
        {
          JSON_CondEffectParam json1 = json.Cond[index];
          CondEffectParam condEffectParam = new CondEffectParam();
          this.mCondEffectParam.Add(condEffectParam);
          condEffectParam.Deserialize(json1);
        }
      }
      if (json.Ability != null)
      {
        if (this.mAbilityParam == null)
          this.mAbilityParam = new List<AbilityParam>(json.Ability.Length);
        if (this.mAbilityDictionary == null)
          this.mAbilityDictionary = new Dictionary<string, AbilityParam>(json.Ability.Length);
        for (int index = 0; index < json.Ability.Length; ++index)
        {
          JSON_AbilityParam json1 = json.Ability[index];
          AbilityParam abilityParam = new AbilityParam();
          this.mAbilityParam.Add(abilityParam);
          abilityParam.Deserialize(json1);
          this.mAbilityDictionary.Add(json1.iname, abilityParam);
        }
      }
      if (json.Item != null)
      {
        if (this.mItemParam == null)
          this.mItemParam = new List<ItemParam>(json.Item.Length);
        if (this.mItemDictionary == null)
          this.mItemDictionary = new Dictionary<string, ItemParam>(json.Item.Length);
        for (int index = 0; index < json.Item.Length; ++index)
        {
          JSON_ItemParam json1 = json.Item[index];
          ItemParam itemParam = new ItemParam();
          this.mItemParam.Add(itemParam);
          itemParam.Deserialize(json1);
          itemParam.no = index + 1;
          this.mItemDictionary.Add(json1.iname, itemParam);
        }
        this.AddUnitToItem();
      }
      if (json.Artifact != null)
      {
        if (this.mArtifactParam == null)
          this.mArtifactParam = new List<ArtifactParam>(json.Artifact.Length);
        if (this.mArtifactDictionary == null)
          this.mArtifactDictionary = new Dictionary<string, ArtifactParam>(json.Artifact.Length);
        for (int index = 0; index < json.Artifact.Length; ++index)
        {
          JSON_ArtifactParam json1 = json.Artifact[index];
          if (json1.iname != null)
          {
            ArtifactParam artifactParam = new ArtifactParam();
            this.mArtifactParam.Add(artifactParam);
            artifactParam.Deserialize(json1);
            this.mArtifactDictionary.Add(json1.iname, artifactParam);
          }
        }
      }
      if (json.Weapon != null)
      {
        if (this.mWeaponParam == null)
          this.mWeaponParam = new List<WeaponParam>(json.Weapon.Length);
        for (int index = 0; index < json.Weapon.Length; ++index)
        {
          JSON_WeaponParam json1 = json.Weapon[index];
          WeaponParam weaponParam = new WeaponParam();
          this.mWeaponParam.Add(weaponParam);
          weaponParam.Deserialize(json1);
        }
      }
      if (json.Recipe != null)
      {
        if (this.mRecipeParam == null)
          this.mRecipeParam = new List<RecipeParam>(json.Recipe.Length);
        for (int index = 0; index < json.Recipe.Length; ++index)
        {
          JSON_RecipeParam json1 = json.Recipe[index];
          RecipeParam recipeParam = new RecipeParam();
          this.mRecipeParam.Add(recipeParam);
          recipeParam.Deserialize(json1);
        }
      }
      if (json.Job != null)
      {
        if (this.mJobParam == null)
          this.mJobParam = new List<JobParam>(json.Job.Length);
        for (int index = 0; index < json.Job.Length; ++index)
        {
          JSON_JobParam json1 = json.Job[index];
          JobParam jobParam = new JobParam();
          this.mJobParam.Add(jobParam);
          this.mJobParamDict[json1.iname] = jobParam;
          jobParam.Deserialize(json1);
        }
      }
      if (json.JobSet != null)
      {
        if (this.mJobSetParam == null)
          this.mJobSetParam = new List<JobSetParam>(json.JobSet.Length);
        if (this.mJobsetDictionary == null)
          this.mJobsetDictionary = new Dictionary<string, List<JobSetParam>>(json.Unit.Length);
        for (int index = 0; index < json.JobSet.Length; ++index)
        {
          JSON_JobSetParam job = json.JobSet[index];
          JobSetParam jobSetParam = new JobSetParam();
          this.mJobSetParam.Add(jobSetParam);
          jobSetParam.Deserialize(job);
          if (!string.IsNullOrEmpty(jobSetParam.target_unit))
          {
            List<JobSetParam> jobSetParamList;
            if (this.mJobsetDictionary.ContainsKey(jobSetParam.target_unit))
            {
              jobSetParamList = this.mJobsetDictionary[jobSetParam.target_unit];
            }
            else
            {
              jobSetParamList = new List<JobSetParam>(3);
              this.mJobsetDictionary.Add(jobSetParam.target_unit, jobSetParamList);
            }
            jobSetParamList.Add(jobSetParam);
          }
        }
      }
      if (json.Grow != null)
      {
        if (this.mGrowParam == null)
          this.mGrowParam = new List<GrowParam>(json.Grow.Length);
        for (int index = 0; index < json.Grow.Length; ++index)
        {
          JSON_GrowParam json1 = json.Grow[index];
          GrowParam growParam = new GrowParam();
          this.mGrowParam.Add(growParam);
          growParam.Deserialize(json1);
        }
      }
      if (json.AI != null)
      {
        if (this.mAIParam == null)
          this.mAIParam = new List<AIParam>(json.AI.Length);
        for (int index = 0; index < json.AI.Length; ++index)
        {
          JSON_AIParam json1 = json.AI[index];
          AIParam aiParam = new AIParam();
          this.mAIParam.Add(aiParam);
          aiParam.Deserialize(json1);
        }
      }
      if (json.Geo != null)
      {
        if (this.mGeoParam == null)
          this.mGeoParam = new List<GeoParam>(json.Geo.Length);
        for (int index = 0; index < json.Geo.Length; ++index)
        {
          JSON_GeoParam json1 = json.Geo[index];
          GeoParam geoParam = new GeoParam();
          this.mGeoParam.Add(geoParam);
          geoParam.Deserialize(json1);
        }
      }
      if (json.Rarity != null)
      {
        if (this.mRarityParam == null)
          this.mRarityParam = new List<RarityParam>(json.Rarity.Length);
        for (int index = 0; index < json.Rarity.Length; ++index)
        {
          RarityParam rarityParam = new RarityParam();
          this.mRarityParam.Add(rarityParam);
          rarityParam.Deserialize(json.Rarity[index]);
        }
      }
      if (json.Shop != null)
      {
        if (this.mShopParam == null)
          this.mShopParam = new List<ShopParam>(json.Shop.Length);
        for (int index = 0; index < json.Shop.Length; ++index)
        {
          ShopParam shopParam = new ShopParam();
          this.mShopParam.Add(shopParam);
          shopParam.Deserialize(json.Shop[index]);
        }
      }
      if (json.Player != null)
      {
        this.mPlayerParamTbl = new PlayerParam[json.Player.Length];
        for (int index = 0; index < json.Player.Length; ++index)
        {
          JSON_PlayerParam json1 = json.Player[index];
          this.mPlayerParamTbl[index] = new PlayerParam();
          this.mPlayerParamTbl[index].Deserialize(json1);
        }
      }
      if (json.PlayerLvTbl != null)
      {
        this.mPlayerExpTbl = new OInt[json.PlayerLvTbl.Length];
        for (int index = 0; index < json.PlayerLvTbl.Length; ++index)
          this.mPlayerExpTbl[index] = (OInt) json.PlayerLvTbl[index];
      }
      if (json.UnitLvTbl != null)
      {
        this.mUnitExpTbl = new OInt[json.UnitLvTbl.Length];
        for (int index = 0; index < json.UnitLvTbl.Length; ++index)
          this.mUnitExpTbl[index] = (OInt) json.UnitLvTbl[index];
      }
      if (json.ArtifactLvTbl != null)
      {
        this.mArtifactExpTbl = new OInt[json.ArtifactLvTbl.Length];
        for (int index = 0; index < json.ArtifactLvTbl.Length; ++index)
          this.mArtifactExpTbl[index] = (OInt) json.ArtifactLvTbl[index];
      }
      if (json.AbilityRank != null)
      {
        this.mAbilityExpTbl = new OInt[json.AbilityRank.Length];
        for (int index = 0; index < json.AbilityRank.Length; ++index)
          this.mAbilityExpTbl[index] = (OInt) json.AbilityRank[index];
      }
      if (json.AwakePieceTbl != null)
      {
        this.mAwakePieceTbl = new OInt[json.AwakePieceTbl.Length];
        for (int index = 0; index < json.AwakePieceTbl.Length; ++index)
          this.mAwakePieceTbl[index] = (OInt) json.AwakePieceTbl[index];
      }
      this.mLocalNotificationParam = new LocalNotificationParam();
      if (json.LocalNotification != null)
      {
        this.mLocalNotificationParam.msg_stamina = json.LocalNotification[0].msg_stamina;
        this.mLocalNotificationParam.iOSAct_stamina = json.LocalNotification[0].iOSAct_stamina;
        this.mLocalNotificationParam.limitSec_stamina = json.LocalNotification[0].limitSec_stamina;
      }
      Dictionary<int, TrophyCategoryParam> dictionary = new Dictionary<int, TrophyCategoryParam>();
      if (json.TrophyCategory != null)
      {
        List<TrophyCategoryParam> trophyCategoryParamList = new List<TrophyCategoryParam>(json.TrophyCategory.Length);
        for (int index = 0; index < json.TrophyCategory.Length; ++index)
        {
          TrophyCategoryParam trophyCategoryParam = new TrophyCategoryParam();
          if (trophyCategoryParam.Deserialize(json.TrophyCategory[index]))
          {
            trophyCategoryParamList.Add(trophyCategoryParam);
            if (!dictionary.ContainsKey(trophyCategoryParam.hash_code))
              dictionary.Add(trophyCategoryParam.hash_code, trophyCategoryParam);
          }
        }
        this.mTrophyCategory = trophyCategoryParamList.ToArray();
      }
      if (json.Trophy != null && dictionary.Count > 0)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Trophy.Length);
        for (int index = 0; index < json.Trophy.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(json.Trophy[index]))
          {
            if (dictionary.ContainsKey(trophyParam.category_hash_code))
              trophyParam.CategoryParam = dictionary[trophyParam.category_hash_code];
            else
              DebugUtility.LogError(trophyParam.iname + " => 親カテゴリが未設定 or 入力ミス");
            trophyParamList.Add(trophyParam);
          }
        }
        this.mTrophy = trophyParamList.ToArray();
        this.mTrophyInameDict = new Dictionary<string, TrophyParam>();
        foreach (TrophyParam trophyParam in this.mTrophy)
          this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
      }
      if (json.ChallengeCategory != null)
      {
        List<ChallengeCategoryParam> challengeCategoryParamList = new List<ChallengeCategoryParam>(json.ChallengeCategory.Length);
        for (int index = 0; index < json.ChallengeCategory.Length; ++index)
        {
          ChallengeCategoryParam challengeCategoryParam = new ChallengeCategoryParam();
          if (challengeCategoryParam.Deserialize(json.ChallengeCategory[index]))
            challengeCategoryParamList.Add(challengeCategoryParam);
        }
        this.mChallengeCategory = challengeCategoryParamList.ToArray();
      }
      if (json.Challenge != null)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Challenge.Length);
        for (int index = 0; index < json.Challenge.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(json.Challenge[index]))
          {
            trophyParam.Challenge = 1;
            trophyParamList.Add(trophyParam);
          }
        }
        this.mChallenge = trophyParamList.ToArray();
        int length = this.mTrophy.Length;
        Array.Resize<TrophyParam>(ref this.mTrophy, length + this.mChallenge.Length);
        Array.Copy((Array) this.mChallenge, 0, (Array) this.mTrophy, length, this.mChallenge.Length);
        foreach (TrophyParam trophyParam in this.mChallenge)
          this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
      }
      this.CreateTrophyDict();
      if (json.Unlock != null)
      {
        List<UnlockParam> unlockParamList = new List<UnlockParam>(json.Unlock.Length);
        for (int index = 0; index < json.Unlock.Length; ++index)
        {
          UnlockParam unlockParam = new UnlockParam();
          if (unlockParam.Deserialize(json.Unlock[index]))
            unlockParamList.Add(unlockParam);
        }
        this.mUnlock = unlockParamList.ToArray();
      }
      if (json.Vip != null)
      {
        List<VipParam> vipParamList = new List<VipParam>(json.Vip.Length);
        for (int index = 0; index < json.Vip.Length; ++index)
        {
          VipParam vipParam = new VipParam();
          if (vipParam.Deserialize(json.Vip[index]))
            vipParamList.Add(vipParam);
        }
        this.mVip = vipParamList.ToArray();
      }
      if (json.Mov != null)
      {
        this.mStreamingMovies = new JSON_StreamingMovie[json.Mov.Length];
        for (int index = 0; index < json.Mov.Length; ++index)
        {
          this.mStreamingMovies[index] = new JSON_StreamingMovie();
          this.mStreamingMovies[index].alias = json.Mov[index].alias;
          this.mStreamingMovies[index].path = json.Mov[index].path;
        }
      }
      if (json.Banner != null)
      {
        List<BannerParam> bannerParamList = new List<BannerParam>(json.Banner.Length);
        for (int index = 0; index < json.Banner.Length; ++index)
        {
          BannerParam bannerParam = new BannerParam();
          if (bannerParam.Deserialize(json.Banner[index]))
            bannerParamList.Add(bannerParam);
        }
        this.mBanner = bannerParamList.ToArray();
      }
      for (int index = 0; index < this.mJobParam.Count; ++index)
        this.mJobParam[index].UpdateJobRankTransfarStatus(this);
      if (json.QuestClearUnlockUnitData != null)
      {
        List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>(json.QuestClearUnlockUnitData.Length);
        for (int index = 0; index < json.QuestClearUnlockUnitData.Length; ++index)
        {
          QuestClearUnlockUnitDataParam unlockUnitDataParam = new QuestClearUnlockUnitDataParam();
          unlockUnitDataParam.Deserialize(json.QuestClearUnlockUnitData[index]);
          unlockUnitDataParamList.Add(unlockUnitDataParam);
        }
        this.mUnlockUnitDataParam = unlockUnitDataParamList;
      }
      if (json.Award != null)
      {
        if (this.mAwardParam == null)
          this.mAwardParam = new List<AwardParam>(json.Award.Length);
        if (this.mAwardDictionary == null)
          this.mAwardDictionary = new Dictionary<string, AwardParam>(json.Award.Length);
        for (int index = 0; index < json.Award.Length; ++index)
        {
          JSON_AwardParam json1 = json.Award[index];
          AwardParam awardParam = new AwardParam();
          this.mAwardParam.Add(awardParam);
          awardParam.Deserialize(json1);
          this.mAwardDictionary.Add(awardParam.iname, awardParam);
        }
      }
      if (json.LoginInfo != null)
      {
        List<LoginInfoParam> loginInfoParamList = new List<LoginInfoParam>(json.LoginInfo.Length);
        for (int index = 0; index < json.LoginInfo.Length; ++index)
        {
          LoginInfoParam loginInfoParam = new LoginInfoParam();
          if (loginInfoParam.Deserialize(json.LoginInfo[index]))
            loginInfoParamList.Add(loginInfoParam);
        }
        this.mLoginInfoParam = loginInfoParamList.ToArray();
      }
      if (json.CollaboSkill != null)
      {
        List<CollaboSkillParam> collaboSkillParamList = new List<CollaboSkillParam>(json.CollaboSkill.Length);
        for (int index = 0; index < json.CollaboSkill.Length; ++index)
        {
          CollaboSkillParam collaboSkillParam = new CollaboSkillParam();
          collaboSkillParam.Deserialize(json.CollaboSkill[index]);
          collaboSkillParamList.Add(collaboSkillParam);
        }
        this.mCollaboSkillParam = collaboSkillParamList;
        CollaboSkillParam.UpdateCollaboSkill(this.mCollaboSkillParam);
      }
      if (json.Trick != null)
      {
        List<TrickParam> trickParamList = new List<TrickParam>(json.Trick.Length);
        for (int index = 0; index < json.Trick.Length; ++index)
        {
          TrickParam trickParam = new TrickParam();
          trickParam.Deserialize(json.Trick[index]);
          trickParamList.Add(trickParam);
        }
        this.mTrickParam = trickParamList;
      }
      if (json.BreakObj != null)
      {
        List<BreakObjParam> breakObjParamList = new List<BreakObjParam>(json.BreakObj.Length);
        for (int index = 0; index < json.BreakObj.Length; ++index)
        {
          BreakObjParam breakObjParam = new BreakObjParam();
          breakObjParam.Deserialize(json.BreakObj[index]);
          breakObjParamList.Add(breakObjParam);
        }
        this.mBreakObjParam = breakObjParamList;
      }
      if (json.VersusMatchKey != null)
      {
        this.mVersusMatching = new List<VersusMatchingParam>(json.VersusMatchKey.Length);
        for (int index = 0; index < json.VersusMatchKey.Length; ++index)
        {
          VersusMatchingParam versusMatchingParam = new VersusMatchingParam();
          versusMatchingParam.Deserialize(json.VersusMatchKey[index]);
          this.mVersusMatching.Add(versusMatchingParam);
        }
      }
      if (json.VersusMatchCond != null)
      {
        this.mVersusMatchCond = new List<VersusMatchCondParam>(json.VersusMatchCond.Length);
        for (int index = 0; index < json.VersusMatchCond.Length; ++index)
        {
          VersusMatchCondParam versusMatchCondParam = new VersusMatchCondParam();
          versusMatchCondParam.Deserialize(json.VersusMatchCond[index]);
          this.mVersusMatchCond.Add(versusMatchCondParam);
        }
      }
      if (json.TowerScore != null)
      {
        this.mTowerScore = new List<TowerScoreParam>(json.TowerScore.Length);
        for (int index = 0; index < json.TowerScore.Length; ++index)
        {
          TowerScoreParam towerScoreParam = new TowerScoreParam();
          if (towerScoreParam.Deserialize(json.TowerScore[index]))
            this.mTowerScore.Add(towerScoreParam);
        }
      }
      if (json.TowerRank != null)
      {
        this.mTowerRankTbl = new OInt[json.TowerRank.Length];
        for (int index = 0; index < json.TowerRank.Length; ++index)
          this.mTowerRankTbl[index] = (OInt) json.TowerRank[index];
      }
      if (json.MultilimitUnitLv != null)
      {
        this.mMultiLimitUnitLv = new OInt[json.MultilimitUnitLv.Length];
        for (int index = 0; index < json.MultilimitUnitLv.Length; ++index)
          this.mMultiLimitUnitLv[index] = (OInt) json.MultilimitUnitLv[index];
      }
      if (json.FriendPresentItem != null)
      {
        this.mFriendPresentItemParam = new Dictionary<string, FriendPresentItemParam>();
        for (int index = 0; index < json.FriendPresentItem.Length; ++index)
        {
          FriendPresentItemParam presentItemParam = new FriendPresentItemParam();
          presentItemParam.Deserialize(json.FriendPresentItem[index]);
          this.mFriendPresentItemParam.Add(presentItemParam.iname, presentItemParam);
        }
      }
      if (json.Weather != null)
      {
        List<WeatherParam> weatherParamList = new List<WeatherParam>(json.Weather.Length);
        for (int index = 0; index < json.Weather.Length; ++index)
        {
          WeatherParam weatherParam = new WeatherParam();
          weatherParam.Deserialize(json.Weather[index]);
          weatherParamList.Add(weatherParam);
        }
        this.mWeatherParam = weatherParamList;
      }
      this.Loaded = true;
      return true;
    }

    public ItemParam GetCommonEquip(ItemParam item_param, bool is_soul)
    {
      if (!is_soul)
      {
        if (!item_param.IsCommon)
          return (ItemParam) null;
        return MonoSingleton<GameManager>.Instance.GetItemParam((string) this.FixParam.EquipCmn[(int) item_param.cmn_type - 1]);
      }
      int rare = (int) item_param.rare;
      if (this.FixParam.SoulCommonPiece == null || this.FixParam.SoulCommonPiece.Length <= rare)
        return (ItemParam) null;
      return MonoSingleton<GameManager>.Instance.GetItemParam((string) this.FixParam.SoulCommonPiece[rare]);
    }

    public bool IsFriendPresentItemParamValid()
    {
      if (this.mFriendPresentItemParam != null)
        return this.mFriendPresentItemParam.Count > 1;
      return false;
    }

    public FriendPresentItemParam[] GetFriendPresentItemParams()
    {
      if (this.mFriendPresentItemParam == null)
        return new FriendPresentItemParam[0];
      FriendPresentItemParam[] array = new FriendPresentItemParam[this.mFriendPresentItemParam.Values.Count];
      this.mFriendPresentItemParam.Values.CopyTo(array, 0);
      return array;
    }

    public FriendPresentItemParam GetFriendPresentItemParam(string key)
    {
      if (this.mFriendPresentItemParam == null)
        return (FriendPresentItemParam) null;
      if (string.IsNullOrEmpty(key))
        return (FriendPresentItemParam) null;
      FriendPresentItemParam presentItemParam = (FriendPresentItemParam) null;
      if (!this.mFriendPresentItemParam.TryGetValue(key, out presentItemParam))
        DebugUtility.LogError("存在しないフレンドプレゼントアイテムパラメータを参照しています > " + key);
      return presentItemParam;
    }

    public void MakeMapEffectHaveJobLists()
    {
      if (this.mJobParam == null || MapEffectParam.IsMakeHaveJobLists())
        return;
      MapEffectParam.MakeHaveJobLists();
      using (List<JobParam>.Enumerator enumerator = this.mJobParam.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          JobParam current = enumerator.Current;
          if (!string.IsNullOrEmpty(current.MapEffectAbility) && current.IsMapEffectRevReso)
          {
            AbilityParam abilityParam = this.GetAbilityParam(current.MapEffectAbility);
            if (abilityParam != null)
            {
              foreach (LearningSkill skill in abilityParam.skills)
                MapEffectParam.AddHaveJob(skill.iname, current);
            }
          }
        }
      }
    }
  }
}
