namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(1, "Success", 1, 1), Pin(0, "Request", 0, 0), NodeType("System/ReqUpdateTrophy", 0x7fe5)]
    public class FlowNode_ReqUpdateTrophy : FlowNode_Network
    {
        private TrophyParam mTrophyParam;
        private int mLevelOld;
        public GameObject RewardWindow;
        public string ReviewURL_Android;
        public string ReviewURL_iOS;
        public string ReviewURL_Generic;
        public string ReviewURL_Twitter;

        public FlowNode_ReqUpdateTrophy()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnOverItemAmount>m__1C5(GameObject go)
        {
            GameManager manager;
            TrophyParam param;
            TrophyState state;
            List<TrophyState> list;
            List<TrophyState> list2;
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.MasterParam.GetTrophy(GlobalVars.SelectedTrophy);
            state = manager.Player.GetTrophyCounter(param, 0);
            list2 = new List<TrophyState>();
            list2.Add(state);
            list = list2;
            base.ExecRequest(new ReqUpdateTrophy(list, new Network.ResponseCallback(this.ResponseCallback), 1));
            base.set_enabled(1);
            return;
        }

        [CompilerGenerated]
        private void <OnOverItemAmount>m__1C6(GameObject go)
        {
            base.set_enabled(0);
            return;
        }

        private void Deserialize(Json_TrophyConceptCards json)
        {
            RewardData data;
            Json_TrophyConceptCard card;
            Json_TrophyConceptCard[] cardArray;
            int num;
            ItemParam param;
            ConceptCardData data2;
            RewardData data3;
            Json_TrophyConceptCard card2;
            Json_TrophyConceptCard[] cardArray2;
            int num2;
            ItemParam param2;
            ConceptCardData data4;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (json.mail == null)
            {
                goto Label_00A1;
            }
            if (((int) json.mail.Length) <= 0)
            {
                goto Label_00A1;
            }
            data = GlobalVars.LastReward.Get();
            if (data == null)
            {
                goto Label_0038;
            }
            data.IsOverLimit = 1;
        Label_0038:
            cardArray = json.mail;
            num = 0;
            goto Label_0098;
        Label_0046:
            card = cardArray[num];
            if (string.IsNullOrEmpty(card.unit) != null)
            {
                goto Label_0094;
            }
            if (data == null)
            {
                goto Label_0080;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(card.unit);
            data.AddReward(param, 1);
        Label_0080:
            FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(card.iname));
        Label_0094:
            num += 1;
        Label_0098:
            if (num < ((int) cardArray.Length))
            {
                goto Label_0046;
            }
        Label_00A1:
            if (json.direct == null)
            {
                goto Label_013A;
            }
            data3 = GlobalVars.LastReward.Get();
            cardArray2 = json.direct;
            num2 = 0;
            goto Label_012F;
        Label_00C8:
            card2 = cardArray2[num2];
            GlobalVars.IsDirtyConceptCardData.Set(1);
            if (string.IsNullOrEmpty(card2.unit) != null)
            {
                goto Label_0129;
            }
            if (data3 == null)
            {
                goto Label_0114;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(card2.unit);
            data3.AddReward(param2, 1);
        Label_0114:
            FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(card2.iname));
        Label_0129:
            num2 += 1;
        Label_012F:
            if (num2 < ((int) cardArray2.Length))
            {
                goto Label_00C8;
            }
        Label_013A:
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            TrophyParam param;
            PlayerData data;
            TrophyState state;
            List<TrophyState> list;
            List<TrophyState> list2;
            if (pinID != null)
            {
                goto Label_00DB;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.MasterParam.GetTrophy(GlobalVars.SelectedTrophy);
            this.mTrophyParam = param;
            data = MonoSingleton<GameManager>.Instance.Player;
            this.mLevelOld = data.Lv;
            GlobalVars.PlayerExpOld.Set(data.Exp);
            if (Network.Mode != 1)
            {
                goto Label_0099;
            }
            manager.Player.DEBUG_ADD_COIN(param.Coin, 0, 0);
            manager.Player.DEBUG_ADD_GOLD(param.Gold);
            base.set_enabled(0);
            this.Success();
            return;
        Label_0099:
            state = manager.Player.GetTrophyCounter(param, 1);
            list2 = new List<TrophyState>();
            list2.Add(state);
            list = list2;
            base.ExecRequest(new ReqUpdateTrophy(list, new Network.ResponseCallback(this.ResponseCallback), 1));
            base.set_enabled(1);
        Label_00DB:
            return;
        }

        private void OnOverItemAmount()
        {
            string str;
            UIUtility.ConfirmBox(LocalizedText.Get("sys.MAILBOX_ITEM_OVER_MSG"), null, new UIUtility.DialogResultEvent(this.<OnOverItemAmount>m__1C5), new UIUtility.DialogResultEvent(this.<OnOverItemAmount>m__1C6), null, 0, -1);
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if ((this == null) == null)
            {
                goto Label_0013;
            }
            this.OnFailed();
            return;
        Label_0013:
            if (Network.IsError == null)
            {
                goto Label_004E;
            }
            switch ((Network.ErrCode - 0x1004))
            {
                case 0:
                    goto Label_0040;

                case 1:
                    goto Label_0040;

                case 2:
                    goto Label_0040;
            }
            goto Label_0047;
        Label_0040:
            this.OnBack();
            return;
        Label_0047:
            this.OnRetry();
            return;
        Label_004E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_007E;
            }
            this.OnFailed();
            return;
        Label_007E:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                this.Deserialize(response.body.concept_cards);
                goto Label_00EA;
            }
            catch (Exception exception1)
            {
            Label_00D3:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnFailed();
                goto Label_00F5;
            }
        Label_00EA:
            Network.RemoveAPI();
            this.Success();
        Label_00F5:
            return;
        }

        private unsafe void Success()
        {
            TrophyParam[] paramArray1;
            PlayerData data;
            int num;
            int num2;
            RewardData data2;
            GameManager manager;
            int num3;
            ItemData data3;
            ItemData data4;
            int num4;
            int num5;
            string str;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (Network.Mode != 1)
            {
                goto Label_0066;
            }
            num = 0;
            goto Label_0053;
        Label_001D:
            data.GainItem(&(this.mTrophyParam.Items[num]).iname, &(this.mTrophyParam.Items[num]).Num);
            num += 1;
        Label_0053:
            if (num < ((int) this.mTrophyParam.Items.Length))
            {
                goto Label_001D;
            }
        Label_0066:
            num2 = 0;
            goto Label_00A3;
        Label_006D:
            data.OnItemQuantityChange(&(this.mTrophyParam.Items[num2]).iname, &(this.mTrophyParam.Items[num2]).Num);
            num2 += 1;
        Label_00A3:
            if (num2 < ((int) this.mTrophyParam.Items.Length))
            {
                goto Label_006D;
            }
            data.OnCoinChange(this.mTrophyParam.Coin);
            data.OnGoldChange(this.mTrophyParam.Gold);
            if (data.Lv <= this.mLevelOld)
            {
                goto Label_00FC;
            }
            data.OnPlayerLevelChange(data.Lv - this.mLevelOld);
        Label_00FC:
            GlobalVars.PlayerLevelChanged.Set((data.Lv == this.mLevelOld) == 0);
            GlobalVars.PlayerExpNew.Set(data.Exp);
            paramArray1 = new TrophyParam[] { this.mTrophyParam };
            data.MarkTrophiesEnded(paramArray1);
            if ((this.RewardWindow != null) == null)
            {
                goto Label_0248;
            }
            data2 = new RewardData();
            data2.Coin = this.mTrophyParam.Coin;
            data2.Gold = this.mTrophyParam.Gold;
            data2.Exp = this.mTrophyParam.Exp;
            manager = MonoSingleton<GameManager>.Instance;
            num3 = 0;
            goto Label_0228;
        Label_0195:
            data3 = new ItemData();
            if (data3.Setup(0L, &(this.mTrophyParam.Items[num3]).iname, &(this.mTrophyParam.Items[num3]).Num) == null)
            {
                goto Label_0222;
            }
            data2.Items.Add(data3);
            data4 = manager.Player.FindItemDataByItemID(data3.Param.iname);
            num4 = (data4 == null) ? 0 : data4.Num;
            data2.ItemsBeforeAmount.Add(num4);
        Label_0222:
            num3 += 1;
        Label_0228:
            if (num3 < ((int) this.mTrophyParam.Items.Length))
            {
                goto Label_0195;
            }
            DataSource.Bind<RewardData>(this.RewardWindow, data2);
        Label_0248:
            GameCenterManager.SendAchievementProgress(this.mTrophyParam.iname);
            if (this.mTrophyParam == null)
            {
                goto Label_02CF;
            }
            if (this.mTrophyParam.Objectives == null)
            {
                goto Label_02CF;
            }
            num5 = 0;
            goto Label_02BB;
        Label_027B:
            if (this.mTrophyParam.Objectives[num5].type != 0x12)
            {
                goto Label_02B5;
            }
            str = this.ReviewURL_Twitter;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_02CF;
            }
            Application.OpenURL(str);
            goto Label_02CF;
        Label_02B5:
            num5 += 1;
        Label_02BB:
            if (num5 < ((int) this.mTrophyParam.Objectives.Length))
            {
                goto Label_027B;
            }
        Label_02CF:
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

