namespace SRPG
{
    using GR;
    using System;

    [Pin(0x3f2, "お気に入り設定OFF完了", 1, 0x3f2), Pin(0x3e8, "お気に入り設定ON完了", 1, 0x3e8), Pin(110, "お気に入りOFF", 0, 110), NodeType("System/ReqConceptCardFavorite", 0x7fe5), Pin(100, "お気に入りON", 0, 100)]
    public class FlowNode_ReqConceptCardFavorite : FlowNode_Network
    {
        private const int INPUT_FAVORITE_ON = 100;
        private const int INPUT_FAVORITE_OFF = 110;
        private const int OUTPUT_FAVORITE_ON = 0x3e8;
        private const int OUTPUT_FAVORITE_OFF = 0x3f2;
        private int mOutPutPinId;

        public FlowNode_ReqConceptCardFavorite()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            ConceptCardManager manager;
            int num;
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            num = pinID;
            if (num == 100)
            {
                goto Label_002A;
            }
            if (num == 110)
            {
                goto Label_0062;
            }
            goto Label_009A;
        Label_002A:
            base.ExecRequest(new ReqFavoriteConceptCard(manager.SelectedConceptCardData.UniqueID, 1, new Network.ResponseCallback(this.ResponseCallback)));
            this.mOutPutPinId = 0x3e8;
            goto Label_009A;
        Label_0062:
            base.ExecRequest(new ReqFavoriteConceptCard(manager.SelectedConceptCardData.UniqueID, 0, new Network.ResponseCallback(this.ResponseCallback)));
            this.mOutPutPinId = 0x3f2;
        Label_009A:
            base.set_enabled(1);
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ConceptCardFavorite> response;
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
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ConceptCardFavorite>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            Network.RemoveAPI();
        Label_003A:
            try
            {
                MonoSingleton<GameManager>.Instance.Player.Deserialize(response.body.concept_card);
                goto Label_006A;
            }
            catch (Exception exception1)
            {
            Label_0059:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_007E;
            }
        Label_006A:
            base.ActivateOutputLinks(this.mOutPutPinId);
            base.set_enabled(0);
        Label_007E:
            return;
        }

        public class Json_ConceptCardFavorite
        {
            public JSON_ConceptCard concept_card;

            public Json_ConceptCardFavorite()
            {
                base..ctor();
                return;
            }
        }
    }
}

