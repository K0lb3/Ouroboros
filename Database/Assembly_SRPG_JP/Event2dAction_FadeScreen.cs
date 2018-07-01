// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_FadeScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("フェード(2D)", "画面をフェードイン・フェードアウトさせます", 5592405, 4473992)]
  public class Event2dAction_FadeScreen : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/Event2dFade";
    public float FadeTime = 1f;
    private Color FadeInColorWhite = new Color(1f, 1f, 1f, 0.0f);
    private Color FadeInColorBlack = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public bool FadeOut;
    public bool ChangeColor;
    public bool Async;
    private Event2dFade mEvent2dFade;
    private LoadRequest mResource;

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_FadeScreen.\u003CPreloadAssets\u003Ec__IteratorA1()
      {
        \u003C\u003Ef__this = this
      };
    }

    public override void PreStart()
    {
      if (Object.op_Inequality((Object) this.mEvent2dFade, (Object) null))
        return;
      this.mEvent2dFade = Event2dFade.Find();
      if (Object.op_Inequality((Object) this.mEvent2dFade, (Object) null))
        return;
      this.mEvent2dFade = Object.Instantiate(this.mResource.asset) as Event2dFade;
      ((Component) this.mEvent2dFade).get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
      ((Component) this.mEvent2dFade).get_transform().SetAsLastSibling();
      ((Component) this.mEvent2dFade).get_gameObject().SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) this.mEvent2dFade, (Object) null))
        return;
      ((Component) this.mEvent2dFade).get_gameObject().SetActive(true);
      this.StartFade();
    }

    private void StartFade()
    {
      if (this.FadeOut)
      {
        if (this.ChangeColor)
          this.mEvent2dFade.FadeTo(Color.get_white(), this.FadeTime);
        else
          this.mEvent2dFade.FadeTo(Color.get_black(), this.FadeTime);
      }
      else if (this.ChangeColor)
        this.mEvent2dFade.FadeTo(this.FadeInColorWhite, this.FadeTime);
      else
        this.mEvent2dFade.FadeTo(this.FadeInColorBlack, this.FadeTime);
      if (!this.Async)
        return;
      this.ActivateNext();
    }

    public override void Update()
    {
      if (this.mEvent2dFade.IsFading)
        return;
      this.ActivateNext();
    }
  }
}
