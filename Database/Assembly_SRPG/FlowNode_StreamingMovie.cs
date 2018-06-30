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

    [Pin(3, "Success", 1, 100), Pin(4, "Failed", 1, 0x65), Pin(5, "Finished", 1, 0x66), NodeType("UI/StreamingMovie", 0x7fe5), Pin(0x3e8, "Play", 0, 0)]
    public class FlowNode_StreamingMovie : FlowNode
    {
        private const int PIN_ID_SUCCESS = 3;
        private const int PIN_ID_FAILED = 4;
        private const int PIN_ID_FINISHED = 5;
        private const int PIN_ID_PLAY = 0x3e8;
        private const float FadeTime = 1f;
        public string FileName;
        private MySound.VolumeHandle hBGMVolume;
        private MySound.VolumeHandle hVoiceVolume;
        public string ReplayText;
        public string RetryText;
        public bool AutoFade;
        public Color FadeColor;

        public FlowNode_StreamingMovie()
        {
            this.FadeColor = Color.get_black();
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            if (num == 3)
            {
                goto Label_009D;
            }
            if (num == 4)
            {
                goto Label_009D;
            }
            if (num == 0x3e8)
            {
                goto Label_0020;
            }
            goto Label_009D;
        Label_0020:
            if (Application.get_internetReachability() != null)
            {
                goto Label_0082;
            }
            if (string.IsNullOrEmpty(this.RetryText) != null)
            {
                goto Label_006D;
            }
            UIUtility.ConfirmBox(LocalizedText.Get(this.RetryText), new UIUtility.DialogResultEvent(this.OnRetry), new UIUtility.DialogResultEvent(this.OnCancelRetry), null, 1, -1, null, null);
            goto Label_007D;
        Label_006D:
            base.ActivateOutputLinks(4);
            base.ActivateOutputLinks(5);
        Label_007D:
            goto Label_008E;
        Label_0082:
            this.Play(this.FileName);
        Label_008E:
            goto Label_009D;
            goto Label_009D;
        Label_009D:
            return;
        }

        private void OnCancelReplay(GameObject go)
        {
            base.ActivateOutputLinks(3);
            base.ActivateOutputLinks(5);
            return;
        }

        private void OnCancelRetry(GameObject go)
        {
            base.ActivateOutputLinks(4);
            base.ActivateOutputLinks(5);
            return;
        }

        public void OnFinished(bool is_replay)
        {
            if (this.hBGMVolume == null)
            {
                goto Label_001D;
            }
            this.hBGMVolume.Discard();
            this.hBGMVolume = null;
        Label_001D:
            if (this.hVoiceVolume == null)
            {
                goto Label_003A;
            }
            this.hVoiceVolume.Discard();
            this.hVoiceVolume = null;
        Label_003A:
            if (this.AutoFade == null)
            {
                goto Label_005A;
            }
            FadeController.Instance.FadeTo(Color.get_clear(), 1f, 0);
        Label_005A:
            if (string.IsNullOrEmpty(this.ReplayText) != null)
            {
                goto Label_00AA;
            }
            if (is_replay != null)
            {
                goto Label_00AA;
            }
            base.set_enabled(0);
            UIUtility.ConfirmBox(LocalizedText.Get(this.ReplayText), new UIUtility.DialogResultEvent(this.OnRetry), new UIUtility.DialogResultEvent(this.OnCancelReplay), null, 1, -1, null, null);
            goto Label_00C1;
        Label_00AA:
            base.ActivateOutputLinks(3);
            base.ActivateOutputLinks(5);
            base.set_enabled(0);
        Label_00C1:
            return;
        }

        private void OnRetry(GameObject go)
        {
            this.OnActivate(0x3e8);
            return;
        }

        private void Play(string fileName)
        {
            base.set_enabled(1);
            this.hBGMVolume = new MySound.VolumeHandle(0);
            this.hBGMVolume.SetVolume(0f, 0f);
            this.hVoiceVolume = new MySound.VolumeHandle(3);
            this.hVoiceVolume.SetVolume(0f, 0f);
            if (this.AutoFade == null)
            {
                goto Label_0094;
            }
            SRPG_TouchInputModule.LockInput();
            CriticalSection.Enter(1);
            FadeController.Instance.FadeTo(this.FadeColor, 1f, 0);
            base.StartCoroutine(this.PlayDelayed(fileName, new SRPG.StreamingMovie.OnFinished(this.OnFinished)));
            goto Label_00AD;
        Label_0094:
            MonoSingleton<StreamingMovie>.Instance.Play(fileName, new SRPG.StreamingMovie.OnFinished(this.OnFinished), null);
        Label_00AD:
            return;
        }

        [DebuggerHidden]
        private IEnumerator PlayDelayed(string filename, SRPG.StreamingMovie.OnFinished callback)
        {
            <PlayDelayed>c__IteratorCC rcc;
            rcc = new <PlayDelayed>c__IteratorCC();
            rcc.filename = filename;
            rcc.callback = callback;
            rcc.<$>filename = filename;
            rcc.<$>callback = callback;
            return rcc;
        }

        [CompilerGenerated]
        private sealed class <PlayDelayed>c__IteratorCC : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string filename;
            internal StreamingMovie.OnFinished callback;
            internal int $PC;
            internal object $current;
            internal string <$>filename;
            internal StreamingMovie.OnFinished <$>callback;

            public <PlayDelayed>c__IteratorCC()
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
                goto Label_0074;
            Label_0021:
                goto Label_0039;
            Label_0026:
                this.$current = null;
                this.$PC = 1;
                goto Label_0076;
            Label_0039:
                if (FadeController.Instance.IsFading(0) != null)
                {
                    goto Label_0026;
                }
                SRPG_TouchInputModule.UnlockInput(0);
                CriticalSection.Leave(1);
                MonoSingleton<StreamingMovie>.Instance.Play(this.filename, this.callback, null);
                this.$PC = -1;
            Label_0074:
                return 0;
            Label_0076:
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

