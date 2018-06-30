namespace SRPG
{
    using System;

    public class TrickSetting
    {
        public string mId;
        public OInt mGx;
        public OInt mGy;
        public string mTag;

        public TrickSetting()
        {
            base..ctor();
            return;
        }

        public TrickSetting(JSON_MapTrick json)
        {
            base..ctor();
            this.mId = json.id;
            this.mGx = json.gx;
            this.mGy = json.gy;
            this.mTag = json.tag;
            return;
        }
    }
}

