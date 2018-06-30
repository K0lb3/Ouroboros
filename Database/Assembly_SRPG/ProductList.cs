namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "選択された", 1, 0), AddComponentMenu("Payment/ProductList"), Pin(0, "表示", 0, 0)]
    public class ProductList : MonoBehaviour, IFlowInterface
    {
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public GameObject ItemTemplate;
        [Description("詳細画面として使用するゲームオブジェクト")]
        public GameObject DetailTemplate;
        [Description("リストアイテム(VIP)として使用するゲームオブジェクト")]
        public GameObject ItemVipTemplate;
        private GameObject mDetailInfo;
        public UnityEngine.UI.ScrollRect ScrollRect;

        public ProductList()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_000C;
            }
            this.Refresh();
        Label_000C:
            return;
        }

        private void Awake()
        {
            base.set_enabled(1);
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0034;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0034;
            }
            this.ItemTemplate.SetActive(0);
        Label_0034:
            if ((this.DetailTemplate != null) == null)
            {
                goto Label_0061;
            }
            if (this.DetailTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0061;
            }
            this.DetailTemplate.SetActive(0);
        Label_0061:
            if ((this.ItemVipTemplate != null) == null)
            {
                goto Label_008E;
            }
            if (this.ItemVipTemplate.get_activeInHierarchy() == null)
            {
                goto Label_008E;
            }
            this.ItemVipTemplate.SetActive(0);
        Label_008E:
            return;
        }

        private void OnCloseItemDetail(GameObject go)
        {
            if ((this.mDetailInfo != null) == null)
            {
                goto Label_0028;
            }
            Object.DestroyImmediate(this.mDetailInfo.get_gameObject());
            this.mDetailInfo = null;
        Label_0028:
            return;
        }

        private void OnOpenItemDetail(GameObject go)
        {
            PaymentManager.Product product;
            product = DataSource.FindDataOfClass<PaymentManager.Product>(go, null);
            if ((this.mDetailInfo == null) == null)
            {
                goto Label_0048;
            }
            if (product == null)
            {
                goto Label_0048;
            }
            this.mDetailInfo = Object.Instantiate<GameObject>(this.DetailTemplate);
            DataSource.Bind<PaymentManager.Product>(this.mDetailInfo, product);
            this.mDetailInfo.SetActive(1);
        Label_0048:
            return;
        }

        private void OnSelectItem(GameObject go)
        {
            PaymentManager.Product product;
            product = DataSource.FindDataOfClass<PaymentManager.Product>(go, null);
            if (product == null)
            {
                goto Label_0021;
            }
            GlobalVars.SelectedProductID = product.productID;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_0021:
            return;
        }

        public void Refresh()
        {
            ListExtras extras;
            this.RefreshItems(0);
            if ((this.ScrollRect != null) == null)
            {
                goto Label_0050;
            }
            extras = this.ScrollRect.GetComponent<ListExtras>();
            if ((extras != null) == null)
            {
                goto Label_0040;
            }
            extras.SetScrollPos(1f);
            goto Label_0050;
        Label_0040:
            this.ScrollRect.set_normalizedPosition(Vector2.get_one());
        Label_0050:
            return;
        }

        private void RefreshItems(bool is_start)
        {
            Transform transform;
            int num;
            Transform transform2;
            PaymentManager.Product[] productArray;
            int num2;
            bool flag;
            GameObject obj2;
            ListItemEvents events;
            transform = base.get_transform();
            num = transform.get_childCount() - 1;
            goto Label_004D;
        Label_0015:
            transform2 = transform.GetChild(num);
            if ((transform2 == null) == null)
            {
                goto Label_002E;
            }
            goto Label_0049;
        Label_002E:
            if (transform2.get_gameObject().get_activeSelf() == null)
            {
                goto Label_0049;
            }
            Object.DestroyImmediate(transform2.get_gameObject());
        Label_0049:
            num -= 1;
        Label_004D:
            if (num >= 0)
            {
                goto Label_0015;
            }
            productArray = FlowNode_PaymentGetProducts.Products;
            if (((this.ItemTemplate == null) == null) && (productArray != null))
            {
                goto Label_0072;
            }
            return;
        Label_0072:
            num2 = 0;
            goto Label_015D;
        Label_007A:
            obj2 = ((((productArray[num2].productID == MonoSingleton<GameManager>.Instance.MasterParam.FixParam.VipCardProduct) == null) ? 0 : 1) == null) ? Object.Instantiate<GameObject>(this.ItemTemplate) : Object.Instantiate<GameObject>(this.ItemVipTemplate);
            obj2.set_hideFlags(0x34);
            DataSource.Bind<PaymentManager.Product>(obj2, productArray[num2]);
            if (is_start != null)
            {
                goto Label_013C;
            }
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_013C;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            events.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        Label_013C:
            obj2.get_transform().SetParent(transform, 0);
            obj2.get_gameObject().SetActive(1);
            num2 += 1;
        Label_015D:
            if (num2 < ((int) productArray.Length))
            {
                goto Label_007A;
            }
            return;
        }

        private void Start()
        {
            this.RefreshItems(1);
            return;
        }

        private void Update()
        {
        }
    }
}

