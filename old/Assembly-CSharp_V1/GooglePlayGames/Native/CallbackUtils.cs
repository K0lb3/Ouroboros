// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.CallbackUtils
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;

namespace GooglePlayGames.Native
{
  internal static class CallbackUtils
  {
    internal static Action<T> ToOnGameThread<T>(Action<T> toConvert)
    {
      if (toConvert == null)
        return (Action<T>) (_param0 => {});
      return (Action<T>) (val => PlayGamesHelperObject.RunOnGameThread((Action) (() => toConvert(val))));
    }

    internal static Action<T1, T2> ToOnGameThread<T1, T2>(Action<T1, T2> toConvert)
    {
      if (toConvert == null)
        return (Action<T1, T2>) ((_param0, _param1) => {});
      return (Action<T1, T2>) ((val1, val2) => PlayGamesHelperObject.RunOnGameThread((Action) (() => toConvert(val1, val2))));
    }

    internal static Action<T1, T2, T3> ToOnGameThread<T1, T2, T3>(Action<T1, T2, T3> toConvert)
    {
      if (toConvert == null)
        return (Action<T1, T2, T3>) ((_param0, _param1, _param2) => {});
      return (Action<T1, T2, T3>) ((val1, val2, val3) => PlayGamesHelperObject.RunOnGameThread((Action) (() => toConvert(val1, val2, val3))));
    }
  }
}
