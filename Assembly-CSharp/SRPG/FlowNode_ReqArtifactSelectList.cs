// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqArtifactSelectList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqArtifactSelectList", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ReqArtifactSelectList : FlowNode_Network
  {
    public ArtifactSelectListData mArtifactSelectListData;
    public GetArtifactWindow mGetArtifactWindow;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        MailData mail = MonoSingleton<GameManager>.Instance.FindMail((long) GlobalVars.SelectedMailUniqueID);
        if (mail == null)
        {
          ((Behaviour) this).set_enabled(false);
        }
        else
        {
          ((Behaviour) this).set_enabled(true);
          this.ExecRequest((WebAPI) new ReqMailSelect(mail.Find(GiftTypes.SelectArtifactItem).iname, ReqMailSelect.type.artifact, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
      }
      else
      {
        this.Deserialize(this.DummyResponse());
        this.Success();
      }
    }

    private Json_ArtifactSelectResponse DummyResponse()
    {
      string[] strArray = new string[1]{ "AF_ARMS_SWO_MITHRIL_GREEN" };
      int length = strArray.Length;
      Json_ArtifactSelectResponse artifactSelectResponse = new Json_ArtifactSelectResponse();
      artifactSelectResponse.select = new Json_ArtifactSelectItem[length];
      if (Object.op_Equality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
      }
      for (int index = 0; index < length; ++index)
        artifactSelectResponse.select[index] = new Json_ArtifactSelectItem()
        {
          iname = strArray[index]
        };
      return artifactSelectResponse;
    }

    private void Deserialize(Json_ArtifactSelectResponse json)
    {
      this.mArtifactSelectListData = new ArtifactSelectListData();
      this.mArtifactSelectListData.Deserialize(json);
      this.mGetArtifactWindow.Refresh(this.mArtifactSelectListData.items.ToArray());
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_ArtifactSelectResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArtifactSelectResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          for (int index = 0; index < jsonObject.body.select.Length; ++index)
          {
            Json_ArtifactSelectItem artifactSelectItem = jsonObject.body.select[index];
            if ((int) artifactSelectItem.num > 1)
              DebugUtility.LogError("武具は一つしか付与できません " + artifactSelectItem.iname);
          }
          this.Deserialize(jsonObject.body);
          this.Success();
        }
      }
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }
  }
}
