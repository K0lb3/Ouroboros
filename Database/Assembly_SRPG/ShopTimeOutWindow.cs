namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ShopTimeOutWindow : MonoBehaviour
    {
        [SerializeField]
        private GameObject ItemParent;
        [SerializeField]
        private GameObject ItemTemplate;
        private List<GameObject> mItems;

        public ShopTimeOutWindow()
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

        private unsafe void Refresh()
        {
            Transform transform;
            ShopItem item;
            List<ShopItem>.Enumerator enumerator;
            GameObject obj2;
            string str;
            ArtifactParam param;
            ConceptCardData data;
            ConceptCardIcon icon;
            ItemData data2;
            ShopTimeOutItem item2;
            GameUtility.DestroyGameObjects(this.mItems);
            this.mItems.Clear();
            transform = ((this.ItemParent != null) == null) ? this.ItemTemplate.get_transform().get_parent() : this.ItemParent.get_transform();
            if (GlobalVars.TimeOutShopItems == null)
            {
                goto Label_01B4;
            }
            if (GlobalVars.TimeOutShopItems.Count <= 0)
            {
                goto Label_01B4;
            }
            enumerator = GlobalVars.TimeOutShopItems.GetEnumerator();
        Label_006D:
            try
            {
                goto Label_0197;
            Label_0072:
                item = &enumerator.Current;
                obj2 = null;
                str = string.Empty;
                if (item.IsArtifact == null)
                {
                    goto Label_00CF;
                }
                param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname);
                if (param != null)
                {
                    goto Label_00B1;
                }
                goto Label_0197;
            Label_00B1:
                obj2 = this.InstantiateItem<ArtifactParam>(this.ItemTemplate, transform, param);
                str = param.name;
                goto Label_0178;
            Label_00CF:
                if (item.IsConceptCard == null)
                {
                    goto Label_0134;
                }
                data = ConceptCardData.CreateConceptCardDataForDisplay(item.iname);
                if (data != null)
                {
                    goto Label_00F3;
                }
                goto Label_0197;
            Label_00F3:
                obj2 = this.InstantiateItem<ConceptCardData>(this.ItemTemplate, transform, data);
                icon = obj2.GetComponentInChildren<ConceptCardIcon>();
                if ((icon != null) == null)
                {
                    goto Label_0121;
                }
                icon.Setup(data);
            Label_0121:
                str = data.Param.name;
                goto Label_0178;
            Label_0134:
                data2 = new ItemData();
                if (data2.Setup(0L, item.iname, item.num) != null)
                {
                    goto Label_015A;
                }
                goto Label_0197;
            Label_015A:
                obj2 = this.InstantiateItem<ItemData>(this.ItemTemplate, transform, data2);
                str = data2.Param.name;
            Label_0178:
                item2 = obj2.GetComponent<ShopTimeOutItem>();
                if ((item2 != null) == null)
                {
                    goto Label_0197;
                }
                item2.SetShopItemInfo(item, str);
            Label_0197:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0072;
                }
                goto Label_01B4;
            }
            finally
            {
            Label_01A8:
                ((List<ShopItem>.Enumerator) enumerator).Dispose();
            }
        Label_01B4:
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
    }
}

