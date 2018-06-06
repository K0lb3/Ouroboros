// Decompiled with JetBrains decompiler
// Type: SRPG.FriendDetailWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("")]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class FriendDetailWindow : MonoBehaviour, IFlowInterface
  {
    public FriendDetailWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      this.bind();
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void bind()
    {
      FriendData selectedFriend = GlobalVars.SelectedFriend;
      if (selectedFriend != null)
        DataSource.Bind<FriendData>(((Component) this).get_gameObject(), selectedFriend);
      UnitData unit = selectedFriend.Unit;
      if (unit == null)
        return;
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unit);
    }

    private void Awake()
    {
      this.bind();
    }
  }
}
