namespace SRPG
{
    using System;
    using System.Text;

    public class ReqMail : WebAPI
    {
        public ReqMail(int page, bool isPeriod, bool isRead, Network.ResponseCallback response)
        {
            StringBuilder builder;
            base..ctor();
            base.name = "mail";
            builder = WebAPI.GetStringBuilder();
            builder.Append(this.MakeKeyValue("page", page));
            builder.Append(",");
            builder.Append(this.MakeKeyValue("isPeriod", (isPeriod == null) ? 0 : 1));
            builder.Append(",");
            builder.Append(this.MakeKeyValue("isRead", (isRead == null) ? 0 : 1));
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        private string MakeKeyValue(string key, int value)
        {
            return string.Format("\"{0}\":{1}", key, (int) value);
        }

        private string MakeKeyValue(string key, long value)
        {
            return string.Format("\"{0}\":{1}", key, (long) value);
        }

        private string MakeKeyValue(string key, float value)
        {
            return string.Format("\"{0}\":{1}", key, (float) value);
        }

        private string MakeKeyValue(string key, string value)
        {
            return string.Format("\"{0}\":\"{1}\"", key, value);
        }
    }
}

