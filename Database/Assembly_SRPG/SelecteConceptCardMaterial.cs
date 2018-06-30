namespace SRPG
{
    using System;

    public class SelecteConceptCardMaterial
    {
        public OLong mUniqueID;
        public ConceptCardData mSelectedData;
        public int mSelectNum;

        public SelecteConceptCardMaterial()
        {
            base..ctor();
            return;
        }

        public string iname
        {
            get
            {
                if (this.mSelectedData != null)
                {
                    goto Label_000D;
                }
                return null;
            Label_000D:
                return this.mSelectedData.Param.iname;
            }
        }
    }
}

