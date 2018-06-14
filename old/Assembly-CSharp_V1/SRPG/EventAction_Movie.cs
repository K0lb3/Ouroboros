// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Movie
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/Movie", "指定のムービーをストリーミング再生します", 5592405, 4473992)]
  public class EventAction_Movie : EventAction
  {
    private static readonly string PREFIX = "videos/";
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
      string str = "en/";
      string configLanguage = GameUtility.Config_Language;
      if (configLanguage != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (EventAction_Movie.\u003C\u003Ef__switch\u0024mapC == null)
        {
          // ISSUE: reference to a compiler-generated field
          EventAction_Movie.\u003C\u003Ef__switch\u0024mapC = new Dictionary<string, int>(3)
          {
            {
              "french",
              0
            },
            {
              "german",
              1
            },
            {
              "spanish",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (EventAction_Movie.\u003C\u003Ef__switch\u0024mapC.TryGetValue(configLanguage, out num))
        {
          switch (num)
          {
            case 0:
              str = "fr/";
              break;
            case 1:
              str = "de/";
              break;
            case 2:
              str = "es/";
              break;
          }
        }
      }
      switch ((int) Application.get_internetReachability())
      {
        case 0:
          this.ActivateNext();
          break;
        case 1:
        case 2:
          this.PlayMovie(EventAction_Movie.PREFIX + str + this.Filename);
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
