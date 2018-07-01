// Decompiled with JetBrains decompiler
// Type: SRPG.AssetPath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
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
      if (!string.IsNullOrEmpty(job.ac2d))
        return job.ac2d;
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
      JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(jobName);
      string str = jobParam == null ? unit.GetJobImage(jobName) : jobParam.unit_image;
      AssetPath.mSB.Append(string.IsNullOrEmpty(str) ? unit.image : str);
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
      JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(jobName);
      string str = jobParam == null ? unit.GetJobImage(jobName) : jobParam.unit_image;
      AssetPath.mSB.Append(string.IsNullOrEmpty(str) ? unit.image : str);
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
      JobParam jobParam = (JobParam) null;
      if (!string.IsNullOrEmpty(jobName))
        jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(jobName);
      string str = jobParam == null ? unit.GetJobImage(jobName) : jobParam.unit_image;
      AssetPath.mSB.Append(string.IsNullOrEmpty(str) ? unit.model : str);
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
      JobParam jobParam = (JobParam) null;
      if (!string.IsNullOrEmpty(jobName))
        jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(jobName);
      string str = jobParam == null ? unit.GetJobImage(jobName) : jobParam.unit_image;
      AssetPath.mSB.Append(string.IsNullOrEmpty(str) ? unit.model : str);
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
      JobParam jobParam = (JobParam) null;
      if (!string.IsNullOrEmpty(jobName))
        jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(jobName);
      string str = jobParam == null ? unit.GetJobImage(jobName) : jobParam.unit_image;
      AssetPath.mSB.Append(string.IsNullOrEmpty(str) ? unit.model : str);
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
        switch (param.type)
        {
          case EItemType.ArtifactPiece:
            if (!string.IsNullOrEmpty(param.icon))
            {
              AssetPath.mSB.Length = 0;
              AssetPath.mSB.Append("ArtiIcon/");
              AssetPath.mSB.Append(param.icon);
              return AssetPath.mSB.ToString();
            }
            break;
          case EItemType.Unit:
            if (!string.IsNullOrEmpty(param.icon))
            {
              UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(param.iname);
              if (unitParam != null)
                return AssetPath.UnitIconSmall(unitParam, unitParam.GetJobId(0));
              break;
            }
            break;
          default:
            return AssetPath.ItemIcon(param.icon);
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

    public static string TrickEffect(string effect_name)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("MapGimmicks/");
      AssetPath.mSB.Append(effect_name);
      return AssetPath.mSB.ToString();
    }

    public static string TrickIconUI(string marker_name)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("PortraitsM/panel");
      if (!string.IsNullOrEmpty(marker_name))
      {
        AssetPath.mSB.Append("_");
        AssetPath.mSB.Append(marker_name);
      }
      return AssetPath.mSB.ToString();
    }

    public static string WeatherIcon(string icon_name)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("Weathers/");
      AssetPath.mSB.Append(icon_name);
      return AssetPath.mSB.ToString();
    }

    public static string WeatherEffect(string effect_name)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("Weathers/");
      AssetPath.mSB.Append(effect_name);
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

    public static string ConceptCardIcon(ConceptCardParam card)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("ConceptCardIcon/");
      AssetPath.mSB.Append(card.icon);
      return AssetPath.mSB.ToString();
    }

    public static string ConceptCard(ConceptCardParam card)
    {
      AssetPath.mSB.Length = 0;
      AssetPath.mSB.Append("ConceptCard/");
      AssetPath.mSB.Append(card.icon);
      return AssetPath.mSB.ToString();
    }
  }
}
