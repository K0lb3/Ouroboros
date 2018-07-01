// Decompiled with JetBrains decompiler
// Type: SRPG.ReqQuestBookmark
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqQuestBookmark : WebAPI
  {
    public ReqQuestBookmark(Network.ResponseCallback response)
    {
      this.name = "quest/favorite";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }
  }
}
