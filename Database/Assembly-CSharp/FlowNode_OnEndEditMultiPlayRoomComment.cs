// Decompiled with JetBrains decompiler
// Type: FlowNode_OnEndEditMultiPlayRoomComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[FlowNode.Pin(2, "Error", FlowNode.PinTypes.Output, 0)]
[AddComponentMenu("")]
[FlowNode.NodeType("Event/OnEndEditMultiPlayRoomComment", 58751)]
[FlowNode.Pin(1, "Edited", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnEndEditMultiPlayRoomComment : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (InputField), true)]
  public InputField Target;
  public bool CreateMode;

  private void Start()
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null))
      return;
    string str = string.Empty;
    MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
    JSON_MyPhotonRoomParam myPhotonRoomParam = currentRoom == null || string.IsNullOrEmpty(currentRoom.json) ? (JSON_MyPhotonRoomParam) null : JSON_MyPhotonRoomParam.Parse(currentRoom.json);
    if (myPhotonRoomParam != null)
      str = myPhotonRoomParam.comment;
    if (this.CreateMode)
      str = this.GetComment();
    this.Target.set_text(str);
    GlobalVars.EditMultiPlayRoomComment = str;
    // ISSUE: method pointer
    ((UnityEvent<string>) this.Target.get_onEndEdit()).AddListener(new UnityAction<string>((object) this, __methodptr(\u003CStart\u003Em__2B1)));
    ((Behaviour) this).set_enabled(true);
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    GUtility.SetImmersiveMove();
    if (!Object.op_Inequality((Object) this.Target, (Object) null) || this.Target.get_onEndEdit() == null)
      return;
    ((UnityEventBase) this.Target.get_onEndEdit()).RemoveAllListeners();
  }

  private void OnEndEdit(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.get_text().Length <= 0)
      return;
    if (this.CreateMode)
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, field.get_text(), false);
    DebugUtility.Log("OnEndEditRoomName:" + field.get_text());
    GlobalVars.EditMultiPlayRoomComment = field.get_text();
    this.Activate(1);
  }

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    this.ActivateOutputLinks(1);
  }

  private string GetComment()
  {
    string name;
    if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ROOM_COMMENT_KEY))
    {
      name = PlayerPrefsUtility.GetString(PlayerPrefsUtility.ROOM_COMMENT_KEY, string.Empty);
      if (string.IsNullOrEmpty(name))
        name = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
      if (!MyMsgInput.isLegal(name))
        name = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
    }
    else
      name = LocalizedText.Get("sys.DEFAULT_ROOM_COMMENT");
    return name;
  }
}
