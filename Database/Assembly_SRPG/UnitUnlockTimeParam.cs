namespace SRPG
{
    using System;

    public class UnitUnlockTimeParam
    {
        public string iname;
        public string name;
        public DateTime begin_at;
        public DateTime end_at;

        public UnitUnlockTimeParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_UnitUnlockTimeParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.name = json.name;
            if (string.IsNullOrEmpty(json.begin_at) != null)
            {
                goto Label_0057;
            }
        Label_0030:
            try
            {
                this.begin_at = DateTime.Parse(json.begin_at);
                goto Label_0057;
            }
            catch
            {
            Label_0046:
                this.begin_at = DateTime.MaxValue;
                goto Label_0057;
            }
        Label_0057:
            if (string.IsNullOrEmpty(json.end_at) != null)
            {
                goto Label_008E;
            }
        Label_0067:
            try
            {
                this.end_at = DateTime.Parse(json.end_at);
                goto Label_008E;
            }
            catch
            {
            Label_007D:
                this.end_at = DateTime.MinValue;
                goto Label_008E;
            }
        Label_008E:
            return 1;
        }
    }
}

