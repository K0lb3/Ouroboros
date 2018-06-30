namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(100, "アイテム選択単品", 1, 100), Pin(1, "Refresh", 0, 1), Pin(2, "Refresh完了", 1, 2), Pin(0x65, "アイテム選択セット", 1, 0x65), Pin(0x66, "アイテム更新", 1, 0x66), Pin(0x67, "武具選択単品", 1, 0x67)]
    public class ShopBuyWindow : MonoBehaviour, IFlowInterface
    {
        public RectTransform ItemLayoutParent;
        public GameObject ItemTemplate;
        public Button BtnUpdated;
        public Text TxtTitle;
        public Text TxtUpdateTime;
        public Text TxtUpdated;
        public GameObject Updated;
        public GameObject Update;
        public GameObject TitleObj;
        public List<GameObject> mBuyItems;
        public GameObject Lineup;

        public ShopBuyWindow()
        {
            this.mBuyItems = new List<GameObject>(12);
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            this.Refresh();
            return;
        }

        private void Awake()
        {
            EShopType type;
            EShopType type2;
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
            if ((this.BtnUpdated != null) == null)
            {
                goto Label_005A;
            }
            this.BtnUpdated.get_onClick().AddListener(new UnityAction(this, this.OnItemUpdated));
        Label_005A:
            if ((this.TxtTitle != null) == null)
            {
                goto Label_0080;
            }
            this.TxtTitle.set_text(LocalizedText.Get("sys.SHOP_BUY_TITLE"));
        Label_0080:
            if ((this.TxtUpdateTime != null) == null)
            {
                goto Label_00A6;
            }
            this.TxtUpdateTime.set_text(LocalizedText.Get("sys.UPDATE_TIME"));
        Label_00A6:
            if ((this.TxtUpdated != null) == null)
            {
                goto Label_00CC;
            }
            this.TxtUpdated.set_text(LocalizedText.Get("sys.CMD_UPDATED"));
        Label_00CC:
            type2 = GlobalVars.ShopType;
            switch (type2)
            {
                case 0:
                    goto Label_00F3;

                case 1:
                    goto Label_00F3;

                case 2:
                    goto Label_00F3;
            }
            if (type2 == 11)
            {
                goto Label_00F3;
            }
            goto Label_0104;
        Label_00F3:
            this.TitleObj.SetActive(1);
            goto Label_0115;
        Label_0104:
            this.TitleObj.SetActive(0);
        Label_0115:
            return;
        }

        private void OnItemUpdated()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x66);
            return;
        }

        private void OnSelect(GameObject go)
        {
            int num;
            ShopBuyList list;
            <OnSelect>c__AnonStorey3A2 storeya;
            storeya = new <OnSelect>c__AnonStorey3A2();
            storeya.go = go;
            num = this.mBuyItems.FindIndex(new Predicate<GameObject>(storeya.<>m__406));
            list = this.mBuyItems[num].GetComponent<ShopBuyList>();
            GlobalVars.ShopBuyIndex = list.ShopItem.id;
            if (list.ShopItem.IsArtifact == null)
            {
                goto Label_0060;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x67);
            return;
        Label_0060:
            if (list.ShopItem.IsSet != null)
            {
                goto Label_007D;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_0085;
        Label_007D:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
        Label_0085:
            return;
        }

        private void Refresh()
        {
            PlayerData data;
            ShopData data2;
            int num;
            int num2;
            int num3;
            ShopItem item;
            GameObject obj2;
            GameObject obj3;
            ShopBuyList list;
            ArtifactParam param;
            ConceptCardData data3;
            ItemData data4;
            ListItemEvents events;
            Button button;
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            data2 = MonoSingleton<GameManager>.Instance.Player.GetShopData(GlobalVars.ShopType);
            DebugUtility.Assert((data2 == null) == 0, "ショップ情報が存在しない");
            if ((this.Updated != null) == null)
            {
                goto Label_005C;
            }
            this.Updated.SetActive(data2.btn_update);
        Label_005C:
            if ((this.Update != null) == null)
            {
                goto Label_007E;
            }
            this.Update.SetActive(data2.btn_update);
        Label_007E:
            if ((this.Lineup != null) == null)
            {
                goto Label_00A0;
            }
            this.Lineup.SetActive(data2.btn_update);
        Label_00A0:
            num = 0;
            goto Label_00C2;
        Label_00A7:
            this.mBuyItems[num].get_gameObject().SetActive(0);
            num += 1;
        Label_00C2:
            if (num < this.mBuyItems.Count)
            {
                goto Label_00A7;
            }
            num2 = data2.items.Count;
            num3 = 0;
            goto Label_029D;
        Label_00E7:
            item = data2.items[num3];
            if (num3 < this.mBuyItems.Count)
            {
                goto Label_0135;
            }
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.get_transform().SetParent(this.ItemLayoutParent, 0);
            this.mBuyItems.Add(obj2);
        Label_0135:
            obj3 = this.mBuyItems[num3];
            list = obj3.GetComponentInChildren<ShopBuyList>();
            list.ShopItem = item;
            list.SetupDiscountUI();
            DataSource.Bind<ShopItem>(obj3, item);
            if (item.IsArtifact == null)
            {
                goto Label_0198;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(item.iname);
            DataSource.Bind<ArtifactParam>(obj3, param);
            goto Label_023F;
        Label_0198:
            if (item.IsConceptCard == null)
            {
                goto Label_01C0;
            }
            data3 = ConceptCardData.CreateConceptCardDataForDisplay(item.iname);
            list.SetupConceptCard(data3);
            goto Label_023F;
        Label_01C0:
            if (item.IsItem != null)
            {
                goto Label_01D8;
            }
            if (item.IsSet == null)
            {
                goto Label_021D;
            }
        Label_01D8:
            data4 = new ItemData();
            data4.Setup(0L, item.iname, item.num);
            DataSource.Bind<ItemData>(obj3, data4);
            DataSource.Bind<ItemParam>(obj3, MonoSingleton<GameManager>.Instance.GetItemParam(item.iname));
            goto Label_023F;
        Label_021D:
            DebugUtility.LogError(string.Format("不明な商品タイプが設定されています (shopitem.iname({0}) => {1})", item.iname, (EShopItemType) item.ShopItemType));
        Label_023F:
            events = obj3.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0268;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Label_0268:
            button = obj3.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_028F;
            }
            button.set_interactable(item.is_soldout == 0);
        Label_028F:
            obj3.SetActive(1);
            num3 += 1;
        Label_029D:
            if (num3 < num2)
            {
                goto Label_00E7;
            }
            GameParameter.UpdateAll(base.get_gameObject());
            if (GlobalVars.ShopType != 7)
            {
                goto Label_02C5;
            }
            GameParameter.UpdateValuesOfType(0x19f);
        Label_02C5:
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
            return;
        }

        [CompilerGenerated]
        private sealed class <OnSelect>c__AnonStorey3A2
        {
            internal GameObject go;

            public <OnSelect>c__AnonStorey3A2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__406(GameObject p)
            {
                return (p == this.go);
            }
        }
    }
}

