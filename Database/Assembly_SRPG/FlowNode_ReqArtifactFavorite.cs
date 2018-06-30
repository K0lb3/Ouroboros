namespace SRPG
{
    using GR;
    using System;

    [Pin(2, "Add Success", 1, 2), Pin(1, "お気に入り 削除", 0, 1), Pin(3, "Remove Success", 1, 3), Pin(0, "お気に入り 追加", 0, 0), NodeType("System/Artifact/Favorite", 0x7fe5)]
    public class FlowNode_ReqArtifactFavorite : FlowNode_Network
    {
        private const int PIN_ID_FAVORITE_ADD = 0;
        private const int PIN_ID_FAVORITE_REMOVE = 1;
        private const int PIN_ID_FAVORITE_ADD_SUCCESS = 2;
        private const int PIN_ID_FAVORITE_REMOVE_SUCCESS = 3;
        private bool success;

        public FlowNode_ReqArtifactFavorite()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            ArtifactData data;
            ArtifactData data2;
            base.set_enabled(1);
            this.success = 0;
            if (pinID != null)
            {
                goto Label_005E;
            }
            data = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0036;
            }
            DebugUtility.LogWarning("ArtifactDataがバインドされていません");
            goto Label_0059;
        Label_0036:
            base.ExecRequest(new ReqArtifactFavorite(data.UniqueID, 1, new Network.ResponseCallback(this.OnFavoriteAdd)));
        Label_0059:
            goto Label_00B6;
        Label_005E:
            if (pinID != 1)
            {
                goto Label_00AF;
            }
            data2 = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data2 != null)
            {
                goto Label_0087;
            }
            DebugUtility.LogWarning("ArtifactDataがバインドされていません");
            goto Label_00AA;
        Label_0087:
            base.ExecRequest(new ReqArtifactFavorite(data2.UniqueID, 0, new Network.ResponseCallback(this.OnFavoriteRemove)));
        Label_00AA:
            goto Label_00B6;
        Label_00AF:
            base.set_enabled(0);
        Label_00B6:
            return;
        }

        public void OnFavoriteAdd(WWWResult www)
        {
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.OnSuccess(www);
            if (this.success == null)
            {
                goto Label_0026;
            }
            base.ActivateOutputLinks(3);
        Label_0026:
            return;
        }

        public void OnFavoriteRemove(WWWResult www)
        {
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.OnSuccess(www);
            if (this.success == null)
            {
                goto Label_0026;
            }
            base.ActivateOutputLinks(2);
        Label_0026:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_ArtifactFavorite> response;
            Exception exception;
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArtifactFavorite>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (Network.IsError == null)
            {
                goto Label_002F;
            }
            this.OnRetry();
            return;
        Label_002F:
            if (response.body != null)
            {
                goto Label_0041;
            }
            this.OnRetry();
            return;
        Label_0041:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.artifacts, 1);
                goto Label_0083;
            }
            catch (Exception exception1)
            {
            Label_0071:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_008F;
            }
        Label_0083:
            Network.RemoveAPI();
            this.Success();
            return;
        Label_008F:
            return;
        }

        private void Success()
        {
            this.success = 1;
            base.set_enabled(0);
            return;
        }
    }
}

