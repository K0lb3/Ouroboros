// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Gacct.AppStore.Verify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using Gsc.Purchase.API.App;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gsc.Purchase.API.Gacct.AppStore
{
  public class Verify : GenericRequest<Verify, Verify.Response>
  {
    private const string ___path = "/v2/ios/verify";

    public Verify(List<Verify.PurchaseData_t> receipts, string receiptData)
    {
      this.Receipts = receipts;
      this.ReceiptData = receiptData;
    }

    public List<Verify.PurchaseData_t> Receipts { get; set; }

    public string ReceiptData { get; set; }

    public bool IsBundle { get; set; }

    public override string GetPath()
    {
      if (this.IsBundle)
        return SDK.Configuration.Env.BundlePurchaseApiPrefix + "/v2/ios/verify";
      return SDK.Configuration.Env.PurchaseApiPrefix + "/v2/ios/verify";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["receipts"] = Serializer.Instance.WithArray<Verify.PurchaseData_t>().Add<Verify.PurchaseData_t>(new Func<Verify.PurchaseData_t, object>(Serializer.FromObject<Verify.PurchaseData_t>)).Serialize<List<Verify.PurchaseData_t>>(this.Receipts);
      dictionary["receipt_data"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.ReceiptData);
      return dictionary;
    }

    public class PurchaseData_t : Gsc.Network.IObject, IRequestObject
    {
      public PurchaseData_t(string currency, float price, string transactionId)
      {
        this.Currency = currency;
        this.Price = price;
        this.TransactionId = transactionId;
      }

      public string Currency { get; set; }

      public float Price { get; set; }

      public string TransactionId { get; set; }

      public Dictionary<string, object> GetPayload()
      {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        dictionary["currency"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.Currency);
        dictionary["price"] = Serializer.Instance.Add<float>(new Func<float, object>(Serializer.From<float>)).Serialize<float>(this.Price);
        dictionary["transaction_id"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.TransactionId);
        return dictionary;
      }
    }

    public class Response : GenericResponse<Verify.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
        {
          this.SuccessTransactionIds = document.Root["success_transaction_ids"].GetArray().Select<IValue, string>((Func<IValue, string>) (x => x.ToString())).ToArray<string>();
          this.DuplicatedTransactionIds = document.Root["duplicate_transaction_ids"].GetArray().Select<IValue, string>((Func<IValue, string>) (x => x.ToString())).ToArray<string>();
          this.CurrentPaidCoin = document.Root["current_paid_coin"].ToInt();
          this.CurrentFreeCoin = document.Root["current_free_coin"].ToInt();
          this.CurrentCommonCoin = document.Root["current_common_coin"].ToInt();
          this.AdditionalPaidCoin = document.Root["additional_paid_coin"].ToInt();
          this.AdditionalFreeCoin = document.Root["additional_free_coin"].ToInt();
        }
      }

      public string[] SuccessTransactionIds { get; private set; }

      public string[] DuplicatedTransactionIds { get; private set; }

      public int CurrentPaidCoin { get; private set; }

      public int CurrentFreeCoin { get; private set; }

      public int CurrentCommonCoin { get; private set; }

      public int AdditionalPaidCoin { get; private set; }

      public int AdditionalFreeCoin { get; private set; }
    }
  }
}
