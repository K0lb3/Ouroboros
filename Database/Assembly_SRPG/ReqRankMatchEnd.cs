namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqRankMatchEnd : WebAPI
    {
        public unsafe ReqRankMatchEnd(long btlid, BtlResultTypes result, string uid, string fuid, uint turn, int[] myhp, int[] enhp, int atk, int dmg, int heal, int beat, int[] place, Network.ResponseCallback response, string trophyprog, string bingoprog, string missionprog)
        {
            StringBuilder builder;
            int num;
            int num2;
            int num3;
            BtlResultTypes types;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "vs/rankmatch/end";
            builder.Length = 0;
            builder.Append("\"btlid\":");
            builder.Append(btlid);
            builder.Append(0x2c);
            builder.Append("\"btlendparam\":{");
            builder.Append("\"result\":\"");
            types = result;
            switch (types)
            {
                case 0:
                    goto Label_0076;

                case 1:
                    goto Label_0087;

                case 2:
                    goto Label_0098;

                case 3:
                    goto Label_00A9;

                case 4:
                    goto Label_00BA;
            }
            goto Label_00CB;
        Label_0076:
            builder.Append("win");
            goto Label_00CB;
        Label_0087:
            builder.Append("lose");
            goto Label_00CB;
        Label_0098:
            builder.Append("retire");
            goto Label_00CB;
        Label_00A9:
            builder.Append("cancel");
            goto Label_00CB;
        Label_00BA:
            builder.Append("draw");
        Label_00CB:
            builder.Append("\",");
            builder.Append("\"token\":\"");
            builder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
            builder.Append("\",");
            builder.Append("\"turn\":\"");
            builder.Append(turn);
            builder.Append("\"");
            builder.Append(",");
            builder.Append("\"atk\":\"");
            builder.Append(atk);
            builder.Append("\"");
            builder.Append(",");
            builder.Append("\"dmg\":\"");
            builder.Append(dmg);
            builder.Append("\"");
            builder.Append(",");
            builder.Append("\"heal\":\"");
            builder.Append(heal);
            builder.Append("\"");
            builder.Append(",");
            builder.Append("\"beatcnt\":");
            builder.Append(beat);
            if (myhp == null)
            {
                goto Label_022A;
            }
            builder.Append(0x2c);
            builder.Append("\"myhp\":[");
            num = 0;
            goto Label_0214;
        Label_01EC:
            if (num <= 0)
            {
                goto Label_01FC;
            }
            builder.Append(0x2c);
        Label_01FC:
            builder.Append(&(myhp[num]).ToString());
            num += 1;
        Label_0214:
            if (num < ((int) myhp.Length))
            {
                goto Label_01EC;
            }
            builder.Append("]");
        Label_022A:
            if (enhp == null)
            {
                goto Label_028B;
            }
            builder.Append(0x2c);
            builder.Append("\"enhp\":[");
            num2 = 0;
            goto Label_0275;
        Label_024D:
            if (num2 <= 0)
            {
                goto Label_025D;
            }
            builder.Append(0x2c);
        Label_025D:
            builder.Append(&(enhp[num2]).ToString());
            num2 += 1;
        Label_0275:
            if (num2 < ((int) enhp.Length))
            {
                goto Label_024D;
            }
            builder.Append("]");
        Label_028B:
            if (place == null)
            {
                goto Label_02EC;
            }
            builder.Append(0x2c);
            builder.Append("\"place\":[");
            num3 = 0;
            goto Label_02D6;
        Label_02AE:
            if (num3 <= 0)
            {
                goto Label_02BE;
            }
            builder.Append(0x2c);
        Label_02BE:
            builder.Append(&(place[num3]).ToString());
            num3 += 1;
        Label_02D6:
            if (num3 < ((int) place.Length))
            {
                goto Label_02AE;
            }
            builder.Append("]");
        Label_02EC:
            if (builder[builder.Length - 1] != 0x2c)
            {
                goto Label_030F;
            }
            builder.Length -= 1;
        Label_030F:
            builder.Append("},");
            builder.Append("\"uid\":\"");
            builder.Append(uid);
            builder.Append("\",");
            builder.Append("\"fuid\":\"");
            builder.Append(fuid);
            builder.Append("\"");
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_037D;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_037D:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_039E;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_039E:
            if (string.IsNullOrEmpty(missionprog) != null)
            {
                goto Label_03BF;
            }
            builder.Append(",");
            builder.Append(missionprog);
        Label_03BF:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

