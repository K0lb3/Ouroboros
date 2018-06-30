namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "装備", 0, 0), Pin(10, "外す", 0, 10), Pin(100, "装備した", 1, 100), Pin(110, "外した", 1, 110), NodeType("System/ReqConceptCardSet", 0x7fe5)]
    public class FlowNode_ReqConceptCardSet : FlowNode_Network
    {
        private const int INPUT_CONCEPT_CARD_SET = 0;
        private const int INPUT_CONCEPT_CARD_UNSET = 10;
        private const int OUTPUT_CONCEPT_CARD_SET = 100;
        private const int OUTPUT_CONCEPT_CARD_UNSET = 110;
        private long mEquipCardId;
        private long mTargetUnitId;

        public FlowNode_ReqConceptCardSet()
        {
            base..ctor();
            return;
        }

        private void ConceptCardSetResponseCallback(WWWResult www)
        {
            if (FlowNode_Network.HasCommonError(www) != null)
            {
                goto Label_001B;
            }
            this.OnSuccess(www);
            base.ActivateOutputLinks(100);
        Label_001B:
            return;
        }

        private void ConceptCardUnsetResponseCallback(WWWResult www)
        {
            if (FlowNode_Network.HasCommonError(www) != null)
            {
                goto Label_001B;
            }
            this.OnSuccess(www);
            base.ActivateOutputLinks(110);
        Label_001B:
            return;
        }

        public override void OnActivate(int pinID)
        {
            long num;
            if (pinID != null)
            {
                goto Label_0035;
            }
            base.set_enabled(1);
            base.ExecRequest(ReqSetConceptCard.CreateSet(this.mEquipCardId, this.mTargetUnitId, new Network.ResponseCallback(this.ConceptCardSetResponseCallback)));
            goto Label_005F;
        Label_0035:
            if (pinID != 10)
            {
                goto Label_005F;
            }
            num = 0L;
            base.set_enabled(1);
            base.ExecRequest(ReqSetConceptCard.CreateUnset(num, new Network.ResponseCallback(this.ConceptCardUnsetResponseCallback)));
        Label_005F:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
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
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_003A:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                if ((UnitEnhanceV3.Instance != null) == null)
                {
                    goto Label_007E;
                }
                UnitEnhanceV3.Instance.OnEquipConceptCardSelect();
            Label_007E:
                goto Label_0094;
            }
            catch (Exception exception1)
            {
            Label_0083:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_009B;
            }
        Label_0094:
            base.set_enabled(0);
        Label_009B:
            return;
        }

        private void ResetParam()
        {
            this.mEquipCardId = 0L;
            this.mTargetUnitId = 0L;
            return;
        }

        public void SetEquipParam(long equip_card_iid, long unit_iid)
        {
            this.ResetParam();
            this.mEquipCardId = equip_card_iid;
            this.mTargetUnitId = unit_iid;
            return;
        }

        public void SetReleaseParam(long equip_card_iid)
        {
            this.ResetParam();
            this.mEquipCardId = equip_card_iid;
            return;
        }
    }
}

