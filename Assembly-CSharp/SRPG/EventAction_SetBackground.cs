// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SetBackground
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("カメラ/背景イメージを変更", "カメラの背景イメージを変更します", 5592405, 4473992)]
  public class EventAction_SetBackground : EventAction
  {
    [HideInInspector]
    public Texture2D BackgroundImage;

    public override void OnActivate()
    {
      RenderPipeline renderPipeline = GameUtility.RequireComponent<RenderPipeline>(((Component) Camera.get_main()).get_gameObject());
      if (Object.op_Inequality((Object) this.BackgroundImage, (Object) null))
        renderPipeline.BackgroundImage = (Texture) this.BackgroundImage;
      else if (Object.op_Inequality((Object) TacticsSceneSettings.Instance, (Object) null))
        renderPipeline.BackgroundImage = (Texture) TacticsSceneSettings.Instance.BackgroundImage;
      this.ActivateNext();
    }
  }
}
