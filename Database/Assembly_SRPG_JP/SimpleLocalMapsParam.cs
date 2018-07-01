// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleLocalMapsParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class SimpleLocalMapsParam
  {
    public string iname;
    public string[] droplist;

    public bool Deserialize(JSON_SimpleLocalMapsParam json)
    {
      this.iname = json.iname;
      this.droplist = json.droplist;
      return true;
    }
  }
}
