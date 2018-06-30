namespace SRPG
{
    using System;
    using System.Text;

    public class ReqGoogleReview : WebAPI
    {
        public ReqGoogleReview(Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "serial/register/greview";
            builder = WebAPI.GetStringBuilder();
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

