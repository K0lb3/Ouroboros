namespace SRPG
{
    using System;

    [Pin(2, "Failure", 1, 0), Pin(0x65, "Update", 0, 0), Pin(1, "Success", 1, 0), NodeType("Multi/MultiPlayUpdatePlayerParam", 0x7fe5)]
    public class FlowNode_MultiPlayUpdatePlayerParam : FlowNode
    {
        public FlowNode_MultiPlayUpdatePlayerParam()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            MyPhoton photon;
            MyPhoton.MyPlayer player;
            JSON_MyPhotonPlayerParam param;
            if (pinID != 0x65)
            {
                goto Label_0051;
            }
            photon = PunMonoSingleton<MyPhoton>.Instance;
            player = photon.GetMyPlayer();
            if (player != null)
            {
                goto Label_0024;
            }
            base.ActivateOutputLinks(2);
            return;
        Label_0024:
            param = JSON_MyPhotonPlayerParam.Create(player.playerID, photon.MyPlayerIndex);
            param.UpdateMultiTowerPlacement(0);
            photon.SetMyPlayerParam(param.Serialize());
            base.ActivateOutputLinks(1);
        Label_0051:
            return;
        }
    }
}

