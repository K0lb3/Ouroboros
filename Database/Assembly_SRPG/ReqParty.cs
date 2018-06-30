namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqParty : WebAPI
    {
        public ReqParty(Network.ResponseCallback response, bool needUpdateMultiRoom, bool ignoreEmpty, bool needUpdateMultiRoomMT)
        {
            List<PartyData> list;
            StringBuilder builder;
            int num;
            int num2;
            int num3;
            string str;
            base..ctor();
            list = MonoSingleton<GameManager>.Instance.Player.Partys;
            base.name = "party2";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"parties\":[");
            num = 0;
            num2 = 0;
            goto Label_0108;
        Label_003C:
            if (ignoreEmpty == null)
            {
                goto Label_0058;
            }
            if (list[num2].Num != null)
            {
                goto Label_0058;
            }
            goto Label_0104;
        Label_0058:
            if (num2 != 9)
            {
                goto Label_0065;
            }
            goto Label_0104;
        Label_0065:
            if (num <= 0)
            {
                goto Label_0075;
            }
            builder.Append(0x2c);
        Label_0075:
            builder.Append("{\"units\":[");
            num3 = 0;
            goto Label_00B5;
        Label_0089:
            if (num3 <= 0)
            {
                goto Label_009A;
            }
            builder.Append(0x2c);
        Label_009A:
            builder.Append(list[num2].GetUnitUniqueID(num3));
            num3 += 1;
        Label_00B5:
            if (num3 < list[num2].MAX_UNIT)
            {
                goto Label_0089;
            }
            builder.Append(0x5d);
            str = PartyData.GetStringFromPartyType(num2);
            builder.Append(",\"ptype\":\"");
            builder.Append(str);
            builder.Append(0x22);
            builder.Append(0x7d);
            num += 1;
        Label_0104:
            num2 += 1;
        Label_0108:
            if (num2 < list.Count)
            {
                goto Label_003C;
            }
            builder.Append(0x5d);
            if (needUpdateMultiRoom == null)
            {
                goto Label_013E;
            }
            builder.Append(",\"roomowner\":1");
            DebugUtility.Log("UpdateMulti!");
            goto Label_015B;
        Label_013E:
            if (needUpdateMultiRoomMT == null)
            {
                goto Label_015B;
            }
            builder.Append(",\"roomowner_mt\":1");
            DebugUtility.Log("UpdateMultiTower!");
        Label_015B:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

