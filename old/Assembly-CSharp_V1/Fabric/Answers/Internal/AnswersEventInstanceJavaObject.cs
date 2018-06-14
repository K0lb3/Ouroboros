// Decompiled with JetBrains decompiler
// Type: Fabric.Answers.Internal.AnswersEventInstanceJavaObject
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fabric.Answers.Internal
{
  internal class AnswersEventInstanceJavaObject
  {
    public AndroidJavaObject javaObject;

    public AnswersEventInstanceJavaObject(string eventType, Dictionary<string, object> customAttributes, params string[] args)
    {
      this.javaObject = new AndroidJavaObject(string.Format("com.crashlytics.android.answers.{0}", (object) eventType), (object[]) args);
      using (Dictionary<string, object>.Enumerator enumerator = customAttributes.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<string, object> current = enumerator.Current;
          string key = current.Key;
          object o = current.Value;
          if (o == null)
            Debug.Log((object) string.Format("[Answers] Expected custom attribute value to be non-null. Received: {0}", o));
          else if (AnswersEventInstanceJavaObject.IsNumericType(o))
            this.javaObject.Call<AndroidJavaObject>("putCustomAttribute", new object[2]
            {
              (object) key,
              (object) AnswersEventInstanceJavaObject.AsDouble(o)
            });
          else if (o is string)
            this.javaObject.Call<AndroidJavaObject>("putCustomAttribute", new object[2]
            {
              (object) key,
              o
            });
          else
            Debug.Log((object) string.Format("[Answers] Expected custom attribute value to be a string or numeric. Received: {0}", o));
        }
      }
    }

    public void PutMethod(string method)
    {
      this.InvokeSafelyAsString("putMethod", method);
    }

    public void PutSuccess(bool? success)
    {
      this.InvokeSafelyAsBoolean("putSuccess", success);
    }

    public void PutContentName(string contentName)
    {
      this.InvokeSafelyAsString("putContentName", contentName);
    }

    public void PutContentType(string contentType)
    {
      this.InvokeSafelyAsString("putContentType", contentType);
    }

    public void PutContentId(string contentId)
    {
      this.InvokeSafelyAsString("putContentId", contentId);
    }

    public void PutCurrency(string currency)
    {
      this.InvokeSafelyAsCurrency("putCurrency", currency);
    }

    public void InvokeSafelyAsCurrency(string methodName, string currency)
    {
      if (currency == null)
        return;
      this.javaObject.Call<AndroidJavaObject>("putCurrency", new object[1]
      {
        (object) (AndroidJavaObject) ((AndroidJavaObject) new AndroidJavaClass("java.util.Currency")).CallStatic<AndroidJavaObject>("getInstance", new object[1]
        {
          (object) currency
        })
      });
    }

    public void InvokeSafelyAsBoolean(string methodName, bool? arg)
    {
      if (!arg.HasValue)
        return;
      this.javaObject.Call<AndroidJavaObject>(methodName, new object[1]
      {
        (object) arg
      });
    }

    public void InvokeSafelyAsInt(string methodName, int? arg)
    {
      if (!arg.HasValue)
        return;
      this.javaObject.Call<AndroidJavaObject>(methodName, new object[1]
      {
        (object) arg
      });
    }

    public void InvokeSafelyAsString(string methodName, string arg)
    {
      if (arg == null)
        return;
      this.javaObject.Call<AndroidJavaObject>(methodName, new object[1]
      {
        (object) arg
      });
    }

    public void InvokeSafelyAsDecimal(string methodName, object arg)
    {
      if (arg == null)
        return;
      this.javaObject.Call<AndroidJavaObject>(methodName, new object[1]
      {
        (object) new AndroidJavaObject("java.math.BigDecimal", new object[1]
        {
          (object) arg.ToString()
        })
      });
    }

    public void InvokeSafelyAsDouble(string methodName, object arg)
    {
      if (arg == null)
        return;
      this.javaObject.Call<AndroidJavaObject>(methodName, new object[1]
      {
        (object) AnswersEventInstanceJavaObject.AsDouble(arg)
      });
    }

    private static bool IsNumericType(object o)
    {
      switch (Type.GetTypeCode(o.GetType()))
      {
        case TypeCode.SByte:
        case TypeCode.Byte:
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
          return true;
        default:
          return false;
      }
    }

    private static AndroidJavaObject AsDouble(object param)
    {
      return new AndroidJavaObject("java.lang.Double", new object[1]{ (object) param.ToString() });
    }
  }
}
