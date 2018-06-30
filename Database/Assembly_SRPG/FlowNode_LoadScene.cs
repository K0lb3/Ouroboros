namespace SRPG
{
    using Gsc.App;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [Pin(20, "Started", 1, 2), Pin(0x15, "Booted", 1, 0x15), Pin(1, "Finished", 1, 3), Pin(100, "Start", 0, 0), NodeType("LoadScene", 0x7fe5)]
    public class FlowNode_LoadScene : FlowNode
    {
        private const string StartSceneName = "1_CheckVersion";
        public SceneTypes SceneType;
        public string SceneName;
        public float WaitTime;
        private float mLoadStartTime;
        private SceneRequest mOp;

        public FlowNode_LoadScene()
        {
            base..ctor();
            return;
        }

        public static void LoadBootScene()
        {
            BootLoader.Reboot();
            SceneManager.LoadScene("1_CheckVersion");
            return;
        }

        [DebuggerHidden]
        private IEnumerator LoadLevelAsync()
        {
            <LoadLevelAsync>c__IteratorBB rbb;
            rbb = new <LoadLevelAsync>c__IteratorBB();
            rbb.<>f__this = this;
            return rbb;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            SectionParam param;
            SceneTypes types;
            if (pinID != 100)
            {
                goto Label_00D1;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            str = null;
            switch (this.SceneType)
            {
                case 0:
                    goto Label_0038;

                case 1:
                    goto Label_0044;

                case 2:
                    goto Label_0038;

                case 3:
                    goto Label_005C;
            }
        Label_0038:
            str = this.SceneName;
            goto Label_006B;
        Label_0044:
            param = HomeUnitController.GetHomeWorld();
            if (param == null)
            {
                goto Label_006B;
            }
            str = param.home;
            goto Label_006B;
        Label_005C:
            LoadBootScene();
            base.ActivateOutputLinks(0x15);
            return;
        Label_006B:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0081;
            }
            DebugUtility.LogError("No Scene to load");
            return;
        Label_0081:
            base.set_enabled(1);
            CriticalSection.Enter(4);
            base.ActivateOutputLinks(20);
            DebugUtility.Log("LoadScene [" + str + "]");
            if (AssetManager.IsAssetBundle(str) == null)
            {
                goto Label_00CA;
            }
            base.StartCoroutine(this.PreLoadSceneAsync(str));
            goto Label_00D1;
        Label_00CA:
            this.StartSceneLoad(str);
        Label_00D1:
            return;
        }

        private void PreLoadScene(string sceneName)
        {
            this.StartSceneLoad(sceneName);
            return;
        }

        [DebuggerHidden]
        private IEnumerator PreLoadSceneAsync(string sceneName)
        {
            <PreLoadSceneAsync>c__IteratorBA rba;
            rba = new <PreLoadSceneAsync>c__IteratorBA();
            rba.sceneName = sceneName;
            rba.<$>sceneName = sceneName;
            rba.<>f__this = this;
            return rba;
        }

        private void StartSceneLoad(string sceneName)
        {
            if (this.SceneType != 2)
            {
                goto Label_001E;
            }
            this.mOp = AssetManager.LoadSceneAsync(sceneName, 0);
            goto Label_002B;
        Label_001E:
            this.mOp = AssetManager.LoadSceneAsync(sceneName, 1);
        Label_002B:
            this.mLoadStartTime = Time.get_time();
            base.StartCoroutine(this.LoadLevelAsync());
            return;
        }

        [CompilerGenerated]
        private sealed class <LoadLevelAsync>c__IteratorBB : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal FlowNode_LoadScene <>f__this;

            public <LoadLevelAsync>c__IteratorBB()
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
                        goto Label_0029;

                    case 1:
                        goto Label_0045;

                    case 2:
                        goto Label_0093;

                    case 3:
                        goto Label_00F6;
                }
                goto Label_013D;
            Label_0029:
                goto Label_0045;
            Label_002E:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_013F;
            Label_0045:
                if (this.<>f__this.mOp.canBeActivated == null)
                {
                    goto Label_002E;
                }
                goto Label_0093;
            Label_005F:
                this.$current = new WaitForSeconds(this.<>f__this.WaitTime - (Time.get_time() - this.<>f__this.mLoadStartTime));
                this.$PC = 2;
                goto Label_013F;
            Label_0093:
                if ((Time.get_time() - this.<>f__this.mLoadStartTime) < this.<>f__this.WaitTime)
                {
                    goto Label_005F;
                }
                this.<>f__this.mOp.ActivateScene();
                if (this.<>f__this.mOp.isAdditive == null)
                {
                    goto Label_010B;
                }
                goto Label_00F6;
            Label_00DF:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_013F;
            Label_00F6:
                if (this.<>f__this.mOp.isDone == null)
                {
                    goto Label_00DF;
                }
            Label_010B:
                CriticalSection.Leave(4);
                this.<>f__this.mOp = null;
                this.<>f__this.set_enabled(0);
                this.<>f__this.ActivateOutputLinks(1);
                this.$PC = -1;
            Label_013D:
                return 0;
            Label_013F:
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
        private sealed class <PreLoadSceneAsync>c__IteratorBA : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string sceneName;
            internal int $PC;
            internal object $current;
            internal string <$>sceneName;
            internal FlowNode_LoadScene <>f__this;

            public <PreLoadSceneAsync>c__IteratorBA()
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
                        goto Label_003D;

                    case 2:
                        goto Label_0086;
                }
                goto Label_00AD;
            Label_0025:
                goto Label_003D;
            Label_002A:
                this.$current = null;
                this.$PC = 1;
                goto Label_00AF;
            Label_003D:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_002A;
                }
                AssetManager.PrepareAssets(this.sceneName);
                if (AssetDownloader.isDone != null)
                {
                    goto Label_0095;
                }
                AssetDownloader.StartDownload(0, 1, 2);
                ProgressWindow.OpenGenericDownloadWindow();
                goto Label_0086;
            Label_006F:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 2;
                goto Label_00AF;
            Label_0086:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_006F;
                }
                ProgressWindow.Close();
            Label_0095:
                this.<>f__this.PreLoadScene(this.sceneName);
                this.$PC = -1;
            Label_00AD:
                return 0;
            Label_00AF:
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

        public enum SceneTypes
        {
            Additive,
            HomeTown,
            Replace,
            BootScene
        }
    }
}

