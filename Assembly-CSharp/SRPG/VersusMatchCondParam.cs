// Decompiled with JetBrains decompiler
// Type: SRPG.VersusMatchCondParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class VersusMatchCondParam
  {
    public OInt Floor;
    public OInt LvRange;
    public OInt FloorRange;

    public void Deserialize(JSON_VersusMatchCondParam json)
    {
      if (json == null)
        return;
      this.Floor = (OInt) json.floor;
      this.LvRange = (OInt) json.lvrang;
      this.FloorRange = (OInt) json.floorrange;
    }
  }
}
