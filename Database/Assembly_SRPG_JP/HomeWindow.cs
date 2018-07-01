// Decompiled with JetBrains decompiler
// Type: SRPG.HomeWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(11, "FadeOut Start", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(122, "To Scene[22]", FlowNode.PinTypes.Input, 122)]
  [FlowNode.Pin(121, "To Scene[21]", FlowNode.PinTypes.Input, 121)]
  [FlowNode.Pin(120, "To Scene[20]", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(119, "To Scene[19]", FlowNode.PinTypes.Input, 119)]
  [FlowNode.Pin(118, "To Scene[18]", FlowNode.PinTypes.Input, 118)]
  [FlowNode.Pin(117, "To Scene[17]", FlowNode.PinTypes.Input, 117)]
  [FlowNode.Pin(116, "To Scene[16]", FlowNode.PinTypes.Input, 116)]
  [FlowNode.Pin(115, "To Scene[15]", FlowNode.PinTypes.Input, 115)]
  [FlowNode.Pin(114, "To Scene[14]", FlowNode.PinTypes.Input, 114)]
  [FlowNode.Pin(113, "To Scene[13]", FlowNode.PinTypes.Input, 113)]
  [FlowNode.Pin(112, "To Scene[12]", FlowNode.PinTypes.Input, 112)]
  [FlowNode.Pin(111, "To Scene[11]", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(110, "To Scene[10]", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(109, "To Scene[9]", FlowNode.PinTypes.Input, 109)]
  [FlowNode.Pin(108, "To Scene[8]", FlowNode.PinTypes.Input, 108)]
  [FlowNode.Pin(107, "To Scene[7]", FlowNode.PinTypes.Input, 107)]
  [FlowNode.Pin(106, "To Scene[6]", FlowNode.PinTypes.Input, 106)]
  [FlowNode.Pin(105, "To Scene[5]", FlowNode.PinTypes.Input, 105)]
  [FlowNode.Pin(104, "To Scene[4]", FlowNode.PinTypes.Input, 104)]
  [FlowNode.Pin(103, "To Scene[3]", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(102, "To Scene[2]", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(101, "To Scene[1]", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(100, "To Scene[0]", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(99, "To Home", FlowNode.PinTypes.Input, 99)]
  [FlowNode.Pin(30, "Restore", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(16, "Home Leave", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(15, "Home Enter", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(12, "FadeOut End", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(10, "FadeIn Start", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2001, "Beginner Notified", FlowNode.PinTypes.Input, 2001)]
  [FlowNode.Pin(2000, "Beginner Notify", FlowNode.PinTypes.Output, 2000)]
  [FlowNode.Pin(1001, "Rank Match Rewarded", FlowNode.PinTypes.Input, 1001)]
  [FlowNode.Pin(1000, "Req Rank Match Reward", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(128, "To Scene[28]", FlowNode.PinTypes.Input, 128)]
  [FlowNode.Pin(127, "To Scene[27]", FlowNode.PinTypes.Input, 127)]
  [FlowNode.Pin(126, "To Scene[26]", FlowNode.PinTypes.Input, 126)]
  [FlowNode.Pin(125, "To Scene[25]", FlowNode.PinTypes.Input, 125)]
  [FlowNode.Pin(124, "To Scene[24]", FlowNode.PinTypes.Input, 124)]
  [FlowNode.Pin(123, "To Scene[23]", FlowNode.PinTypes.Input, 123)]
  public class HomeWindow : MonoBehaviour, IFlowInterface
  {
    public const int PINID_FADEIN_START = 10;
    public const int PINID_FADEOUT_START = 11;
    public const int PINID_FADEOUT_END = 12;
    public const int PINID_HOME_ENTER = 15;
    public const int PINID_HOME_LEAVE = 16;
    public const int PINOUT_REQ_RANKMATCH_REWARD = 1000;
    public const int PININ_RANKMATCH_REWARDED = 1001;
    public const int PINOUT_BEGINNER_NOTIFY = 2000;
    public const int PININ_BEGINNER_NOTIFIED = 2001;
    public static HomeWindow mInstance;
    private StateMachine<HomeWindow> mStateMachine;
    private static RestorePoints mRestorePoint;
    public string[] SceneNames;
    public string[] IgnoreSameSceneCheck;
    public string UnloadTrigger;
    public string DayChangeTrigger;
    [StringIsResourcePath(typeof (GameObject))]
    public string NewsWindowPath;
    [StringIsResourcePath(typeof (GameObject))]
    public string LoginBonusPath;
    [StringIsResourcePath(typeof (GameObject))]
    public string LoginInfoPath;
    public bool DebugLoginBonus;
    private bool mDesirdSceneSet;
    private bool mFadingOut;
    private string mDesiredSceneName;
    private string mLastSceneName;
    private bool mDesiredSceneIsHome;
    private bool mIgnorePopups;
    private float mSyncTrophyInterval;
    private bool mRelogin;
    private bool mReloginSuccess;
    private bool mRankmatchRewarded;
    private static bool BeginnerNotified;
    public HomeWindow.RestoreScene[] RestoreScenes;
    public static int EnterHomeCount;
    private bool mNewsShown;
    private bool mBeginnerShown;

    public HomeWindow()
    {
      base.\u002Ector();
    }

    public static HomeWindow Current
    {
      get
      {
        return HomeWindow.mInstance;
      }
    }

    public static void SetRestorePoint(RestorePoints restorePoint)
    {
      HomeWindow.mRestorePoint = restorePoint;
    }

    public static RestorePoints GetRestorePoint()
    {
      return HomeWindow.mRestorePoint;
    }

    public bool IsReadyInTown
    {
      get
      {
        if (this.mStateMachine != null && this.mStateMachine.IsInState<HomeWindow.State_Default>())
          return !this.mDesirdSceneSet;
        return false;
      }
    }

    public bool IsSceneChanging
    {
      get
      {
        return this.mDesirdSceneSet;
      }
    }

    public bool DesiredSceneIsHome
    {
      get
      {
        return this.mDesiredSceneIsHome;
      }
    }

    public void Activated(int pinID)
    {
      if (99 <= pinID && pinID < 129)
      {
        if (this.mDesirdSceneSet)
          return;
        string desiredSceneName = (string) null;
        bool flag1 = false;
        bool flag2 = GlobalVars.ForceSceneChange;
        GlobalVars.ForceSceneChange = false;
        if (pinID == 99)
        {
          SectionParam homeWorld = HomeUnitController.GetHomeWorld();
          if (homeWorld != null)
          {
            desiredSceneName = homeWorld.home;
            flag1 = true;
          }
        }
        else
          desiredSceneName = this.SceneNames[pinID - 100];
        if (Array.FindIndex<string>(this.IgnoreSameSceneCheck, (Predicate<string>) (scene => scene.Equals(desiredSceneName))) != -1)
          flag2 = true;
        if (!string.IsNullOrEmpty(desiredSceneName) && (flag2 || this.mLastSceneName != desiredSceneName))
        {
          if (!MonoSingleton<GameManager>.Instance.PrepareSceneChange())
            return;
          this.SceneChangeSendLog(this.mDesiredSceneName, desiredSceneName);
          this.mDesirdSceneSet = true;
          this.mDesiredSceneName = desiredSceneName;
          this.mDesiredSceneIsHome = flag1;
          this.mIgnorePopups = !this.mDesiredSceneIsHome;
          GlobalVars.SetDropTableGeneratedTime();
        }
        else
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) GameObject.Find("EventQuest"), (UnityEngine.Object) null))
            return;
          GlobalEvent.Invoke("UPDATE_EVENT_LIST", (object) this);
        }
      }
      else
      {
        switch (pinID)
        {
          case 12:
            this.mFadingOut = false;
            break;
          case 30:
            if (this.RestoreScenes == null)
              break;
            GlobalVars.IsTutorialEnd = true;
            if (HomeWindow.mRestorePoint != RestorePoints.Home)
            {
              if (!this.IsNotHomeBGM())
                FlowNode_PlayBGM.PlayHomeBGM();
              for (int index = 0; index < this.RestoreScenes.Length; ++index)
              {
                if (this.RestoreScenes[index].Type == HomeWindow.mRestorePoint)
                {
                  this.Activated(100 + this.RestoreScenes[index].Index);
                  return;
                }
              }
            }
            this.Activated(99);
            break;
          case 1001:
            this.mRankmatchRewarded = true;
            break;
          case 2001:
            HomeWindow.BeginnerNotified = true;
            break;
        }
      }
    }

    private bool IsNotHomeBGM()
    {
      RestorePoints mRestorePoint = HomeWindow.mRestorePoint;
      switch (mRestorePoint)
      {
        case RestorePoints.Tower:
        case RestorePoints.EventQuestList:
        case RestorePoints.MultiTower:
        case RestorePoints.Ordeal:
          return true;
        default:
          if (mRestorePoint != RestorePoints.QuestList)
            return false;
          goto case RestorePoints.Tower;
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) HomeWindow.mInstance, (UnityEngine.Object) null))
        HomeWindow.mInstance = this;
      this.mStateMachine = new StateMachine<HomeWindow>(this);
      this.mStateMachine.GotoState<HomeWindow.State_Default>();
    }

    private void OnDestroy()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) HomeWindow.mInstance, (UnityEngine.Object) this))
        return;
      HomeWindow.mInstance = (HomeWindow) null;
    }

    private void OnApplicationPause(bool pausing)
    {
      if (!pausing || Network.Mode != Network.EConnectMode.Online || !MonoSingleton<GameManager>.Instance.update_trophy_lock.IsLock)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (!player.IsTrophyDirty())
        return;
      TrophyState[] trophyStates = player.TrophyStates;
      List<TrophyState> trophyprogs1 = new List<TrophyState>(trophyStates.Length);
      List<TrophyState> trophyprogs2 = new List<TrophyState>(trophyStates.Length);
      for (int index = 0; index < trophyStates.Length; ++index)
      {
        if (trophyStates[index].Param.IsChallengeMission)
        {
          if (trophyStates[index].IsDirty)
            trophyprogs2.Add(trophyStates[index]);
        }
        else if (trophyStates[index].IsDirty)
          trophyprogs1.Add(trophyStates[index]);
      }
      if (trophyprogs1.Count > 0)
        Network.RequestAPIImmediate((WebAPI) new ReqUpdateTrophy(trophyprogs1, new Network.ResponseCallback(this.OnUpdateTrophyImmediate), false), true);
      if (trophyprogs2.Count <= 0)
        return;
      Network.RequestAPIImmediate((WebAPI) new ReqUpdateBingo(trophyprogs2, new Network.ResponseCallback(this.OnUpdateTrophyImmediate), false), true);
    }

    private void OnApplicationFocus(bool focus)
    {
      if (focus)
        return;
      this.OnApplicationPause(true);
    }

    private void OnUpdateTrophyImmediate(WWWResult www)
    {
      Network.RemoveAPI();
    }

    public void SetVisible(bool visible)
    {
      Canvas component = (Canvas) ((Component) this).GetComponent<Canvas>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      ((Behaviour) component).set_enabled(visible);
    }

    private void Update()
    {
      this.mStateMachine.Update();
    }

    public void FgGIDLoginCheck()
    {
      if (!this.mDesiredSceneIsHome || this.mDesirdSceneSet || ((CriticalSection.GetActive() & CriticalSections.SceneChange) != (CriticalSections) 0 || MonoSingleton<GameManager>.Instance.AuthStatus != ReqFgGAuth.eAuthStatus.Synchronized))
        return;
      MonoSingleton<GameManager>.Instance.Player.OnFgGIDLogin();
    }

    public void UnlockContents()
    {
      if (!this.mDesiredSceneIsHome || this.mDesirdSceneSet || ((CriticalSection.GetActive() & CriticalSections.SceneChange) != (CriticalSections) 0 || !PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV)) || !PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num1 = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, 0);
      int lv = player.Lv;
      int num2 = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, 0);
      int vipRank = player.VipRank;
      if (num1 >= lv && num2 >= vipRank)
        return;
      foreach (UnlockParam unlock in MonoSingleton<GameManager>.Instance.MasterParam.Unlocks)
      {
        if ((unlock.PlayerLevel == 0 || num1 < unlock.PlayerLevel && unlock.PlayerLevel <= lv) && (unlock.VipRank == 0 || num2 < unlock.VipRank && unlock.VipRank <= vipRank))
          NotifyList.PushContentsUnlock(unlock);
      }
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, lv, false);
      PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, vipRank, false);
      LevelLock.UpdateLockStates();
    }

    private void NotifySupportResult()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.SupportGold <= 0)
        return;
      NotifyList.PushQuestSupport(player.SupportCount, player.SupportGold);
      player.OnGoldChange(player.SupportGold);
    }

    private void CheckTrophies()
    {
    }

    private void NotifyNewFriendRequests()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num = 0;
      StringBuilder stringBuilder = new StringBuilder(200);
      string[] array;
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.FRIEND_REQUEST_CACHE))
        array = PlayerPrefsUtility.GetString(PlayerPrefsUtility.FRIEND_REQUEST_CACHE, string.Empty).Split(',');
      else
        array = new string[0];
      for (int index = 0; index < player.FollowerUID.Count; ++index)
      {
        if (Array.IndexOf<string>(array, player.FollowerUID[index]) < 0)
          ++num;
        if (stringBuilder.Length > 0)
          stringBuilder.Append(',');
        stringBuilder.Append(player.FollowerUID[index]);
      }
      if (num > 0)
        NotifyList.Push(LocalizedText.Get("sys.FRIENDREQS", new object[1]
        {
          (object) num
        }));
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.FRIEND_REQUEST_CACHE, stringBuilder.ToString(), true);
    }

    public void ChangeNewsState()
    {
      if (!(this.mStateMachine.CurrentState.Name == "State_Default"))
        return;
      GameUtility.setLoginInfoRead(string.Empty);
      this.mStateMachine.GotoState<HomeWindow.State_News>();
      LoginNewsInfo.UpdateBeforePubInfo();
    }

    private void MiscBeforeDefaultState()
    {
      this.NotifyNewFriendRequests();
      if (HomeWindow.EnterHomeCount == 0)
        this.CheckTrophies();
      if (this.mNewsShown || GlobalVars.IsTitleStart.Get() || !MonoSingleton<GameManager>.Instance.Player.IsFirstLogin && !this.DebugLoginBonus)
        return;
      this.mNewsShown = true;
      Json_LoginBonus recentLoginBonus = MonoSingleton<GameManager>.Instance.Player.RecentLoginBonus;
      if (recentLoginBonus != null)
      {
        string iname;
        int num;
        if (recentLoginBonus.coin > 0)
        {
          iname = "$COIN";
          num = recentLoginBonus.coin;
        }
        else
        {
          iname = recentLoginBonus.iname;
          num = recentLoginBonus.num;
        }
        ItemData data = new ItemData();
        if (data.Setup(0L, iname, num))
          NotifyList.PushLoginBonus(data);
      }
      this.NotifySupportResult();
    }

    private void SceneChangeSendLog(string before, string after)
    {
      if (before == null)
        before = "Start";
      if (after == "EventQuestList")
      {
        string name = Enum.GetName(typeof (GlobalVars.EventQuestListType), (object) GlobalVars.ReqEventPageListType);
        after = after + "-" + name;
      }
      FlowNode_SendLogMessage.SceneChangeEvent("scene", before, after);
    }

    [Serializable]
    public struct RestoreScene
    {
      public RestorePoints Type;
      public int Index;
    }

    private class State_Default : State<HomeWindow>
    {
      public override void Begin(HomeWindow self)
      {
      }

      public override void Update(HomeWindow self)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (self.mDesirdSceneSet && !CriticalSection.IsActive)
        {
          self.mStateMachine.GotoState<HomeWindow.State_ChangeScene>();
        }
        else
        {
          player.UpdateStaminaDailyMission();
          player.UpdateVipDailyMission(player.VipRank);
          player.UpdateCardDailyMission();
          if (!MonoSingleton<GameManager>.Instance.update_trophy_lock.IsLock || !player.IsTrophyDirty())
            return;
          if ((double) self.mSyncTrophyInterval > 0.0)
            self.mSyncTrophyInterval -= Time.get_unscaledDeltaTime();
          if ((double) self.mSyncTrophyInterval > 0.0 || CriticalSection.IsActive || Network.Mode != Network.EConnectMode.Online)
            return;
          self.mStateMachine.GotoState<HomeWindow.State_UpdateTrophy>();
        }
      }
    }

    private class State_ChangeScene : State<HomeWindow>
    {
      private GameObject mNewScene;
      private int state;
      private SceneRequest req;
      private AsyncOperation asyncOp;

      public override void Begin(HomeWindow self)
      {
        CriticalSection.Enter(CriticalSections.SceneChange);
      }

      [DebuggerHidden]
      private IEnumerator ChangeSceneAsync()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new HomeWindow.State_ChangeScene.\u003CChangeSceneAsync\u003Ec__Iterator11C()
        {
          \u003C\u003Ef__this = this
        };
      }

      private void OnSceneAwake(GameObject scene)
      {
        this.mNewScene = scene;
        SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
      }

      public override void Update(HomeWindow self)
      {
        if (this.state == 0)
        {
          self.mFadingOut = true;
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 11);
          this.state = 1;
        }
        if (this.state == 1)
        {
          if (!AssetDownloader.isDone)
            return;
          if (AssetManager.IsAssetBundle(self.mDesiredSceneName))
          {
            AssetManager.PrepareAssets(self.mDesiredSceneName);
            if (self.mDesiredSceneIsHome)
            {
              foreach (string resourcePath in FlowNode_PlayBGM.GetHomeBGM())
                AssetManager.PrepareAssets(resourcePath);
            }
            if (!AssetDownloader.isDone)
            {
              ProgressWindow.OpenGenericDownloadWindow();
              AssetDownloader.StartDownload(false, true, ThreadPriority.Normal);
              this.state = 2;
              return;
            }
          }
          this.state = 3;
        }
        if (this.state == 2)
        {
          if (!AssetDownloader.isDone)
            return;
          ProgressWindow.Close();
          this.state = 3;
        }
        if (this.state == 3)
        {
          if (self.mDesiredSceneIsHome)
            FlowNode_PlayBGM.PlayHomeBGM();
          self.mLastSceneName = self.mDesiredSceneName;
          this.req = AssetManager.LoadSceneAsync(self.mDesiredSceneName, true);
          this.state = 4;
        }
        if (this.state == 4)
        {
          if (!this.req.canBeActivated || self.mFadingOut || MonoSingleton<GameManager>.Instance.IsImportantJobRunning)
            return;
          if (!string.IsNullOrEmpty(self.UnloadTrigger))
            GlobalEvent.Invoke(self.UnloadTrigger, (object) this);
          SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
          this.req.ActivateScene();
          if (!string.IsNullOrEmpty(self.mLastSceneName))
            AssetManager.UnloadScene(self.mLastSceneName);
          this.state = 5;
        }
        if (this.state == 5)
        {
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mNewScene, (UnityEngine.Object) null) || !this.req.isDone)
            return;
          CriticalSection.Leave(CriticalSections.SceneChange);
          this.state = 6;
        }
        if (this.state == 6)
        {
          if (CriticalSection.IsActive)
            return;
          this.asyncOp = AssetManager.UnloadUnusedAssets();
          this.state = 7;
        }
        if (this.state != 7 || this.asyncOp != null && !this.asyncOp.get_isDone())
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 10);
        GameUtility.FadeIn(0.5f);
        self.mDesirdSceneSet = false;
        if (!self.mIgnorePopups && !self.mNewsShown && !GlobalVars.IsTitleStart.Get() && (MonoSingleton<GameManager>.Instance.Player.IsFirstLogin || GameUtility.isLoginInfoDisplay()))
        {
          self.mNewsShown = true;
          GlobalVars.IsTitleStart.Set(true);
          self.mStateMachine.GotoState<HomeWindow.State_LoginBonus>();
        }
        else if (!self.mIgnorePopups && !GlobalVars.IsTitleStart.Get() && !self.mBeginnerShown)
        {
          self.mBeginnerShown = true;
          self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
        }
        else
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
        if (self.mDesiredSceneIsHome)
        {
          self.UnlockContents();
          self.FgGIDLoginCheck();
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 15);
        }
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 16);
      }
    }

    private class State_UpdateTrophy : State<HomeWindow>
    {
      private List<TrophyState> mDirtyList;

      public override void Begin(HomeWindow self)
      {
        TrophyState[] trophyStates = MonoSingleton<GameManager>.Instance.Player.TrophyStates;
        this.mDirtyList = new List<TrophyState>(trophyStates.Length);
        for (int index = 0; index < trophyStates.Length; ++index)
        {
          if (!trophyStates[index].Param.IsChallengeMission && trophyStates[index].IsDirty)
            this.mDirtyList.Add(trophyStates[index]);
        }
        if (this.mDirtyList.Count > 0)
          Network.RequestAPI((WebAPI) new ReqUpdateTrophy(this.mDirtyList, new Network.ResponseCallback(this.ResponseCallback), false), false);
        else
          self.mStateMachine.GotoState<HomeWindow.State_UpdateChallengeMission>();
      }

      private void ResponseCallback(WWWResult www)
      {
        this.self.mSyncTrophyInterval = 5f;
        if (!Network.IsError)
        {
          for (int index = 0; index < this.mDirtyList.Count; ++index)
            this.mDirtyList[index].IsDirty = false;
          MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
          Network.RemoveAPI();
          this.self.mStateMachine.GotoState<HomeWindow.State_UpdateChallengeMission>();
        }
        else
          FlowNode_Network.Retry();
      }
    }

    private class State_UpdateChallengeMission : State<HomeWindow>
    {
      private List<TrophyState> mDirtyList;

      public override void Begin(HomeWindow self)
      {
        TrophyState[] trophyStates = MonoSingleton<GameManager>.Instance.Player.TrophyStates;
        this.mDirtyList = new List<TrophyState>(trophyStates.Length);
        for (int index = 0; index < trophyStates.Length; ++index)
        {
          if (trophyStates[index].Param.IsChallengeMission && trophyStates[index].IsDirty)
            this.mDirtyList.Add(trophyStates[index]);
        }
        if (this.mDirtyList.Count > 0)
        {
          Network.RequestAPI((WebAPI) new ReqUpdateBingo(this.mDirtyList, new Network.ResponseCallback(this.ResponseCallback), false), false);
        }
        else
        {
          self.mSyncTrophyInterval = 5f;
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
      }

      private void ResponseCallback(WWWResult www)
      {
        this.self.mSyncTrophyInterval = 5f;
        if (!Network.IsError)
        {
          for (int index = 0; index < this.mDirtyList.Count; ++index)
            this.mDirtyList[index].IsDirty = false;
          Network.RemoveAPI();
          this.self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
        else
          FlowNode_Network.Retry();
      }
    }

    private class State_News : State<HomeWindow>
    {
      private LoadRequest mReq;
      private GameObject mInstance;
      private bool mHasNotifiedRankmatch;

      public override void Begin(HomeWindow self)
      {
        if (FlowNode_Variable.Get("REDRAW_GACHA_PENDING") == "1")
        {
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
          GlobalEvent.Invoke("MENU_GACHA_REDRAW", (object) this);
        }
        else
        {
          if (string.IsNullOrEmpty(self.LoginInfoPath))
            return;
          this.mReq = AssetManager.LoadAsync<GameObject>(self.LoginInfoPath);
          this.mHasNotifiedRankmatch = false;
        }
      }

      public override void Update(HomeWindow self)
      {
        if (this.mReq != null)
        {
          if (!this.mReq.isDone)
            return;
          if (UnityEngine.Object.op_Inequality(this.mReq.asset, (UnityEngine.Object) null))
            this.mInstance = UnityEngine.Object.Instantiate(this.mReq.asset) as GameObject;
          this.mReq = (LoadRequest) null;
        }
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mInstance, (UnityEngine.Object) null) && !this.mHasNotifiedRankmatch)
        {
          this.mHasNotifiedRankmatch = true;
          self.mRankmatchRewarded = false;
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 1000);
        }
        if (!self.mRankmatchRewarded)
          return;
        if (!self.mBeginnerShown)
        {
          self.mBeginnerShown = true;
          self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
        }
        else
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
      }
    }

    private class State_LoginBonus : State<HomeWindow>
    {
      private LoadRequest mReq;
      private GameObject mInstance;
      private string mLoginBonusType;

      public override void Begin(HomeWindow self)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (!player.HasQueuedLoginBonus)
          return;
        this.mLoginBonusType = player.DequeueNextLoginBonusTableID();
        if (string.IsNullOrEmpty(this.mLoginBonusType))
          return;
        string bonusePrefabName = player.GetLoginBonusePrefabName(this.mLoginBonusType);
        if (string.IsNullOrEmpty(bonusePrefabName))
        {
          if (string.IsNullOrEmpty(self.LoginBonusPath))
            return;
          this.mReq = AssetManager.LoadAsync<GameObject>(self.LoginBonusPath);
        }
        else
          this.mReq = AssetManager.LoadAsync<GameObject>("UI/LoginBonus/" + bonusePrefabName);
      }

      public override void Update(HomeWindow self)
      {
        if (this.mReq != null)
        {
          if (!this.mReq.isDone)
            return;
          if (UnityEngine.Object.op_Inequality(this.mReq.asset, (UnityEngine.Object) null))
          {
            this.mInstance = UnityEngine.Object.Instantiate(this.mReq.asset) as GameObject;
            LoginBonusWindow component1 = (LoginBonusWindow) this.mInstance.GetComponent<LoginBonusWindow>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
              component1.TableID = this.mLoginBonusType;
            LoginBonusWindow28days component2 = (LoginBonusWindow28days) this.mInstance.GetComponent<LoginBonusWindow28days>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
              component2.TableID = this.mLoginBonusType;
          }
          this.mReq = (LoadRequest) null;
        }
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mInstance, (UnityEngine.Object) null))
          return;
        if (MonoSingleton<GameManager>.Instance.Player.HasQueuedLoginBonus)
          self.mStateMachine.GotoState<HomeWindow.State_LoginBonus>();
        else if (GameUtility.isLoginInfoDisplay())
          self.mStateMachine.GotoState<HomeWindow.State_News>();
        else if (!self.mBeginnerShown)
        {
          self.mBeginnerShown = true;
          self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
        }
        else
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
      }
    }

    private class State_BeginnerNotify : State<HomeWindow>
    {
      public override void Begin(HomeWindow self)
      {
        if (HomeWindow.BeginnerNotified)
        {
          self.MiscBeforeDefaultState();
          self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 2000);
      }

      public override void Update(HomeWindow self)
      {
        if (!HomeWindow.BeginnerNotified)
          return;
        self.MiscBeforeDefaultState();
        self.mStateMachine.GotoState<HomeWindow.State_Default>();
      }
    }
  }
}
