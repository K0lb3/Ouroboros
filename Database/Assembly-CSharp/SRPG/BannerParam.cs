// Decompiled with JetBrains decompiler
// Type: SRPG.BannerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class BannerParam
  {
    public string iname;
    public BannerType type;
    public string sval;
    public string banner;
    public string banr_sprite;
    public string begin_at;
    public string end_at;
    public int priority;

    public bool Deserialize(JSON_BannerParam json)
    {
      if (json == null)
        return false;
      try
      {
        this.iname = json.iname;
        this.type = (BannerType) Enum.Parse(typeof (BannerType), json.type);
        this.sval = json.sval;
        this.banner = json.banr;
        this.banr_sprite = json.banr_sprite;
        this.begin_at = json.begin_at;
        this.end_at = json.end_at;
        this.priority = json.priority > 0 ? json.priority : int.MaxValue;
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
        return false;
      }
      return true;
    }

    public bool IsAvailablePeriod(DateTime now)
    {
      DateTime minValue = DateTime.MinValue;
      DateTime maxValue = DateTime.MaxValue;
      try
      {
        if (!string.IsNullOrEmpty(this.begin_at))
          minValue = DateTime.Parse(this.begin_at);
        if (!string.IsNullOrEmpty(this.end_at))
          maxValue = DateTime.Parse(this.end_at);
      }
      catch
      {
        return false;
      }
      return !(now < minValue) && !(maxValue < now);
    }
  }
}
