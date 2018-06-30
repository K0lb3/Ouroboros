namespace SRPG
{
    using GR;
    using System;

    [Pin(0x66, "クエストクリア編成情報機能メンテナンス中", 1, 12), Pin(0x67, "クエストクリア編成情報アップロード制限中", 1, 13), NodeType("System/BtlComRecordUpload", 0x7fe5), Pin(1, "Request", 0, 0), Pin(100, "Success", 1, 10), Pin(0x65, "Failed", 1, 11)]
    public class FlowNode_ReqBtlComRecordUpload : FlowNode_Network
    {
        public FlowNode_ReqBtlComRecordUpload()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0023;
            }
            if (Network.Mode != 1)
            {
                goto Label_001D;
            }
            this.Success();
            goto Label_0023;
        Label_001D:
            this.RequestUpload();
        Label_0023:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_005A;
            }
            code = Network.ErrCode;
            if (code == 0xcc)
            {
                goto Label_002B;
            }
            if (code == 0xced)
            {
                goto Label_003F;
            }
            goto Label_0053;
        Label_002B:
            Network.RemoveAPI();
            Network.ResetError();
            base.ActivateOutputLinks(0x66);
            return;
        Label_003F:
            Network.RemoveAPI();
            Network.ResetError();
            base.ActivateOutputLinks(0x67);
            return;
        Label_0053:
            this.OnRetry();
            return;
        Label_005A:
            GlobalVars.PartyUploadFinished = 1;
            Network.RemoveAPI();
            this.Success();
            return;
        }

        private unsafe void RequestUpload()
        {
            PlayerData data;
            BattleCore core;
            string str;
            long num;
            BattleCore.Record record;
            int[] numArray;
            int num2;
            int[] numArray2;
            int num3;
            int[] numArray3;
            int num4;
            int[] numArray4;
            int num5;
            int[] numArray5;
            QuestParam param;
            bool flag;
            int num6;
            data = MonoSingleton<GameManager>.Instance.Player;
            core = SceneBattle.Instance.Battle;
            str = data.FUID;
            num = core.BtlID;
            record = core.GetQuestRecord();
            numArray = new int[(int) record.drops.Length];
            num2 = 0;
            goto Label_0067;
        Label_0044:
            numArray[num2] = *(&(record.drops[num2]));
            num2 += 1;
        Label_0067:
            if (num2 < ((int) record.drops.Length))
            {
                goto Label_0044;
            }
            numArray2 = new int[(int) record.item_steals.Length];
            num3 = 0;
            goto Label_00B2;
        Label_008F:
            numArray2[num3] = *(&(record.item_steals[num3]));
            num3 += 1;
        Label_00B2:
            if (num3 < ((int) record.item_steals.Length))
            {
                goto Label_008F;
            }
            numArray3 = new int[(int) record.gold_steals.Length];
            num4 = 0;
            goto Label_00FD;
        Label_00DA:
            numArray3[num4] = *(&(record.gold_steals[num4]));
            num4 += 1;
        Label_00FD:
            if (num4 < ((int) record.gold_steals.Length))
            {
                goto Label_00DA;
            }
            numArray4 = new int[record.bonusCount];
            num5 = 0;
            goto Label_0149;
        Label_0123:
            numArray4[num5] = ((record.allBonusFlags & (1 << (num5 & 0x1f))) == null) ? 0 : 1;
            num5 += 1;
        Label_0149:
            if (num5 < ((int) numArray4.Length))
            {
                goto Label_0123;
            }
            numArray5 = new int[3];
            numArray5[0] = (core.PlayByManually == null) ? 1 : 0;
            numArray5[1] = (core.IsAllAlive() == null) ? 0 : 1;
            param = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            flag = 1;
            if (param.bonusObjective == null)
            {
                goto Label_01E0;
            }
            num6 = 0;
            goto Label_01D2;
        Label_01B0:
            if ((record.allBonusFlags & (1 << (num6 & 0x1f))) != null)
            {
                goto Label_01CC;
            }
            flag = 0;
            goto Label_01E0;
        Label_01CC:
            num6 += 1;
        Label_01D2:
            if (num6 < record.bonusCount)
            {
                goto Label_01B0;
            }
        Label_01E0:
            numArray5[2] = (flag == null) ? 0 : 1;
            base.ExecRequest(new ReqBtlComRecordUpload(str, num, numArray5, 0, 0, numArray, numArray2, numArray3, numArray4, record.used_items, new Network.ResponseCallback(this.ResponseCallback)));
            return;
        }

        private void Success()
        {
            base.ActivateOutputLinks(100);
            return;
        }
    }
}

