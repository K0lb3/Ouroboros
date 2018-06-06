// Decompiled with JetBrains decompiler
// Type: SRPG.LoginInfoParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class LoginInfoParam
  {
    public string iname;
    public string path;
    public LoginInfoParam.SelectScene scene;
    public long begin_at;
    public long end_at;

    public bool Deserialize(JSON_LoginInfoParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.path = json.path;
      this.scene = (LoginInfoParam.SelectScene) json.scene;
      DateTime result1 = DateTime.MinValue;
      DateTime result2 = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out result1);
      if (!string.IsNullOrEmpty(json.end_at))
        DateTime.TryParse(json.end_at, out result2);
      this.begin_at = TimeManager.FromDateTime(result1);
      this.end_at = TimeManager.FromDateTime(result2);
      return true;
    }

    public enum SelectScene : byte
    {
      None,
      Gacha,
      LimitedShop,
      EventQuest,
      TowerQuest,
      BuyShop,
    }
  }
}
