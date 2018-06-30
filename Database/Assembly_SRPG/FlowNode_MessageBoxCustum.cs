namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(10, "Open", 0, 0), NodeType("UI/MessageBoxCustum", 0x7fe5), Pin(1, "Closed", 1, 1), Pin(11, "ForceClose", 0, 11), Pin(100, "Opened", 1, 100), Pin(0x65, "ForceClosed", 1, 0x65)]
    public class FlowNode_MessageBoxCustum : FlowNode
    {
        [StringIsResourcePath(typeof(GameObject))]
        public string ResourcePath;
        public string Caption;
        public string Text;
        public bool systemModal;
        public int systemModalPriority;
        public GameObject parent;
        public string parentName;
        public bool unscaledTime;
        public bool richTag;
        private GameObject winGO;

        public FlowNode_MessageBoxCustum()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnActivate>m__1A5(GameObject go)
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

        private GameObject CreatePrefab(string resource_path, string title, string msg, UIUtility.DialogResultEvent ok_event_listener, GameObject go_parent, bool system_modal, int system_modal_priority)
        {
            Canvas canvas;
            GameObject obj2;
            GameObject obj3;
            Win_Btn_Decide_Title_Flx flx;
            canvas = UIUtility.PushCanvas(system_modal, system_modal_priority);
            if ((go_parent != null) == null)
            {
                goto Label_0029;
            }
            canvas.get_transform().SetParent(go_parent.get_transform());
        Label_0029:
            obj2 = AssetManager.Load<GameObject>(resource_path);
            if (obj2 != null)
            {
                goto Label_0052;
            }
            Debug.LogError("FlowNode_MessageBoxCustum/Load failed. '" + resource_path + "'");
            return null;
        Label_0052:
            obj3 = Object.Instantiate<GameObject>(obj2);
            if (obj3 != null)
            {
                goto Label_007B;
            }
            Debug.LogError("FlowNode_MessageBoxCustum/Instantiate failed. '" + resource_path + "'");
            return null;
        Label_007B:
            flx = obj3.GetComponent<Win_Btn_Decide_Title_Flx>();
            if (flx != null)
            {
                goto Label_0099;
            }
            Debug.LogError("FlowNode_MessageBoxCustum/Component not attached. 'Win_Btn_Decide_Title_Flx'");
            return null;
        Label_0099:
            flx.get_transform().SetParent(canvas.get_transform(), 0);
            flx.Text_Title.set_text(title);
            flx.Text_Message.set_text(msg);
            flx.OnClickYes = ok_event_listener;
            return flx.get_gameObject();
        }

        public override void OnActivate(int pinID)
        {
            string str;
            Animator animator;
            Win_Btn_Decide_Title_Flx flx;
            if (pinID != 10)
            {
                goto Label_00F1;
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
            this.winGO = this.CreatePrefab(this.ResourcePath, LocalizedText.Get(this.Caption), str, new UIUtility.DialogResultEvent(this.<OnActivate>m__1A5), this.parent, this.systemModal, this.systemModalPriority);
            if ((this.winGO == null) || (this.unscaledTime == null))
            {
                goto Label_00E3;
            }
            animator = this.winGO.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00E3;
            }
            animator.set_updateMode(2);
        Label_00E3:
            base.ActivateOutputLinks(100);
            goto Label_0152;
        Label_00F1:
            if (pinID != 11)
            {
                goto Label_0152;
            }
            if ((this.winGO == null) == null)
            {
                goto Label_010B;
            }
            return;
        Label_010B:
            flx = ((this.winGO == null) == null) ? this.winGO.GetComponent<Win_Btn_Decide_Title_Flx>() : null;
            this.winGO = null;
            if ((flx != null) == null)
            {
                goto Label_0149;
            }
            flx.BeginClose();
            flx = null;
        Label_0149:
            base.ActivateOutputLinks(0x65);
        Label_0152:
            return;
        }
    }
}

