// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ExecGacha
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(20, "レアガチャ", FlowNode.PinTypes.Input, 20)]
  [FlowNode.NodeType("System/ExecGacha", 32741)]
  [FlowNode.Pin(10, "ノーマルガチャ", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "ノーマルガチャ10連", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(21, "レアガチャ10連", FlowNode.PinTypes.Input, 21)]
  [FlowNode.Pin(30, "VIPガチャ", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(4, "Success", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "Failed", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(6, "ゴールド不足", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(7, "コイン不足", FlowNode.PinTypes.Output, 7)]
  public class FlowNode_ExecGacha : FlowNode_Network
  {
    private static readonly int GACHA_GOLD_COST = 1000;
    private static readonly int GACHA_GOLD_MANY_COST = FlowNode_ExecGacha.GACHA_GOLD_COST * 9;
    private static readonly int GACHA_COIN_COST = 1;
    private static readonly int GACHA_COIN_MANY_COST = FlowNode_ExecGacha.GACHA_COIN_COST * 9;
    public string GachaScene_Normal;
    public string GachaScene_NormalX;
    public string GachaScene_Rare;
    public string GachaScene_RareX;
    public string GachaScene_VIP;
    private GachaOffline mGacha;
    private FlowNode_ExecGacha.GachaModes mGachaMode;
    private string[] mGachaResult;
    private bool mGachaResultSet;
    private GachaScene mGachaScene;

    public override void OnActivate(int pinID)
    {
      this.mGacha = new GachaOffline();
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      switch (pinID)
      {
        case 10:
          if (player.Gold < FlowNode_ExecGacha.GACHA_GOLD_COST)
          {
            this.ActivateOutputLinks(6);
            ((Behaviour) this).set_enabled(false);
            break;
          }
          this.ExecNormalGacha();
          break;
        case 11:
          if (player.Gold < FlowNode_ExecGacha.GACHA_GOLD_MANY_COST)
          {
            this.ActivateOutputLinks(6);
            ((Behaviour) this).set_enabled(false);
            break;
          }
          this.ExecNormalGachaMany();
          break;
        case 20:
          if (player.Coin < FlowNode_ExecGacha.GACHA_COIN_COST)
          {
            this.ActivateOutputLinks(7);
            ((Behaviour) this).set_enabled(false);
            break;
          }
          this.ExecRareGacha();
          break;
        case 21:
          if (player.Coin < FlowNode_ExecGacha.GACHA_COIN_MANY_COST)
          {
            this.ActivateOutputLinks(7);
            ((Behaviour) this).set_enabled(false);
            break;
          }
          this.ExecRareGachaMany();
          break;
        case 30:
          this.Success();
          break;
        default:
          this.Failure();
          break;
      }
    }

    private void OnClickYes(GameObject dialog)
    {
      this.Success();
    }

    private void ShowResultDialog(string result)
    {
      UIUtility.SystemMessage("獲得", result, new UIUtility.DialogResultEvent(this.OnClickYes), (GameObject) null, false, -1);
    }

    private void ExecNormalGacha()
    {
      this.StartGachaScene(FlowNode_ExecGacha.GachaModes.Normal);
      MonoSingleton<GameManager>.Instance.Player.OnGacha(GachaTypes.Normal, 1);
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        string itemID = this.mGacha.ExecGacha("Gacha_gold");
        MonoSingleton<GameManager>.Instance.Player.GainItem(itemID, 1);
        MonoSingleton<GameManager>.Instance.Player.DEBUG_ADD_GOLD(-FlowNode_ExecGacha.GACHA_GOLD_COST);
        this.SetGachaResult(new string[1]{ itemID });
      }
      else
        ((Behaviour) this).set_enabled(true);
    }

    private void ExecNormalGachaMany()
    {
      this.StartGachaScene(FlowNode_ExecGacha.GachaModes.NormalX);
      MonoSingleton<GameManager>.Instance.Player.OnGacha(GachaTypes.Normal, 10);
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        int length = 10;
        string[] items = new string[length];
        for (int index = 0; index < length; ++index)
        {
          string itemID = this.mGacha.ExecGacha("Gacha_gold");
          MonoSingleton<GameManager>.Instance.Player.GainItem(itemID, 1);
          items[index] = itemID;
        }
        DebugUtility.Log(FlowNode_ExecGacha.GACHA_GOLD_MANY_COST.ToString());
        MonoSingleton<GameManager>.Instance.Player.DEBUG_ADD_GOLD(-FlowNode_ExecGacha.GACHA_GOLD_MANY_COST);
        this.SetGachaResult(items);
      }
      else
        ((Behaviour) this).set_enabled(true);
    }

    private void ExecRareGacha()
    {
      this.StartGachaScene(FlowNode_ExecGacha.GachaModes.Rare);
      MonoSingleton<GameManager>.Instance.Player.OnGacha(GachaTypes.Rare, 1);
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        string unitID = this.mGacha.ExecGacha("Gacha_kakin");
        MonoSingleton<GameManager>.Instance.Player.GainUnit(unitID);
        MonoSingleton<GameManager>.Instance.Player.DEBUG_CONSUME_COIN(FlowNode_ExecGacha.GACHA_COIN_COST);
        this.SetGachaResult(new string[1]{ unitID });
      }
      else
        ((Behaviour) this).set_enabled(true);
    }

    private void ExecRareGachaMany()
    {
      this.StartGachaScene(FlowNode_ExecGacha.GachaModes.RareX);
      MonoSingleton<GameManager>.Instance.Player.OnGacha(GachaTypes.Rare, 10);
      if (Network.Mode == Network.EConnectMode.Offline)
      {
        int length = 10;
        string[] items = new string[length];
        for (int index = 0; index < length; ++index)
        {
          string unitID = this.mGacha.ExecGacha("Gacha_kakin");
          MonoSingleton<GameManager>.Instance.Player.GainUnit(unitID);
          items[index] = unitID;
        }
        MonoSingleton<GameManager>.Instance.Player.DEBUG_CONSUME_COIN(FlowNode_ExecGacha.GACHA_COIN_MANY_COST);
        this.SetGachaResult(items);
      }
      else
        ((Behaviour) this).set_enabled(true);
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(4);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(5);
    }

    private string MakeResultString(Json_DropInfo[] drops)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      UnitParam[] allUnits = masterParam.GetAllUnits();
      string str = string.Empty;
      foreach (Json_DropInfo drop in drops)
      {
        ItemParam itemParam = masterParam.GetItemParam(drop.iname);
        if (itemParam != null)
        {
          str = str + itemParam.name + "\n";
        }
        else
        {
          foreach (UnitParam unitParam in allUnits)
          {
            if (unitParam.iname == drop.iname)
            {
              str = str + unitParam.name + "\n";
              break;
            }
          }
        }
      }
      return str;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoGacha:
            this.OnFailed();
            break;
          case Network.EErrCode.GachaCostShort:
            this.OnBack();
            break;
          case Network.EErrCode.GachaItemMax:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_GachaResult> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_GachaResult>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          this.Failure();
          return;
        }
        string[] items = new string[jsonObject.body.add.Length];
        for (int index = 0; index < jsonObject.body.add.Length; ++index)
          items[index] = jsonObject.body.add[index].iname;
        this.SetGachaResult(items);
        this.Success();
      }
    }

    private void SetGachaResult(string[] items)
    {
      this.mGachaResult = items;
      this.mGachaResultSet = true;
    }

    private void StartGachaScene(FlowNode_ExecGacha.GachaModes mode)
    {
      CriticalSection.Enter(CriticalSections.SceneChange);
      this.mGachaMode = mode;
      this.mGachaResultSet = false;
      ((Behaviour) this).set_enabled(true);
      this.StartCoroutine(this.StartGachaSceneAsync(mode));
    }

    [DebuggerHidden]
    private IEnumerator StartGachaSceneAsync(FlowNode_ExecGacha.GachaModes mode)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ExecGacha.\u003CStartGachaSceneAsync\u003Ec__Iterator84() { \u003C\u003Ef__this = this };
    }

    private void OnGachaSceneLoad(GameObject scene)
    {
      this.mGachaScene = (GachaScene) scene.GetComponent<GachaScene>();
      if (Object.op_Inequality((Object) this.mGachaScene, (Object) null))
        SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnGachaSceneLoad));
      CriticalSection.Leave(CriticalSections.SceneChange);
    }

    private enum GachaModes
    {
      Normal,
      NormalX,
      Rare,
      RareX,
      VIP,
    }
  }
}
