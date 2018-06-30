namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;

    public class GpsGift : WebAPI
    {
        public unsafe GpsGift(Vector2 location, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "mail/area";
            builder.Append("\"location\":{");
            builder.Append("\"lat\":" + ((float) &location.x) + ",");
            builder.Append("\"lng\":" + ((float) &location.y));
            builder.Append("}");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

