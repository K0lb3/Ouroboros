namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("New/カメラ/カメラストリーム", "カメラ制御用のアニメーションを設定します。", 0x555555, 0x444488)]
    internal class EventAction_CameraStream : EventAction
    {
        public AnimationClip m_CameraAnime;
        public bool m_Async;
        public float Near;
        public float Far;
        public bool ScaleToFov;

        public EventAction_CameraStream()
        {
            this.Near = 0.01f;
            this.Far = 1000f;
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            Animation animation;
            if ((this.m_CameraAnime != null) == null)
            {
                goto Label_00A5;
            }
            animation = Camera.get_main().get_gameObject().GetComponent<Animation>();
            if ((animation == null) == null)
            {
                goto Label_003D;
            }
            animation = Camera.get_main().get_gameObject().AddComponent<Animation>();
        Label_003D:
            if ((animation != null) == null)
            {
                goto Label_0072;
            }
            animation.AddClip(this.m_CameraAnime, this.m_CameraAnime.ToString());
            animation.Play(this.m_CameraAnime.ToString());
        Label_0072:
            Camera.get_main().set_nearClipPlane(this.Near);
            Camera.get_main().set_farClipPlane(this.Far);
            if (this.m_Async == null)
            {
                goto Label_00A4;
            }
            base.ActivateNext(1);
        Label_00A4:
            return;
        Label_00A5:
            base.ActivateNext();
            return;
        }

        public override unsafe void Update()
        {
            Vector3 vector;
            Animation animation;
            if ((Camera.get_main() != null) == null)
            {
                goto Label_0086;
            }
            vector = Camera.get_main().get_transform().get_localScale();
            if (this.ScaleToFov == null)
            {
                goto Label_003C;
            }
            Camera.get_main().set_fieldOfView(&vector.x);
        Label_003C:
            animation = Camera.get_main().get_gameObject().GetComponent<Animation>();
            if ((animation != null) == null)
            {
                goto Label_0086;
            }
            if (animation.get_isPlaying() != null)
            {
                goto Label_0086;
            }
            animation.Stop();
            if (this.m_Async != null)
            {
                goto Label_007F;
            }
            base.ActivateNext();
            goto Label_0086;
        Label_007F:
            base.enabled = 0;
        Label_0086:
            return;
        }
    }
}

