namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [NodeType("System/CheckTutrial"), Pin(0, "CheckTutrial", 0, 0), Pin(3, "Skip", 1, 3), Pin(2, "No", 1, 2), Pin(1, "Yes", 1, 1)]
    public class FlowNode_CheckTutrial : FlowNode
    {
        public FlowNode_CheckTutrial()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            if (pinID != null)
            {
                goto Label_0084;
            }
            if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) != null)
            {
                goto Label_002C;
            }
            if (GlobalVars.BtlID == null)
            {
                goto Label_0035;
            }
        Label_002C:
            base.ActivateOutputLinks(3);
            return;
        Label_0035:
            manager = MonoSingleton<GameManager>.Instance;
            manager.UpdateTutorialStep();
            if (manager.TutorialStep != null)
            {
                goto Label_0084;
            }
            if (GameUtility.IsDebugBuild == null)
            {
                goto Label_0084;
            }
            UIUtility.ConfirmBox(LocalizedText.Get("sys.PLAYTUTORIAL"), new UIUtility.DialogResultEvent(this.OnPlayTutorial), new UIUtility.DialogResultEvent(this.OnSkipTutorial), null, 0, -1, null, null);
            return;
        Label_0084:
            base.ActivateOutputLinks(3);
            return;
        }

        private void OnPlayTutorial(GameObject go)
        {
            GlobalVars.DebugIsPlayTutorial = 1;
            base.ActivateOutputLinks(1);
            return;
        }

        private void OnSkipTutorial(GameObject go)
        {
            GlobalVars.DebugIsPlayTutorial = 0;
            base.ActivateOutputLinks(2);
            return;
        }

        private void Start()
        {
        }
    }
}

