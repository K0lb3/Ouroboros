// Decompiled with JetBrains decompiler
// Type: SRPG.VersusMatchCondParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
