// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Android.AndroidTokenClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Threading;
using UnityEngine;

namespace GooglePlayGames.Android
{
  internal class AndroidTokenClient : TokenClient
  {
    public static AndroidJavaObject GetActivity()
    {
      using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        return (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity");
    }

    public AndroidJavaObject GetApiClient(bool getServerAuthCode = false, string serverClientID = null)
    {
      Debug.Log((object) "Calling GetApiClient....");
      using (AndroidJavaObject activity = AndroidTokenClient.GetActivity())
      {
        using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.android.gms.plus.Plus"))
        {
          using (AndroidJavaObject androidJavaObject1 = new AndroidJavaObject("com.google.android.gms.common.api.GoogleApiClient$Builder", new object[1]{ (object) activity }))
          {
            androidJavaObject1.Call<AndroidJavaObject>("addApi", new object[1]
            {
              (object) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("API")
            });
            androidJavaObject1.Call<AndroidJavaObject>("addScope", new object[1]
            {
              (object) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("SCOPE_PLUS_LOGIN")
            });
            if (getServerAuthCode)
              androidJavaObject1.Call<AndroidJavaObject>("requestServerAuthCode", new object[2]
              {
                (object) serverClientID,
                (object) androidJavaObject1
              });
            AndroidJavaObject androidJavaObject2 = (AndroidJavaObject) androidJavaObject1.Call<AndroidJavaObject>("build", new object[0]);
            androidJavaObject2.Call("connect", new object[0]);
            int num = 100;
            while (androidJavaObject2.Call<bool>("isConnected", new object[0]) == null && num-- != 0)
              Thread.Sleep(100);
            Debug.Log((object) ("Done GetApiClient is " + (object) androidJavaObject2));
            return androidJavaObject2;
          }
        }
      }
    }

    private string GetAccountName()
    {
      using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.android.gms.plus.Plus"))
      {
        using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("AccountApi"))
        {
          using (AndroidJavaObject apiClient = this.GetApiClient(false, (string) null))
            return (string) androidJavaObject.Call<string>("getAccountName", new object[1]{ (object) apiClient });
        }
      }
    }

    public string GetEmail()
    {
      return this.GetAccountName();
    }

    public string GetAuthorizationCode(string serverClientID)
    {
      throw new NotImplementedException();
    }

    public string GetAccessToken()
    {
      string str1 = (string) null;
      string str2 = this.GetAccountName() ?? "NULL";
      string str3 = "oauth2:https://www.googleapis.com/auth/plus.me";
      using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.android.gms.auth.GoogleAuthUtil"))
        str1 = (string) ((AndroidJavaObject) androidJavaClass).CallStatic<string>("getToken", new object[3]
        {
          (object) AndroidTokenClient.GetActivity(),
          (object) str2,
          (object) str3
        });
      Debug.Log((object) ("Access Token " + str1));
      return str1;
    }

    public string GetIdToken(string serverClientID)
    {
      string str1 = (string) null;
      string str2 = this.GetAccountName() ?? "NULL";
      string str3 = "audience:server:client_id:" + serverClientID;
      using (AndroidJavaClass androidJavaClass1 = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
      {
        using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.google.android.gms.auth.GoogleAuthUtil"))
        {
          using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass1).GetStatic<AndroidJavaObject>("currentActivity"))
            str1 = (string) ((AndroidJavaObject) androidJavaClass2).CallStatic<string>("getToken", new object[3]
            {
              (object) androidJavaObject,
              (object) str2,
              (object) str3
            });
        }
      }
      Debug.Log((object) ("ID Token " + str1));
      return str1;
    }
  }
}
