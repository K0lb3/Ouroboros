// Decompiled with JetBrains decompiler
// Type: PlayerPrefsUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Diagnostics;
using UnityEngine;

public class PlayerPrefsUtility
{
  public static readonly string CONFIG_SOUNDVOLUME = "SoundVolume";
  public static readonly string CONFIG_MUSICVOLUME = "MusicVolume";
  public static readonly string CONFIG_VOICEVOLUME = "VoiceVolume";
  public static readonly string CONFIG_INPUTMETHOD = "InputMethod";
  public static readonly string CONFIG_OKYAKUSAMACODE = "CUID";
  public static readonly string PARTY_TEAM_PREFIX = "TEAM_";
  public static readonly string VERSUS_ID_KEY = "VERSUS_PLACEMENT_";
  public static readonly string MULTITW_ID_KEY = "MULTITW_PLACEMENT_";
  public static readonly string FRIEND_REQUEST_CACHE = "FRIENDS";
  public static readonly string HOME_LASTACCESS_PLAYER_LV = "lastplv";
  public static readonly string HOME_LASTACCESS_VIP_LV = "lastviplv";
  public static readonly string PREFS_KEY_FRIEND_SORT = "FRIENDLIST_SORTTYPE";
  public static readonly string SAVE_UPDATE_TROPHY_LIST_KEY = "gm_sutlist";
  public static readonly string GAMEMANAGER_UPSCALE = "UPSCALE";
  public static readonly string TEAM_ID_KEY = "TeamID";
  public static readonly string MULTI_PLAY_TEAM_ID_KEY = "MultiPlayTeamID";
  public static readonly string ARENA_TEAM_ID_KEY = "ArenaTeamID";
  public static readonly string ROOM_COMMENT_KEY = "MultiPlayRoomComment";
  public static readonly string USE_ASSETBUNDLES = "UseAssetBundles";
  public static readonly string DEBUG_USE_DEV_SERVER = "UseDevServer";
  public static readonly string DEBUG_USE_AWS_SERVER = "UseAwsServer";
  public static readonly string DEBUG_NEWGAME = "NewGame";
  public static readonly string DEBUG_USE_LOCAL_DATA = "LocalData";
  public static readonly string DEBUG_AUTO_MARK = "AutoPlayMark";
  public static readonly string DEBUG_AUTOPLAY = "AutoPlay";
  public static readonly string SCENESTARTUP_CLEARCACHE = "CLEARCACHE";
  public static readonly string PLAYERDATA_INVENTORY = "INVENTORY";
  public static readonly string ALTER_PREV_CHECK_HASH = "PREV_CHECK_HASH";
  public static readonly string UNITLIST_UNIT_SORT_MODE = "UnitSortMode";
  public static readonly string UNIT_SORT_FILTER_PREFIX = "UNITLIST";
  public static readonly string EDITOR_SELECT_PLATFORM = "EditorPlatform";
  public static readonly string CONFIG_USE_PUSH_STAMINA = "UsePushStamina";
  public static readonly string CONFIG_USE_PUSH_NEWS = "UsePushNews";
  public static readonly string CONFIG_USE_CHAT_STATE = "ChatState";
  public static readonly string CONFIG_USE_STAMP_STATE = "StampState";
  public static readonly string CONFIG_USE_CHARGE_DISP = "CONFIG_CHARGE_DISP";
  public static readonly string CONFIG_USE_AUTO_PLAY = "UseAutoPlay";
  public static readonly string CONFIG_USE_AUTOMODE_TREASURE = "UseAutoTreasure";
  public static readonly string CONFIG_USE_AUTOMODE_DISABLE_SKILL = "UseAutoDisableSkill";
  public static readonly string CONFIG_USE_DIRECTIONCUT = "DirectionCut";
  public static readonly string RECOMMENDED_TEAM_SETTING_KEY = "RecommendedTeamSetting";
  public static readonly string UNIT_LEVELUP_EXPITEM_CHECKS = "UnitLevelUpExpItemChecks";
  public static readonly string ARTIFACT_BULK_LEVELUP = "ArtifactBulkLevelUp";
  public static readonly string CHAT_TEMPLATE_MESSAGE = "ChatTemplateMessage";
  public static readonly string CHALLENGEMISSION_HAS_SHOW_MESSAGE = nameof (CHALLENGEMISSION_HAS_SHOW_MESSAGE);

  public static bool SetInt(string key, int value, bool IsSave = false)
  {
    if (string.IsNullOrEmpty(key))
      return false;
    PlayerPrefs.SetInt(key, value);
    if (IsSave)
      PlayerPrefs.Save();
    return true;
  }

  public static int GetInt(string key, int default_value = 0)
  {
    int num = default_value;
    if (!string.IsNullOrEmpty(key) && PlayerPrefs.HasKey(key))
      num = PlayerPrefs.GetInt(key);
    return num;
  }

  public static bool SetFloat(string key, float value, bool IsSave = false)
  {
    if (string.IsNullOrEmpty(key))
      return false;
    PlayerPrefs.SetFloat(key, value);
    if (IsSave)
      PlayerPrefs.Save();
    return true;
  }

  public static float GetFloat(string key, float default_value = 0.0f)
  {
    float num = default_value;
    if (!string.IsNullOrEmpty(key) && PlayerPrefs.HasKey(key))
      num = PlayerPrefs.GetFloat(key);
    return num;
  }

  public static bool SetString(string key, string value, bool IsSave = false)
  {
    if (string.IsNullOrEmpty(key))
      return false;
    PlayerPrefs.SetString(key, value);
    if (IsSave)
      PlayerPrefs.Save();
    return true;
  }

  public static string GetString(string key, string default_value = "")
  {
    string str = default_value;
    if (!string.IsNullOrEmpty(key) && PlayerPrefs.HasKey(key))
      str = PlayerPrefs.GetString(key);
    return str;
  }

  public static bool HasKey(string key)
  {
    if (string.IsNullOrEmpty(key))
      return false;
    return PlayerPrefs.HasKey(key);
  }

  public static void DeleteKey(string key)
  {
    if (!PlayerPrefs.HasKey(key))
      return;
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    PlayerPrefs.DeleteKey(key);
    stopwatch.Stop();
    DebugUtility.Log("PlayerPrefs.DeleteKey:" + key);
    DebugUtility.Log("DeleteKey Time:" + stopwatch.Elapsed.ToString());
    DebugUtility.Log("DeleteKey Time:" + stopwatch.ElapsedMilliseconds.ToString() + "(ms)");
  }

  public static void DeleteAll()
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    PlayerPrefs.DeleteAll();
    stopwatch.Stop();
    DebugUtility.Log("PlayerPrefs.DeleteAll");
    DebugUtility.Log("DeleteKey Time:" + stopwatch.Elapsed.ToString());
    DebugUtility.Log("DeleteKey Time:" + stopwatch.ElapsedMilliseconds.ToString() + "(ms)");
  }

  public static void Save()
  {
    PlayerPrefs.Save();
  }
}
