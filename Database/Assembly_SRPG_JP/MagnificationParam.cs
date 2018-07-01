// Decompiled with JetBrains decompiler
// Type: SRPG.MagnificationParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class MagnificationParam
  {
    public string iname;
    public int[] atkMagnifications;

    public void Deserialize(JSON_MagnificationParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.iname = json.iname;
      this.atkMagnifications = json.atk;
    }
  }
}
