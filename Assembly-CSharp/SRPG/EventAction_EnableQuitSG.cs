// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_EnableQuitSG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

namespace SRPG
{
  [EventActionInfo("Forced termination / permission (3D)", "Allow forced termination", 5592405, 4473992)]
  public class EventAction_EnableQuitSG : EventAction
  {
    private static readonly string AssetPath = "UI/BtnSkip_movie";
    public bool Special;
    protected static EventQuit mQuit;
    private LoadRequest mResource;
    public AnalyticsManager.TrackingType AnalyticsTypeToTrackOnQuit;

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
      return (IEnumerator) new EventAction_EnableQuitSG.\u003CPreloadAssets\u003Ec__Iterator3F()
      {
        \u003C\u003Ef__this = this
      };
    }

    public override void PreStart()
    {
      if (Object.op_Inequality((Object) null, (Object) EventAction_EnableQuitSG.mQuit) || this.mResource == null)
        return;
      EventAction_EnableQuitSG.mQuit = Object.Instantiate(this.mResource.asset) as EventQuit;
      ((Component) EventAction_EnableQuitSG.mQuit).get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
      ((Component) EventAction_EnableQuitSG.mQuit).get_transform().SetAsLastSibling();
      // ISSUE: method pointer
      EventAction_EnableQuitSG.mQuit.OnClick = new UnityAction((object) this, __methodptr(\u003CPreStart\u003Em__146));
      ((Component) EventAction_EnableQuitSG.mQuit).get_gameObject().SetActive(false);
    }

    private void SkipButtonAction(bool inIsSpecial, EventScript.Sequence inEventScriptSequence, GameObject inSkipButtonGameObject)
    {
      if (!inIsSpecial)
        GameUtility.FadeOut(2f);
      else
        inEventScriptSequence.GoToEndState();
      inEventScriptSequence.OnQuitImmediate();
      inSkipButtonGameObject.SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) null, (Object) EventAction_EnableQuitSG.mQuit))
        return;
      ((Component) EventAction_EnableQuitSG.mQuit).get_gameObject().SetActive(true);
      ((Component) EventAction_EnableQuitSG.mQuit).get_transform().SetAsLastSibling();
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      EventAction_EnableQuitSG.mQuit = (EventQuit) null;
    }
  }
}
