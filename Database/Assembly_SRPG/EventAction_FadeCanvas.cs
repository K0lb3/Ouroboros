namespace SRPG
{
    using System;
    using UnityEngine;

    [EventActionInfo("オブジェクト/キャンバスフェード", "Canvasをフェードさせます", 0x555555, 0x444488)]
    public class EventAction_FadeCanvas : EventAction
    {
        public AnimationCurve Curve;
        public string CanvasID;
        private CanvasGroup[] mCanvasGroup;
        private float mTime;
        public float Time;

        public unsafe EventAction_FadeCanvas()
        {
            Keyframe[] keyframeArray1;
            keyframeArray1 = new Keyframe[2];
            *(&(keyframeArray1[0])) = new Keyframe(0f, 0f);
            *(&(keyframeArray1[1])) = new Keyframe(1f, 1f);
            this.Curve = new AnimationCurve(keyframeArray1);
            this.Time = 1f;
            base..ctor();
            return;
        }

        public override void GoToEndState()
        {
            this.mTime = this.Time;
            return;
        }

        public override void OnActivate()
        {
            GameObject[] objArray;
            int num;
            objArray = GameObjectID.FindGameObjects(this.CanvasID);
            if (((int) objArray.Length) <= 0)
            {
                goto Label_004C;
            }
            this.mCanvasGroup = new CanvasGroup[(int) objArray.Length];
            num = 0;
            goto Label_003E;
        Label_002A:
            this.mCanvasGroup[num] = GameUtility.RequireComponent<CanvasGroup>(objArray[num]);
            num += 1;
        Label_003E:
            if (num < ((int) objArray.Length))
            {
                goto Label_002A;
            }
            goto Label_0053;
        Label_004C:
            base.ActivateNext();
            return;
        Label_0053:
            return;
        }

        public override void SkipImmediate()
        {
            this.mTime = this.Time;
            return;
        }

        public override unsafe void Update()
        {
            int num;
            float num2;
            Keyframe keyframe;
            Keyframe keyframe2;
            this.mTime += UnityEngine.Time.get_deltaTime();
            num = 0;
            goto Label_00C3;
        Label_0019:
            if ((this.mCanvasGroup[num] != null) == null)
            {
                goto Label_00BF;
            }
            if (this.Time <= 0f)
            {
                goto Label_0080;
            }
            num2 = this.Curve.Evaluate(Mathf.Clamp01(this.mTime / this.Time) * &this.Curve.get_Item(this.Curve.get_length() - 1).get_time());
            goto Label_00AC;
        Label_0080:
            num2 = this.Curve.Evaluate(&this.Curve.get_Item(this.Curve.get_length() - 1).get_time());
        Label_00AC:
            this.mCanvasGroup[num].set_alpha(Mathf.Clamp01(num2));
        Label_00BF:
            num += 1;
        Label_00C3:
            if (num < ((int) this.mCanvasGroup.Length))
            {
                goto Label_0019;
            }
            if (this.mTime < this.Time)
            {
                goto Label_00E8;
            }
            base.ActivateNext();
        Label_00E8:
            return;
        }
    }
}

