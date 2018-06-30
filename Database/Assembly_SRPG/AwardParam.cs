namespace SRPG
{
    using System;

    public class AwardParam
    {
        public int id;
        public string iname;
        public string name;
        public string expr;
        public string icon;
        public string bg;
        public string txt_img;
        public DateTime start_at;
        public int grade;
        public int hash;
        public int tab;

        public AwardParam()
        {
            base..ctor();
            return;
        }

        public unsafe bool Deserialize(JSON_AwardParam json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.id = json.id;
            this.iname = json.iname;
            this.name = json.name;
            this.expr = json.expr;
            this.icon = json.icon;
            this.bg = json.bg;
            this.txt_img = json.txt_img;
            this.start_at = DateTime.MinValue;
            if (string.IsNullOrEmpty(json.start_at) != null)
            {
                goto Label_0089;
            }
            DateTime.TryParse(json.start_at, &this.start_at);
        Label_0089:
            this.grade = json.grade;
            this.hash = json.iname.GetHashCode();
            this.tab = json.tab;
            return 1;
        }

        public bool IsAvailableStart(DateTime now)
        {
            return (this.start_at < now);
        }

        public ItemParam ToItemParam()
        {
            JSON_ItemParam param;
            ItemParam param2;
            param = new JSON_ItemParam();
            param.iname = this.iname;
            param.name = this.name;
            param.icon = this.icon;
            param.rare = this.grade - 1;
            param.type = 0x13;
            param2 = new ItemParam();
            param2.Deserialize(param);
            return param2;
        }

        public enum AwardGrade
        {
            None,
            Grade1,
            Grade2,
            Grade3,
            Grade4,
            Grade5,
            GradeEx
        }

        public enum Tab
        {
            None = -1,
            Normal = 0,
            Extra = 1
        }
    }
}

