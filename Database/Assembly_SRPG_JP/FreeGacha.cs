// Decompiled with JetBrains decompiler
// Type: SRPG.FreeGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class FreeGacha
  {
    public int num;
    public long at;

    public bool Deserialize(Json_FreeGacha json)
    {
      this.num = json.num;
      this.at = json.at;
      return true;
    }
  }
}
