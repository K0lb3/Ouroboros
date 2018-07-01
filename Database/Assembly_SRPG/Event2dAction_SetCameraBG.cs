// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SetCameraBG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("カメラ/背景イメージを変更(2D)", "カメラの背景イメージを変更します", 5592405, 4473992)]
  public class Event2dAction_SetCameraBG : EventAction
  {
    [HideInInspector]
    public Texture2D BackgroundImage;

    public override void OnActivate()
    {
      this.ActivateNext();
    }
  }
}
