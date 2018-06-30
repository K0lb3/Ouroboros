namespace SRPG
{
    using GR;
    using System;

    [NodeType("System/ReqConceptCardMix", 0x7fe5), Pin(10, "合成開始", 0, 10), Pin(0x3e8, "合成完了", 1, 0x3e8)]
    public class FlowNode_ReqConceptCardMix : FlowNode_Network
    {
        private const int INPUT_CONCEPT_CARD_MIX_START = 10;
        private const int OUTPUT_CONCEPT_CARD_MIX_END = 0x3e8;
        private long mBaseCardId;
        private long[] mMixCardIds;
        private int totalMixZeny;

        public FlowNode_ReqConceptCardMix()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 10)
            {
                goto Label_003A;
            }
            this.SetMixParam();
            base.set_enabled(1);
            base.ExecRequest(new ReqMixConceptCard(this.mBaseCardId, this.mMixCardIds, new Network.ResponseCallback(this.ResponseCallback), null, null));
        Label_003A:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ConceptCardMix> response;
            long num;
            int num2;
            int num3;
            int num4;
            ConceptCardData data;
            ConceptCardData data2;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0018;
            }
            code = Network.ErrCode;
            this.OnRetry();
            return;
        Label_0018:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ConceptCardMix>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
            num = -1L;
            num2 = -1;
            num3 = -1;
            num4 = -1;
            data = null;
            data2 = null;
            if (response.body.concept_card == null)
            {
                goto Label_00AD;
            }
            num = response.body.concept_card.iid;
            data = MonoSingleton<GameManager>.Instance.Player.FindConceptCardByUniqueID(num);
            if (data == null)
            {
                goto Label_00AD;
            }
            num2 = data.Lv;
            num3 = data.AwakeCount;
            num4 = data.Trust;
        Label_00AD:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.concept_card);
                MonoSingleton<GameManager>.Instance.Player.RemoveConceptCardData(response.body.mix_ids);
                goto Label_010E;
            }
            catch (Exception exception1)
            {
            Label_00FB:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0196;
            }
        Label_010E:
            MonoSingleton<GameManager>.Instance.Player.OnGoldChange(this.totalMixZeny);
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
            data2 = MonoSingleton<GameManager>.Instance.Player.FindConceptCardByUniqueID(num);
            MonoSingleton<GameManager>.Instance.Player.OnMixedConceptCard(data2.Param.iname, num2, data2.Lv, num3, data2.AwakeCount, num4, data2.Trust);
            base.ActivateOutputLinks(0x3e8);
            base.set_enabled(0);
        Label_0196:
            return;
        }

        public unsafe void SetMixParam()
        {
            ConceptCardManager manager;
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            ConceptCardManager.GalcTotalMixZeny(manager.SelectedMaterials, &this.totalMixZeny);
            manager.SetupLevelupAnimation();
            this.mBaseCardId = manager.SelectedConceptCardData.UniqueID;
            this.mMixCardIds = manager.SelectedMaterials.GetUniqueIDs().ToArray();
            manager.SelectedMaterials.Clear();
            return;
        }

        public class Json_ConceptCardMix
        {
            public Json_PlayerData player;
            public JSON_ConceptCard concept_card;
            public long[] mix_ids;

            public Json_ConceptCardMix()
            {
                base..ctor();
                return;
            }
        }
    }
}

