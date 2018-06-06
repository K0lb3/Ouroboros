// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LoginMethod
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Facebook.MiniJSON;
using Facebook.Unity;
using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(16, "FB Already Linked", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(0, "Guest Login", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "FB Login", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "FB Invite", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "Guest Login Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Guest Login Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "FB Login Success", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(13, "FB Login Failed", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(14, "Register FB To Device", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(15, "No Account", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(17, "Linked with other FB", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(18, "Logged out", FlowNode.PinTypes.Output, 18)]
  [FlowNode.NodeType("Network/Login_Method", 32741)]
  public class FlowNode_LoginMethod : FlowNode_Network
  {
    private string Status;
    private string LastResponse;
    private string LastResponseTexture;
    private bool isFacebookInvite;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.GuestLogin();
          break;
        case 1:
          this.FacebookLogin();
          break;
        case 2:
          this.isFacebookInvite = true;
          this.FacebookLogin();
          break;
      }
    }

    private void GuestLogin()
    {
      this.ActivateOutputLinks(10);
    }

    private void FacebookLogin()
    {
      if (MonoSingleton<GameManager>.Instance.IsDeviceId())
      {
        if (!FB.get_IsInitialized())
        {
          // ISSUE: method pointer
          // ISSUE: method pointer
          FB.Init(new InitDelegate((object) this, __methodptr(OnInitComplete)), new HideUnityDelegate((object) this, __methodptr(OnHideUnity)), (string) null);
          this.Status = "FB.Init() called with " + FB.get_AppId();
        }
        else
          this.CallFBLogin();
      }
      else
        this.ActivateOutputLinks(15);
    }

    private void CallFBLogin()
    {
      if (PlayerPrefs.HasKey("AccountLinked") && PlayerPrefs.GetInt("AccountLinked") == 1)
      {
        if (this.isFacebookInvite)
        {
          this.ActivateOutputLinks(16);
        }
        else
        {
          FB.LogOut();
          PlayerPrefs.SetInt("AccountLinked", 0);
          this.ActivateOutputLinks(18);
        }
      }
      else
      {
        // ISSUE: method pointer
        FB.LogInWithReadPermissions((IEnumerable<string>) new List<string>()
        {
          "public_profile",
          "email",
          "user_friends"
        }, new FacebookDelegate<ILoginResult>((object) this, __methodptr(HandleResult)));
      }
    }

    private void OnInitComplete()
    {
      this.Status = "Success - Check log for details";
      this.LastResponse = "Success Response: OnInitComplete Called\n";
      Debug.LogError((object) string.Format("OnInitCompleteCalled IsLoggedIn='{0}' IsInitialized='{1}'", (object) FB.get_IsLoggedIn(), (object) FB.get_IsInitialized()));
      this.CallFBLogin();
      this.Status = "Login called";
    }

    private void OnHideUnity(bool isGameShown)
    {
      this.Status = "Success - Check log for details";
      this.LastResponse = string.Format("Success Response: OnHideUnity Called {0}\n", (object) isGameShown);
      Debug.Log((object) ("Is game shown: " + (object) isGameShown));
    }

    protected void HandleResult(IResult result)
    {
      if (result == null)
      {
        this.LastResponse = "Null Response\n";
        Debug.Log((object) this.LastResponse);
        this.ActivateOutputLinks(13);
      }
      else
      {
        this.LastResponseTexture = (string) null;
        if (!string.IsNullOrEmpty(result.get_Error()))
        {
          this.Status = "Error - Check log for details";
          this.LastResponse = "Error Response:\n" + result.get_Error();
          if (AccessToken.get_CurrentAccessToken() != null)
          {
            FB.LogOut();
            this.CallFBLogin();
          }
          else
          {
            Debug.LogError((object) result.get_Error());
            this.ActivateOutputLinks(13);
          }
        }
        else if (result.get_Cancelled())
        {
          this.Status = "Cancelled - Check log for details";
          this.LastResponse = "Cancelled Response:\n" + result.get_RawResult();
          Debug.Log((object) result.get_RawResult());
          this.ActivateOutputLinks(13);
        }
        else if (!string.IsNullOrEmpty(result.get_RawResult()))
        {
          this.Status = "Success - Check log for details";
          this.LastResponse = "Success Response:\n" + result.get_RawResult();
          Debug.Log((object) result.get_RawResult());
          string accesstoken = (string) (Json.Deserialize(result.get_RawResult()) as Dictionary<string, object>)["access_token"];
          GlobalVars.OAuth2AccessToken = accesstoken;
          this.ExecRequest((WebAPI) new ReqAttachFacebookToDevice(accesstoken, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).set_enabled(true);
        }
        else
        {
          this.LastResponse = "Empty Response\n";
          Debug.Log((object) this.LastResponse);
          this.ActivateOutputLinks(13);
        }
      }
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
        WebAPI.JSON_BodyResponse<Json_AttachFacebookToDeviceResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_AttachFacebookToDeviceResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          if (jsonObject.body.linked == 1)
          {
            if (jsonObject.body.device_id == MonoSingleton<GameManager>.Instance.DeviceId && jsonObject.body.secret_key == MonoSingleton<GameManager>.Instance.SecretKey)
            {
              PlayerPrefs.SetInt("AccountLinked", 1);
              this.ActivateOutputLinks(16);
            }
            else
            {
              Facebook_Migrate.old_device_id = jsonObject.body.device_id;
              Facebook_Migrate.old_secret_key = jsonObject.body.secret_key;
              GlobalVars.NewPlayerName = jsonObject.body.name;
              GlobalVars.NewPlayerLevel = jsonObject.body.lv.ToString();
              this.ActivateOutputLinks(12);
            }
          }
          else
          {
            if (PlayerPrefs.HasKey("AccountLinked") && PlayerPrefs.GetInt("AccountLinked") == 1)
            {
              this.ActivateOutputLinks(17);
              return;
            }
            if (!string.IsNullOrEmpty(GlobalVars.FacebookID))
            {
              this.ActivateOutputLinks(17);
              return;
            }
            this.ActivateOutputLinks(14);
          }
          ((Behaviour) this).set_enabled(false);
        }
      }
    }
  }
}
