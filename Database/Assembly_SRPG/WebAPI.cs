namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Experimental.Networking;

    public abstract class WebAPI
    {
        public string name;
        public string body;
        public Network.ResponseCallback callback;
        public readonly string GumiTransactionId;
        public ReqeustType reqtype;
        public DownloadHandler dlHandler;
        private static StringBuilder mSB;

        static WebAPI()
        {
            mSB = new StringBuilder(0x200);
            return;
        }

        protected unsafe WebAPI()
        {
            Guid guid;
            this.GumiTransactionId = &Guid.NewGuid().ToString();
            base..ctor();
            return;
        }

        public static string EscapeString(string s)
        {
            s = s.Replace(@"\", @"\\");
            s = s.Replace("\"", "\\\"");
            return s;
        }

        protected static unsafe string GetBtlEndParamString(BattleCore.Record record, bool multi)
        {
            object[] objArray1;
            string str;
            int num;
            string str2;
            int[] numArray;
            int num2;
            int[] numArray2;
            int num3;
            int[] numArray3;
            int num4;
            int[] numArray4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            string str3;
            str = null;
            if (record == null)
            {
                goto Label_0360;
            }
            num = 0;
            str2 = "win";
            if ((multi == null) || (record.result != null))
            {
                goto Label_002C;
            }
            str2 = "retire";
            goto Label_003E;
        Label_002C:
            if (record.result == 1)
            {
                goto Label_003E;
            }
            str2 = "lose";
        Label_003E:
            numArray = new int[(int) record.drops.Length];
            num2 = 0;
            goto Label_0075;
        Label_0054:
            numArray[num2] = *(&(record.drops[num2]));
            num2 += 1;
        Label_0075:
            if (num2 < ((int) record.drops.Length))
            {
                goto Label_0054;
            }
            numArray2 = new int[(int) record.item_steals.Length];
            num3 = 0;
            goto Label_00BD;
        Label_009B:
            numArray2[num3] = *(&(record.item_steals[num3]));
            num3 += 1;
        Label_00BD:
            if (num3 < ((int) record.item_steals.Length))
            {
                goto Label_009B;
            }
            numArray3 = new int[(int) record.gold_steals.Length];
            num4 = 0;
            goto Label_0105;
        Label_00E3:
            numArray3[num4] = *(&(record.gold_steals[num4]));
            num4 += 1;
        Label_0105:
            if (num4 < ((int) record.gold_steals.Length))
            {
                goto Label_00E3;
            }
            numArray4 = new int[record.bonusCount];
            num5 = 0;
            goto Label_014E;
        Label_0129:
            numArray4[num5] = ((record.bonusFlags & (1 << (num5 & 0x1f))) == null) ? 0 : 1;
            num5 += 1;
        Label_014E:
            if (num5 < ((int) numArray4.Length))
            {
                goto Label_0129;
            }
            str3 = str + "\"btlendparam\":{";
            objArray1 = new object[] { str3, "\"time\":", (int) num, "," };
            str = (string.Concat(objArray1) + "\"result\":\"" + str2 + "\",") + "\"beats\":[";
            num6 = 0;
            goto Label_01EA;
        Label_01B8:
            str = str + &(numArray[num6]).ToString();
            if (num6 == (((int) numArray.Length) - 1))
            {
                goto Label_01E4;
            }
            str = str + ",";
        Label_01E4:
            num6 += 1;
        Label_01EA:
            if (num6 < ((int) numArray.Length))
            {
                goto Label_01B8;
            }
            str = ((str + "],") + "\"steals\":{") + "\"items\":[";
            num7 = 0;
            goto Label_0253;
        Label_0220:
            str = str + &(numArray2[num7]).ToString();
            if (num7 == (((int) numArray.Length) - 1))
            {
                goto Label_024D;
            }
            str = str + ",";
        Label_024D:
            num7 += 1;
        Label_0253:
            if (num7 < ((int) numArray2.Length))
            {
                goto Label_0220;
            }
            str = (str + "],") + "\"golds\":[";
            num8 = 0;
            goto Label_02B1;
        Label_027E:
            str = str + &(numArray3[num8]).ToString();
            if (num8 == (((int) numArray.Length) - 1))
            {
                goto Label_02AB;
            }
            str = str + ",";
        Label_02AB:
            num8 += 1;
        Label_02B1:
            if (num8 < ((int) numArray3.Length))
            {
                goto Label_027E;
            }
            str = ((str + "]") + "},") + "\"missions\":[";
            num9 = 0;
            goto Label_031C;
        Label_02E8:
            str = str + &(numArray4[num9]).ToString();
            if (num9 == (((int) numArray4.Length) - 1))
            {
                goto Label_0316;
            }
            str = str + ",";
        Label_0316:
            num9 += 1;
        Label_031C:
            if (num9 < ((int) numArray4.Length))
            {
                goto Label_02E8;
            }
            str = str + "]";
            if (multi == null)
            {
                goto Label_0354;
            }
            str = str + ",\"token\":\"" + JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName) + "\"";
        Label_0354:
            str = str + "}";
        Label_0360:
            return str;
        }

        protected static string GetRequestString(string body)
        {
            string str;
            str = "{\"ticket\":" + ((int) Network.TicketID);
            if (string.IsNullOrEmpty(body) != null)
            {
                goto Label_0032;
            }
            str = str + ",\"param\":{" + body + "}";
        Label_0032:
            return (str + "}");
        }

        protected static string GetRequestString<T>(T param)
        {
            RequestParamWithTicketId<T> id;
            id = new RequestParamWithTicketId<T>(Network.TicketID, param);
            return JsonUtility.ToJson(id);
        }

        protected static StringBuilder GetStringBuilder()
        {
            mSB.Length = 0;
            return mSB;
        }

        public static string KeyValueToString(string key, bool value)
        {
            return string.Format("\"{0}\":{1}", key, (int) ((value == null) ? 0 : 1));
        }

        public static string KeyValueToString(string key, byte value)
        {
            return string.Format("\"{0}\":{1}", key, (byte) value);
        }

        public static string KeyValueToString(string key, short value)
        {
            return string.Format("\"{0}\":{1}", key, (short) value);
        }

        public static string KeyValueToString(string key, int value)
        {
            return string.Format("\"{0}\":{1}", key, (int) value);
        }

        public static string KeyValueToString(string key, long value)
        {
            return string.Format("\"{0}\":{1}", key, (long) value);
        }

        public static string KeyValueToString(string key, string value)
        {
            return string.Format("\"{0}\":\"{1}\"", key, value);
        }

        public static string KeyValueToString(string key, ushort value)
        {
            return string.Format("\"{0}\":{1}", key, (ushort) value);
        }

        public static string KeyValueToString(string key, uint value)
        {
            return string.Format("\"{0}\":{1}", key, (uint) value);
        }

        public static string KeyValueToString(string key, ulong value)
        {
            return string.Format("\"{0}\":{1}", key, (ulong) value);
        }

        public class JSON_BaseResponse
        {
            public int stat;
            public string stat_msg;
            public string stat_code;
            public long time;
            public int ticket;

            public JSON_BaseResponse()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_BodyResponse<T> : WebAPI.JSON_BaseResponse
        {
            public T body;

            public JSON_BodyResponse()
            {
                base..ctor();
                return;
            }
        }

        public enum ReqeustType
        {
            REQ_GSC,
            REQ_STREAM
        }

        protected class RequestParamWithTicketId<T>
        {
            public int ticket;
            public T param;

            public RequestParamWithTicketId(int _ticket, T _param)
            {
                base..ctor();
                this.ticket = _ticket;
                this.param = _param;
                return;
            }
        }
    }
}

