// Decompiled with JetBrains decompiler
// Type: SRPG.VersusEnableTimeScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class VersusEnableTimeScheduleParam
  {
    private string mBegin;
    private string mOpen;
    private string mQuestIname;
    private List<DateTime> mAddDateList;

    public string Begin
    {
      get
      {
        return this.mBegin;
      }
    }

    public string Open
    {
      get
      {
        return this.mOpen;
      }
    }

    public string QuestIname
    {
      get
      {
        return this.mQuestIname;
      }
    }

    public List<DateTime> AddDateList
    {
      get
      {
        return this.mAddDateList;
      }
    }

    public bool Deserialize(JSON_VersusEnableTimeScheduleParam json)
    {
      this.mBegin = json.begin_time;
      this.mOpen = json.open_time;
      this.mQuestIname = json.quest_iname;
      try
      {
        if (json.add_date != null)
        {
          this.mAddDateList = new List<DateTime>();
          for (int index = 0; index < json.add_date.Length; ++index)
          {
            if (!string.IsNullOrEmpty(json.add_date[index]))
              this.mAddDateList.Add(DateTime.Parse(json.add_date[index]));
          }
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.Message);
        return false;
      }
      return true;
    }
  }
}
