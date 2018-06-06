// Decompiled with JetBrains decompiler
// Type: AndroidPermissionsManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class AndroidPermissionsManager
{
  private static AndroidJavaObject m_Activity;
  private static AndroidJavaObject m_PermissionService;

  private static AndroidJavaObject GetActivity()
  {
    if (AndroidPermissionsManager.m_Activity == null)
      AndroidPermissionsManager.m_Activity = (AndroidJavaObject) ((AndroidJavaObject) new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity");
    return AndroidPermissionsManager.m_Activity;
  }

  private static AndroidJavaObject GetPermissionsService()
  {
    return AndroidPermissionsManager.m_PermissionService ?? (AndroidPermissionsManager.m_PermissionService = new AndroidJavaObject("com.unity3d.plugin.UnityAndroidPermissions", new object[0]));
  }

  public static bool IsPermissionGranted(string permissionName)
  {
    return (bool) AndroidPermissionsManager.GetPermissionsService().Call<bool>(nameof (IsPermissionGranted), new object[2]{ (object) AndroidPermissionsManager.GetActivity(), (object) permissionName });
  }

  public static void RequestPermission(string[] permissionNames, AndroidPermissionCallback callback)
  {
    AndroidPermissionsManager.GetPermissionsService().Call("RequestPermissionAsync", new object[3]
    {
      (object) AndroidPermissionsManager.GetActivity(),
      (object) permissionNames,
      (object) callback
    });
  }
}
