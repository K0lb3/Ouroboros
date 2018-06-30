namespace SRPG
{
    using System;
    using System.Text;

    public class ReqFriendBlockApply : WebAPI
    {
        public ReqFriendBlockApply(string[] _friends, string[] _blocks, Network.ResponseCallback _response)
        {
            StringBuilder builder;
            int num;
            int num2;
            base..ctor();
            base.name = "friend/multi/req";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"friends\":[");
            if (_friends == null)
            {
                goto Label_007B;
            }
            if (((int) _friends.Length) <= 0)
            {
                goto Label_007B;
            }
            num = 0;
            goto Label_0072;
        Label_0039:
            if (num <= 0)
            {
                goto Label_004C;
            }
            builder.Append(",");
        Label_004C:
            builder.Append("\"");
            builder.Append(_friends[num]);
            builder.Append("\"");
            num += 1;
        Label_0072:
            if (num < ((int) _friends.Length))
            {
                goto Label_0039;
            }
        Label_007B:
            builder.Append("]");
            builder.Append(",");
            builder.Append("\"blocks\":[");
            if (_blocks == null)
            {
                goto Label_00F7;
            }
            if (((int) _blocks.Length) <= 0)
            {
                goto Label_00F7;
            }
            num2 = 0;
            goto Label_00EE;
        Label_00B5:
            if (num2 <= 0)
            {
                goto Label_00C8;
            }
            builder.Append(",");
        Label_00C8:
            builder.Append("\"");
            builder.Append(_blocks[num2]);
            builder.Append("\"");
            num2 += 1;
        Label_00EE:
            if (num2 < ((int) _blocks.Length))
            {
                goto Label_00B5;
            }
        Label_00F7:
            builder.Append("]");
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = _response;
            return;
        }
    }
}

