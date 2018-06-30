namespace SRPG
{
    using System;
    using System.Reflection;
    using System.Text;

    public class BattleLogServer
    {
        public static readonly int MAX_REGISTER_BATTLE_LOG;
        private static readonly int BATTLE_LOG_REPORT_SIZE;
        private BattleLog[] mLogs;
        private int mLogNum;
        private int mLogTop;
        private StringBuilder mReport;

        static BattleLogServer()
        {
            MAX_REGISTER_BATTLE_LOG = 0x100;
            BATTLE_LOG_REPORT_SIZE = 0x800;
            return;
        }

        public BattleLogServer()
        {
            this.mReport = new StringBuilder(BATTLE_LOG_REPORT_SIZE);
            base..ctor();
            this.mLogs = new BattleLog[MAX_REGISTER_BATTLE_LOG];
            this.Reset();
            return;
        }

        public LogType Log<LogType>() where LogType: BattleLog, new()
        {
            int num;
            LogType local;
            if (this.mLogNum <= ((int) this.mLogs.Length))
            {
                goto Label_0024;
            }
            DebugUtility.LogError("BattleLog: failed many log.");
            return (LogType) null;
        Label_0024:
            num = (this.mLogTop + this.mLogNum) % ((int) this.mLogs.Length);
            local = Activator.CreateInstance<LogType>();
            this.mLogs[num] = (LogType) local;
            this.mLogNum += 1;
            return local;
        }

        public void Release()
        {
            int num;
            if (this.mLogs == null)
            {
                goto Label_0034;
            }
            num = 0;
            goto Label_001F;
        Label_0012:
            this.mLogs[num] = null;
            num += 1;
        Label_001F:
            if (num < ((int) this.mLogs.Length))
            {
                goto Label_0012;
            }
            this.mLogs = null;
        Label_0034:
            this.mReport = null;
            return;
        }

        public void RemoveLog()
        {
            if (this.mLogs[this.mLogTop] == null)
            {
                goto Label_002A;
            }
            this.mLogs[this.mLogTop].Serialize(this.mReport);
        Label_002A:
            this.mLogs[this.mLogTop] = null;
            this.mLogTop = (this.mLogTop + 1) % ((int) this.mLogs.Length);
            this.mLogNum -= 1;
            return;
        }

        public void RemoveLogLast()
        {
            int num;
            if (this.mLogNum > 0)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            num = (this.mLogTop + (this.mLogNum - 1)) % ((int) this.mLogs.Length);
            if (num >= 0)
            {
                goto Label_002F;
            }
            num = 0;
        Label_002F:
            this.mLogs[num] = null;
            this.mLogNum -= 1;
            return;
        }

        public void RemoveLogOffs(int offs)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (offs < 0)
            {
                goto Label_0013;
            }
            if (offs < this.mLogNum)
            {
                goto Label_0014;
            }
        Label_0013:
            return;
        Label_0014:
            if (offs != null)
            {
                goto Label_0021;
            }
            this.RemoveLog();
            return;
        Label_0021:
            num = (this.mLogTop + offs) % ((int) this.mLogs.Length);
            this.mLogs[num] = null;
            num2 = 0;
            goto Label_0081;
        Label_0043:
            num3 = (((this.mLogTop + offs) + num2) + 1) % ((int) this.mLogs.Length);
            num4 = ((this.mLogTop + offs) + num2) % ((int) this.mLogs.Length);
            this.mLogs[num4] = this.mLogs[num3];
            num2 += 1;
        Label_0081:
            if (num2 < (this.mLogNum - offs))
            {
                goto Label_0043;
            }
            if (this.mLogNum != MAX_REGISTER_BATTLE_LOG)
            {
                goto Label_00BE;
            }
            this.mLogs[((this.mLogTop + MAX_REGISTER_BATTLE_LOG) - 1) % ((int) this.mLogs.Length)] = null;
        Label_00BE:
            this.mLogNum -= 1;
            return;
        }

        public void Reset()
        {
            int num;
            num = 0;
            goto Label_0014;
        Label_0007:
            this.mLogs[num] = null;
            num += 1;
        Label_0014:
            if (num < ((int) this.mLogs.Length))
            {
                goto Label_0007;
            }
            this.mLogNum = 0;
            this.mLogTop = 0;
            return;
        }

        public BattleLog this[int offset]
        {
            get
            {
                return this.mLogs[(this.mLogTop + offset) % ((int) this.mLogs.Length)];
            }
        }

        public int Num
        {
            get
            {
                return this.mLogNum;
            }
        }

        public int Top
        {
            get
            {
                return this.mLogTop;
            }
        }

        public BattleLog Peek
        {
            get
            {
                return this.mLogs[this.mLogTop];
            }
        }

        public BattleLog Last
        {
            get
            {
                int num;
                num = (this.mLogTop + (this.mLogNum - 1)) % ((int) this.mLogs.Length);
                if (num >= 0)
                {
                    goto Label_0022;
                }
                num = 0;
            Label_0022:
                return this.mLogs[num];
            }
        }

        public StringBuilder Report
        {
            get
            {
                return this.mReport;
            }
        }
    }
}

