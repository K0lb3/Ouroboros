// Decompiled with JetBrains decompiler
// Type: MyMetaps
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System.Text;

public static class MyMetaps
{
  private static readonly Encoding encoding = Encoding.GetEncoding("utf-8");

  private static bool IsEnable
  {
    get
    {
      return false;
    }
  }

  public static void Setup()
  {
  }

  public static void Destroy()
  {
  }

  public static bool TrackEvent(string category, string name)
  {
    return false;
  }

  public static bool TrackEvent(string category, string name, int value)
  {
    return false;
  }

  public static bool TrackPurchase(string productId, string currency, double price = 0)
  {
    return false;
  }

  public static bool TrackSpend(string category, string name, int value)
  {
    return false;
  }

  public static bool Validate(string data)
  {
    return !string.IsNullOrEmpty(data) && MyMetaps.encoding.GetByteCount(data) == data.Length && data.Length <= 64;
  }

  public static bool TrackTutorialBegin()
  {
    return MyMetaps.TrackEvent("tutorial", "start");
  }

  public static bool TrackTutorialPoint(string point)
  {
    return MyMetaps.TrackEvent("tutorial", point);
  }

  public static bool TrackTutorialEnd()
  {
    return MyMetaps.TrackEvent("tutorial", "end");
  }

  public static bool TrackDebugInstall()
  {
    return MyMetaps.TrackEvent("debug_install", "installed");
  }

  public static bool TrackSpendCoin(string name, int value)
  {
    return MyMetaps.TrackSpend(ESaleType.Coin.ToString(), name, value);
  }

  public static bool TrackSpendShop(ESaleType sale_type, EShopType shop_type, int value)
  {
    string name = "ShopBuy." + shop_type.ToString();
    return MyMetaps.TrackSpend(sale_type.ToString(), name, value);
  }

  public static bool TrackSpendShopUpdate(ESaleType sale_type, EShopType shop_type, int value)
  {
    string name = "ShopUpdate." + shop_type.ToString();
    return MyMetaps.TrackSpend(sale_type.ToString(), name, value);
  }
}
