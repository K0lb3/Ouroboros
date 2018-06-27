// Decompiled with JetBrains decompiler
// Type: PurchaseService
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class PurchaseService : MonoBehaviour
{
  private static bool _Initialized;

  public PurchaseService()
  {
    base.\u002Ector();
  }

  public static bool Initialized
  {
    get
    {
      return PurchaseService._Initialized;
    }
    private set
    {
      PurchaseService._Initialized = value;
      Debug.Log((object) ("::::: " + (object) value));
    }
  }

  private static PurchaseService Instance { get; set; }

  private void Awake()
  {
    PurchaseService.Instance = this;
  }

  public void Init(string[] productIds, PurchaseKit.Logger logger, IntPtr nativeLogger)
  {
    PurchaseBridge.unity_purchasekit_attach(((Object) ((Component) this).get_gameObject()).get_name(), PurchaseBridge.SetLogger(logger), nativeLogger);
    if (productIds != null)
      PurchaseBridge.purchasekit_initWithProducts(productIds, productIds.Length);
    else
      PurchaseBridge.purchasekit_init();
  }

  private void __PurchaseKit__onInitResult__(string message)
  {
    IntPtr int64 = (IntPtr) Convert.ToInt64(message, 16);
    PurchaseBridge.UnmanagedResult structure = PurchaseBridge.MarshalSupport.ToStructure<PurchaseBridge.UnmanagedResult>(int64);
    PurchaseBridge.unity_purchasekit_purge_cs_init_message(ref int64);
    PurchaseKit.Listener.OnInitResult((int) structure.resultCode);
  }

  private void __PurchaseKit__onProductResult__(string message)
  {
    IntPtr int64 = (IntPtr) Convert.ToInt64(message, 16);
    PurchaseBridge.UnmanagedResult structure = PurchaseBridge.MarshalSupport.ToStructure<PurchaseBridge.UnmanagedResult>(int64);
    PurchaseKit.ProductResponse response = structure.response <= 0UL ? (PurchaseKit.ProductResponse) null : new PurchaseKit.ProductResponse((IntPtr) ((long) structure.response));
    PurchaseBridge.unity_purchasekit_purge_cs_product_message(ref int64);
    PurchaseKit.Listener.OnProductResult((int) structure.resultCode, response);
  }

  private void __PurchaseKit__onPurchaseResult__(string message)
  {
    IntPtr int64 = (IntPtr) Convert.ToInt64(message, 16);
    PurchaseBridge.UnmanagedResult structure = PurchaseBridge.MarshalSupport.ToStructure<PurchaseBridge.UnmanagedResult>(int64);
    PurchaseKit.PurchaseResponse response = structure.response <= 0UL ? (PurchaseKit.PurchaseResponse) null : new PurchaseKit.PurchaseResponse((IntPtr) ((long) structure.response));
    PurchaseBridge.unity_purchasekit_purge_cs_purchase_message(ref int64);
    PurchaseKit.Listener.OnPurchaseResult((int) structure.resultCode, response);
  }
}
