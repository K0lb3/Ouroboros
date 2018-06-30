namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0), NodeType("System/SupportSet", 0x7fe5)]
    public class FlowNode_SupportSet : FlowNode_Network
    {
        public GameObject UnitParent;

        public FlowNode_SupportSet()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            UnitData data;
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedSupportUnitUniqueID);
            base.ExecRequest(new ReqSetSupport(data.UniqueID, new Network.ResponseCallback(this.ResponseCallback)));
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            UnitData data;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnFailed();
            return;
        Label_0017:
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedSupportUnitUniqueID);
            DataSource.Bind<UnitData>(this.UnitParent, data);
            GameParameter.UpdateAll(base.get_gameObject());
            Network.RemoveAPI();
            base.ActivateOutputLinks(1);
            return;
        }

        private void OnUnitSelect(long uniqueID)
        {
        }
    }
}

