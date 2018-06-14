// Decompiled with JetBrains decompiler
// Type: GameUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using MsgPack;
using SRPG;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GameUtility
{
  private static Toggle.ToggleEvent nullToggleEvent = new Toggle.ToggleEvent();
  private static StringBuilder mSB = new StringBuilder(128);
  private static string LOGININFO_ALREADY_READ = "LoginInfoRead";
  private static string CONFIG_DEVSETTING = "DevServerSetting_SG";
  private static float m_sound_volume = -1f;
  private static float m_music_volume = -1f;
  private static float m_voice_volume = -1f;
  public static GameUtility.BooleanConfig Config_UseAssetBundles = new GameUtility.BooleanConfig(PlayerPrefsUtility.USE_ASSETBUNDLES, false);
  public static GameUtility.BooleanConfig Config_UseDevServer = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_USE_DEV_SERVER, false);
  public static GameUtility.BooleanConfig Config_UseAwsServer = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_USE_AWS_SERVER, false);
  public static GameUtility.BooleanConfig Config_NewGame = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_NEWGAME, false);
  public static GameUtility.BooleanConfig Config_UseLocalData = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_USE_LOCAL_DATA, false);
  public static GameUtility.BooleanConfig Config_AutoPlayMark = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_AUTO_MARK, false);
  public static GameUtility.BooleanConfig Config_AutoPlay = new GameUtility.BooleanConfig(PlayerPrefsUtility.DEBUG_AUTOPLAY, false);
  public static GameUtility.BooleanConfig Config_UseAutoPlay = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_AUTO_PLAY, false);
  public static GameUtility.BooleanConfig Config_AutoMode_Treasure = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_AUTOMODE_TREASURE, false);
  public static GameUtility.BooleanConfig Config_AutoMode_DisableSkill = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_AUTOMODE_DISABLE_SKILL, false);
  public static GameUtility.BooleanConfig Config_DirectionCut = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_DIRECTIONCUT, true);
  public static GameUtility.BooleanConfig Config_UsePushStamina = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_PUSH_STAMINA, true);
  public static GameUtility.BooleanConfig Config_UsePushNews = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_PUSH_NEWS, false);
  public static GameUtility.BooleanConfig Config_ChatState = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_CHAT_STATE, true);
  public static GameUtility.BooleanConfig Config_MultiState = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_STAMP_STATE, true);
  public static GameUtility.BooleanConfig Config_ChargeDisp = new GameUtility.BooleanConfig(PlayerPrefsUtility.CONFIG_USE_CHARGE_DISP, true);
  private static ObjectPacker Packer = new ObjectPacker();
  private static GameUtility.UnitShowSetting[] UnitShowSettings = new GameUtility.UnitShowSetting[29]
  {
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_FIRE"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_WATER"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_WIND"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_THUNDER"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_SHINE"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_DARK"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_ZENEI"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_TYUEI"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_KOUEI"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE1"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE2"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE3"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE4"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE5"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "FILTER_RARE6"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_SYOJUN"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "SORT_KOUJUN"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 1,
      key = "SORT_LEVEL"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_FAVORITE"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_JOBRANK"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_HP"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_ATK"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_DEF"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_MAG"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_MND"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_SPD"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_TOTAL"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_AWAKE"
    },
    new GameUtility.UnitShowSetting()
    {
      on = 0,
      key = "SORT_COMBINATION"
    }
  };
  public static readonly GameUtility.UnitSortModes[] UnitSortMenuItems = new GameUtility.UnitSortModes[13]
  {
    GameUtility.UnitSortModes.Time,
    GameUtility.UnitSortModes.Level,
    GameUtility.UnitSortModes.JobRank,
    GameUtility.UnitSortModes.HP,
    GameUtility.UnitSortModes.Atk,
    GameUtility.UnitSortModes.Def,
    GameUtility.UnitSortModes.Mag,
    GameUtility.UnitSortModes.Mnd,
    GameUtility.UnitSortModes.Spd,
    GameUtility.UnitSortModes.Total,
    GameUtility.UnitSortModes.Awake,
    GameUtility.UnitSortModes.Combination,
    GameUtility.UnitSortModes.Rarity
  };
  public static readonly Color32 Color32_White = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
  public static readonly Color32 Color32_Black = new Color32((byte) 0, (byte) 0, (byte) 0, byte.MaxValue);
  private static Color mFadeInColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
  private static readonly Vector3 _deformRadiusLength = new Vector3(100f, 0.01f, 0.0f);
  public static Quaternion Yaw180 = Quaternion.AngleAxis(180f, Vector3.get_up());
  public static Quaternion Yaw90 = Quaternion.AngleAxis(90f, Vector3.get_up());
  public static Quaternion Yaw90Inv = Quaternion.AngleAxis(-90f, Vector3.get_up());
  private static int mNeverSleepCount = 0;
  public static string LocalizedQuestParamFileName = "LocalizedQuestParam";
  public static string LocalizedMasterParamFileName = "LocalizedMasterParam";
  public static string LocalizedNotificationFileName = "LocalizedNotification";
  public static string LocalizedGachaFileName = "LocalizedGacha";
  public static string LocalisedProductSaleFileName = "LocalizedProductSaleParam";
  public static string LocalisedChatChannelName = "LocalizedChatChannelParam";
  private static string CONFIG_LANGUAGE = "Selected_Language";
  private static string CONFIG_CULTURE = "Selected_Culture";
  public static string CONFIG_SKILL_ANIMATION = "Skill_Animation";
  public const string Lang_en = "english";
  public const string Lang_fr = "french";
  public const string Lang_de = "german";
  public const string Lang_es = "spanish";
  public const string Lang_none = "None";
  public const float SmallNumber = 0.0001f;
  private static CultureInfo _cultureSetting;
  public static bool ShowEnumOnly;
  public static bool ForceLanguageSelection;
  private static Application.LogCallback mLogCallbacks;
  private static bool mATCTextureSupport;
  private static bool mDXTTextureSupport;
  private static bool mPVRTextureSupport;
  private static bool mASTCTextureSupport;
  private static string[] mGLExtensions;
  private static int mLayerBG;
  private static int mLayerCH;
  private static int mLayerHidden;
  private static int mLayerDefault;
  private static int mLayerCH0;
  private static int mLayerCH1;
  private static int mLayerCH2;
  private static int mLayerCHBG0;
  private static int mLayerCHBG1;
  private static int mLayerCHBG2;
  private static int mLayerUI;
  private static int mLayerEffect;
  private static bool mLayerIndexInitialized;
  private static Texture2D mTransparentTexture;

  public static CultureInfo CultureSetting
  {
    get
    {
      if (GameUtility._cultureSetting == null)
      {
        string configLanguage = GameUtility.Config_Language;
        if (configLanguage != null)
        {
          if (GameUtility.\u003C\u003Ef__switch\u0024map6 == null)
            GameUtility.\u003C\u003Ef__switch\u0024map6 = new Dictionary<string, int>(4)
            {
              {
                "english",
                0
              },
              {
                "french",
                1
              },
              {
                "spanish",
                2
              },
              {
                "german",
                3
              }
            };
          int num;
          if (GameUtility.\u003C\u003Ef__switch\u0024map6.TryGetValue(configLanguage, out num))
          {
            switch (num)
            {
              case 0:
                GameUtility._cultureSetting = new CultureInfo("en-US");
                break;
              case 1:
                GameUtility._cultureSetting = new CultureInfo("fr-FR");
                break;
              case 2:
                GameUtility._cultureSetting = new CultureInfo("es-ES");
                break;
              case 3:
                GameUtility._cultureSetting = new CultureInfo("de-DE");
                break;
            }
          }
        }
      }
      return GameUtility._cultureSetting;
    }
  }

  public static string Localized_TimePattern_Short
  {
    get
    {
      return GameUtility._cultureSetting.DateTimeFormat.MonthDayPattern.Replace("MMMM", "MMM") + " " + GameUtility._cultureSetting.DateTimeFormat.ShortTimePattern;
    }
  }

  public static string Localized_TimePattern
  {
    get
    {
      return GameUtility._cultureSetting.DateTimeFormat.FullDateTimePattern;
    }
  }

  public static string Config_Language
  {
    get
    {
      if (!PlayerPrefs.HasKey(GameUtility.CONFIG_LANGUAGE))
      {
        SystemLanguage systemLanguage = Application.get_systemLanguage();
        switch (systemLanguage - 10)
        {
          case 0:
            GameUtility.Config_Language = "english";
            break;
          case 4:
            GameUtility.Config_Language = "french";
            break;
          case 5:
            GameUtility.Config_Language = "german";
            break;
          default:
            GameUtility.Config_Language = systemLanguage == 34 ? "spanish" : "None";
            break;
        }
      }
      return PlayerPrefs.GetString(GameUtility.CONFIG_LANGUAGE);
    }
    set
    {
      PlayerPrefs.SetString(GameUtility.CONFIG_LANGUAGE, value);
      GameUtility._cultureSetting = (CultureInfo) null;
    }
  }

  public static void ClearPreferences()
  {
    string configLanguage = GameUtility.Config_Language;
    PlayerPrefs.DeleteAll();
    EditorPlayerPrefs.DeleteAll();
    GameUtility.Config_Language = configLanguage;
  }

  public static bool Config_SkillAnimation
  {
    get
    {
      if (!PlayerPrefs.HasKey(GameUtility.CONFIG_SKILL_ANIMATION))
        return true;
      return PlayerPrefs.GetInt(GameUtility.CONFIG_SKILL_ANIMATION) > 0;
    }
    set
    {
      PlayerPrefs.SetInt(GameUtility.CONFIG_SKILL_ANIMATION, !value ? 0 : 1);
    }
  }

  public static string ConvertLanguageToISO639(string language)
  {
    string str = "en";
    string key = language;
    if (key != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (GameUtility.\u003C\u003Ef__switch\u0024map7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        GameUtility.\u003C\u003Ef__switch\u0024map7 = new Dictionary<string, int>(5)
        {
          {
            "english",
            0
          },
          {
            "japanese",
            1
          },
          {
            "french",
            2
          },
          {
            "german",
            3
          },
          {
            "spanish",
            4
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (GameUtility.\u003C\u003Ef__switch\u0024map7.TryGetValue(key, out num))
      {
        switch (num)
        {
          case 0:
            str = "en";
            break;
          case 1:
            str = "ja";
            break;
          case 2:
            str = "fr";
            break;
          case 3:
            str = "de";
            break;
          case 4:
            str = "es";
            break;
        }
      }
    }
    return str;
  }

  public static string SceneNameHome()
  {
    string str = "Home";
    string configLanguage = GameUtility.Config_Language;
    if (configLanguage != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (GameUtility.\u003C\u003Ef__switch\u0024map8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        GameUtility.\u003C\u003Ef__switch\u0024map8 = new Dictionary<string, int>(3)
        {
          {
            "french",
            0
          },
          {
            "spanish",
            1
          },
          {
            "german",
            2
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (GameUtility.\u003C\u003Ef__switch\u0024map8.TryGetValue(configLanguage, out num))
      {
        switch (num)
        {
          case 0:
            str = "Home_fr";
            break;
          case 1:
            str = "Home_es";
            break;
          case 2:
            str = "Home_de";
            break;
        }
      }
    }
    return str;
  }

  public static void SetToggle(Toggle toggle, bool value)
  {
    Toggle.ToggleEvent onValueChanged = (Toggle.ToggleEvent) toggle.onValueChanged;
    ToggleGroup group = toggle.get_group();
    toggle.onValueChanged = (__Null) GameUtility.nullToggleEvent;
    toggle.set_group((ToggleGroup) null);
    toggle.set_isOn(value);
    toggle.onValueChanged = (__Null) onValueChanged;
    toggle.set_group(group);
  }

  public static bool IsDebugBuild
  {
    get
    {
      return Debug.get_isDebugBuild();
    }
  }

  public static bool IsStripBuild
  {
    get
    {
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) Resources.Load<TextAsset>("strip"), (UnityEngine.Object) null);
    }
  }

  public static StringBuilder GetStringBuilder()
  {
    GameUtility.mSB.Length = 0;
    return GameUtility.mSB;
  }

  public static void ReparentGameObjects<T>(List<T> objects, Transform newParent) where T : Component
  {
    if (objects == null)
      return;
    for (int index = 0; index < objects.Count; ++index)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) (object) objects[index], (UnityEngine.Object) null))
        objects[index].get_transform().SetParent(newParent, false);
    }
  }

  public static void ReparentGameObjects(List<GameObject> objects, Transform newParent)
  {
    if (objects == null)
      return;
    for (int index = 0; index < objects.Count; ++index)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) objects[index], (UnityEngine.Object) null))
        objects[index].get_transform().SetParent(newParent, false);
    }
  }

  public static void ToggleGraphic(GameObject go, bool enable)
  {
    foreach (Behaviour componentsInChild in (Graphic[]) go.GetComponentsInChildren<Graphic>(false))
      componentsInChild.set_enabled(enable);
  }

  public static MoveInputMethods Config_InputMethod
  {
    get
    {
      return (MoveInputMethods) PlayerPrefsUtility.GetInt(PlayerPrefsUtility.CONFIG_INPUTMETHOD, 0);
    }
    set
    {
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.CONFIG_INPUTMETHOD, (int) value, false);
    }
  }

  public static float Config_SoundVolume
  {
    get
    {
      if (0.0 <= (double) GameUtility.m_sound_volume)
        return GameUtility.m_sound_volume;
      GameUtility.m_sound_volume = PlayerPrefsUtility.GetFloat(PlayerPrefsUtility.CONFIG_SOUNDVOLUME, 1f);
      return GameUtility.m_sound_volume;
    }
    set
    {
      GameUtility.m_sound_volume = value;
      PlayerPrefsUtility.SetFloat(PlayerPrefsUtility.CONFIG_SOUNDVOLUME, value, false);
    }
  }

  public static float Config_MusicVolume
  {
    get
    {
      if (0.0 <= (double) GameUtility.m_music_volume)
        return GameUtility.m_music_volume;
      GameUtility.m_music_volume = PlayerPrefsUtility.GetFloat(PlayerPrefsUtility.CONFIG_MUSICVOLUME, 1f);
      return GameUtility.m_music_volume;
    }
    set
    {
      GameUtility.m_music_volume = value;
      PlayerPrefsUtility.SetFloat(PlayerPrefsUtility.CONFIG_MUSICVOLUME, value, false);
    }
  }

  public static float Config_VoiceVolume
  {
    get
    {
      if (0.0 <= (double) GameUtility.m_voice_volume)
        return GameUtility.m_voice_volume;
      GameUtility.m_voice_volume = PlayerPrefsUtility.GetFloat(PlayerPrefsUtility.CONFIG_VOICEVOLUME, 1f);
      return GameUtility.m_voice_volume;
    }
    set
    {
      GameUtility.m_voice_volume = value;
      PlayerPrefsUtility.SetFloat(PlayerPrefsUtility.CONFIG_VOICEVOLUME, value, false);
    }
  }

  public static string Config_OkyakusamaCode
  {
    get
    {
      return PlayerPrefsUtility.GetString(PlayerPrefsUtility.CONFIG_OKYAKUSAMACODE, (string) null);
    }
    set
    {
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONFIG_OKYAKUSAMACODE, value, true);
    }
  }

  public static string getLoginInfoRead()
  {
    string empty = string.Empty;
    return (string) MonoSingleton<UserInfoManager>.Instance.GetValue(GameUtility.LOGININFO_ALREADY_READ);
  }

  public static void setLoginInfoRead(string value)
  {
    string str = (string) MonoSingleton<UserInfoManager>.Instance.GetValue(GameUtility.LOGININFO_ALREADY_READ);
    if (value == null || !(str != value))
      return;
    MonoSingleton<UserInfoManager>.Instance.SetValue(GameUtility.LOGININFO_ALREADY_READ, (object) value, true);
  }

  public static bool isLoginInfoDisplay()
  {
    string loginInfoRead = GameUtility.getLoginInfoRead();
    if (string.IsNullOrEmpty(loginInfoRead))
      return true;
    return TimeManager.FromDateTime(TimeManager.ServerTime) > TimeManager.FromDateTime(DateTime.Parse(loginInfoRead).AddDays(1.0));
  }

  public static string DevServerSetting
  {
    get
    {
      if (PlayerPrefs.HasKey(GameUtility.CONFIG_DEVSETTING))
        return PlayerPrefs.GetString(GameUtility.CONFIG_DEVSETTING);
      PlayerPrefs.SetString(GameUtility.CONFIG_DEVSETTING, "http://dev06-app.alcww.gumi.sg/");
      return "http://dev06-app.alcww.gumi.sg/";
    }
    set
    {
      PlayerPrefs.SetString(GameUtility.CONFIG_DEVSETTING, value);
      PlayerPrefs.Save();
    }
  }

  public static bool GetUnitShowSetting(int index)
  {
    if (index < 0 || index >= GameUtility.UnitShowSettings.Length)
      return false;
    if (!PlayerPrefsUtility.HasKey(GameUtility.UnitShowSettings[index].key))
      return GameUtility.UnitShowSettings[index].on != 0;
    return PlayerPrefsUtility.GetInt(GameUtility.UnitShowSettings[index].key, 0) != 0;
  }

  public static void SetUnitShowSetting(int index, bool value)
  {
    if (index < 0 || index >= GameUtility.UnitShowSettings.Length)
      return;
    PlayerPrefsUtility.SetInt(GameUtility.UnitShowSettings[index].key, !value ? 0 : 1, false);
  }

  public static void ResetUnitShowSetting()
  {
    for (int index = 0; index < GameUtility.UnitShowSettings.Length; ++index)
      GameUtility.SetUnitShowSetting(index, GameUtility.UnitShowSettings[index].on != 0);
  }

  public static void SortUnits(List<UnitData> units, GameUtility.UnitSortModes type, bool ascending, out int[] sortValues, bool outputSortedValues = false)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GameUtility.\u003CSortUnits\u003Ec__AnonStorey1E7 unitsCAnonStorey1E7 = new GameUtility.\u003CSortUnits\u003Ec__AnonStorey1E7();
    List<SortKeyValue<UnitData, int>> sortKeyValueList = new List<SortKeyValue<UnitData, int>>(units.Count);
    for (int index = 0; index < units.Count; ++index)
      sortKeyValueList.Add(new SortKeyValue<UnitData, int>()
      {
        mValue = units[index]
      });
    sortValues = new int[units.Count];
    // ISSUE: reference to a compiler-generated field
    unitsCAnonStorey1E7.compares = new List<Func<UnitData, UnitData, int>>();
    // ISSUE: reference to a compiler-generated field
    unitsCAnonStorey1E7.compares.Clear();
    // ISSUE: reference to a compiler-generated field
    unitsCAnonStorey1E7.compares.Add(new Func<UnitData, UnitData, int>(UnitData.CompareTo_Lv));
    // ISSUE: reference to a compiler-generated field
    unitsCAnonStorey1E7.compares.Add(new Func<UnitData, UnitData, int>(UnitData.CompareTo_Rarity));
    // ISSUE: reference to a compiler-generated field
    unitsCAnonStorey1E7.compares.Add(new Func<UnitData, UnitData, int>(UnitData.CompareTo_JobRank));
    // ISSUE: reference to a compiler-generated field
    unitsCAnonStorey1E7.compares.Add(new Func<UnitData, UnitData, int>(UnitData.CompareTo_RarityMax));
    // ISSUE: reference to a compiler-generated field
    unitsCAnonStorey1E7.compares.Add(new Func<UnitData, UnitData, int>(UnitData.CompareTo_RarityInit));
    switch (type)
    {
      case GameUtility.UnitSortModes.Time:
        return;
      case GameUtility.UnitSortModes.Level:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = units[index].Lv;
        // ISSUE: reference to a compiler-generated field
        unitsCAnonStorey1E7.compares.Remove(new Func<UnitData, UnitData, int>(UnitData.CompareTo_Lv));
        break;
      case GameUtility.UnitSortModes.JobRank:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = units[index].CurrentJob.Rank;
        // ISSUE: reference to a compiler-generated field
        unitsCAnonStorey1E7.compares.Remove(new Func<UnitData, UnitData, int>(UnitData.CompareTo_JobRank));
        break;
      case GameUtility.UnitSortModes.HP:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.hp;
        break;
      case GameUtility.UnitSortModes.Atk:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.atk;
        break;
      case GameUtility.UnitSortModes.Def:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.def;
        break;
      case GameUtility.UnitSortModes.Mag:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.mag;
        break;
      case GameUtility.UnitSortModes.Mnd:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.mnd;
        break;
      case GameUtility.UnitSortModes.Spd:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = (int) units[index].Status.param.spd;
        break;
      case GameUtility.UnitSortModes.Total:
        for (int index = 0; index < units.Count; ++index)
        {
          sortKeyValueList[index].mKey = (int) units[index].Status.param.atk;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.def;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.mag;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.mnd;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.spd;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.dex;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.cri;
          sortKeyValueList[index].mKey += (int) units[index].Status.param.luk;
        }
        break;
      case GameUtility.UnitSortModes.Awake:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = units[index].AwakeLv;
        break;
      case GameUtility.UnitSortModes.Combination:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = units[index].GetCombination();
        break;
      case GameUtility.UnitSortModes.Rarity:
        for (int index = 0; index < units.Count; ++index)
          sortKeyValueList[index].mKey = units[index].Rarity;
        // ISSUE: reference to a compiler-generated field
        unitsCAnonStorey1E7.compares.Remove(new Func<UnitData, UnitData, int>(UnitData.CompareTo_Rarity));
        break;
    }
    for (int index = 0; index < units.Count; ++index)
      sortValues[index] = sortKeyValueList[index].mKey;
    // ISSUE: reference to a compiler-generated field
    unitsCAnonStorey1E7.compareResult = 0;
    // ISSUE: reference to a compiler-generated method
    sortKeyValueList.Sort(new Comparison<SortKeyValue<UnitData, int>>(unitsCAnonStorey1E7.\u003C\u003Em__138));
    if (outputSortedValues)
    {
      for (int index = 0; index < sortKeyValueList.Count; ++index)
      {
        units[index] = sortKeyValueList[index].mValue;
        sortValues[index] = sortKeyValueList[index].mKey;
      }
    }
    else
    {
      for (int index = 0; index < sortKeyValueList.Count; ++index)
        units[index] = sortKeyValueList[index].mValue;
    }
  }

  public static void SortUnits(List<UnitData> units, GameUtility.UnitSortModes type, bool ascending)
  {
    switch (type)
    {
      case GameUtility.UnitSortModes.Level:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? dsc.Lv - src.Lv : src.Lv - dsc.Lv;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.JobRank:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? dsc.CurrentJob.Rank - src.CurrentJob.Rank : src.CurrentJob.Rank - dsc.CurrentJob.Rank;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.HP:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.hp - (int) src.Status.param.hp : (int) src.Status.param.hp - (int) dsc.Status.param.hp;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Atk:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.atk - (int) src.Status.param.atk : (int) src.Status.param.atk - (int) dsc.Status.param.atk;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Def:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.def - (int) src.Status.param.def : (int) src.Status.param.def - (int) dsc.Status.param.def;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Mag:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.mag - (int) src.Status.param.mag : (int) src.Status.param.mag - (int) dsc.Status.param.mag;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Mnd:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.mnd - (int) src.Status.param.mnd : (int) src.Status.param.mnd - (int) dsc.Status.param.mnd;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Spd:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? (int) dsc.Status.param.spd - (int) src.Status.param.spd : (int) src.Status.param.spd - (int) dsc.Status.param.spd;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Total:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num1 = 0 + (int) src.Status.param.atk + (int) src.Status.param.def + (int) src.Status.param.mag + (int) src.Status.param.mnd + (int) src.Status.param.spd + (int) src.Status.param.dex + (int) src.Status.param.cri + (int) src.Status.param.luk;
          int num2 = 0 + (int) dsc.Status.param.atk + (int) dsc.Status.param.def + (int) dsc.Status.param.mag + (int) dsc.Status.param.mnd + (int) dsc.Status.param.spd + (int) dsc.Status.param.dex + (int) dsc.Status.param.cri + (int) dsc.Status.param.luk;
          int num3 = !ascending ? num2 - num1 : num1 - num2;
          if (num3 == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num3;
        }));
        break;
      case GameUtility.UnitSortModes.Awake:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? dsc.AwakeLv - src.AwakeLv : src.AwakeLv - dsc.AwakeLv;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Combination:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? dsc.GetCombination() - src.GetCombination() : src.GetCombination() - dsc.GetCombination();
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
      case GameUtility.UnitSortModes.Rarity:
        units.Sort((Comparison<UnitData>) ((src, dsc) =>
        {
          int num = !ascending ? dsc.Rarity - src.Rarity : src.Rarity - dsc.Rarity;
          if (num == 0)
            return (int) (src.UniqueID - dsc.UniqueID);
          return num;
        }));
        break;
    }
  }

  public static void ApplyAvoidance(ref Vector4[] points, int iteration, float aspectRatio)
  {
    bool flag = false;
    for (int index = 0; index < points.Length; ++index)
    {
      if (points[index].w <= 1.0 / 1000.0)
        points[index].w = (__Null) (1.0 / 1000.0);
      flag |= points[index].z > 0.0;
    }
    if (!flag)
      return;
    float num1 = (double) aspectRatio <= 0.0 ? 0.0f : 1f / aspectRatio;
    for (int index1 = 0; index1 < iteration; ++index1)
    {
      Vector2[] vector2Array = new Vector2[points.Length];
      for (int index2 = 0; index2 < points.Length; ++index2)
      {
        for (int index3 = index2 + 1; index3 < points.Length; ++index3)
        {
          Vector2 vector2;
          // ISSUE: explicit reference operation
          ((Vector2) @vector2).\u002Ector((float) (points[index3].x - points[index2].x), (float) (points[index3].y - points[index2].y));
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local1 = @vector2;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local1).y = (__Null) ((^local1).y * (double) num1);
          // ISSUE: explicit reference operation
          float num2 = (float) (points[index2].z + points[index3].z) - ((Vector2) @vector2).get_magnitude();
          if ((double) num2 > 0.0)
          {
            // ISSUE: explicit reference operation
            ((Vector2) @vector2).Normalize();
            float num3 = (float) (points[index2].w + points[index3].w);
            if ((double) num3 <= 0.0)
              num3 = 1f;
            float num4 = 1f / num3 * num2;
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector2& local2 = @vector2Array[index2];
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ^local2 = Vector2.op_Subtraction(^local2, Vector2.op_Multiply(vector2, (float) points[index2].w * num4));
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector2& local3 = @vector2Array[index3];
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ^local3 = Vector2.op_Addition(^local3, Vector2.op_Multiply(vector2, (float) points[index3].w * num4));
          }
        }
      }
      for (int index2 = 0; index2 < points.Length; ++index2)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector4& local1 = @points[index2];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local1).x = (^local1).x + vector2Array[index2].x;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector4& local2 = @points[index2];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local2).y = (^local2).y + vector2Array[index2].y;
      }
    }
  }

  public static string GetGameObjectPath(GameObject go)
  {
    return go.GetPath((GameObject) null);
  }

  public static Color32 ParseColor(string src)
  {
    Color32 color32 = (Color32) null;
    string[] strArray = src.Split(',');
    try
    {
      color32.r = (__Null) (int) (byte) int.Parse(strArray[0]);
      color32.g = (__Null) (int) (byte) int.Parse(strArray[1]);
      color32.b = (__Null) (int) (byte) int.Parse(strArray[2]);
      color32.a = strArray.Length < 4 ? (__Null) (int) byte.MaxValue : (__Null) (int) (byte) int.Parse(strArray[3]);
    }
    catch (Exception ex)
    {
      color32 = Color32.op_Implicit(Color.get_clear());
    }
    return color32;
  }

  public static GameUtility.EScene GetCurrentScene()
  {
    GameUtility.EScene escene = GameUtility.EScene.UNKNOWN;
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
      escene = !PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay ? GameUtility.EScene.BATTLE : GameUtility.EScene.BATTLE_MULTI;
    else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GameObject.Find("SRPG_HOME_MULTIPLAY"), (UnityEngine.Object) null))
      escene = GameUtility.EScene.HOME_MULTI;
    else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GameObject.Find("SRPG_MAINMENU"), (UnityEngine.Object) null))
      escene = GameUtility.EScene.HOME;
    else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GameObject.Find("SRPG_TITLE"), (UnityEngine.Object) null))
      escene = GameUtility.EScene.TITLE;
    return escene;
  }

  public static string ComposeJobIconPath(UnitParam unitparam)
  {
    if (unitparam == null)
      return (string) null;
    JobParam job = (JobParam) null;
    if (unitparam.jobsets != null && unitparam.jobsets.Length > 0)
      job = MonoSingleton<GameManager>.Instance.GetJobParam(MonoSingleton<GameManager>.Instance.GetJobSetParam((string) unitparam.jobsets[0]).job);
    return AssetPath.JobIconSmall(job);
  }

  public static string ComposeQuestBonusObjectiveText(QuestBonusObjective bonus)
  {
    switch (bonus.Type)
    {
      case EMissionType.KillAllEnemy:
        return LocalizedText.Get("sys.BONUS_KILLALL");
      case EMissionType.NoDeath:
        return LocalizedText.Get("sys.BONUS_NODEATH");
      case EMissionType.LimitedTurn:
        return string.Format(LocalizedText.Get("sys.BONUS_LIMITEDTURN"), (object) bonus.TypeParam);
      case EMissionType.ComboCount:
        return string.Format(LocalizedText.Get("sys.BONUS_COMBOCOUNT"), (object) bonus.TypeParam);
      case EMissionType.MaxSkillCount:
        int result1;
        if (int.TryParse(bonus.TypeParam, out result1) && result1 == 0)
          return LocalizedText.Get("sys.BONUS_NOSKILL");
        return string.Format(LocalizedText.Get("sys.BONUS_MAXSKILLCOUNT"), (object) bonus.TypeParam);
      case EMissionType.MaxItemCount:
        int result2;
        if (int.TryParse(bonus.TypeParam, out result2) && result2 == 0)
          return LocalizedText.Get("sys.BONUS_NOITEM");
        return string.Format(LocalizedText.Get("sys.BONUS_MAXITEMCOUNT"), (object) bonus.TypeParam);
      case EMissionType.MaxPartySize:
        return string.Format(LocalizedText.Get("sys.BONUS_MAXPARTYSIZE"), (object) bonus.TypeParam);
      case EMissionType.LimitedUnitElement:
        try
        {
          return string.Format(LocalizedText.Get("sys.BONUS_LIMITELEMENT"), (object) LocalizedText.Get("sys.UNIT_ELEMENT_" + (object) (int) Enum.Parse(typeof (EElement), bonus.TypeParam, true)));
        }
        catch (Exception ex)
        {
        }
        return string.Empty;
      case EMissionType.LimitedUnitID:
        UnitParam unitParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(bonus.TypeParam);
        return string.Format(LocalizedText.Get("sys.BONUS_LIMITUNIT"), unitParam1 == null ? (object) string.Empty : (object) unitParam1.name);
      case EMissionType.NoMercenary:
        return LocalizedText.Get("sys.BONUS_NOMERCENARY");
      case EMissionType.Killstreak:
        return string.Format(LocalizedText.Get("sys.BONUS_KILLSTREAK"), (object) bonus.TypeParam);
      case EMissionType.TotalHealHPMax:
        if (bonus.TypeParam == "0")
          return string.Format(LocalizedText.Get("sys.BONUS_NOHEAL"), (object) bonus.TypeParam);
        return string.Format(LocalizedText.Get("sys.BONUS_TOTALHEALMAX"), (object) bonus.TypeParam);
      case EMissionType.TotalHealHPMin:
        return string.Format(LocalizedText.Get("sys.BONUS_TOTALHEALMIN"), (object) bonus.TypeParam);
      case EMissionType.TotalDamagesTakenMax:
        return string.Format(LocalizedText.Get("sys.BONUS_TOTALDAMAGESTAKENMAX"), (object) bonus.TypeParam);
      case EMissionType.TotalDamagesTakenMin:
        return string.Format(LocalizedText.Get("sys.BONUS_TOTALDAMAGESTAKENMIN"), (object) bonus.TypeParam);
      case EMissionType.TotalDamagesMax:
        return string.Format(LocalizedText.Get("sys.BONUS_TOTALDAMAGESMAX"), (object) bonus.TypeParam);
      case EMissionType.TotalDamagesMin:
        return string.Format(LocalizedText.Get("sys.BONUS_TOTALDAMAGESMIN"), (object) bonus.TypeParam);
      case EMissionType.LimitedCT:
        return string.Format(LocalizedText.Get("sys.BONUS_CTMAX"), (object) bonus.TypeParam);
      case EMissionType.LimitedContinue:
        if (bonus.TypeParam == "0")
          return string.Format(LocalizedText.Get("sys.BONUS_NOCONTINUE"), (object) bonus.TypeParam);
        return string.Format(LocalizedText.Get("sys.BONUS_CONTINUEMAX"), (object) bonus.TypeParam);
      case EMissionType.NoNpcDeath:
        return LocalizedText.Get("sys.BONUS_NONPCDEATH");
      case EMissionType.TargetKillstreak:
        string[] strArray1 = bonus.TypeParam.Split(',');
        string str1 = "XXXX";
        try
        {
          str1 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray1[0].Trim()).name;
        }
        catch (Exception ex)
        {
          DebugUtility.LogError(ex.ToString());
        }
        string str2 = strArray1.Length <= 1 ? "X" : strArray1[1].Trim();
        return string.Format(LocalizedText.Get("sys.BONUS_TARGETKILLSTREAK"), (object) str1, (object) str2);
      case EMissionType.NoTargetDeath:
        string str3 = "XXXX";
        try
        {
          str3 = MonoSingleton<GameManager>.Instance.GetUnitParam(bonus.TypeParam.Trim()).name;
        }
        catch (Exception ex)
        {
          DebugUtility.LogError(ex.ToString());
        }
        return string.Format(LocalizedText.Get("sys.BONUS_NOTARGETDEATH"), (object) str3);
      case EMissionType.BreakObjClashMax:
        string str4 = string.Empty;
        string str5 = "1";
        string[] strArray2 = bonus.TypeParam.Split(',');
        if (strArray2 != null)
        {
          if (strArray2.Length >= 1)
            str4 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray2[0].Trim()).name;
          if (strArray2.Length >= 2)
            str5 = strArray2[1].Trim();
        }
        return string.Format(LocalizedText.Get("sys.BONUS_BREAKOBJCLASHMAX"), (object) str4, (object) str5);
      case EMissionType.BreakObjClashMin:
        string str6 = string.Empty;
        string str7 = "1";
        string[] strArray3 = bonus.TypeParam.Split(',');
        if (strArray3 != null)
        {
          if (strArray3.Length >= 1)
            str6 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray3[0].Trim()).name;
          if (strArray3.Length >= 2)
            str7 = strArray3[1].Trim();
        }
        return string.Format(LocalizedText.Get("sys.BONUS_BREAKOBJCLASHMIN"), (object) str6, (object) str7);
      case EMissionType.WithdrawUnit:
        return string.Format(LocalizedText.Get("sys.BONUS_WITHDRAWUNIT"), (object) MonoSingleton<GameManager>.Instance.GetUnitParam(bonus.TypeParam).name);
      case EMissionType.UseMercenary:
        return string.Format(LocalizedText.Get("sys.BONUS_USEMERCENARY"));
      case EMissionType.LimitedUnitID_MainOnly:
        UnitParam unitParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(bonus.TypeParam);
        return string.Format(LocalizedText.Get("sys.BONUS_LIMITEDUNITID_MAINONLY"), unitParam2 == null ? (object) string.Empty : (object) unitParam2.name);
      case EMissionType.MissionAllCompleteAtOnce:
        return string.Format(LocalizedText.Get("sys.BONUS_MISSIONALLCOMPLETEATONCE"));
      case EMissionType.OnlyTargetArtifactType:
        string empty1 = string.Empty;
        string[] strArray4 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray4.Length; ++index)
        {
          empty1 += strArray4[index];
          if (strArray4.Length > index + 1)
            empty1 += "、";
        }
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYTARGETARTIFACTTYPE"), (object) empty1);
      case EMissionType.OnlyTargetArtifactType_MainOnly:
        string empty2 = string.Empty;
        string[] strArray5 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray5.Length; ++index)
        {
          empty2 += strArray5[index];
          if (strArray5.Length > index + 1)
            empty2 += "、";
        }
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYTARGETARTIFACTTYPE_MAINONLY"), (object) empty2);
      case EMissionType.OnlyTargetJobs:
        string empty3 = string.Empty;
        string[] strArray6 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray6.Length; ++index)
        {
          empty3 += MonoSingleton<GameManager>.Instance.GetJobParam(strArray6[index]).name;
          if (strArray6.Length > index + 1)
            empty3 += "、";
        }
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYTARGETJOBS"), (object) empty3);
      case EMissionType.OnlyTargetJobs_MainOnly:
        string empty4 = string.Empty;
        string[] strArray7 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray7.Length; ++index)
        {
          empty4 += MonoSingleton<GameManager>.Instance.GetJobParam(strArray7[index]).name;
          if (strArray7.Length > index + 1)
            empty4 += "、";
        }
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYTARGETJOBS_MAINONLY"), (object) empty4);
      case EMissionType.OnlyTargetUnitBirthplace:
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYTARGETUNITBIRTHPLACE"), (object) bonus.TypeParam);
      case EMissionType.OnlyTargetUnitBirthplace_MainOnly:
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYTARGETUNITBIRTHPLACE_MAINONLY"), (object) bonus.TypeParam);
      case EMissionType.OnlyTargetSex:
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYTARGETSEX"), (object) LocalizedText.Get("sys.SEX_" + bonus.TypeParam));
      case EMissionType.OnlyTargetSex_MainOnly:
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYTARGETSEX_MAINONLY"), (object) LocalizedText.Get("sys.SEX_" + bonus.TypeParam));
      case EMissionType.OnlyHeroUnit:
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYHEROUNIT"));
      case EMissionType.OnlyHeroUnit_MainOnly:
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYHEROUNIT_MAINONLY"));
      case EMissionType.Finisher:
        UnitParam unitParam3 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(bonus.TypeParam);
        return string.Format(LocalizedText.Get("sys.BONUS_FINISHER"), unitParam3 == null ? (object) string.Empty : (object) unitParam3.name);
      case EMissionType.TotalGetTreasureCount:
        return string.Format(LocalizedText.Get("sys.BONUS_TOTALGETTREASURECOUNT"), (object) bonus.TypeParam);
      case EMissionType.KillstreakByUsingTargetItem:
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(bonus.TypeParam);
        return string.Format(LocalizedText.Get("sys.BONUS_KILLSTREAKBYUSINGTARGETITEM"), itemParam == null ? (object) string.Empty : (object) itemParam.name);
      case EMissionType.KillstreakByUsingTargetSkill:
        SkillParam skillParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(bonus.TypeParam);
        return string.Format(LocalizedText.Get("sys.BONUS_KILLSTREAKBYUSINGTARGETSKILL"), skillParam1 == null ? (object) string.Empty : (object) skillParam1.name);
      case EMissionType.MaxPartySize_IgnoreFriend:
        return string.Format(LocalizedText.Get("sys.BONUS_MAXPARTYSIZE_IGNOREFRIEND"), (object) bonus.TypeParam);
      case EMissionType.NoAutoMode:
        return string.Format(LocalizedText.Get("sys.BONUS_NOAUTOMODE"));
      case EMissionType.NoDeath_NoContinue:
        return string.Format(LocalizedText.Get("sys.BONUS_NODEATH_NOCONTINUE"));
      case EMissionType.OnlyTargetUnits:
        string empty5 = string.Empty;
        string[] strArray8 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray8.Length; ++index)
        {
          empty5 += MonoSingleton<GameManager>.Instance.GetUnitParam(strArray8[index]).name;
          if (strArray8.Length > index + 1)
            empty5 += "、";
        }
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYTARGETUNITS"), (object) empty5);
      case EMissionType.OnlyTargetUnits_MainOnly:
        string empty6 = string.Empty;
        string[] strArray9 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray9.Length; ++index)
        {
          empty6 += MonoSingleton<GameManager>.Instance.GetUnitParam(strArray9[index]).name;
          if (strArray9.Length > index + 1)
            empty6 += "、";
        }
        return string.Format(LocalizedText.Get("sys.BONUS_ONLYTARGETUNITS_MAINONLY"), (object) empty6);
      case EMissionType.LimitedTurn_Leader:
        return string.Format(LocalizedText.Get("sys.BONUS_LIMITEDTURN_LEADER"), (object) bonus.TypeParam);
      case EMissionType.NoDeathTargetNpcUnits:
        string empty7 = string.Empty;
        string[] strArray10 = bonus.TypeParam.Split(',');
        for (int index = 0; index < strArray10.Length; ++index)
        {
          empty7 += MonoSingleton<GameManager>.Instance.GetUnitParam(strArray10[index]).name;
          if (strArray10.Length > index + 1)
            empty7 += "、";
        }
        return string.Format(LocalizedText.Get("sys.BONUS_NODEATHTARGETNPCUNITS"), (object) empty7);
      case EMissionType.UseTargetSkill:
        SkillParam skillParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(bonus.TypeParam);
        return string.Format(LocalizedText.Get("sys.BONUS_USETARGETSKILL"), skillParam2 == null ? (object) string.Empty : (object) skillParam2.name);
      case EMissionType.TotalKillstreakCount:
        return string.Format(LocalizedText.Get("sys.BONUS_TOTALKILLSTREAKCOUNT"), (object) bonus.TypeParam);
      case EMissionType.TotalGetGemCount_Over:
        return string.Format(LocalizedText.Get("sys.BONUS_TOTALGETGEMCOUNT_OVER"), (object) bonus.TypeParam);
      case EMissionType.TotalGetGemCount_Less:
        return string.Format(LocalizedText.Get("sys.BONUS_TOTALGETGEMCOUNT_LESS"), (object) bonus.TypeParam);
      default:
        return bonus.Type.ToString();
    }
  }

  public static string ComposeCharacterQuestMainUnitConditionText(UnitData unit, QuestParam param)
  {
    string str = string.Format(LocalizedText.Get("sys.QUEST_CHARACTER_CONDITION"), (object) unit.UnitParam.name, (object) Mathf.Max(param.EntryConditionCh.ulvmin, 1));
    List<string> questConditionsCh = param.GetEntryQuestConditionsCh(true, true, true);
    List<string> unlockConditions = unit.GetQuestUnlockConditions(param);
    if (unlockConditions != null && unlockConditions.Count >= 1 || questConditionsCh != null && questConditionsCh.Count >= 2)
      str += LocalizedText.Get("sys.QUEST_CHARACTER_CONDITION_OTHER");
    return str;
  }

  public static double InternalToMapHeight(double InternalHeight)
  {
    return InternalHeight * 0.5;
  }

  public static float InternalToMapHeight(float InternalHeight)
  {
    return InternalHeight * 0.5f;
  }

  public static double MapToInternalHeight(double MapHeight)
  {
    return MapHeight * 2.0;
  }

  public static float MapToInternalHeight(float MapHeight)
  {
    return MapHeight * 2f;
  }

  public static Vector3 RaycastGround(Vector3 position)
  {
    RaycastHit raycastHit;
    if (Physics.Raycast(new Vector3((float) position.x, 1000f, (float) position.z), Vector3.op_UnaryNegation(Vector3.get_up()), ref raycastHit))
    {
      // ISSUE: explicit reference operation
      return ((RaycastHit) @raycastHit).get_point();
    }
    position.y = (__Null) 0.0;
    return position;
  }

  public static float CalcHeight(float x, float y)
  {
    float num1 = 100f;
    float num2 = Mathf.Floor(x - 0.5f) + 0.5f;
    float num3 = Mathf.Floor(y - 0.5f) + 0.5f;
    float num4 = Mathf.Ceil(x - 0.5f) + 0.5f;
    float num5 = Mathf.Ceil(y - 0.5f) + 0.5f;
    float num6 = 0.0f;
    float num7 = 0.0f;
    float num8 = 0.0f;
    float num9 = 0.0f;
    RaycastHit raycastHit;
    if (Physics.Raycast(new Vector3(num2, num1, num3), Vector3.op_UnaryNegation(Vector3.get_up()), ref raycastHit))
    {
      // ISSUE: explicit reference operation
      num6 = (float) ((RaycastHit) @raycastHit).get_point().y;
    }
    if (Physics.Raycast(new Vector3(num4, num1, num3), Vector3.op_UnaryNegation(Vector3.get_up()), ref raycastHit))
    {
      // ISSUE: explicit reference operation
      num7 = (float) ((RaycastHit) @raycastHit).get_point().y;
    }
    if (Physics.Raycast(new Vector3(num2, num1, num5), Vector3.op_UnaryNegation(Vector3.get_up()), ref raycastHit))
    {
      // ISSUE: explicit reference operation
      num8 = (float) ((RaycastHit) @raycastHit).get_point().y;
    }
    if (Physics.Raycast(new Vector3(num4, num1, num5), Vector3.op_UnaryNegation(Vector3.get_up()), ref raycastHit))
    {
      // ISSUE: explicit reference operation
      num9 = (float) ((RaycastHit) @raycastHit).get_point().y;
    }
    float num10 = x - num2;
    float num11 = y - num3;
    return Mathf.Lerp(Mathf.Lerp(num6, num7, num10), Mathf.Lerp(num8, num9, num10), num11);
  }

  public static Vector3 RaycastGround(Vector2 position)
  {
    return GameUtility.RaycastGround(new Vector3((float) position.x, 0.0f, (float) position.y));
  }

  public static float RaycastGround(float x, float z)
  {
    return (float) GameUtility.RaycastGround(new Vector3(x, 0.0f, z)).y;
  }

  public static float CalcDistance2D(Vector3 a, Vector3 b)
  {
    a = Vector3.op_Subtraction(a, b);
    a.y = (__Null) 0.0;
    // ISSUE: explicit reference operation
    return ((Vector3) @a).get_magnitude();
  }

  public static float CalcDistance2D(Vector3 a)
  {
    a.y = (__Null) 0.0;
    // ISSUE: explicit reference operation
    return ((Vector3) @a).get_magnitude();
  }

  private static void HandleLog(string logString, string stackTrace, LogType type)
  {
    if (GameUtility.mLogCallbacks == null)
      return;
    GameUtility.mLogCallbacks.Invoke(logString, stackTrace, type);
  }

  public static void RegisterLogCallback(Application.LogCallback callback)
  {
    GameUtility.mLogCallbacks = GameUtility.mLogCallbacks == null ? callback : (Application.LogCallback) System.Delegate.Combine((System.Delegate) GameUtility.mLogCallbacks, (System.Delegate) callback);
    // ISSUE: method pointer
    Application.add_logMessageReceived(new Application.LogCallback((object) null, __methodptr(HandleLog)));
  }

  public static void UnregisterLogCallback(Application.LogCallback callback)
  {
    GameUtility.mLogCallbacks = (Application.LogCallback) System.Delegate.Remove((System.Delegate) GameUtility.mLogCallbacks, (System.Delegate) callback);
  }

  public static float EvaluateCurveLooped(AnimationCurve curve, float time)
  {
    if (curve == null || curve.get_length() < 2)
      return 0.0f;
    Keyframe keyframe1 = curve.get_Item(curve.get_length() - 1);
    // ISSUE: explicit reference operation
    double time1 = (double) ((Keyframe) @keyframe1).get_time();
    Keyframe keyframe2 = curve.get_Item(0);
    // ISSUE: explicit reference operation
    double time2 = (double) ((Keyframe) @keyframe2).get_time();
    float num1 = (float) (time1 - time2);
    if ((double) num1 <= 0.0)
      return 0.0f;
    double num2 = (double) time;
    Keyframe keyframe3 = curve.get_Item(0);
    // ISSUE: explicit reference operation
    double time3 = (double) ((Keyframe) @keyframe3).get_time();
    double num3 = (num2 - time3) % (double) num1;
    Keyframe keyframe4 = curve.get_Item(0);
    // ISSUE: explicit reference operation
    double time4 = (double) ((Keyframe) @keyframe4).get_time();
    time = (float) (num3 + time4);
    return curve.Evaluate(time);
  }

  public static void RemoveDuplicatedMainCamera()
  {
    Camera main = Camera.get_main();
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) main, (UnityEngine.Object) null))
      return;
    Camera[] objectsOfType = (Camera[]) UnityEngine.Object.FindObjectsOfType<Camera>();
    for (int index = objectsOfType.Length - 1; index >= 0; --index)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) objectsOfType[index], (UnityEngine.Object) main) && ((Component) objectsOfType[index]).CompareTag("MainCamera"))
        UnityEngine.Object.DestroyImmediate((UnityEngine.Object) ((Component) objectsOfType[index]).get_gameObject());
    }
  }

  public static bool IsATCTextureSupported
  {
    get
    {
      GameUtility.CacheGLExtensions();
      return GameUtility.mATCTextureSupport;
    }
  }

  public static bool IsDXTTextureSupported
  {
    get
    {
      GameUtility.CacheGLExtensions();
      return GameUtility.mDXTTextureSupport;
    }
  }

  public static bool IsPVRTextureSupported
  {
    get
    {
      GameUtility.CacheGLExtensions();
      return GameUtility.mPVRTextureSupport;
    }
  }

  public static bool IsASTCTextureSupported
  {
    get
    {
      GameUtility.CacheGLExtensions();
      return GameUtility.mASTCTextureSupport;
    }
  }

  public static void PrintGLExtensions()
  {
    GameUtility.CacheGLExtensions();
    for (int index = 0; index < GameUtility.mGLExtensions.Length; ++index)
      Debug.Log((object) GameUtility.mGLExtensions[index]);
  }

  public static bool IsGLExtensionSupported(string extName)
  {
    GameUtility.CacheGLExtensions();
    for (int index = GameUtility.mGLExtensions.Length - 1; index >= 0; --index)
    {
      if (GameUtility.mGLExtensions[index] == extName)
        return true;
    }
    return false;
  }

  private static void CacheGLExtensions()
  {
    if (GameUtility.mGLExtensions != null)
      return;
    byte[] numArray = new byte[2048];
    NativePlugin.GetGLExtensions(numArray, numArray.Length);
    GameUtility.mGLExtensions = Encoding.ASCII.GetString(numArray).Split(' ');
    GameUtility.mPVRTextureSupport = GameUtility.IsGLExtensionSupported("GL_IMG_texture_compression_pvrtc");
    GameUtility.mDXTTextureSupport = GameUtility.IsGLExtensionSupported("GL_EXT_texture_compression_s3tc");
    GameUtility.mATCTextureSupport = GameUtility.IsGLExtensionSupported("GL_AMD_compressed_ATC_texture");
    GameUtility.mASTCTextureSupport = GameUtility.IsGLExtensionSupported("GL_OES_texture_compression_astc");
  }

  [DllImport("NativePlugin")]
  private static extern int GetGLBufferNum();

  public static string GetBufferNum()
  {
    return GameUtility.GetGLBufferNum().ToString();
  }

  [DllImport("NativePlugin")]
  private static extern float GetGLUsedBufferSize();

  public static string GetUsedBufferSize()
  {
    return GameUtility.GetGLUsedBufferSize().ToString() + "MB";
  }

  [DllImport("NativePlugin")]
  private static extern float GetGLUsedRenderSize();

  public static string GetUsedRenderSize()
  {
    return GameUtility.GetGLUsedRenderSize().ToString() + ":MB";
  }

  [DllImport("NativePlugin")]
  private static extern int GetGLTextureNum();

  public static string GetTextureNum()
  {
    return GameUtility.GetGLTextureNum().ToString();
  }

  [DllImport("NativePlugin")]
  private static extern int IsTextureOnRam(uint texture);

  public static string GetUsedTextureSize()
  {
    Texture[] objectsOfTypeAll = Resources.FindObjectsOfTypeAll(typeof (Texture)) as Texture[];
    float num1 = 0.0f;
    foreach (Texture texture in objectsOfTypeAll)
    {
      if (GameUtility.IsTextureOnRam((uint) texture.GetNativeTextureID()) > 0)
      {
        float num2 = 32f;
        if (GameUtility.IsPVRTextureSupported)
          num2 = 8f;
        else if (GameUtility.IsDXTTextureSupported)
          num2 = 8f;
        else if (GameUtility.IsATCTextureSupported)
          num2 = 8f;
        num1 += (float) (texture.get_width() * texture.get_height()) * num2;
      }
    }
    return (num1 / 8f / 1024f / 1024f).ToString() + ":MB";
  }

  public static bool ValidateAnimator(Animator animator)
  {
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) animator, (UnityEngine.Object) null))
      return animator.get_layerCount() > 0;
    return false;
  }

  public static void EnableBehaviour<T>(GameObject go, bool enable) where T : Behaviour
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    Behaviour component = (Behaviour) (object) (T) go.GetComponent(typeof (T));
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      return;
    component.set_enabled(enable);
  }

  public static void EnableBehaviour<T>(Component go, bool enable) where T : Behaviour
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    Behaviour component = (Behaviour) (object) (T) go.GetComponent(typeof (T));
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      return;
    component.set_enabled(enable);
  }

  public static T GetArrayElementSafe<T>(T[] list, int index)
  {
    if (list != null && 0 <= index && index < list.Length)
      return list[index];
    return default (T);
  }

  public static bool IsScreenFading
  {
    get
    {
      return FadeController.Instance.IsFading(0);
    }
  }

  public static void FadeIn(float time)
  {
    FadeController.Instance.FadeTo(GameUtility.mFadeInColor, time, 0);
  }

  public static void FadeOut(float time)
  {
    FadeController.Instance.FadeTo(Color.get_black(), time, 0);
  }

  private static int GetLayerIndex(string name)
  {
    int num = LayerMask.NameToLayer(name);
    if (num < 0)
    {
      Debug.LogError((object) ("Layer '" + name + "' not found."));
      num = 0;
    }
    return num;
  }

  private static void InitializeLayerIndices()
  {
    GameUtility.mLayerBG = GameUtility.GetLayerIndex("BG");
    GameUtility.mLayerHidden = GameUtility.GetLayerIndex("HIDDEN");
    GameUtility.mLayerDefault = GameUtility.GetLayerIndex("Default");
    GameUtility.mLayerUI = GameUtility.GetLayerIndex("UI");
    GameUtility.mLayerCH = GameUtility.GetLayerIndex("CH");
    GameUtility.mLayerCH0 = GameUtility.GetLayerIndex("CH0");
    GameUtility.mLayerCH1 = GameUtility.GetLayerIndex("CH1");
    GameUtility.mLayerCH2 = GameUtility.GetLayerIndex("CH2");
    GameUtility.mLayerCHBG0 = GameUtility.GetLayerIndex("CHBG0");
    GameUtility.mLayerCHBG1 = GameUtility.GetLayerIndex("CHBG1");
    GameUtility.mLayerCHBG2 = GameUtility.GetLayerIndex("CHBG2");
    GameUtility.mLayerEffect = GameUtility.GetLayerIndex("EFFECT");
    GameUtility.mLayerIndexInitialized = true;
  }

  public static int LayerDefault
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerDefault;
    }
  }

  public static int LayerBG
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerBG;
    }
  }

  public static int LayerCH
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCH;
    }
  }

  public static int LayerHidden
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerHidden;
    }
  }

  public static int LayerUI
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerUI;
    }
  }

  public static int LayerEffect
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerEffect;
    }
  }

  public static int LayerCH0
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCH0;
    }
  }

  public static int LayerCH1
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCH1;
    }
  }

  public static int LayerCH2
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCH2;
    }
  }

  public static int LayerCHBG0
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCHBG0;
    }
  }

  public static int LayerCHBG1
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCHBG1;
    }
  }

  public static int LayerCHBG2
  {
    get
    {
      if (!GameUtility.mLayerIndexInitialized)
        GameUtility.InitializeLayerIndices();
      return GameUtility.mLayerCHBG2;
    }
  }

  public static int LayerMaskEffect
  {
    get
    {
      return 1 << GameUtility.LayerEffect;
    }
  }

  public static int LayerMaskCH
  {
    get
    {
      return 1 << GameUtility.LayerCH;
    }
  }

  public static int LayerMaskBG
  {
    get
    {
      return 1 << GameUtility.LayerBG;
    }
  }

  public static int LayerMaskHidden
  {
    get
    {
      return 1 << GameUtility.LayerHidden;
    }
  }

  public static int LayerMaskUI
  {
    get
    {
      return 1 << GameUtility.LayerUI;
    }
  }

  public static Texture2D TransparentTexture
  {
    get
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GameUtility.mTransparentTexture, (UnityEngine.Object) null))
      {
        GameUtility.mTransparentTexture = new Texture2D(1, 1, (TextureFormat) 4, false);
        GameUtility.mTransparentTexture.SetPixel(0, 0, Color.get_clear());
        GameUtility.mTransparentTexture.Apply();
      }
      return GameUtility.mTransparentTexture;
    }
  }

  public static void DeactivateActiveChildComponents<T>(Component parent)
  {
    Component[] componentsInChildren = parent.GetComponentsInChildren(typeof (T));
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (componentsInChildren[index].get_gameObject().get_activeSelf())
        componentsInChildren[index].get_gameObject().SetActive(false);
    }
  }

  public static Vector3 DeformPosition(Vector3 pos, float zOffset)
  {
    float num1 = (float) ((pos.z - (double) zOffset) * GameUtility._deformRadiusLength.y);
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    Vector3& local = @pos;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).y = (^local).y + GameUtility._deformRadiusLength.x;
    pos.z = (__Null) 0.0;
    float num2 = Mathf.Cos(num1);
    float num3 = Mathf.Sin(num1);
    Vector3 vector3_1;
    // ISSUE: explicit reference operation
    ((Vector3) @vector3_1).\u002Ector(0.0f, num2, -num3);
    Vector3 vector3_2;
    // ISSUE: explicit reference operation
    ((Vector3) @vector3_2).\u002Ector(0.0f, num3, num2);
    return new Vector3((float) pos.x, Vector3.Dot(pos, vector3_1) - (float) GameUtility._deformRadiusLength.x, Vector3.Dot(pos, vector3_2) + zOffset);
  }

  public static T GetComponentInAllChildren<T>(GameObject go) where T : Component
  {
    Component[] componentsInChildren = go.GetComponentsInChildren(typeof (T), true);
    if (componentsInChildren.Length > 0)
      return (T) componentsInChildren[0];
    return (T) null;
  }

  public static void SetLayer(GameObject go, int layer, bool changeChildren)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    if (changeChildren)
    {
      foreach (Component componentsInChild in (Transform[]) go.GetComponentsInChildren<Transform>(true))
        componentsInChild.get_gameObject().set_layer(layer);
    }
    else
      go.get_gameObject().set_layer(layer);
  }

  public static void SetLayer(Component go, int layer, bool changeChildren)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    GameUtility.SetLayer(go.get_gameObject(), layer, changeChildren);
  }

  public static void SetGameObjectActive(GameObject obj, bool active)
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) obj, (UnityEngine.Object) null))
      return;
    obj.SetActive(active);
  }

  public static void SetGameObjectActive(Component go, bool active)
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    go.get_gameObject().SetActive(active);
  }

  public static void DestroyGameObjects(GameObject[] list)
  {
    for (int index = 0; index < list.Length; ++index)
      GameUtility.DestroyGameObject(list[index]);
  }

  public static void DestroyGameObjects<T>(T[] list) where T : Component
  {
    for (int index = 0; index < list.Length; ++index)
      GameUtility.DestroyGameObject((Component) (object) list[index]);
  }

  public static void DestroyGameObjects(List<GameObject> list)
  {
    for (int index = 0; index < list.Count; ++index)
      GameUtility.DestroyGameObject(list[index]);
  }

  public static void DestroyGameObjects<T>(List<T> list) where T : Component
  {
    for (int index = 0; index < list.Count; ++index)
      GameUtility.DestroyGameObject((Component) (object) list[index]);
  }

  public static void DestroyGameObject(GameObject go)
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    UnityEngine.Object.Destroy((UnityEngine.Object) go.get_gameObject());
  }

  public static void DestroyGameObject(Component go)
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    GameUtility.DestroyGameObject(go.get_gameObject());
  }

  public static bool IsAnimatorRunning(GameObject go)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return false;
    Animator component = (Animator) go.GetComponent<Animator>();
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
      return false;
    AnimatorStateInfo animatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
    // ISSUE: explicit reference operation
    if (((AnimatorStateInfo) @animatorStateInfo).get_loop())
      return true;
    if (component.IsInTransition(0))
      return false;
    // ISSUE: explicit reference operation
    return (double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() < 1.0;
  }

  public static bool IsAnimatorRunning(Component go)
  {
    return GameUtility.IsAnimatorRunning(go.get_gameObject());
  }

  public static bool CompareAnimatorStateName(GameObject go, string stateName)
  {
    AnimatorStateInfo animatorStateInfo = ((Animator) go.GetComponent<Animator>()).GetCurrentAnimatorStateInfo(0);
    // ISSUE: explicit reference operation
    return ((AnimatorStateInfo) @animatorStateInfo).IsName(stateName);
  }

  public static bool CompareAnimatorStateName(Component go, string stateName)
  {
    return GameUtility.CompareAnimatorStateName(go.get_gameObject(), stateName);
  }

  public static void SetAnimatorTrigger(GameObject go, string trigger)
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    Animator component = (Animator) go.GetComponent<Animator>();
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      return;
    component.SetTrigger(trigger);
  }

  public static void SetAnimatorTrigger(Component go, string trigger)
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    GameUtility.SetAnimatorTrigger(go.get_gameObject(), trigger);
  }

  public static void SetAnimatorBool(Component go, string name, bool value)
  {
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    Animator component = (Animator) go.GetComponent<Animator>();
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      return;
    component.SetBool(name, value);
  }

  public static void RemoveComponent<T>(GameObject go) where T : Component
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return;
    Component component;
    while (UnityEngine.Object.op_Inequality((UnityEngine.Object) (component = go.GetComponent(typeof (T))), (UnityEngine.Object) null))
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) component);
  }

  public static T RequireComponent<T>(GameObject go) where T : Component
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
      return (T) null;
    T obj = go.GetComponent<T>();
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) (object) obj, (UnityEngine.Object) null))
      obj = go.AddComponent<T>();
    return obj;
  }

  public static GameObject SpawnParticle(GameObject prefab, Vector3 position, Quaternion rotation, GameObject parentScene)
  {
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) prefab, (UnityEngine.Object) null))
      return (GameObject) null;
    GameObject go = UnityEngine.Object.Instantiate((UnityEngine.Object) prefab, position, rotation) as GameObject;
    go.RequireComponent<OneShotParticle>();
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) parentScene, (UnityEngine.Object) null))
      go.get_transform().SetParent(parentScene.get_transform(), true);
    return go;
  }

  public static GameObject SpawnParticle(GameObject prefab, Transform position, GameObject parentScene)
  {
    return GameUtility.SpawnParticle(prefab, position.get_position(), position.get_rotation(), parentScene);
  }

  public static void StopEmitters(GameObject obj)
  {
    ParticleSystem[] componentsInChildren = (ParticleSystem[]) obj.GetComponentsInChildren<ParticleSystem>(true);
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      ParticleSystem.EmissionModule emission = componentsInChildren[index].get_emission();
      // ISSUE: explicit reference operation
      ((ParticleSystem.EmissionModule) @emission).set_enabled(false);
      componentsInChildren[index].set_loop(false);
    }
  }

  public static LoadRequest LoadResourceAsyncChecked(string path)
  {
    LoadRequest loadRequest = AssetManager.LoadAsync(path);
    if (loadRequest.isDone && UnityEngine.Object.op_Equality(loadRequest.asset, (UnityEngine.Object) null))
      Debug.LogError((object) ("Failed to load resource [" + path + "]"));
    return loadRequest;
  }

  public static LoadRequest LoadResourceAsyncChecked<T>(string path)
  {
    LoadRequest loadRequest = AssetManager.LoadAsync(path, typeof (T));
    if (loadRequest.isDone && UnityEngine.Object.op_Equality(loadRequest.asset, (UnityEngine.Object) null))
      Debug.LogError((object) ("Failed to load resource [" + path + "]"));
    return loadRequest;
  }

  public static LoadRequest LoadResourceAsync(string path)
  {
    return AssetManager.LoadAsync(path);
  }

  public static LoadRequest LoadResourceAsync<T>(string path)
  {
    return AssetManager.LoadAsync(path, typeof (T));
  }

  public static void RequestScene(string name)
  {
    SceneManager.LoadScene(name);
  }

  public static void SetAnimationClip(Animation animation, AnimationClip clip)
  {
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) animation.GetClip(((UnityEngine.Object) clip).get_name()), (UnityEngine.Object) null))
      animation.RemoveClip(((UnityEngine.Object) clip).get_name());
    animation.AddClip(clip, ((UnityEngine.Object) clip).get_name());
  }

  public static void Reparent(Transform child, Transform newParent)
  {
    child.SetParent(newParent, false);
  }

  public static string GetPlatformID()
  {
    switch ((int) Application.get_platform())
    {
      case 0:
      case 1:
        return "mac";
      case 2:
      case 7:
        return "win";
      case 8:
        return "ios";
      case 11:
        return "android";
      default:
        throw new Exception("Unknown Platform:" + ((Enum) (object) Application.get_platform()).ToString());
    }
  }

  public static void CopyTransform(Transform dest, Transform src)
  {
    dest.set_position(src.get_position());
    dest.set_rotation(src.get_rotation());
  }

  public static void InterpTransform(Transform tr, Transform start, Transform end, float t)
  {
    tr.set_position(Vector3.Lerp(start.get_position(), end.get_position(), t));
    tr.set_rotation(Quaternion.Slerp(start.get_rotation(), end.get_rotation(), t));
  }

  public static RectTransform GetRectTransform(GameObject obj)
  {
    return (RectTransform) obj.get_transform();
  }

  public static RectTransform GetRectTransform(Component obj)
  {
    return (RectTransform) obj.get_gameObject().get_transform();
  }

  public static Transform findChildRecursively(Transform parent, string name)
  {
    for (int index = 0; index < parent.get_childCount(); ++index)
    {
      Transform parent1 = parent.GetChild(index);
      if (((UnityEngine.Object) parent1).get_name() == name || UnityEngine.Object.op_Inequality((UnityEngine.Object) (parent1 = GameUtility.findChildRecursively(parent1, name)), (UnityEngine.Object) null))
        return parent1;
    }
    return (Transform) null;
  }

  public static void swap<T>(ref T a, ref T b)
  {
    T obj = a;
    a = b;
    b = obj;
  }

  public static float GUIScale
  {
    get
    {
      return (float) Screen.get_width() / 960f;
    }
  }

  public static float ScaledScreenWidth
  {
    get
    {
      return (float) Screen.get_width() / GameUtility.GUIScale;
    }
  }

  public static float ScaledScreenHeight
  {
    get
    {
      return (float) Screen.get_height() / GameUtility.GUIScale;
    }
  }

  public static void ScaleGUI(float scale = 1f)
  {
    Matrix4x4 identity = Matrix4x4.get_identity();
    identity.m00 = (__Null) (double) (identity.m11 = (__Null) (GameUtility.GUIScale * scale));
    GUI.set_matrix(identity);
  }

  public static string CalcResourceSizeSum<T>()
  {
    return "not available";
  }

  public static string CreateUUID()
  {
    Random random = new Random();
    string empty = string.Empty;
    for (int index = 0; index < 32; ++index)
    {
      int num1 = random.Next(0, 16);
      if (index == 8 || index == 12 || (index == 16 || index == 20))
        empty += "-";
      switch (index)
      {
        case 12:
          empty += Convert.ToString(4, 16);
          break;
        case 16:
          int num2 = num1 & 3 | 8;
          empty += Convert.ToString(num2, 16);
          break;
        default:
          empty += Convert.ToString(num1, 16);
          break;
      }
    }
    return empty;
  }

  public static DateTime UnixtimeToLocalTime(long unixtime)
  {
    return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double) (unixtime - 28800L));
  }

  public static void SetNeverSleep()
  {
    ++GameUtility.mNeverSleepCount;
    GameUtility.UpdateSetting();
  }

  public static void SetDefaultSleepSetting()
  {
    GameUtility.mNeverSleepCount = GameUtility.mNeverSleepCount <= 1 ? 0 : --GameUtility.mNeverSleepCount;
    GameUtility.UpdateSetting();
  }

  private static void UpdateSetting()
  {
    if (GameUtility.mNeverSleepCount > 0)
      Screen.set_sleepTimeout(-1);
    else
      Screen.set_sleepTimeout(-2);
  }

  public static void ForceSetDefaultSleepSetting()
  {
    GameUtility.mNeverSleepCount = 0;
    Screen.set_sleepTimeout(-2);
  }

  public static byte[] Object2Binary<T>(T obj)
  {
    if ((object) obj == null)
      return (byte[]) null;
    if (GameUtility.Packer == null)
      return (byte[]) null;
    return GameUtility.Packer.Pack((object) obj);
  }

  public static bool Binary2Object<T>(out T buffer, byte[] data)
  {
    if (data != null)
    {
      if (GameUtility.Packer != null)
      {
        try
        {
          buffer = GameUtility.Packer.Unpack<T>(data);
          return true;
        }
        catch (Exception ex)
        {
          DebugUtility.LogError(ex.Message);
          buffer = default (T);
          return false;
        }
      }
    }
    buffer = default (T);
    return false;
  }

  public static string HalfNum2FullNum(string str)
  {
    string empty = string.Empty;
    return Regex.Replace(str, "[0-9]", (MatchEvaluator) (m => ((char) (65296 + ((int) m.Value[0] - 48))).ToString()));
  }

  public static int GetSDKLevel()
  {
    IntPtr num = AndroidJNI.FindClass("android.os.Build$VERSION");
    IntPtr staticFieldId = AndroidJNI.GetStaticFieldID(num, "SDK_INT", "I");
    return AndroidJNI.GetStaticIntField(num, staticFieldId);
  }

  public class BooleanConfig
  {
    private string mKey;
    private bool mDefaultValue;

    public BooleanConfig(string configName, bool defaultValue)
    {
      this.mKey = configName;
      this.mDefaultValue = defaultValue;
    }

    public bool Value
    {
      get
      {
        if (!PlayerPrefsUtility.HasKey(this.mKey))
          return this.mDefaultValue;
        return PlayerPrefsUtility.GetInt(this.mKey, 0) != 0;
      }
      set
      {
        PlayerPrefsUtility.SetInt(this.mKey, !value ? 0 : 1, false);
      }
    }
  }

  private struct UnitShowSetting
  {
    public string key;
    public int on;
  }

  public enum UnitShowSettingTypes
  {
    FILTER_FIRE,
    FILTER_WATER,
    FILTER_WIND,
    FILTER_THUNDER,
    FILTER_SHINE,
    FILTER_DARK,
    FILTER_ZENEI,
    FILTER_TYUEI,
    FILTER_KOUEI,
    FILTER_RARE1,
    FILTER_RARE2,
    FILTER_RARE3,
    FILTER_RARE4,
    FILTER_RARE5,
    FILTER_RARE6,
    SORT_SYOJUN,
    SORT_KOUJUN,
    SORT_LEVEL,
    SORT_FAVORITE,
    SORT_JOBRANK,
    SORT_HP,
    SORT_ATK,
    SORT_DEF,
    SORT_MAG,
    SORT_MND,
    SORT_SPD,
    SORT_TOTAL,
    SORT_AWAKE,
    SORT_COMBINATION,
  }

  public enum UnitSortModes
  {
    Time,
    Level,
    JobRank,
    HP,
    Atk,
    Def,
    Mag,
    Mnd,
    Rec,
    Spd,
    Total,
    Awake,
    Combination,
    Rarity,
  }

  public enum EScene
  {
    UNKNOWN,
    BATTLE,
    BATTLE_MULTI,
    HOME,
    HOME_MULTI,
    TITLE,
  }
}
