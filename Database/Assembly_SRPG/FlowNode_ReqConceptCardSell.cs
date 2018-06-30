namespace SRPG
{
    using GR;
    using System;

    [Pin(100, "売却した", 1, 100), NodeType("System/ReqConceptCardSell", 0x7fe5), Pin(0, "売却する", 0, 0)]
    public class FlowNode_ReqConceptCardSell : FlowNode_Network
    {
        private const int INPUT_CONCEPT_CARD_SELL = 0;
        private const int OUTPUT_CONCEPT_CARD_SOLD = 100;
        private long[] sellCardIDs;
        private int totalSellZeny;

        public FlowNode_ReqConceptCardSell()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0030;
            }
            this.SetSellParam();
            base.set_enabled(1);
            base.ExecRequest(new ReqSellConceptCard(this.sellCardIDs, new Network.ResponseCallback(this.ResponseCallback)));
        Label_0030:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ConceptCardSell> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ConceptCardSell>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_003A:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Player.RemoveConceptCardData(response.body.sell_ids);
                goto Label_007F;
            }
            catch (Exception exception1)
            {
            Label_006E:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_00A4;
            }
        Label_007F:
            MonoSingleton<GameManager>.Instance.Player.OnGoldChange(this.totalSellZeny);
            base.ActivateOutputLinks(100);
            base.set_enabled(0);
        Label_00A4:
            return;
        }

        public unsafe void SetSellParam()
        {
            ConceptCardManager manager;
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            ConceptCardManager.GalcTotalSellZeny(manager.SelectedMaterials, &this.totalSellZeny);
            this.sellCardIDs = manager.SelectedMaterials.GetUniqueIDs().ToArray();
            manager.SelectedMaterials.Clear();
            return;
        }

        public class Json_ConceptCardSell
        {
            public Json_PlayerData player;
            public long[] sell_ids;

            public Json_ConceptCardSell()
            {
                base..ctor();
                return;
            }
        }
    }
}

