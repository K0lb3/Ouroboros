// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "成功", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(0, "メール取得", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqMail", 32741)]
  public class FlowNode_ReqMail : FlowNode_Network
  {
    private const int PIN_ID_REQUEST = 0;
    private const int PIN_ID_SUCCESS = 10;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MailWindow.MailPageRequestData dataOfClass = DataSource.FindDataOfClass<MailWindow.MailPageRequestData>(((Component) this).get_gameObject(), (MailWindow.MailPageRequestData) null);
      this.ExecRequest((WebAPI) new ReqMail(dataOfClass.page, dataOfClass.isPeriod, dataOfClass.isRead, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoMail:
            this.OnBack();
            break;
          case Network.EErrCode.MailReadable:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_MailPage> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_MailPage>>(www.text);
        MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.mails);
        if (jsonObject.body.mails != null)
          GlobalVars.ConceptCardNum.Set(jsonObject.body.mails.concept_count);
        this.ActivateOutputLinks(10);
        Network.RemoveAPI();
      }
    }
  }
}
