namespace SRPG
{
    using GR;
    using System;

    [Pin(3, "False", 1, 0), Pin(1, "CheckFlag", 0, 0), NodeType("Tutorial/CheckTutorialFlag", 0x7fe5), Pin(2, "True", 1, 0)]
    public class FlowNode_CheckTutorialFlag : FlowNode
    {
        private const int PIN_ID_IN = 1;
        private const int PIN_ID_TRUE = 2;
        private const int PIN_ID_FALSE = 3;
        public TutorialFlags mTutorialFlags;
        public string FlagID;

        public FlowNode_CheckTutorialFlag()
        {
            base..ctor();
            return;
        }

        private bool CheckFlag(TutorialFlags flag)
        {
            if (((long) this.mTutorialFlags) != null)
            {
                goto Label_001D;
            }
            return MonoSingleton<GameManager>.Instance.IsTutorialFlagSet(this.FlagID);
        Label_001D:
            return (((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & ((long) flag)) == 0L) == 0);
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_002D;
            }
            if (this.CheckFlag(this.mTutorialFlags) == null)
            {
                goto Label_0025;
            }
            base.ActivateOutputLinks(2);
            goto Label_002D;
        Label_0025:
            base.ActivateOutputLinks(3);
        Label_002D:
            return;
        }
    }
}

