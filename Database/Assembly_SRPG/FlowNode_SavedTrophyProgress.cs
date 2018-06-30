namespace SRPG
{
    using GR;
    using System;

    [Pin(2, "UpdateByGameManager", 0, 2), Pin(1, "LoadTrophyProgress", 0, 1), Pin(0, "Success", 1, 0), NodeType("System/SavedTrophyProgress", 0x7fe5)]
    public class FlowNode_SavedTrophyProgress : FlowNode
    {
        public FlowNode_SavedTrophyProgress()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0016;
            }
            MonoSingleton<GameManager>.Instance.LoadUpdateTrophyList();
            goto Label_002C;
        Label_0016:
            if (pinID != 2)
            {
                goto Label_002C;
            }
            MonoSingleton<GameManager>.Instance.update_trophy_lock.LockClear();
        Label_002C:
            base.ActivateOutputLinks(0);
            return;
        }
    }
}

