// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.IPurchaseFlowListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.Purchase
{
  public interface IPurchaseFlowListener : IPurchaseResultListener
  {
    void InputBirthday(PurchaseFlow flow);

    void Confirm(PurchaseFlow flow, ProductInfo product);

    void OnInvalidBirthday(PurchaseFlow flow);

    void OnProducts(PurchaseFlow flow, ProductInfo[] products);
  }
}
