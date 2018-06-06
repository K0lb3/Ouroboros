// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleAlchemicPower
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ToggleAlchemicPower : AnimEvent
  {
    private static readonly Color SceneFadeColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    public bool Invert;

    public override void OnStart(GameObject go)
    {
      this.SetRenderMode(go, !this.Invert ? 0.0f : 1f);
    }

    public override void OnEnd(GameObject go)
    {
      this.SetRenderMode(go, !this.Invert ? 1f : 0.0f);
    }

    public override void OnTick(GameObject go, float ratio)
    {
      this.SetRenderMode(go, !this.Invert ? ratio : 1f - ratio);
    }

    private void SetRenderMode(GameObject go, float strength)
    {
      UnitController componentInParent = (UnitController) go.GetComponentInParent<UnitController>();
      if (Object.op_Inequality((Object) componentInParent, (Object) null))
        componentInParent.AnimateVessel(strength, 0.0f);
      TacticsUnitController[] array = (TacticsUnitController[]) null;
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
      {
        array = SceneBattle.Instance.GetActiveUnits();
        Array.Resize<TacticsUnitController>(ref array, array.Length + 1);
        array[array.Length - 1] = (TacticsUnitController) componentInParent;
      }
      FadeController.Instance.BeginSceneFade(Color.Lerp(Color.get_white(), ToggleAlchemicPower.SceneFadeColor, strength), 0.0f, array, (TacticsUnitController[]) null);
    }
  }
}
