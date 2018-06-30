namespace SRPG
{
    using System;

    [Pin(100, "Out", 1, 100), NodeType("Battle/Pause", 0x7fe5), Pin(1, "再開", 0, 1), Pin(0, "一時停止", 0, 0)]
    public class FlowNode_BattlePause : FlowNode
    {
        public FlowNode_BattlePause()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (this.IsPauseAllowed == null)
            {
                goto Label_001B;
            }
            if ((SceneBattle.Instance == null) == null)
            {
                goto Label_002F;
            }
        Label_001B:
            DebugUtility.Log("=== BattlePause => OutputLinks(100) ===");
            base.ActivateOutputLinks(100);
            return;
        Label_002F:
            if (pinID != null)
            {
                goto Label_004E;
            }
            SceneBattle.Instance.Pause(1);
            base.ActivateOutputLinks(100);
            goto Label_0069;
        Label_004E:
            if (pinID != 1)
            {
                goto Label_0069;
            }
            SceneBattle.Instance.Pause(0);
            base.ActivateOutputLinks(100);
        Label_0069:
            return;
        }

        private bool IsPauseAllowed
        {
            get
            {
                if (GameUtility.GetCurrentScene() != 2)
                {
                    goto Label_000D;
                }
                return 0;
            Label_000D:
                return 1;
            }
        }
    }
}

