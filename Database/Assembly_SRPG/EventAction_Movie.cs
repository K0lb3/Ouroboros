// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Movie
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/Movie", "指定のムービーをストリーミング再生します", 5592405, 4473992)]
  public class EventAction_Movie : EventAction
  {
    private static readonly string PREFIX = "movies/";
    public float FadeTime = 1f;
    public Color FadeColor = Color.get_black();
    private const string PREFAB_PATH = "UI/FullScreenMovieDemo";
    public string Filename;
    public bool AutoFade;
    private bool Played;
    private string PlayFilename;
    private MySound.VolumeHandle hBGMVolume;
    private MySound.VolumeHandle hVoiceVolume;

    public override void OnActivate()
    {
      switch ((int) Application.get_internetReachability())
      {
        case 0:
          this.ActivateNext();
          break;
        case 1:
        case 2:
          this.PlayMovie(EventAction_Movie.PREFIX + this.Filename);
          break;
      }
    }

    private void PlayMovie(string filename)
    {
      this.hBGMVolume = new MySound.VolumeHandle(MySound.EType.BGM);
      this.hBGMVolume.SetVolume(0.0f, 0.0f);
      this.hVoiceVolume = new MySound.VolumeHandle(MySound.EType.VOICE);
      this.hVoiceVolume.SetVolume(0.0f, 0.0f);
      if (this.AutoFade)
      {
        SRPG_TouchInputModule.LockInput();
        CriticalSection.Enter(CriticalSections.Default);
        FadeController.Instance.FadeTo(this.FadeColor, this.FadeTime, 0);
        this.PlayFilename = filename;
      }
      else
      {
        MonoSingleton<StreamingMovie>.Instance.Play(filename, new StreamingMovie.OnFinished(this.Finished), "UI/FullScreenMovieDemo");
        this.Played = true;
      }
    }

    public override void Update()
    {
      if (this.Played || FadeController.Instance.IsFading(0))
        return;
      MonoSingleton<StreamingMovie>.Instance.Play(this.PlayFilename, new StreamingMovie.OnFinished(this.Finished), "UI/FullScreenMovieDemo");
      this.Played = true;
    }

    public void Finished()
    {
      if (this.hBGMVolume != null)
      {
        this.hBGMVolume.Discard();
        this.hBGMVolume = (MySound.VolumeHandle) null;
      }
      if (this.hVoiceVolume != null)
      {
        this.hVoiceVolume.Discard();
        this.hVoiceVolume = (MySound.VolumeHandle) null;
      }
      if (this.AutoFade)
      {
        SRPG_TouchInputModule.UnlockInput(false);
        CriticalSection.Leave(CriticalSections.Default);
        FadeController.Instance.FadeTo(Color.get_clear(), this.FadeTime, 0);
      }
      this.ActivateNext();
    }

    public override bool ReplaySkipButtonEnable()
    {
      return false;
    }

    public override void SkipImmediate()
    {
      MonoSingleton<StreamingMovie>.Instance.Skip();
    }
  }
}
