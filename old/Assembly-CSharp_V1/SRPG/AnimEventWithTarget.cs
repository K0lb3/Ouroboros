// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEventWithTarget
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG.AnimEvents;
using UnityEngine;

namespace SRPG
{
  public abstract class AnimEventWithTarget : AnimEvent
  {
    public string BoneName = string.Empty;
    public Vector3 Offset = Vector3.get_zero();
    public Vector3 Rotation = Vector3.get_zero();
    public bool LocalOffset = true;
    public bool LocalRotation = true;
    protected const string BONE_NAME_CAMERA = "CAMERA";

    public void CalcPosition(GameObject go, GameObject prefab, out Vector3 spawnPos, out Quaternion spawnRot)
    {
      this.CalcPosition(go, prefab.get_transform().get_localPosition(), prefab.get_transform().get_localRotation(), out spawnPos, out spawnRot);
    }

    public void CalcPosition(GameObject go, Vector3 deltaOffset, Quaternion deltaRotation, out Vector3 spawnPos, out Quaternion spawnRot)
    {
      spawnPos = Vector3.op_Addition(this.Offset, deltaOffset);
      spawnRot = Quaternion.op_Multiply(Quaternion.Euler((float) this.Rotation.x, (float) this.Rotation.y, (float) this.Rotation.z), deltaRotation);
      Transform transform = string.IsNullOrEmpty(this.BoneName) ? go.get_transform() : GameUtility.findChildRecursively(go.get_transform(), this.BoneName);
      if (this is ParticleGenerator && this.BoneName == "CAMERA" && Object.op_Implicit((Object) Camera.get_main()))
        transform = ((Component) Camera.get_main()).get_transform();
      if (!Object.op_Inequality((Object) transform, (Object) null))
        return;
      spawnPos = !this.LocalOffset ? Vector3.op_Addition(transform.TransformPoint(Vector3.get_zero()), spawnPos) : transform.TransformPoint(spawnPos);
      if (!this.LocalRotation)
        return;
      spawnRot = Quaternion.op_Multiply(transform.get_rotation(), spawnRot);
    }
  }
}
