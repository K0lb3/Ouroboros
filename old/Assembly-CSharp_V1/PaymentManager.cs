// Decompiled with JetBrains decompiler
// Type: PaymentManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using gu3.Device;
using gu3.gacct;
using gu3.Payment;
using SRPG;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PaymentManager : MonoSingleton<PaymentManager>, IPaymentListener, IGacctListener
{
  private readonly string googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAhhYVmsxrlV0aek2RM6sBbzTxzNH1PrH5czhH2GpZqcpVIt2PclMzMinpNmhiNN4lShpUqLty+wg0No0jkxarOO8FhdmrwOl0mr166/JpGO8UYXD1qbqZP4nyhDsI2AmWH1me2Us75GHUlIdubOcVTSYWCg3nRZwyUKW8G5PVtF9dcciDDU+nUrTGwZXcVWghNyg0Wswov3oTOlZ3lDhGCP9SkQHfGYyR42/ZSuSiH7kwb7FsK6XqoiYbU4NbKJVLfODwBHpRrnykxNVpUDyV1VCdhkY8CfkM2aj0ra4d88xnrEMH/crl8uPc1K7oNCw4wSnVVWs10WGiuWZ54bRF0wIDAQAB";
  private Dictionary<string, gu3.Payment.Product> ProductOnStores = new Dictionary<string, gu3.Payment.Product>();
  private PaymentManager.ERequestPurchaseResult purchaseResult = PaymentManager.ERequestPurchaseResult.NONE;
  private Dictionary<string, gu3.Payment.Product> BundleOnStores = new Dictionary<string, gu3.Payment.Product>();
  private const int verifyRetryMax = 5;
  private bool isBundleSetupOK;
  public PaymentManager.ShowBundlesDelegate OnShowBundles;
  private bool isSetupOK;
  private static int verifyRetryCount;
  private bool isInitialized;
  public PaymentManager.ShowItemsDelegate OnShowItems;
  public PaymentManager.RequestPurchaseDelegate OnRequestPurchase;
  public PaymentManager.RegisterBirthdayDelegate OnRegisterBirthday;
  public PaymentManager.CheckChargeLimitDelegate OnCheckChargeLimit;
  private PurchaseContainer purchaseContainerTemp;

  private List<BundleParam> BundleMasters { get; set; }

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

  private List<ProductParam> ProductMasters { get; set; }

  private List<ProductSaleParam> SaleMasters { get; set; }

  private string[] ProductIds
  {
    get
    {
      if (this.ProductMasters == null)
        return new string[0];
      string[] strArray = new string[this.ProductMasters.Count];
      int num = 0;
      using (List<ProductParam>.Enumerator enumerator = this.ProductMasters.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ProductParam current = enumerator.Current;
          strArray[num++] = current.ProductId;
        }
      }
      return strArray;
    }
  }

  public static string ApiPath
  {
    get
    {
      return Network.Host + "charge";
    }
  }

  public bool IsAvailable
  {
    get
    {
      return this.isSetupOK;
    }
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

  protected override void Initialize()
  {
    PaymentManager.MyDebug.PushMessage("PaymentManager.Initialize");
    Object.DontDestroyOnLoad((Object) this);
    ((Component) this).get_gameObject().AddComponent<PaymentObserver>();
  }

  protected override void Release()
  {
    PaymentManager.MyDebug.PushMessage("PaymentManager.Release");
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
    PaymentManager.MyDebug.PushMessage("PaymentManager.Init");
    this.isSetupOK = false;
    if (isResetMaster)
    {
      if (res != null)
        this.ProductMasters = res.products;
      else
        this.ProductMasters.Clear();
    }
    if (!this.isInitialized)
    {
      PaymentKit.set_EventListener((IPaymentListener) this);
      Client.set_Listener((IGacctListener) this);
      if (MonoSingleton<DebugManager>.Instance.IsWebViewEnable())
        Client.SetUserAgent(gu3.Device.System.GetUserAgent());
      else
        Client.SetUserAgent("Mozilla/5.0 (Linux; U; Android 4.3; ja-jp; Nexus 7 Build/JSS15Q) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Safari/534.30");
      PaymentKit.InitAndroid(this.googlePublicKey);
      this.isInitialized = true;
    }
    DebugUtility.LogWarning("PaymentManager:isSetupOK=>" + this.isSetupOK.ToString());
    return true;
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
    foreach (string productId in this.ProductIds)
    {
      PaymentManager.Product product = PaymentManager.Product.Create(productId);
      if (product != null)
        productList.Add(product);
    }
    return productList;
  }

  public bool ShowItems()
  {
    this.OnShowItems(PaymentManager.EShowItemsResult.SUCCESS, this.GetProducts().ToArray());
    return true;
  }

  public bool RequestPurchase(string productId)
  {
    PaymentManager.MyDebug.PushMessage(nameof (RequestPurchase));
    PaymentManager.verifyRetryCount = 0;
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.NONE;
    PaymentKit.StartPayment(productId);
    return true;
  }

  public bool RegisterBirthday(int year, int month, int day)
  {
    PaymentManager.MyDebug.PushMessage(nameof (RegisterBirthday));
    if (1900 > year || 2100 < year || (0 > month || 12 < month) || (0 > day || 31 < day))
      return false;
    Client.SendAge(PaymentManager.ApiPath, Network.SessionID, year, month, day);
    return true;
  }

  public bool CheckChargeLimit(List<string> productIds)
  {
    PaymentManager.MyDebug.PushMessage(nameof (CheckChargeLimit));
    double[] numArray = new double[productIds.Count];
    string str = string.Empty;
    int num = 0;
    using (List<string>.Enumerator enumerator = productIds.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        gu3.Payment.Product productOnStore = this.ProductOnStores[enumerator.Current];
        numArray[num++] = (double) productOnStore.price;
        str = (string) productOnStore.currency;
      }
    }
    Client.SendChargeLimit(PaymentManager.ApiPath, Network.SessionID, str, numArray);
    return true;
  }

  public void OnAgree()
  {
    this.AgreedVer = Application.GetBundleVersion();
  }

  public bool IsAgree()
  {
    if (this.AgreedVer != null)
      return this.AgreedVer.Length > 0;
    return false;
  }

  public void OnSetupSucceeded(string message)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnSetupSucceeded));
    int length1 = this.ProductIds.Length + this.BundleIds.Length;
    string[] strArray = new string[length1];
    for (int index = 0; index < this.ProductIds.Length; ++index)
      strArray[index] = this.ProductIds[index];
    int index1 = 0;
    for (int length2 = this.ProductIds.Length; length2 < length1; ++length2)
    {
      strArray[length2] = this.BundleIds[index1];
      ++index1;
    }
    PaymentKit.RequestProductDetails(strArray);
  }

  public void OnSetupFailed(string message)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnSetupFailed));
    this.Init(false, (ProductParamResponse) null);
  }

  public void OnUpdateProductDetails(ProductContainer products)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnUpdateProductDetails));
    this.ProductOnStores.Clear();
    foreach (gu3.Payment.Product content in ((Allocator<MetaProduct, gu3.Payment.Product>) products).get_Contents())
    {
      if (!((string) content.id).Contains("bundle"))
        this.ProductOnStores[(string) content.id] = content;
    }
    if (this.ProductOnStores.Count > 0)
      this.isSetupOK = true;
    this.BundleOnStores.Clear();
    foreach (gu3.Payment.Product content in ((Allocator<MetaProduct, gu3.Payment.Product>) products).get_Contents())
    {
      if (((string) content.id).Contains("bundle"))
        this.BundleOnStores[(string) content.id] = content;
    }
    if (this.BundleOnStores.Count > 0)
      this.isBundleSetupOK = true;
    DebugUtility.LogWarning("PaymentManager:isSetupOK=>" + this.isSetupOK.ToString());
    DebugUtility.LogWarning("PaymentManager:isBundleSetupOK=>" + this.isBundleSetupOK.ToString());
  }

  public void OnQueryInventoryFailed(string message)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnQueryInventoryFailed));
    this.OnSetupSucceeded(message);
  }

  public void OnPurchaseSucceeded(PurchaseContainer purchases)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnPurchaseSucceeded));
    if (((Allocator<MetaPurchase, Purchase>) purchases).get_Length() > 0)
    {
      if (((string) ((Allocator<MetaPurchase, Purchase>) purchases).get_Item(0).productId).Contains("bundle"))
      {
        GlobalVars.SelectedPurchaseType = GlobalVars.PurchaseType.Bundles;
        Debug.Log((object) (">>>> Bundle purchase succeeded : " + (object) purchases));
      }
      else
      {
        GlobalVars.SelectedPurchaseType = GlobalVars.PurchaseType.Gems;
        Debug.Log((object) (">>>> Gem purchase succeeded : " + (object) purchases));
      }
    }
    else
      Debug.Log((object) ">>>> [ERROR] Purchase succeeded but couldn't find purchase IDs.");
    this.purchaseContainerTemp = purchases;
    List<gu3.Payment.Product> productList = new List<gu3.Payment.Product>();
    if (GlobalVars.SelectedPurchaseType == GlobalVars.PurchaseType.Bundles)
    {
      using (Dictionary<string, gu3.Payment.Product>.ValueCollection.Enumerator enumerator = this.BundleOnStores.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          gu3.Payment.Product current = enumerator.Current;
          productList.Add(current);
        }
      }
      this.purchaseResult = PaymentManager.ERequestPurchaseResult.SUCCESS;
      Client.SendVerify(PaymentManager.BundleApiPath, Network.SessionID, purchases, productList.ToArray());
    }
    else
    {
      using (Dictionary<string, gu3.Payment.Product>.ValueCollection.Enumerator enumerator = this.ProductOnStores.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          gu3.Payment.Product current = enumerator.Current;
          productList.Add(current);
        }
      }
      this.purchaseResult = PaymentManager.ERequestPurchaseResult.SUCCESS;
      Client.SendVerify(PaymentManager.ApiPath, Network.SessionID, purchases, productList.ToArray());
    }
  }

  public void OnPurchaseFailed(string message)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnPurchaseFailed));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.NONE;
  }

  public void OnPurchaseCanceled(string message)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnPurchaseCanceled));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.CANCEL;
  }

  public void OnPurchaseAlreadyOwned(string message)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnPurchaseAlreadyOwned));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.ALREADY_OWN;
  }

  public void OnPurchaseDeferred(string message)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnPurchaseDeferred));
    this.purchaseResult = PaymentManager.ERequestPurchaseResult.DEFERRED;
  }

  public void OnPurchaseFinished(bool isSuccess)
  {
    PaymentManager.MyDebug.PushMessage("OnPurchaseFinished " + (object) isSuccess);
    if (isSuccess || this.purchaseResult == PaymentManager.ERequestPurchaseResult.SUCCESS || this.OnRequestPurchase == null)
      return;
    this.OnRequestPurchase(this.purchaseResult, (PaymentManager.CoinRecord) null);
  }

  public void OnAgeResponse(AgeResponse response)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnAgeResponse));
    if (this.OnRegisterBirthday == null)
      return;
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    this.OnRegisterBirthday((^(GACCTError&) @((Allocator<MetaAge, Age>) response).get_Meta().error).isError == null ? PaymentManager.ERegisterBirthdayResult.SUCCESS : PaymentManager.ERegisterBirthdayResult.ERROR);
  }

  public void OnChargeLimitResponse(ChargeLimitResponse response)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnChargeLimitResponse));
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    if ((^(GACCTError&) @((Allocator<MetaChargeLimit, ChargeLimit>) response).get_Meta().error).isError != null && "AGE000" == (string) (^(GACCTError&) @((Allocator<MetaChargeLimit, ChargeLimit>) response).get_Meta().error).reasons)
    {
      if (this.OnCheckChargeLimit == null)
        return;
      this.OnCheckChargeLimit(PaymentManager.ECheckChargeLimitResult.NEED_BIRTHDAY);
    }
    else
    {
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if ((^(GACCTError&) @((Allocator<MetaChargeLimit, ChargeLimit>) response).get_Meta().error).isError != null)
      {
        if (this.OnCheckChargeLimit == null)
          return;
        this.OnCheckChargeLimit(PaymentManager.ECheckChargeLimitResult.ERROR);
      }
      else
      {
        bool flag = false;
        foreach (ChargeLimit content in ((Allocator<MetaChargeLimit, ChargeLimit>) response).get_Contents())
        {
          if (content.active != null)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          if (this.OnCheckChargeLimit == null)
            return;
          this.OnCheckChargeLimit(PaymentManager.ECheckChargeLimitResult.LIMIT_OVER);
        }
        else
        {
          PaymentManager.ECheckChargeLimitResult result = 20 <= ((Allocator<MetaChargeLimit, ChargeLimit>) response).get_Meta().age ? (this.IsAgree() ? PaymentManager.ECheckChargeLimitResult.SUCCESS : PaymentManager.ECheckChargeLimitResult.NEED_CHECK) : PaymentManager.ECheckChargeLimitResult.NONAGE;
          if (this.OnCheckChargeLimit == null)
            return;
          this.OnCheckChargeLimit(result);
        }
      }
    }
  }

  public void OnVerifyResponse(VerifyResponse response)
  {
    PaymentManager.MyDebug.PushMessage(nameof (OnVerifyResponse));
    List<string> stringList = new List<string>();
    foreach (Verify content in ((Allocator<MetaVerify, Verify>) response).get_Contents())
      stringList.Add((string) content.productId);
    gu3.Payment.Product product1 = (gu3.Payment.Product) null;
    using (List<string>.Enumerator enumerator = stringList.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        string current = enumerator.Current;
        gu3.Payment.Product product2;
        if (this.BundleOnStores.TryGetValue(current, out product2))
        {
          GlobalVars.SelectedPurchaseType = GlobalVars.PurchaseType.Bundles;
          product1 = product2;
        }
        if (this.ProductOnStores.TryGetValue(current, out product2))
        {
          GlobalVars.SelectedPurchaseType = GlobalVars.PurchaseType.Gems;
          product1 = product2;
        }
      }
    }
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    if ((^(GACCTError&) @((Allocator<MetaVerify, Verify>) response).get_Meta().error).isError != null)
    {
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      PaymentManager.MyDebug.PushMessage("OnVerifyResponse err: " + (string) (^(GACCTError&) @((Allocator<MetaVerify, Verify>) response).get_Meta().error).reasons);
      if (PaymentManager.verifyRetryCount++ < 5)
      {
        PaymentManager.MyDebug.PushMessage("ResumePayment");
        PaymentKit.ResumePayment();
      }
      else
      {
        PaymentManager.verifyRetryCount = 0;
        if (this.OnRequestPurchase == null)
          return;
        AnalyticsManager.TrackPurchase(product1, response, GlobalVars.SelectedPurchaseType, (string) ((Allocator<MetaPurchase, Purchase>) this.purchaseContainerTemp).get_Contents()[0].androidSignature, ((Allocator<MetaPurchase, Purchase>) this.purchaseContainerTemp).get_Contents()[0]);
        this.OnRequestPurchase(PaymentManager.ERequestPurchaseResult.ERROR, (PaymentManager.CoinRecord) null);
      }
    }
    else
    {
      AnalyticsManager.TrackPurchase(product1, response, GlobalVars.SelectedPurchaseType, (string) ((Allocator<MetaPurchase, Purchase>) this.purchaseContainerTemp).get_Contents()[0].androidSignature, ((Allocator<MetaPurchase, Purchase>) this.purchaseContainerTemp).get_Contents()[0]);
      PaymentManager.CoinRecord record = new PaymentManager.CoinRecord(stringList.ToArray(), (int) ((Allocator<MetaVerify, Verify>) response).get_Meta().CurrentPaidCoin, (int) ((Allocator<MetaVerify, Verify>) response).get_Meta().CurrentFreeCoin, (int) ((Allocator<MetaVerify, Verify>) response).get_Meta().AdditionalPaidCoin, (int) ((Allocator<MetaVerify, Verify>) response).get_Meta().AdditionalFreeCoin);
      PaymentManager.MyDebug.PushMessage("OnVerifyResponse " + (object) record);
      if (this.OnRequestPurchase == null)
        return;
      this.OnRequestPurchase(PaymentManager.ERequestPurchaseResult.SUCCESS, record);
    }
  }

  public void OnApplicationPause(bool pauseState)
  {
    PaymentManager.MyDebug.PushMessage("OnApplicationPause " + (object) pauseState);
    if (!this.IsAvailable || pauseState)
      return;
    PaymentManager.MyDebug.PushMessage("ResumePayment");
    PaymentKit.ResumePayment();
  }

  public class Bundle
  {
    private gu3.Payment.Product store;
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
        return (string) this.store.localizedPrice;
      }
    }

    public double sellPrice
    {
      get
      {
        return (double) this.store.price;
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
      PaymentManager.MyDebug.PushMessage("Bundle.Refresh " + this.ToString());
      if (flag)
        return null != this.master;
      return false;
    }

    public override string ToString()
    {
      string str = "Bundle: id=" + this.productID + " store_id=" + (string) this.store.id;
      if (this.master != null)
        str = str + " master_name=" + this.master.Name;
      return str;
    }
  }

  public class Product
  {
    private gu3.Payment.Product store;
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
        return (string) this.store.localizedPrice;
      }
    }

    public double sellPrice
    {
      get
      {
        return (double) this.store.price;
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

    public int remainingDays
    {
      get
      {
        return this.master.RemainingDays;
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
      PaymentManager.MyDebug.PushMessage("Product.Reflesh " + this.ToString());
      if (flag)
        return null != this.master;
      return false;
    }

    public override string ToString()
    {
      string str = "Product: id=" + this.productID + " store_id=" + (string) this.store.id;
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
    ERROR = 4,
  }

  public enum ERegisterBirthdayResult
  {
    SUCCESS,
    ERROR,
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
      PaymentManager.MyDebug.\u003CEachMessage\u003Ec__Iterator2D messageCIterator2D1 = new PaymentManager.MyDebug.\u003CEachMessage\u003Ec__Iterator2D();
      // ISSUE: variable of a compiler-generated type
      PaymentManager.MyDebug.\u003CEachMessage\u003Ec__Iterator2D messageCIterator2D2 = messageCIterator2D1;
      // ISSUE: reference to a compiler-generated field
      messageCIterator2D2.\u0024PC = -2;
      return (IEnumerable<string>) messageCIterator2D2;
    }
  }

  public delegate void ShowBundlesDelegate(PaymentManager.EShowItemsResult result, PaymentManager.Bundle[] bundles);

  public delegate void ShowItemsDelegate(PaymentManager.EShowItemsResult result, PaymentManager.Product[] products);

  public delegate void RequestPurchaseDelegate(PaymentManager.ERequestPurchaseResult result, PaymentManager.CoinRecord record = null);

  public delegate void RegisterBirthdayDelegate(PaymentManager.ERegisterBirthdayResult result);

  public delegate void CheckChargeLimitDelegate(PaymentManager.ECheckChargeLimitResult result);
}
