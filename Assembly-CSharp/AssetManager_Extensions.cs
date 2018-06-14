// Decompiled with JetBrains decompiler
// Type: AssetManager_Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public static class AssetManager_Extensions
{
  public static string ToPath(this AssetManager.AssetFormats platform)
  {
    switch (platform)
    {
      case AssetManager.AssetFormats.AndroidGeneric:
      case AssetManager.AssetFormats.AndroidATC:
        return "aatc/";
      case AssetManager.AssetFormats.AndroidDXT:
        return "adxt/";
      case AssetManager.AssetFormats.AndroidPVR:
        return "apvr/";
      case AssetManager.AssetFormats.Windows:
        return "aatc/";
      case AssetManager.AssetFormats.Text:
        return "Text/";
      case AssetManager.AssetFormats.AndroidASTC:
        return "astc/";
      default:
        return "iOS/";
    }
  }
}
