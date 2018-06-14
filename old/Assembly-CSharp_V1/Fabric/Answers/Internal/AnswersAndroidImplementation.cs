// Decompiled with JetBrains decompiler
// Type: Fabric.Answers.Internal.AnswersAndroidImplementation
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace Fabric.Answers.Internal
{
  internal class AnswersAndroidImplementation : IAnswers
  {
    private AnswersSharedInstanceJavaObject answersSharedInstance;

    public AnswersAndroidImplementation()
    {
      this.answersSharedInstance = new AnswersSharedInstanceJavaObject();
    }

    public void LogSignUp(string method, bool? success, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("SignUpEvent", customAttributes, new string[0]);
      eventInstance.PutMethod(method);
      eventInstance.PutSuccess(success);
      this.answersSharedInstance.Log("logSignUp", eventInstance);
    }

    public void LogLogin(string method, bool? success, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("LoginEvent", customAttributes, new string[0]);
      eventInstance.PutMethod(method);
      eventInstance.PutSuccess(success);
      this.answersSharedInstance.Log("logLogin", eventInstance);
    }

    public void LogShare(string method, string contentName, string contentType, string contentId, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("ShareEvent", customAttributes, new string[0]);
      eventInstance.PutMethod(method);
      eventInstance.PutContentName(contentName);
      eventInstance.PutContentType(contentType);
      eventInstance.PutContentId(contentId);
      this.answersSharedInstance.Log("logShare", eventInstance);
    }

    public void LogInvite(string method, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("InviteEvent", customAttributes, new string[0]);
      eventInstance.PutMethod(method);
      this.answersSharedInstance.Log("logInvite", eventInstance);
    }

    public void LogLevelStart(string level, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("LevelStartEvent", customAttributes, new string[0]);
      eventInstance.InvokeSafelyAsString("putLevelName", level);
      this.answersSharedInstance.Log("logLevelStart", eventInstance);
    }

    public void LogLevelEnd(string level, double? score, bool? success, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("LevelEndEvent", customAttributes, new string[0]);
      eventInstance.InvokeSafelyAsString("putLevelName", level);
      eventInstance.InvokeSafelyAsDouble("putScore", (object) score);
      eventInstance.PutSuccess(success);
      this.answersSharedInstance.Log("logLevelEnd", eventInstance);
    }

    public void LogAddToCart(Decimal? itemPrice, string currency, string itemName, string itemType, string itemId, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("AddToCartEvent", customAttributes, new string[0]);
      eventInstance.InvokeSafelyAsDecimal("putItemPrice", (object) itemPrice);
      eventInstance.PutCurrency(currency);
      eventInstance.InvokeSafelyAsString("putItemName", itemName);
      eventInstance.InvokeSafelyAsString("putItemId", itemId);
      eventInstance.InvokeSafelyAsString("putItemType", itemType);
      this.answersSharedInstance.Log("logAddToCart", eventInstance);
    }

    public void LogPurchase(Decimal? price, string currency, bool? success, string itemName, string itemType, string itemId, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("PurchaseEvent", customAttributes, new string[0]);
      eventInstance.InvokeSafelyAsDecimal("putItemPrice", (object) price);
      eventInstance.PutCurrency(currency);
      eventInstance.PutSuccess(success);
      eventInstance.InvokeSafelyAsString("putItemName", itemName);
      eventInstance.InvokeSafelyAsString("putItemId", itemId);
      eventInstance.InvokeSafelyAsString("putItemType", itemType);
      this.answersSharedInstance.Log("logPurchase", eventInstance);
    }

    public void LogStartCheckout(Decimal? totalPrice, string currency, int? itemCount, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("StartCheckoutEvent", customAttributes, new string[0]);
      eventInstance.InvokeSafelyAsDecimal("putTotalPrice", (object) totalPrice);
      eventInstance.PutCurrency(currency);
      eventInstance.InvokeSafelyAsInt("putItemCount", itemCount);
      this.answersSharedInstance.Log("logStartCheckout", eventInstance);
    }

    public void LogRating(int? rating, string contentName, string contentType, string contentId, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("RatingEvent", customAttributes, new string[0]);
      eventInstance.InvokeSafelyAsInt("putRating", rating);
      eventInstance.PutContentName(contentName);
      eventInstance.PutContentType(contentType);
      eventInstance.PutContentId(contentId);
      this.answersSharedInstance.Log("logRating", eventInstance);
    }

    public void LogContentView(string contentName, string contentType, string contentId, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("ContentViewEvent", customAttributes, new string[0]);
      eventInstance.PutContentName(contentName);
      eventInstance.PutContentType(contentType);
      eventInstance.PutContentId(contentId);
      this.answersSharedInstance.Log("logContentView", eventInstance);
    }

    public void LogSearch(string query, Dictionary<string, object> customAttributes)
    {
      AnswersEventInstanceJavaObject eventInstance = new AnswersEventInstanceJavaObject("SearchEvent", customAttributes, new string[0]);
      eventInstance.InvokeSafelyAsString("putQuery", query);
      this.answersSharedInstance.Log("logSearch", eventInstance);
    }

    public void LogCustom(string eventName, Dictionary<string, object> customAttributes)
    {
      this.answersSharedInstance.Log("logCustom", new AnswersEventInstanceJavaObject("CustomEvent", customAttributes, new string[1]
      {
        eventName
      }));
    }
  }
}
