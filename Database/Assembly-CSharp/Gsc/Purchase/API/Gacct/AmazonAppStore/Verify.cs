// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Gacct.AmazonAppStore.Verify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using Gsc.Purchase.API.App;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gsc.Purchase.API.Gacct.AmazonAppStore
{
  public class Verify : GenericRequest<Verify, Verify.Response>
  {
    private const string ___path = "/amazon/verify";

    public Verify(string userId, string currency, float price, string receiptId)
    {
      this.UserId = userId;
      this.Currency = currency;
      this.Price = price;
      this.ReceiptId = receiptId;
    }

    public string UserId { get; set; }

    public string Currency { get; set; }

    public float Price { get; set; }

    public string ReceiptId { get; set; }

    public bool IsBundle { get; set; }

    public override string GetPath()
    {
      if (this.IsBundle)
        return SDK.Configuration.Env.BundlePurchaseApiPrefix + "/amazon/verify";
      return SDK.Configuration.Env.PurchaseApiPrefix + "/amazon/verify";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["user"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.UserId);
      dictionary["currency"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.Currency);
      dictionary["price"] = Serializer.Instance.Add<float>(new Func<float, object>(Serializer.From<float>)).Serialize<float>(this.Price);
      dictionary["receipt"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.ReceiptId);
      return dictionary;
    }

    public class Response : GenericResponse<Verify.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
        {
          foreach (IMember member in (IEnumerable<IMember>) document.Root.GetObject())
            Debug.Log((object) member.Name);
          this.SuccessTransactionId = document.Root["receipt_id"].ToString();
          this.CurrentPaidCoin = document.Root["current_paid_coin"].ToInt();
          this.CurrentFreeCoin = document.Root["current_free_coin"].ToInt();
          this.CurrentCommonCoin = document.Root["current_common_coin"].ToInt();
          this.AdditionalPaidCoin = document.Root["additional_paid_coin"].ToInt();
          this.AdditionalFreeCoin = document.Root["additional_free_coin"].ToInt();
        }
      }

      public string SuccessTransactionId { get; private set; }

      public int CurrentPaidCoin { get; private set; }

      public int CurrentFreeCoin { get; private set; }

      public int CurrentCommonCoin { get; private set; }

      public int AdditionalPaidCoin { get; private set; }

      public int AdditionalFreeCoin { get; private set; }
    }
  }
}
