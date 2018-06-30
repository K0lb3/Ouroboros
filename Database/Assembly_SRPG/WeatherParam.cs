namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class WeatherParam
    {
        private string mIname;
        private string mName;
        private string mExpr;
        private string mIcon;
        private string mEffect;
        private List<string> mBuffIdLists;
        private List<string> mCondIdLists;

        public WeatherParam()
        {
            this.mBuffIdLists = new List<string>();
            this.mCondIdLists = new List<string>();
            base..ctor();
            return;
        }

        public void Deserialize(JSON_WeatherParam json)
        {
            string str;
            string[] strArray;
            int num;
            string str2;
            string[] strArray2;
            int num2;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mIname = json.iname;
            this.mName = json.name;
            this.mExpr = json.expr;
            this.mIcon = json.icon;
            this.mEffect = json.effect;
            this.mBuffIdLists.Clear();
            if (json.buff_ids == null)
            {
                goto Label_0084;
            }
            strArray = json.buff_ids;
            num = 0;
            goto Label_007B;
        Label_0067:
            str = strArray[num];
            this.mBuffIdLists.Add(str);
            num += 1;
        Label_007B:
            if (num < ((int) strArray.Length))
            {
                goto Label_0067;
            }
        Label_0084:
            this.mCondIdLists.Clear();
            if (json.cond_ids == null)
            {
                goto Label_00CD;
            }
            strArray2 = json.cond_ids;
            num2 = 0;
            goto Label_00C2;
        Label_00AA:
            str2 = strArray2[num2];
            this.mCondIdLists.Add(str2);
            num2 += 1;
        Label_00C2:
            if (num2 < ((int) strArray2.Length))
            {
                goto Label_00AA;
            }
        Label_00CD:
            return;
        }

        public string Iname
        {
            get
            {
                return this.mIname;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
        }

        public string Expr
        {
            get
            {
                return this.mExpr;
            }
        }

        public string Icon
        {
            get
            {
                return this.mIcon;
            }
        }

        public string Effect
        {
            get
            {
                return this.mEffect;
            }
        }

        public List<string> BuffIdLists
        {
            get
            {
                return this.mBuffIdLists;
            }
        }

        public List<string> CondIdLists
        {
            get
            {
                return this.mCondIdLists;
            }
        }
    }
}

