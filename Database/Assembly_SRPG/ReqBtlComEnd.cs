namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ReqBtlComEnd : WebAPI
    {
        public ReqBtlComEnd(string req_fuid, int opp_rank, int my_rank, BtlResultTypes result, int[] beats, int[] itemSteals, int[] goldSteals, int[] missions, string[] fuid, Dictionary<OString, OInt> usedItems, Network.ResponseCallback response, BtlEndTypes apiType, string trophyprog, string bingoprog)
        {
            StringBuilder builder;
            bool? nullable;
            base..ctor();
            base.name = "btl/colo/exec";
            builder = WebAPI.GetStringBuilder();
            builder.Append("\"fuid\":\"");
            builder.Append(req_fuid);
            builder.Append("\"");
            builder.Append(",\"opp_rank\":");
            builder.Append(opp_rank);
            builder.Append(",\"my_rank\":");
            builder.Append(my_rank);
            base.body = WebAPI.GetRequestString(builder.ToString() + "," + this.makeBody(1, 0L, 0, result, beats, itemSteals, goldSteals, missions, fuid, usedItems, response, apiType, trophyprog, bingoprog, 0, null, 0, new bool?()));
            base.callback = response;
            return;
        }

        public ReqBtlComEnd(long btlid, int time, BtlResultTypes result, int[] beats, int[] itemSteals, int[] goldSteals, int[] missions, string[] fuid, Dictionary<OString, OInt> usedItems, Network.ResponseCallback response, BtlEndTypes apiType, string trophyprog, string bingoprog, int elem, string rankingQuestEndParam, bool is_rehash, bool? is_skip)
        {
            StringBuilder builder;
            base..ctor();
            builder = WebAPI.GetStringBuilder();
            builder.Append("btl/");
            builder.Append(((BtlEndTypes) apiType).ToString());
            builder.Append("/end");
            base.name = builder.ToString();
            base.body = WebAPI.GetRequestString(this.makeBody(0, btlid, time, result, beats, itemSteals, goldSteals, missions, fuid, usedItems, response, apiType, trophyprog, bingoprog, elem, rankingQuestEndParam, is_rehash, is_skip));
            base.callback = response;
            return;
        }

        public static string CreateRankingQuestEndParam(int main_score, int sub_score)
        {
            StringBuilder builder;
            builder = new StringBuilder(0x80);
            builder.Append("\"score\":{");
            builder.Append("\"main_score\":");
            builder.Append(main_score);
            builder.Append(",");
            builder.Append("\"sub_score\":");
            builder.Append(sub_score);
            builder.Append("}");
            return builder.ToString();
        }

        private unsafe string makeBody(bool is_arena, long btlid, int time, BtlResultTypes result, int[] beats, int[] itemSteals, int[] goldSteals, int[] missions, string[] fuid, Dictionary<OString, OInt> usedItems, Network.ResponseCallback response, BtlEndTypes apiType, string trophyprog, string bingoprog, int elem, string rankingQuestEndParam, bool is_rehash, bool? is_skip)
        {
            StringBuilder builder;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            KeyValuePair<OString, OInt> pair;
            Dictionary<OString, OInt>.Enumerator enumerator;
            int num6;
            BtlResultTypes types;
            builder = WebAPI.GetStringBuilder();
            builder.Length = 0;
            if (is_arena != null)
            {
                goto Label_0030;
            }
            builder.Append("\"btlid\":");
            builder.Append(btlid);
            builder.Append(0x2c);
        Label_0030:
            builder.Append("\"btlendparam\":{");
            builder.Append("\"time\":");
            builder.Append(time);
            builder.Append(0x2c);
            builder.Append("\"result\":\"");
            types = result;
            switch (types)
            {
                case 0:
                    goto Label_0085;

                case 1:
                    goto Label_0096;

                case 2:
                    goto Label_00A7;

                case 3:
                    goto Label_00B8;
            }
            goto Label_00C9;
        Label_0085:
            builder.Append("win");
            goto Label_00C9;
        Label_0096:
            builder.Append("lose");
            goto Label_00C9;
        Label_00A7:
            builder.Append("retire");
            goto Label_00C9;
        Label_00B8:
            builder.Append("cancel");
        Label_00C9:
            if (result != null)
            {
                goto Label_010C;
            }
            if (beats != null)
            {
                goto Label_00DF;
            }
            beats = new int[0];
        Label_00DF:
            if (itemSteals != null)
            {
                goto Label_00EE;
            }
            itemSteals = new int[0];
        Label_00EE:
            if (goldSteals != null)
            {
                goto Label_00FD;
            }
            goldSteals = new int[0];
        Label_00FD:
            if (missions != null)
            {
                goto Label_010C;
            }
            missions = new int[3];
        Label_010C:
            if ((result == 3) || (usedItems != null))
            {
                goto Label_0122;
            }
            usedItems = new Dictionary<OString, OInt>();
        Label_0122:
            builder.Append("\",");
            if (beats == null)
            {
                goto Label_0186;
            }
            builder.Append("\"beats\":[");
            num = 0;
            goto Label_0170;
        Label_0148:
            if (num <= 0)
            {
                goto Label_0158;
            }
            builder.Append(0x2c);
        Label_0158:
            builder.Append(&(beats[num]).ToString());
            num += 1;
        Label_0170:
            if (num < ((int) beats.Length))
            {
                goto Label_0148;
            }
            builder.Append("],");
        Label_0186:
            if ((itemSteals == null) && (goldSteals == null))
            {
                goto Label_0279;
            }
            builder.Append("\"steals\":{");
            if (itemSteals == null)
            {
                goto Label_01FD;
            }
            builder.Append("\"items\":[");
            num2 = 0;
            goto Label_01E7;
        Label_01BA:
            builder.Append(&(itemSteals[num2]).ToString());
            if (num2 == (((int) beats.Length) - 1))
            {
                goto Label_01E3;
            }
            builder.Append(0x2c);
        Label_01E3:
            num2 += 1;
        Label_01E7:
            if (num2 < ((int) itemSteals.Length))
            {
                goto Label_01BA;
            }
            builder.Append("]");
        Label_01FD:
            if (goldSteals == null)
            {
                goto Label_026D;
            }
            if (itemSteals == null)
            {
                goto Label_0214;
            }
            builder.Append(0x2c);
        Label_0214:
            builder.Append("\"golds\":[");
            num3 = 0;
            goto Label_0257;
        Label_0227:
            builder.Append(&(goldSteals[num3]).ToString());
            if (num3 == (((int) beats.Length) - 1))
            {
                goto Label_0253;
            }
            builder.Append(",");
        Label_0253:
            num3 += 1;
        Label_0257:
            if (num3 < ((int) goldSteals.Length))
            {
                goto Label_0227;
            }
            builder.Append("]");
        Label_026D:
            builder.Append("},");
        Label_0279:
            if (missions == null)
            {
                goto Label_02D7;
            }
            builder.Append("\"missions\":[");
            num4 = 0;
            goto Label_02C0;
        Label_0294:
            if (num4 <= 0)
            {
                goto Label_02A5;
            }
            builder.Append(0x2c);
        Label_02A5:
            builder.Append(&(missions[num4]).ToString());
            num4 += 1;
        Label_02C0:
            if (num4 < ((int) missions.Length))
            {
                goto Label_0294;
            }
            builder.Append("],");
        Label_02D7:
            if (usedItems == null)
            {
                goto Label_03A7;
            }
            builder.Append("\"inputs\":[");
            num5 = 0;
            enumerator = usedItems.GetEnumerator();
        Label_02F6:
            try
            {
                goto Label_037D;
            Label_02FB:
                pair = &enumerator.Current;
                if (num5 <= 0)
                {
                    goto Label_0315;
                }
                builder.Append(0x2c);
            Label_0315:
                builder.Append("{");
                builder.Append("\"use\":\"");
                builder.Append(&pair.Key);
                builder.Append("\",");
                builder.Append("\"n\":");
                builder.Append(&pair.Value);
                builder.Append("}");
                num5 += 1;
            Label_037D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_02FB;
                }
                goto Label_039B;
            }
            finally
            {
            Label_038E:
                ((Dictionary<OString, OInt>.Enumerator) enumerator).Dispose();
            }
        Label_039B:
            builder.Append("],");
        Label_03A7:
            if (apiType != 1)
            {
                goto Label_03D8;
            }
            builder.Append("\"token\":\"");
            builder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
            builder.Append("\",");
        Label_03D8:
            if (string.IsNullOrEmpty(rankingQuestEndParam) != null)
            {
                goto Label_03F9;
            }
            builder.Append(rankingQuestEndParam);
            builder.Append(",");
        Label_03F9:
            if ((result != 3) || (is_rehash == null))
            {
                goto Label_0428;
            }
            builder.Append("\"is_rehash\":");
            builder.Append(1);
            builder.Append(",");
        Label_0428:
            if (&is_skip.HasValue == null)
            {
                goto Label_0466;
            }
            builder.Append("\"is_skip\":");
            builder.Append((&is_skip.Value == null) ? 0 : 1);
            builder.Append(",");
        Label_0466:
            if (builder[builder.Length - 1] != 0x2c)
            {
                goto Label_0489;
            }
            builder.Length -= 1;
        Label_0489:
            builder.Append(0x7d);
            if (apiType != 1)
            {
                goto Label_0518;
            }
            if (fuid == null)
            {
                goto Label_0518;
            }
            builder.Append(",\"fuids\":[");
            num6 = 0;
            goto Label_0501;
        Label_04B5:
            if (fuid[num6] != null)
            {
                goto Label_04C4;
            }
            goto Label_04FB;
        Label_04C4:
            if (num6 == null)
            {
                goto Label_04D7;
            }
            builder.Append(", ");
        Label_04D7:
            builder.Append("\"");
            builder.Append(fuid[num6]);
            builder.Append("\"");
        Label_04FB:
            num6 += 1;
        Label_0501:
            if (num6 < ((int) fuid.Length))
            {
                goto Label_04B5;
            }
            builder.Append("]");
        Label_0518:
            if (string.IsNullOrEmpty(trophyprog) != null)
            {
                goto Label_0539;
            }
            builder.Append(",");
            builder.Append(trophyprog);
        Label_0539:
            if (string.IsNullOrEmpty(bingoprog) != null)
            {
                goto Label_055A;
            }
            builder.Append(",");
            builder.Append(bingoprog);
        Label_055A:
            if (elem == null)
            {
                goto Label_058E;
            }
            builder.Append(",");
            builder.Append("\"support_elem\":\"");
            builder.Append(elem);
            builder.Append("\"");
        Label_058E:
            return builder.ToString();
        }
    }
}

