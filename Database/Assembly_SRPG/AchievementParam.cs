namespace SRPG
{
    using System;

    public class AchievementParam
    {
        public int id;
        public string iname;
        public string ios;
        public string googleplay;

        public AchievementParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_AchievementParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.id = json.fields.id;
            this.iname = json.fields.iname;
            this.ios = json.fields.ios;
            this.googleplay = json.fields.googleplay;
            return 1;
        }

        public string AchievementID
        {
            get
            {
                return string.Empty;
            }
        }
    }
}

