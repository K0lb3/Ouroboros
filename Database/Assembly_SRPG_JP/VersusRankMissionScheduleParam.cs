// Decompiled with JetBrains decompiler
// Type: SRPG.VersusRankMissionScheduleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusRankMissionScheduleParam
  {
    private int mScheduleId;
    private string mIName;

    public int ScheduleId
    {
      get
      {
        return this.mScheduleId;
      }
    }

    public string IName
    {
      get
      {
        return this.mIName;
      }
    }

    public bool Deserialize(JSON_VersusRankMissionScheduleParam json)
    {
      if (json == null)
        return false;
      this.mScheduleId = json.schedule_id;
      this.mIName = json.iname;
      return true;
    }
  }
}
