// Decompiled with JetBrains decompiler
// Type: AssetManager_Extensions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public static class AssetManager_Extensions
{
  public static string ToPath(this AssetManager.AssetFormats platform)
  {
    switch (platform)
    {
      case AssetManager.AssetFormats.AndroidGeneric:
      case AssetManager.AssetFormats.AndroidDXT:
      case AssetManager.AssetFormats.AndroidPVR:
      case AssetManager.AssetFormats.AndroidATC:
        return "aatc/";
      case AssetManager.AssetFormats.Windows:
        return "aatc/";
      case AssetManager.AssetFormats.Text:
        return "Text/";
      default:
        return "iOS/";
    }
  }
}
