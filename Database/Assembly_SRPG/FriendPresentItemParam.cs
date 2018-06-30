namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;

    public class FriendPresentItemParam
    {
        public const string DEAULT_ID = "FP_DEFAULT";
        public static FriendPresentItemParam DefaultParam;
        private string m_Id;
        private string m_Name;
        private string m_Expr;
        private ItemParam m_Item;
        private int m_Num;
        private int m_Zeny;
        private long m_BeginAt;
        private long m_EndAt;

        static FriendPresentItemParam()
        {
        }

        public FriendPresentItemParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_FriendPresentItemParam json, MasterParam master)
        {
            DateTime time;
            DateTime time2;
            Exception exception;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.m_Id = json.iname;
            this.m_Name = json.name;
            this.m_Expr = json.expr;
            if (string.IsNullOrEmpty(json.item) != null)
            {
                goto Label_0056;
            }
            this.m_Item = MonoSingleton<GameManager>.Instance.GetItemParam(json.item);
        Label_0056:
            this.m_Num = json.num;
            this.m_Zeny = json.zeny;
        Label_006E:
            try
            {
                if (string.IsNullOrEmpty(json.begin_at) != null)
                {
                    goto Label_0096;
                }
                time = DateTime.Parse(json.begin_at);
                this.m_BeginAt = TimeManager.GetUnixSec(time);
            Label_0096:
                if (string.IsNullOrEmpty(json.end_at) != null)
                {
                    goto Label_00BE;
                }
                time2 = DateTime.Parse(json.end_at);
                this.m_EndAt = TimeManager.GetUnixSec(time2);
            Label_00BE:
                if ((this.m_Id == "FP_DEFAULT") == null)
                {
                    goto Label_00D9;
                }
                DefaultParam = this;
            Label_00D9:
                goto Label_00EF;
            }
            catch (Exception exception1)
            {
            Label_00DE:
                exception = exception1;
                DebugUtility.LogError(exception.ToString());
                goto Label_00EF;
            }
        Label_00EF:
            return;
        }

        public long GetRestTime(long serverTime)
        {
            long num;
            num = this.m_EndAt - serverTime;
            if (num >= 0L)
            {
                goto Label_0014;
            }
            num = 0L;
        Label_0014:
            return num;
        }

        public bool HasTimeLimit()
        {
            return ((this.m_BeginAt > 0L) ? 1 : (this.m_EndAt > 0L));
        }

        public bool IsDefault()
        {
            return (this.m_Id == "FP_DEFAULT");
        }

        public bool IsItem()
        {
            return ((this.m_Item == null) == 0);
        }

        public bool IsValid(long nowSec)
        {
            if (this.HasTimeLimit() != null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            return ((this.m_BeginAt > nowSec) ? 0 : (nowSec < this.m_EndAt));
        }

        public bool IsZeny()
        {
            return (this.m_Item == null);
        }

        public string iname
        {
            get
            {
                return this.m_Id;
            }
        }

        public string name
        {
            get
            {
                return this.m_Name;
            }
        }

        public string expr
        {
            get
            {
                return this.m_Expr;
            }
        }

        public ItemParam item
        {
            get
            {
                return this.m_Item;
            }
        }

        public int num
        {
            get
            {
                return this.m_Num;
            }
        }

        public int zeny
        {
            get
            {
                return this.m_Zeny;
            }
        }

        public long begin_at
        {
            get
            {
                return this.m_BeginAt;
            }
        }

        public long end_at
        {
            get
            {
                return this.m_EndAt;
            }
        }
    }
}

