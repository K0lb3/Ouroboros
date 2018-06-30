namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("オブジェクト/アニメーション (レガシー)", "オブジェクトにアタッチされたアニメーションを再生します。", 0x555555, 0x444488)]
    public class EventAction_AnimateObject : EventAction
    {
        public string ObjectID;
        [HideInInspector]
        public string AnimationID;

        public EventAction_AnimateObject()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            GameObject[] objArray;
            int num;
            Animation animation;
            int num2;
            Animation animation2;
            objArray = GameObjectID.FindGameObjects(this.ObjectID);
            num = 0;
            goto Label_0046;
        Label_0013:
            if ((objArray[num] != null) == null)
            {
                goto Label_0042;
            }
            animation = objArray[num].GetComponent<Animation>();
            if ((animation != null) == null)
            {
                goto Label_0042;
            }
            PlayAnimation(animation, this.AnimationID);
        Label_0042:
            num += 1;
        Label_0046:
            if (num < ((int) objArray.Length))
            {
                goto Label_0013;
            }
            num2 = 0;
            goto Label_00CE;
        Label_0056:
            if ((base.Sequence.SpawnedObjects[num2] != null) == null)
            {
                goto Label_00CA;
            }
            if ((base.Sequence.SpawnedObjects[num2].get_name() == this.ObjectID) == null)
            {
                goto Label_00CA;
            }
            animation2 = base.Sequence.SpawnedObjects[num2].GetComponent<Animation>();
            if ((animation2 != null) == null)
            {
                goto Label_00CA;
            }
            PlayAnimation(animation2, this.AnimationID);
        Label_00CA:
            num2 += 1;
        Label_00CE:
            if (num2 < base.Sequence.SpawnedObjects.Count)
            {
                goto Label_0056;
            }
            base.ActivateNext();
            return;
        }

        private static void PlayAnimation(Animation animation, string animationID)
        {
            AnimationClip clip;
            clip = animation.GetClip(animationID);
            if ((clip != null) == null)
            {
                goto Label_001B;
            }
            animation.set_clip(clip);
        Label_001B:
            animation.Play(animationID);
            return;
        }
    }
}

