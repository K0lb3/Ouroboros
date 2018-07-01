// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SetCameraBG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
