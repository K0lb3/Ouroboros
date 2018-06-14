// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.IPurchaseFlowImpl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.Purchase
{
  public interface IPurchaseFlowImpl
  {
    void Init(string[] productIds);

    void UpdateProducts(string[] productIds);

    void Resume();

    bool Confirmed();

    bool Purchase(ProductInfo product);

    void Consume(string transactionId);
  }
}
