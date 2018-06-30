namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReqOrdealPartyUpdate : WebAPI
    {
        public unsafe ReqOrdealPartyUpdate(Network.ResponseCallback response, List<PartyEditData> parties)
        {
            PartyData data;
            StringBuilder builder;
            int num;
            PartyEditData data2;
            List<PartyEditData>.Enumerator enumerator;
            int num2;
            string str;
            base..ctor();
            data = MonoSingleton<GameManager>.Instance.Player.Partys[9];
            base.name = "party2/ordeal/update";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"parties\":[");
            num = 0;
            builder.Append("{\"units\":[");
            enumerator = parties.GetEnumerator();
        Label_0050:
            try
            {
                goto Label_00E6;
            Label_0055:
                data2 = &enumerator.Current;
                if (num <= 0)
                {
                    goto Label_006D;
                }
                builder.Append(0x2c);
            Label_006D:
                builder.Append(0x5b);
                num2 = 0;
                goto Label_00BD;
            Label_007E:
                if (data2.Units[num2] != null)
                {
                    goto Label_0091;
                }
                goto Label_00D9;
            Label_0091:
                if (num2 <= 0)
                {
                    goto Label_00A2;
                }
                builder.Append(0x2c);
            Label_00A2:
                builder.Append(data2.Units[num2].UniqueID);
                num2 += 1;
            Label_00BD:
                if (num2 >= data.MAX_UNIT)
                {
                    goto Label_00D9;
                }
                if (num2 < ((int) data2.Units.Length))
                {
                    goto Label_007E;
                }
            Label_00D9:
                builder.Append(0x5d);
                num += 1;
            Label_00E6:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0055;
                }
                goto Label_0104;
            }
            finally
            {
            Label_00F7:
                ((List<PartyEditData>.Enumerator) enumerator).Dispose();
            }
        Label_0104:
            builder.Append(0x5d);
            str = PartyData.GetStringFromPartyType(9);
            builder.Append(",\"ptype\":\"");
            builder.Append(str);
            builder.Append(0x22);
            builder.Append(0x7d);
            builder.Append(0x5d);
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

