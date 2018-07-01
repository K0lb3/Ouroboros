// Decompiled with JetBrains decompiler
// Type: SRPG.UnitUnlockTimeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class UnitUnlockTimeParam
  {
    public string iname;
    public string name;
    public DateTime begin_at;
    public DateTime end_at;

    public bool Deserialize(JSON_UnitUnlockTimeParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      if (!string.IsNullOrEmpty(json.begin_at))
      {
        try
        {
          this.begin_at = DateTime.Parse(json.begin_at);
        }
        catch
        {
          this.begin_at = DateTime.MaxValue;
        }
      }
      if (!string.IsNullOrEmpty(json.end_at))
      {
        try
        {
          this.end_at = DateTime.Parse(json.end_at);
        }
        catch
        {
          this.end_at = DateTime.MinValue;
        }
      }
      return true;
    }
  }
}
