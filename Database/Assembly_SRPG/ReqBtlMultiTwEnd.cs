namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqBtlMultiTwEnd : WebAPI
    {
        public unsafe ReqBtlMultiTwEnd(long btlid, int time, BtlResultTypes result, int[] myhp, string[] myUnit, string[] fuid, Network.ResponseCallback response, string trophyprog, string bingoprog)
        {
            StringBuilder builder;
            int num;
            int num2;
            int num3;
            BtlResultTypes types;
            base..ctor();
            base.name = "btl/multi/tower/end";
            builder = WebAPI.GetStringBuilder();
            builder.Length = 0;
            builder.Append("\"btlid\":");
            builder.Append(btlid);
            builder.Append(0x2c);
            builder.Append("\"btlendparam\":{");
            builder.Append("\"time\":");
            builder.Append(time);
            builder.Append(0x2c);
            builder.Append("\"result\":\"");
            types = result;
            switch (types)
            {
                case 0:
                    goto Label_0093;

                case 1:
                    goto Label_00A4;

                case 2:
                    goto Label_00B5;

                case 3:
                    goto Label_00C6;

                case 4:
                    goto Label_00D7;
            }
            goto Label_00E8;
        Label_0093:
            builder.Append("win");
            goto Label_00E8;
        Label_00A4:
            builder.Append("lose");
            goto Label_00E8;
        Label_00B5:
            builder.Append("retire");
            goto Label_00E8;
        Label_00C6:
            builder.Append("cancel");
            goto Label_00E8;
        Label_00D7:
            builder.Append("draw");
        Label_00E8:
            builder.Append("\",");
            builder.Append("\"token\":\"");
            builder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
            builder.Append("\"");
            if (myhp == null)
            {
                goto Label_017E;
            }
            builder.Append(0x2c);
            builder.Append("\"myhp\":[");
            num = 0;
            goto Label_0168;
        Label_0140:
            if (num <= 0)
            {
                goto Label_0150;
            }
            builder.Append(0x2c);
        Label_0150:
            builder.Append(&(myhp[num]).ToString());
            num += 1;
        Label_0168:
            if (num < ((int) myhp.Length))
            {
                goto Label_0140;
            }
            builder.Append("]");
        Label_017E:
            if (myUnit == null)
            {
                goto Label_01E5;
            }
            builder.Append(0x2c);
            builder.Append("\"myUnit\":[");
            num2 = 0;
            goto Label_01CF;
        Label_01A1:
            if (num2 <= 0)
            {
                goto Label_01B1;
            }
            builder.Append(0x2c);
        Label_01B1:
            builder.Append("\"" + myUnit[num2] + "\"");
            num2 += 1;
        Label_01CF:
            if (num2 < ((int) myhp.Length))
            {
                goto Label_01A1;
            }
            builder.Append("]");
        Label_01E5:
            builder.Append("}");
            if (fuid == null)
            {
                goto Label_0268;
            }
            builder.Append(",\"fuids\":[");
            num3 = 0;
            goto Label_0252;
        Label_020B:
            if (fuid[num3] != null)
            {
                goto Label_0219;
            }
            goto Label_024E;
        Label_0219:
            if (num3 == null)
            {
                goto Label_022B;
            }
            builder.Append(", ");
        Label_022B:
            builder.Append("\"");
            builder.Append(fuid[num3]);
            builder.Append("\"");
        Label_024E:
            num3 += 1;
        Label_0252:
            if (num3 < ((int) fuid.Length))
            {
                goto Label_020B;
            }
            builder.Append("]");
        Label_0268:
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_0289;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_0289:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_02AA;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_02AA:
            base.body = WebAPI.GetRequestString(builder.ToString());
            base.callback = response;
            return;
        }
    }
}

