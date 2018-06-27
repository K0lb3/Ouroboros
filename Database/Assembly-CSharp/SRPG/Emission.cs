// Decompiled with JetBrains decompiler
// Type: SRPG.Emission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class Emission : MonoBehaviour
  {
    public AnimationCurve AnimationCurve;
    public float Delay;
    public float Duration;
    public Image Image;
    private float m_Factor;
    private float m_Time;

    public Emission()
    {
      base.\u002Ector();
    }

    private void Update()
    {
      float deltaTime = Time.get_deltaTime();
      this.m_Time += deltaTime;
      if ((double) this.m_Time < (double) this.Delay)
      {
        this.m_Factor = 0.0f;
      }
      else
      {
        this.m_Factor += deltaTime;
        float num1 = (double) this.Duration > 0.0 ? this.m_Factor / this.Duration : 1f;
        if ((double) num1 >= 1.0)
          this.m_Factor = 0.0f;
        float num2 = this.AnimationCurve.Evaluate(Mathf.Clamp01(num1));
        if (!Object.op_Inequality((Object) this.Image, (Object) null))
          return;
        Color color = ((Graphic) this.Image).get_color();
        color.a = (__Null) (double) num2;
        ((Graphic) this.Image).set_color(color);
        ((Behaviour) this.Image).set_enabled(true);
      }
    }
  }
}
