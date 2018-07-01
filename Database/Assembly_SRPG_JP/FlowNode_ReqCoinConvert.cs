// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqCoinConvert
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqCoinConvert", 32741)]
  [FlowNode.Pin(10, "次に進む", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(4, "コインはコンバートされなかった", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(3, "旧コインのコンバートが実行された", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(2, "新コインのコンバートが実行された", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "新旧コインのコンバートが実行された", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqCoinConvert : FlowNode_Network
  {
    private const int CONVERTED_NEW_OLD = 1;
    private const int CONVERTED_NEW = 2;
    private const int CONVERTED_OLD = 3;
    private const int DO_NOTHING = 4;
    private const int NEXT = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqCoinConvert(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        WebAPI.JSON_BodyResponse<FlowNode_ReqCoinConvert.JSON_ConvertedCoin> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqCoinConvert.JSON_ConvertedCoin>>(www.text);
        if (jsonObject == null)
        {
          this.ActivateOutputLinks(1);
        }
        else
        {
          FlowNode_ReqCoinConvert.JSON_ConvertedCoin body = jsonObject.body;
          GlobalVars.SummonCoinInfo summonCoinInfo1 = new GlobalVars.SummonCoinInfo();
          summonCoinInfo1.ConvertedSummonCoin = body.oldcoin.coin;
          summonCoinInfo1.ReceivedStone = body.oldcoin.stone;
          summonCoinInfo1.ConvertedDate = (long) body.oldcoin.convdate;
          GlobalVars.OldSummonCoinInfo = summonCoinInfo1;
          GlobalVars.SummonCoinInfo summonCoinInfo2 = new GlobalVars.SummonCoinInfo();
          summonCoinInfo2.ConvertedSummonCoin = body.newcoin.coin;
          summonCoinInfo2.ReceivedStone = body.newcoin.stone;
          summonCoinInfo2.SummonCoinStock = body.newcoin.stock;
          summonCoinInfo2.Period = (long) body.newcoin.period;
          summonCoinInfo2.ConvertedDate = (long) body.newcoin.convdate;
          GlobalVars.NewSummonCoinInfo = summonCoinInfo2;
          if (body.item != null && body.item.Length > 0)
            MonoSingleton<GameManager>.Instance.Player.Deserialize(body.item);
          if (summonCoinInfo1.ConvertedSummonCoin > 0 && summonCoinInfo2.ConvertedSummonCoin > 0)
            this.ActivateOutputLinks(1);
          else if (summonCoinInfo1.ConvertedSummonCoin > 0)
            this.ActivateOutputLinks(3);
          else if (summonCoinInfo2.ConvertedSummonCoin > 0)
            this.ActivateOutputLinks(2);
          else
            this.ActivateOutputLinks(4);
          this.ActivateOutputLinks(10);
        }
      }
    }

    private class JSON_ConvertedCoin
    {
      public FlowNode_ReqCoinConvert.JSON_OldCoin oldcoin;
      public FlowNode_ReqCoinConvert.JSON_NewCoin newcoin;
      public Json_Item[] item;
    }

    private class JSON_NewCoin
    {
      public int coin;
      public int stone;
      public int stock;
      public int period;
      public int convdate;
    }

    private class JSON_OldCoin
    {
      public int coin;
      public int stone;
      public int convdate;
    }
  }
}
