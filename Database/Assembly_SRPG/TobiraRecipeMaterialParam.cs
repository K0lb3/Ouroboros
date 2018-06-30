namespace SRPG
{
    using System;

    public class TobiraRecipeMaterialParam
    {
        private string mIname;
        private int mNum;

        public TobiraRecipeMaterialParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TobiraRecipeMaterialParam json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mIname = json.iname;
            this.mNum = json.num;
            return;
        }

        public string Iname
        {
            get
            {
                return this.mIname;
            }
        }

        public int Num
        {
            get
            {
                return this.mNum;
            }
        }
    }
}

