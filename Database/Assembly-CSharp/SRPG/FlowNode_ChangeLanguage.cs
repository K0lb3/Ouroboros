// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ChangeLanguage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Change To English", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Localization/ChangeLanguage", 32741)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(102, "Send API Set Language", FlowNode.PinTypes.Input, 6)]
  [FlowNode.Pin(101, "Change To System Language", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(5, "Change To Spanish", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(4, "Change To German", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(3, "Change To French", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(2, "Change To Japanese", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_ChangeLanguage : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      string str = "english";
      switch (pinID)
      {
        case 1:
          str = "english";
          break;
        case 2:
          str = "japanese";
          break;
        case 3:
          str = "french";
          break;
        case 4:
          str = "german";
          break;
        case 5:
          str = "spanish";
          break;
        case 101:
          SystemLanguage systemLanguage = Application.get_systemLanguage();
          switch (systemLanguage - 10)
          {
            case 0:
              str = "english";
              break;
            case 4:
              str = "french";
              break;
            case 5:
              str = "german";
              break;
            default:
              if (systemLanguage == 34)
              {
                str = "spanish";
                break;
              }
              break;
          }
      }
      if (pinID == 102)
      {
        this.ExecRequest((WebAPI) new ReqSetLanguage(GameUtility.Config_Language, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
      {
        GameUtility.Config_Language = str;
        LocalizedText.LanguageCode = str;
        LocalizedText.UnloadAll();
        this.ActivateOutputLinks(10);
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          this.ActivateOutputLinks(10);
        }
      }
    }
  }
}
