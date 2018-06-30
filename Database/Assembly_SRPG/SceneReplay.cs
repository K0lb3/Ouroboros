namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using UnityEngine;

    [Pin(0, "Skip", 0, 0), Pin(1, "Skip生成完了", 0, 0)]
    public class SceneReplay : Scene, IFlowInterface
    {
        private const string CREATE_SKIP_CANVAS = "CREATE_SKIP_CANVAS";
        private const string DESTROY_SKIP_CANVAS = "DESTROY_SKIP_CANVAS";
        private const string ENABLE_SKIP_BUTTON = "ENABLE_SKIP";
        private const string DISABLE_SKIP_BUTTON = "DISABLE_SKIP";
        private const string SCENE_EXIT = "EXIT";
        private const int PIN_ID_SKIP = 0;
        private const int PIN_ID_SKIP_CANVAS_CREATED = 1;
        public static readonly string QUEST_TEXTTABLE;
        private StateMachine<SceneReplay> mState;
        private bool mStartCalled;
        private QuestParam mCurrentQuest;
        private BattleCore mBattle;
        private IEnumerator<string> mEventNames;
        private bool skip;
        private bool canvasCreating;
        private bool mSceneExiting;

        static SceneReplay()
        {
            QUEST_TEXTTABLE = "quest";
            return;
        }

        public SceneReplay()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0012;
            }
            this.skip = 1;
            goto Label_0020;
        Label_0012:
            if (pinID != 1)
            {
                goto Label_0020;
            }
            this.canvasCreating = 0;
        Label_0020:
            return;
        }

        private void CreateSkipCanvas()
        {
            this.canvasCreating = 1;
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "CREATE_SKIP_CANVAS");
            return;
        }

        private void DestroySkipCanvas()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "DESTROY_SKIP_CANVAS");
            return;
        }

        private void DisableSkipButton()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "DISABLE_SKIP");
            return;
        }

        [DebuggerHidden]
        private IEnumerator DownloadQuestAsync(QuestParam quest)
        {
            <DownloadQuestAsync>c__Iterator82 iterator;
            iterator = new <DownloadQuestAsync>c__Iterator82();
            iterator.quest = quest;
            iterator.<$>quest = quest;
            iterator.<>f__this = this;
            return iterator;
        }

        private void EnableSkipButton()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "ENABLE_SKIP");
            return;
        }

        private void ExitScene()
        {
            if (this.mSceneExiting == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mSceneExiting = 1;
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "EXIT");
            return;
        }

        public void GotoState<StateType>() where StateType: State<SceneReplay>, new()
        {
            this.mState.GotoState<StateType>();
            return;
        }

        public bool IsInState<StateType>() where StateType: State<SceneReplay>
        {
            return this.mState.IsInState<StateType>();
        }

        private void OnDestroy()
        {
            LocalizedText.UnloadTable(QUEST_TEXTTABLE);
            if (this.mBattle == null)
            {
                goto Label_0027;
            }
            this.mBattle.Release();
            this.mBattle = null;
        Label_0027:
            return;
        }

        public void RemoveLog()
        {
            DebugUtility.Log("RemoveLog(): " + this.mBattle.Logs.Peek.GetType());
            this.mBattle.Logs.RemoveLog();
            return;
        }

        private void Start()
        {
            LocalizedText.LoadTable(QUEST_TEXTTABLE, 0);
            FadeController.Instance.ResetSceneFade(0f);
            this.mState = new StateMachine<SceneReplay>(this);
            this.mStartCalled = 1;
            this.mBattle = new BattleCore();
            return;
        }

        public void StartQuest(string questID)
        {
            base.StartCoroutine(this.StartQuestAsync(questID));
            return;
        }

        [DebuggerHidden]
        private IEnumerator StartQuestAsync(string questID)
        {
            <StartQuestAsync>c__Iterator81 iterator;
            iterator = new <StartQuestAsync>c__Iterator81();
            iterator.questID = questID;
            iterator.<$>questID = questID;
            iterator.<>f__this = this;
            return iterator;
        }

        private void Update()
        {
            if (this.mState == null)
            {
                goto Label_0016;
            }
            this.mState.Update();
        Label_0016:
            return;
        }

        [CompilerGenerated]
        private sealed class <DownloadQuestAsync>c__Iterator82 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal QuestParam quest;
            internal bool <downloadCurrentQuest>__0;
            internal ThreadPriority <priority>__1;
            internal int $PC;
            internal object $current;
            internal QuestParam <$>quest;
            internal SceneReplay <>f__this;

            public <DownloadQuestAsync>c__Iterator82()
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
                        goto Label_002D;

                    case 1:
                        goto Label_0080;

                    case 2:
                        goto Label_00C4;

                    case 3:
                        goto Label_0108;

                    case 4:
                        goto Label_0125;
                }
                goto Label_012C;
            Label_002D:
                this.<downloadCurrentQuest>__0 = this.quest == this.<>f__this.mCurrentQuest;
                if (this.<downloadCurrentQuest>__0 == null)
                {
                    goto Label_005D;
                }
                this.<priority>__1 = 2;
                goto Label_0064;
            Label_005D:
                this.<priority>__1 = 0;
            Label_0064:
                goto Label_0080;
            Label_0069:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_012E;
            Label_0080:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0069;
                }
                DownloadUtility.DownloadQuestBase(this.quest);
                AssetDownloader.StartDownload(0, this.<downloadCurrentQuest>__0, this.<priority>__1);
                goto Label_00C4;
            Label_00AD:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_012E;
            Label_00C4:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_00AD;
                }
                DownloadUtility.DownloadQuestMaps(this.quest);
                AssetDownloader.StartDownload(0, this.<downloadCurrentQuest>__0, this.<priority>__1);
                goto Label_0108;
            Label_00F1:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_012E;
            Label_0108:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_00F1;
                }
                this.$current = null;
                this.$PC = 4;
                goto Label_012E;
            Label_0125:
                this.$PC = -1;
            Label_012C:
                return 0;
            Label_012E:
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

        [CompilerGenerated]
        private sealed class <StartQuestAsync>c__Iterator81 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string questID;
            internal List<string> <eventNames>__0;
            internal int $PC;
            internal object $current;
            internal string <$>questID;
            internal SceneReplay <>f__this;

            public <StartQuestAsync>c__Iterator81()
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
                        goto Label_0025;

                    case 1:
                        goto Label_0041;

                    case 2:
                        goto Label_00E3;
                }
                goto Label_019B;
            Label_0025:
                goto Label_0041;
            Label_002A:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_019D;
            Label_0041:
                if (this.<>f__this.mStartCalled == null)
                {
                    goto Label_002A;
                }
                this.<>f__this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(this.questID);
                ProgressWindow.SetTexts(this.<>f__this.mCurrentQuest.name, LocalizedText.Get(this.<>f__this.mCurrentQuest.storyTextID));
                if (GameUtility.Config_UseAssetBundles.Value == null)
                {
                    goto Label_00E3;
                }
                if (AssetManager.AssetRevision <= 0)
                {
                    goto Label_00E3;
                }
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.DownloadQuestAsync(this.<>f__this.mCurrentQuest));
                this.$PC = 2;
                goto Label_019D;
            Label_00E3:
                this.<eventNames>__0 = new List<string>();
                if (string.IsNullOrEmpty(this.<>f__this.mCurrentQuest.event_start) != null)
                {
                    goto Label_0123;
                }
                this.<eventNames>__0.Add(this.<>f__this.mCurrentQuest.event_start);
            Label_0123:
                if (string.IsNullOrEmpty(this.<>f__this.mCurrentQuest.event_clear) != null)
                {
                    goto Label_016E;
                }
                if (this.<>f__this.mCurrentQuest.state != 2)
                {
                    goto Label_016E;
                }
                this.<eventNames>__0.Add(this.<>f__this.mCurrentQuest.event_clear);
            Label_016E:
                this.<>f__this.mEventNames = (List<string>.Enumerator) this.<eventNames>__0.GetEnumerator();
                this.<>f__this.GotoState<SceneReplay.State_PreReplay>();
                this.$PC = -1;
            Label_019B:
                return 0;
            Label_019D:
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

        private class State_Exit : State<SceneReplay>
        {
            public State_Exit()
            {
                base..ctor();
                return;
            }

            public override void Begin(SceneReplay self)
            {
                self.ExitScene();
                return;
            }
        }

        private class State_PostReplay : State<SceneReplay>
        {
            private AsyncOperation mAsyncOp;

            public State_PostReplay()
            {
                base..ctor();
                return;
            }

            public override void Begin(SceneReplay self)
            {
                this.mAsyncOp = AssetManager.UnloadUnusedAssets();
                return;
            }

            public override void Update(SceneReplay self)
            {
                if (this.mAsyncOp.get_isDone() != null)
                {
                    goto Label_0011;
                }
                return;
            Label_0011:
                if (self.mEventNames.MoveNext() == null)
                {
                    goto Label_002C;
                }
                self.GotoState<SceneReplay.State_Replay>();
                goto Label_0032;
            Label_002C:
                self.GotoState<SceneReplay.State_Exit>();
            Label_0032:
                return;
            }
        }

        private class State_PreReplay : State<SceneReplay>
        {
            public State_PreReplay()
            {
                base..ctor();
                return;
            }

            public override void Begin(SceneReplay self)
            {
            }

            public override void Update(SceneReplay self)
            {
                if (self.mEventNames.MoveNext() == null)
                {
                    goto Label_001B;
                }
                self.GotoState<SceneReplay.State_Replay>();
                goto Label_0021;
            Label_001B:
                self.GotoState<SceneReplay.State_Exit>();
            Label_0021:
                return;
            }
        }

        private class State_Replay : State<SceneReplay>
        {
            private string eventName;
            private string path;
            private bool is3DEvent;

            public State_Replay()
            {
                base..ctor();
                return;
            }

            public override void Begin(SceneReplay self)
            {
                this.eventName = self.mEventNames.Current;
                this.path = "Events/" + this.eventName;
                if (string.IsNullOrEmpty(this.eventName) != null)
                {
                    goto Label_0053;
                }
                if (this.eventName.EndsWith("_3d") == null)
                {
                    goto Label_0053;
                }
                this.is3DEvent = 1;
            Label_0053:
                self.StartCoroutine(this.Exec());
                return;
            }

            [DebuggerHidden]
            private IEnumerator Exec()
            {
                <Exec>c__Iterator83 iterator;
                iterator = new <Exec>c__Iterator83();
                iterator.<>f__this = this;
                return iterator;
            }

            [CompilerGenerated]
            private sealed class <Exec>c__Iterator83 : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal LoadRequest <request>__0;
                internal bool <skipping>__1;
                internal EventScript <script>__2;
                internal EventScript.Sequence <seq>__3;
                internal bool <skipEnable>__4;
                internal bool <prevSkipEnable>__5;
                internal bool <skipInitialized>__6;
                internal int $PC;
                internal object $current;
                internal SceneReplay.State_Replay <>f__this;

                public <Exec>c__Iterator83()
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
                            goto Label_0031;

                        case 1:
                            goto Label_005E;

                        case 2:
                            goto Label_0096;

                        case 3:
                            goto Label_00C6;

                        case 4:
                            goto Label_016D;

                        case 5:
                            goto Label_0233;
                    }
                    goto Label_0331;
                Label_0031:
                    this.<request>__0 = GameUtility.LoadResourceAsyncChecked<EventScript>(this.<>f__this.path);
                Label_0047:
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 1;
                    goto Label_0333;
                Label_005E:
                    ProgressWindow.SetLoadProgress(this.<request>__0.progress);
                    if (this.<request>__0.isDone == null)
                    {
                        goto Label_0047;
                    }
                    goto Label_0096;
                Label_0083:
                    this.$current = null;
                    this.$PC = 2;
                    goto Label_0333;
                Label_0096:
                    if (ProgressWindow.ShouldKeepVisible != null)
                    {
                        goto Label_0083;
                    }
                    ProgressWindow.Close();
                    GameUtility.FadeOut(1f);
                Label_00AF:
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 3;
                    goto Label_0333;
                Label_00C6:
                    if (GameUtility.IsScreenFading != null)
                    {
                        goto Label_00AF;
                    }
                    if ((this.<request>__0.asset != null) == null)
                    {
                        goto Label_02F5;
                    }
                    this.<skipping>__1 = MonoSingleton<GameManager>.Instance.EnableAnimationFrameSkipping;
                    MonoSingleton<GameManager>.Instance.EnableAnimationFrameSkipping = 0;
                    this.<script>__2 = this.<request>__0.asset as EventScript;
                    this.<script>__2.ResetTriggers();
                    this.<seq>__3 = this.<script>__2.OnStart(0, 0);
                    if (this.<>f__this.is3DEvent == null)
                    {
                        goto Label_0182;
                    }
                    this.<>f__this.self.CreateSkipCanvas();
                    goto Label_016D;
                Label_015A:
                    this.$current = null;
                    this.$PC = 4;
                    goto Label_0333;
                Label_016D:
                    if (this.<>f__this.self.canvasCreating != null)
                    {
                        goto Label_015A;
                    }
                Label_0182:
                    this.<skipEnable>__4 = 0;
                    this.<prevSkipEnable>__5 = 0;
                    this.<skipInitialized>__6 = 0;
                    goto Label_0274;
                Label_019C:
                    if (this.<>f__this.is3DEvent == null)
                    {
                        goto Label_021C;
                    }
                    this.<skipEnable>__4 = this.<seq>__3.ReplaySkipButtonEnable();
                    if (this.<skipInitialized>__6 == null)
                    {
                        goto Label_01D9;
                    }
                    if (this.<skipEnable>__4 == this.<prevSkipEnable>__5)
                    {
                        goto Label_0210;
                    }
                Label_01D9:
                    this.<skipInitialized>__6 = 1;
                    if (this.<skipEnable>__4 == null)
                    {
                        goto Label_0200;
                    }
                    this.<>f__this.self.EnableSkipButton();
                    goto Label_0210;
                Label_0200:
                    this.<>f__this.self.DisableSkipButton();
                Label_0210:
                    this.<prevSkipEnable>__5 = this.<skipEnable>__4;
                Label_021C:
                    this.$current = new WaitForEndOfFrame();
                    this.$PC = 5;
                    goto Label_0333;
                Label_0233:
                    if (this.<>f__this.self.skip == null)
                    {
                        goto Label_0274;
                    }
                    this.<>f__this.self.skip = 0;
                    if (this.<>f__this.is3DEvent == null)
                    {
                        goto Label_0274;
                    }
                    this.<seq>__3.OnQuitImmediate();
                Label_0274:
                    if ((this.<seq>__3 != null) == null)
                    {
                        goto Label_0295;
                    }
                    if (this.<seq>__3.IsPlaying != null)
                    {
                        goto Label_019C;
                    }
                Label_0295:
                    if (this.<>f__this.is3DEvent == null)
                    {
                        goto Label_02B5;
                    }
                    this.<>f__this.self.DestroySkipCanvas();
                Label_02B5:
                    MonoSingleton<GameManager>.Instance.EnableAnimationFrameSkipping = this.<skipping>__1;
                    this.<script>__2.ResetTriggers();
                    Object.Destroy(this.<seq>__3.get_gameObject());
                    this.<seq>__3 = null;
                    this.<script>__2 = null;
                    this.<request>__0 = null;
                Label_02F5:
                    if (FadeController.Instance.IsScreenFaded(0) != null)
                    {
                        goto Label_031A;
                    }
                    FadeController.Instance.FadeTo(Color.get_black(), 0f, 0);
                Label_031A:
                    this.<>f__this.self.GotoState<SceneReplay.State_PostReplay>();
                    this.$PC = -1;
                Label_0331:
                    return 0;
                Label_0333:
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
    }
}

