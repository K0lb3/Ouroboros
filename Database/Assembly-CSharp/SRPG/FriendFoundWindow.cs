// Decompiled with JetBrains decompiler
// Type: SRPG.FriendFoundWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class FriendFoundWindow : MonoBehaviour
  {
    public FriendFoundWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      FriendData foundFriend = GlobalVars.FoundFriend;
      if (foundFriend != null)
        DataSource.Bind<FriendData>(((Component) this).get_gameObject(), foundFriend);
      UnitData unit = foundFriend.Unit;
      if (unit == null)
        return;
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unit);
    }
  }
}
