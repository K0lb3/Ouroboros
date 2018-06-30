namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [NodeType("UI/MessageBox", 0x7fe5), Pin(10, "Open", 0, 0), Pin(1, "Closed", 1, 1), Pin(11, "ForceClose", 0, 11), Pin(100, "Opened", 1, 100), Pin(0x65, "ForceClosed", 1, 0x65)]
    public class FlowNode_MessageBox : FlowNode
    {
        public string Caption;
        public string Text;
        public bool systemModal;
        public int systemModalPriority;
        public GameObject parent;
        public string parentName;
        public bool unscaledTime;
        public bool richTag;
        private GameObject winGO;

        public FlowNode_MessageBox()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnActivate>m__1A4(GameObject go)
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
            Win_Btn_Decide_Title_Flx flx;
            if (pinID != 10)
            {
                goto Label_00EA;
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
            this.winGO = UIUtility.SystemMessage(LocalizedText.Get(this.Caption), str, new UIUtility.DialogResultEvent(this.<OnActivate>m__1A4), this.parent, this.systemModal, this.systemModalPriority);
            if ((this.winGO == null) || (this.unscaledTime == null))
            {
                goto Label_00DC;
            }
            animator = this.winGO.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00DC;
            }
            animator.set_updateMode(2);
        Label_00DC:
            base.ActivateOutputLinks(100);
            goto Label_014B;
        Label_00EA:
            if (pinID != 11)
            {
                goto Label_014B;
            }
            if ((this.winGO == null) == null)
            {
                goto Label_0104;
            }
            return;
        Label_0104:
            flx = ((this.winGO == null) == null) ? this.winGO.GetComponent<Win_Btn_Decide_Title_Flx>() : null;
            this.winGO = null;
            if ((flx != null) == null)
            {
                goto Label_0142;
            }
            flx.BeginClose();
            flx = null;
        Label_0142:
            base.ActivateOutputLinks(0x65);
        Label_014B:
            return;
        }
    }
}

