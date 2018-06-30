namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(10, "Open", 0, 0), Pin(1, "Closed", 1, 1), Pin(11, "ForceClose", 0, 11), Pin(100, "Opened", 1, 100), Pin(0x65, "ForceClosed", 1, 0x65), NodeType("UI/Message", 0x7fe5)]
    public class FlowNode_Message : FlowNode
    {
        public string Text;
        public bool systemModal;
        public int systemModalPriority;
        public GameObject parent;
        public string parentName;
        public bool unscaledTime;
        public bool richTag;
        public bool anim;
        public bool input;
        public float bgAlpha;
        public float autoClose;
        private Win_SysMessage_Flx m_Window;

        public FlowNode_Message()
        {
            base..ctor();
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            string str;
            GameSettings settings;
            Canvas canvas;
            Animator animator;
            if (pinID != 10)
            {
                goto Label_0180;
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
            settings = GameSettings.Instance;
            canvas = UIUtility.PushCanvas(this.systemModal, this.systemModalPriority);
            if ((this.parent != null) == null)
            {
                goto Label_00AC;
            }
            canvas.get_transform().SetParent(this.parent.get_transform());
        Label_00AC:
            this.m_Window = Object.Instantiate<Win_SysMessage_Flx>(&settings.Dialogs.SysMsgDialog);
            this.m_Window.get_transform().SetParent(canvas.get_transform(), 0);
            this.m_Window.Text_Message.set_text(str);
            this.m_Window.Initialize(this.input, this.bgAlpha);
            if (this.anim == null)
            {
                goto Label_0117;
            }
            this.m_Window.StartAnim();
        Label_0117:
            if (this.autoClose <= 0f)
            {
                goto Label_0138;
            }
            this.m_Window.AutoClose(this.autoClose);
        Label_0138:
            if (this.m_Window == null)
            {
                goto Label_0172;
            }
            if (this.unscaledTime == null)
            {
                goto Label_0172;
            }
            animator = this.m_Window.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0172;
            }
            animator.set_updateMode(2);
        Label_0172:
            base.ActivateOutputLinks(100);
            goto Label_01C6;
        Label_0180:
            if (pinID != 11)
            {
                goto Label_01C6;
            }
            if ((this.m_Window == null) == null)
            {
                goto Label_019A;
            }
            return;
        Label_019A:
            if ((this.m_Window != null) == null)
            {
                goto Label_01BD;
            }
            this.m_Window.BeginClose();
            this.m_Window = null;
        Label_01BD:
            base.ActivateOutputLinks(0x65);
        Label_01C6:
            return;
        }

        public void Update()
        {
            if ((this.m_Window != null) == null)
            {
                goto Label_0036;
            }
            if ((this.m_Window.get_gameObject() == null) == null)
            {
                goto Label_002F;
            }
            base.ActivateOutputLinks(1);
        Label_002F:
            this.m_Window = null;
        Label_0036:
            return;
        }
    }
}

