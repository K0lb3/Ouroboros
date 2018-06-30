namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [EventActionInfo("カメラ/調整", "指定したアクターが画面内に収まるようにカメラ位置を調整します。", 0x555555, 0x444488)]
    public class EventAction_AdjustCameraPosition : EventAction
    {
        public CameraInterpSpeed InterpSpeed;
        [SerializeField]
        private string[] ActorIDs;

        public EventAction_AdjustCameraPosition()
        {
            this.ActorIDs = new string[1];
            base..ctor();
            return;
        }

        public override unsafe void OnActivate()
        {
            Vector3 vector;
            List<GameObject> list;
            int num;
            GameObject obj2;
            Camera camera;
            Transform transform;
            vector = Vector3.get_zero();
            list = new List<GameObject>();
            num = 0;
            goto Label_004F;
        Label_0013:
            obj2 = EventAction.FindActor(this.ActorIDs[num]);
            if ((obj2 == null) == null)
            {
                goto Label_0032;
            }
            goto Label_004B;
        Label_0032:
            vector += obj2.get_transform().get_position();
            list.Add(obj2);
        Label_004B:
            num += 1;
        Label_004F:
            if (num < ((int) this.ActorIDs.Length))
            {
                goto Label_0013;
            }
            if (list.Count > 0)
            {
                goto Label_0070;
            }
            base.ActivateNext();
            return;
        Label_0070:
            vector *= 1f / ((float) list.Count);
            camera = Camera.get_main();
            transform = camera.get_transform();
            &vector.y += GameSettings.Instance.GameCamera_UnitHeightOffset;
            vector -= camera.get_transform().get_forward() * GameSettings.Instance.GameCamera_EventCameraDistance;
            ObjectAnimator.Get(camera).AnimateTo(vector, transform.get_rotation(), SRPG_Extensions.ToSpan(this.InterpSpeed), 3);
            return;
        }

        public override void Update()
        {
            if (ObjectAnimator.Get(Camera.get_main()).isMoving != null)
            {
                goto Label_001B;
            }
            base.ActivateNext();
            return;
        Label_001B:
            return;
        }
    }
}

