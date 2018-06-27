// Decompiled with JetBrains decompiler
// Type: PurchaseKit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class PurchaseKit
{
  public const int RESULT_SUCCEEDED = 0;
  public const int RESULT_FAILED = 1;
  public const int RESULT_UNAVAILABLED = 2;
  public const int RESULT_CANCELED = 16;
  public const int RESULT_ALREADYOWNED = 17;
  public const int RESULT_DEFERRED = 32;

  internal static IPurchaseListener Listener { get; private set; }

  public static void Init(IPurchaseListener listener, GameObject serviceNode = null, PurchaseKit.Logger logger = null, [Optional] IntPtr nativeLogger)
  {
    PurchaseKit.Init((string[]) null, listener, serviceNode, logger, nativeLogger);
  }

  public static void Init(string[] productIds, IPurchaseListener listener, GameObject serviceNode = null, PurchaseKit.Logger logger = null, [Optional] IntPtr nativeLogger)
  {
    Debug.Log((object) PurchaseService.Initialized);
    if (PurchaseService.Initialized)
      return;
    PurchaseKit.Listener = listener;
    if (Object.op_Equality((Object) serviceNode, (Object) null))
    {
      serviceNode = new GameObject("PurchaseService");
      GameObject gameObject = serviceNode;
      ((Object) gameObject).set_hideFlags((HideFlags) (((Object) gameObject).get_hideFlags() | 1));
      Object.DontDestroyOnLoad((Object) serviceNode);
    }
    PurchaseService purchaseService = (PurchaseService) serviceNode.GetComponent<PurchaseService>();
    if (Object.op_Equality((Object) purchaseService, (Object) null))
      purchaseService = (PurchaseService) serviceNode.AddComponent<PurchaseService>();
    purchaseService.Init(productIds, logger, nativeLogger);
  }

  public static void SetAndroidService(string serviceName)
  {
    PurchaseBridge.purchasekit_setAndroidService(serviceName);
  }

  public static void UpdateProducts(string[] productIds)
  {
    PurchaseBridge.purchasekit_updateProducts(productIds, productIds.Length);
  }

  public static bool Purchase(string productId)
  {
    return PurchaseBridge.purchasekit_purchase(productId);
  }

  public static void Resume()
  {
    PurchaseBridge.purchasekit_resume();
  }

  public static void Consume(string transactionId)
  {
    PurchaseBridge.purchasekit_consume(transactionId);
  }

  public class ProductResponse : PurchaseKit.AbstractResponse<PurchaseKit.MetaProduct, PurchaseKit.ProductData>
  {
    public ProductResponse(IntPtr ptr)
      : base(ptr)
    {
    }
  }

  public class PurchaseResponse : PurchaseKit.AbstractResponse<PurchaseKit.MetaPurchase, PurchaseKit.PurchaseData>
  {
    public PurchaseResponse(IntPtr ptr)
      : base(ptr)
    {
    }
  }

  public abstract class AbstractResponse<TMeta, TValue> where TMeta : PurchaseKit.IContent, new() where TValue : PurchaseKit.IContent, new()
  {
    public readonly TMeta Meta;
    public readonly TValue[] Values;

    public AbstractResponse(IntPtr intptr)
    {
      PurchaseBridge.UnmanagedResponse structure = PurchaseBridge.MarshalSupport.ToStructure<PurchaseBridge.UnmanagedResponse>(intptr);
      if (structure.meta > 0UL)
      {
        this.Meta = new TMeta();
        this.Meta.Parse((IntPtr) ((long) structure.meta));
      }
      this.Values = new TValue[(IntPtr) structure.value_count];
      for (int index = 0; (long) index < (long) structure.value_count; ++index)
      {
        TValue obj = new TValue();
        obj.Parse((IntPtr) ((long) structure.values + (long) ((int) structure.value_size * index)));
        this.Values[index] = obj;
      }
    }
  }

  public interface IContent
  {
    void Parse(IntPtr ptr);
  }

  public class ProductData : PurchaseKit.IContent
  {
    public string ID { get; protected set; }

    public string LocalizedTitle { get; protected set; }

    public string LocalizedDescription { get; protected set; }

    public string LocalizedPrice { get; protected set; }

    public string Currency { get; protected set; }

    public double Price { get; protected set; }

    public void Parse(IntPtr intptr)
    {
      PurchaseBridge.UnmanagedProductData structure = PurchaseBridge.MarshalSupport.ToStructure<PurchaseBridge.UnmanagedProductData>(intptr);
      this.ID = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.id));
      this.LocalizedTitle = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.localizedTitle));
      this.LocalizedDescription = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.localizedDescription));
      this.LocalizedPrice = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.localizedPrice));
      this.Currency = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.currency));
      this.Price = structure.price;
    }
  }

  public class PurchaseData : PurchaseKit.IContent
  {
    public string ID { get; protected set; }

    public string ProductId { get; protected set; }

    public string Data0 { get; protected set; }

    public string Data1 { get; protected set; }

    public void Parse(IntPtr intptr)
    {
      PurchaseBridge.UnmanagedPurchaseData structure = PurchaseBridge.MarshalSupport.ToStructure<PurchaseBridge.UnmanagedPurchaseData>(intptr);
      this.ID = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.id));
      this.ProductId = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.productId));
      this.Data0 = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.data0));
      this.Data1 = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.data1));
    }
  }

  public class MetaProduct : PurchaseKit.IContent
  {
    public void Parse(IntPtr intptr)
    {
    }
  }

  public class MetaPurchase : PurchaseKit.IContent
  {
    public string Data0 { get; protected set; }

    public string Data1 { get; protected set; }

    public void Parse(IntPtr intptr)
    {
      PurchaseBridge.UnmanagedMetaPurchase structure = PurchaseBridge.MarshalSupport.ToStructure<PurchaseBridge.UnmanagedMetaPurchase>(intptr);
      this.Data0 = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.data0));
      this.Data1 = PurchaseBridge.MarshalSupport.ToString((IntPtr) ((long) structure.data1));
    }
  }

  public delegate void Logger(int type, string tag, string message);
}
