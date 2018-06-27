// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.PurchaseFlow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace Gsc.Purchase
{
  public class PurchaseFlow
  {
    public static PurchaseFlow Instance = new PurchaseFlow();
    private static PurchaseHandler _Handler = PurchaseHandler.Instance;

    private PurchaseFlow()
    {
    }

    public static bool initialized
    {
      get
      {
        if (PurchaseFlow._Handler != null)
          return PurchaseFlow._Handler.initialized;
        return false;
      }
    }

    public static bool unavailabled
    {
      get
      {
        if (PurchaseFlow._Handler != null)
          return PurchaseFlow._Handler.unavailabled;
        return false;
      }
    }

    public static ProductInfo[] ProductList
    {
      get
      {
        if (PurchaseFlow._Handler != null)
          return PurchaseFlow._Handler.ProductList;
        return (ProductInfo[]) null;
      }
    }

    public static IPurchaseGlobalListener Listener { get; private set; }

    public static void Init(string[] productIds, IPurchaseGlobalListener listener)
    {
      Debug.Log((object) ((object[]) productIds).ToStringFull());
      Debug.Log((object) PurchaseFlow.initialized);
      if (PurchaseFlow.initialized)
        return;
      PurchaseFlow.Listener = listener;
      PurchaseFlow._Handler.Init(productIds);
    }

    public static void UpdateProducts(string[] productIds)
    {
      if (!PurchaseFlow.initialized)
        return;
      PurchaseFlow._Handler.UpdateProducts(productIds);
    }

    public static void Resume()
    {
      if (!PurchaseFlow.initialized)
        return;
      PurchaseFlow._Handler.Resume();
    }

    public static void LaunchFlow<T>(T listener) where T : MonoBehaviour, IPurchaseFlowListener
    {
      PurchaseFlow.LaunchFlow<T>(listener, false);
    }

    public static void LaunchFlow<T>(T listener, bool enableInactiveCallback) where T : MonoBehaviour, IPurchaseFlowListener
    {
      if (!PurchaseFlow.initialized)
        return;
      PurchaseFlow._Handler.Launch((IPurchaseFlowListener) listener, enableInactiveCallback);
    }

    public bool Purchase(string productId)
    {
      return PurchaseFlow._Handler.Purchase(productId);
    }

    public void InputBirthday(int year, int month, int date)
    {
      PurchaseFlow._Handler.InputBirthday(year, month, date);
    }

    public void Confirmed(bool isOK)
    {
      PurchaseFlow._Handler.Confirmed(isOK);
    }

    public static bool IsEnable(ProductInfo productInfo)
    {
      if (PurchaseFlow._Handler.HasCreditLimit)
        return (double) productInfo.Price <= (double) PurchaseFlow._Handler.CreditLimit;
      return true;
    }
  }
}
