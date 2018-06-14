// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactFavorite
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqArtifactFavorite : WebAPI
  {
    public ReqArtifactFavorite(long iid, bool isFavorite, Network.ResponseCallback response)
    {
      this.name = "unit/job/artifact/favorite";
      this.body = WebAPI.GetRequestString("\"iid\":" + (object) iid + ",\"fav\":" + (object) (!isFavorite ? 0 : 1));
      this.callback = response;
    }
  }
}
