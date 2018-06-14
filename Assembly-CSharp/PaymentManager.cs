// Decompiled with JetBrains decompiler
// Type: PaymentManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using Gsc.Purchase;
using SRPG;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PaymentManager : MonoSingleton<PaymentManager>
{
  private Dictionary<string, ProductInfo> ProductOnStores = new Dictionary<string, ProductInfo>();
  private PaymentManager.ERequestPurchaseResult purchaseResult = PaymentManager.ERequestPurchaseResult.NONE;
  private Dictionary<string, ProductInfo> BundleOnStores = new Dictionary<string, ProductInfo>();
  private bool isBundleSetupOK;
  public PaymentManager.ShowBundlesDelegate OnShowBundles;
  private bool isSetupOK;
  public PaymentManager.ShowItemsDelegate OnShowItems;
  public PaymentManager.RequestPurchaseDelegate OnRequestPurchase;
  public PaymentManager.RegisterBirthdayDelegate OnRegisterBirthday;
  public PaymentManager.RequestSucceededDelegate OnRequestSucceeded;
  public PaymentManager.RequestProcessingDelegate OnRequestProcessing;

  private List<BundleParam> BundleMasters { get; set; }

  private List<BundleParam> BundleMastersAll { get; set; }

  public bool IsBundleAvailable
  {
    get
    {
      return this.isBundleSetupOK;
    }
  }

  private string[] BundleIds
  {
    get
    {
      if (this.BundleMasters == null)
        return new string[0];
      string[] strArray = new string[this.BundleMasters.Count];
      int num = 0;
      using (List<BundleParam>.Enumerator enumerator = this.BundleMasters.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          BundleParam current = enumerator.Current;
          strArray[num++] = current.ProductId;
        }
      }
      return strArray;
    }
  }

  public static string BundleApiPath
  {
    get
    {
      return Network.Host + "bundle";
    }
  }

  private BundleParam GetBundleMaster(string productId)
  {
    using (List<BundleParam>.Enumerator enumerator = this.BundleMasters.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        BundleParam current = enumerator.Current;
        if (productId == current.ProductId)
          return current;
      }
    }
    return (BundleParam) null;
  }

  public bool InitOnlyBundleMaster(bool isResetMaster = false, BundleParamResponse res = null)
  {
    this.BundleMastersAll = res.bundles_all;
    if (isResetMaster)
    {
      if (res != null)
        this.BundleMasters = res.bundles;
      else
        this.BundleMasters.Clear();
    }
    return true;
  }

  public bool InitBundle(bool isResetMaster = false, BundleParamResponse res = null)
  {
    PaymentManager.MyDebug.PushMessage("PaymentManager.InitBundle");
    this.BundleMastersAll = res.bundles_all;
    this.isBundleSetupOK = false;
    if (isResetMaster)
    {
      if (res != null)
        this.BundleMasters = res.bundles;
      else
        this.BundleMasters.Clear();
    }
    DebugUtility.LogWarning("PaymentManager:isBundleSetupOK=>" + this.isBundleSetupOK.ToString());
    return true;
  }

  public PaymentManager.Bundle GetBundle(string productId)
  {
    if (productId == null)
      return (PaymentManager.Bundle) null;
    return PaymentManager.Bundle.Create(productId);
  }

  public List<PaymentManager.Bundle> GetBundles()
  {
    List<PaymentManager.Bundle> bundleList = new List<PaymentManager.Bundle>();
    foreach (string bundleId in this.BundleIds)
    {
      PaymentManager.Bundle bundle = PaymentManager.Bundle.Create(bundleId);
      if (bundle != null)
        bundleList.Add(bundle);
    }
    return bundleList;
  }

  public bool ShowBundles()
  {
    this.OnShowBundles(PaymentManager.EShowItemsResult.SUCCESS, this.GetBundles().ToArray());
    return true;
  }

  public bool IsAvailable
  {
    get
    {
      return this.isSetupOK;
    }
  }

  private List<ProductParam> ProductMasters { get; set; }

  private ProductParam GetProductMaster(string productId)
  {
    using (List<ProductParam>.Enumerator enumerator = this.ProductMasters.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        ProductParam current = enumerator.Current;
        if (productId == current.ProductId)
          return current;
      }
    }
    return (ProductParam) null;
  }

  public PaymentManager.Product GetProduct(string productId)
  {
    if (productId == null)
      return (PaymentManager.Product) null;
    return PaymentManager.Product.Create(productId);
  }

  public List<PaymentManager.Product> GetProducts()
  {
    List<PaymentManager.Product> productList = new List<PaymentManager.Product>();
    if (this.ProductMasters == null)
    {
      DebugUtility.LogWarning("PaymentManager::GetProducts ProductMasters NULL");
      return productList;
    }
    using (List<ProductParam>.Enumerator enumerator = this.ProductMasters.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        PaymentManager.Product product = PaymentManager.Product.Create(enumerator.Current.ProductId);
        if (product != null)
          productList.Add(product);
      }
    }
    DebugUtility.LogWarning("PaymentManager::GetProducts products length: " + (object) productList.Count);
    return productList;
  }

  private string AgreedVer
  {
    get
    {
      return (string) MonoSingleton<UserInfoManager>.Instance.GetValue("payment_agreed_ver");
    }
    set
    {
      MonoSingleton<UserInfoManager>.Instance.SetValue("payment_agreed_ver", (object) value, true);
    }
  }

  protected override void Initialize()
  {
    Debug.Log((object) "PaymentManager.Initialize");
    Object.DontDestroyOnLoad((Object) this);
  }

  protected override void Release()
  {
    Debug.Log((object) "PaymentManager.Release");
  }

  public bool InitOnlyProductMaster(bool isResetMaster = false, ProductParamResponse res = null)
  {
    if (isResetMaster)
    {
      if (res != null)
        this.ProductMasters = res.products;
      else
        this.ProductMasters.Clear();
    }
    return true;
  }

  public bool Init(bool isResetMaster = false, ProductParamResponse res = null)
  {
    this.InitOnlyProductMaster(isResetMaster, res);
    Debug.Log((object) ("PaymentManager.Init len" + (object) this.ProductMasters.Count));
    List<ProductParam> ProductMasters = new List<ProductParam>((IEnumerable<ProductParam>) this.ProductMasters);
    ProductMasters.AddRange((IEnumerable<ProductParam>) this.BundleMastersAll.ToArray());
    MonoSingleton<PurchaseManager>.Instance.Init(ProductMasters);
    DebugUtility.LogWarning("PaymentManager:isSetupOK=>" + this.isSetupOK.ToString());
    return true;
  }

  public bool ShowItems()
  {
    this.OnShowItems(PaymentManager.EShowItemsResult.SUCCESS, this.GetProducts().ToArray());
    return true;
  }

  public bool RequestPurchase(string productId)
  {
    Debug.Log((object) nameof (RequestPurchase));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.NONE;
    MonoSingleton<PurchaseManager>.Instance.Purchase(productId);
    return true;
  }

  public bool RegisterBirthday(int year, int month, int day)
  {
    Debug.Log((object) nameof (RegisterBirthday));
    if (1900 > year || 2100 < year || (0 > month || 12 < month) || (0 > day || 31 < day))
      return false;
    MonoSingleton<PurchaseManager>.Instance.InputBirthday(year, month, day);
    return true;
  }

  public void OnAgree()
  {
    this.AgreedVer = Network.Version;
  }

  public bool IsAgree()
  {
    if (this.AgreedVer != null)
      return this.AgreedVer.Length > 0;
    return false;
  }

  public void OnAgeResponse(PaymentManager.ERegisterBirthdayResult result)
  {
    Debug.Log((object) "OnAgeResponsePurchase");
    if (this.OnRegisterBirthday == null)
      return;
    this.OnRegisterBirthday(result);
  }

  public void OnUpdateProductDetails(ProductInfo[] products)
  {
    Debug.Log((object) "OnUpdateProductDetailsPurchase");
    Debug.Log((object) ("OnUpdateProductDetailsPurchase " + ((object[]) products).ToStringFull()));
    if (products == null)
      return;
    this.ProductOnStores.Clear();
    this.BundleOnStores.Clear();
    foreach (ProductInfo product in products)
    {
      Debug.Log((object) (product.ID + " : " + product.ToString()));
      if (!product.ID.Contains("bundle"))
        this.ProductOnStores.Add(product.ID, product);
      else
        this.BundleOnStores.Add(product.ID, product);
    }
    if (this.BundleOnStores.Count > 0)
      this.isBundleSetupOK = true;
    if (this.ProductOnStores.Count > 0)
      this.isSetupOK = true;
    DebugUtility.LogWarning("PaymentManager:isSetupOK=>" + this.isSetupOK.ToString());
    DebugUtility.LogWarning("PaymentManager:isBundleSetupOK=>" + this.isBundleSetupOK.ToString());
  }

  public void OnPurchaseSucceeded(FulfillmentResult.OrderInfo order)
  {
    Debug.Log((object) nameof (OnPurchaseSucceeded));
    ProductInfo productInfo;
    if (this.ProductOnStores.TryGetValue(order.ProductId, out productInfo))
    {
      string id = productInfo.ID;
      string currencyCode = productInfo.CurrencyCode;
      double price = (double) productInfo.Price;
      MyMetaps.TrackPurchase(id, currencyCode, price);
      AnalyticsManager.TrackPurchase(id, currencyCode, price);
      AnalyticsManager.TrackPaidPremiumCurrencyObtain((long) order.PaidCoin, "IAP Purchase");
      AnalyticsManager.TrackFreePremiumCurrencyObtain((long) order.FreeCoin, "IAP Purchase");
    }
    if (Object.op_Inequality((Object) MonoSingleton<GameManager>.Instance, (Object) null) && MonoSingleton<GameManager>.Instance.Player != null)
      MonoSingleton<GameManager>.Instance.Player.SetOrderResult(order);
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.SUCCESS;
    if (this.OnRequestSucceeded == null)
      return;
    this.OnRequestSucceeded();
  }

  public void OnPurchaseFailed()
  {
    Debug.Log((object) nameof (OnPurchaseFailed));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.ERROR;
  }

  public void OnInsufficientBalances()
  {
    Debug.Log((object) nameof (OnInsufficientBalances));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.INSUFFICIENT_BALANCES;
  }

  public void OnPurchaseCanceled(string message)
  {
    Debug.Log((object) nameof (OnPurchaseCanceled));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.CANCEL;
  }

  public void OnPurchaseAlreadyOwned(string message)
  {
    Debug.Log((object) nameof (OnPurchaseAlreadyOwned));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.ALREADY_OWN;
  }

  public void OnPurchaseDeferred()
  {
    Debug.Log((object) nameof (OnPurchaseDeferred));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.DEFERRED;
  }

  public void OnOverCreditLimited()
  {
    Debug.Log((object) nameof (OnOverCreditLimited));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.OVER_LIMITED;
  }

  public void OnPurchaseFinished(bool isSuccess)
  {
    Debug.Log((object) ("OnPurchaseFinished " + (object) isSuccess));
    if (this.OnRequestPurchase == null)
      return;
    this.OnRequestPurchase(this.purchaseResult, (PaymentManager.CoinRecord) null);
  }

  public void OnPurchaseProcessing()
  {
    if (this.OnRequestProcessing == null)
      return;
    this.OnRequestProcessing();
  }

  public void OnNeedBirthday()
  {
    Debug.Log((object) nameof (OnNeedBirthday));
    if (this.OnRequestProcessing != null)
    {
      this.purchaseResult = PaymentManager.ERequestPurchaseResult.NEED_BIRTHDAY;
      this.OnRequestProcessing();
    }
    else
    {
      if (this.OnRegisterBirthday == null)
        return;
      this.OnRegisterBirthday(PaymentManager.ERegisterBirthdayResult.ERROR);
    }
  }

  public class Bundle
  {
    private ProductInfo store;
    private BundleParam master;

    private Bundle()
    {
    }

    public string productID { get; private set; }

    public string name
    {
      get
      {
        return this.master.Name;
      }
    }

    public string desc
    {
      get
      {
        return this.master.Description;
      }
    }

    public string price
    {
      get
      {
        return this.store.LocalizedPrice;
      }
    }

    public double sellPrice
    {
      get
      {
        return (double) this.store.Price;
      }
    }

    public int numPaid
    {
      get
      {
        return this.master.AdditionalPaidCoin;
      }
    }

    public int numFree
    {
      get
      {
        return this.master.AdditionalFreeCoin;
      }
    }

    public int maxPurchaseLimit
    {
      get
      {
        return this.master.PurchaseLimit;
      }
    }

    public long endDate
    {
      get
      {
        return this.master.EndDate;
      }
    }

    public string iconImage
    {
      get
      {
        return this.master.IconImage;
      }
    }

    public int displayOrder
    {
      get
      {
        return this.master.DisplayOrder;
      }
    }

    public static PaymentManager.Bundle Create(string productId_)
    {
      PaymentManager.Bundle bundle = new PaymentManager.Bundle();
      if (!bundle.Refresh(productId_))
        return (PaymentManager.Bundle) null;
      return bundle;
    }

    public bool Refresh(string productId_ = null)
    {
      if (productId_ != null)
        this.productID = productId_;
      PaymentManager instance = MonoSingleton<PaymentManager>.Instance;
      bool flag = instance.BundleOnStores.TryGetValue(this.productID, out this.store);
      this.master = instance.GetBundleMaster(this.productID);
      if (flag)
        return null != this.master;
      return false;
    }

    public override string ToString()
    {
      string str = "Bundle: id=" + this.productID + " store_id=" + this.store.ID;
      if (this.master != null)
        str = str + " master_name=" + this.master.Name;
      return str;
    }
  }

  public enum ECheckChargeLimitResult
  {
    SUCCESS,
    NONAGE,
    NEED_CHECK,
    ERROR,
    NEED_BIRTHDAY,
    LIMIT_OVER,
  }

  public class Product
  {
    private ProductInfo store;
    private ProductParam master;

    private Product()
    {
    }

    public string productID { get; private set; }

    public string name
    {
      get
      {
        return this.master.Name;
      }
    }

    public string desc
    {
      get
      {
        return this.master.Description;
      }
    }

    public string price
    {
      get
      {
        return this.store.LocalizedPrice;
      }
    }

    public double sellPrice
    {
      get
      {
        return (double) this.store.Price;
      }
    }

    public int numPaid
    {
      get
      {
        return this.master.AdditionalPaidCoin;
      }
    }

    public int numFree
    {
      get
      {
        return this.master.AdditionalFreeCoin;
      }
    }

    public int enabled
    {
      get
      {
        return this.master.IsEnabled;
      }
    }

    public bool onSale
    {
      get
      {
        return this.master.IsOnSale;
      }
    }

    public static PaymentManager.Product Create(string productId_)
    {
      PaymentManager.Product product = new PaymentManager.Product();
      if (!product.Reflesh(productId_))
        return (PaymentManager.Product) null;
      return product;
    }

    public bool Reflesh(string productId_ = null)
    {
      if (productId_ != null)
        this.productID = productId_;
      PaymentManager instance = MonoSingleton<PaymentManager>.Instance;
      bool flag = instance.ProductOnStores.TryGetValue(this.productID, out this.store);
      this.master = instance.GetProductMaster(this.productID);
      Debug.Log((object) ("Product.Reflesh " + this.ToString()));
      if (flag)
        return null != this.master;
      return false;
    }

    public override string ToString()
    {
      string str = "Product: id=" + this.productID;
      if (this.store != null)
        str = str + " store_id=" + this.store.ID;
      if (this.master != null)
        str = str + " master_name=" + this.master.Name;
      return str;
    }
  }

  public class CoinRecord
  {
    public string[] productIds;
    public int currentPaidCoin;
    public int currentFreeCoin;
    public int additionalPaidCoin;
    public int additionalFreeCoin;

    public CoinRecord(string[] productIds_, int currentPaidCoin_, int currentFreeCoin_, int additionalPaidCoin_, int additionalFreeCoin_)
    {
      this.productIds = productIds_;
      this.currentPaidCoin = currentPaidCoin_;
      this.currentFreeCoin = currentFreeCoin_;
      this.additionalPaidCoin = additionalPaidCoin_;
      this.additionalFreeCoin = additionalFreeCoin_;
    }
  }

  public enum EShowItemsResult
  {
    SUCCESS,
    ERROR,
  }

  public enum ERequestPurchaseResult
  {
    NONE = -1,
    SUCCESS = 0,
    CANCEL = 1,
    ALREADY_OWN = 2,
    DEFERRED = 3,
    INSUFFICIENT_BALANCES = 4,
    OVER_LIMITED = 5,
    NEED_BIRTHDAY = 6,
    ERROR = 7,
  }

  public enum ERegisterBirthdayResult
  {
    SUCCESS,
    ERROR,
  }

  private class MyDebug
  {
    private static PaymentManager.MyDebug self = new PaymentManager.MyDebug();
    private int max = 10;
    private List<string> contents = new List<string>();

    public static void PushMessage(string msg)
    {
      PaymentManager.MyDebug.self.contents.Add(msg);
      if (PaymentManager.MyDebug.self.contents.Count <= PaymentManager.MyDebug.self.max)
        return;
      PaymentManager.MyDebug.self.contents.RemoveAt(0);
    }

    [DebuggerHidden]
    public static IEnumerable<string> EachMessage()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PaymentManager.MyDebug.\u003CEachMessage\u003Ec__Iterator4D messageCIterator4D1 = new PaymentManager.MyDebug.\u003CEachMessage\u003Ec__Iterator4D();
      // ISSUE: variable of a compiler-generated type
      PaymentManager.MyDebug.\u003CEachMessage\u003Ec__Iterator4D messageCIterator4D2 = messageCIterator4D1;
      // ISSUE: reference to a compiler-generated field
      messageCIterator4D2.\u0024PC = -2;
      return (IEnumerable<string>) messageCIterator4D2;
    }
  }

  public delegate void ShowBundlesDelegate(PaymentManager.EShowItemsResult result, PaymentManager.Bundle[] bundles);

  public delegate void ShowItemsDelegate(PaymentManager.EShowItemsResult result, PaymentManager.Product[] products);

  public delegate void RequestPurchaseDelegate(PaymentManager.ERequestPurchaseResult result, PaymentManager.CoinRecord record = null);

  public delegate void RegisterBirthdayDelegate(PaymentManager.ERegisterBirthdayResult result);

  public delegate void RequestSucceededDelegate();

  public delegate void RequestProcessingDelegate();
}
