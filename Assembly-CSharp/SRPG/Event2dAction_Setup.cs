// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_Setup
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("初期化", "2Dデモの初期化を行います", 5592405, 4473992)]
  public class Event2dAction_Setup : EventAction
  {
    public override void OnActivate()
    {
      ((Component) this.ActiveCanvas).get_gameObject().AddComponent<UIZSort>();
      GameUtility.FadeIn(1f);
    }

    public override void Update()
    {
      if (GameUtility.IsScreenFading)
        return;
      this.ActivateNext();
    }
  }
}
