// Decompiled with JetBrains decompiler
// Type: SRPG.PlayerParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class PlayerParam
  {
    public OInt pt;
    public OInt ucap;
    public OInt icap;
    public OInt ecap;
    public OInt fcap;

    public bool Deserialize(JSON_PlayerParam json)
    {
      if (json == null)
        return false;
      this.pt = (OInt) json.pt;
      this.ucap = (OInt) json.ucap;
      this.icap = (OInt) json.icap;
      this.ecap = (OInt) json.ecap;
      this.fcap = (OInt) json.fcap;
      return true;
    }
  }
}
