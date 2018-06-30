namespace SRPG
{
    using System;
    using UnityEngine;

    public class ConceptCardIconBattleResult : ConceptCardIcon
    {
        [SerializeField]
        private GameObject UnitBody;
        [SerializeField]
        private GameObject BlackCover;
        [SerializeField]
        private GameObject ConceptCardBody;
        [SerializeField]
        private Animator TrustUpAnimator;

        public ConceptCardIconBattleResult()
        {
            base..ctor();
            return;
        }

        public void ShowAnimationAfter()
        {
            bool flag;
            flag = (base.ConceptCard == null) == 0;
            if ((this.UnitBody != null) == null)
            {
                goto Label_002D;
            }
            this.UnitBody.SetActive(flag == 0);
        Label_002D:
            if ((this.BlackCover != null) == null)
            {
                goto Label_004D;
            }
            this.BlackCover.SetActive(flag == 0);
        Label_004D:
            return;
        }

        public void ShowStartAnimation(bool isTrustUp)
        {
            bool flag;
            Animator animator;
            flag = (base.ConceptCard == null) == 0;
            if ((this.ConceptCardBody != null) == null)
            {
                goto Label_0069;
            }
            this.ConceptCardBody.SetActive(flag);
            if (flag == null)
            {
                goto Label_0069;
            }
            animator = this.ConceptCardBody.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0069;
            }
            if (isTrustUp == null)
            {
                goto Label_005E;
            }
            animator.SetTrigger("up");
            goto Label_0069;
        Label_005E:
            animator.SetTrigger("open");
        Label_0069:
            return;
        }

        public void StartTrustUpAnimation()
        {
            if ((this.TrustUpAnimator == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.TrustUpAnimator.Play("up");
            return;
        }
    }
}

