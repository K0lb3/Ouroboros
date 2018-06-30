namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "Success", 1, 1), Pin(4, "素材が足りない", 1, 4), Pin(0, "Request", 0, 0), Pin(100, "ワンタップ合成", 0, 100), Pin(2, "所持限界に達している", 1, 2), Pin(3, "費用が足りない", 1, 3), NodeType("System/CreateItem", 0x7fe5)]
    public class FlowNode_CreateItem : FlowNode_Network
    {
        private ItemParam mItemParam;

        public FlowNode_CreateItem()
        {
            base..ctor();
            return;
        }

        public void CallApi(NeedEquipItemList need_euip_item, PlayerData player)
        {
            if (Network.Mode != null)
            {
                goto Label_003E;
            }
            base.ExecRequest(new ReqItemCompositAll(this.mItemParam.iname, need_euip_item.IsEnoughCommon(), new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_004B;
        Label_003E:
            player.CreateItemAll(this.mItemParam);
        Label_004B:
            return;
        }

        public void CallApiNormal(PlayerData player, CreateItemResult result_type)
        {
            if (Network.Mode != null)
            {
                goto Label_003C;
            }
            base.ExecRequest(new ReqItemComposit(this.mItemParam.iname, result_type == 2, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            goto Label_0049;
        Label_003C:
            player.CreateItem(this.mItemParam);
        Label_0049:
            return;
        }

        public override unsafe void OnActivate(int pinID)
        {
            object[] objArray2;
            object[] objArray1;
            RecipeParam param;
            int num;
            Dictionary<string, int> dictionary;
            bool flag;
            NeedEquipItemList list;
            string str;
            string str2;
            int num2;
            Dictionary<string, int> dictionary2;
            bool flag2;
            bool flag3;
            string str3;
            string str4;
            <OnActivate>c__AnonStorey268 storey;
            <OnActivate>c__AnonStorey269 storey2;
            <OnActivate>c__AnonStorey26A storeya;
            storey = new <OnActivate>c__AnonStorey268();
            storey.<>f__this = this;
            if (pinID == null)
            {
                goto Label_001E;
            }
            if (pinID == 100)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            storey.player = MonoSingleton<GameManager>.Instance.Player;
            this.mItemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.SelectedCreateItemID);
            if (storey.player.CheckItemCapacity(this.mItemParam, 1) != null)
            {
                goto Label_006C;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        Label_006C:
            if (pinID != null)
            {
                goto Label_018D;
            }
            storey2 = new <OnActivate>c__AnonStorey269();
            storey2.<>f__ref$616 = storey;
            storey2.<>f__this = this;
            if (MonoSingleton<GameManager>.Instance.GetRecipeParam(this.mItemParam.recipe).cost <= storey.player.Gold)
            {
                goto Label_00C7;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        Label_00C7:
            storey2.result_type = storey.player.CheckCreateItem(this.mItemParam);
            if (storey2.result_type != null)
            {
                goto Label_00FC;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(4);
            return;
        Label_00FC:
            if (storey2.result_type != 2)
            {
                goto Label_0174;
            }
            num = 0;
            dictionary = null;
            flag = 0;
            list = new NeedEquipItemList();
            MonoSingleton<GameManager>.GetInstanceDirect().Player.CheckEnableCreateItem(this.mItemParam, &flag, &num, &dictionary, list);
            str = list.GetCommonItemListString();
            objArray1 = new object[] { str };
            UIUtility.ConfirmBox(LocalizedText.Get("sys.COMMON_EQUIP_CHECK_MADE", objArray1), new UIUtility.DialogResultEvent(storey2.<>m__194), null, null, 0, -1, null, null);
            goto Label_0188;
        Label_0174:
            this.CallApiNormal(storey.player, storey2.result_type);
        Label_0188:
            goto Label_0290;
        Label_018D:
            storeya = new <OnActivate>c__AnonStorey26A();
            storeya.<>f__ref$616 = storey;
            storeya.<>f__this = this;
            num2 = 0;
            dictionary2 = null;
            flag2 = 0;
            storeya.need_euip_item = new NeedEquipItemList();
            flag3 = storey.player.CheckEnableCreateItem(this.mItemParam, &flag2, &num2, &dictionary2, storeya.need_euip_item);
            if (num2 <= storey.player.Gold)
            {
                goto Label_01FE;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(3);
            return;
        Label_01FE:
            if (flag3 != null)
            {
                goto Label_0226;
            }
            if (storeya.need_euip_item.IsEnoughCommon() != null)
            {
                goto Label_0226;
            }
            base.set_enabled(0);
            base.ActivateOutputLinks(4);
            return;
        Label_0226:
            if (storeya.need_euip_item.IsEnoughCommon() == null)
            {
                goto Label_027C;
            }
            str3 = storeya.need_euip_item.GetCommonItemListString();
            objArray2 = new object[] { str3 };
            UIUtility.ConfirmBox(LocalizedText.Get("sys.COMMON_EQUIP_CHECK_ONETAP", objArray2), new UIUtility.DialogResultEvent(storeya.<>m__195), null, null, 0, -1, null, null);
            goto Label_0290;
        Label_027C:
            this.CallApi(storeya.need_euip_item, storey.player);
        Label_0290:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0040;
            }
            code = Network.ErrCode;
            if (code == 0xc81)
            {
                goto Label_0032;
            }
            if (code == 0xc82)
            {
                goto Label_002B;
            }
            goto Label_0039;
        Label_002B:
            this.OnFailed();
            return;
        Label_0032:
            this.OnFailed();
            return;
        Label_0039:
            this.OnRetry();
            return;
        Label_0040:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0070;
            }
            this.OnRetry();
            return;
        Label_0070:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                goto Label_00B6;
            }
            catch (Exception exception1)
            {
            Label_009F:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00F1;
            }
        Label_00B6:
            if (this.mItemParam == null)
            {
                goto Label_00E6;
            }
            UIUtility.SystemMessage(null, string.Format(LocalizedText.Get("sys.UNIT_EQUIPMENT_CREATE_MESSAGE"), this.mItemParam.name), null, null, 0, -1);
        Label_00E6:
            Network.RemoveAPI();
            this.Success();
        Label_00F1:
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey268
        {
            internal PlayerData player;
            internal FlowNode_CreateItem <>f__this;

            public <OnActivate>c__AnonStorey268()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey269
        {
            internal CreateItemResult result_type;
            internal FlowNode_CreateItem.<OnActivate>c__AnonStorey268 <>f__ref$616;
            internal FlowNode_CreateItem <>f__this;

            public <OnActivate>c__AnonStorey269()
            {
                base..ctor();
                return;
            }

            internal void <>m__194(GameObject go)
            {
                this.<>f__this.CallApiNormal(this.<>f__ref$616.player, this.result_type);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <OnActivate>c__AnonStorey26A
        {
            internal NeedEquipItemList need_euip_item;
            internal FlowNode_CreateItem.<OnActivate>c__AnonStorey268 <>f__ref$616;
            internal FlowNode_CreateItem <>f__this;

            public <OnActivate>c__AnonStorey26A()
            {
                base..ctor();
                return;
            }

            internal void <>m__195(GameObject go)
            {
                this.<>f__this.CallApi(this.need_euip_item, this.<>f__ref$616.player);
                return;
            }
        }
    }
}

