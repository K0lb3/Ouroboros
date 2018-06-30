namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(10, "SetText", 0, 10), NodeType("SRPG/ShopTelop", 0x7fe5), Pin(1, "Out", 1, 1)]
    public class FlowNode_ShopTelop : FlowNode
    {
        public string Text;
        public string ShopTelopGameObjectID;

        public FlowNode_ShopTelop()
        {
            this.ShopTelopGameObjectID = "ShopTelop";
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameObject obj2;
            ShopTelop telop;
            if (pinID != 10)
            {
                goto Label_0052;
            }
            obj2 = GameObjectID.FindGameObject(this.ShopTelopGameObjectID);
            telop = ((obj2 == null) == null) ? obj2.GetComponent<ShopTelop>() : null;
            if ((telop != null) == null)
            {
                goto Label_004A;
            }
            telop.SetText(LocalizedText.Get(this.Text));
        Label_004A:
            base.ActivateOutputLinks(1);
        Label_0052:
            return;
        }
    }
}

