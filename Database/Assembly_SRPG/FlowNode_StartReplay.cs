namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(10, "Started", 1, 10), NodeType("System/Replay/StartReplay", 0x7fe5), Pin(11, "Failed", 1, 11), Pin(0, "Load", 0, 0)]
    public class FlowNode_StartReplay : FlowNode
    {
        private const int PIN_ID_LOAD = 0;
        private const int PIN_ID_STARTED = 10;
        private const int PIN_ID_FAILED = 11;
        [HideInInspector]
        public RestorePoints RestorePoint;
        public bool SetRestorePoint;

        public FlowNode_StartReplay()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            QuestParam param;
            if (pinID != null)
            {
                goto Label_0042;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            base.set_enabled(1);
            CriticalSection.Enter(4);
            param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.ReplaySelectedQuestID);
            base.StartCoroutine(this.StartScene(param));
        Label_0042:
            return;
        }

        private void OnSceneLoad(GameObject sceneRoot)
        {
            SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneLoad));
            CriticalSection.Leave(4);
            return;
        }

        [DebuggerHidden]
        private IEnumerator StartScene(QuestParam questParam)
        {
            <StartScene>c__IteratorCB rcb;
            rcb = new <StartScene>c__IteratorCB();
            rcb.questParam = questParam;
            rcb.<$>questParam = questParam;
            rcb.<>f__this = this;
            return rcb;
        }

        [CompilerGenerated]
        private sealed class <StartScene>c__IteratorCB : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string <sceneName>__0;
            internal SceneRequest <req>__1;
            internal QuestParam questParam;
            internal FlowNode_StartReplay.QuestLauncher <questLauncher>__2;
            internal int $PC;
            internal object $current;
            internal QuestParam <$>questParam;
            internal FlowNode_StartReplay <>f__this;

            public <StartScene>c__IteratorCB()
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
                        goto Label_0070;

                    case 2:
                        goto Label_00CB;

                    case 3:
                        goto Label_0104;

                    case 4:
                        goto Label_015F;

                    case 5:
                        goto Label_0182;
                }
                goto Label_0189;
            Label_0031:
                ProgressWindow.OpenQuestLoadScreen(null, null);
                if (this.<>f__this.SetRestorePoint == null)
                {
                    goto Label_0070;
                }
                HomeWindow.SetRestorePoint(this.<>f__this.RestorePoint);
                goto Label_0070;
            Label_005D:
                this.$current = null;
                this.$PC = 1;
                goto Label_018B;
            Label_0070:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_005D;
                }
                this.<sceneName>__0 = "Replay";
                if (AssetManager.IsAssetBundle(this.<sceneName>__0) == null)
                {
                    goto Label_00DA;
                }
                AssetManager.PrepareAssets(this.<sceneName>__0);
                if (AssetDownloader.isDone != null)
                {
                    goto Label_00DA;
                }
                ProgressWindow.OpenGenericDownloadWindow();
                AssetDownloader.StartDownload(0, 1, 2);
            Label_00B8:
                this.$current = null;
                this.$PC = 2;
                goto Label_018B;
            Label_00CB:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_00B8;
                }
                ProgressWindow.Close();
            Label_00DA:
                this.<req>__1 = AssetManager.LoadSceneAsync(this.<sceneName>__0, 0);
                goto Label_0104;
            Label_00F1:
                this.$current = null;
                this.$PC = 3;
                goto Label_018B;
            Label_0104:
                if (this.<req>__1.canBeActivated == null)
                {
                    goto Label_00F1;
                }
                this.<questLauncher>__2 = new FlowNode_StartReplay.QuestLauncher(this.questParam);
                SceneAwakeObserver.AddListener(new SceneAwakeObserver.SceneEvent(this.<questLauncher>__2.OnSceneAwake));
                this.<req>__1.ActivateScene();
                goto Label_015F;
            Label_014C:
                this.$current = null;
                this.$PC = 4;
                goto Label_018B;
            Label_015F:
                if (this.<req>__1.isDone == null)
                {
                    goto Label_014C;
                }
                this.$current = null;
                this.$PC = 5;
                goto Label_018B;
            Label_0182:
                this.$PC = -1;
            Label_0189:
                return 0;
            Label_018B:
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

        private class QuestLauncher
        {
            private QuestParam mQuestParam;

            public QuestLauncher(QuestParam questParam)
            {
                base..ctor();
                this.mQuestParam = questParam;
                return;
            }

            public void OnSceneAwake(GameObject scene)
            {
                SceneReplay replay;
                replay = scene.GetComponent<SceneReplay>();
                if ((replay != null) == null)
                {
                    goto Label_003B;
                }
                CriticalSection.Leave(4);
                SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnSceneAwake));
                replay.StartQuest(this.mQuestParam.iname);
            Label_003B:
                return;
            }
        }
    }
}

