// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMasterParam : WebAPI
  {
    public ReqMasterParam(Network.ResponseCallback response)
    {
      this.name = "mst/10/master";
      this.callback = response;
    }
  }
}
