// Decompiled with JetBrains decompiler
// Type: SRPG.VersusScheduleParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusScheduleParam
  {
    public string iname;
    public string begin_at;
    public string end_at;
    public string gift_begin_at;
    public string gift_end_at;

    public void Deserialize(JSON_VersusSchedule json)
    {
      if (json == null)
        return;
      this.iname = json.iname;
      this.begin_at = json.begin_at;
      this.end_at = json.end_at;
      this.gift_begin_at = json.gift_begin_at;
      this.gift_end_at = json.gift_end_at;
    }
  }
}
