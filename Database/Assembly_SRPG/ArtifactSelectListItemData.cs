namespace SRPG
{
    using System;

    public class ArtifactSelectListItemData
    {
        public string iname;
        public int id;
        public int num;
        public ArtifactParam param;

        public ArtifactSelectListItemData()
        {
            base..ctor();
            return;
        }

        public void Deserialize(Json_ArtifactSelectItem json)
        {
            this.iname = json.iname;
            this.id = json.id;
            this.num = json.num;
            return;
        }
    }
}

