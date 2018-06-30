namespace SRPG
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class MultiPlayAPIRoom
    {
        private static readonly string LOCKED;
        public int roomid;
        public string comment;
        public Quest quest;
        public string pwd_hash;
        public int limit;
        public int unitlv;
        public int clear;
        public int is_clear;
        public int floor;
        public int num;
        public Owner owner;

        static MultiPlayAPIRoom()
        {
            LOCKED = "1";
            return;
        }

        public MultiPlayAPIRoom()
        {
            base..ctor();
            return;
        }

        public static string CalcHash(string pwd)
        {
            MD5 md;
            byte[] buffer;
            byte[] buffer2;
            StringBuilder builder;
            int num;
            if (string.IsNullOrEmpty(pwd) == null)
            {
                goto Label_0011;
            }
            return string.Empty;
        Label_0011:
            md = new MD5CryptoServiceProvider();
            buffer = Encoding.UTF8.GetBytes(pwd);
            buffer2 = md.ComputeHash(buffer);
            builder = new StringBuilder();
            num = 0;
            goto Label_0054;
        Label_0039:
            builder.AppendFormat("{0:x2}", (byte) buffer2[num]);
            num += 1;
        Label_0054:
            if (num < ((int) buffer2.Length))
            {
                goto Label_0039;
            }
            return builder.ToString();
        }

        public static bool IsLocked(string pwd)
        {
            return (pwd == LOCKED);
        }

        public class Owner
        {
            public string name;
            public string level;
            public Json_Unit[] units;

            public Owner()
            {
                base..ctor();
                return;
            }
        }

        public class Quest
        {
            public string iname;

            public Quest()
            {
                base..ctor();
                return;
            }
        }
    }
}

