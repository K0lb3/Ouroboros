// Decompiled with JetBrains decompiler
// Type: SRPG.MultiFuid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
