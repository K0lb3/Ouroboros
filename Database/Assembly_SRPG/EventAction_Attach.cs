namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("アタッチデタッチ", "指定オブジェクトを別オブジェクトにアタッチ/デタッチします。", 0x555555, 0x444488)]
    public class EventAction_Attach : EventAction
    {
        public bool Detach;
        public string AttachmentID;
        [HideInInspector]
        public string TargetID;
        [HideInInspector]
        public string BoneName;

        public EventAction_Attach()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            GameObject obj2;
            GameObject obj3;
            Transform transform;
            DefaultParentReference reference;
            DefaultParentReference reference2;
            obj2 = EventAction.FindActor(this.AttachmentID);
            obj3 = EventAction.FindActor(this.TargetID);
            if ((obj2 == null) == null)
            {
                goto Label_0039;
            }
            Debug.LogError(this.AttachmentID + "は存在しません。");
        Label_0039:
            if (this.Detach != null)
            {
                goto Label_00C6;
            }
            if ((obj3 == null) == null)
            {
                goto Label_006A;
            }
            Debug.LogError(this.TargetID + "は存在しません。");
            goto Label_00C6;
        Label_006A:
            if (string.IsNullOrEmpty(this.BoneName) != null)
            {
                goto Label_00C6;
            }
            transform = GameUtility.findChildRecursively(obj3.get_transform(), this.BoneName);
            if ((transform == null) == null)
            {
                goto Label_00BF;
            }
            obj3 = null;
            Debug.LogError(this.TargetID + "の子供に" + this.BoneName + "は存在しません。");
            goto Label_00C6;
        Label_00BF:
            obj3 = transform.get_gameObject();
        Label_00C6:
            if (this.Detach == null)
            {
                goto Label_010D;
            }
            if ((obj2 != null) == null)
            {
                goto Label_016B;
            }
            reference = obj2.GetComponent<DefaultParentReference>();
            if ((reference != null) == null)
            {
                goto Label_016B;
            }
            obj2.get_transform().SetParent(reference.DefaultParent, 1);
            Object.DestroyImmediate(reference);
            goto Label_016B;
        Label_010D:
            if ((obj2 != null) == null)
            {
                goto Label_016B;
            }
            if ((obj3 != null) == null)
            {
                goto Label_016B;
            }
            if ((obj2.GetComponent<DefaultParentReference>() == null) == null)
            {
                goto Label_0159;
            }
            obj2.get_gameObject().AddComponent<DefaultParentReference>().DefaultParent = obj2.get_transform().get_parent();
        Label_0159:
            obj2.get_transform().SetParent(obj3.get_transform(), 0);
        Label_016B:
            base.ActivateNext();
            return;
        }
    }
}

