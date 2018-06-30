namespace SRPG
{
    using GR;
    using System;

    public class MapParam
    {
        public string mapSceneName;
        public string mapSetName;
        private short battleSceneName_index;
        public string eventSceneName;
        private short bgmName_index;

        public MapParam()
        {
            this.battleSceneName_index = -1;
            this.bgmName_index = -1;
            base..ctor();
            return;
        }

        public void Deserialize(JSON_MapParam json)
        {
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.mapSceneName = json.scn;
            this.mapSetName = json.set;
            this.battleSceneName = json.btl;
            this.eventSceneName = json.ev;
            this.bgmName = json.bgm;
            return;
        }

        public string battleSceneName
        {
            get
            {
                return Singleton<ShareVariable>.Instance.str.Get(7, this.battleSceneName_index);
            }
            set
            {
                this.battleSceneName_index = Singleton<ShareVariable>.Instance.str.Set(7, value);
                return;
            }
        }

        public string bgmName
        {
            get
            {
                return Singleton<ShareVariable>.Instance.str.Get(8, this.bgmName_index);
            }
            set
            {
                this.bgmName_index = Singleton<ShareVariable>.Instance.str.Set(8, value);
                return;
            }
        }
    }
}

