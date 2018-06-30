namespace SRPG
{
    using System;

    public class ChangeListData
    {
        public int ItemID;
        public Type MetaDataType;
        public object MetaData;
        public string Label;
        public string ValOld;
        public string ValNew;

        public ChangeListData()
        {
            base..ctor();
            return;
        }
    }
}

