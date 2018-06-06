// Decompiled with JetBrains decompiler
// Type: SRPG.TriggerCameraShake
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TriggerCameraShake : MonoBehaviour
  {
    public float Duration;
    public float FrequencyX;
    public float FrequencyY;
    public float AmplitudeX;
    public float AmplitudeY;

    public TriggerCameraShake()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      Camera main = Camera.get_main();
      if (Object.op_Inequality((Object) main, (Object) null))
      {
        CameraShakeEffect cameraShakeEffect = (CameraShakeEffect) ((Component) main).get_gameObject().AddComponent<CameraShakeEffect>();
        cameraShakeEffect.Duration = this.Duration;
        cameraShakeEffect.FrequencyX = this.FrequencyX;
        cameraShakeEffect.FrequencyY = this.FrequencyY;
        cameraShakeEffect.AmplitudeX = this.AmplitudeX;
        cameraShakeEffect.AmplitudeY = this.AmplitudeY;
      }
      Object.Destroy((Object) this);
    }
  }
}
