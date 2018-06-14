// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusStatus : WebAPI
  {
    public ReqVersusStatus(Network.ResponseCallback response)
    {
      this.name = "vs/status";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public int floor;
      public int key;
      public int wincnt;
      public int is_season_gift;
      public int is_give_season_gift;
      public long end_at;
      public string vstower_id;
      public string tower_iname;
      public string last_enemyuid;
      public int daycnt;
    }
  }
}
