namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;

    [Pin(11, "FadeOut Start", 1, 11), Pin(0x7a, "To Scene[22]", 0, 0x7a), Pin(0x79, "To Scene[21]", 0, 0x79), Pin(120, "To Scene[20]", 0, 120), Pin(0x77, "To Scene[19]", 0, 0x77), Pin(0x76, "To Scene[18]", 0, 0x76), Pin(0x75, "To Scene[17]", 0, 0x75), Pin(0x74, "To Scene[16]", 0, 0x74), Pin(0x73, "To Scene[15]", 0, 0x73), Pin(0x72, "To Scene[14]", 0, 0x72), Pin(0x71, "To Scene[13]", 0, 0x71), Pin(0x70, "To Scene[12]", 0, 0x70), Pin(0x6f, "To Scene[11]", 0, 0x6f), Pin(110, "To Scene[10]", 0, 110), Pin(0x6d, "To Scene[9]", 0, 0x6d), Pin(0x6c, "To Scene[8]", 0, 0x6c), Pin(0x6b, "To Scene[7]", 0, 0x6b), Pin(0x6a, "To Scene[6]", 0, 0x6a), Pin(0x69, "To Scene[5]", 0, 0x69), Pin(0x68, "To Scene[4]", 0, 0x68), Pin(0x67, "To Scene[3]", 0, 0x67), Pin(0x66, "To Scene[2]", 0, 0x66), Pin(0x65, "To Scene[1]", 0, 0x65), Pin(100, "To Scene[0]", 0, 100), Pin(0x63, "To Home", 0, 0x63), Pin(30, "Restore", 0, 30), Pin(0x10, "Home Leave", 1, 0x10), Pin(15, "Home Enter", 1, 15), Pin(12, "FadeOut End", 0, 12), Pin(10, "FadeIn Start", 1, 10), Pin(0x7d1, "Beginner Notified", 0, 0x7d1), Pin(0x7d0, "Beginner Notify", 1, 0x7d0), Pin(0x3e9, "Rank Match Rewarded", 0, 0x3e9), Pin(0x3e8, "Req Rank Match Reward", 1, 0x3e8), Pin(0x80, "To Scene[28]", 0, 0x80), Pin(0x7f, "To Scene[27]", 0, 0x7f), Pin(0x7e, "To Scene[26]", 0, 0x7e), Pin(0x7d, "To Scene[25]", 0, 0x7d), Pin(0x7c, "To Scene[24]", 0, 0x7c), Pin(0x7b, "To Scene[23]", 0, 0x7b)]
    public class HomeWindow : MonoBehaviour, IFlowInterface
    {
        public const int PINID_FADEIN_START = 10;
        public const int PINID_FADEOUT_START = 11;
        public const int PINID_FADEOUT_END = 12;
        public const int PINID_HOME_ENTER = 15;
        public const int PINID_HOME_LEAVE = 0x10;
        public const int PINOUT_REQ_RANKMATCH_REWARD = 0x3e8;
        public const int PININ_RANKMATCH_REWARDED = 0x3e9;
        public const int PINOUT_BEGINNER_NOTIFY = 0x7d0;
        public const int PININ_BEGINNER_NOTIFIED = 0x7d1;
        public static HomeWindow mInstance;
        private StateMachine<HomeWindow> mStateMachine;
        private static RestorePoints mRestorePoint;
        public string[] SceneNames;
        public string[] IgnoreSameSceneCheck;
        public string UnloadTrigger;
        public string DayChangeTrigger;
        [StringIsResourcePath(typeof(GameObject))]
        public string NewsWindowPath;
        [StringIsResourcePath(typeof(GameObject))]
        public string LoginBonusPath;
        [StringIsResourcePath(typeof(GameObject))]
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
        public RestoreScene[] RestoreScenes;
        public static int EnterHomeCount;
        private bool mNewsShown;
        private bool mBeginnerShown;

        public HomeWindow()
        {
            this.SceneNames = new string[0];
            this.IgnoreSameSceneCheck = new string[0];
            this.UnloadTrigger = "UNLOAD_MENU";
            this.DayChangeTrigger = "DAY_CHANGE";
            this.mSyncTrophyInterval = 5f;
            this.RestoreScenes = new RestoreScene[0];
            base..ctor();
            return;
        }

        public unsafe void Activated(int pinID)
        {
            bool flag;
            bool flag2;
            SectionParam param;
            int num;
            <Activated>c__AnonStorey34E storeye;
            int num2;
            if (0x63 > pinID)
            {
                goto Label_013B;
            }
            if (pinID >= 0x81)
            {
                goto Label_013B;
            }
            if (this.mDesirdSceneSet != null)
            {
                goto Label_013A;
            }
            storeye = new <Activated>c__AnonStorey34E();
            storeye.desiredSceneName = null;
            flag = 0;
            flag2 = GlobalVars.ForceSceneChange;
            GlobalVars.ForceSceneChange = 0;
            if (pinID != 0x63)
            {
                goto Label_0063;
            }
            param = HomeUnitController.GetHomeWorld();
            if (param == null)
            {
                goto Label_0075;
            }
            storeye.desiredSceneName = param.home;
            flag = 1;
            goto Label_0075;
        Label_0063:
            storeye.desiredSceneName = this.SceneNames[pinID - 100];
        Label_0075:
            if (Array.FindIndex<string>(this.IgnoreSameSceneCheck, new Predicate<string>(storeye.<>m__34C)) == -1)
            {
                goto Label_0095;
            }
            flag2 = 1;
        Label_0095:
            if (string.IsNullOrEmpty(storeye.desiredSceneName) != null)
            {
                goto Label_011A;
            }
            if (flag2 != null)
            {
                goto Label_00C3;
            }
            if ((this.mLastSceneName != storeye.desiredSceneName) == null)
            {
                goto Label_011A;
            }
        Label_00C3:
            if (MonoSingleton<GameManager>.Instance.PrepareSceneChange() != null)
            {
                goto Label_00D3;
            }
            return;
        Label_00D3:
            this.SceneChangeSendLog(this.mDesiredSceneName, storeye.desiredSceneName);
            this.mDesirdSceneSet = 1;
            this.mDesiredSceneName = storeye.desiredSceneName;
            this.mDesiredSceneIsHome = flag;
            this.mIgnorePopups = this.mDesiredSceneIsHome == 0;
            GlobalVars.SetDropTableGeneratedTime();
            goto Label_013A;
        Label_011A:
            if ((GameObject.Find("EventQuest") != null) == null)
            {
                goto Label_013A;
            }
            GlobalEvent.Invoke("UPDATE_EVENT_LIST", this);
        Label_013A:
            return;
        Label_013B:
            num2 = pinID;
            if (num2 == 12)
            {
                goto Label_016D;
            }
            if (num2 == 30)
            {
                goto Label_0190;
            }
            if (num2 == 0x3e9)
            {
                goto Label_0179;
            }
            if (num2 == 0x7d1)
            {
                goto Label_0185;
            }
            goto Label_0218;
        Label_016D:
            this.mFadingOut = 0;
            goto Label_0218;
        Label_0179:
            this.mRankmatchRewarded = 1;
            goto Label_0218;
        Label_0185:
            BeginnerNotified = 1;
            goto Label_0218;
        Label_0190:
            if (this.RestoreScenes == null)
            {
                goto Label_0218;
            }
            GlobalVars.IsTutorialEnd = 1;
            if (mRestorePoint == null)
            {
                goto Label_020A;
            }
            if (this.IsNotHomeBGM() != null)
            {
                goto Label_01BB;
            }
            FlowNode_PlayBGM.PlayHomeBGM();
        Label_01BB:
            num = 0;
            goto Label_01FC;
        Label_01C2:
            if (&(this.RestoreScenes[num]).Type != mRestorePoint)
            {
                goto Label_01F8;
            }
            this.Activated(100 + &(this.RestoreScenes[num]).Index);
            return;
        Label_01F8:
            num += 1;
        Label_01FC:
            if (num < ((int) this.RestoreScenes.Length))
            {
                goto Label_01C2;
            }
        Label_020A:
            this.Activated(0x63);
            return;
        Label_0218:
            return;
        }

        public void ChangeNewsState()
        {
            if ((this.mStateMachine.CurrentState.Name == "State_Default") == null)
            {
                goto Label_0039;
            }
            GameUtility.setLoginInfoRead(string.Empty);
            this.mStateMachine.GotoState<State_News>();
            LoginNewsInfo.UpdateBeforePubInfo();
        Label_0039:
            return;
        }

        private void CheckTrophies()
        {
        }

        public void FgGIDLoginCheck()
        {
            if (this.mDesiredSceneIsHome != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mDesirdSceneSet != null)
            {
                goto Label_0023;
            }
            if ((CriticalSection.GetActive() & 4) == null)
            {
                goto Label_0024;
            }
        Label_0023:
            return;
        Label_0024:
            if (MonoSingleton<GameManager>.Instance.AuthStatus != 3)
            {
                goto Label_0043;
            }
            MonoSingleton<GameManager>.Instance.Player.OnFgGIDLogin();
        Label_0043:
            return;
        }

        public static RestorePoints GetRestorePoint()
        {
            return mRestorePoint;
        }

        private bool IsNotHomeBGM()
        {
            RestorePoints points;
            points = mRestorePoint;
            switch ((points - 9))
            {
                case 0:
                    goto Label_0037;

                case 1:
                    goto Label_0037;

                case 2:
                    goto Label_002B;

                case 3:
                    goto Label_002B;

                case 4:
                    goto Label_002B;

                case 5:
                    goto Label_0037;

                case 6:
                    goto Label_0037;
            }
        Label_002B:
            if (points == 1)
            {
                goto Label_0037;
            }
            goto Label_0039;
        Label_0037:
            return 1;
        Label_0039:
            return 0;
        }

        private void MiscBeforeDefaultState()
        {
            Json_LoginBonus bonus;
            string str;
            int num;
            ItemData data;
            this.NotifyNewFriendRequests();
            if (EnterHomeCount != null)
            {
                goto Label_0016;
            }
            this.CheckTrophies();
        Label_0016:
            if (this.mNewsShown != null)
            {
                goto Label_00B9;
            }
            if (GlobalVars.IsTitleStart.Get() != null)
            {
                goto Label_00B9;
            }
            if (MonoSingleton<GameManager>.Instance.Player.IsFirstLogin != null)
            {
                goto Label_004F;
            }
            if (this.DebugLoginBonus == null)
            {
                goto Label_00B9;
            }
        Label_004F:
            this.mNewsShown = 1;
            bonus = MonoSingleton<GameManager>.Instance.Player.RecentLoginBonus;
            if (bonus == null)
            {
                goto Label_00B3;
            }
            if (bonus.coin <= 0)
            {
                goto Label_008A;
            }
            str = "$COIN";
            num = bonus.coin;
            goto Label_0098;
        Label_008A:
            str = bonus.iname;
            num = bonus.num;
        Label_0098:
            data = new ItemData();
            if (data.Setup(0L, str, num) == null)
            {
                goto Label_00B3;
            }
            NotifyList.PushLoginBonus(data);
        Label_00B3:
            this.NotifySupportResult();
        Label_00B9:
            return;
        }

        private void NotifyNewFriendRequests()
        {
            object[] objArray1;
            char[] chArray1;
            PlayerData data;
            string[] strArray;
            int num;
            StringBuilder builder;
            int num2;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            builder = new StringBuilder(200);
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.FRIEND_REQUEST_CACHE) == null)
            {
                goto Label_004C;
            }
            chArray1 = new char[] { 0x2c };
            strArray = PlayerPrefsUtility.GetString(PlayerPrefsUtility.FRIEND_REQUEST_CACHE, string.Empty).Split(chArray1);
            goto Label_0053;
        Label_004C:
            strArray = new string[0];
        Label_0053:
            num2 = 0;
            goto Label_00A7;
        Label_005B:
            if (Array.IndexOf<string>(strArray, data.FollowerUID[num2]) >= 0)
            {
                goto Label_0078;
            }
            num += 1;
        Label_0078:
            if (builder.Length <= 0)
            {
                goto Label_008D;
            }
            builder.Append(0x2c);
        Label_008D:
            builder.Append(data.FollowerUID[num2]);
            num2 += 1;
        Label_00A7:
            if (num2 < data.FollowerUID.Count)
            {
                goto Label_005B;
            }
            if (num <= 0)
            {
                goto Label_00DE;
            }
            objArray1 = new object[] { (int) num };
            NotifyList.Push(LocalizedText.Get("sys.FRIENDREQS", objArray1));
        Label_00DE:
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.FRIEND_REQUEST_CACHE, builder.ToString(), 1);
            return;
        }

        private void NotifySupportResult()
        {
            PlayerData data;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data.SupportGold <= 0)
            {
                goto Label_0034;
            }
            NotifyList.PushQuestSupport(data.SupportCount, data.SupportGold);
            data.OnGoldChange(data.SupportGold);
        Label_0034:
            return;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus != null)
            {
                goto Label_000D;
            }
            this.OnApplicationPause(1);
        Label_000D:
            return;
        }

        private void OnApplicationPause(bool pausing)
        {
            PlayerData data;
            TrophyState[] stateArray;
            List<TrophyState> list;
            List<TrophyState> list2;
            int num;
            ReqUpdateTrophy trophy;
            ReqUpdateBingo bingo;
            if (pausing == null)
            {
                goto Label_0106;
            }
            if (Network.Mode != null)
            {
                goto Label_0106;
            }
            if (MonoSingleton<GameManager>.Instance.update_trophy_lock.IsLock != null)
            {
                goto Label_0025;
            }
            return;
        Label_0025:
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data.IsTrophyDirty() == null)
            {
                goto Label_0106;
            }
            stateArray = data.TrophyStates;
            list = new List<TrophyState>((int) stateArray.Length);
            list2 = new List<TrophyState>((int) stateArray.Length);
            num = 0;
            goto Label_00AA;
        Label_005C:
            if (stateArray[num].Param.IsChallengeMission == null)
            {
                goto Label_008C;
            }
            if (stateArray[num].IsDirty == null)
            {
                goto Label_00A4;
            }
            list2.Add(stateArray[num]);
            goto Label_00A4;
        Label_008C:
            if (stateArray[num].IsDirty == null)
            {
                goto Label_00A4;
            }
            list.Add(stateArray[num]);
        Label_00A4:
            num += 1;
        Label_00AA:
            if (num < ((int) stateArray.Length))
            {
                goto Label_005C;
            }
            if (list.Count <= 0)
            {
                goto Label_00DD;
            }
            trophy = new ReqUpdateTrophy(list, new Network.ResponseCallback(this.OnUpdateTrophyImmediate), 0);
            Network.RequestAPIImmediate(trophy, 1);
        Label_00DD:
            if (list2.Count <= 0)
            {
                goto Label_0106;
            }
            bingo = new ReqUpdateBingo(list2, new Network.ResponseCallback(this.OnUpdateTrophyImmediate), 0);
            Network.RequestAPIImmediate(bingo, 1);
        Label_0106:
            return;
        }

        private void OnDestroy()
        {
            if ((mInstance == this) == null)
            {
                goto Label_0016;
            }
            mInstance = null;
        Label_0016:
            return;
        }

        private void OnUpdateTrophyImmediate(WWWResult www)
        {
            Network.RemoveAPI();
            return;
        }

        private void SceneChangeSendLog(string before, string after)
        {
            string str;
            if (before != null)
            {
                goto Label_000D;
            }
            before = "Start";
        Label_000D:
            if ((after == "EventQuestList") == null)
            {
                goto Label_0045;
            }
            str = Enum.GetName(typeof(GlobalVars.EventQuestListType), (GlobalVars.EventQuestListType) GlobalVars.ReqEventPageListType);
            after = after + "-" + str;
        Label_0045:
            FlowNode_SendLogMessage.SceneChangeEvent("scene", before, after);
            return;
        }

        public static void SetRestorePoint(RestorePoints restorePoint)
        {
            mRestorePoint = restorePoint;
            return;
        }

        public void SetVisible(bool visible)
        {
            Canvas canvas;
            canvas = base.GetComponent<Canvas>();
            if ((canvas != null) == null)
            {
                goto Label_001A;
            }
            canvas.set_enabled(visible);
        Label_001A:
            return;
        }

        private void Start()
        {
            if ((mInstance == null) == null)
            {
                goto Label_0016;
            }
            mInstance = this;
        Label_0016:
            this.mStateMachine = new StateMachine<HomeWindow>(this);
            this.mStateMachine.GotoState<State_Default>();
            return;
        }

        public void UnlockContents()
        {
            PlayerData data;
            int num;
            int num2;
            int num3;
            int num4;
            UnlockParam[] paramArray;
            int num5;
            UnlockParam param;
            bool flag;
            bool flag2;
            if (this.mDesiredSceneIsHome != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.mDesirdSceneSet == null) && ((CriticalSection.GetActive() & 4) == null))
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            if ((PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV) != null) && (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV) != null))
            {
                goto Label_0043;
            }
            return;
        Label_0043:
            data = MonoSingleton<GameManager>.Instance.Player;
            num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, 0);
            num2 = data.Lv;
            num3 = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, 0);
            num4 = data.VipRank;
            if ((num < num2) || (num3 < num4))
            {
                goto Label_0085;
            }
            return;
        Label_0085:
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.Unlocks;
            num5 = 0;
            goto Label_011D;
        Label_009E:
            param = paramArray[num5];
            flag = (param.PlayerLevel == null) ? 1 : ((num >= param.PlayerLevel) ? 0 : ((param.PlayerLevel > num2) == 0));
            flag2 = (param.VipRank == null) ? 1 : ((num3 >= param.VipRank) ? 0 : ((param.VipRank > num4) == 0));
            if (flag == null)
            {
                goto Label_0117;
            }
            if (flag2 == null)
            {
                goto Label_0117;
            }
            NotifyList.PushContentsUnlock(param);
        Label_0117:
            num5 += 1;
        Label_011D:
            if (num5 < ((int) paramArray.Length))
            {
                goto Label_009E;
            }
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_PLAYER_LV, num2, 0);
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.HOME_LASTACCESS_VIP_LV, num4, 0);
            LevelLock.UpdateLockStates();
            return;
        }

        private void Update()
        {
            this.mStateMachine.Update();
            return;
        }

        public static HomeWindow Current
        {
            get
            {
                return mInstance;
            }
        }

        public bool IsReadyInTown
        {
            get
            {
                return (((this.mStateMachine == null) || (this.mStateMachine.IsInState<State_Default>() == null)) ? 0 : (this.mDesirdSceneSet == 0));
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

        [CompilerGenerated]
        private sealed class <Activated>c__AnonStorey34E
        {
            internal string desiredSceneName;

            public <Activated>c__AnonStorey34E()
            {
                base..ctor();
                return;
            }

            internal bool <>m__34C(string scene)
            {
                return scene.Equals(this.desiredSceneName);
            }
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RestoreScene
        {
            public RestorePoints Type;
            public int Index;
        }

        private class State_BeginnerNotify : State<HomeWindow>
        {
            public State_BeginnerNotify()
            {
                base..ctor();
                return;
            }

            public override void Begin(HomeWindow self)
            {
                if (HomeWindow.BeginnerNotified == null)
                {
                    goto Label_0020;
                }
                self.MiscBeforeDefaultState();
                self.mStateMachine.GotoState<HomeWindow.State_Default>();
                goto Label_002B;
            Label_0020:
                FlowNode_GameObject.ActivateOutputLinks(self, 0x7d0);
            Label_002B:
                return;
            }

            public override void Update(HomeWindow self)
            {
                if (HomeWindow.BeginnerNotified == null)
                {
                    goto Label_001B;
                }
                self.MiscBeforeDefaultState();
                self.mStateMachine.GotoState<HomeWindow.State_Default>();
            Label_001B:
                return;
            }
        }

        private class State_ChangeScene : State<HomeWindow>
        {
            private GameObject mNewScene;
            private int state;
            private SceneRequest req;
            private AsyncOperation asyncOp;

            public State_ChangeScene()
            {
                base..ctor();
                return;
            }

            public override void Begin(HomeWindow self)
            {
                CriticalSection.Enter(4);
                return;
            }

            [DebuggerHidden]
            private IEnumerator ChangeSceneAsync()
            {
                <ChangeSceneAsync>c__Iterator11C iteratorc;
                iteratorc = new <ChangeSceneAsync>c__Iterator11C();
                iteratorc.<>f__this = this;
                return iteratorc;
            }

            private void OnSceneAwake(GameObject scene)
            {
                this.mNewScene = scene;
                SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
                return;
            }

            public override void Update(HomeWindow self)
            {
                string[] strArray;
                int num;
                if (this.state != null)
                {
                    goto Label_0021;
                }
                self.mFadingOut = 1;
                FlowNode_GameObject.ActivateOutputLinks(self, 11);
                this.state = 1;
            Label_0021:
                if (this.state != 1)
                {
                    goto Label_00A7;
                }
                if (AssetDownloader.isDone != null)
                {
                    goto Label_0038;
                }
                return;
            Label_0038:
                if (AssetManager.IsAssetBundle(self.mDesiredSceneName) == null)
                {
                    goto Label_00A0;
                }
                AssetManager.PrepareAssets(self.mDesiredSceneName);
                if (self.mDesiredSceneIsHome == null)
                {
                    goto Label_0080;
                }
                strArray = FlowNode_PlayBGM.GetHomeBGM();
                num = 0;
                goto Label_0077;
            Label_006B:
                AssetManager.PrepareAssets(strArray[num]);
                num += 1;
            Label_0077:
                if (num < ((int) strArray.Length))
                {
                    goto Label_006B;
                }
            Label_0080:
                if (AssetDownloader.isDone != null)
                {
                    goto Label_00A0;
                }
                ProgressWindow.OpenGenericDownloadWindow();
                AssetDownloader.StartDownload(0, 1, 2);
                this.state = 2;
                return;
            Label_00A0:
                this.state = 3;
            Label_00A7:
                if (this.state != 2)
                {
                    goto Label_00CA;
                }
                if (AssetDownloader.isDone != null)
                {
                    goto Label_00BE;
                }
                return;
            Label_00BE:
                ProgressWindow.Close();
                this.state = 3;
            Label_00CA:
                if (this.state != 3)
                {
                    goto Label_010B;
                }
                if (self.mDesiredSceneIsHome == null)
                {
                    goto Label_00E6;
                }
                FlowNode_PlayBGM.PlayHomeBGM();
            Label_00E6:
                self.mLastSceneName = self.mDesiredSceneName;
                this.req = AssetManager.LoadSceneAsync(self.mDesiredSceneName, 1);
                this.state = 4;
            Label_010B:
                if (this.state != 4)
                {
                    goto Label_019F;
                }
                if (this.req.canBeActivated != null)
                {
                    goto Label_0128;
                }
                return;
            Label_0128:
                if (self.mFadingOut == null)
                {
                    goto Label_0134;
                }
                return;
            Label_0134:
                if (MonoSingleton<GameManager>.Instance.IsImportantJobRunning == null)
                {
                    goto Label_0144;
                }
                return;
            Label_0144:
                if (string.IsNullOrEmpty(self.UnloadTrigger) != null)
                {
                    goto Label_0160;
                }
                GlobalEvent.Invoke(self.UnloadTrigger, this);
            Label_0160:
                SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
                this.req.ActivateScene();
                if (string.IsNullOrEmpty(self.mLastSceneName) != null)
                {
                    goto Label_0198;
                }
                AssetManager.UnloadScene(self.mLastSceneName);
            Label_0198:
                this.state = 5;
            Label_019F:
                if (this.state != 5)
                {
                    goto Label_01DB;
                }
                if ((this.mNewScene == null) == null)
                {
                    goto Label_01BD;
                }
                return;
            Label_01BD:
                if (this.req.isDone != null)
                {
                    goto Label_01CE;
                }
                return;
            Label_01CE:
                CriticalSection.Leave(4);
                this.state = 6;
            Label_01DB:
                if (this.state != 6)
                {
                    goto Label_0204;
                }
                if (CriticalSection.IsActive == null)
                {
                    goto Label_01F2;
                }
                return;
            Label_01F2:
                this.asyncOp = AssetManager.UnloadUnusedAssets();
                this.state = 7;
            Label_0204:
                if (this.state != 7)
                {
                    goto Label_0323;
                }
                if (this.asyncOp == null)
                {
                    goto Label_022C;
                }
                if (this.asyncOp.get_isDone() != null)
                {
                    goto Label_022C;
                }
                return;
            Label_022C:
                FlowNode_GameObject.ActivateOutputLinks(self, 10);
                GameUtility.FadeIn(0.5f);
                self.mDesirdSceneSet = 0;
                if (self.mIgnorePopups != null)
                {
                    goto Label_02AA;
                }
                if (self.mNewsShown != null)
                {
                    goto Label_02AA;
                }
                if (GlobalVars.IsTitleStart.Get() != null)
                {
                    goto Label_02AA;
                }
                if (MonoSingleton<GameManager>.Instance.Player.IsFirstLogin != null)
                {
                    goto Label_0288;
                }
                if (GameUtility.isLoginInfoDisplay() == null)
                {
                    goto Label_02AA;
                }
            Label_0288:
                self.mNewsShown = 1;
                GlobalVars.IsTitleStart.Set(1);
                self.mStateMachine.GotoState<HomeWindow.State_LoginBonus>();
                goto Label_02F7;
            Label_02AA:
                if (self.mIgnorePopups != null)
                {
                    goto Label_02E6;
                }
                if (GlobalVars.IsTitleStart.Get() != null)
                {
                    goto Label_02E6;
                }
                if (self.mBeginnerShown != null)
                {
                    goto Label_02E6;
                }
                self.mBeginnerShown = 1;
                self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
                goto Label_02F7;
            Label_02E6:
                self.MiscBeforeDefaultState();
                self.mStateMachine.GotoState<HomeWindow.State_Default>();
            Label_02F7:
                if (self.mDesiredSceneIsHome == null)
                {
                    goto Label_031B;
                }
                self.UnlockContents();
                self.FgGIDLoginCheck();
                FlowNode_GameObject.ActivateOutputLinks(self, 15);
                goto Label_0323;
            Label_031B:
                FlowNode_GameObject.ActivateOutputLinks(self, 0x10);
            Label_0323:
                return;
            }

            [CompilerGenerated]
            private sealed class <ChangeSceneAsync>c__Iterator11C : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal SceneRequest <req>__0;
                internal AsyncOperation <asyncOp>__1;
                internal int $PC;
                internal object $current;
                internal HomeWindow.State_ChangeScene <>f__this;

                public <ChangeSceneAsync>c__Iterator11C()
                {
                    base..ctor();
                    return;
                }

                [DebuggerHidden]
                public void Dispose()
                {
                    this.$PC = -1;
                    return;
                }

                public bool MoveNext()
                {
                    uint num;
                    bool flag;
                    num = this.$PC;
                    this.$PC = -1;
                    switch (num)
                    {
                        case 0:
                            goto Label_0049;

                        case 1:
                            goto Label_0084;

                        case 2:
                            goto Label_00E8;

                        case 3:
                            goto Label_014B;

                        case 4:
                            goto Label_0173;

                        case 5:
                            goto Label_01A0;

                        case 6:
                            goto Label_021E;

                        case 7:
                            goto Label_027B;

                        case 8:
                            goto Label_02A4;

                        case 9:
                            goto Label_02BD;

                        case 10:
                            goto Label_02EB;

                        case 11:
                            goto Label_04AC;
                    }
                    goto Label_04B3;
                Label_0049:
                    this.<>f__this.self.mFadingOut = 1;
                    FlowNode_GameObject.ActivateOutputLinks(this.<>f__this.self, 11);
                    goto Label_0084;
                Label_0071:
                    this.$current = null;
                    this.$PC = 1;
                    goto Label_04B5;
                Label_0084:
                    if (AssetDownloader.isDone == null)
                    {
                        goto Label_0071;
                    }
                    if (AssetManager.IsAssetBundle(this.<>f__this.self.mDesiredSceneName) == null)
                    {
                        goto Label_00F7;
                    }
                    AssetManager.PrepareAssets(this.<>f__this.self.mDesiredSceneName);
                    if (AssetDownloader.isDone != null)
                    {
                        goto Label_00F7;
                    }
                    ProgressWindow.OpenGenericDownloadWindow();
                    AssetDownloader.StartDownload(0, 1, 2);
                Label_00D5:
                    this.$current = null;
                    this.$PC = 2;
                    goto Label_04B5;
                Label_00E8:
                    if (AssetDownloader.isDone == null)
                    {
                        goto Label_00D5;
                    }
                    ProgressWindow.Close();
                Label_00F7:
                    this.<>f__this.self.mLastSceneName = this.<>f__this.self.mDesiredSceneName;
                    this.<req>__0 = AssetManager.LoadSceneAsync(this.<>f__this.self.mDesiredSceneName, 1);
                    goto Label_014B;
                Label_0138:
                    this.$current = null;
                    this.$PC = 3;
                    goto Label_04B5;
                Label_014B:
                    if (this.<req>__0.canBeActivated == null)
                    {
                        goto Label_0138;
                    }
                    goto Label_0173;
                Label_0160:
                    this.$current = null;
                    this.$PC = 4;
                    goto Label_04B5;
                Label_0173:
                    if (this.<>f__this.self.mFadingOut != null)
                    {
                        goto Label_0160;
                    }
                    goto Label_01A0;
                Label_018D:
                    this.$current = null;
                    this.$PC = 5;
                    goto Label_04B5;
                Label_01A0:
                    if (MonoSingleton<GameManager>.Instance.IsImportantJobRunning != null)
                    {
                        goto Label_018D;
                    }
                    if (string.IsNullOrEmpty(this.<>f__this.self.UnloadTrigger) != null)
                    {
                        goto Label_01E4;
                    }
                    GlobalEvent.Invoke(this.<>f__this.self.UnloadTrigger, this.<>f__this);
                Label_01E4:
                    SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.<>f__this.OnSceneAwake));
                    this.<req>__0.ActivateScene();
                    goto Label_021E;
                Label_020B:
                    this.$current = null;
                    this.$PC = 6;
                    goto Label_04B5;
                Label_021E:
                    if ((this.<>f__this.mNewScene == null) != null)
                    {
                        goto Label_020B;
                    }
                    if (string.IsNullOrEmpty(this.<>f__this.self.mLastSceneName) != null)
                    {
                        goto Label_027B;
                    }
                    AssetManager.UnloadScene(this.<>f__this.self.mLastSceneName);
                    goto Label_027B;
                Label_0268:
                    this.$current = null;
                    this.$PC = 7;
                    goto Label_04B5;
                Label_027B:
                    if (this.<req>__0.isDone == null)
                    {
                        goto Label_0268;
                    }
                    CriticalSection.Leave(4);
                    this.$current = null;
                    this.$PC = 8;
                    goto Label_04B5;
                Label_02A4:
                    goto Label_02BD;
                Label_02A9:
                    this.$current = null;
                    this.$PC = 9;
                    goto Label_04B5;
                Label_02BD:
                    if (CriticalSection.IsActive != null)
                    {
                        goto Label_02A9;
                    }
                    this.<asyncOp>__1 = AssetManager.UnloadUnusedAssets();
                    goto Label_02EB;
                Label_02D7:
                    this.$current = null;
                    this.$PC = 10;
                    goto Label_04B5;
                Label_02EB:
                    if (this.<asyncOp>__1.get_isDone() == null)
                    {
                        goto Label_02D7;
                    }
                    FlowNode_GameObject.ActivateOutputLinks(this.<>f__this.self, 10);
                    GameUtility.FadeIn(0.5f);
                    this.<>f__this.self.mDesirdSceneSet = 0;
                    if (this.<>f__this.self.mIgnorePopups != null)
                    {
                        goto Label_03B5;
                    }
                    if (this.<>f__this.self.mNewsShown != null)
                    {
                        goto Label_03B5;
                    }
                    if (GlobalVars.IsTitleStart.Get() != null)
                    {
                        goto Label_03B5;
                    }
                    if (MonoSingleton<GameManager>.Instance.Player.IsFirstLogin != null)
                    {
                        goto Label_037F;
                    }
                    if (GameUtility.isLoginInfoDisplay() == null)
                    {
                        goto Label_03B5;
                    }
                Label_037F:
                    this.<>f__this.self.mNewsShown = 1;
                    GlobalVars.IsTitleStart.Set(1);
                    this.<>f__this.self.mStateMachine.GotoState<HomeWindow.State_LoginBonus>();
                    goto Label_043E;
                Label_03B5:
                    if (this.<>f__this.self.mIgnorePopups != null)
                    {
                        goto Label_0419;
                    }
                    if (GlobalVars.IsTitleStart.Get() != null)
                    {
                        goto Label_0419;
                    }
                    if (this.<>f__this.self.mBeginnerShown != null)
                    {
                        goto Label_0419;
                    }
                    this.<>f__this.self.mBeginnerShown = 1;
                    this.<>f__this.self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
                    goto Label_043E;
                Label_0419:
                    this.<>f__this.self.MiscBeforeDefaultState();
                    this.<>f__this.self.mStateMachine.GotoState<HomeWindow.State_Default>();
                Label_043E:
                    if (this.<>f__this.self.mDesiredSceneIsHome == null)
                    {
                        goto Label_0486;
                    }
                    HomeWindow.EnterHomeCount += 1;
                    this.<>f__this.self.UnlockContents();
                    FlowNode_GameObject.ActivateOutputLinks(this.<>f__this.self, 15);
                    goto Label_0498;
                Label_0486:
                    FlowNode_GameObject.ActivateOutputLinks(this.<>f__this.self, 0x10);
                Label_0498:
                    this.$current = null;
                    this.$PC = 11;
                    goto Label_04B5;
                Label_04AC:
                    this.$PC = -1;
                Label_04B3:
                    return 0;
                Label_04B5:
                    return 1;
                    return flag;
                }

                [DebuggerHidden]
                public void Reset()
                {
                    throw new NotSupportedException();
                }

                object IEnumerator<object>.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }

                object IEnumerator.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }
            }
        }

        private class State_Default : State<HomeWindow>
        {
            public State_Default()
            {
                base..ctor();
                return;
            }

            public override void Begin(HomeWindow self)
            {
            }

            public override void Update(HomeWindow self)
            {
                PlayerData data;
                data = MonoSingleton<GameManager>.Instance.Player;
                if (self.mDesirdSceneSet == null)
                {
                    goto Label_002C;
                }
                if (CriticalSection.IsActive != null)
                {
                    goto Label_002C;
                }
                self.mStateMachine.GotoState<HomeWindow.State_ChangeScene>();
                return;
            Label_002C:
                data.UpdateStaminaDailyMission();
                data.UpdateVipDailyMission(data.VipRank);
                data.UpdateCardDailyMission();
                if (MonoSingleton<GameManager>.Instance.update_trophy_lock.IsLock == null)
                {
                    goto Label_00B5;
                }
                if (data.IsTrophyDirty() == null)
                {
                    goto Label_00B5;
                }
                if (self.mSyncTrophyInterval <= 0f)
                {
                    goto Label_0085;
                }
                self.mSyncTrophyInterval -= Time.get_unscaledDeltaTime();
            Label_0085:
                if (self.mSyncTrophyInterval > 0f)
                {
                    goto Label_00B5;
                }
                if (CriticalSection.IsActive != null)
                {
                    goto Label_00B5;
                }
                if (Network.Mode != null)
                {
                    goto Label_00B5;
                }
                self.mStateMachine.GotoState<HomeWindow.State_UpdateTrophy>();
                return;
            Label_00B5:
                return;
            }
        }

        private class State_LoginBonus : State<HomeWindow>
        {
            private LoadRequest mReq;
            private GameObject mInstance;
            private string mLoginBonusType;

            public State_LoginBonus()
            {
                base..ctor();
                return;
            }

            public override void Begin(HomeWindow self)
            {
                PlayerData data;
                string str;
                string str2;
                data = MonoSingleton<GameManager>.Instance.Player;
                if (data.HasQueuedLoginBonus != null)
                {
                    goto Label_0017;
                }
                return;
            Label_0017:
                this.mLoginBonusType = data.DequeueNextLoginBonusTableID();
                if (string.IsNullOrEmpty(this.mLoginBonusType) == null)
                {
                    goto Label_0034;
                }
                return;
            Label_0034:
                str = data.GetLoginBonusePrefabName(this.mLoginBonusType);
                if (string.IsNullOrEmpty(str) == null)
                {
                    goto Label_006E;
                }
                if (string.IsNullOrEmpty(self.LoginBonusPath) != null)
                {
                    goto Label_006D;
                }
                this.mReq = AssetManager.LoadAsync<GameObject>(self.LoginBonusPath);
            Label_006D:
                return;
            Label_006E:
                str2 = "UI/LoginBonus/" + str;
                this.mReq = AssetManager.LoadAsync<GameObject>(str2);
                return;
            }

            public override void Update(HomeWindow self)
            {
                LoginBonusWindow window;
                LoginBonusWindow28days windowdays;
                if (this.mReq == null)
                {
                    goto Label_009C;
                }
                if (this.mReq.isDone != null)
                {
                    goto Label_001C;
                }
                return;
            Label_001C:
                if ((this.mReq.asset != null) == null)
                {
                    goto Label_0095;
                }
                this.mInstance = Object.Instantiate(this.mReq.asset) as GameObject;
                window = this.mInstance.GetComponent<LoginBonusWindow>();
                if ((window != null) == null)
                {
                    goto Label_0071;
                }
                window.TableID = this.mLoginBonusType;
            Label_0071:
                windowdays = this.mInstance.GetComponent<LoginBonusWindow28days>();
                if ((windowdays != null) == null)
                {
                    goto Label_0095;
                }
                windowdays.TableID = this.mLoginBonusType;
            Label_0095:
                this.mReq = null;
            Label_009C:
                if ((this.mInstance == null) == null)
                {
                    goto Label_011A;
                }
                if (MonoSingleton<GameManager>.Instance.Player.HasQueuedLoginBonus == null)
                {
                    goto Label_00CD;
                }
                self.mStateMachine.GotoState<HomeWindow.State_LoginBonus>();
                return;
            Label_00CD:
                if (GameUtility.isLoginInfoDisplay() == null)
                {
                    goto Label_00E7;
                }
                self.mStateMachine.GotoState<HomeWindow.State_News>();
                goto Label_011A;
            Label_00E7:
                if (self.mBeginnerShown != null)
                {
                    goto Label_0109;
                }
                self.mBeginnerShown = 1;
                self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
                goto Label_011A;
            Label_0109:
                self.MiscBeforeDefaultState();
                self.mStateMachine.GotoState<HomeWindow.State_Default>();
            Label_011A:
                return;
            }
        }

        private class State_News : State<HomeWindow>
        {
            private LoadRequest mReq;
            private GameObject mInstance;
            private bool mHasNotifiedRankmatch;

            public State_News()
            {
                base..ctor();
                return;
            }

            public override void Begin(HomeWindow self)
            {
                if ((FlowNode_Variable.Get("REDRAW_GACHA_PENDING") == "1") == null)
                {
                    goto Label_0030;
                }
                self.mStateMachine.GotoState<HomeWindow.State_Default>();
                GlobalEvent.Invoke("MENU_GACHA_REDRAW", this);
                return;
            Label_0030:
                if (string.IsNullOrEmpty(self.LoginInfoPath) != null)
                {
                    goto Label_0058;
                }
                this.mReq = AssetManager.LoadAsync<GameObject>(self.LoginInfoPath);
                this.mHasNotifiedRankmatch = 0;
            Label_0058:
                return;
            }

            public override void Update(HomeWindow self)
            {
                if (this.mReq == null)
                {
                    goto Label_0054;
                }
                if (this.mReq.isDone != null)
                {
                    goto Label_001C;
                }
                return;
            Label_001C:
                if ((this.mReq.asset != null) == null)
                {
                    goto Label_004D;
                }
                this.mInstance = Object.Instantiate(this.mReq.asset) as GameObject;
            Label_004D:
                this.mReq = null;
            Label_0054:
                if ((this.mInstance == null) == null)
                {
                    goto Label_0089;
                }
                if (this.mHasNotifiedRankmatch != null)
                {
                    goto Label_0089;
                }
                this.mHasNotifiedRankmatch = 1;
                self.mRankmatchRewarded = 0;
                FlowNode_GameObject.ActivateOutputLinks(self, 0x3e8);
            Label_0089:
                if (self.mRankmatchRewarded == null)
                {
                    goto Label_00C7;
                }
                if (self.mBeginnerShown != null)
                {
                    goto Label_00B6;
                }
                self.mBeginnerShown = 1;
                self.mStateMachine.GotoState<HomeWindow.State_BeginnerNotify>();
                goto Label_00C7;
            Label_00B6:
                self.MiscBeforeDefaultState();
                self.mStateMachine.GotoState<HomeWindow.State_Default>();
            Label_00C7:
                return;
            }
        }

        private class State_UpdateChallengeMission : State<HomeWindow>
        {
            private List<TrophyState> mDirtyList;

            public State_UpdateChallengeMission()
            {
                base..ctor();
                return;
            }

            public override void Begin(HomeWindow self)
            {
                PlayerData data;
                TrophyState[] stateArray;
                int num;
                ReqUpdateBingo bingo;
                stateArray = MonoSingleton<GameManager>.Instance.Player.TrophyStates;
                this.mDirtyList = new List<TrophyState>((int) stateArray.Length);
                num = 0;
                goto Label_0058;
            Label_0027:
                if (stateArray[num].Param.IsChallengeMission == null)
                {
                    goto Label_0054;
                }
                if (stateArray[num].IsDirty == null)
                {
                    goto Label_0054;
                }
                this.mDirtyList.Add(stateArray[num]);
            Label_0054:
                num += 1;
            Label_0058:
                if (num < ((int) stateArray.Length))
                {
                    goto Label_0027;
                }
                if (this.mDirtyList.Count <= 0)
                {
                    goto Label_0097;
                }
                bingo = new ReqUpdateBingo(this.mDirtyList, new SRPG.Network.ResponseCallback(this.ResponseCallback), 0);
                Network.RequestAPI(bingo, 0);
                goto Label_00AD;
            Label_0097:
                self.mSyncTrophyInterval = 5f;
                self.mStateMachine.GotoState<HomeWindow.State_Default>();
            Label_00AD:
                return;
            }

            private void ResponseCallback(WWWResult www)
            {
                int num;
                base.self.mSyncTrophyInterval = 5f;
                if (Network.IsError != null)
                {
                    goto Label_004D;
                }
                num = 0;
                goto Label_0037;
            Label_0021:
                this.mDirtyList[num].IsDirty = 0;
                num += 1;
            Label_0037:
                if (num < this.mDirtyList.Count)
                {
                    goto Label_0021;
                }
                goto Label_0053;
            Label_004D:
                FlowNode_Network.Retry();
                return;
            Label_0053:
                Network.RemoveAPI();
                base.self.mStateMachine.GotoState<HomeWindow.State_Default>();
                return;
            }
        }

        private class State_UpdateTrophy : State<HomeWindow>
        {
            private List<TrophyState> mDirtyList;

            public State_UpdateTrophy()
            {
                base..ctor();
                return;
            }

            public override void Begin(HomeWindow self)
            {
                PlayerData data;
                TrophyState[] stateArray;
                int num;
                ReqUpdateTrophy trophy;
                stateArray = MonoSingleton<GameManager>.Instance.Player.TrophyStates;
                this.mDirtyList = new List<TrophyState>((int) stateArray.Length);
                num = 0;
                goto Label_0058;
            Label_0027:
                if (stateArray[num].Param.IsChallengeMission != null)
                {
                    goto Label_0054;
                }
                if (stateArray[num].IsDirty == null)
                {
                    goto Label_0054;
                }
                this.mDirtyList.Add(stateArray[num]);
            Label_0054:
                num += 1;
            Label_0058:
                if (num < ((int) stateArray.Length))
                {
                    goto Label_0027;
                }
                if (this.mDirtyList.Count <= 0)
                {
                    goto Label_0097;
                }
                trophy = new ReqUpdateTrophy(this.mDirtyList, new SRPG.Network.ResponseCallback(this.ResponseCallback), 0);
                Network.RequestAPI(trophy, 0);
                goto Label_00A2;
            Label_0097:
                self.mStateMachine.GotoState<HomeWindow.State_UpdateChallengeMission>();
            Label_00A2:
                return;
            }

            private void ResponseCallback(WWWResult www)
            {
                int num;
                base.self.mSyncTrophyInterval = 5f;
                if (Network.IsError != null)
                {
                    goto Label_004D;
                }
                num = 0;
                goto Label_0037;
            Label_0021:
                this.mDirtyList[num].IsDirty = 0;
                num += 1;
            Label_0037:
                if (num < this.mDirtyList.Count)
                {
                    goto Label_0021;
                }
                goto Label_0053;
            Label_004D:
                FlowNode_Network.Retry();
                return;
            Label_0053:
                MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
                Network.RemoveAPI();
                base.self.mStateMachine.GotoState<HomeWindow.State_UpdateChallengeMission>();
                return;
            }
        }
    }
}

