namespace SRPG
{
    using System;

    [Pin(100, "Request", 0, 0), Pin(2, "Reset to Title", 1, 11), NodeType("System/ReqBtlComRaid", 0x7fe5), Pin(1, "Success", 1, 10)]
    public class FlowNode_ReqBtlComRaid : FlowNode_Network
    {
        public FlowNode_ReqBtlComRaid()
        {
            base..ctor();
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
        }
    }
}

