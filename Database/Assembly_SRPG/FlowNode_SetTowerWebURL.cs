namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [NodeType("System/SetTowerWebURL", 0x7fe5), Pin(2, "Error", 1, 0), Pin(1, "Succes", 1, 0), Pin(0, "Set", 0, 0)]
    public class FlowNode_SetTowerWebURL : FlowNode
    {
        [SerializeField]
        private string URL;
        [SerializeField]
        private string Value;

        public FlowNode_SetTowerWebURL()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            TowerParam param;
            if (pinID != null)
            {
                goto Label_0049;
            }
            param = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
            if (param != null)
            {
                goto Label_0025;
            }
            base.ActivateOutputLinks(2);
            return;
        Label_0025:
            FlowNode_Variable.Set(this.Value, this.URL + param.URL);
            base.ActivateOutputLinks(1);
        Label_0049:
            return;
        }
    }
}

