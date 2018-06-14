// Decompiled with JetBrains decompiler
// Type: FlowNode_OnUrlSchemeLaunch
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

[FlowNode.Pin(102, "StartCheck", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "OnMultiPlayLINE", FlowNode.PinTypes.Output, 0)]
[AddComponentMenu("")]
[FlowNode.NodeType("Event/OnUrlSchemeLaunch", 58751)]
public class FlowNode_OnUrlSchemeLaunch : FlowNodePersistent
{
  public static FlowNode_OnUrlSchemeLaunch.LINEParam LINEParam_Pending = new FlowNode_OnUrlSchemeLaunch.LINEParam();
  public static FlowNode_OnUrlSchemeLaunch.LINEParam LINEParam_decided = new FlowNode_OnUrlSchemeLaunch.LINEParam();
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
      if (Object.op_Equality((Object) gameObject, (Object) null))
      {
        DebugUtility.Log("obj is null");
        return false;
      }
      FlowNode_OnUrlSchemeLaunch component = (FlowNode_OnUrlSchemeLaunch) gameObject.GetComponent<FlowNode_OnUrlSchemeLaunch>();
      if (Object.op_Equality((Object) component, (Object) null))
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
    if (pinID != 102)
      return;
    this.StartCheck = true;
    this.StartExec = false;
    DebugUtility.Log("UrlScheme StartCheck!");
  }

  private void Update()
  {
    if (this.StartExec)
    {
      this.UpdatePendingParam();
    }
    else
    {
      if (!MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.MultiPlay) || !this.StartCheck || (!FlowNode_GetCurrentScene.IsAfterLogin() || Object.op_Equality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null)) || (string.IsNullOrEmpty(MonoSingleton<GameManager>.GetInstanceDirect().Player.Name) || Object.op_Equality((Object) EventSystem.get_current(), (Object) null) || (Object.op_Equality((Object) EventSystem.get_current().get_currentInputModule(), (Object) null) || !((Behaviour) EventSystem.get_current().get_currentInputModule()).get_enabled())) || (Network.IsConnecting || BlockInterrupt.IsBlocked(BlockInterrupt.EType.URL_SCHEME_LAUNCH) || (CriticalSection.IsActive || !GlobalVars.IsTutorialEnd)))
        return;
      if (GameUtility.GetCurrentScene() == GameUtility.EScene.HOME)
      {
        HomeWindow current = HomeWindow.Current;
        if (Object.op_Equality((Object) current, (Object) null) || !current.IsReadyInTown)
          return;
      }
      if (!this.UpdatePendingParam())
        return;
      this.StartCheck = false;
      this.StartExec = true;
      DebugUtility.Log("UrlScheme MultiPlayLINE start. CheckEnd!" + (object) (FlowNode_OnUrlSchemeLaunch.LINEParam_Pending != null));
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
