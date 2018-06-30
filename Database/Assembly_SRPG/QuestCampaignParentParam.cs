namespace SRPG
{
    using System;

    public class QuestCampaignParentParam
    {
        public string iname;
        public DateTime beginAt;
        public DateTime endAt;
        public string[] children;

        public QuestCampaignParentParam()
        {
            base..ctor();
            return;
        }

        public unsafe bool Deserialize(JSON_QuestCampaignParentParam json)
        {
            this.iname = json.iname;
            this.children = json.children;
            this.beginAt = DateTime.MinValue;
            this.endAt = DateTime.MaxValue;
            if (string.IsNullOrEmpty(json.begin_at) != null)
            {
                goto Label_0050;
            }
            DateTime.TryParse(json.begin_at, &this.beginAt);
        Label_0050:
            if (string.IsNullOrEmpty(json.end_at) != null)
            {
                goto Label_0072;
            }
            DateTime.TryParse(json.end_at, &this.endAt);
        Label_0072:
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

        public bool IsChild(string childId)
        {
            string str;
            string[] strArray;
            int num;
            if (this.children == null)
            {
                goto Label_0038;
            }
            strArray = this.children;
            num = 0;
            goto Label_002F;
        Label_0019:
            str = strArray[num];
            if ((str == childId) == null)
            {
                goto Label_002B;
            }
            return 1;
        Label_002B:
            num += 1;
        Label_002F:
            if (num < ((int) strArray.Length))
            {
                goto Label_0019;
            }
        Label_0038:
            return 0;
        }
    }
}

