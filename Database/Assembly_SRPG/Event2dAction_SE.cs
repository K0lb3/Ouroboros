// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [EventActionInfo("SE再生(2D)", "SEを再生します", 5592405, 4473992)]
  public class Event2dAction_SE : EventAction
  {
    public string SE;

    public override void OnActivate()
    {
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SE, 0.0f);
      this.ActivateNext();
    }
  }
}
