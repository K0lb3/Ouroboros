// Decompiled with JetBrains decompiler
// Type: SRPG.ProductList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(101, "LimitReached", FlowNode.PinTypes.Output, 0)]
  [AddComponentMenu("Payment/ProductList")]
  [FlowNode.Pin(100, "選択された", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(0, "表示", FlowNode.PinTypes.Input, 0)]
  public class ProductList : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    [Description("リストアイテム(VIP)として使用するゲームオブジェクト")]
    public GameObject ItemVipTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;

    public ProductList()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Awake()
    {
      ((Behaviour) this).set_enabled(true);
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.DetailTemplate, (Object) null) && this.DetailTemplate.get_activeInHierarchy())
        this.DetailTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.ItemVipTemplate, (Object) null) || !this.ItemVipTemplate.get_activeInHierarchy())
        return;
      this.ItemVipTemplate.SetActive(false);
    }

    private void Start()
    {
      this.RefreshItems(true);
    }

    public void Refresh()
    {
      this.RefreshItems(false);
      if (!Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        return;
      ListExtras component = (ListExtras) ((Component) this.ScrollRect).GetComponent<ListExtras>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetScrollPos(1f);
      else
        this.ScrollRect.set_normalizedPosition(Vector2.get_one());
    }

    private void RefreshItems(bool is_start)
    {
      Transform transform = ((Component) this).get_transform();
      for (int index = transform.get_childCount() - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!Object.op_Equality((Object) child, (Object) null) && ((Component) child).get_gameObject().get_activeSelf())
          Object.DestroyImmediate((Object) ((Component) child).get_gameObject());
      }
      PaymentManager.Product[] products = FlowNode_PaymentGetProducts.Products;
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || products == null)
        return;
      for (int index = 0; index < products.Length; ++index)
      {
        GameObject gameObject = !products[index].productID.Contains("sub") ? (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate) : (GameObject) Object.Instantiate<GameObject>((M0) this.ItemVipTemplate);
        ((Object) gameObject).set_hideFlags((HideFlags) 52);
        DataSource.Bind<PaymentManager.Product>(gameObject, products[index]);
        if (!is_start)
        {
          ListItemEvents component = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
          }
        }
        gameObject.get_transform().SetParent(transform, false);
        gameObject.get_gameObject().SetActive(true);
      }
    }

    private void OnSelectItem(GameObject go)
    {
      PaymentManager.Product dataOfClass = DataSource.FindDataOfClass<PaymentManager.Product>(go, (PaymentManager.Product) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.enabled == 0)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      else
      {
        GlobalVars.SelectedProductID = dataOfClass.productID;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!Object.op_Inequality((Object) this.mDetailInfo, (Object) null))
        return;
      Object.DestroyImmediate((Object) this.mDetailInfo.get_gameObject());
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      PaymentManager.Product dataOfClass = DataSource.FindDataOfClass<PaymentManager.Product>(go, (PaymentManager.Product) null);
      if (!Object.op_Equality((Object) this.mDetailInfo, (Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = (GameObject) Object.Instantiate<GameObject>((M0) this.DetailTemplate);
      DataSource.Bind<PaymentManager.Product>(this.mDetailInfo, dataOfClass);
      this.mDetailInfo.SetActive(true);
    }

    private void Update()
    {
    }
  }
}
