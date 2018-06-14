// Decompiled with JetBrains decompiler
// Type: RegularLocalNotificationParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public class RegularLocalNotificationParam
{
  public static readonly string CATEGORY_MORNING = "local_ordinary_morning";
  public static readonly string CATEGORY_NOON = "local_ordinary_noon";
  public static readonly string CATEGORY_AFTERNOON = "local_ordinary_afternoon";
  public string mMessage = string.Empty;
  public string mCategory = string.Empty;
  public int mHour;
  public int mMinute;
  public int mSecond;

  public RegularLocalNotificationParam(string message, string category, int hour, int min, int sec)
  {
    this.mMessage = message;
    this.mCategory = category;
    this.mHour = hour;
    this.mMinute = min;
    this.mSecond = sec;
  }
}
