namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;

    public class ReqBtlComGps : WebAPI
    {
        public unsafe ReqBtlComGps(Network.ResponseCallback response, Vector2 location, bool isMulti)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "btl/com/areaquest";
            builder.Append("\"location\":{");
            builder.Append("\"lat\":" + ((float) &location.x) + ",");
            builder.Append("\"lng\":" + ((float) &location.y));
            builder.Append("}");
            builder.Append(",");
            builder.Append("\"is_multi\":");
            builder.Append((isMulti == null) ? 0 : 1);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

