namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqVersusEnd : WebAPI
    {
        public unsafe ReqVersusEnd(long btlid, BtlResultTypes result, string uid, string fuid, uint turn, int[] myhp, int[] enhp, int atk, int dmg, int heal, int beat, int[] place, Network.ResponseCallback response, VERSUS_TYPE type, string trophyprog, string bingoprog)
        {
            StringBuilder builder;
            int num;
            int num2;
            int num3;
            BtlResultTypes types;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("vs/");
            builder.Append(((VERSUS_TYPE) type).ToString().ToLower());
            builder.Append("match/end");
            base.name = builder.ToString();
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
                    goto Label_00A7;

                case 1:
                    goto Label_00B8;

                case 2:
                    goto Label_00C9;

                case 3:
                    goto Label_00DA;

                case 4:
                    goto Label_00EB;
            }
            goto Label_00FC;
        Label_00A7:
            builder.Append("win");
            goto Label_00FC;
        Label_00B8:
            builder.Append("lose");
            goto Label_00FC;
        Label_00C9:
            builder.Append("retire");
            goto Label_00FC;
        Label_00DA:
            builder.Append("cancel");
            goto Label_00FC;
        Label_00EB:
            builder.Append("draw");
        Label_00FC:
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
                goto Label_025B;
            }
            builder.Append(0x2c);
            builder.Append("\"myhp\":[");
            num = 0;
            goto Label_0245;
        Label_021D:
            if (num <= 0)
            {
                goto Label_022D;
            }
            builder.Append(0x2c);
        Label_022D:
            builder.Append(&(myhp[num]).ToString());
            num += 1;
        Label_0245:
            if (num < ((int) myhp.Length))
            {
                goto Label_021D;
            }
            builder.Append("]");
        Label_025B:
            if (enhp == null)
            {
                goto Label_02BC;
            }
            builder.Append(0x2c);
            builder.Append("\"enhp\":[");
            num2 = 0;
            goto Label_02A6;
        Label_027E:
            if (num2 <= 0)
            {
                goto Label_028E;
            }
            builder.Append(0x2c);
        Label_028E:
            builder.Append(&(enhp[num2]).ToString());
            num2 += 1;
        Label_02A6:
            if (num2 < ((int) enhp.Length))
            {
                goto Label_027E;
            }
            builder.Append("]");
        Label_02BC:
            if (place == null)
            {
                goto Label_031D;
            }
            builder.Append(0x2c);
            builder.Append("\"place\":[");
            num3 = 0;
            goto Label_0307;
        Label_02DF:
            if (num3 <= 0)
            {
                goto Label_02EF;
            }
            builder.Append(0x2c);
        Label_02EF:
            builder.Append(&(place[num3]).ToString());
            num3 += 1;
        Label_0307:
            if (num3 < ((int) place.Length))
            {
                goto Label_02DF;
            }
            builder.Append("]");
        Label_031D:
            if (builder[builder.Length - 1] != 0x2c)
            {
                goto Label_0340;
            }
            builder.Length -= 1;
        Label_0340:
            builder.Append("},");
            builder.Append("\"uid\":\"");
            builder.Append(uid);
            builder.Append("\",");
            builder.Append("\"fuid\":\"");
            builder.Append(fuid);
            builder.Append("\"");
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_03AE;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_03AE:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_03CF;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_03CF:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

