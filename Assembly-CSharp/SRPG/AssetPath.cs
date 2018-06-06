// Decompiled with JetBrains decompiler
// Type: SRPG.AssetPath
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public static class AssetPath
  {
    private static StringBuilder mSB = new StringBuilder(512);
    private static string JobResourceID_None = "none";

    private static string GetJobResourceID(JobParam job)
    {
      if (job == null)
        return AssetPath.JobResourceID_None;
      return job.model;
    }

    public static string AbilityIcon(AbilityParam ability)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("AbilityIcon/");
      AssetPath.mSB.Append(ability.icon);
      return AssetPath.mSB.ToString();
    }

    public static string LocalMap(string sceneName)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("LocalMaps/");
      AssetPath.mSB.Append(sceneName);
      return AssetPath.mSB.ToString();
    }

    public static string Navigation(QuestParam quest)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("UI/");
      AssetPath.mSB.Append(quest.navigation);
      return AssetPath.mSB.ToString();
    }

    public static string QuestEvent(string eventName)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("Events/");
      AssetPath.mSB.Append(eventName);
      return AssetPath.mSB.ToString();
    }

    public static string UnitImage(UnitParam unit, string jobName)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("UnitImages/");
      string jobImage = unit.GetJobImage(jobName);
      AssetPath.mSB.Append(string.IsNullOrEmpty(jobImage) ? unit.image : jobImage);
      return AssetPath.mSB.ToString();
    }

    public static string UnitSkinImage(UnitParam unit, ArtifactParam skin, string jobName)
    {
      if (skin == null)
        return AssetPath.UnitImage(unit, jobName);
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("UnitImages/");
      AssetPath.mSB.Append(unit.image);
      AssetPath.mSB.Append("_");
      AssetPath.mSB.Append(skin.asset);
      return AssetPath.mSB.ToString();
    }

    public static string UnitImage2(UnitParam unit, string jobName)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("UnitImages2/");
      string jobImage = unit.GetJobImage(jobName);
      AssetPath.mSB.Append(string.IsNullOrEmpty(jobImage) ? unit.image : jobImage);
      return AssetPath.mSB.ToString();
    }

    public static string UnitSkinImage2(UnitParam unit, ArtifactParam skin, string jobName)
    {
      if (skin == null)
        return AssetPath.UnitImage2(unit, jobName);
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("UnitImages2/");
      AssetPath.mSB.Append(unit.image);
      AssetPath.mSB.Append("_");
      AssetPath.mSB.Append(skin.asset);
      return AssetPath.mSB.ToString();
    }

    public static string UnitIconSmall(UnitParam unit, string jobName)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("Portraits/");
      string jobImage = unit.GetJobImage(jobName);
      AssetPath.mSB.Append(string.IsNullOrEmpty(jobImage) ? unit.model : jobImage);
      return AssetPath.mSB.ToString();
    }

    public static string UnitSkinIconSmall(UnitParam unit, ArtifactParam skin, string jobName)
    {
      if (skin == null)
        return AssetPath.UnitIconSmall(unit, jobName);
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("Portraits/");
      AssetPath.mSB.Append(unit.model);
      AssetPath.mSB.Append("_");
      AssetPath.mSB.Append(skin.asset);
      return AssetPath.mSB.ToString();
    }

    public static string UnitIconMedium(UnitParam unit, string jobName)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("PortraitsM/");
      string jobImage = unit.GetJobImage(jobName);
      AssetPath.mSB.Append(string.IsNullOrEmpty(jobImage) ? unit.model : jobImage);
      return AssetPath.mSB.ToString();
    }

    public static string UnitSkinIconMedium(UnitParam unit, ArtifactParam skin, string jobName)
    {
      if (skin == null)
        return AssetPath.UnitIconMedium(unit, jobName);
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("PortraitsM/");
      AssetPath.mSB.Append(unit.model);
      AssetPath.mSB.Append("_");
      AssetPath.mSB.Append(skin.asset);
      return AssetPath.mSB.ToString();
    }

    public static string UnitEyeImage(UnitParam unit, string jobName)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("UnitEyeImages/");
      string jobImage = unit.GetJobImage(jobName);
      AssetPath.mSB.Append(string.IsNullOrEmpty(jobImage) ? unit.model : jobImage);
      return AssetPath.mSB.ToString();
    }

    public static string UnitSkinEyeImage(UnitParam unit, ArtifactParam skin, string jobName)
    {
      if (skin == null)
        return AssetPath.UnitEyeImage(unit, jobName);
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("UnitEyeImages/");
      AssetPath.mSB.Append(unit.model);
      AssetPath.mSB.Append("_");
      AssetPath.mSB.Append(skin.asset);
      return AssetPath.mSB.ToString();
    }

    public static string JobIconSmall(JobParam job)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("JobIcon/");
      AssetPath.mSB.Append(AssetPath.GetJobResourceID(job));
      return AssetPath.mSB.ToString();
    }

    public static string JobIconMedium(JobParam job)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("JobIconM/");
      AssetPath.mSB.Append(AssetPath.GetJobResourceID(job));
      return AssetPath.mSB.ToString();
    }

    public static string UnitCurrentJobIconSmall(UnitData unit)
    {
      return AssetPath.JobIconSmall(unit.CurrentJob == null ? (JobParam) null : unit.CurrentJob.Param);
    }

    public static string UnitCurrentJobIconMedium(UnitData unit)
    {
      return AssetPath.JobIconMedium(unit.CurrentJob == null ? (JobParam) null : unit.CurrentJob.Param);
    }

    public static string JobIconThumbnail()
    {
      return "JobIcon/small";
    }

    public static string JobEquipment(JobParam job)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("Equipments/");
      AssetPath.mSB.Append(job.wepmdl);
      return AssetPath.mSB.ToString();
    }

    public static string Artifacts(ArtifactParam artifalct)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("Equipments/");
      AssetPath.mSB.Append(artifalct.asset);
      return AssetPath.mSB.ToString();
    }

    public static string ItemIcon(ItemParam param)
    {
      if (param != null)
      {
        if (param.type != EItemType.ArtifactPiece)
          return AssetPath.ItemIcon((string) param.icon);
        if (!string.IsNullOrEmpty((string) param.icon))
        {
          AssetPath.mSB.Length = 0;
          AssetPath.mSB.Append("ArtiIcon/");
          AssetPath.mSB.Append((string) param.icon);
          return AssetPath.mSB.ToString();
        }
      }
      return (string) null;
    }

    public static string ItemIcon(string iconName)
    {
      if (string.IsNullOrEmpty(iconName))
        return (string) null;
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("ItemIcon/");
      AssetPath.mSB.Append(iconName);
      return AssetPath.mSB.ToString();
    }

    public static string SkillEffect(SkillParam skill)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("SkillEff/");
      AssetPath.mSB.Append(skill.effect);
      return AssetPath.mSB.ToString();
    }

    public static string SkillScene(string sceneName)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("SkillBG/");
      AssetPath.mSB.Append(sceneName);
      return AssetPath.mSB.ToString();
    }

    public static string UnitVoiceFileName(UnitParam unit, ArtifactParam artifact = null, string jobVoice = "")
    {
      if (string.IsNullOrEmpty(unit.voice))
        return unit.voice;
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append(unit.voice);
      if (artifact != null && !string.IsNullOrEmpty(artifact.voice))
      {
        AssetPath.mSB.Append('_');
        AssetPath.mSB.Append(artifact.voice);
      }
      else if (!string.IsNullOrEmpty(jobVoice))
      {
        AssetPath.mSB.Append('_');
        AssetPath.mSB.Append(jobVoice);
      }
      return AssetPath.mSB.ToString();
    }

    public static string ArtifactIcon(ArtifactParam arti)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("ArtiIcon/");
      AssetPath.mSB.Append(arti.icon);
      return AssetPath.mSB.ToString();
    }

    public static string BundleIcon(string iconName)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("Bundle/");
      AssetPath.mSB.Append(iconName);
      return AssetPath.mSB.ToString();
    }
  }
}
