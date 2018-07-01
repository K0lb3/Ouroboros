// Decompiled with JetBrains decompiler
// Type: SRPG.VersusEnableTimeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class VersusEnableTimeParam
  {
    private int mScheduleId;
    private VERSUS_TYPE mVersusType;
    private DateTime mBeginAt;
    private DateTime mEndAt;
    private List<VersusEnableTimeScheduleParam> mSchedule;
    private VersusDraftType mDraftType;

    public int ScheduleId
    {
      get
      {
        return this.mScheduleId;
      }
    }

    public VERSUS_TYPE VersusType
    {
      get
      {
        return this.mVersusType;
      }
    }

    public DateTime BeginAt
    {
      get
      {
        return this.mBeginAt;
      }
    }

    public DateTime EndAt
    {
      get
      {
        return this.mEndAt;
      }
    }

    public List<VersusEnableTimeScheduleParam> Schedule
    {
      get
      {
        return this.mSchedule;
      }
    }

    public VersusDraftType DraftType
    {
      get
      {
        return this.mDraftType;
      }
    }

    public bool Deserialize(JSON_VersusEnableTimeParam json)
    {
      if (json == null)
        return false;
      this.mScheduleId = json.id;
      this.mVersusType = (VERSUS_TYPE) json.mode;
      try
      {
        if (!string.IsNullOrEmpty(json.begin_at))
          this.mBeginAt = DateTime.Parse(json.begin_at);
        if (!string.IsNullOrEmpty(json.end_at))
          this.mEndAt = DateTime.Parse(json.end_at);
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.Message);
        return false;
      }
      this.mSchedule = new List<VersusEnableTimeScheduleParam>();
      for (int index = 0; index < json.schedule.Length; ++index)
      {
        VersusEnableTimeScheduleParam timeScheduleParam = new VersusEnableTimeScheduleParam();
        if (timeScheduleParam.Deserialize(json.schedule[index]))
          this.mSchedule.Add(timeScheduleParam);
      }
      this.mDraftType = (VersusDraftType) json.draft_type;
      return true;
    }
  }
}
