namespace SRPG
{
    using GR;
    using System;

    [Pin(0x6a, "CheckTowerSeasonGift", 0, 0x6a), Pin(0x6b, "CheckTopFloor", 0, 0x6b), NodeType("Multi/MultiPlayVersusType", 0x7fe5), Pin(0x6c, "CheckAudience", 0, 0x6c), Pin(0x6d, "CheckCPU", 0, 0x6d), Pin(110, "SetCpuBtl", 0, 110), Pin(0x6f, "CheckBeginRankMatch", 0, 0x6f), Pin(0x65, "Tower", 0, 0x65), Pin(100, "Free", 0, 100), Pin(0x70, "RankMatch", 0, 0x70), Pin(0x71, "CheckDraft", 0, 0x71), Pin(0x72, "CheckDraftFriend", 0, 0x72), Pin(200, "Out", 1, 200), Pin(0xc9, "OK", 1, 0xc9), Pin(0xca, "NG", 1, 0xca), Pin(0x66, "Friend", 0, 0x66), Pin(0x67, "Check", 0, 0x67), Pin(0x68, "CheckBeginTower", 0, 0x68), Pin(0x69, "CheckTowerReceipt", 0, 0x69)]
    public class FlowNode_MultiPlayVersusType : FlowNode
    {
        private const int FREE = 100;
        private const int TOWER = 0x65;
        private const int FRIEND = 0x66;
        private const int CHECK = 0x67;
        private const int CHECK_TOWER = 0x68;
        private const int CHECK_RECEIPT = 0x69;
        private const int CHECK_SEASONGIFT = 0x6a;
        private const int CHECK_TOPFLOOR = 0x6b;
        private const int CHECK_AUDIENCE = 0x6c;
        private const int CHECK_CPUBATTLE = 0x6d;
        private const int CPUBTL = 110;
        private const int PIN_IN_CHECK_RANKMATCH = 0x6f;
        private const int PIN_IN_RANKMATCH = 0x70;
        private const int CHECK_DRAFT = 0x71;
        private const int CHECK_DRAFT_FRIEND = 0x72;
        private const int PIN_OUT_OUT = 200;
        private const int PIN_OUT_OK = 0xc9;
        private const int PIN_OUT_NG = 0xca;
        public VERSUS_TYPE type;

        public FlowNode_MultiPlayVersusType()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            PlayerData data;
            VsTowerMatchEndParam param;
            VersusTowerParam param2;
            long num;
            long num2;
            manager = MonoSingleton<GameManager>.Instance;
            if (pinID != 100)
            {
                goto Label_0019;
            }
            GlobalVars.SelectedMultiPlayVersusType = 0;
            goto Label_02C7;
        Label_0019:
            if (pinID != 0x65)
            {
                goto Label_002C;
            }
            GlobalVars.SelectedMultiPlayVersusType = 1;
            goto Label_02C7;
        Label_002C:
            if (pinID != 0x66)
            {
                goto Label_003F;
            }
            GlobalVars.SelectedMultiPlayVersusType = 2;
            goto Label_02C7;
        Label_003F:
            if (pinID != 110)
            {
                goto Label_0053;
            }
            manager.IsVSCpuBattle = 1;
            goto Label_02C7;
        Label_0053:
            if (pinID != 0x70)
            {
                goto Label_0066;
            }
            GlobalVars.SelectedMultiPlayVersusType = 3;
            goto Label_02C7;
        Label_0066:
            if (pinID != 0x67)
            {
                goto Label_00C1;
            }
            if (GlobalVars.SelectedMultiPlayVersusType != this.type)
            {
                goto Label_00B4;
            }
            if (this.type != 1)
            {
                goto Label_00A7;
            }
            if (manager.VersusTowerMatchBegin == null)
            {
                goto Label_00B4;
            }
            base.ActivateOutputLinks(0xc9);
            return;
            goto Label_00B4;
        Label_00A7:
            base.ActivateOutputLinks(0xc9);
            return;
        Label_00B4:
            base.ActivateOutputLinks(0xca);
            return;
        Label_00C1:
            if (pinID != 0x68)
            {
                goto Label_00EE;
            }
            if (manager.VersusTowerMatchBegin == null)
            {
                goto Label_00E1;
            }
            base.ActivateOutputLinks(0xc9);
            return;
        Label_00E1:
            base.ActivateOutputLinks(0xca);
            return;
        Label_00EE:
            if (pinID != 0x69)
            {
                goto Label_011B;
            }
            if (manager.VersusTowerMatchReceipt == null)
            {
                goto Label_010E;
            }
            base.ActivateOutputLinks(0xc9);
            return;
        Label_010E:
            base.ActivateOutputLinks(0xca);
            return;
        Label_011B:
            if (pinID != 0x6a)
            {
                goto Label_014F;
            }
            if (manager.Player.VersusSeazonGiftReceipt == null)
            {
                goto Label_0142;
            }
            base.ActivateOutputLinks(0xc9);
            return;
        Label_0142:
            base.ActivateOutputLinks(0xca);
            return;
        Label_014F:
            if (pinID != 0x6b)
            {
                goto Label_01AC;
            }
            param = manager.GetVsTowerMatchEndParam();
            if (param == null)
            {
                goto Label_019F;
            }
            param2 = manager.GetCurrentVersusTowerParam(param.floor);
            if (param2 == null)
            {
                goto Label_019F;
            }
            if (param2.RankupNum != null)
            {
                goto Label_019F;
            }
            if (param.rankup != null)
            {
                goto Label_019F;
            }
            base.ActivateOutputLinks(0xc9);
            return;
        Label_019F:
            base.ActivateOutputLinks(0xca);
            return;
        Label_01AC:
            if (pinID != 0x6c)
            {
                goto Label_01D9;
            }
            if (manager.AudienceMode == null)
            {
                goto Label_01CC;
            }
            base.ActivateOutputLinks(0xc9);
            return;
        Label_01CC:
            base.ActivateOutputLinks(0xca);
            return;
        Label_01D9:
            if (pinID != 0x6d)
            {
                goto Label_0206;
            }
            if (manager.IsVSCpuBattle == null)
            {
                goto Label_01F9;
            }
            base.ActivateOutputLinks(0xc9);
            return;
        Label_01F9:
            base.ActivateOutputLinks(0xca);
            return;
        Label_0206:
            if (pinID != 0x6f)
            {
                goto Label_0257;
            }
            num = manager.RankMatchExpiredTime;
            num2 = TimeManager.FromDateTime(TimeManager.ServerTime);
            manager.RankMatchBegin = num2 < num;
            if (manager.RankMatchBegin == null)
            {
                goto Label_024A;
            }
            base.ActivateOutputLinks(0xc9);
            goto Label_0256;
        Label_024A:
            base.ActivateOutputLinks(0xca);
        Label_0256:
            return;
        Label_0257:
            if (pinID != 0x71)
            {
                goto Label_0289;
            }
            if (manager.VSDraftType != 1)
            {
                goto Label_027C;
            }
            base.ActivateOutputLinks(0xc9);
            goto Label_0288;
        Label_027C:
            base.ActivateOutputLinks(0xca);
        Label_0288:
            return;
        Label_0289:
            if (pinID != 0x72)
            {
                goto Label_02C7;
            }
            if (manager.VSDraftType == 1)
            {
                goto Label_02A9;
            }
            if (manager.VSDraftType != 2)
            {
                goto Label_02BA;
            }
        Label_02A9:
            base.ActivateOutputLinks(0xc9);
            goto Label_02C6;
        Label_02BA:
            base.ActivateOutputLinks(0xca);
        Label_02C6:
            return;
        Label_02C7:
            base.ActivateOutputLinks(200);
            return;
        }
    }
}

