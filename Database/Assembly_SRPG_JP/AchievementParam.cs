// Decompiled with JetBrains decompiler
// Type: SRPG.AchievementParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class AchievementParam
  {
    public int id;
    public string iname;
    public string ios;
    public string googleplay;

    public bool Deserialize(JSON_AchievementParam json)
    {
      if (json == null)
        return false;
      this.id = json.fields.id;
      this.iname = json.fields.iname;
      this.ios = json.fields.ios;
      this.googleplay = json.fields.googleplay;
      return true;
    }

    public string AchievementID
    {
      get
      {
        return string.Empty;
      }
    }
  }
}
