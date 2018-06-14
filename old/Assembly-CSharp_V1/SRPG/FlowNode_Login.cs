// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Login
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(4, "Reset to Title", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(6, "No Login until Specified Date", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(5, "No Login Indefinitely", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(3, "Success To ReqBtlCom", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(2, "Success To SetName", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(1, "Success To PlayNew", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(10, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/Login", 32741)]
  public class FlowNode_Login : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 10)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        MonoSingleton<GameManager>.Instance.Player.ClearUnits();
        MonoSingleton<GameManager>.Instance.Player.ClearItems();
        this.ExecRequest((WebAPI) new ReqLogin(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(3);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(4);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        if (jsonObject.body == null)
        {
          ((Behaviour) this).set_enabled(false);
          PlayerPrefs.SetInt("lastplv", 0);
          PlayerPrefs.SetInt("lastviplv", 0);
          MonoSingleton<GameManager>.Instance.Player.ClearTrophies();
          this.ActivateOutputLinks(1);
        }
        else
        {
          GlobalVars.CustomerID = jsonObject.body.cuid;
          int status = jsonObject.body.status;
          if (status != 0)
          {
            GlobalVars.BanStatus = jsonObject.body.status;
            if (status == 1)
              this.ActivateOutputLinks(5);
            else if (jsonObject.body.status > 1)
              this.ActivateOutputLinks(6);
            ((Behaviour) this).set_enabled(false);
          }
          else
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            long lastConnectionTime = Network.LastConnectionTime;
            instance.Player.LoginDate = TimeManager.FromUnixTime(lastConnectionTime);
            instance.Player.TutorialFlags = jsonObject.body.tut;
            try
            {
              instance.Deserialize(jsonObject.body.player);
              instance.Deserialize(jsonObject.body.units);
              instance.Deserialize(jsonObject.body.items);
              instance.Deserialize(jsonObject.body.parties);
              instance.Deserialize(jsonObject.body.notify);
              instance.Deserialize(jsonObject.body.artifacts, false);
              instance.Deserialize(jsonObject.body.skins);
              instance.Deserialize(jsonObject.body.vs);
            }
            catch (Exception ex)
            {
              DebugUtility.LogException(ex);
              this.Failure();
              return;
            }
            if (Object.op_Inequality((Object) this, (Object) null))
              ((Behaviour) this).set_enabled(false);
            GlobalVars.BtlID.Set(jsonObject.body.player.btlid);
            if (!string.IsNullOrEmpty(jsonObject.body.player.btltype))
              GlobalVars.QuestType = QuestParam.ToQuestType(jsonObject.body.player.btltype);
            GameUtility.Config_OkyakusamaCode = instance.Player.OkyakusamaCode;
            if (!PlayerPrefs.HasKey("lastplv"))
              PlayerPrefs.SetInt("lastplv", MonoSingleton<GameManager>.Instance.Player.Lv);
            if (!PlayerPrefs.HasKey("lastviplv"))
              PlayerPrefs.SetInt("lastviplv", MonoSingleton<GameManager>.Instance.Player.VipRank);
            instance.PostLogin();
            this.ActivateOutputLinks(!string.IsNullOrEmpty(jsonObject.body.player.name) ? 3 : 2);
          }
        }
      }
    }
  }
}
