// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.GameInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames
{
  public static class GameInfo
  {
    private const string UnescapedApplicationId = "APP_ID";
    private const string UnescapedIosClientId = "IOS_CLIENTID";
    private const string UnescapedWebClientId = "WEB_CLIENTID";
    private const string UnescapedNearbyServiceId = "NEARBY_SERVICE_ID";
    public const string ApplicationId = "813126952066";
    public const string IosClientId = "";
    public const string WebClientId = "";
    public const string NearbyConnectionServiceId = "";

    public static bool ApplicationIdInitialized()
    {
      if (!string.IsNullOrEmpty("813126952066"))
        return !"813126952066".Equals(GameInfo.ToEscapedToken("APP_ID"));
      return false;
    }

    public static bool IosClientIdInitialized()
    {
      if (!string.IsNullOrEmpty(string.Empty))
        return !string.Empty.Equals(GameInfo.ToEscapedToken("IOS_CLIENTID"));
      return false;
    }

    public static bool WebClientIdInitialized()
    {
      if (!string.IsNullOrEmpty(string.Empty))
        return !string.Empty.Equals(GameInfo.ToEscapedToken("WEB_CLIENTID"));
      return false;
    }

    public static bool NearbyConnectionsInitialized()
    {
      if (!string.IsNullOrEmpty(string.Empty))
        return !string.Empty.Equals(GameInfo.ToEscapedToken("NEARBY_SERVICE_ID"));
      return false;
    }

    private static string ToEscapedToken(string token)
    {
      return string.Format("__{0}__", (object) token);
    }
  }
}
