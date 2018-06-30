namespace SRPG
{
    using GR;
    using System;

    [NodeType("UI/DeactivateInventorySlot", 0x7fe5), Pin(100, "Out", 1, 100), Pin(1, "Deactivate", 0, 1)]
    public class FlowNode_DeactivateInventorySlot : FlowNode
    {
        public FlowNode_DeactivateInventorySlot()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0025;
            }
            InventorySlot.Active = null;
            MonoSingleton<GameManager>.Instance.Player.SaveInventory();
            base.ActivateOutputLinks(100);
        Label_0025:
            return;
        }
    }
}

