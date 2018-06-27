// Decompiled with JetBrains decompiler
// Type: SRPG.ReqArtifactFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
