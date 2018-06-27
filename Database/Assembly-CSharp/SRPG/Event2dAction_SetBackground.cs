// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SetBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("背景/配置(2D)", "背景を配置します", 5592405, 4473992)]
  public class Event2dAction_SetBackground : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/EventBackGround";
    [HideInInspector]
    public Texture2D Background;
    [HideInInspector]
    public EventBackGround mBackGround;
    private LoadRequest mBackGroundResource;

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
      return (IEnumerator) new Event2dAction_SetBackground.\u003CPreloadAssets\u003Ec__IteratorB3() { \u003C\u003Ef__this = this };
    }

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mBackGround, (Object) null))
        return;
      this.mBackGround = EventBackGround.Find();
      if (!Object.op_Equality((Object) this.mBackGround, (Object) null) || this.mBackGroundResource == null)
        return;
      this.mBackGround = Object.Instantiate(this.mBackGroundResource.asset) as EventBackGround;
      ((Component) this.mBackGround).get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
      ((Component) this.mBackGround).get_transform().SetAsFirstSibling();
      ((Component) this.mBackGround).get_gameObject().SetActive(false);
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mBackGround, (Object) null) && !((Component) this.mBackGround).get_gameObject().get_activeInHierarchy())
        ((Component) this.mBackGround).get_gameObject().SetActive(true);
      if (Object.op_Inequality((Object) this.mBackGround, (Object) null) || Object.op_Inequality((Object) ((RawImage) ((Component) this.mBackGround).get_gameObject().GetComponent<RawImage>()).get_texture(), (Object) this.Background))
        ((RawImage) ((Component) this.mBackGround).get_gameObject().GetComponent<RawImage>()).set_texture((Texture) this.Background);
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mBackGround, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mBackGround).get_gameObject());
    }
  }
}
