// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqArtifactFavorite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Add Success", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "お気に入り 削除", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(3, "Remove Success", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(0, "お気に入り 追加", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/Artifact/Favorite", 32741)]
  public class FlowNode_ReqArtifactFavorite : FlowNode_Network
  {
    private const int PIN_ID_FAVORITE_ADD = 0;
    private const int PIN_ID_FAVORITE_REMOVE = 1;
    private const int PIN_ID_FAVORITE_ADD_SUCCESS = 2;
    private const int PIN_ID_FAVORITE_REMOVE_SUCCESS = 3;
    private bool success;

    public override void OnActivate(int pinID)
    {
      ((Behaviour) this).set_enabled(true);
      this.success = false;
      switch (pinID)
      {
        case 0:
          ArtifactData dataOfClass1 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).get_gameObject(), (ArtifactData) null);
          if (dataOfClass1 == null)
          {
            DebugUtility.LogWarning("ArtifactDataがバインドされていません");
            break;
          }
          this.ExecRequest((WebAPI) new ReqArtifactFavorite((long) dataOfClass1.UniqueID, true, new Network.ResponseCallback(this.OnFavoriteAdd)));
          break;
        case 1:
          ArtifactData dataOfClass2 = DataSource.FindDataOfClass<ArtifactData>(((Component) this).get_gameObject(), (ArtifactData) null);
          if (dataOfClass2 == null)
          {
            DebugUtility.LogWarning("ArtifactDataがバインドされていません");
            break;
          }
          this.ExecRequest((WebAPI) new ReqArtifactFavorite((long) dataOfClass2.UniqueID, false, new Network.ResponseCallback(this.OnFavoriteRemove)));
          break;
        default:
          ((Behaviour) this).set_enabled(false);
          break;
      }
    }

    private void Success()
    {
      this.success = true;
      ((Behaviour) this).set_enabled(false);
    }

    public void OnFavoriteRemove(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      if (!this.success)
        return;
      this.ActivateOutputLinks(2);
    }

    public void OnFavoriteAdd(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      this.OnSuccess(www);
      if (!this.success)
        return;
      this.ActivateOutputLinks(3);
    }

    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_ArtifactFavorite> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArtifactFavorite>>(www.text);
      DebugUtility.Assert(jsonObject != null, "res == null");
      if (Network.IsError)
        this.OnRetry();
      else if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, true);
        }
        catch (Exception ex)
        {
          this.OnRetry(ex);
          return;
        }
        Network.RemoveAPI();
        this.Success();
      }
    }
  }
}
