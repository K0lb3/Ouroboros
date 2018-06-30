namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ReqBtlComRecordUpload : WebAPI
    {
        public ReqBtlComRecordUpload(string fuid, long btlid, int[] achieved, int time, BtlResultTypes result, int[] beats, int[] itemSteals, int[] goldSteals, int[] missions, Dictionary<OString, OInt> usedItems, Network.ResponseCallback response)
        {
            base..ctor();
            base.name = "btl/com/record/upload";
            base.body = CreateRequestString(makeBody(fuid, btlid, achieved, time, result, beats, itemSteals, goldSteals, missions, usedItems));
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

        private static unsafe string makeBody(string fuid, long btlid, int[] achieved, int time, BtlResultTypes result, int[] beats, int[] itemSteals, int[] goldSteals, int[] missions, Dictionary<OString, OInt> usedItems)
        {
            StringBuilder builder;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            KeyValuePair<OString, OInt> pair;
            Dictionary<OString, OInt>.Enumerator enumerator;
            BtlResultTypes types;
            builder = WebAPI.GetStringBuilder();
            builder.Length = 0;
            builder.Append("\"btlid\":");
            builder.Append(btlid);
            builder.Append(0x2c);
            if (achieved == null)
            {
                goto Label_007F;
            }
            builder.Append("\"achieved\":[");
            num = 0;
            goto Label_006A;
        Label_0043:
            if (num <= 0)
            {
                goto Label_0053;
            }
            builder.Append(0x2c);
        Label_0053:
            builder.Append(&(achieved[num]).ToString());
            num += 1;
        Label_006A:
            if (num < ((int) achieved.Length))
            {
                goto Label_0043;
            }
            builder.Append("],");
        Label_007F:
            builder.Append("\"btlendparam\":{");
            builder.Append("\"time\":");
            builder.Append(time);
            builder.Append(0x2c);
            builder.Append("\"result\":\"");
            types = result;
            switch (types)
            {
                case 0:
                    goto Label_00D4;

                case 1:
                    goto Label_00E5;

                case 2:
                    goto Label_00F6;

                case 3:
                    goto Label_0107;
            }
            goto Label_0118;
        Label_00D4:
            builder.Append("win");
            goto Label_0118;
        Label_00E5:
            builder.Append("lose");
            goto Label_0118;
        Label_00F6:
            builder.Append("retire");
            goto Label_0118;
        Label_0107:
            builder.Append("cancel");
        Label_0118:
            if (result != null)
            {
                goto Label_015B;
            }
            if (beats != null)
            {
                goto Label_012E;
            }
            beats = new int[0];
        Label_012E:
            if (itemSteals != null)
            {
                goto Label_013D;
            }
            itemSteals = new int[0];
        Label_013D:
            if (goldSteals != null)
            {
                goto Label_014C;
            }
            goldSteals = new int[0];
        Label_014C:
            if (missions != null)
            {
                goto Label_015B;
            }
            missions = new int[3];
        Label_015B:
            builder.Append("\",");
            if (beats == null)
            {
                goto Label_01BF;
            }
            builder.Append("\"beats\":[");
            num2 = 0;
            goto Label_01A9;
        Label_0181:
            if (num2 <= 0)
            {
                goto Label_0191;
            }
            builder.Append(0x2c);
        Label_0191:
            builder.Append(&(beats[num2]).ToString());
            num2 += 1;
        Label_01A9:
            if (num2 < ((int) beats.Length))
            {
                goto Label_0181;
            }
            builder.Append("],");
        Label_01BF:
            if (itemSteals != null)
            {
                goto Label_01CD;
            }
            if (goldSteals == null)
            {
                goto Label_02B8;
            }
        Label_01CD:
            builder.Append("\"steals\":{");
            if (itemSteals == null)
            {
                goto Label_0236;
            }
            builder.Append("\"items\":[");
            num3 = 0;
            goto Label_0220;
        Label_01F3:
            builder.Append(&(itemSteals[num3]).ToString());
            if (num3 == (((int) beats.Length) - 1))
            {
                goto Label_021C;
            }
            builder.Append(0x2c);
        Label_021C:
            num3 += 1;
        Label_0220:
            if (num3 < ((int) itemSteals.Length))
            {
                goto Label_01F3;
            }
            builder.Append("]");
        Label_0236:
            if (goldSteals == null)
            {
                goto Label_02AC;
            }
            if (itemSteals == null)
            {
                goto Label_024D;
            }
            builder.Append(0x2c);
        Label_024D:
            builder.Append("\"golds\":[");
            num4 = 0;
            goto Label_0295;
        Label_0261:
            builder.Append(&(goldSteals[num4]).ToString());
            if (num4 == (((int) beats.Length) - 1))
            {
                goto Label_028F;
            }
            builder.Append(",");
        Label_028F:
            num4 += 1;
        Label_0295:
            if (num4 < ((int) goldSteals.Length))
            {
                goto Label_0261;
            }
            builder.Append("]");
        Label_02AC:
            builder.Append("},");
        Label_02B8:
            if (missions == null)
            {
                goto Label_0316;
            }
            builder.Append("\"missions\":[");
            num5 = 0;
            goto Label_02FF;
        Label_02D3:
            if (num5 <= 0)
            {
                goto Label_02E4;
            }
            builder.Append(0x2c);
        Label_02E4:
            builder.Append(&(missions[num5]).ToString());
            num5 += 1;
        Label_02FF:
            if (num5 < ((int) missions.Length))
            {
                goto Label_02D3;
            }
            builder.Append("],");
        Label_0316:
            if (usedItems == null)
            {
                goto Label_03E6;
            }
            builder.Append("\"inputs\":[");
            num6 = 0;
            enumerator = usedItems.GetEnumerator();
        Label_0335:
            try
            {
                goto Label_03BC;
            Label_033A:
                pair = &enumerator.Current;
                if (num6 <= 0)
                {
                    goto Label_0354;
                }
                builder.Append(0x2c);
            Label_0354:
                builder.Append("{");
                builder.Append("\"use\":\"");
                builder.Append(&pair.Key);
                builder.Append("\",");
                builder.Append("\"n\":");
                builder.Append(&pair.Value);
                builder.Append("}");
                num6 += 1;
            Label_03BC:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_033A;
                }
                goto Label_03DA;
            }
            finally
            {
            Label_03CD:
                ((Dictionary<OString, OInt>.Enumerator) enumerator).Dispose();
            }
        Label_03DA:
            builder.Append("],");
        Label_03E6:
            if (builder[builder.Length - 1] != 0x2c)
            {
                goto Label_0409;
            }
            builder.Length -= 1;
        Label_0409:
            builder.Append(0x7d);
            return builder.ToString();
        }
    }
}

