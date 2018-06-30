namespace SRPG
{
    using System;

    public class TobiraCategoriesParam
    {
        private TobiraParam.Category mCategory;
        private string mName;

        public TobiraCategoriesParam()
        {
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TobiraCategoriesParam json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mCategory = json.category;
            this.mName = json.name;
            return;
        }

        public TobiraParam.Category TobiraCategory
        {
            get
            {
                return this.mCategory;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
        }
    }
}

