// Decompiled with JetBrains decompiler
// Type: Com.Google.Android.Gms.Games.Games
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using Com.Google.Android.Gms.Games.Stats;
using Google.Developers;
using System;

namespace Com.Google.Android.Gms.Games
{
  public class Games : JavaObjWrapper
  {
    private const string CLASS_NAME = "com/google/android/gms/games/Games";

    public Games(IntPtr ptr)
      : base(ptr)
    {
    }

    public static string EXTRA_PLAYER_IDS
    {
      get
      {
        return JavaObjWrapper.GetStaticStringField("com/google/android/gms/games/Games", nameof (EXTRA_PLAYER_IDS));
      }
    }

    public static string EXTRA_STATUS
    {
      get
      {
        return JavaObjWrapper.GetStaticStringField("com/google/android/gms/games/Games", nameof (EXTRA_STATUS));
      }
    }

    public static object SCOPE_GAMES
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (SCOPE_GAMES), "Lcom/google/android/gms/common/api/Scope;");
      }
    }

    public static object API
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (API), "Lcom/google/android/gms/common/api/Api;");
      }
    }

    public static object GamesMetadata
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (GamesMetadata), "Lcom/google/android/gms/games/GamesMetadata;");
      }
    }

    public static object Achievements
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (Achievements), "Lcom/google/android/gms/games/achievement/Achievements;");
      }
    }

    public static object Events
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (Events), "Lcom/google/android/gms/games/event/Events;");
      }
    }

    public static object Leaderboards
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (Leaderboards), "Lcom/google/android/gms/games/leaderboard/Leaderboards;");
      }
    }

    public static object Invitations
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (Invitations), "Lcom/google/android/gms/games/multiplayer/Invitations;");
      }
    }

    public static object TurnBasedMultiplayer
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (TurnBasedMultiplayer), "Lcom/google/android/gms/games/multiplayer/turnbased/TurnBasedMultiplayer;");
      }
    }

    public static object RealTimeMultiplayer
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (RealTimeMultiplayer), "Lcom/google/android/gms/games/multiplayer/realtime/RealTimeMultiplayer;");
      }
    }

    public static object Players
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (Players), "Lcom/google/android/gms/games/Players;");
      }
    }

    public static object Notifications
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (Notifications), "Lcom/google/android/gms/games/Notifications;");
      }
    }

    public static object Quests
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (Quests), "Lcom/google/android/gms/games/quest/Quests;");
      }
    }

    public static object Requests
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (Requests), "Lcom/google/android/gms/games/request/Requests;");
      }
    }

    public static object Snapshots
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<object>("com/google/android/gms/games/Games", nameof (Snapshots), "Lcom/google/android/gms/games/snapshot/Snapshots;");
      }
    }

    public static StatsObject Stats
    {
      get
      {
        return JavaObjWrapper.GetStaticObjectField<StatsObject>("com/google/android/gms/games/Games", nameof (Stats), "Lcom/google/android/gms/games/stats/Stats;");
      }
    }

    public static string getAppId(GoogleApiClient arg_GoogleApiClient_1)
    {
      return JavaObjWrapper.StaticInvokeCall<string>("com/google/android/gms/games/Games", nameof (getAppId), "(Lcom/google/android/gms/common/api/GoogleApiClient;)Ljava/lang/String;", (object) arg_GoogleApiClient_1);
    }

    public static string getCurrentAccountName(GoogleApiClient arg_GoogleApiClient_1)
    {
      return JavaObjWrapper.StaticInvokeCall<string>("com/google/android/gms/games/Games", nameof (getCurrentAccountName), "(Lcom/google/android/gms/common/api/GoogleApiClient;)Ljava/lang/String;", (object) arg_GoogleApiClient_1);
    }

    public static int getSdkVariant(GoogleApiClient arg_GoogleApiClient_1)
    {
      return JavaObjWrapper.StaticInvokeCall<int>("com/google/android/gms/games/Games", nameof (getSdkVariant), "(Lcom/google/android/gms/common/api/GoogleApiClient;)I", (object) arg_GoogleApiClient_1);
    }

    public static object getSettingsIntent(GoogleApiClient arg_GoogleApiClient_1)
    {
      return JavaObjWrapper.StaticInvokeCall<object>("com/google/android/gms/games/Games", nameof (getSettingsIntent), "(Lcom/google/android/gms/common/api/GoogleApiClient;)Landroid/content/Intent;", (object) arg_GoogleApiClient_1);
    }

    public static void setGravityForPopups(GoogleApiClient arg_GoogleApiClient_1, int arg_int_2)
    {
      JavaObjWrapper.StaticInvokeCallVoid("com/google/android/gms/games/Games", nameof (setGravityForPopups), "(Lcom/google/android/gms/common/api/GoogleApiClient;I)V", (object) arg_GoogleApiClient_1, (object) arg_int_2);
    }

    public static void setViewForPopups(GoogleApiClient arg_GoogleApiClient_1, object arg_object_2)
    {
      JavaObjWrapper.StaticInvokeCallVoid("com/google/android/gms/games/Games", nameof (setViewForPopups), "(Lcom/google/android/gms/common/api/GoogleApiClient;Landroid/view/View;)V", (object) arg_GoogleApiClient_1, arg_object_2);
    }

    public static PendingResult<Status> signOut(GoogleApiClient arg_GoogleApiClient_1)
    {
      return JavaObjWrapper.StaticInvokeCall<PendingResult<Status>>("com/google/android/gms/games/Games", nameof (signOut), "(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/PendingResult;", (object) arg_GoogleApiClient_1);
    }
  }
}
