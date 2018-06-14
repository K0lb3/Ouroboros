// Decompiled with JetBrains decompiler
// Type: SRPG.CameraShakeEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class CameraShakeEffect : MonoBehaviour
  {
    private float mSeedX;
    private float mSeedY;
    private float mTime;
    public float Duration;
    public float FrequencyX;
    public float FrequencyY;
    public float AmplitudeX;
    public float AmplitudeY;

    public CameraShakeEffect()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      this.mSeedX = Random.get_value();
      this.mSeedY = Random.get_value();
    }

    private void Update()
    {
      this.mTime += Time.get_deltaTime();
      if ((double) this.mTime < (double) this.Duration)
        return;
      Object.Destroy((Object) this);
    }

    private void OnPreCull()
    {
      float num = 1f - Mathf.Clamp01(this.mTime / this.Duration);
      ((Component) this).get_transform().set_rotation(Quaternion.op_Multiply(((Component) this).get_transform().get_rotation(), Quaternion.op_Multiply(Quaternion.AngleAxis(Mathf.Sin((float) (((double) Time.get_time() + (double) this.mSeedX) * (double) this.FrequencyX * 3.14159274101257)) * this.AmplitudeX * num, Vector3.get_up()), Quaternion.AngleAxis(Mathf.Sin((float) (((double) Time.get_time() + (double) this.mSeedY) * (double) this.FrequencyY * 3.14159274101257)) * this.AmplitudeY * num, Vector3.get_right()))));
    }
  }
}
