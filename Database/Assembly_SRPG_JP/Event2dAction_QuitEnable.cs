// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_QuitEnable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  [EventActionInfo("強制終了/許可(2D)", "強制終了を許可します", 5592405, 4473992)]
  public class Event2dAction_QuitEnable : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/BtnSkip";
    protected static EventQuit mQuit;
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
      return (IEnumerator) new Event2dAction_QuitEnable.\u003CPreloadAssets\u003Ec__IteratorA2()
      {
        \u003C\u003Ef__this = this
      };
    }

    public override void PreStart()
    {
      if (Object.op_Inequality((Object) null, (Object) Event2dAction_QuitEnable.mQuit) || this.mResource == null)
        return;
      Event2dAction_QuitEnable.mQuit = Object.Instantiate(this.mResource.asset) as EventQuit;
      ((Component) Event2dAction_QuitEnable.mQuit).get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
      ((Component) Event2dAction_QuitEnable.mQuit).get_transform().SetAsLastSibling();
      // ISSUE: method pointer
      Event2dAction_QuitEnable.mQuit.OnClick = new UnityAction((object) this, __methodptr(\u003CPreStart\u003Em__181));
      ((Component) Event2dAction_QuitEnable.mQuit).get_gameObject().SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) null, (Object) Event2dAction_QuitEnable.mQuit))
        return;
      ((Component) Event2dAction_QuitEnable.mQuit).get_gameObject().SetActive(true);
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      Event2dAction_QuitEnable.mQuit = (EventQuit) null;
    }
  }
}
