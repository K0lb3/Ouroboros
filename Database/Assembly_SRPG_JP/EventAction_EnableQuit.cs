// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_EnableQuit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  [EventActionInfo("強制終了/許可", "スクリプトの強制終了を有効にします。", 5592405, 4473992)]
  public class EventAction_EnableQuit : EventAction
  {
    private static readonly string AssetPath = "UI/BtnSkip_movie";
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
      return (IEnumerator) new EventAction_EnableQuit.\u003CPreloadAssets\u003Ec__Iterator8A()
      {
        \u003C\u003Ef__this = this
      };
    }

    public override void PreStart()
    {
      if (Object.op_Inequality((Object) null, (Object) EventAction_EnableQuit.mQuit) || this.mResource == null)
        return;
      EventQuit eventQuit = EventQuit.Find();
      EventAction_EnableQuit.mQuit = !Object.op_Equality((Object) eventQuit, (Object) null) ? eventQuit : Object.Instantiate(this.mResource.asset) as EventQuit;
      ((Component) EventAction_EnableQuit.mQuit).get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
      ((Component) EventAction_EnableQuit.mQuit).get_transform().SetAsLastSibling();
      // ISSUE: method pointer
      EventAction_EnableQuit.mQuit.OnClick = new UnityAction((object) this, __methodptr(\u003CPreStart\u003Em__170));
      ((Component) EventAction_EnableQuit.mQuit).get_gameObject().SetActive(false);
    }

    private void SkipButtonAction(EventScript.Sequence inEventScriptSequence, GameObject inSkipButtonGameObject)
    {
      GlobalVars.IsSkipQuestDemo = true;
      inEventScriptSequence.GoToEndState();
      inSkipButtonGameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) null, (Object) EventAction_EnableQuit.mQuit))
        return;
      ((Component) EventAction_EnableQuit.mQuit).get_gameObject().SetActive(true);
      ((Component) EventAction_EnableQuit.mQuit).get_transform().SetAsLastSibling();
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      EventAction_EnableQuit.mQuit = (EventQuit) null;
    }
  }
}
