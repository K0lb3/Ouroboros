// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_CameraStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/カメラ/カメラストリーム", "カメラ制御用のアニメーションを設定します。", 5592405, 4473992)]
  internal class EventAction_CameraStream : EventAction
  {
    public float Near = 0.01f;
    public float Far = 1000f;
    public AnimationClip m_CameraAnime;
    public bool m_Async;
    public bool ScaleToFov;

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.m_CameraAnime, (Object) null))
      {
        Animation animation = (Animation) ((Component) Camera.get_main()).get_gameObject().GetComponent<Animation>();
        if (Object.op_Equality((Object) animation, (Object) null))
          animation = (Animation) ((Component) Camera.get_main()).get_gameObject().AddComponent<Animation>();
        if (Object.op_Inequality((Object) animation, (Object) null))
        {
          animation.AddClip(this.m_CameraAnime, ((Object) this.m_CameraAnime).ToString());
          animation.Play(((Object) this.m_CameraAnime).ToString());
        }
        Camera.get_main().set_nearClipPlane(this.Near);
        Camera.get_main().set_farClipPlane(this.Far);
        if (!this.m_Async)
          return;
        this.ActivateNext(true);
      }
      else
        this.ActivateNext();
    }

    public override void Update()
    {
      if (!Object.op_Inequality((Object) Camera.get_main(), (Object) null))
        return;
      Vector3 localScale = ((Component) Camera.get_main()).get_transform().get_localScale();
      if (this.ScaleToFov)
        Camera.get_main().set_fieldOfView((float) localScale.x);
      Animation component = (Animation) ((Component) Camera.get_main()).get_gameObject().GetComponent<Animation>();
      if (!Object.op_Inequality((Object) component, (Object) null) || component.get_isPlaying())
        return;
      component.Stop();
      if (!this.m_Async)
        this.ActivateNext();
      else
        this.enabled = false;
    }
  }
}
