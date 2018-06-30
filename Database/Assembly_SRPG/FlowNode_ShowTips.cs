namespace SRPG
{
    using System;
    using UnityEngine;

    [NodeType("Tips/ShowTips", 0x7fe5)]
    public class FlowNode_ShowTips : FlowNode_GUI
    {
        private const int PIN_ID_IN = 1;
        [SerializeField]
        private string Tips;

        public FlowNode_ShowTips()
        {
            base..ctor();
            return;
        }

        protected override void OnCreatePinActive()
        {
            GlobalVars.LastReadTips = this.Tips;
            base.OnCreatePinActive();
            return;
        }
    }
}

