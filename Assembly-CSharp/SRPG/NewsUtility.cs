// Decompiled with JetBrains decompiler
// Type: SRPG.NewsUtility
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class NewsUtility
  {
    public static bool isNewsDisplay()
    {
      return NewsUtility.getNewsTypes() != NewsUtility.NewsTypes.None;
    }

    public static void clearNewsType()
    {
      GlobalVars.UrgencyPubHash = string.Empty;
    }

    public static void setNewsState(string pub_hash, string urgency_pub_hash, bool force_display)
    {
      string str1 = (string) MonoSingleton<UserInfoManager>.Instance.GetValue("PubHash");
      if (!string.IsNullOrEmpty(pub_hash) && (str1 != pub_hash || force_display))
      {
        MonoSingleton<UserInfoManager>.Instance.SetValue("PubHash", (object) pub_hash, true);
        GlobalVars.PubHash = pub_hash;
      }
      string str2 = (string) MonoSingleton<UserInfoManager>.Instance.GetValue("UrgencyPubHash");
      if (string.IsNullOrEmpty(urgency_pub_hash) || !(str2 != urgency_pub_hash))
        return;
      MonoSingleton<UserInfoManager>.Instance.SetValue("UrgencyPubHash", (object) urgency_pub_hash, true);
      GlobalVars.UrgencyPubHash = urgency_pub_hash;
    }

    public static NewsUtility.NewsTypes getNewsTypes()
    {
      if (!string.IsNullOrEmpty(GlobalVars.UrgencyPubHash))
        return NewsUtility.NewsTypes.Urgency;
      return !string.IsNullOrEmpty(GlobalVars.PubHash) ? NewsUtility.NewsTypes.Normal : NewsUtility.NewsTypes.None;
    }

    public enum NewsTypes
    {
      None,
      Normal,
      Urgency,
    }
  }
}
