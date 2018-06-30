namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;

    public class ReqLogin : WebAPI
    {
        public ReqLogin(Network.ResponseCallback response)
        {
            StringBuilder builder;
            string str;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"device\":\"");
            builder.Append(SystemInfo.get_deviceModel());
            builder.Append("\",");
            str = AssetManager_Extensions.ToPath(AssetManager.Format).Replace("/", string.Empty);
            builder.Append("\"dlc\":\"");
            builder.Append(str);
            builder.Append("\"");
            base.name = "login";
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

