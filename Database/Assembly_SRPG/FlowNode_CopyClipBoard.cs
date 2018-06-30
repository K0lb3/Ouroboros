namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(2, "失敗", 1, 2), Pin(1, "成功", 1, 1), Pin(0, "コピー", 0, 0), NodeType("System/CopyClipBoard", 0x7fe5)]
    public class FlowNode_CopyClipBoard : FlowNode
    {
        [SerializeField]
        private Text Target;
        public string LocalizeText;

        public FlowNode_CopyClipBoard()
        {
            base..ctor();
            return;
        }

        private bool CopyFrom(string text)
        {
            if (string.IsNullOrEmpty(text) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            text = text.Replace("<br>", "\n");
            GUIUtility.set_systemCopyBuffer(text);
            return 1;
        }

        private bool CopyFrom(Text target)
        {
            if ((target == null) == null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            return this.CopyFrom(target.get_text());
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_006C;
            }
            if (string.IsNullOrEmpty(this.LocalizeText) == null)
            {
                goto Label_0041;
            }
            if (this.CopyFrom(this.Target) == null)
            {
                goto Label_0034;
            }
            base.ActivateOutputLinks(1);
            goto Label_003C;
        Label_0034:
            base.ActivateOutputLinks(2);
        Label_003C:
            goto Label_006C;
        Label_0041:
            if (this.CopyFrom(LocalizedText.Get(this.LocalizeText)) == null)
            {
                goto Label_0064;
            }
            base.ActivateOutputLinks(1);
            goto Label_006C;
        Label_0064:
            base.ActivateOutputLinks(2);
        Label_006C:
            return;
        }
    }
}

