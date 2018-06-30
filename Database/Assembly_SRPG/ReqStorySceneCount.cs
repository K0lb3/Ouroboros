namespace SRPG
{
    using System;
    using System.Text;

    public class ReqStorySceneCount : WebAPI
    {
        public ReqStorySceneCount(string iname, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "story/scene/count";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"iname\":\"");
            builder.Append(iname);
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

