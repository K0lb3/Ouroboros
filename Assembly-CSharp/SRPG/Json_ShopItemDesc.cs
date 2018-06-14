// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ShopItemDesc
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class Json_ShopItemDesc
  {
    public string iname;
    public int num;
    public int maxnum;
    public int boughtnum;

    public bool IsArtifact
    {
      get
      {
        return this.iname.StartsWith("AF_");
      }
    }
  }
}
