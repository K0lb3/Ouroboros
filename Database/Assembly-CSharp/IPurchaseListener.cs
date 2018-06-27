// Decompiled with JetBrains decompiler
// Type: IPurchaseListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public interface IPurchaseListener
{
  void OnInitResult(int resultCode);

  void OnProductResult(int resultCode, PurchaseKit.ProductResponse response);

  void OnPurchaseResult(int resultCode, PurchaseKit.PurchaseResponse response);
}
