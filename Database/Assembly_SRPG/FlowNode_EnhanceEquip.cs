namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [Pin(0, "Request", 0, 0), NodeType("System/EnhanceEquip", 0x7fe5), Pin(2, "費用が足りない", 1, 2), Pin(1, "Success", 1, 1)]
    public class FlowNode_EnhanceEquip : FlowNode_Network
    {
        public FlowNode_EnhanceEquip()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            PlayerData data;
            UnitData data2;
            JobData data3;
            int num;
            EquipData data4;
            int num2;
            int num3;
            List<EnhanceMaterial> list;
            int num4;
            EnhanceMaterial material;
            ItemData data5;
            int num5;
            EnhanceMaterial material2;
            ItemData data6;
            Dictionary<string, int> dictionary;
            int num6;
            EnhanceMaterial material3;
            ItemData data7;
            Dictionary<string, int> dictionary2;
            string str;
            int num7;
            if (pinID == null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (base.get_enabled() == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            data2 = manager.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            data3 = data2.Jobs[GlobalVars.SelectedUnitJobIndex];
            num = GlobalVars.SelectedEquipmentSlot;
            data4 = GlobalVars.SelectedEquipData;
            num2 = 0;
            num3 = 0;
            list = GlobalVars.SelectedEnhanceMaterials;
            if (list == null)
            {
                goto Label_007C;
            }
            if (list.Count >= 1)
            {
                goto Label_007D;
            }
        Label_007C:
            return;
        Label_007D:
            num4 = 0;
            goto Label_00DC;
        Label_0085:
            material = list[num4];
            data5 = material.item;
            num2 += ((data5.Param.enhace_cost * data4.GetEnhanceCostScale()) / 100) * material.num;
            num3 += data5.Param.enhace_point * material.num;
            num4 += 1;
        Label_00DC:
            if (num4 < list.Count)
            {
                goto Label_0085;
            }
            if (num2 <= data.Gold)
            {
                goto Label_0100;
            }
            base.ActivateOutputLinks(2);
            return;
        Label_0100:
            data4.GainExp(num3);
            if (data2 == null)
            {
                goto Label_0115;
            }
            data2.CalcStatus();
        Label_0115:
            num5 = 0;
            goto Label_0145;
        Label_011D:
            material2 = list[num5];
            material2.item.Used(material2.num);
            num5 += 1;
        Label_0145:
            if (num5 < list.Count)
            {
                goto Label_011D;
            }
            data.GainGold(-num2);
            if (Network.Mode != null)
            {
                goto Label_0223;
            }
            dictionary = new Dictionary<string, int>();
            num6 = 0;
            goto Label_01EC;
        Label_0175:
            material3 = list[num6];
            data7 = material3.item;
            if (material3.num >= 1)
            {
                goto Label_019B;
            }
            goto Label_01E6;
        Label_019B:
            if (dictionary.ContainsKey(data7.ItemID) != null)
            {
                goto Label_01BD;
            }
            dictionary[data7.ItemID] = 0;
        Label_01BD:
            num7 = dictionary2[str];
            (dictionary2 = dictionary)[str = data7.ItemID] = num7 + material3.num;
        Label_01E6:
            num6 += 1;
        Label_01EC:
            if (num6 < list.Count)
            {
                goto Label_0175;
            }
            base.ExecRequest(new ReqEquipExpAdd(data3.UniqueID, num, dictionary, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            return;
        Label_0223:
            this.Success();
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0046;
            }
            switch ((Network.ErrCode - 0xa28))
            {
                case 0:
                    goto Label_0031;

                case 1:
                    goto Label_0031;

                case 2:
                    goto Label_0038;

                case 3:
                    goto Label_0038;
            }
            goto Label_003F;
        Label_0031:
            this.OnFailed();
            return;
        Label_0038:
            this.OnBack();
            return;
        Label_003F:
            this.OnRetry();
            return;
        Label_0046:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0076;
            }
            this.OnRetry();
            return;
        Label_0076:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                goto Label_00D1;
            }
            catch (Exception exception1)
            {
            Label_00BA:
                exception = exception1;
                DebugUtility.LogException(exception);
                this.OnRetry();
                goto Label_00DC;
            }
        Label_00D1:
            Network.RemoveAPI();
            this.Success();
        Label_00DC:
            return;
        }

        private void Success()
        {
            MonoSingleton<GameManager>.Instance.Player.OnSoubiPowerUp();
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

