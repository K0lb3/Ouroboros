// Decompiled with JetBrains decompiler
// Type: SRPG.TimeParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class TimeParser
  {
    private string str_time;
    private DateTime date_time;

    public void Set(string str_time_at, DateTime base_time)
    {
      this.str_time = str_time_at;
      this.date_time = base_time;
      if (string.IsNullOrEmpty(this.str_time))
        return;
      try
      {
        this.date_time = DateTime.Parse(this.str_time);
      }
      catch (Exception ex)
      {
        DebugUtility.LogWarning("Failed to parse date! [" + this.str_time + "]");
      }
    }

    public string StrTime
    {
      get
      {
        return this.str_time;
      }
    }

    public DateTime DateTimes
    {
      get
      {
        return this.date_time;
      }
    }
  }
}
