namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class LimitedShopGiftWindow : MonoBehaviour
    {
        [SerializeField]
        private GameObject ItemParent;
        [SerializeField]
        private GameObject ItemTemplate;
        private List<GameObject> mItems;

        public LimitedShopGiftWindow()
        {
            this.mItems = new List<GameObject>();
            base..ctor();
            return;
        }

        public GameObject InstantiateItem<BindType>(GameObject template, Transform parent, BindType item)
        {
            GameObject obj2;
            obj2 = Object.Instantiate<GameObject>(template);
            obj2.get_transform().SetParent(parent, 0);
            DataSource.Bind<BindType>(obj2, item);
            this.mItems.Add(obj2);
            obj2.SetActive(1);
            return obj2;
        }

        private void Refresh()
        {
            LimitedShopData data;
            Transform transform;
            LimitedShopItem item;
            List<Json_ShopItemDesc> list;
            Json_ShopItemDesc desc;
            int num;
            Json_ShopItemDesc desc2;
            GameObject obj2;
            string str;
            ArtifactParam param;
            ConceptCardData data2;
            ConceptCardIcon icon;
            ItemData data3;
            LimitedShopGiftItem item2;
            <Refresh>c__AnonStorey357 storey;
            storey = new <Refresh>c__AnonStorey357();
            GameUtility.DestroyGameObjects(this.mItems);
            this.mItems.Clear();
            data = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
            if (((data != null) && (data.items.Count > 0)) && ((this.ItemTemplate == null) == null))
            {
                goto Label_0056;
            }
            return;
        Label_0056:
            storey.shopdata_index = GlobalVars.ShopBuyIndex;
            transform = ((this.ItemParent != null) == null) ? this.ItemTemplate.get_transform().get_parent() : this.ItemParent.get_transform();
            item = Enumerable.FirstOrDefault<LimitedShopItem>(data.items, new Func<LimitedShopItem, bool>(storey.<>m__358));
            list = new List<Json_ShopItemDesc>();
            if (item.IsArtifact == null)
            {
                goto Label_00FE;
            }
            desc = new Json_ShopItemDesc();
            desc.iname = item.iname;
            desc.itype = ShopData.ShopItemType2String(item.ShopItemType);
            desc.num = item.num;
            list.Add(desc);
            goto Label_0123;
        Label_00FE:
            if (item.children == null)
            {
                goto Label_0123;
            }
            if (((int) item.children.Length) <= 0)
            {
                goto Label_0123;
            }
            list.AddRange(item.children);
        Label_0123:
            if (list.Count <= 0)
            {
                goto Label_0283;
            }
            num = 0;
            goto Label_0276;
        Label_0137:
            desc2 = list[num];
            obj2 = null;
            str = string.Empty;
            if (desc2.IsArtifact == null)
            {
                goto Label_019A;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(desc2.iname);
            if (param != null)
            {
                goto Label_017B;
            }
            goto Label_0270;
        Label_017B:
            obj2 = this.InstantiateItem<ArtifactParam>(this.ItemTemplate, transform, param);
            str = param.name;
            goto Label_024A;
        Label_019A:
            if (desc2.IsConceptCard == null)
            {
                goto Label_0203;
            }
            data2 = ConceptCardData.CreateConceptCardDataForDisplay(desc2.iname);
            if (data2 != null)
            {
                goto Label_01C0;
            }
            goto Label_0270;
        Label_01C0:
            obj2 = this.InstantiateItem<ConceptCardData>(this.ItemTemplate, transform, data2);
            icon = obj2.GetComponentInChildren<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_01F0;
            }
            icon.Setup(data2);
        Label_01F0:
            str = data2.Param.name;
            goto Label_024A;
        Label_0203:
            data3 = new ItemData();
            if (data3.Setup(0L, desc2.iname, desc2.num) != null)
            {
                goto Label_022B;
            }
            goto Label_0270;
        Label_022B:
            obj2 = this.InstantiateItem<ItemData>(this.ItemTemplate, transform, data3);
            str = data3.Param.name;
        Label_024A:
            item2 = obj2.GetComponent<LimitedShopGiftItem>();
            if ((item2 != null) == null)
            {
                goto Label_0270;
            }
            item2.SetShopItemInfo(desc2, str, GlobalVars.ShopBuyAmount);
        Label_0270:
            num += 1;
        Label_0276:
            if (num < list.Count)
            {
                goto Label_0137;
            }
        Label_0283:
            GameParameter.UpdateAll(transform.get_gameObject());
            return;
        }

        private void Start()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.ItemTemplate.SetActive(0);
        Label_001D:
            this.Refresh();
            return;
        }

        [CompilerGenerated]
        private sealed class <Refresh>c__AnonStorey357
        {
            internal int shopdata_index;

            public <Refresh>c__AnonStorey357()
            {
                base..ctor();
                return;
            }

            internal bool <>m__358(LimitedShopItem item)
            {
                return (item.id == this.shopdata_index);
            }
        }
    }
}

