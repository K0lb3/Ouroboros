namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [EventActionInfo("New/Movie", "指定のムービーをストリーミング再生します", 0x555555, 0x444488)]
    public class EventAction_Movie : EventAction
    {
        private const string PREFAB_PATH = "UI/FullScreenMovieDemo";
        private static readonly string PREFIX;
        public string Filename;
        public float FadeTime;
        public bool AutoFade;
        public Color FadeColor;
        private bool Played;
        private string PlayFilename;
        private MySound.VolumeHandle hBGMVolume;
        private MySound.VolumeHandle hVoiceVolume;

        static EventAction_Movie()
        {
            PREFIX = "movies/";
            return;
        }

        public EventAction_Movie()
        {
            this.FadeTime = 1f;
            this.FadeColor = Color.get_black();
            base..ctor();
            return;
        }

        public void Finished(bool is_replay)
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
                goto Label_0067;
            }
            SRPG_TouchInputModule.UnlockInput(0);
            CriticalSection.Leave(1);
            FadeController.Instance.FadeTo(Color.get_clear(), this.FadeTime, 0);
        Label_0067:
            base.ActivateNext();
            return;
        }

        public override void GoToEndState()
        {
            MonoSingleton<StreamingMovie>.Instance.Skip();
            return;
        }

        public override void OnActivate()
        {
            NetworkReachability reachability;
            switch (Application.get_internetReachability())
            {
                case 0:
                    goto Label_0038;

                case 1:
                    goto Label_001D;

                case 2:
                    goto Label_001D;
            }
            goto Label_0043;
        Label_001D:
            this.PlayMovie(PREFIX + this.Filename);
            goto Label_0043;
        Label_0038:
            base.ActivateNext();
        Label_0043:
            return;
        }

        private void PlayMovie(string filename)
        {
            this.hBGMVolume = new MySound.VolumeHandle(0);
            this.hBGMVolume.SetVolume(0f, 0f);
            this.hVoiceVolume = new MySound.VolumeHandle(3);
            this.hVoiceVolume.SetVolume(0f, 0f);
            if (this.AutoFade == null)
            {
                goto Label_007B;
            }
            SRPG_TouchInputModule.LockInput();
            CriticalSection.Enter(1);
            FadeController.Instance.FadeTo(this.FadeColor, this.FadeTime, 0);
            this.PlayFilename = filename;
            goto Label_009F;
        Label_007B:
            MonoSingleton<StreamingMovie>.Instance.Play(filename, new StreamingMovie.OnFinished(this.Finished), "UI/FullScreenMovieDemo");
            this.Played = 1;
        Label_009F:
            return;
        }

        public override bool ReplaySkipButtonEnable()
        {
            return 0;
        }

        public override void SkipImmediate()
        {
            MonoSingleton<StreamingMovie>.Instance.Skip();
            return;
        }

        public override void Update()
        {
            if (this.Played == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (FadeController.Instance.IsFading(0) == null)
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            MonoSingleton<StreamingMovie>.Instance.Play(this.PlayFilename, new StreamingMovie.OnFinished(this.Finished), "UI/FullScreenMovieDemo");
            this.Played = 1;
            return;
        }
    }
}

