namespace SRPG
{
    using GR;
    using System;

    public class ShareStringList
    {
        private short[] mIndexs;
        private ShareString.Type mType;

        public ShareStringList(ShareString.Type type)
        {
            base..ctor();
            this.mType = type;
            return;
        }

        public void Clear()
        {
            this.mIndexs = null;
            this.mType = 0;
            return;
        }

        public string Get(int index)
        {
            if (this.mIndexs == null)
            {
                goto Label_0019;
            }
            if (index < ((int) this.mIndexs.Length))
            {
                goto Label_001B;
            }
        Label_0019:
            return null;
        Label_001B:
            return Singleton<ShareVariable>.Instance.str.Get(this.mType, this.mIndexs[index]);
        }

        public string[] GetList()
        {
            string[] strArray;
            int num;
            if (this.mIndexs == null)
            {
                goto Label_0019;
            }
            if (((int) this.mIndexs.Length) >= 0)
            {
                goto Label_001B;
            }
        Label_0019:
            return null;
        Label_001B:
            strArray = new string[(int) this.mIndexs.Length];
            num = 0;
            goto Label_0054;
        Label_0030:
            strArray[num] = Singleton<ShareVariable>.Instance.str.Get(this.mType, this.mIndexs[num]);
            num += 1;
        Label_0054:
            if (num < ((int) this.mIndexs.Length))
            {
                goto Label_0030;
            }
            return strArray;
        }

        public bool IsNotNull()
        {
            return ((this.mIndexs == null) == 0);
        }

        public void Set(int index, string value)
        {
            if (this.mIndexs == null)
            {
                goto Label_0019;
            }
            if (index < ((int) this.mIndexs.Length))
            {
                goto Label_001A;
            }
        Label_0019:
            return;
        Label_001A:
            this.mIndexs[index] = Singleton<ShareVariable>.Instance.str.Set(this.mType, value);
            return;
        }

        public void Setup(int length)
        {
            int num;
            this.mIndexs = new short[length];
            num = 0;
            goto Label_0020;
        Label_0013:
            this.mIndexs[num] = -1;
            num += 1;
        Label_0020:
            if (num < length)
            {
                goto Label_0013;
            }
            return;
        }

        public int Length
        {
            get
            {
                if (this.mIndexs == null)
                {
                    goto Label_0019;
                }
                if (((int) this.mIndexs.Length) >= 0)
                {
                    goto Label_001B;
                }
            Label_0019:
                return 0;
            Label_001B:
                return (int) this.mIndexs.Length;
            }
        }
    }
}

