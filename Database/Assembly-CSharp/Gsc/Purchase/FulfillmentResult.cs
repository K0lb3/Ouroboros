// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.FulfillmentResult
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using System;
using System.Linq;

namespace Gsc.Purchase
{
  public class FulfillmentResult : Gsc.Network.IObject, IResponseObject
  {
    public readonly int CurrentFreeCoin;
    public readonly int CurrentPaidCoin;
    public readonly int CurrentCommonCoin;
    public readonly FulfillmentResult.OrderInfo[] SucceededTransactions;
    public readonly FulfillmentResult.OrderInfo[] DuplicatedTransactions;

    public FulfillmentResult(int currentFreeCoin, int currentPaidCoin, int currentCommonCoin, FulfillmentResult.OrderInfo[] succeededTransactions, FulfillmentResult.OrderInfo[] duplicatedTransactions)
    {
      this.CurrentFreeCoin = currentFreeCoin;
      this.CurrentPaidCoin = currentPaidCoin;
      this.CurrentCommonCoin = currentCommonCoin;
      this.SucceededTransactions = succeededTransactions;
      this.DuplicatedTransactions = duplicatedTransactions;
    }

    public FulfillmentResult(Gsc.DOM.IObject node)
    {
      this.CurrentFreeCoin = node["current_free_coin"].ToInt();
      this.CurrentPaidCoin = node["current_paid_coin"].ToInt();
      this.CurrentCommonCoin = node["current_common_coin"].ToInt();
      this.SucceededTransactions = node["succeeded_orders"].GetArray().Select<IValue, FulfillmentResult.OrderInfo>((Func<IValue, FulfillmentResult.OrderInfo>) (x => new FulfillmentResult.OrderInfo(x.GetObject()))).ToArray<FulfillmentResult.OrderInfo>();
      this.DuplicatedTransactions = node["duplicated_orders"].GetArray().Select<IValue, FulfillmentResult.OrderInfo>((Func<IValue, FulfillmentResult.OrderInfo>) (x => new FulfillmentResult.OrderInfo(x.GetObject()))).ToArray<FulfillmentResult.OrderInfo>();
    }

    public class OrderInfo : Gsc.Network.IObject, IResponseObject
    {
      public readonly int FreeCoin;
      public readonly int PaidCoin;
      public readonly string ProductId;
      public readonly string TransactionId;

      public OrderInfo(int freeCoin, int paidCoin, string productId, string transactionId)
      {
        this.FreeCoin = freeCoin;
        this.PaidCoin = paidCoin;
        this.ProductId = productId;
        this.TransactionId = transactionId;
      }

      public OrderInfo(Gsc.DOM.IObject node)
      {
        this.FreeCoin = node["free_coin"].ToInt();
        this.PaidCoin = node["paid_coin"].ToInt();
        this.ProductId = node["product_id"].ToString();
        this.TransactionId = node["order_id"].ToString();
      }
    }
  }
}
