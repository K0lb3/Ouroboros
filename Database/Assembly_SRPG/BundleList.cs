// Decompiled with JetBrains decompiler
// Type: SRPG.BundleList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 0)]
  [AddComponentMenu("Payment/BundleList")]
  public class BundleList : MonoBehaviour, IFlowInterface
  {
    [Description("Gameobject used as list item")]
    public GameObject ItemTemplate;
    [Description("Gameobject used as detailed screen")]
    public GameObject DetailTemplate;
    [Description("Gameobject used as list item (VIP)")]
    public GameObject ItemVipTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;

    public BundleList()
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
      this.RefreshItems();
    }

    public void Refresh()
    {
      this.RefreshItems();
      if (!Object.op_Inequality((Object) this.ScrollRect, (Object) null))
        return;
      ListExtras component = (ListExtras) ((Component) this.ScrollRect).GetComponent<ListExtras>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetScrollPos(1f);
      else
        this.ScrollRect.set_normalizedPosition(Vector2.get_one());
    }

    private void RefreshItems()
    {
      Transform transform = ((Component) this).get_transform();
      for (int index = transform.get_childCount() - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!Object.op_Equality((Object) child, (Object) null) && ((Component) child).get_gameObject().get_activeSelf())
          Object.DestroyImmediate((Object) ((Component) child).get_gameObject());
      }
      PaymentManager.Bundle[] bundles = FlowNode_PaymentGetBundles.Bundles;
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || bundles == null)
        return;
      GameObject[] gameObjectArray = new GameObject[bundles.Length];
      for (int index = 0; index < bundles.Length; ++index)
      {
        gameObjectArray[index] = !bundles[index].productID.Contains("vip") ? (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate) : (GameObject) Object.Instantiate<GameObject>((M0) this.ItemVipTemplate);
        ((Object) gameObjectArray[index]).set_hideFlags((HideFlags) 52);
        DataSource.Bind<PaymentManager.Bundle>(gameObjectArray[index], bundles[index]);
        ListItemEvents component = (ListItemEvents) gameObjectArray[index].GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
          component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
          component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        }
        gameObjectArray[index].get_transform().SetParent(transform, false);
        gameObjectArray[index].get_gameObject().SetActive(true);
        gameObjectArray[index].get_transform().SetSiblingIndex(bundles[index].displayOrder);
      }
      for (int index = 0; index < gameObjectArray.Length; ++index)
      {
        if (gameObjectArray[index].get_transform().GetSiblingIndex() != bundles[index].displayOrder)
          gameObjectArray[index].get_transform().SetSiblingIndex(bundles[index].displayOrder);
      }
    }

    private void OnSelectItem(GameObject go)
    {
      PaymentManager.Bundle dataOfClass = DataSource.FindDataOfClass<PaymentManager.Bundle>(go, (PaymentManager.Bundle) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedProductID = dataOfClass.productID;
      GlobalVars.SelectedProductPrice = dataOfClass.price;
      GlobalVars.SelectedProductIcon = dataOfClass.iconImage;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
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
