// Decompiled with JetBrains decompiler
// Type: SRPG.FriendFoundWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
