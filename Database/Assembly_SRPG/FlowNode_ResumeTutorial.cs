namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [Pin(10, "DebugEndMovieLoad", 0, 10), Pin(0x16, "ResumeNormalQuest Cancel", 0, 0x16), Pin(0x15, "ResumeMultiTower Cancel", 1, 0x15), Pin(20, "ClearResumeMultiTower", 0, 20), Pin(0x13, "Resume MultiTower", 1, 0x13), Pin(0x12, "FgGChainWish", 1, 0x12), Pin(0x11, "GotoHome", 0, 0x11), Pin(0x10, "ClearResumeVersus", 0, 0x10), Pin(15, "Resume Versus Cancel", 1, 15), Pin(14, "Resume Versus", 1, 14), Pin(13, "Resume Multi Cancel", 1, 13), Pin(12, "ClearTutorial", 1, 12), Pin(11, "DebugMovieLoad", 1, 11), Pin(8, "ResumeTowerError", 0, 8), Pin(7, "ClearResumeMulti", 0, 7), Pin(6, "Resume Multi", 1, 6), Pin(5, "Resume Tower", 1, 5), Pin(4, "Tutorial Skipped", 1, 4), Pin(3, "Resume Quest", 1, 3), Pin(2, "Start Quest", 1, 2), Pin(1, "Next Step", 0, 1), Pin(0, "Try", 0, 0), NodeType("System/Tutorial")]
    public class FlowNode_ResumeTutorial : FlowNode
    {
        private bool mSkipTutorial;

        public FlowNode_ResumeTutorial()
        {
            base..ctor();
            return;
        }

        private void ClearMultiResumeData()
        {
            bool? nullable;
            BattleCore.RemoveSuspendData();
            Network.RequestAPI(new ReqBtlComEnd(GlobalVars.BtlID, 0, 3, null, null, null, null, null, null, new Network.ResponseCallback(this.OnBtlComEnd), 1, null, null, 0, null, 0, new bool?()), 0);
            GlobalVars.BtlID.Set(0L);
            return;
        }

        private void ClearMultiTowerResumeData()
        {
            BattleCore.RemoveSuspendData();
            Network.RequestAPI(new ReqBtlMultiTwEnd(GlobalVars.BtlID, 0, 2, null, null, null, new Network.ResponseCallback(this.OnBtlComEnd), null, null), 0);
            GlobalVars.BtlID.Set(0L);
            return;
        }

        private void ClearResumeData(bool is_rehash)
        {
            bool? nullable;
            BattleCore.RemoveSuspendData();
            Network.RequestAPI(new ReqBtlComEnd(GlobalVars.BtlID, 0, 3, null, null, null, null, null, null, new Network.ResponseCallback(this.OnBtlComEnd), 0, null, null, 0, null, is_rehash, new bool?()), 0);
            GlobalVars.BtlID.Set(0L);
            return;
        }

        private void ClearTowerResumeData()
        {
            BattleCore.RemoveSuspendData();
            Network.RequestAPI(new ReqTowerBtlComEnd(GlobalVars.BtlID, null, null, 0, 0, 0, 3, null, new Network.ResponseCallback(this.OnBtlComEnd), null, null, null, null), 0);
            GlobalVars.BtlID.Set(0L);
            return;
        }

        private void ClearVersusResumeData()
        {
            BattleCore.RemoveSuspendData();
            Network.RequestAPI(new ReqVersusEnd(GlobalVars.BtlID, 3, null, null, 0, null, null, 0, 0, 0, 0, null, new Network.ResponseCallback(this.OnBtlComEnd), GlobalVars.SelectedMultiPlayVersusType, null, null), 0);
            GlobalVars.BtlID.Set(0L);
            return;
        }

        private void CompleteTutorial()
        {
            GameManager manager;
            MonoSingleton<GameManager>.Instance.UpdateTutorialFlags(1L);
            base.StartCoroutine(this.WaitCompleteTutorialAsync());
            return;
        }

        [DebuggerHidden]
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            <LoadSceneAsync>c__IteratorC9 rc;
            rc = new <LoadSceneAsync>c__IteratorC9();
            rc.sceneName = sceneName;
            rc.<$>sceneName = sceneName;
            rc.<>f__this = this;
            return rc;
        }

        private void LoadStartScene()
        {
            base.StartCoroutine(this.LoadSceneAsync("Home"));
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            QuestTypes types;
            if (pinID != null)
            {
                goto Label_0235;
            }
            manager = MonoSingleton<GameManager>.Instance;
            base.set_enabled(1);
            if (GlobalVars.BtlID == null)
            {
                goto Label_01AD;
            }
            if ((manager.Player.TutorialFlags & 1L) != null)
            {
                goto Label_003E;
            }
            base.ActivateOutputLinks(3);
            return;
        Label_003E:
            types = GlobalVars.QuestType;
            switch ((types - 7))
            {
                case 0:
                    goto Label_0080;

                case 1:
                    goto Label_00E4;

                case 2:
                    goto Label_00E4;

                case 3:
                    goto Label_0074;

                case 4:
                    goto Label_0074;

                case 5:
                    goto Label_0148;

                case 6:
                    goto Label_0074;

                case 7:
                    goto Label_00B2;

                case 8:
                    goto Label_0074;

                case 9:
                    goto Label_0116;
            }
        Label_0074:
            if (types == 1)
            {
                goto Label_00B2;
            }
            goto Label_017A;
        Label_0080:
            UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnTowerResumeAccept), new UIUtility.DialogResultEvent(this.OnTowerResumeCancel), null, 0, -1, null, null);
            goto Label_01AC;
        Label_00B2:
            UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnMultiResumeAccept), new UIUtility.DialogResultEvent(this.OnMultiResumeCancel), null, 0, -1, null, null);
            goto Label_01AC;
        Label_00E4:
            UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_VERSUS_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnVersusAccept), new UIUtility.DialogResultEvent(this.OnVersusResumeCancel), null, 0, -1, null, null);
            goto Label_01AC;
        Label_0116:
            UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_VERSUS_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnVersusAccept), new UIUtility.DialogResultEvent(this.OnVersusResumeCancel), null, 0, -1, null, null);
            goto Label_01AC;
        Label_0148:
            UIUtility.ConfirmBox(LocalizedText.Get("sys.MULTI_TOWER_CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnMultiTowerAccept), new UIUtility.DialogResultEvent(this.OnMultiTowerResumeCancel), null, 0, -1, null, null);
            goto Label_01AC;
        Label_017A:
            UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RESUMEQUEST"), new UIUtility.DialogResultEvent(this.OnResumeAccept), new UIUtility.DialogResultEvent(this.OnResumeCancel), null, 0, -1, null, null);
        Label_01AC:
            return;
        Label_01AD:
            BattleCore.RemoveSuspendData();
            if ((manager.Player.TutorialFlags & 1L) == null)
            {
                goto Label_01F0;
            }
            GlobalVars.IsTutorialEnd = 1;
            if (MonoSingleton<GameManager>.Instance.AuthStatus != 3)
            {
                goto Label_01E6;
            }
            this.LoadStartScene();
            goto Label_01EF;
        Label_01E6:
            base.ActivateOutputLinks(0x12);
        Label_01EF:
            return;
        Label_01F0:
            manager.UpdateTutorialStep();
            if (manager.TutorialStep != null)
            {
                goto Label_022E;
            }
            if (GameUtility.IsDebugBuild == null)
            {
                goto Label_022E;
            }
            if (GlobalVars.DebugIsPlayTutorial == null)
            {
                goto Label_0220;
            }
            this.PlayTutorial();
            goto Label_022D;
        Label_0220:
            this.mSkipTutorial = 1;
            this.CompleteTutorial();
        Label_022D:
            return;
        Label_022E:
            this.PlayTutorial();
            return;
        Label_0235:
            if (pinID != 1)
            {
                goto Label_0251;
            }
            MonoSingleton<GameManager>.Instance.CompleteTutorialStep();
            this.PlayTutorial();
            goto Label_02D0;
        Label_0251:
            if (pinID != 10)
            {
                goto Label_0264;
            }
            this.PlayTutorial();
            goto Label_02D0;
        Label_0264:
            if (pinID != 7)
            {
                goto Label_0276;
            }
            this.ClearMultiResumeData();
            goto Label_02D0;
        Label_0276:
            if (pinID != 8)
            {
                goto Label_0288;
            }
            this.ClearTowerResumeData();
            goto Label_02D0;
        Label_0288:
            if (pinID != 0x10)
            {
                goto Label_029B;
            }
            this.ClearVersusResumeData();
            goto Label_02D0;
        Label_029B:
            if (pinID != 0x11)
            {
                goto Label_02AE;
            }
            this.LoadStartScene();
            goto Label_02D0;
        Label_02AE:
            if (pinID != 20)
            {
                goto Label_02C1;
            }
            this.ClearMultiTowerResumeData();
            goto Label_02D0;
        Label_02C1:
            if (pinID != 0x16)
            {
                goto Label_02D0;
            }
            this.ClearResumeData(1);
        Label_02D0:
            return;
        }

        private unsafe void OnBtlComEnd(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (GlobalVars.QuestType != 7)
            {
                goto Label_0023;
            }
            if (TowerErrorHandle.Error(null) == null)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            if (Network.IsError == null)
            {
                goto Label_005A;
            }
            code = Network.ErrCode;
            if (code == 0xdac)
            {
                goto Label_004E;
            }
            if (code == 0xf3c)
            {
                goto Label_004E;
            }
            goto Label_0054;
        Label_004E:
            FlowNode_Network.Failed();
            return;
        Label_0054:
            FlowNode_Network.Retry();
            return;
        Label_005A:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            if (response.body != null)
            {
                goto Label_0078;
            }
            FlowNode_Network.Retry();
            return;
        Label_0078:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                if (response.body.mails == null)
                {
                    goto Label_00DD;
                }
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.mails);
            Label_00DD:
                goto Label_00F8;
            }
            catch (Exception exception1)
            {
            Label_00E2:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_0103;
            }
        Label_00F8:
            Network.RemoveAPI();
            this.LoadStartScene();
        Label_0103:
            return;
        }

        private void OnMultiResumeAccept(GameObject go)
        {
            base.ActivateOutputLinks(6);
            return;
        }

        private void OnMultiResumeCancel(GameObject go)
        {
            base.ActivateOutputLinks(13);
            return;
        }

        private void OnMultiTowerAccept(GameObject go)
        {
            base.ActivateOutputLinks(0x13);
            return;
        }

        private void OnMultiTowerResumeCancel(GameObject go)
        {
            base.ActivateOutputLinks(0x15);
            return;
        }

        private void OnPlayTutorial(GameObject go)
        {
            base.ActivateOutputLinks(11);
            return;
        }

        private void OnResumeAccept(GameObject go)
        {
            base.ActivateOutputLinks(3);
            return;
        }

        private void OnResumeCancel(GameObject go)
        {
            this.ClearResumeData(0);
            return;
        }

        private void OnSkipTutorial(GameObject go)
        {
            this.mSkipTutorial = 1;
            this.CompleteTutorial();
            return;
        }

        private void OnTowerResumeAccept(GameObject go)
        {
            base.ActivateOutputLinks(5);
            return;
        }

        private void OnTowerResumeCancel(GameObject go)
        {
            this.ClearTowerResumeData();
            return;
        }

        private void OnVersusAccept(GameObject go)
        {
            base.ActivateOutputLinks(14);
            return;
        }

        private void OnVersusResumeCancel(GameObject go)
        {
            base.ActivateOutputLinks(15);
            return;
        }

        private void PlayTutorial()
        {
            GameManager manager;
            string str;
            QuestParam param;
            manager = MonoSingleton<GameManager>.Instance;
            str = manager.GetNextTutorialStep();
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_001F;
            }
            this.CompleteTutorial();
            return;
        Label_001F:
            if (manager.FindQuest(str) != null)
            {
                goto Label_003C;
            }
            base.StartCoroutine(this.LoadSceneAsync(str));
            return;
        Label_003C:
            GlobalVars.SelectedQuestID = str;
            GlobalVars.SelectedFriendID = string.Empty;
            base.ActivateOutputLinks(2);
            return;
        }

        [DebuggerHidden]
        private IEnumerator WaitCompleteTutorialAsync()
        {
            <WaitCompleteTutorialAsync>c__IteratorC8 rc;
            rc = new <WaitCompleteTutorialAsync>c__IteratorC8();
            rc.<>f__this = this;
            return rc;
        }

        [CompilerGenerated]
        private sealed class <LoadSceneAsync>c__IteratorC9 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string sceneName;
            internal SceneRequest <mReq>__0;
            internal int $PC;
            internal object $current;
            internal string <$>sceneName;
            internal FlowNode_ResumeTutorial <>f__this;

            public <LoadSceneAsync>c__IteratorC9()
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
                        goto Label_0045;

                    case 2:
                        goto Label_00A9;

                    case 3:
                        goto Label_0144;

                    case 4:
                        goto Label_0166;
                }
                goto Label_0195;
            Label_002D:
                goto Label_0045;
            Label_0032:
                this.$current = null;
                this.$PC = 1;
                goto Label_0197;
            Label_0045:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0032;
                }
                if (GameUtility.Config_UseAssetBundles.Value == null)
                {
                    goto Label_00B8;
                }
                if (AssetManager.IsAssetBundle(this.sceneName) == null)
                {
                    goto Label_00B8;
                }
                AssetManager.PrepareAssets(this.sceneName);
                AssetDownloader.StartDownload(0, 1, 2);
                if (AssetDownloader.isDone != null)
                {
                    goto Label_00B8;
                }
                ProgressWindow.OpenGenericDownloadWindow();
                goto Label_00A9;
            Label_0096:
                this.$current = null;
                this.$PC = 2;
                goto Label_0197;
            Label_00A9:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_0096;
                }
                ProgressWindow.Close();
            Label_00B8:
                if (FadeController.Instance.IsScreenFaded(0) != null)
                {
                    goto Label_011A;
                }
                FadeController.Instance.FadeTo(new Color(0f, 0f, 0f, 0f), 0f, 0);
                FadeController.Instance.FadeTo(new Color(0f, 0f, 0f, 1f), 1f, 0);
            Label_011A:
                this.<mReq>__0 = AssetManager.LoadSceneAsync(this.sceneName, 0);
                goto Label_0144;
            Label_0131:
                this.$current = null;
                this.$PC = 3;
                goto Label_0197;
            Label_0144:
                if (GameUtility.IsScreenFading != null)
                {
                    goto Label_0131;
                }
                goto Label_0166;
            Label_0153:
                this.$current = null;
                this.$PC = 4;
                goto Label_0197;
            Label_0166:
                if (this.<mReq>__0.canBeActivated == null)
                {
                    goto Label_0153;
                }
                this.<>f__this.set_enabled(0);
                this.<mReq>__0.ActivateScene();
                this.$PC = -1;
            Label_0195:
                return 0;
            Label_0197:
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
        private sealed class <WaitCompleteTutorialAsync>c__IteratorC8 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal FlowNode_ResumeTutorial <>f__this;

            public <WaitCompleteTutorialAsync>c__IteratorC8()
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
                        goto Label_0021;

                    case 1:
                        goto Label_0039;
                }
                goto Label_00A6;
            Label_0021:
                goto Label_0039;
            Label_0026:
                this.$current = null;
                this.$PC = 1;
                goto Label_00A8;
            Label_0039:
                if (Network.IsConnecting != null)
                {
                    goto Label_0026;
                }
                GlobalVars.IsTutorialEnd = 1;
                this.<>f__this.ActivateOutputLinks(12);
                if (this.<>f__this.mSkipTutorial == null)
                {
                    goto Label_0085;
                }
                this.<>f__this.mSkipTutorial = 0;
                this.<>f__this.ActivateOutputLinks(4);
                goto Label_00A6;
            Label_0085:
                MonoSingleton<GameManager>.Instance.Player.ForceFirstLogin();
                this.<>f__this.LoadStartScene();
                this.$PC = -1;
            Label_00A6:
                return 0;
            Label_00A8:
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

