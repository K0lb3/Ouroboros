// Decompiled with JetBrains decompiler
// Type: SRPG.UnitIconEx
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitIconEx : UnitIcon
  {
    private const string DefaultTootTipPath = "UI/UnitTooltip.prefab";
    public string GeneralTooltipPath;

    protected override void ShowTooltip(Vector2 screen)
    {
      if (!this.Tooltip)
        return;
      UnitData instanceData = this.GetInstanceData();
      if (instanceData == null)
        return;
      DataSource.Bind<UnitData>((GameObject) Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>(!string.IsNullOrEmpty(this.GeneralTooltipPath) ? this.GeneralTooltipPath : "UI/UnitTooltip.prefab")), instanceData);
    }
  }
}
