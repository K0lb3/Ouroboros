// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiPlayVersusType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(106, "CheckTowerSeasonGift", FlowNode.PinTypes.Input, 106)]
  [FlowNode.Pin(107, "CheckTopFloor", FlowNode.PinTypes.Input, 107)]
  [FlowNode.NodeType("Multi/MultiPlayVersusType", 32741)]
  [FlowNode.Pin(108, "CheckAudience", FlowNode.PinTypes.Input, 108)]
  [FlowNode.Pin(109, "CheckCPU", FlowNode.PinTypes.Input, 109)]
  [FlowNode.Pin(110, "SetCpuBtl", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(111, "CheckBeginRankMatch", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(101, "Tower", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(100, "Free", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(112, "RankMatch", FlowNode.PinTypes.Input, 112)]
  [FlowNode.Pin(113, "CheckDraft", FlowNode.PinTypes.Input, 113)]
  [FlowNode.Pin(114, "CheckDraftFriend", FlowNode.PinTypes.Input, 114)]
  [FlowNode.Pin(200, "Out", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "OK", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "NG", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(102, "Friend", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(103, "Check", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(104, "CheckBeginTower", FlowNode.PinTypes.Input, 104)]
  [FlowNode.Pin(105, "CheckTowerReceipt", FlowNode.PinTypes.Input, 105)]
  public class FlowNode_MultiPlayVersusType : FlowNode
  {
    private const int FREE = 100;
    private const int TOWER = 101;
    private const int FRIEND = 102;
    private const int CHECK = 103;
    private const int CHECK_TOWER = 104;
    private const int CHECK_RECEIPT = 105;
    private const int CHECK_SEASONGIFT = 106;
    private const int CHECK_TOPFLOOR = 107;
    private const int CHECK_AUDIENCE = 108;
    private const int CHECK_CPUBATTLE = 109;
    private const int CPUBTL = 110;
    private const int PIN_IN_CHECK_RANKMATCH = 111;
    private const int PIN_IN_RANKMATCH = 112;
    private const int CHECK_DRAFT = 113;
    private const int CHECK_DRAFT_FRIEND = 114;
    private const int PIN_OUT_OUT = 200;
    private const int PIN_OUT_OK = 201;
    private const int PIN_OUT_NG = 202;
    public VERSUS_TYPE type;

    public override void OnActivate(int pinID)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      switch (pinID)
      {
        case 100:
          GlobalVars.SelectedMultiPlayVersusType = VERSUS_TYPE.Free;
          break;
        case 101:
          GlobalVars.SelectedMultiPlayVersusType = VERSUS_TYPE.Tower;
          break;
        case 102:
          GlobalVars.SelectedMultiPlayVersusType = VERSUS_TYPE.Friend;
          break;
        case 103:
          if (GlobalVars.SelectedMultiPlayVersusType == this.type)
          {
            if (this.type == VERSUS_TYPE.Tower)
            {
              if (instance.VersusTowerMatchBegin)
              {
                this.ActivateOutputLinks(201);
                return;
              }
            }
            else
            {
              this.ActivateOutputLinks(201);
              return;
            }
          }
          this.ActivateOutputLinks(202);
          return;
        case 104:
          if (instance.VersusTowerMatchBegin)
          {
            this.ActivateOutputLinks(201);
            return;
          }
          this.ActivateOutputLinks(202);
          return;
        case 105:
          if (instance.VersusTowerMatchReceipt)
          {
            this.ActivateOutputLinks(201);
            return;
          }
          this.ActivateOutputLinks(202);
          return;
        case 106:
          if (instance.Player.VersusSeazonGiftReceipt)
          {
            this.ActivateOutputLinks(201);
            return;
          }
          this.ActivateOutputLinks(202);
          return;
        case 107:
          VsTowerMatchEndParam towerMatchEndParam = instance.GetVsTowerMatchEndParam();
          if (towerMatchEndParam != null)
          {
            VersusTowerParam versusTowerParam = instance.GetCurrentVersusTowerParam(towerMatchEndParam.floor);
            if (versusTowerParam != null && (int) versusTowerParam.RankupNum == 0 && !towerMatchEndParam.rankup)
            {
              this.ActivateOutputLinks(201);
              return;
            }
          }
          this.ActivateOutputLinks(202);
          return;
        case 108:
          if (instance.AudienceMode)
          {
            this.ActivateOutputLinks(201);
            return;
          }
          this.ActivateOutputLinks(202);
          return;
        case 109:
          if (instance.IsVSCpuBattle)
          {
            this.ActivateOutputLinks(201);
            return;
          }
          this.ActivateOutputLinks(202);
          return;
        case 110:
          instance.IsVSCpuBattle = true;
          break;
        case 111:
          long matchExpiredTime = instance.RankMatchExpiredTime;
          long num = TimeManager.FromDateTime(TimeManager.ServerTime);
          instance.RankMatchBegin = num < matchExpiredTime;
          if (instance.RankMatchBegin)
          {
            this.ActivateOutputLinks(201);
            return;
          }
          this.ActivateOutputLinks(202);
          return;
        case 112:
          GlobalVars.SelectedMultiPlayVersusType = VERSUS_TYPE.RankMatch;
          break;
        case 113:
          if (instance.VSDraftType == VersusDraftType.Draft)
          {
            this.ActivateOutputLinks(201);
            return;
          }
          this.ActivateOutputLinks(202);
          return;
        case 114:
          if (instance.VSDraftType == VersusDraftType.Draft || instance.VSDraftType == VersusDraftType.DraftFriend)
          {
            this.ActivateOutputLinks(201);
            return;
          }
          this.ActivateOutputLinks(202);
          return;
      }
      this.ActivateOutputLinks(200);
    }
  }
}
