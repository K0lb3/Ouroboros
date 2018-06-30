namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [NodeType("FgGID/FgGBtn", 0x7fe5), Pin(10, "Input", 0, 0), Pin(1, "Output", 1, 1)]
    public class FlowNode_FgGBtn : FlowNode
    {
        [ShowInInfo]
        public string ParameterName;
        [ShowInInfo, DropTarget(typeof(GameObject), true)]
        public GameObject Target;
        public bool UpdateAnimator;

        public FlowNode_FgGBtn()
        {
            this.ParameterName = "authStatus";
            base..ctor();
            return;
        }

        public override string GetCaption()
        {
            return (base.GetCaption() + ":" + this.ParameterName);
        }

        public override void OnActivate(int pinID)
        {
            GameObject obj2;
            Animator animator;
            ReqFgGAuth.eAuthStatus status;
            obj2 = ((this.Target != null) == null) ? base.get_gameObject() : this.Target;
            animator = obj2.GetComponent<Animator>();
            switch ((MonoSingleton<GameManager>.Instance.AuthStatus - 1))
            {
                case 0:
                    goto Label_004E;

                case 1:
                    goto Label_005F;

                case 2:
                    goto Label_0093;
            }
            goto Label_00C7;
        Label_004E:
            this.Target.SetActive(0);
            goto Label_00D8;
        Label_005F:
            if ((animator != null) == null)
            {
                goto Label_00D8;
            }
            animator.SetInteger(this.ParameterName, 2);
            if (this.UpdateAnimator == null)
            {
                goto Label_00D8;
            }
            animator.Update(0f);
            goto Label_00D8;
        Label_0093:
            if ((animator != null) == null)
            {
                goto Label_00D8;
            }
            animator.SetInteger(this.ParameterName, 3);
            if (this.UpdateAnimator == null)
            {
                goto Label_00D8;
            }
            animator.Update(0f);
            goto Label_00D8;
        Label_00C7:
            this.Target.SetActive(0);
        Label_00D8:
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

