// Decompiled with JetBrains decompiler
// Type: SRPG.HomeWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
  [FlowNode.Pin(116, "To Scene[16]", FlowNode.PinTypes.Input, 116)]
  [FlowNode.Pin(120, "To Scene[20]", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(119, "To Scene[19]", FlowNode.PinTypes.Input, 119)]
  [FlowNode.Pin(118, "To Scene[18]", FlowNode.PinTypes.Input, 118)]
  [FlowNode.Pin(117, "To Scene[17]", FlowNode.PinTypes.Input, 117)]
  [FlowNode.Pin(121, "To Scene[21]", FlowNode.PinTypes.Input, 121)]
  [FlowNode.Pin(115, "To Scene[15]", FlowNode.PinTypes.Input, 115)]
  [FlowNode.Pin(114, "To Scene[14]", FlowNode.PinTypes.Input, 114)]
  [FlowNode.Pin(113, "To Scene[13]", FlowNode.PinTypes.Input, 113)]
  [FlowNode.Pin(1001, "Show Tutorial", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(112, "To Scene[12]", FlowNode.PinTypes.Input, 112)]
  [FlowNode.Pin(111, "To Scene[11]", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(110, "To Scene[10]", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(10, "FadeIn Start", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(109, "To Scene[9]", FlowNode.PinTypes.Input, 109)]
  [FlowNode.Pin(11, "FadeOut Start", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(108, "To Scene[8]", FlowNode.PinTypes.Input, 108)]
  [FlowNode.Pin(107, "To Scene[7]", FlowNode.PinTypes.Input, 107)]
  [FlowNode.Pin(106, "To Scene[6]", FlowNode.PinTypes.Input, 106)]
  [FlowNode.Pin(105, "To Scene[5]", FlowNode.PinTypes.Input, 105)]
  [FlowNode.Pin(104, "To Scene[4]", FlowNode.PinTypes.Input, 104)]
  [FlowNode.Pin(103, "To Scene[3]", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(12, "FadeOut End", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(15, "Home Enter", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(102, "To Scene[2]", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(101, "To Scene[1]", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(100, "To Scene[0]", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(99, "To Home", FlowNode.PinTypes.Input, 99)]
  [FlowNode.Pin(30, "Restore", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(1002, "Next Tutorial", FlowNode.PinTypes.Input, 1002)]
  [FlowNode.Pin(16, "Home Leave", FlowNode.PinTypes.Output, 16)]
  public class HomeWindow : MonoBehaviour, IFlowInterface
  {
    public const int PINID_FADEIN_START = 10;
    public const int PINID_FADEOUT_START = 11;
    public const int PINID_FADEOUT_END = 12;
    public const int PINID_HOME_ENTER = 15;
    public const int PINID_HOME_LEAVE = 16;
    public const int PINID_HOME_SHOW_TUTORIAL = 1001;
    public const int PINID_HOME_NEXT_TUTORIAL = 1002;
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
    public HomeWindow.RestoreScene[] RestoreScenes;
    public static int EnterHomeCount;
    private bool mNewsShown;

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

    public void Activated(int pinID)
    {
      if (99 <= pinID && pinID < 122)
      {
        TrophyState[] trophyStates = MonoSingleton<GameManager>.Instance.Player.TrophyStates;
        if (trophyStates != null)
        {
          for (int index = 0; index < trophyStates.Length; ++index)
          {
            if (trophyStates[index].IsCompleted)
            {
              TrophyParam trophy = MonoSingleton<GameManager>.Instance.MasterParam.GetTrophy(trophyStates[index].iname);
              if (trophy != null)
                GameCenterManager.SendAchievementProgress(trophy.iname);
            }
          }
        }
        if (this.mDesirdSceneSet)
          return;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        HomeWindow.\u003CActivated\u003Ec__AnonStorey251 activatedCAnonStorey251 = new HomeWindow.\u003CActivated\u003Ec__AnonStorey251();
        // ISSUE: reference to a compiler-generated field
        activatedCAnonStorey251.desiredSceneName = (string) null;
        bool flag1 = false;
        bool flag2 = GlobalVars.ForceSceneChange;
        GlobalVars.ForceSceneChange = false;
        if (pinID == 99)
        {
          SectionParam homeWorld = HomeUnitController.GetHomeWorld();
          if (homeWorld != null)
          {
            // ISSUE: reference to a compiler-generated field
            activatedCAnonStorey251.desiredSceneName = homeWorld.home;
            flag1 = true;
          }
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          activatedCAnonStorey251.desiredSceneName = this.SceneNames[pinID - 100];
          // ISSUE: reference to a compiler-generated field
          if (activatedCAnonStorey251.desiredSceneName == "world001")
          {
            // ISSUE: reference to a compiler-generated field
            activatedCAnonStorey251.desiredSceneName = "world001_sg";
          }
        }
        // ISSUE: reference to a compiler-generated method
        if (Array.FindIndex<string>(this.IgnoreSameSceneCheck, new Predicate<string>(activatedCAnonStorey251.\u003C\u003Em__29C)) != -1)
          flag2 = true;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (string.IsNullOrEmpty(activatedCAnonStorey251.desiredSceneName) || !flag2 && !(this.mLastSceneName != activatedCAnonStorey251.desiredSceneName) || !MonoSingleton<GameManager>.Instance.PrepareSceneChange())
          return;
        this.mDesirdSceneSet = true;
        // ISSUE: reference to a compiler-generated field
        this.mDesiredSceneName = activatedCAnonStorey251.desiredSceneName;
        this.mDesiredSceneIsHome = flag1;
        if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) == 0L)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (!this.mDesiredSceneIsHome)
          {
            // ISSUE: reference to a compiler-generated field
            if (activatedCAnonStorey251.desiredSceneName == "Home_Gacha")
            {
              if (instance.GetNextTutorialStep() != "ShowSummonButton")
                this.mDesirdSceneSet = false;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (activatedCAnonStorey251.desiredSceneName == "Home_UnitList")
              {
                if (instance.GetNextTutorialStep() != "ShowUnitButton")
                  this.mDesirdSceneSet = false;
              }
              else
              {
                // ISSUE: reference to a compiler-generated field
                if (activatedCAnonStorey251.desiredSceneName == "world001_sg")
                {
                  if (instance.GetNextTutorialStep() != "ShowStoryButton")
                    this.mDesirdSceneSet = false;
                }
                else
                {
                  this.mDesirdSceneSet = false;
                  return;
                }
              }
            }
          }
        }
        this.mIgnorePopups = !this.mDesiredSceneIsHome;
        if (!this.mDesiredSceneIsHome)
          return;
        FlowNode_PlayBGM.PlayHomeBGM();
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
          case 1002:
            this.mStateMachine.GotoState<HomeWindow.State_Tutorial>();
            break;
        }
      }
    }

    private bool IsNotHomeBGM()
    {
      switch (HomeWindow.mRestorePoint)
      {
        case RestorePoints.QuestList:
        case RestorePoints.Tower:
        case RestorePoints.EventQuestList:
          return true;
        default:
          return false;
      }
    }

    private void Start()
    {
      if (Object.op_Equality((Object) HomeWindow.mInstance, (Object) null))
        HomeWindow.mInstance = this;
      this.mStateMachine = new StateMachine<HomeWindow>(this);
      this.mStateMachine.GotoState<HomeWindow.State_Default>();
    }

    private void OnDestroy()
    {
      if (!Object.op_Equality((Object) HomeWindow.mInstance, (Object) this))
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
      {
        int num1 = (int) Network.RequestAPIImmediate((WebAPI) new ReqUpdateTrophy(trophyprogs1, new Network.ResponseCallback(this.OnUpdateTrophyImmediate), false), true);
      }
      if (trophyprogs2.Count <= 0)
        return;
      int num2 = (int) Network.RequestAPIImmediate((WebAPI) new ReqUpdateBingo(trophyprogs2, new Network.ResponseCallback(this.OnUpdateTrophyImmediate), false), true);
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
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Behaviour) component).set_enabled(visible);
    }

    private void Update()
    {
      this.mStateMachine.Update();
    }

    public void FgGIDLoginCheck()
    {
      if (!this.mDesiredSceneIsHome || this.mDesirdSceneSet || (CriticalSection.GetActive() & CriticalSections.SceneChange) != (CriticalSections) 0)
        return;
      MonoSingleton<GameManager>.Instance.Player.OnFacebookLogin();
      if (MonoSingleton<GameManager>.Instance.AuthStatus != ReqFgGAuth.eAuthStatus.Synchronized)
        return;
      MonoSingleton<GameManager>.Instance.Player.OnFgGIDLogin();
    }

    public void UnlockContents()
    {
      if (!this.mDesiredSceneIsHome || this.mDesirdSceneSet || ((CriticalSection.GetActive() & CriticalSections.SceneChange) != (CriticalSections) 0 || !PlayerPrefs.HasKey("lastplv")) || !PlayerPrefs.HasKey("lastviplv"))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num1 = PlayerPrefs.GetInt("lastplv");
      int lv = player.Lv;
      int num2 = PlayerPrefs.GetInt("lastviplv");
      int vipRank = player.VipRank;
      if (num1 >= lv && num2 >= vipRank)
        return;
      foreach (UnlockParam unlock in MonoSingleton<GameManager>.Instance.MasterParam.Unlocks)
      {
        if ((unlock.PlayerLevel == 0 || num1 < unlock.PlayerLevel && unlock.PlayerLevel <= lv) && (unlock.VipRank == 0 || num2 < unlock.VipRank && unlock.VipRank <= vipRank))
          NotifyList.PushContentsUnlock(unlock);
      }
      PlayerPrefs.SetInt("lastplv", lv);
      PlayerPrefs.SetInt("lastviplv", vipRank);
      LevelLock.UpdateLockStates();
    }

    private void NotifySupportResult()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (player.SupportGold <= 0)
        return;
      NotifyList.PushQuestSupport(player.SupportCount, player.SupportGold);
    }

    private void CheckTrophies()
    {
      MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(true);
    }

    private void NotifyNewFriendRequests()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num = 0;
      StringBuilder stringBuilder = new StringBuilder(200);
      string[] array;
      if (PlayerPrefs.HasKey("FRIENDS"))
        array = PlayerPrefs.GetString("FRIENDS").Split(',');
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
      PlayerPrefs.SetString("FRIENDS", stringBuilder.ToString());
      PlayerPrefs.Save();
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

    private class State_Tutorial : State<HomeWindow>
    {
      public override void Begin(HomeWindow self)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        string nextTutorialStep = instance.GetNextTutorialStep();
        if (nextTutorialStep == "Home_SG" || nextTutorialStep == "ShowMissionDetailsButton" || (nextTutorialStep == "ShowStartTutorialUnitsDialog" || nextTutorialStep == "ShowQuestButton") || nextTutorialStep == "ShowStartTutorialDialog")
        {
          DebugUtility.LogWarning("Entering home tutorial");
          FlowNode_GameObject.ActivateOutputLinks((Component) self, 15);
        }
        instance.ResumeTutorialSG();
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 1001);
      }

      public override void Update(HomeWindow self)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (!self.mDesirdSceneSet || CriticalSection.IsActive)
          return;
        self.mStateMachine.GotoState<HomeWindow.State_ChangeScene>();
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
        return (IEnumerator) new HomeWindow.State_ChangeScene.\u003CChangeSceneAsync\u003Ec__IteratorB9() { \u003C\u003Ef__this = this };
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
          if (Object.op_Equality((Object) this.mNewScene, (Object) null) || !this.req.isDone)
            return;
          CriticalSection.Leave(CriticalSections.SceneChange);
          this.state = 6;
        }
        if (this.state == 6)
        {
          if (CriticalSection.IsActive)
            return;
          GC.Collect();
          this.asyncOp = AssetManager.UnloadUnusedAssets();
          this.state = (MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) != 0L ? 7 : 101;
        }
        if (this.state == 7)
        {
          if (this.asyncOp != null && !this.asyncOp.get_isDone())
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
          else
          {
            self.NotifyNewFriendRequests();
            if (HomeWindow.EnterHomeCount == 0)
              self.CheckTrophies();
            if (!self.mNewsShown && !GlobalVars.IsTitleStart.Get() && (MonoSingleton<GameManager>.Instance.Player.IsFirstLogin || self.DebugLoginBonus))
            {
              self.mNewsShown = true;
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
              self.NotifySupportResult();
            }
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
        if (this.state != 101)
          return;
        DebugUtility.LogWarning("TUTORIAL: HERE");
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 16);
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 10);
        GameUtility.FadeIn(0.5f);
        self.mDesirdSceneSet = false;
        self.mStateMachine.GotoState<HomeWindow.State_Tutorial>();
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

      public override void Begin(HomeWindow self)
      {
        if (string.IsNullOrEmpty(self.LoginInfoPath))
          return;
        this.mReq = AssetManager.LoadAsync<GameObject>(self.LoginInfoPath);
      }

      public override void Update(HomeWindow self)
      {
        if (this.mReq != null)
        {
          if (!this.mReq.isDone)
            return;
          if (Object.op_Inequality(this.mReq.asset, (Object) null))
            this.mInstance = Object.Instantiate(this.mReq.asset) as GameObject;
          this.mReq = (LoadRequest) null;
        }
        if (!Object.op_Equality((Object) this.mInstance, (Object) null))
          return;
        self.mStateMachine.GotoState<HomeWindow.State_Default>();
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
          if (Object.op_Inequality(this.mReq.asset, (Object) null))
          {
            this.mInstance = Object.Instantiate(this.mReq.asset) as GameObject;
            LoginBonusWindow component1 = (LoginBonusWindow) this.mInstance.GetComponent<LoginBonusWindow>();
            if (Object.op_Inequality((Object) component1, (Object) null))
              component1.TableID = this.mLoginBonusType;
            LoginBonusWindow28days component2 = (LoginBonusWindow28days) this.mInstance.GetComponent<LoginBonusWindow28days>();
            if (Object.op_Inequality((Object) component2, (Object) null))
              component2.TableID = this.mLoginBonusType;
          }
          this.mReq = (LoadRequest) null;
        }
        if (!Object.op_Equality((Object) this.mInstance, (Object) null))
          return;
        if (MonoSingleton<GameManager>.Instance.Player.HasQueuedLoginBonus)
        {
          self.mStateMachine.GotoState<HomeWindow.State_LoginBonus>();
        }
        else
        {
          self.NotifyNewFriendRequests();
          self.NotifySupportResult();
          self.CheckTrophies();
          if (GameUtility.isLoginInfoDisplay())
            self.mStateMachine.GotoState<HomeWindow.State_News>();
          else
            self.mStateMachine.GotoState<HomeWindow.State_Default>();
        }
      }
    }
  }
}
