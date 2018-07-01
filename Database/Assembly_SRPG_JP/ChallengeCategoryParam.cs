// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeCategoryParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ChallengeCategoryParam
  {
    public TimeParser begin_at = new TimeParser();
    public TimeParser end_at = new TimeParser();
    public string iname;
    public int prio;

    public int Priority
    {
      get
      {
        return this.prio;
      }
    }

    public bool Deserialize(JSON_ChallengeCategoryParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.begin_at.Set(json.begin_at, DateTime.MinValue);
      this.end_at.Set(json.end_at, DateTime.MaxValue);
      this.prio = json.prio;
      return true;
    }

    public bool IsAvailablePeriod(DateTime now)
    {
      DateTime dateTimes1 = this.begin_at.DateTimes;
      DateTime dateTimes2 = this.end_at.DateTimes;
      if (now >= dateTimes1)
        return dateTimes2 >= now;
      return false;
    }
  }
}
