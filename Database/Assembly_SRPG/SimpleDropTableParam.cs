namespace SRPG
{
    using System;

    public class SimpleDropTableParam
    {
        public string iname;
        public DateTime beginAt;
        public DateTime endAt;
        public string[] dropList;
        public string[] dropcards;

        public SimpleDropTableParam()
        {
            base..ctor();
            return;
        }

        public unsafe bool Deserialize(JSON_SimpleDropTableParam json)
        {
            this.iname = json.iname;
            this.dropList = json.droplist;
            this.dropcards = json.dropcards;
            this.beginAt = DateTime.MinValue;
            this.endAt = DateTime.MaxValue;
            if (string.IsNullOrEmpty(json.begin_at) != null)
            {
                goto Label_005C;
            }
            DateTime.TryParse(json.begin_at, &this.beginAt);
        Label_005C:
            if (string.IsNullOrEmpty(json.end_at) != null)
            {
                goto Label_007E;
            }
            DateTime.TryParse(json.end_at, &this.endAt);
        Label_007E:
            return 1;
        }

        public bool IsAvailablePeriod(DateTime now)
        {
            if ((now < this.beginAt) != null)
            {
                goto Label_0022;
            }
            if ((this.endAt < now) == null)
            {
                goto Label_0024;
            }
        Label_0022:
            return 0;
        Label_0024:
            return 1;
        }

        public string GetCommonName
        {
            get
            {
                char[] chArray1;
                string[] strArray;
                if (string.IsNullOrEmpty(this.iname) == null)
                {
                    goto Label_0016;
                }
                return string.Empty;
            Label_0016:
                chArray1 = new char[] { 0x3a };
                return this.iname.Split(chArray1)[0];
            }
        }

        public bool IsSuffix
        {
            get
            {
                char[] chArray1;
                string[] strArray;
                chArray1 = new char[] { 0x3a };
                strArray = this.iname.Split(chArray1);
                return ((2 > ((int) strArray.Length)) ? 0 : 1);
            }
        }
    }
}

