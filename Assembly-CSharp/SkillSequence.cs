// Decompiled with JetBrains decompiler
// Type: SkillSequence
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkillSequence
{
  public bool InterpSkillCamera = true;
  private static List<SkillSequence> mSequences;
  public string Name;
  public int NameHash;
  public SkillSequence.AnimationSettings ChantAnimation;
  public SkillSequence.AnimationSettings SkillAnimation;
  public SkillSequence.AnimationSettings EndAnimation;
  public string ChantCameraClipName;
  public string StartAnimation;
  public bool InterpChantCamera;
  public string MainCameraClipName;
  public string EndCameraClipName;
  public float EndLength;
  public float ProjectileHitDelay;
  [Description("スキルの種類")]
  public SkillSequence.SkillTypes SkillType;
  [Description("スキル使用時の振り向きの種類")]
  public SkillSequence.SkillTurnTypes SkillTurnType;
  [Description("MAPで行うスキル演出に対するカメラ動作")]
  public SkillSequence.MapCameraTypes MapCameraType;
  public bool NotMirror;

  public SkillSequence()
  {
    this.Name = string.Empty;
    this.NameHash = 0;
    this.ChantAnimation.Name = string.Empty;
    this.ChantAnimation.UseCamera = false;
    this.SkillAnimation.Name = string.Empty;
    this.SkillAnimation.UseCamera = false;
    this.EndAnimation.Name = string.Empty;
    this.EndAnimation.UseCamera = false;
    this.ChantCameraClipName = string.Empty;
    this.InterpChantCamera = false;
    this.MainCameraClipName = string.Empty;
    this.InterpSkillCamera = false;
    this.EndCameraClipName = string.Empty;
    this.EndLength = 0.0f;
    this.ProjectileHitDelay = 0.0f;
    this.SkillType = SkillSequence.SkillTypes.Melee;
    this.SkillTurnType = SkillSequence.SkillTurnTypes.None;
    this.MapCameraType = SkillSequence.MapCameraTypes.None;
    this.StartAnimation = string.Empty;
    this.NotMirror = false;
  }

  private static void OnApplicationQuit()
  {
    SkillSequence.mSequences = (List<SkillSequence>) null;
  }

  public static void UnloadAll()
  {
    SkillSequence.mSequences = (List<SkillSequence>) null;
  }

  private static void LoadSequences()
  {
    if (!Application.get_isPlaying() || SkillSequence.mSequences != null)
      return;
    SkillSequence.mSequences = new List<SkillSequence>(512);
    string s = AssetManager.LoadTextData("SkillSeq/SKILLSEQ");
    if (s == null)
      return;
    using (StringReader stringReader = new StringReader(s))
    {
      string[] array = stringReader.ReadLine().Split('\t');
      int index1 = Array.IndexOf<string>(array, "ID");
      int index2 = Array.IndexOf<string>(array, "CHANT");
      int index3 = Array.IndexOf<string>(array, "CHANTCAM");
      int index4 = Array.IndexOf<string>(array, "SKILL");
      int index5 = Array.IndexOf<string>(array, "SKILLCAM");
      int index6 = Array.IndexOf<string>(array, "END");
      int index7 = Array.IndexOf<string>(array, "ENDCAM");
      int index8 = Array.IndexOf<string>(array, "CHANTCAMCLIP");
      int index9 = Array.IndexOf<string>(array, "INTERPCHANTCAM");
      int index10 = Array.IndexOf<string>(array, "SKILLCAMCLIP");
      int index11 = Array.IndexOf<string>(array, "INTERPSKILLCAM");
      int index12 = Array.IndexOf<string>(array, "ENDCAMCLIP");
      int index13 = Array.IndexOf<string>(array, "ENDLENGTH");
      int index14 = Array.IndexOf<string>(array, "HITDELAY");
      int index15 = Array.IndexOf<string>(array, "TYPE");
      int index16 = Array.IndexOf<string>(array, "TURNTYPE");
      int index17 = Array.IndexOf<string>(array, "PREPARE");
      int index18 = Array.IndexOf<string>(array, "MAPCAMTYPE");
      int index19 = Array.IndexOf<string>(array, "NOTMIRROR");
      string str;
      while ((str = stringReader.ReadLine()) != null)
      {
        string[] strArray = str.Split('\t');
        if (!string.IsNullOrEmpty(strArray[0]) && strArray.Length > 1)
        {
          SkillSequence skillSequence = new SkillSequence();
          if (index1 >= 0)
          {
            skillSequence.Name = strArray[index1];
            skillSequence.NameHash = skillSequence.Name.GetHashCode();
          }
          if (index2 >= 0)
            skillSequence.ChantAnimation.Name = strArray[index2];
          if (index3 >= 0)
            skillSequence.ChantAnimation.UseCamera = SkillSequence.ParseBool(strArray[index3]);
          if (index4 >= 0)
            skillSequence.SkillAnimation.Name = strArray[index4];
          if (index5 >= 0)
            skillSequence.SkillAnimation.UseCamera = SkillSequence.ParseBool(strArray[index5]);
          if (index6 >= 0)
            skillSequence.EndAnimation.Name = strArray[index6];
          if (index7 >= 0)
            skillSequence.EndAnimation.UseCamera = SkillSequence.ParseBool(strArray[index7]);
          if (index8 >= 0)
            skillSequence.ChantCameraClipName = strArray[index8];
          if (index9 >= 0)
            skillSequence.InterpChantCamera = SkillSequence.ParseBool(strArray[index9]);
          if (index10 >= 0)
            skillSequence.MainCameraClipName = strArray[index10];
          if (index11 >= 0)
            skillSequence.InterpSkillCamera = SkillSequence.ParseBool(strArray[index11]);
          if (index12 >= 0)
            skillSequence.EndCameraClipName = strArray[index12];
          if (index13 >= 0)
            skillSequence.EndLength = SkillSequence.ParseFloat(strArray[index13]);
          if (index14 >= 0)
            skillSequence.ProjectileHitDelay = SkillSequence.ParseFloat(strArray[index14]);
          if (index15 >= 0)
            skillSequence.SkillType = (SkillSequence.SkillTypes) Enum.Parse(typeof (SkillSequence.SkillTypes), strArray[index15]);
          if (index16 >= 0)
            skillSequence.SkillTurnType = (SkillSequence.SkillTurnTypes) Enum.Parse(typeof (SkillSequence.SkillTurnTypes), strArray[index16]);
          if (index18 >= 0)
            skillSequence.MapCameraType = (SkillSequence.MapCameraTypes) Enum.Parse(typeof (SkillSequence.MapCameraTypes), !(strArray[index18] == string.Empty) ? strArray[index18] : "0");
          if (index19 >= 0)
            skillSequence.NotMirror = SkillSequence.ParseBool(strArray[index19]);
          if (index17 >= 0)
            skillSequence.StartAnimation = strArray[index17];
          SkillSequence.mSequences.Add(skillSequence);
        }
      }
    }
  }

  private static float ParseFloat(string s)
  {
    float result;
    if (!float.TryParse(s, out result))
      result = 0.0f;
    return result;
  }

  private static bool ParseBool(string s)
  {
    if (string.IsNullOrEmpty(s))
      return false;
    int result;
    if (!int.TryParse(s, out result))
      result = 0;
    return result != 0;
  }

  public static SkillSequence FindSequence(string name)
  {
    SkillSequence.LoadSequences();
    int hashCode = name.GetHashCode();
    for (int index = 0; index < SkillSequence.mSequences.Count; ++index)
    {
      if (SkillSequence.mSequences[index].NameHash == hashCode && SkillSequence.mSequences[index].Name == name)
        return SkillSequence.mSequences[index];
    }
    return (SkillSequence) null;
  }

  [Serializable]
  public struct AnimationSettings
  {
    public string Name;
    public bool UseCamera;
  }

  public enum MapCameraTypes
  {
    None,
    FirstTargetCenter,
    FarDistance,
    MoreFarDistance,
  }

  public enum SkillTypes
  {
    Melee,
    Ranged,
    RangedRayNoMovCmr,
    RangedRayFirstMovCmr,
    RangedRayLastMovCmr,
  }

  public enum SkillTurnTypes
  {
    None,
    Target,
    Axis,
  }
}
