// Decompiled with JetBrains decompiler
// Type: Fabric.Answers.Answers
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Fabric.Answers.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fabric.Answers
{
  public class Answers : MonoBehaviour
  {
    private static IAnswers implementation;

    public Answers()
    {
      base.\u002Ector();
    }

    private static IAnswers Implementation
    {
      get
      {
        if (Fabric.Answers.Answers.implementation == null)
          Fabric.Answers.Answers.implementation = (IAnswers) new AnswersAndroidImplementation();
        return Fabric.Answers.Answers.implementation;
      }
    }

    public static void LogSignUp(string method = null, bool? success = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogSignUp(method, success, customAttributes);
    }

    public static void LogLogin(string method = null, bool? success = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogLogin(method, success, customAttributes);
    }

    public static void LogShare(string method = null, string contentName = null, string contentType = null, string contentId = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogShare(method, contentName, contentType, contentId, customAttributes);
    }

    public static void LogInvite(string method = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogInvite(method, customAttributes);
    }

    public static void LogLevelStart(string level = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogLevelStart(level, customAttributes);
    }

    public static void LogLevelEnd(string level = null, double? score = null, bool? success = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogLevelEnd(level, score, success, customAttributes);
    }

    public static void LogAddToCart(Decimal? itemPrice = null, string currency = null, string itemName = null, string itemType = null, string itemId = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogAddToCart(itemPrice, currency, itemName, itemType, itemId, customAttributes);
    }

    public static void LogPurchase(Decimal? price = null, string currency = null, bool? success = null, string itemName = null, string itemType = null, string itemId = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogPurchase(price, currency, success, itemName, itemType, itemId, customAttributes);
    }

    public static void LogStartCheckout(Decimal? totalPrice = null, string currency = null, int? itemCount = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogStartCheckout(totalPrice, currency, itemCount, customAttributes);
    }

    public static void LogRating(int? rating = null, string contentName = null, string contentType = null, string contentId = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogRating(rating, contentName, contentType, contentId, customAttributes);
    }

    public static void LogContentView(string contentName = null, string contentType = null, string contentId = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogContentView(contentName, contentType, contentId, customAttributes);
    }

    public static void LogSearch(string query = null, Dictionary<string, object> customAttributes = null)
    {
      if (customAttributes == null)
        customAttributes = new Dictionary<string, object>();
      Fabric.Answers.Answers.Implementation.LogSearch(query, customAttributes);
    }

    public static void LogCustom(string eventName, Dictionary<string, object> customAttributes = null)
    {
      if (eventName == null)
      {
        Debug.Log((object) "Answers' Custom Events require event names. Skipping this event because its name is null.");
      }
      else
      {
        if (customAttributes == null)
          customAttributes = new Dictionary<string, object>();
        Fabric.Answers.Answers.Implementation.LogCustom(eventName, customAttributes);
      }
    }
  }
}
