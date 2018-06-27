// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.PAS.DMMGamesStore.PurchaseList
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

namespace Gsc.Purchase.API.PAS.DMMGamesStore
{
  public class PurchaseList : Request<PurchaseList, PurchaseList.Response>
  {
    private const string ___path = "{0}/pas/dmmgamesstore/{1}/purchase/list";

    public PurchaseList(int viewerId, string onetimeToken)
    {
      this.ViewerID = viewerId;
      this.OnetimeToken = onetimeToken;
    }

    public int ViewerID { get; set; }

    public string OnetimeToken { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/pas/dmmgamesstore/{1}/purchase/list", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath()
    {
      return "{0}/pas/dmmgamesstore/{1}/purchase/list";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["dmm_viewer_id"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.ViewerID);
      dictionary["dmm_onetime_token"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.OnetimeToken);
      return dictionary;
    }

    public class Response : GenericResponse<PurchaseList.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
          this.Infos = document.Root["purchase_infos"].GetArray().Select<IValue, PurchaseList.Response.PurchaseInfo_t>((Func<IValue, PurchaseList.Response.PurchaseInfo_t>) (x => new PurchaseList.Response.PurchaseInfo_t(x))).ToArray<PurchaseList.Response.PurchaseInfo_t>();
      }

      public PurchaseList.Response.PurchaseInfo_t[] Infos { get; private set; }

      public class PurchaseInfo_t : Gsc.Network.IObject, IResponseObject
      {
        public PurchaseInfo_t(IValue node)
        {
          Gsc.DOM.IObject @object = node.GetObject();
          this.PaymentId = @object["dmm_payment_id"].ToString();
          this.ProductId = @object["product_id"].ToString();
        }

        public string PaymentId { get; private set; }

        public string ProductId { get; private set; }
      }
    }
  }
}
