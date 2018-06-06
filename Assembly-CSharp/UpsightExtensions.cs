// Decompiled with JetBrains decompiler
// Type: UpsightExtensions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

public static class UpsightExtensions
{
  private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

  public static long ToUnixTimestamp(this DateTime dateTime)
  {
    TimeSpan timeSpan = dateTime - UpsightExtensions.UnixEpoch;
    return (long) timeSpan.Seconds + (long) timeSpan.Minutes * 60L + (long) timeSpan.Hours * 60L * 60L + (long) timeSpan.Days * 24L * 60L * 60L;
  }

  public static DateTime ToDateTime(this long timestamp)
  {
    TimeSpan timeSpan = TimeSpan.FromSeconds((double) timestamp);
    return UpsightExtensions.UnixEpoch + timeSpan;
  }

  public static DateTime ToDateTime(this double timestamp)
  {
    TimeSpan timeSpan = TimeSpan.FromSeconds(timestamp);
    return UpsightExtensions.UnixEpoch + timeSpan;
  }
}
