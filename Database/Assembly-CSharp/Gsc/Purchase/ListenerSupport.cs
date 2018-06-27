// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.ListenerSupport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace Gsc.Purchase
{
  public static class ListenerSupport
  {
    private static bool IsAliveMethod(bool enabledInactiveCallback, Delegate method)
    {
      return ListenerSupport.IsAliveObject<object>(enabledInactiveCallback, method.Target);
    }

    public static bool IsAliveObject<T>(bool enabledInactiveCallback, T obj)
    {
      if ((object) obj == null)
        return false;
      if (!((object) obj is MonoBehaviour))
        return true;
      MonoBehaviour monoBehaviour = (object) obj as MonoBehaviour;
      if (Object.op_Equality((Object) monoBehaviour, (Object) null) || !((Behaviour) monoBehaviour).get_enabled())
        return false;
      if (!enabledInactiveCallback)
        return ((Component) monoBehaviour).get_gameObject().get_activeInHierarchy();
      return true;
    }

    public static bool Call(bool enabledInactiveCallback, Action method)
    {
      if (!ListenerSupport.IsAliveMethod(enabledInactiveCallback, (Delegate) method))
        return false;
      method();
      return true;
    }

    public static bool Call<T1>(bool enabledInactiveCallback, Action<T1> method, T1 arg1)
    {
      if (!ListenerSupport.IsAliveMethod(enabledInactiveCallback, (Delegate) method))
        return false;
      method(arg1);
      return true;
    }

    public static bool Call<T1, T2>(bool enabledInactiveCallback, Action<T1, T2> method, T1 arg1, T2 arg2)
    {
      if (!ListenerSupport.IsAliveMethod(enabledInactiveCallback, (Delegate) method))
        return false;
      method(arg1, arg2);
      return true;
    }

    public static bool CallResult(bool enabledInactiveCallback, IPurchaseResultListener listener, ResultCode resultCode, FulfillmentResult result)
    {
      switch (resultCode)
      {
        case ResultCode.Succeeded:
          return ListenerSupport.Call<FulfillmentResult>(enabledInactiveCallback, new Action<FulfillmentResult>(listener.OnPurchaseSucceeded), result);
        case ResultCode.Canceled:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnPurchaseCanceled));
        case ResultCode.AlreadyOwned:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnPurchaseAlreadyOwned));
        case ResultCode.Deferred:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnPurchaseDeferred));
        case ResultCode.OverCreditLimit:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnOverCreditLimited));
        case ResultCode.InsufficientBalances:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnInsufficientBalances));
        default:
          return ListenerSupport.Call(enabledInactiveCallback, new Action(listener.OnPurchaseFailed));
      }
    }
  }
}
