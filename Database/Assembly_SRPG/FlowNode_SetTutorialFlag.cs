namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [NodeType("Tutorial/SetTutorialFlag", 0x7fe5), Pin(1, "Set", 0, 0), Pin(12, "False", 1, 0x16), Pin(11, "True", 1, 0x15), Pin(10, "Test", 0, 20), Pin(5, "Flag Unchanged", 1, 3), Pin(3, "Flag Changed (No)", 1, 2), Pin(2, "Flag Changed (Yes)", 1, 1)]
    public class FlowNode_SetTutorialFlag : FlowNode
    {
        private const int PIN_ID_SET = 1;
        private const int PIN_ID_UPDATE1 = 2;
        private const int PIN_ID_UPDATE2 = 3;
        private const int PIN_ID_NOUPDATE = 5;
        private const int PIN_ID_TEST = 10;
        private const int PIN_ID_TRUE = 11;
        private const int PIN_ID_FALSE = 12;
        public TutorialFlags mTutorialFlags;
        public string FlagID;
        public string ConfirmText;

        public FlowNode_SetTutorialFlag()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0078;
            }
            if (MonoSingleton<GameManager>.Instance.IsTutorialFlagSet(this.FlagID) == null)
            {
                goto Label_0029;
            }
            base.ActivateOutputLinks(5);
            goto Label_0073;
        Label_0029:
            if (string.IsNullOrEmpty(this.ConfirmText) != null)
            {
                goto Label_006C;
            }
            UIUtility.ConfirmBox(LocalizedText.Get(this.ConfirmText), new UIUtility.DialogResultEvent(this.OnYes), new UIUtility.DialogResultEvent(this.OnNo), null, 1, -1, null, null);
            goto Label_0073;
        Label_006C:
            this.OnYes(null);
        Label_0073:
            goto Label_00AC;
        Label_0078:
            if (pinID != 10)
            {
                goto Label_00AC;
            }
            if (MonoSingleton<GameManager>.Instance.IsTutorialFlagSet(this.FlagID) == null)
            {
                goto Label_00A3;
            }
            base.ActivateOutputLinks(11);
            goto Label_00AC;
        Label_00A3:
            base.ActivateOutputLinks(12);
        Label_00AC:
            return;
        }

        private void OnNo(GameObject go)
        {
            MonoSingleton<GameManager>.Instance.UpdateTutorialFlags(this.FlagID);
            base.ActivateOutputLinks(3);
            return;
        }

        private void OnYes(GameObject go)
        {
            MonoSingleton<GameManager>.Instance.UpdateTutorialFlags(this.FlagID);
            base.ActivateOutputLinks(2);
            return;
        }
    }
}

