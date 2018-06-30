namespace SRPG
{
    using GR;
    using System;
    using System.Text;

    public class ReqBtlComRecord : WebAPI
    {
        public ReqBtlComRecord(string questIname, int page, int id, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "btl/com/record";
            builder = new StringBuilder();
            builder.Append("\"iname\":\"" + questIname + "\"");
            if (page <= 1)
            {
                goto Label_0063;
            }
            builder.Append(",\"id\":" + ((int) id));
            builder.Append(",\"page\":" + ((int) page));
        Label_0063:
            base.body = CreateRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        private static string CreateRequestString(string body)
        {
            StringBuilder builder;
            builder = new StringBuilder();
            builder.Append("{\"ticket\":" + ((int) Network.TicketID) + ",");
            builder.Append("\"access_token\":\"" + Network.SessionID + "\",");
            builder.Append("\"device_id\":\"" + MonoSingleton<GameManager>.Instance.DeviceId + "\"");
            if (string.IsNullOrEmpty(body) != null)
            {
                goto Label_0083;
            }
            builder.Append(",\"param\":{" + body + "}");
        Label_0083:
            builder.Append("}");
            return builder.ToString();
        }
    }
}

