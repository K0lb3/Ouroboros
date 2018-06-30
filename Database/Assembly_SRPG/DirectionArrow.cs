namespace SRPG
{
    using System;
    using UnityEngine;

    public class DirectionArrow : MonoBehaviour
    {
        public ArrowStates State;
        public EUnitDirection Direction;
        private Animator mAnimator;
        [HelpBox("方向の選択状態にあわせてAnimatorのStateNameを変更します (0=Normal,1=Press,2=Hilit,3=Close)。矢印はアニメーションが停止したら破棄されるので、PressとClose状態以外はループアニメーションにしてください。")]
        public string StateName;

        public DirectionArrow()
        {
            this.StateName = "state";
            base..ctor();
            return;
        }

        private void Start()
        {
            this.mAnimator = base.GetComponent<Animator>();
            return;
        }

        private unsafe void Update()
        {
            AnimatorStateInfo info;
            AnimatorStateInfo info2;
            if ((this.mAnimator == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mAnimator.SetInteger(this.StateName, this.State);
            if (&this.mAnimator.GetCurrentAnimatorStateInfo(0).get_loop() != null)
            {
                goto Label_007C;
            }
            if (&this.mAnimator.GetCurrentAnimatorStateInfo(0).get_normalizedTime() < 1f)
            {
                goto Label_007C;
            }
            if (this.mAnimator.IsInTransition(0) != null)
            {
                goto Label_007C;
            }
            Object.Destroy(base.get_gameObject());
        Label_007C:
            return;
        }

        public enum ArrowStates
        {
            Normal,
            Press,
            Hilit,
            Close
        }
    }
}

