namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqVersusCpuEnd : WebAPI
    {
        public unsafe ReqVersusCpuEnd(long btlid, BtlResultTypes result, uint turn, int[] myhp, int[] enhp, int atk, int dmg, int heal, int beat, int[] place, Network.ResponseCallback response, string trophyprog, string bingoprog)
        {
            StringBuilder builder;
            int num;
            int num2;
            int num3;
            BtlResultTypes types;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            base.name = "vs/com/end";
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
                goto Label_0200;
            }
            builder.Append(0x2c);
            builder.Append("\"myhp\":[");
            num = 0;
            goto Label_01EA;
        Label_01C2:
            if (num <= 0)
            {
                goto Label_01D2;
            }
            builder.Append(0x2c);
        Label_01D2:
            builder.Append(&(myhp[num]).ToString());
            num += 1;
        Label_01EA:
            if (num < ((int) myhp.Length))
            {
                goto Label_01C2;
            }
            builder.Append("]");
        Label_0200:
            if (enhp == null)
            {
                goto Label_0261;
            }
            builder.Append(0x2c);
            builder.Append("\"enhp\":[");
            num2 = 0;
            goto Label_024B;
        Label_0223:
            if (num2 <= 0)
            {
                goto Label_0233;
            }
            builder.Append(0x2c);
        Label_0233:
            builder.Append(&(enhp[num2]).ToString());
            num2 += 1;
        Label_024B:
            if (num2 < ((int) enhp.Length))
            {
                goto Label_0223;
            }
            builder.Append("]");
        Label_0261:
            if (place == null)
            {
                goto Label_02C2;
            }
            builder.Append(0x2c);
            builder.Append("\"place\":[");
            num3 = 0;
            goto Label_02AC;
        Label_0284:
            if (num3 <= 0)
            {
                goto Label_0294;
            }
            builder.Append(0x2c);
        Label_0294:
            builder.Append(&(place[num3]).ToString());
            num3 += 1;
        Label_02AC:
            if (num3 < ((int) place.Length))
            {
                goto Label_0284;
            }
            builder.Append("]");
        Label_02C2:
            if (builder[builder.Length - 1] != 0x2c)
            {
                goto Label_02E5;
            }
            builder.Length -= 1;
        Label_02E5:
            builder.Append("}");
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_0312;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_0312:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_0333;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_0333:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

