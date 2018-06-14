// Decompiled with JetBrains decompiler
// Type: Fabric.Answers.Internal.IAnswers
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Fabric.Answers.Internal
{
  internal interface IAnswers
  {
    void LogSignUp(string method, bool? success, Dictionary<string, object> customAttributes);

    void LogLogin(string method, bool? success, Dictionary<string, object> customAttributes);

    void LogShare(string method, string contentName, string contentType, string contentId, Dictionary<string, object> customAttributes);

    void LogInvite(string method, Dictionary<string, object> customAttributes);

    void LogLevelStart(string level, Dictionary<string, object> customAttributes);

    void LogLevelEnd(string level, double? score, bool? success, Dictionary<string, object> customAttributes);

    void LogPurchase(Decimal? price, string currency, bool? success, string itemName, string itemType, string itemId, Dictionary<string, object> customAttributes);

    void LogAddToCart(Decimal? itemPrice, string currency, string itemName, string itemType, string itemId, Dictionary<string, object> customAttributes);

    void LogStartCheckout(Decimal? totalPrice, string currency, int? itemCount, Dictionary<string, object> customAttributes);

    void LogRating(int? rating, string contentName, string contentType, string contentId, Dictionary<string, object> customAttributes);

    void LogContentView(string contentName, string contentType, string contentId, Dictionary<string, object> customAttributes);

    void LogSearch(string query, Dictionary<string, object> customAttributes);

    void LogCustom(string eventName, Dictionary<string, object> customAttributes);
  }
}
