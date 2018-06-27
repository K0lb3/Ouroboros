// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class MapEffectParam
  {
    private List<string> mValidSkillLists = new List<string>();
    private string localizedNameID;
    private string localizedExprID;
    private int mIndex;
    private string mIname;
    private string mName;
    private string mExpr;
    private static int CurrentIndex;
    private static Dictionary<string, List<JobParam>> mHaveJobDict;

    protected void localizeFields(string language)
    {
      this.init();
      this.mName = LocalizedText.SGGet(language, GameUtility.LocalizedQuestParamFileName, this.localizedNameID);
    }

    protected void init()
    {
      this.localizedNameID = this.GetType().GenerateLocalizedID(this.mIname, "NAME");
    }

    public void Deserialize(string language, JSON_MapEffectParam json)
    {
      this.Deserialize(json);
      this.localizeFields(language);
    }

    public int Index
    {
      get
      {
        return this.mIndex;
      }
    }

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
    }

    public string Expr
    {
      get
      {
        return this.mExpr;
      }
    }

    public List<string> ValidSkillLists
    {
      get
      {
        return this.mValidSkillLists;
      }
    }

    public void Deserialize(JSON_MapEffectParam json)
    {
      if (json == null)
        return;
      this.mIndex = ++MapEffectParam.CurrentIndex;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mExpr = json.expr;
      this.mValidSkillLists.Clear();
      if (json.skills == null)
        return;
      foreach (string skill in json.skills)
        this.mValidSkillLists.Add(skill);
    }

    public bool IsValidSkill(string skill)
    {
      if (string.IsNullOrEmpty(skill))
        return false;
      return this.mValidSkillLists.Contains(skill);
    }

    public static List<JobParam> GetHaveJobLists(string skill_iname)
    {
      List<JobParam> jobParamList = new List<JobParam>();
      if (string.IsNullOrEmpty(skill_iname) || MapEffectParam.mHaveJobDict == null || !MapEffectParam.mHaveJobDict.ContainsKey(skill_iname))
        return jobParamList;
      jobParamList = MapEffectParam.mHaveJobDict[skill_iname];
      return jobParamList;
    }

    public static List<MapEffectParam> GetHaveMapEffectLists(string skill_iname)
    {
      List<MapEffectParam> mapEffectParamList = new List<MapEffectParam>();
      if (string.IsNullOrEmpty(skill_iname))
        return mapEffectParamList;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!Object.op_Implicit((Object) instanceDirect) || instanceDirect.MapEffectParam == null)
        return mapEffectParamList;
      using (List<MapEffectParam>.Enumerator enumerator = instanceDirect.MapEffectParam.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MapEffectParam current = enumerator.Current;
          if (current.ValidSkillLists.Contains(skill_iname))
            mapEffectParamList.Add(current);
        }
      }
      return mapEffectParamList;
    }

    public static bool IsMakeHaveJobLists()
    {
      return MapEffectParam.mHaveJobDict != null;
    }

    public static void MakeHaveJobLists()
    {
      MapEffectParam.mHaveJobDict = new Dictionary<string, List<JobParam>>();
    }

    public static void AddHaveJob(string skill_iname, JobParam job_param)
    {
      if (MapEffectParam.mHaveJobDict == null)
        MapEffectParam.MakeHaveJobLists();
      if (!MapEffectParam.mHaveJobDict.ContainsKey(skill_iname))
      {
        MapEffectParam.mHaveJobDict.Add(skill_iname, new List<JobParam>((IEnumerable<JobParam>) new JobParam[1]
        {
          job_param
        }));
      }
      else
      {
        if (MapEffectParam.mHaveJobDict[skill_iname].Contains(job_param))
          return;
        MapEffectParam.mHaveJobDict[skill_iname].Add(job_param);
      }
    }
  }
}
