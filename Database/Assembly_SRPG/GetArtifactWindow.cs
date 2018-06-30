namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(1, "Refresh", 0, 1), Pin(100, "武具選択", 1, 100), Pin(0x65, "武具更新", 1, 0x65)]
    public class GetArtifactWindow : SRPG_FixedList, IFlowInterface
    {
        public RectTransform ItemLayoutParent;
        public GameObject ItemTemplate;
        private ArtifactSelectListItemData[] mArtifactListItem;

        public GetArtifactWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        protected override void Awake()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_002D;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_002D;
            }
            this.ItemTemplate.SetActive(0);
        Label_002D:
            return;
        }

        public override void BindData()
        {
            int num;
            int num2;
            num = 0;
            goto Label_00C8;
        Label_0007:
            num2 = ((base.mPage * base.mPageSize) + num) - ((int) base.ExtraItems.Length);
            if (0 > num2)
            {
                goto Label_00B2;
            }
            if (num2 >= ((int) base.Data.Length))
            {
                goto Label_00B2;
            }
            DataSource.Bind(base.mItems[num], base.mDataType, base.Data[num2]);
            DataSource.Bind(base.mItems[num], typeof(ArtifactSelectListItemData), this.mArtifactListItem[num2]);
            this.OnUpdateItem(base.mItems[num], num2);
            base.mItems[num].SetActive(1);
            GameParameter.UpdateAll(base.mItems[num]);
            goto Label_00C4;
        Label_00B2:
            base.mItems[num].SetActive(0);
        Label_00C4:
            num += 1;
        Label_00C8:
            if (num < base.mItems.Count)
            {
                goto Label_0007;
            }
            return;
        }

        protected override GameObject CreateItem()
        {
            return Object.Instantiate<GameObject>(this.ItemTemplate);
        }

        protected override void OnItemSelect(GameObject go)
        {
            ArtifactSelectListItemData data;
            GlobalVars.ArtifactListItem = DataSource.FindDataOfClass<ArtifactSelectListItemData>(go, null);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        public void Refresh(ArtifactSelectListItemData[] data)
        {
            List<ArtifactData> list;
            int num;
            ArtifactData data2;
            Json_Artifact artifact;
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            list = new List<ArtifactData>();
            num = 0;
            goto Label_005E;
        Label_001F:
            data2 = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iname = data[num].iname;
            artifact.rare = data[num].param.rareini;
            data2.Deserialize(artifact);
            list.Add(data2);
            num += 1;
        Label_005E:
            if (num < ((int) data.Length))
            {
                goto Label_001F;
            }
            this.SetData(list.ToArray(), typeof(ArtifactData));
            this.mArtifactListItem = data;
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        protected override void Start()
        {
        }
    }
}

