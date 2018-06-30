namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(1, "Updated", 1, 1), AddComponentMenu(""), NodeType("UI/UpdateParameter", 0x7fe5), Pin(100, "Update", 0, 0), Pin(0x65, "UpdateAll", 0, 2)]
    public class FlowNode_UpdateParameter : FlowNode
    {
        [ShowInInfo, DropTarget(typeof(GameObject), false)]
        public GameObject Target;

        public FlowNode_UpdateParameter()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            Component[] componentArray;
            int num;
            if (pinID == 100)
            {
                goto Label_0010;
            }
            if (pinID != 0x65)
            {
                goto Label_0065;
            }
        Label_0010:
            if ((this.Target != null) == null)
            {
                goto Label_0065;
            }
            componentArray = this.Target.GetComponentsInChildren(typeof(IGameParameter), pinID == 0x65);
            num = 0;
            goto Label_0054;
        Label_0043:
            ((IGameParameter) componentArray[num]).UpdateValue();
            num += 1;
        Label_0054:
            if (num < ((int) componentArray.Length))
            {
                goto Label_0043;
            }
            base.ActivateOutputLinks(1);
        Label_0065:
            return;
        }
    }
}

