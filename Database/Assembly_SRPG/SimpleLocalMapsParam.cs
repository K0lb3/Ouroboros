// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleLocalMapsParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
