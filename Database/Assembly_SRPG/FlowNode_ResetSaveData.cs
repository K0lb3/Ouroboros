namespace SRPG
{
    using GR;
    using System;

    [NodeType("SRPG/セーブデータリセット", 0x7fe5), Pin(0, "Reset", 0, 0), Pin(1, "Out", 1, 1)]
    public class FlowNode_ResetSaveData : FlowNode
    {
        public FlowNode_ResetSaveData()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_002C;
            }
            MonoSingleton<GameManager>.Instance.Player.InitPlayerPrefs();
            MonoSingleton<GameManager>.Instance.Player.LoadPlayerPrefs();
            base.ActivateOutputLinks(1);
        Label_002C:
            return;
        }
    }
}

