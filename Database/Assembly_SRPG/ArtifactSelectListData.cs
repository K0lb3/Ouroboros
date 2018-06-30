namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class ArtifactSelectListData
    {
        public List<ArtifactSelectListItemData> items;

        public ArtifactSelectListData()
        {
            base..ctor();
            return;
        }

        public void Deserialize(Json_ArtifactSelectResponse json)
        {
            int num;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (json.select != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            this.items = new List<ArtifactSelectListItemData>();
            num = 0;
            goto Label_0083;
        Label_0025:
            this.items.Add(new ArtifactSelectListItemData());
            this.items[num].Deserialize(json.select[num]);
            this.items[num].param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.items[num].iname);
            num += 1;
        Label_0083:
            if (num < ((int) json.select.Length))
            {
                goto Label_0025;
            }
            return;
        }
    }
}

