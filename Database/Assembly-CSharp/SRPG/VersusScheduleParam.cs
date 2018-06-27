// Decompiled with JetBrains decompiler
// Type: SRPG.VersusScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class VersusScheduleParam
  {
    public string tower_iname;
    public string iname;
    public string begin_at;
    public string end_at;
    public string gift_begin_at;
    public string gift_end_at;
    private DateTime BeginDate;
    private DateTime EndDate;
    private DateTime GiftBeginDate;
    private DateTime GiftEndDate;

    public bool IsOpen
    {
      get
      {
        DateTime serverTime = TimeManager.ServerTime;
        if (this.BeginDate < serverTime)
          return serverTime < this.EndDate;
        return false;
      }
    }

    public bool IsGift
    {
      get
      {
        DateTime serverTime = TimeManager.ServerTime;
        if (this.GiftBeginDate < serverTime)
          return serverTime < this.GiftEndDate;
        return false;
      }
    }

    public void Deserialize(JSON_VersusSchedule json)
    {
      if (json == null)
        return;
      this.tower_iname = json.tower_iname;
      this.iname = json.iname;
      this.begin_at = json.begin_at;
      this.end_at = json.end_at;
      this.gift_begin_at = json.gift_begin_at;
      this.gift_end_at = json.gift_end_at;
      try
      {
        this.BeginDate = DateTime.Parse(this.begin_at);
        this.EndDate = DateTime.Parse(this.end_at);
        this.GiftBeginDate = DateTime.Parse(this.gift_begin_at);
        this.GiftEndDate = DateTime.Parse(this.gift_end_at);
      }
      catch (Exception ex)
      {
        DebugUtility.Log(ex.ToString());
      }
    }
  }
}
