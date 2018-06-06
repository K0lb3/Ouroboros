// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqMail
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqMail", 32741)]
  [FlowNode.Pin(0, "メール取得", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "成功", FlowNode.PinTypes.Output, 10)]
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
        MonoSingleton<GameManager>.Instance.Player.Deserialize(JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_MailPage>>(www.text).body.mails);
        this.ActivateOutputLinks(10);
        Network.RemoveAPI();
      }
    }
  }
}
