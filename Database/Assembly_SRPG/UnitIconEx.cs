// Decompiled with JetBrains decompiler
// Type: SRPG.UnitIconEx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
      GameObject root = (GameObject) Object.Instantiate<GameObject>((M0) AssetManager.Load<GameObject>(!string.IsNullOrEmpty(this.GeneralTooltipPath) ? this.GeneralTooltipPath : "UI/UnitTooltip.prefab"));
      DataSource.Bind<UnitData>(root, instanceData);
      GameParameter.UpdateAll(root);
    }
  }
}
