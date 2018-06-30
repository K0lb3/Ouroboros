namespace SRPG
{
    using System;
    using System.Text;

    public class ReqMailSelect : WebAPI
    {
        public ReqMailSelect(string ticketid, type type, Network.ResponseCallback response)
        {
            StringBuilder builder;
            ReqMailSelect.type type2;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "mail/select";
            builder.Append("\"iname\" : \"");
            builder.Append(ticketid);
            builder.Append("\",");
            builder.Append("\"type\" : \"");
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_0060;

                case 1:
                    goto Label_0071;

                case 2:
                    goto Label_0082;

                case 3:
                    goto Label_0093;
            }
            goto Label_00A4;
        Label_0060:
            builder.Append("item");
            goto Label_00A4;
        Label_0071:
            builder.Append("unit");
            goto Label_00A4;
        Label_0082:
            builder.Append("artifact");
            goto Label_00A4;
        Label_0093:
            builder.Append("conceptcard");
        Label_00A4:
            builder.Append("\"");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }

        public enum type : byte
        {
            item = 0,
            unit = 1,
            artifact = 2,
            conceptcard = 3
        }
    }
}

