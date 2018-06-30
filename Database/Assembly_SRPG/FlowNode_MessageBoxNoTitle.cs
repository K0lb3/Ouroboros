namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "Closed", 1, 1), NodeType("UI/MessageBoxNoTitle", 0x7fe5), Pin(10, "Open", 0, 0), Pin(11, "ForceClose", 0, 11), Pin(100, "Opened", 1, 100), Pin(0x65, "ForceClosed", 1, 0x65)]
    public class FlowNode_MessageBoxNoTitle : FlowNode
    {
        public string Text;
        public bool systemModal;
        public int systemModalPriority;
        public GameObject parent;
        public string parentName;
        public bool unscaledTime;
        public bool richTag;
        private GameObject winGO;

        public FlowNode_MessageBoxNoTitle()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnActivate>m__1A6(GameObject go)
        {
            if ((this.winGO != null) == null)
            {
                goto Label_0020;
            }
            this.winGO = null;
            base.ActivateOutputLinks(1);
        Label_0020:
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            Animator animator;
            Win_Btn_Decide_Flx flx;
            if (pinID != 10)
            {
                goto Label_00DF;
            }
            if (string.IsNullOrEmpty(this.parentName) != null)
            {
                goto Label_004F;
            }
            this.parent = GameObject.Find(this.parentName);
            if ((this.parent == null) == null)
            {
                goto Label_004F;
            }
            DebugUtility.LogWarning("can not found gameObject:" + this.parentName);
        Label_004F:
            str = LocalizedText.Get(this.Text);
            if (this.richTag == null)
            {
                goto Label_006D;
            }
            str = LocalizedText.ReplaceTag(str);
        Label_006D:
            this.winGO = UIUtility.SystemMessage(str, new UIUtility.DialogResultEvent(this.<OnActivate>m__1A6), this.parent, this.systemModal, this.systemModalPriority);
            if ((this.winGO == null) || (this.unscaledTime == null))
            {
                goto Label_00D1;
            }
            animator = this.winGO.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00D1;
            }
            animator.set_updateMode(2);
        Label_00D1:
            base.ActivateOutputLinks(100);
            goto Label_0140;
        Label_00DF:
            if (pinID != 11)
            {
                goto Label_0140;
            }
            if ((this.winGO == null) == null)
            {
                goto Label_00F9;
            }
            return;
        Label_00F9:
            flx = ((this.winGO == null) == null) ? this.winGO.GetComponent<Win_Btn_Decide_Flx>() : null;
            this.winGO = null;
            if ((flx != null) == null)
            {
                goto Label_0137;
            }
            flx.BeginClose();
            flx = null;
        Label_0137:
            base.ActivateOutputLinks(0x65);
        Label_0140:
            return;
        }
    }
}

