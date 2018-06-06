// Decompiled with JetBrains decompiler
// Type: DownloadUtility
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using System.Text;

public static class DownloadUtility
{
  public static void DownloadQuestBase(QuestParam q)
  {
    if (q.map.Count > 0)
    {
      string mapSceneName = q.map[0].mapSceneName;
      string mapSetName = q.map[0].mapSetName;
      if (!string.IsNullOrEmpty(mapSceneName))
      {
        AssetManager.PrepareAssets(mapSceneName);
        AssetManager.PrepareAssets(AssetPath.LocalMap(mapSceneName));
      }
      if (!string.IsNullOrEmpty(mapSetName))
        AssetManager.PrepareAssets(AssetPath.LocalMap(mapSetName));
    }
    if (!string.IsNullOrEmpty(q.storyTextID))
      AssetManager.PrepareAssets(LocalizedText.GetResourcePath(q.storyTextID));
    if (!string.IsNullOrEmpty(q.navigation))
      AssetManager.PrepareAssets(AssetPath.Navigation(q));
    if (!string.IsNullOrEmpty(q.event_start))
      AssetManager.PrepareAssets(AssetPath.QuestEvent(q.event_start));
    if (q.map != null)
    {
      for (int index = 0; index < q.map.Count; ++index)
      {
        if (!string.IsNullOrEmpty(q.map[index].eventSceneName))
          AssetManager.PrepareAssets(AssetPath.QuestEvent(q.map[index].eventSceneName));
      }
    }
    if (string.IsNullOrEmpty(q.event_clear))
      return;
    AssetManager.PrepareAssets(AssetPath.QuestEvent(q.event_clear));
  }

  public static void DownloadQuestMaps(QuestParam quest)
  {
    for (int index1 = 0; index1 < quest.map.Count; ++index1)
    {
      if (!string.IsNullOrEmpty(quest.map[index1].mapSetName))
      {
        string src = AssetManager.LoadTextData(AssetPath.LocalMap(quest.map[index1].mapSetName));
        if (src != null)
        {
          JSON_MapUnit jsonObject = JSONParser.parseJSONObject<JSON_MapUnit>(src);
          if ((int) jsonObject.is_rand > 0)
          {
            if (jsonObject.deck != null)
            {
              for (int index2 = 0; index2 < jsonObject.deck.Length; ++index2)
                DownloadUtility.DownloadUnit(new NPCSetting(jsonObject.deck[index2]));
            }
          }
          else if (jsonObject.enemy != null)
          {
            for (int index2 = 0; index2 < jsonObject.enemy.Length; ++index2)
              DownloadUtility.DownloadUnit(new NPCSetting(jsonObject.enemy[index2]));
          }
        }
      }
    }
  }

  public static void DownloadJobEquipment(JobParam job)
  {
    if (job == null)
      return;
    string resourcePath = AssetPath.JobEquipment(job);
    if (string.IsNullOrEmpty(resourcePath))
      return;
    AssetManager.PrepareAssets(resourcePath);
  }

  public static void DownloadArtifact(ArtifactParam artifalct)
  {
    if (artifalct == null)
      return;
    string resourcePath = AssetPath.Artifacts(artifalct);
    if (string.IsNullOrEmpty(resourcePath))
      return;
    AssetManager.PrepareAssets(resourcePath);
  }

  public static void DownloadUnit(UnitParam unit, JobData[] jobs = null)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DownloadUtility.\u003CDownloadUnit\u003Ec__AnonStorey1ED unitCAnonStorey1Ed = new DownloadUtility.\u003CDownloadUnit\u003Ec__AnonStorey1ED();
    // ISSUE: reference to a compiler-generated field
    unitCAnonStorey1Ed.unit = unit;
    // ISSUE: reference to a compiler-generated field
    if (unitCAnonStorey1Ed.unit == null)
      return;
    // ISSUE: reference to a compiler-generated field
    CharacterDB.Character character = CharacterDB.FindCharacter(unitCAnonStorey1Ed.unit.model);
    if (character == null)
      return;
    GameManager instance = MonoSingleton<GameManager>.Instance;
    // ISSUE: reference to a compiler-generated field
    if (unitCAnonStorey1Ed.unit.jobsets != null)
    {
      // ISSUE: reference to a compiler-generated field
      for (int index = 0; index < unitCAnonStorey1Ed.unit.jobsets.Length; ++index)
      {
        // ISSUE: reference to a compiler-generated field
        for (JobSetParam jobSetParam = instance.GetJobSetParam((string) unitCAnonStorey1Ed.unit.jobsets[index]); jobSetParam != null; jobSetParam = string.IsNullOrEmpty(jobSetParam.jobchange) ? (JobSetParam) null : instance.GetJobSetParam(jobSetParam.jobchange))
        {
          JobParam jobParam = instance.GetJobParam(jobSetParam.job);
          // ISSUE: reference to a compiler-generated field
          SkillParam skillParam = string.IsNullOrEmpty(jobParam.atkskill[0]) ? instance.MasterParam.GetSkillParam(jobParam.atkskill[(int) unitCAnonStorey1Ed.unit.element]) : instance.MasterParam.GetSkillParam(jobParam.atkskill[0]);
          if (skillParam != null)
          {
            SkillSequence sequence = SkillSequence.FindSequence(skillParam.motion);
            if (sequence != null && !string.IsNullOrEmpty(sequence.SkillAnimation.Name) && index < character.Jobs.Count)
              DownloadUtility.PrepareUnitAnimation(character.Jobs[index], sequence.SkillAnimation.Name, false, (JobParam) null);
          }
          DownloadUtility.DownloadJobEquipment(jobParam);
          ArtifactParam artifactParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(jobParam.artifact);
          if (artifactParam != null)
            DownloadUtility.DownloadArtifact(artifactParam);
          int artifactSlotIndex = JobData.GetArtifactSlotIndex(ArtifactTypes.Arms);
          if (jobs != null)
          {
            foreach (JobData job in jobs)
            {
              if (job != null)
              {
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                DownloadUtility.\u003CDownloadUnit\u003Ec__AnonStorey1EC unitCAnonStorey1Ec = new DownloadUtility.\u003CDownloadUnit\u003Ec__AnonStorey1EC();
                List<ArtifactData> artifacts = MonoSingleton<GameManager>.GetInstanceDirect().Player.Artifacts;
                // ISSUE: reference to a compiler-generated field
                unitCAnonStorey1Ec.uniqId = job.Artifacts[artifactSlotIndex];
                // ISSUE: reference to a compiler-generated method
                ArtifactData artifactData = artifacts.Find(new Predicate<ArtifactData>(unitCAnonStorey1Ec.\u003C\u003Em__1B4));
                if (artifactData != null)
                  DownloadUtility.DownloadArtifact(artifactData.ArtifactParam);
              }
            }
          }
          else
            DownloadUtility.DownloadArtifact(instance.MasterParam.GetArtifactParam(jobParam.artifact));
          // ISSUE: reference to a compiler-generated field
          AssetManager.PrepareAssets(AssetPath.UnitImage(unitCAnonStorey1Ed.unit, jobSetParam.job));
          // ISSUE: reference to a compiler-generated field
          AssetManager.PrepareAssets(AssetPath.UnitImage2(unitCAnonStorey1Ed.unit, jobSetParam.job));
          // ISSUE: reference to a compiler-generated field
          AssetManager.PrepareAssets(AssetPath.UnitIconSmall(unitCAnonStorey1Ed.unit, jobSetParam.job));
          // ISSUE: reference to a compiler-generated field
          AssetManager.PrepareAssets(AssetPath.UnitIconMedium(unitCAnonStorey1Ed.unit, jobSetParam.job));
          // ISSUE: reference to a compiler-generated field
          AssetManager.PrepareAssets(AssetPath.UnitEyeImage(unitCAnonStorey1Ed.unit, jobSetParam.job));
        }
      }
      // ISSUE: reference to a compiler-generated field
      JobSetParam[] changeJobSetParam = instance.GetClassChangeJobSetParam(unitCAnonStorey1Ed.unit.iname);
      if (changeJobSetParam != null && changeJobSetParam.Length > 0)
      {
        for (int index = 0; index < changeJobSetParam.Length; ++index)
        {
          JobSetParam jobSetParam = changeJobSetParam[index];
          if (jobSetParam != null)
          {
            JobParam jobParam = instance.GetJobParam(jobSetParam.job);
            ArtifactParam artifactParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(jobParam.artifact);
            if (artifactParam != null)
              DownloadUtility.DownloadArtifact(artifactParam);
            // ISSUE: reference to a compiler-generated field
            SkillParam skillParam = string.IsNullOrEmpty(jobParam.atkskill[0]) ? instance.MasterParam.GetSkillParam(jobParam.atkskill[(int) unitCAnonStorey1Ed.unit.element]) : instance.MasterParam.GetSkillParam(jobParam.atkskill[0]);
            if (skillParam != null)
            {
              SkillSequence sequence = SkillSequence.FindSequence(skillParam.motion);
              if (sequence != null && !string.IsNullOrEmpty(sequence.SkillAnimation.Name))
                DownloadUtility.PrepareUnitAnimation(character.Jobs[index], sequence.SkillAnimation.Name, false, (JobParam) null);
            }
          }
        }
      }
    }
    for (int index = 0; index < character.Jobs.Count; ++index)
    {
      CharacterDB.Job job = character.Jobs[index];
      DownloadUtility.PrepareUnitModels(job);
      DownloadUtility.PrepareUnitAnimation(job, "unit_info_idle0", true, (JobParam) null);
      DownloadUtility.PrepareUnitAnimation(job, "unit_info_act0", true, (JobParam) null);
    }
    // ISSUE: reference to a compiler-generated field
    AssetManager.PrepareAssets("CHM/Home_" + unitCAnonStorey1Ed.unit.model + "_walk0");
    // ISSUE: reference to a compiler-generated field
    if (unitCAnonStorey1Ed.unit.skins != null)
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.Artifacts;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DownloadUtility.\u003CDownloadUnit\u003Ec__AnonStorey1EE unitCAnonStorey1Ee = new DownloadUtility.\u003CDownloadUnit\u003Ec__AnonStorey1EE();
      // ISSUE: reference to a compiler-generated field
      unitCAnonStorey1Ee.\u003C\u003Ef__ref\u0024493 = unitCAnonStorey1Ed;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (unitCAnonStorey1Ee.i = 0; unitCAnonStorey1Ee.i < unitCAnonStorey1Ed.unit.skins.Length; ++unitCAnonStorey1Ee.i)
      {
        // ISSUE: reference to a compiler-generated method
        ArtifactParam skin = artifacts.Find(new Predicate<ArtifactParam>(unitCAnonStorey1Ee.\u003C\u003Em__1B5));
        if (skin != null)
        {
          // ISSUE: reference to a compiler-generated field
          AssetManager.PrepareAssets(AssetPath.UnitSkinImage(unitCAnonStorey1Ed.unit, skin, (string) null));
          // ISSUE: reference to a compiler-generated field
          AssetManager.PrepareAssets(AssetPath.UnitSkinImage2(unitCAnonStorey1Ed.unit, skin, (string) null));
          // ISSUE: reference to a compiler-generated field
          AssetManager.PrepareAssets(AssetPath.UnitSkinIconSmall(unitCAnonStorey1Ed.unit, skin, (string) null));
          // ISSUE: reference to a compiler-generated field
          AssetManager.PrepareAssets(AssetPath.UnitSkinIconMedium(unitCAnonStorey1Ed.unit, skin, (string) null));
          // ISSUE: reference to a compiler-generated field
          AssetManager.PrepareAssets(AssetPath.UnitSkinEyeImage(unitCAnonStorey1Ed.unit, skin, (string) null));
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    DownloadUtility.PrepareUnitVoice(unitCAnonStorey1Ed.unit);
  }

  private static void PrepareUnitVoice(UnitParam unitParam)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DownloadUtility.\u003CPrepareUnitVoice\u003Ec__AnonStorey1EF voiceCAnonStorey1Ef = new DownloadUtility.\u003CPrepareUnitVoice\u003Ec__AnonStorey1EF();
    // ISSUE: reference to a compiler-generated field
    voiceCAnonStorey1Ef.unitParam = unitParam;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (voiceCAnonStorey1Ef.unitParam == null || string.IsNullOrEmpty(voiceCAnonStorey1Ef.unitParam.voice))
      return;
    // ISSUE: reference to a compiler-generated field
    string[] strArray1 = MySound.VoiceCueSheetFileName(voiceCAnonStorey1Ef.unitParam.voice);
    if (strArray1 == null)
      return;
    for (int index = 0; index < strArray1.Length; ++index)
      AssetManager.PrepareAssets("StreamingAssets/" + strArray1[index]);
    string empty = string.Empty;
    // ISSUE: reference to a compiler-generated field
    if (voiceCAnonStorey1Ef.unitParam.skins != null)
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.Artifacts;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DownloadUtility.\u003CPrepareUnitVoice\u003Ec__AnonStorey1F0 voiceCAnonStorey1F0 = new DownloadUtility.\u003CPrepareUnitVoice\u003Ec__AnonStorey1F0();
      // ISSUE: reference to a compiler-generated field
      voiceCAnonStorey1F0.\u003C\u003Ef__ref\u0024495 = voiceCAnonStorey1Ef;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (voiceCAnonStorey1F0.i = 0; voiceCAnonStorey1F0.i < voiceCAnonStorey1Ef.unitParam.skins.Length; ++voiceCAnonStorey1F0.i)
      {
        // ISSUE: reference to a compiler-generated method
        ArtifactParam artifact = artifacts.Find(new Predicate<ArtifactParam>(voiceCAnonStorey1F0.\u003C\u003Em__1B6));
        if (artifact != null && !string.IsNullOrEmpty(artifact.voice))
        {
          // ISSUE: reference to a compiler-generated field
          string charName = AssetPath.UnitVoiceFileName(voiceCAnonStorey1Ef.unitParam, artifact, string.Empty);
          if (!string.IsNullOrEmpty(charName))
          {
            string[] strArray2 = MySound.VoiceCueSheetFileName(charName);
            if (strArray2 != null)
            {
              for (int index = 0; index < strArray2.Length; ++index)
                AssetManager.PrepareAssets("StreamingAssets/" + strArray2[index]);
            }
          }
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (voiceCAnonStorey1Ef.unitParam.job_voices == null)
      return;
    // ISSUE: reference to a compiler-generated field
    for (int index1 = 0; index1 < voiceCAnonStorey1Ef.unitParam.job_voices.Length; ++index1)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      string charName = AssetPath.UnitVoiceFileName(voiceCAnonStorey1Ef.unitParam, (ArtifactParam) null, (string) voiceCAnonStorey1Ef.unitParam.job_voices[index1]);
      if (!string.IsNullOrEmpty(charName))
      {
        string[] strArray2 = MySound.VoiceCueSheetFileName(charName);
        if (strArray2 != null)
        {
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
            AssetManager.PrepareAssets("StreamingAssets/" + strArray2[index2]);
        }
      }
    }
  }

  public static void DownloadUnit(NPCSetting npc)
  {
    Unit unit = new Unit();
    unit.Setup((UnitData) null, (UnitSetting) npc, (Unit.DropItem) null, (Unit.DropItem) null);
    DownloadUtility.DownloadUnit(unit, false);
  }

  public static void PrepareUnitModels(CharacterDB.Job jobData)
  {
    if (!string.IsNullOrEmpty(jobData.BodyName))
      AssetManager.PrepareAssets("CH/BODY/" + jobData.BodyName);
    if (!string.IsNullOrEmpty(jobData.BodyAttachmentName))
      AssetManager.PrepareAssets("CH/BODYOPT/" + jobData.BodyAttachmentName);
    if (!string.IsNullOrEmpty(jobData.BodyTextureName))
      AssetManager.PrepareAssets("CH/BODYTEX/" + jobData.BodyTextureName);
    if (!string.IsNullOrEmpty(jobData.HeadName))
      AssetManager.PrepareAssets("CH/HEAD/" + jobData.HeadName);
    if (!string.IsNullOrEmpty(jobData.HeadAttachmentName))
      AssetManager.PrepareAssets("CH/HEADOPT/" + jobData.HeadAttachmentName);
    if (string.IsNullOrEmpty(jobData.HairName))
      return;
    AssetManager.PrepareAssets("CH/HAIR/" + jobData.HairName);
  }

  private static void PrepareUnitAnimation(CharacterDB.Job jobData, string animationName, bool addJobName, JobParam param = null)
  {
    StringBuilder stringBuilder = GameUtility.GetStringBuilder();
    stringBuilder.Append("CHM/");
    stringBuilder.Append(jobData.AssetPrefix);
    stringBuilder.Append('_');
    if (addJobName)
    {
      if (param != null)
      {
        stringBuilder.Append(param.model);
        stringBuilder.Append('_');
      }
      else
      {
        stringBuilder.Append(jobData.JobID);
        stringBuilder.Append('_');
      }
    }
    stringBuilder.Append(animationName);
    AssetManager.PrepareAssets(stringBuilder.ToString());
  }

  private static CharacterDB.Job GetCharacterData(UnitParam unit, JobParam job, ArtifactParam skin)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DownloadUtility.\u003CGetCharacterData\u003Ec__AnonStorey1F1 dataCAnonStorey1F1 = new DownloadUtility.\u003CGetCharacterData\u003Ec__AnonStorey1F1();
    CharacterDB.Character character = CharacterDB.FindCharacter(unit.model);
    if (character == null)
    {
      DebugUtility.LogWarning("Unknown character '" + unit.model + "'");
      return (CharacterDB.Job) null;
    }
    // ISSUE: reference to a compiler-generated field
    dataCAnonStorey1F1.jobResourceID = skin == null ? string.Empty : skin.asset;
    // ISSUE: reference to a compiler-generated field
    if (string.IsNullOrEmpty(dataCAnonStorey1F1.jobResourceID))
    {
      // ISSUE: reference to a compiler-generated field
      dataCAnonStorey1F1.jobResourceID = job == null ? "none" : job.model;
    }
    // ISSUE: reference to a compiler-generated method
    int index = character.Jobs.FindIndex(new Predicate<CharacterDB.Job>(dataCAnonStorey1F1.\u003C\u003Em__1B7));
    if (index < 0)
    {
      // ISSUE: reference to a compiler-generated field
      DebugUtility.LogWarning("Invalid Character " + unit.model + "@" + dataCAnonStorey1F1.jobResourceID);
      index = 0;
    }
    return character.Jobs[index];
  }

  public static void DownloadUnit(Unit unit, bool dlStatusEffects = false)
  {
    UnitParam unitParam = unit.UnitParam;
    JobParam job = unit.Job == null ? (JobParam) null : unit.Job.Param;
    ArtifactParam selectedSkin = unit.UnitData.GetSelectedSkin(-1);
    CharacterDB.Job characterData = DownloadUtility.GetCharacterData(unitParam, job, selectedSkin);
    if (characterData == null)
      return;
    DownloadUtility.PrepareUnitAssets(characterData, job);
    if (unit.Job != null)
      DownloadUtility.PrepareJobAssets(unit.Job.Param);
    string jobName = unit.UnitData.CurrentJob == null ? string.Empty : unit.UnitData.CurrentJob.JobID;
    AssetManager.PrepareAssets(AssetPath.UnitSkinIconSmall(unitParam, selectedSkin, jobName));
    AssetManager.PrepareAssets(AssetPath.UnitSkinIconMedium(unitParam, selectedSkin, jobName));
    AssetManager.PrepareAssets(AssetPath.UnitSkinEyeImage(unitParam, selectedSkin, jobName));
    SkillData attackSkill = unit.GetAttackSkill();
    if (attackSkill != null)
      DownloadUtility.PrepareSkillAssets(characterData, attackSkill.SkillParam);
    for (int index = unit.BattleSkills.Count - 1; index >= 0; --index)
      DownloadUtility.PrepareSkillAssets(characterData, unit.BattleSkills[index].SkillParam);
    for (int index = unit.BattleAbilitys.Count - 1; index >= 0; --index)
    {
      AbilityData battleAbility = unit.BattleAbilitys[index];
      if (battleAbility != null && battleAbility.Param != null)
        AssetManager.PrepareAssets(AssetPath.AbilityIcon(battleAbility.Param));
    }
    if (unit != null)
      DownloadUtility.PrepareUnitVoice(unit.UnitParam);
    if (dlStatusEffects)
    {
      for (int index = 0; index < BadStatusEffects.Effects.Count; ++index)
      {
        if (!string.IsNullOrEmpty(BadStatusEffects.Effects[index].AnimationName))
          DownloadUtility.PrepareUnitAnimation(characterData, BadStatusEffects.Effects[index].AnimationName, false, (JobParam) null);
      }
    }
    JobData[] jobs = unit.UnitData.Jobs;
    int artifactSlotIndex = JobData.GetArtifactSlotIndex(ArtifactTypes.Arms);
    if (jobs != null)
    {
      List<ArtifactParam> artifacts = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.Artifacts;
      foreach (JobData jobData in jobs)
      {
        if (jobData != null)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: reference to a compiler-generated method
          ArtifactParam artifalct = artifacts.Find(new Predicate<ArtifactParam>(new DownloadUtility.\u003CDownloadUnit\u003Ec__AnonStorey1F2() { uniqId = (string) null, uniqId = jobData.ArtifactDatas[artifactSlotIndex] == null ? jobData.Param.artifact : jobData.ArtifactDatas[artifactSlotIndex].ArtifactParam.iname }.\u003C\u003Em__1B8));
          if (artifalct != null)
            DownloadUtility.DownloadArtifact(artifalct);
        }
      }
    }
    else
    {
      if (unit.Job == null)
        return;
      DownloadUtility.DownloadArtifact(MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(unit.Job.Param.artifact));
    }
  }

  public static void PrepareUnitAssets(CharacterDB.Job jobData, JobParam param = null)
  {
    DownloadUtility.PrepareUnitModels(jobData);
    DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_IDLE_FIELD, true, param);
    DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_IDLE_DEMO, true, (JobParam) null);
    if (jobData.Movable)
    {
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_RUN_FIELD, true, param);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_STEP, false, (JobParam) null);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_FALL_LOOP, false, (JobParam) null);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_FALL_END, false, (JobParam) null);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_CLIMBUP, false, (JobParam) null);
      DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_GENKIDAMA, false, (JobParam) null);
    }
    DownloadUtility.PrepareUnitAnimation(jobData, TacticsUnitController.ANIM_PICKUP, false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_downstand0", false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_dodge0", false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_damage0", false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_damageair0", false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_down0", false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "itemuse0", false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_toss_lift0", false, (JobParam) null);
    DownloadUtility.PrepareUnitAnimation(jobData, "cmn_toss_throw0", false, (JobParam) null);
  }

  public static void PrepareJobAssets(JobParam job)
  {
    if (job == null)
      return;
    AssetManager.PrepareAssets(AssetPath.JobIconMedium(job));
    AssetManager.PrepareAssets(AssetPath.JobIconSmall(job));
    if (string.IsNullOrEmpty(job.wepmdl))
      return;
    AssetManager.PrepareAssets(AssetPath.JobEquipment(job));
  }

  private static void PrepareSkillAssets(CharacterDB.Job jobData, SkillParam skill)
  {
    if (skill == null || string.IsNullOrEmpty(skill.motion) && string.IsNullOrEmpty(skill.effect))
      return;
    SkillSequence sequence = SkillSequence.FindSequence(skill.motion);
    if (sequence == null)
      return;
    if (!string.IsNullOrEmpty(sequence.SkillAnimation.Name))
      DownloadUtility.PrepareUnitAnimation(jobData, sequence.SkillAnimation.Name, false, (JobParam) null);
    if (!string.IsNullOrEmpty(sequence.ChantAnimation.Name))
      DownloadUtility.PrepareUnitAnimation(jobData, sequence.ChantAnimation.Name, false, (JobParam) null);
    if (!string.IsNullOrEmpty(sequence.EndAnimation.Name))
      DownloadUtility.PrepareUnitAnimation(jobData, sequence.EndAnimation.Name, false, (JobParam) null);
    if (!string.IsNullOrEmpty(sequence.StartAnimation))
      DownloadUtility.PrepareUnitAnimation(jobData, sequence.StartAnimation, false, (JobParam) null);
    if (!string.IsNullOrEmpty(skill.effect))
    {
      AssetManager.PrepareAssets(AssetPath.SkillEffect(skill));
      if (!string.IsNullOrEmpty(skill.CollaboMainId))
        AssetManager.PrepareAssets(AssetPath.SkillEffect(skill) + "_sub");
    }
    if (string.IsNullOrEmpty(skill.SceneName))
      return;
    AssetManager.PrepareAssets(AssetPath.SkillScene(skill.SceneName));
  }

  public static void PrepareInventoryAssets()
  {
    GameManager instance = MonoSingleton<GameManager>.Instance;
    PlayerData player = instance.Player;
    for (int index = 0; index < player.Inventory.Length; ++index)
    {
      ItemData itemData = player.Inventory[index];
      if (itemData != null && itemData.Param != null && !string.IsNullOrEmpty((string) itemData.Param.skill))
      {
        SkillParam skillParam = instance.GetSkillParam((string) itemData.Param.skill);
        if (skillParam != null && !string.IsNullOrEmpty(skillParam.effect))
          AssetManager.PrepareAssets(AssetPath.SkillEffect(skillParam));
      }
    }
  }
}
