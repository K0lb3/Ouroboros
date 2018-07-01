// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ParticleGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ParticleGenerator : AnimEventWithTarget
  {
    public GameObject Template;
    public bool Attach;
    public bool NotParticle;

    public override void OnStart(GameObject go)
    {
      if (Object.op_Equality((Object) this.Template, (Object) null))
        return;
      Vector3 spawnPos;
      Quaternion spawnRot;
      this.CalcPosition(go, this.Template, out spawnPos, out spawnRot);
      GameObject go1 = (GameObject) Object.Instantiate((Object) this.Template, spawnPos, spawnRot);
      if (!this.NotParticle)
        GameUtility.RequireComponent<OneShotParticle>(go1);
      if (go.get_transform().get_lossyScale().x * go.get_transform().get_lossyScale().z < 0.0)
      {
        Vector3 localScale = go1.get_transform().get_localScale();
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local = @localScale;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).z = (__Null) ((^local).z * -1.0);
        go1.get_transform().set_localScale(localScale);
      }
      if (this.Attach && !string.IsNullOrEmpty(this.BoneName))
      {
        Transform transform = GameUtility.findChildRecursively(go.get_transform(), this.BoneName);
        if (this.BoneName == "CAMERA" && Object.op_Implicit((Object) Camera.get_main()))
          transform = ((Component) Camera.get_main()).get_transform();
        if (Object.op_Inequality((Object) transform, (Object) null))
          go1.get_transform().SetParent(transform);
      }
      this.OnGenerate(go1);
      if ((double) this.End <= (double) this.Start + 0.100000001490116)
        return;
      DestructTimer destructTimer = GameUtility.RequireComponent<DestructTimer>(go1);
      if (!Object.op_Implicit((Object) destructTimer))
        return;
      destructTimer.Timer = this.End - this.Start;
    }

    protected virtual void OnGenerate(GameObject go)
    {
    }
  }
}
