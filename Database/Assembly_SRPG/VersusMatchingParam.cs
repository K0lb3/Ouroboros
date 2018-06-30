namespace SRPG
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class VersusMatchingParam
    {
        public static string TOWER_KEY;
        public static string FREE_KEY;
        public static string FRIEND_KEY;
        public string MatchKey;
        public string MatchKeyHash;
        public int MatchLinePoint;

        static VersusMatchingParam()
        {
            TOWER_KEY = "tower";
            FREE_KEY = "free";
            FRIEND_KEY = "friend";
            return;
        }

        public VersusMatchingParam()
        {
            base..ctor();
            return;
        }

        public static string CalcHash(string msg)
        {
            MD5 md;
            byte[] buffer;
            byte[] buffer2;
            StringBuilder builder;
            int num;
            md = new MD5CryptoServiceProvider();
            buffer = Encoding.UTF8.GetBytes(msg);
            buffer2 = md.ComputeHash(buffer);
            builder = new StringBuilder();
            num = 0;
            goto Label_0043;
        Label_0028:
            builder.AppendFormat("{0:x2}", (byte) buffer2[num]);
            num += 1;
        Label_0043:
            if (num < ((int) buffer2.Length))
            {
                goto Label_0028;
            }
            return builder.ToString();
        }

        public void Deserialize(JSON_VersusMatchingParam json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.MatchKey = json.key;
            this.MatchKeyHash = CalcHash(json.key);
            this.MatchLinePoint = json.point;
            return;
        }
    }
}

