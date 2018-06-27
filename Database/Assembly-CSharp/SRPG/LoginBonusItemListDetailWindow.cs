// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusItemListDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class LoginBonusItemListDetailWindow : MonoBehaviour
  {
    public LoginBonusItemListDetailWindow()
    {
      base.\u002Ector();
    }

    public void Refresh()
    {
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(GlobalVars.SelectedItemID);
      if (itemDataByItemId != null)
      {
        DataSource.Bind<ItemData>(((Component) this).get_gameObject(), itemDataByItemId);
      }
      else
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(GlobalVars.SelectedItemID);
        if (itemParam == null)
          return;
        DataSource.Bind<ItemParam>(((Component) this).get_gameObject(), itemParam);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      ((Behaviour) this).set_enabled(true);
    }
  }
}
