// Decompiled with JetBrains decompiler
// Type: SRPG.GameCenterManager
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class GameCenterManager
  {
    private static List<AchievementParam> mAchievementList;
    private static int mIsValidEnvironment;

    public static bool isValidEnvironment()
    {
      if (GameCenterManager.mIsValidEnvironment != 0)
        return GameCenterManager.mIsValidEnvironment == 1;
      string deviceModel = SystemInfo.get_deviceModel();
      string operatingSystem = SystemInfo.get_operatingSystem();
      if (!string.IsNullOrEmpty(deviceModel))
      {
        if (!string.IsNullOrEmpty(operatingSystem))
        {
          try
          {
            string lower1 = deviceModel.ToLower();
            string lower2 = operatingSystem.ToLower();
            string[] strArray1 = lower1.Split(' ');
            string[] strArray2 = lower2.Split(' ');
            string str_maker = strArray1[0];
            string str_device = strArray1[1];
            string str_osversion = strArray2[2].Split('.')[0] + (object) '.' + strArray2[2].Split('.')[1];
            IgnoreDevice ignoreDevice = new IgnoreDevice();
            ignoreDevice.SetDevices("samsung", new string[5]
            {
              "SC-02E",
              "SGH-N025",
              "SC-03E",
              "SGH-N035",
              "SC-04E"
            }, "4.3");
            GameCenterManager.mIsValidEnvironment = !ignoreDevice.checkIgnoreDevice(str_maker, str_device, str_osversion) ? 1 : -1;
          }
          catch (Exception ex)
          {
            Debug.Log((object) ("<DeviceCheck:EXCEPTION> " + ex.Message + "\n-----------------------\n" + ex.StackTrace));
            GameCenterManager.mIsValidEnvironment = 1;
          }
          return GameCenterManager.mIsValidEnvironment == 1;
        }
      }
      return true;
    }

    public static void Auth()
    {
      if (!GameCenterManager.isValidEnvironment())
        return;
      PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
      PlayGamesPlatform.Activate();
      if (PlayGamesPlatform.Instance.localUser.get_authenticated())
        return;
      PlayGamesPlatform.Instance.Authenticate(new Action<bool>(GameCenterManager.ProcessAuthGameCenter), true);
    }

    public static void ReAuth()
    {
      if (!GameCenterManager.isValidEnvironment() || PlayGamesPlatform.Instance.localUser.get_authenticated())
        return;
      PlayGamesPlatform.Instance.Authenticate(new Action<bool>(GameCenterManager.ProcessAuthGameCenter));
    }

    public static bool IsAuth()
    {
      return PlayGamesPlatform.Instance.localUser.get_authenticated();
    }

    private static void ProcessAuthGameCenter(bool success)
    {
      if (success)
        Debug.Log((object) "[GameCenter]UserLogin Success!!");
      else
        Debug.Log((object) "[GameCenter]UserLogin Failed!!");
    }

    public static void ShowLeaderBoard()
    {
      Social.ShowLeaderboardUI();
    }

    public static void ShowAchievement()
    {
      Social.ShowAchievementsUI();
    }

    public static void SendLeaderBoardScore(string leader_board_id, long score)
    {
      if (!Social.get_localUser().get_authenticated())
        return;
      Social.ReportScore(score, leader_board_id, (Action<bool>) (success => Debug.Log((object) "[GameCenter]ReportScore Success!!")));
    }

    public static void SendAchievementProgress(string achievement_id, long progress)
    {
      if (!PlayGamesPlatform.Instance.localUser.get_authenticated())
        return;
      ((PlayGamesPlatform) Social.get_Active()).ReportProgress(achievement_id, (double) (int) progress, (Action<bool>) (success => Debug.Log((object) "[GameCenter]ReportProgress GooglePlayGameService Success!!")));
    }

    public static void GetLeaderboardData()
    {
    }

    public static List<AchievementParam> GetAchievementData()
    {
      if (GameCenterManager.mAchievementList != null)
        return GameCenterManager.mAchievementList;
      GameCenterManager.mAchievementList = new List<AchievementParam>();
      string empty = string.Empty;
      JSON_AchievementParam[] jsonArray = JSONParser.parseJSONArray<JSON_AchievementParam>(AssetManager.LoadTextData("GameCenter/acheivement"));
      if (jsonArray == null)
        return (List<AchievementParam>) null;
      foreach (JSON_AchievementParam achievementParam in jsonArray)
        GameCenterManager.mAchievementList.Add(new AchievementParam()
        {
          id = achievementParam.fields.id,
          iname = achievementParam.fields.iname,
          ios = achievementParam.fields.ios,
          googleplay = achievementParam.fields.googleplay
        });
      return GameCenterManager.mAchievementList;
    }

    public static string GetPlatformAchievementId(string iname)
    {
      List<AchievementParam> achievementData = GameCenterManager.GetAchievementData();
      if (achievementData == null)
        return string.Empty;
      using (List<AchievementParam>.Enumerator enumerator = achievementData.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          AchievementParam current = enumerator.Current;
          if (current.iname == iname)
            return current.googleplay;
        }
      }
      return string.Empty;
    }

    public static void SendAchievementProgress(string iname)
    {
      Debug.Log((object) ("Call SendAchievementProgress:" + iname));
      string platformAchievementId = GameCenterManager.GetPlatformAchievementId(iname);
      if (string.IsNullOrEmpty(platformAchievementId))
        return;
      Debug.Log((object) ("Exec SendAchievementProgress:" + platformAchievementId));
      long progress = 100;
      GameCenterManager.SendAchievementProgress(platformAchievementId, progress);
    }
  }
}
