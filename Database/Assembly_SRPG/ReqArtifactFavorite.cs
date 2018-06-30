namespace SRPG
{
    using System;

    public class ReqArtifactFavorite : WebAPI
    {
        public ReqArtifactFavorite(long iid, bool isFavorite, Network.ResponseCallback response)
        {
            object[] objArray1;
            base..ctor();
            base.name = "unit/job/artifact/favorite";
            objArray1 = new object[] { "\"iid\":", (long) iid, ",\"fav\":", (int) ((isFavorite == null) ? 0 : 1) };
            base.body = WebAPI.GetRequestString(string.Concat(objArray1));
            base.callback = response;
            return;
        }
    }
}

