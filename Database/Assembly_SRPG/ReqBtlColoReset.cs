namespace SRPG
{
    using System;
    using System.Text;

    public class ReqBtlColoReset : WebAPI
    {
        public ReqBtlColoReset(ColoResetTypes reset, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "btl/colo/reset/" + ((ColoResetTypes) reset).ToString();
            builder = WebAPI.GetStringBuilder();
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

