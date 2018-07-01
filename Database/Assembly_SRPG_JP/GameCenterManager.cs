// Decompiled with JetBrains decompiler
// Type: SRPG.GameCenterManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class GameCenterManager
  {
    private static List<AchievementParam> mAchievementList;

    public static bool isValidEnvironment()
    {
      return true;
    }

    public static void Auth()
    {
      if (!GameCenterManager.isValidEnvironment())
        ;
    }

    public static void ReAuth()
    {
      if (!GameCenterManager.isValidEnvironment())
        ;
    }

    public static bool IsAuth()
    {
      return true;
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
    }

    public static void ShowAchievement()
    {
    }

    public static void SendLeaderBoardScore(string leader_board_id, long score)
    {
    }

    public static bool IsLogin
    {
      get
      {
        if (Social.get_localUser() != null)
          return Social.get_localUser().get_authenticated();
        DebugUtility.Log("[GameCenterManager]Login Error!");
        return false;
      }
    }

    public static void SendAchievementProgress(string achievement_id, long progress)
    {
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

    private static AchievementParam GetAchievementParam(string iname)
    {
      List<AchievementParam> achievementData = GameCenterManager.GetAchievementData();
      if (achievementData == null || achievementData.Count < 1)
        return (AchievementParam) null;
      using (List<AchievementParam>.Enumerator enumerator = achievementData.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          AchievementParam current = enumerator.Current;
          if (current.iname == iname)
            return current;
        }
      }
      return (AchievementParam) null;
    }

    public static void SendAchievementProgress(string iname)
    {
      AchievementParam achievementParam = GameCenterManager.GetAchievementParam(iname);
      if (achievementParam == null)
        return;
      GameCenterManager.SendAchievementProgressInternal(achievementParam.AchievementID);
    }

    public static void SendAchievementProgress(AchievementParam param)
    {
      if (param == null)
        return;
      GameCenterManager.SendAchievementProgressInternal(param.AchievementID);
    }

    public static void SendAchievementProgressInternal(string achievementID)
    {
      if (string.IsNullOrEmpty(achievementID))
        return;
      long progress = 100;
      GameCenterManager.SendAchievementProgress(achievementID, progress);
    }
  }
}
