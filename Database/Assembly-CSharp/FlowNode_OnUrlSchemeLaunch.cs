// Decompiled with JetBrains decompiler
// Type: FlowNode_OnUrlSchemeLaunch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

[FlowNode.Pin(1, "OnMultiPlayLINE", FlowNode.PinTypes.Output, 1)]
[FlowNode.Pin(102, "StartCheck", FlowNode.PinTypes.Input, 102)]
[FlowNode.NodeType("Event/OnUrlSchemeLaunch", 58751)]
[AddComponentMenu("")]
[FlowNode.Pin(999, "OnDebug", FlowNode.PinTypes.Input, 999)]
[FlowNode.Pin(2, "OnQRCodeAccess", FlowNode.PinTypes.Output, 2)]
public class FlowNode_OnUrlSchemeLaunch : FlowNodePersistent
{
  public static FlowNode_OnUrlSchemeLaunch.LINEParam LINEParam_Pending = new FlowNode_OnUrlSchemeLaunch.LINEParam();
  public static FlowNode_OnUrlSchemeLaunch.LINEParam LINEParam_decided = new FlowNode_OnUrlSchemeLaunch.LINEParam();
  public static bool IsQRAccess = false;
  public static int QRCampaignID = -1;
  public static string QRSerialID = string.Empty;
  private bool StartCheck;
  private bool StartExec;

  private static bool IsEqual(string s0, string s1)
  {
    if (string.IsNullOrEmpty(s0))
      return string.IsNullOrEmpty(s1);
    return !string.IsNullOrEmpty(s1) && s0.Equals(s1);
  }

  public static bool IsExecuting
  {
    get
    {
      if (!string.IsNullOrEmpty(MonoSingleton<UrlScheme>.Instance.ParamString))
      {
        DebugUtility.Log("param is not null");
        return true;
      }
      GameObject gameObject = GameObject.Find("UrlSchemeObserver(Clone)");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      {
        DebugUtility.Log("obj is null");
        return false;
      }
      FlowNode_OnUrlSchemeLaunch component = (FlowNode_OnUrlSchemeLaunch) gameObject.GetComponent<FlowNode_OnUrlSchemeLaunch>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
      {
        DebugUtility.Log("instance is null");
        return false;
      }
      return !component.StartCheck;
    }
  }

  private void Start()
  {
    ((Behaviour) this).set_enabled(true);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID == 102)
    {
      this.StartCheck = true;
      this.StartExec = false;
      DebugUtility.Log("UrlScheme StartCheck!");
    }
    else
    {
      if (pinID != 999)
        return;
      MonoSingleton<UrlScheme>.Instance.ParamString = "jp.co.gu3.alchemist://join?param=cXJhY2Nlc3M9MSZjYW1wYWlnbmlkPTEmc2VyaWFsaWQ9MWFiY2RlZmdoaWpr";
    }
  }

  private void Update()
  {
    if (this.StartExec)
    {
      this.UpdatePendingParam();
    }
    else
    {
      if (!this.StartCheck || !FlowNode_GetCurrentScene.IsAfterLogin() || (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null) || string.IsNullOrEmpty(MonoSingleton<GameManager>.GetInstanceDirect().Player.Name)) || (UnityEngine.Object.op_Equality((UnityEngine.Object) EventSystem.get_current(), (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) EventSystem.get_current().get_currentInputModule(), (UnityEngine.Object) null) || (!((Behaviour) EventSystem.get_current().get_currentInputModule()).get_enabled() || Network.IsConnecting)) || (BlockInterrupt.IsBlocked(BlockInterrupt.EType.URL_SCHEME_LAUNCH) || CriticalSection.IsActive || !GlobalVars.IsTutorialEnd))
        return;
      if (GameUtility.GetCurrentScene() == GameUtility.EScene.HOME)
      {
        HomeWindow current = HomeWindow.Current;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) current, (UnityEngine.Object) null) || !current.IsReadyInTown)
          return;
      }
      if (!this.UpdatePendingParam() || !FlowNode_OnUrlSchemeLaunch.IsQRAccess && !MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.MultiPlay))
        return;
      this.StartCheck = false;
      this.StartExec = true;
      if (FlowNode_OnUrlSchemeLaunch.IsQRAccess)
      {
        DebugUtility.Log("ActivatedOutPutLinks(2) => QRCode");
        this.ActivateOutputLinks(2);
      }
      else
        this.ActivateOutputLinks(1);
    }
  }

  private bool UpdatePendingParam()
  {
    string paramString = MonoSingleton<UrlScheme>.Instance.ParamString;
    if (string.IsNullOrEmpty(paramString))
      return false;
    MonoSingleton<UrlScheme>.Instance.ParamString = (string) null;
    DebugUtility.Log("OnUrlSchemeLaunch:" + paramString);
    string[] schemeParams = this.ParseSchemeParams(paramString, JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY);
    int num = 0;
    FlowNode_OnUrlSchemeLaunch.IsQRAccess = false;
    FlowNode_OnUrlSchemeLaunch.QRCampaignID = -1;
    FlowNode_OnUrlSchemeLaunch.QRSerialID = string.Empty;
    if (schemeParams != null && schemeParams.Length == 3)
    {
      DebugUtility.Log("=== QRCode->ParameterSet START ===");
      foreach (string str in schemeParams)
      {
        char[] chArray = new char[1]{ '=' };
        string[] strArray = str.Split(chArray);
        if (strArray == null || strArray.Length != 2)
          return false;
        if (strArray[0].Equals("qraccess"))
        {
          FlowNode_OnUrlSchemeLaunch.IsQRAccess = !string.IsNullOrEmpty(strArray[1]) && !(strArray[1] == "0");
          DebugUtility.Log("qraccess = " + strArray[1]);
          num |= 1;
        }
        else if (strArray[0].Equals("campaignid"))
        {
          FlowNode_OnUrlSchemeLaunch.QRCampaignID = int.Parse(strArray[1]);
          DebugUtility.Log("campaignid = " + strArray[1]);
          num |= 2;
        }
        else if (strArray[0].Equals("serialid"))
        {
          FlowNode_OnUrlSchemeLaunch.QRSerialID = strArray[1];
          DebugUtility.Log("serialid = " + strArray[1]);
          num |= 4;
        }
      }
      DebugUtility.Log("=== QRCode->ParameterSet END ===");
      for (int index = 0; index < 3; ++index)
      {
        if ((num & 1 << index) == 0)
        {
          DebugUtility.Log("=== QRCode->ParameterSet FAILED ===");
          FlowNode_OnUrlSchemeLaunch.IsQRAccess = false;
          FlowNode_OnUrlSchemeLaunch.QRCampaignID = -1;
          FlowNode_OnUrlSchemeLaunch.QRSerialID = string.Empty;
          return false;
        }
      }
      DebugUtility.Log("=== QRCode->ParameterSet SUCCESS ===");
      return true;
    }
    FlowNode_OnUrlSchemeLaunch.LINEParam lineParam = this.Analyze(paramString);
    if (lineParam == null)
      return false;
    if (FlowNode_OnUrlSchemeLaunch.LINEParam_decided != null && FlowNode_OnUrlSchemeLaunch.LINEParam_decided.Equals((object) lineParam))
    {
      DebugUtility.Log("Checking param is same...");
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState == MyPhoton.MyState.ROOM)
      {
        DebugUtility.Log("in room.");
        MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
        JSON_MyPhotonRoomParam myPhotonRoomParam = currentRoom != null ? JSON_MyPhotonRoomParam.Parse(currentRoom.json) : (JSON_MyPhotonRoomParam) null;
        if (myPhotonRoomParam.isLINE == 0)
          DebugUtility.Log("not LINE.");
        else if (!FlowNode_OnUrlSchemeLaunch.IsEqual(myPhotonRoomParam.iname, lineParam.iname))
          DebugUtility.Log("iname is not match.");
        else if (!FlowNode_OnUrlSchemeLaunch.IsEqual(myPhotonRoomParam.creatorFUID, lineParam.creatorFUID))
          DebugUtility.Log("creatorFUID is not match.");
        else if ((JSON_MyPhotonRoomParam.EType) myPhotonRoomParam.type != lineParam.type)
        {
          DebugUtility.Log("type is not match.");
        }
        else
        {
          DebugUtility.Log("UrlScheme MultiPlayLINE start skip. same param.");
          return false;
        }
      }
    }
    DebugUtility.Log("UrlScheme MultiPlayLINE start get pending." + (object) (lineParam != null));
    FlowNode_OnUrlSchemeLaunch.LINEParam_Pending = lineParam;
    return true;
  }

  private string[] ParseSchemeParams(string str, int encode_key = -1)
  {
    if (string.IsNullOrEmpty(str) || encode_key == -1)
      return (string[]) null;
    string[] strArray1 = str.Split('?');
    if (strArray1 == null || strArray1.Length <= 1 || strArray1[1] == null)
      return (string[]) null;
    string[] strArray2 = strArray1[1].Split('=');
    if (strArray2 == null || strArray2.Length <= 1 || strArray2[1] == null)
      return (string[]) null;
    string s = WWW.UnEscapeURL(strArray2[1]);
    byte[] data;
    try
    {
      data = Convert.FromBase64String(s);
    }
    catch
    {
      DebugUtility.LogError("UrlScheme invalid param");
      return (string[]) null;
    }
    return MyEncrypt.Decrypt(encode_key, data, false).Split('&');
  }

  private void CreateQRCode()
  {
    string str = "jp.co.gu3.alchemist://join?param=";
    string msg = string.Empty + "qraccess=" + (object) 1 + "&" + "campaignid=" + (object) 1 + "&" + "serialid=1abcdefghijk";
    byte[] inArray = MyEncrypt.Encrypt(JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY, msg, false);
    Debug.Log((object) ("URLScheme=>" + (str + WWW.EscapeURL(Convert.ToBase64String(inArray)))));
  }

  private FlowNode_OnUrlSchemeLaunch.LINEParam Analyze(string str)
  {
    string[] strArray1 = str.Split('?');
    if (strArray1 == null)
      return (FlowNode_OnUrlSchemeLaunch.LINEParam) null;
    if (strArray1.Length <= 1 || strArray1[1] == null)
      return (FlowNode_OnUrlSchemeLaunch.LINEParam) null;
    string s = WWW.UnEscapeURL(strArray1[1].Split('=')[1]);
    byte[] data;
    try
    {
      data = Convert.FromBase64String(s);
    }
    catch
    {
      DebugUtility.LogError("UrlScheme invalid param");
      return (FlowNode_OnUrlSchemeLaunch.LINEParam) null;
    }
    string[] strArray2 = MyEncrypt.Decrypt(JSON_MyPhotonRoomParam.LINE_PARAM_ENCODE_KEY, data, false).Split('&');
    if (strArray2 == null || strArray2.Length != 4)
      return (FlowNode_OnUrlSchemeLaunch.LINEParam) null;
    int num = 0;
    FlowNode_OnUrlSchemeLaunch.LINEParam lineParam = new FlowNode_OnUrlSchemeLaunch.LINEParam();
    foreach (string str1 in strArray2)
    {
      char[] chArray = new char[1]{ '=' };
      string[] strArray3 = str1.Split(chArray);
      if (strArray3 == null || strArray3.Length != 2)
        return (FlowNode_OnUrlSchemeLaunch.LINEParam) null;
      if (strArray3[0].Equals("iname"))
      {
        lineParam.iname = strArray3[1];
        num |= 1;
      }
      else if (strArray3[0].Equals("type"))
      {
        lineParam.type = (JSON_MyPhotonRoomParam.EType) int.Parse(strArray3[1]);
        num |= 2;
      }
      else if (strArray3[0].Equals("creatorFUID"))
      {
        lineParam.creatorFUID = strArray3[1];
        num |= 4;
      }
      else if (strArray3[0].Equals("roomid"))
      {
        lineParam.roomid = int.Parse(strArray3[1]);
        num |= 8;
      }
    }
    for (int index = 0; index < 4; ++index)
    {
      if ((num & 1 << index) == 0)
        return (FlowNode_OnUrlSchemeLaunch.LINEParam) null;
    }
    return lineParam;
  }

  public class LINEParam
  {
    public string iname;
    public JSON_MyPhotonRoomParam.EType type;
    public string creatorFUID;
    public int roomid;

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override bool Equals(object tgt)
    {
      if (tgt == null)
        return false;
      FlowNode_OnUrlSchemeLaunch.LINEParam lineParam = tgt as FlowNode_OnUrlSchemeLaunch.LINEParam;
      return lineParam != null && this.type == lineParam.type && (FlowNode_OnUrlSchemeLaunch.IsEqual(this.iname, lineParam.iname) && FlowNode_OnUrlSchemeLaunch.IsEqual(this.creatorFUID, lineParam.creatorFUID)) && this.roomid == lineParam.roomid;
    }
  }
}
