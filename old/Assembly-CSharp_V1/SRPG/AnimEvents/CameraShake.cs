// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.CameraShake
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class CameraShake : AnimEvent
  {
    public float FrequencyX = 10f;
    public float FrequencyY = 10f;
    public float AmplitudeX = 1f;
    public float AmplitudeY = 1f;

    public Quaternion CalcOffset(float time, float randX, float randY)
    {
      float num = (float) (1.0 - ((double) this.Start >= (double) this.End ? 0.0 : ((double) time - (double) this.Start) / ((double) this.End - (double) this.Start)));
      return Quaternion.op_Multiply(Quaternion.AngleAxis(Mathf.Sin((float) (((double) time + (double) randX) * (double) this.FrequencyX * 3.14159274101257)) * this.AmplitudeX * num, Vector3.get_up()), Quaternion.AngleAxis(Mathf.Sin((float) (((double) time + (double) randY) * (double) this.FrequencyY * 3.14159274101257)) * this.AmplitudeY * num, Vector3.get_right()));
    }
  }
}
