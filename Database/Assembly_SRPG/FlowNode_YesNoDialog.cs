namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(1, "Yes", 1, 1), Pin(0x65, "ForceClosed", 1, 0x65), Pin(11, "ForceClose", 0, 11), Pin(2, "No", 1, 2), Pin(100, "Opened", 1, 100), Pin(10, "Open", 0, 0), NodeType("UI/YesNoDialog", 0x7fe5)]
    public class FlowNode_YesNoDialog : FlowNode
    {
        public string Title;
        public string Text;
        public bool systemModal;
        public int systemModalPriority;
        public GameObject parent;
        public string parentName;
        public bool richTag;
        public bool unscaledTime;
        public string yesText;
        public string noText;
        private GameObject winGO;

        public FlowNode_YesNoDialog()
        {
            base..ctor();
            return;
        }

        public override string[] GetInfoLines()
        {
            string[] textArray1;
            if (string.IsNullOrEmpty(this.Text) != null)
            {
                goto Label_002A;
            }
            textArray1 = new string[] { "Text is " + this.Text };
            return textArray1;
        Label_002A:
            return base.GetInfoLines();
        }

        public override void OnActivate(int pinID)
        {
            string str;
            string str2;
            string str3;
            string str4;
            Animator animator;
            Win_Btn_DecideCancel_FL_C l_fl_c;
            Win_Btn_YN_Title_Flx flx;
            if (pinID != 10)
            {
                goto Label_0185;
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
            str2 = (string.IsNullOrEmpty(this.yesText) == null) ? this.yesText : null;
            str3 = (string.IsNullOrEmpty(this.noText) == null) ? this.noText : null;
            if (string.IsNullOrEmpty(this.Title) == null)
            {
                goto Label_00F4;
            }
            this.winGO = UIUtility.ConfirmBox(str, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel), this.parent, this.systemModal, this.systemModalPriority, str2, str3);
            goto Label_0139;
        Label_00F4:
            str4 = LocalizedText.Get(this.Title);
            this.winGO = UIUtility.ConfirmBoxTitle(str4, str, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel), this.parent, this.systemModal, this.systemModalPriority, str2, str3);
        Label_0139:
            if (((this.winGO != null) == null) || (this.unscaledTime == null))
            {
                goto Label_0177;
            }
            animator = this.winGO.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0177;
            }
            animator.set_updateMode(2);
        Label_0177:
            base.ActivateOutputLinks(100);
            goto Label_0241;
        Label_0185:
            if (pinID != 11)
            {
                goto Label_0241;
            }
            if ((this.winGO == null) == null)
            {
                goto Label_019F;
            }
            return;
        Label_019F:
            if (string.IsNullOrEmpty(this.Title) == null)
            {
                goto Label_01F6;
            }
            l_fl_c = ((this.winGO == null) == null) ? this.winGO.GetComponent<Win_Btn_DecideCancel_FL_C>() : null;
            this.winGO = null;
            if ((l_fl_c != null) == null)
            {
                goto Label_0238;
            }
            l_fl_c.BeginClose();
            l_fl_c = null;
            goto Label_0238;
        Label_01F6:
            flx = ((this.winGO == null) == null) ? this.winGO.GetComponent<Win_Btn_YN_Title_Flx>() : null;
            this.winGO = null;
            if ((flx != null) == null)
            {
                goto Label_0238;
            }
            flx.BeginClose();
            flx = null;
        Label_0238:
            base.ActivateOutputLinks(0x65);
        Label_0241:
            return;
        }

        private void OnClickCancel(GameObject go)
        {
            if ((this.winGO == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.winGO = null;
            base.ActivateOutputLinks(2);
            return;
        }

        private void OnClickOK(GameObject go)
        {
            if ((this.winGO == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.winGO = null;
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

