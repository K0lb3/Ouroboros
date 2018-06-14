// Decompiled with JetBrains decompiler
// Type: PurchaseBridge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using AOT;
using System;
using System.Runtime.InteropServices;
using System.Text;

public static class PurchaseBridge
{
  private const string LIBNAME = "purchasekit";
  private static PurchaseKit.Logger _logger;

  public static PurchaseKit.Logger SetLogger(PurchaseKit.Logger logger)
  {
    if (logger == null)
      return (PurchaseKit.Logger) null;
    PurchaseBridge._logger = logger;
    return new PurchaseKit.Logger(PurchaseBridge.OnCSLog);
  }

  [MonoPInvokeCallback(typeof (PurchaseBridge.NativeLogger))]
  private static void OnCSLog(int type, [MarshalAs(UnmanagedType.LPStr)] string tag, [MarshalAs(UnmanagedType.LPStr)] string message)
  {
    if (PurchaseBridge._logger == null)
      return;
    PurchaseBridge._logger(type, tag, message);
  }

  [DllImport("purchasekit")]
  internal static extern void unity_purchasekit_attach(string gameObjName, PurchaseKit.Logger logger, IntPtr nativeLogger);

  [DllImport("purchasekit")]
  internal static extern void unity_purchasekit_purge_cs_init_message(ref IntPtr message);

  [DllImport("purchasekit")]
  internal static extern void unity_purchasekit_purge_cs_product_message(ref IntPtr message);

  [DllImport("purchasekit")]
  internal static extern void unity_purchasekit_purge_cs_purchase_message(ref IntPtr message);

  [DllImport("purchasekit")]
  internal static extern void unity_purchasekit_get_rawdata_from_container(IntPtr container, out IntPtr data, out int size);

  [DllImport("purchasekit")]
  internal static extern void purchasekit_init();

  [DllImport("purchasekit")]
  internal static extern void purchasekit_initWithProducts(string[] productIds, int length);

  [DllImport("purchasekit")]
  internal static extern void purchasekit_updateProducts(string[] productIds, int length);

  [DllImport("purchasekit")]
  internal static extern bool purchasekit_purchase(string productId);

  [DllImport("purchasekit")]
  internal static extern void purchasekit_resume();

  [DllImport("purchasekit")]
  internal static extern void purchasekit_consume(string orderId);

  [DllImport("purchasekit")]
  internal static extern void purchasekit_setAndroidService(string serviceName);

  public static class MarshalSupport
  {
    public static string ToString(IntPtr ptr)
    {
      if (ptr != IntPtr.Zero)
      {
        IntPtr data;
        int size;
        PurchaseBridge.unity_purchasekit_get_rawdata_from_container(ptr, out data, out size);
        if (data != IntPtr.Zero && size > 0)
        {
          byte[] numArray = new byte[size];
          Marshal.Copy(data, numArray, 0, size);
          return Encoding.UTF8.GetString(numArray);
        }
      }
      return (string) null;
    }

    public static T ToStructure<T>(IntPtr ptr)
    {
      return (T) Marshal.PtrToStructure(ptr, typeof (T));
    }
  }

  internal struct UnmanagedResult
  {
    public readonly ulong resultCode;
    public readonly ulong response;
  }

  internal struct UnmanagedResponse
  {
    public readonly uint value_count;
    public readonly ushort meta_size;
    public readonly ushort value_size;
    public readonly ulong meta;
    public readonly ulong values;
    private readonly ulong _original;
  }

  internal struct UnmanagedProductData
  {
    public readonly ulong id;
    public readonly ulong localizedTitle;
    public readonly ulong localizedDescription;
    public readonly ulong localizedPrice;
    public readonly ulong currency;
    [MarshalAs(UnmanagedType.R8)]
    public readonly double price;
  }

  internal struct UnmanagedPurchaseData
  {
    public readonly ulong id;
    public readonly ulong productId;
    public readonly ulong data0;
    public readonly ulong data1;
  }

  internal struct UnmanagedMetaProduct
  {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
    private readonly string _padding;
  }

  internal struct UnmanagedMetaPurchase
  {
    public readonly ulong data0;
    public readonly ulong data1;
  }

  public delegate void NativeLogger(int type, [MarshalAs(UnmanagedType.LPStr)] string tag, [MarshalAs(UnmanagedType.LPStr)] string message);
}
