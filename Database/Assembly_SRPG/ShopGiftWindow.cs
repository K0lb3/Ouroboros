namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class ShopGiftWindow : MonoBehaviour
    {
        [SerializeField]
        private GameObject ItemParent;
        [SerializeField]
        private GameObject ItemTemplate;
        private List<GameObject> mItems;

        public ShopGiftWindow()
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
            ShopData data;
            Transform transform;
            ShopItem item;
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
            ShopGiftItem item2;
            <Refresh>c__AnonStorey3A3 storeya;
            storeya = new <Refresh>c__AnonStorey3A3();
            GameUtility.DestroyGameObjects(this.mItems);
            this.mItems.Clear();
            data = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType);
            if (((data != null) && (data.items.Count > 0)) && ((this.ItemTemplate == null) == null))
            {
                goto Label_005B;
            }
            return;
        Label_005B:
            storeya.shopdata_index = GlobalVars.ShopBuyIndex;
            transform = ((this.ItemParent != null) == null) ? this.ItemTemplate.get_transform().get_parent() : this.ItemParent.get_transform();
            item = Enumerable.FirstOrDefault<ShopItem>(data.items, new Func<ShopItem, bool>(storeya.<>m__407));
            list = new List<Json_ShopItemDesc>();
            if (item.IsArtifact == null)
            {
                goto Label_0103;
            }
            desc = new Json_ShopItemDesc();
            desc.iname = item.iname;
            desc.itype = ShopData.ShopItemType2String(item.ShopItemType);
            desc.num = item.num;
            list.Add(desc);
            goto Label_0128;
        Label_0103:
            if (item.children == null)
            {
                goto Label_0128;
            }
            if (((int) item.children.Length) <= 0)
            {
                goto Label_0128;
            }
            list.AddRange(item.children);
        Label_0128:
            if (list.Count <= 0)
            {
                goto Label_0288;
            }
            num = 0;
            goto Label_027B;
        Label_013C:
            desc2 = list[num];
            obj2 = null;
            str = string.Empty;
            if (desc2.IsArtifact == null)
            {
                goto Label_019F;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(desc2.iname);
            if (param != null)
            {
                goto Label_0180;
            }
            goto Label_0275;
        Label_0180:
            obj2 = this.InstantiateItem<ArtifactParam>(this.ItemTemplate, transform, param);
            str = param.name;
            goto Label_024F;
        Label_019F:
            if (desc2.IsConceptCard == null)
            {
                goto Label_0208;
            }
            data2 = ConceptCardData.CreateConceptCardDataForDisplay(desc2.iname);
            if (data2 != null)
            {
                goto Label_01C5;
            }
            goto Label_0275;
        Label_01C5:
            obj2 = this.InstantiateItem<ConceptCardData>(this.ItemTemplate, transform, data2);
            icon = obj2.GetComponentInChildren<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_01F5;
            }
            icon.Setup(data2);
        Label_01F5:
            str = data2.Param.name;
            goto Label_024F;
        Label_0208:
            data3 = new ItemData();
            if (data3.Setup(0L, desc2.iname, desc2.num) != null)
            {
                goto Label_0230;
            }
            goto Label_0275;
        Label_0230:
            obj2 = this.InstantiateItem<ItemData>(this.ItemTemplate, transform, data3);
            str = data3.Param.name;
        Label_024F:
            item2 = obj2.GetComponent<ShopGiftItem>();
            if ((item2 != null) == null)
            {
                goto Label_0275;
            }
            item2.SetShopItemInfo(desc2, str, GlobalVars.ShopBuyAmount);
        Label_0275:
            num += 1;
        Label_027B:
            if (num < list.Count)
            {
                goto Label_013C;
            }
        Label_0288:
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
        private sealed class <Refresh>c__AnonStorey3A3
        {
            internal int shopdata_index;

            public <Refresh>c__AnonStorey3A3()
            {
                base..ctor();
                return;
            }

            internal bool <>m__407(ShopItem item)
            {
                return (item.id == this.shopdata_index);
            }
        }
    }
}

