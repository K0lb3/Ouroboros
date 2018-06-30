namespace SRPG
{
    using System;
    using System.Text;

    public class HomeApi : WebAPI
    {
        public HomeApi(bool isMultiPush, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "home";
            if (isMultiPush == null)
            {
                goto Label_003F;
            }
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"is_multi_push\":1");
            base.body = WebAPI.GetRequestString(builder.ToString());
            goto Label_004B;
        Label_003F:
            base.body = WebAPI.GetRequestString(null);
        Label_004B:
            base.callback = response;
            return;
        }
    }
}

