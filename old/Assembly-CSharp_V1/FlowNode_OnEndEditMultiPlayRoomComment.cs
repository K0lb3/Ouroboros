// Decompiled with JetBrains decompiler
// Type: FlowNode_OnEndEditMultiPlayRoomComment
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[FlowNode.Pin(1, "Edited", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/OnEndEditMultiPlayRoomComment", 58751)]
[AddComponentMenu("")]
public class FlowNode_OnEndEditMultiPlayRoomComment : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (InputField), true)]
  public InputField Target;

  private void Start()
  {
    if (!Object.op_Inequality((Object) this.Target, (Object) null))
      return;
    string empty = string.Empty;
    MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
    JSON_MyPhotonRoomParam myPhotonRoomParam = currentRoom == null || string.IsNullOrEmpty(currentRoom.json) ? (JSON_MyPhotonRoomParam) null : JSON_MyPhotonRoomParam.Parse(currentRoom.json);
    string str = myPhotonRoomParam != null ? myPhotonRoomParam.comment : string.Empty;
    this.Target.set_text(str);
    GlobalVars.EditMultiPlayRoomComment = str;
    // ISSUE: method pointer
    ((UnityEvent<string>) this.Target.get_onEndEdit()).AddListener(new UnityAction<string>((object) this, __methodptr(\u003CStart\u003Em__1FC)));
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
}
