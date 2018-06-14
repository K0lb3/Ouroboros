// Decompiled with JetBrains decompiler
// Type: SRPG.MultiFuid
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class MultiFuid
  {
    public string fuid;
    public string status;

    public bool Deserialize(Json_MultiFuids json)
    {
      this.fuid = json.fuid;
      this.status = json.status;
      return true;
    }
  }
}
