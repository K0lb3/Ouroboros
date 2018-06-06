// Decompiled with JetBrains decompiler
// Type: SRPG.GachaButtonSort
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class GachaButtonSort : MonoBehaviour
  {
    [BitMask]
    public GameManager.BadgeTypes BadgeType;

    public GachaButtonSort()
    {
      base.\u002Ector();
    }

    private void Update()
    {
      this.UpdateButtonPlacement();
    }

    private void UpdateButtonPlacement()
    {
      if (this.BadgeType == ~GameManager.BadgeTypes.All)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (Object.op_Equality((Object) instanceDirect, (Object) null) || instanceDirect.CheckBusyBadges(this.BadgeType))
        return;
      if (this.BadgeType == GameManager.BadgeTypes.RareGacha)
      {
        if (MonoSingleton<GameManager>.GetInstanceDirect().CheckBadges(this.BadgeType))
          ((Component) this).get_transform().SetAsFirstSibling();
        else
          ((Component) this).get_transform().SetSiblingIndex(((Component) this).get_transform().get_parent().get_childCount() - 2);
      }
      else
      {
        if (this.BadgeType != GameManager.BadgeTypes.GoldGacha)
          return;
        if (MonoSingleton<GameManager>.GetInstanceDirect().CheckBadges(this.BadgeType))
        {
          if (MonoSingleton<GameManager>.GetInstanceDirect().CheckBadges(GameManager.BadgeTypes.RareGacha))
            ((Component) this).get_transform().SetSiblingIndex(1);
          else
            ((Component) this).get_transform().SetAsFirstSibling();
        }
        else
          ((Component) this).get_transform().SetSiblingIndex(((Component) this).get_transform().get_parent().get_childCount() - 2);
      }
    }
  }
}
