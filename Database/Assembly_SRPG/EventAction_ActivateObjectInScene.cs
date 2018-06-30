namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [EventActionInfo("New/オブジェクト/シーン内オブジェクトを表示", "シーン内のオブジェクトを表示・非表示します", 0x555555, 0x444488)]
    public class EventAction_ActivateObjectInScene : EventAction
    {
        public VisibleType visibleType;
        public string objectName;
        public Vector3 objectPosition;

        public EventAction_ActivateObjectInScene()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate()
        {
            TacticsSceneSettings settings;
            List<Transform> list;
            float num;
            Transform transform;
            Transform[] transformArray;
            int num2;
            float num3;
            Vector3 vector;
            if (string.IsNullOrEmpty(this.objectName) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            settings = TacticsSceneSettings.LastScene;
            if ((settings == null) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            list = new List<Transform>();
            num = (float) 1.0 / (float) 0.0;
            transform = null;
            transformArray = settings.get_gameObject().GetComponentsInChildren<Transform>(1);
            num2 = 0;
            goto Label_00AE;
        Label_0048:
            if ((transformArray[num2].get_name() == this.objectName) == null)
            {
                goto Label_00A8;
            }
            Debug.Log("find");
            list.Add(transformArray[num2]);
            vector = transformArray[num2].get_position() - this.objectPosition;
            num3 = &vector.get_sqrMagnitude();
            if (num3 >= num)
            {
                goto Label_00A8;
            }
            transform = transformArray[num2];
            num = num3;
        Label_00A8:
            num2 += 1;
        Label_00AE:
            if (num2 < ((int) transformArray.Length))
            {
                goto Label_0048;
            }
            if ((transform != null) == null)
            {
                goto Label_00D9;
            }
            transform.get_gameObject().SetActive(this.visibleType == 0);
        Label_00D9:
            base.ActivateNext();
            return;
        }

        public enum VisibleType
        {
            Visible,
            Invisible
        }
    }
}

