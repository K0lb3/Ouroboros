// Decompiled with JetBrains decompiler
// Type: MultiInvitationBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class MultiInvitationBadge : MonoBehaviour
{
  public AnimationCurve AnimationCurve;
  public float Delay;
  public float Duration;
  private Image m_Image;
  private bool m_StartAnimation;
  private float m_Factor;
  private float m_Time;

  public MultiInvitationBadge()
  {
    base.\u002Ector();
  }

  public static bool isValid { set; get; }

  private void Awake()
  {
    this.m_Image = (Image) ((Component) this).get_gameObject().GetComponent<Image>();
  }

  private void Start()
  {
    if (Object.op_Inequality((Object) this.m_Image, (Object) null))
    {
      Color color = ((Graphic) this.m_Image).get_color();
      color.a = (__Null) 1.0;
      ((Graphic) this.m_Image).set_color(color);
      ((Behaviour) this.m_Image).set_enabled(false);
    }
    this.m_StartAnimation = false;
    this.m_Time = 0.0f;
    this.m_Factor = 0.0f;
  }

  private void Update()
  {
    if (MultiInvitationBadge.isValid)
      this.Play();
    else
      this.Stop();
    if (!this.m_StartAnimation)
      return;
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
      if (!Object.op_Inequality((Object) this.m_Image, (Object) null))
        return;
      Color color = ((Graphic) this.m_Image).get_color();
      color.a = (__Null) (double) num2;
      ((Graphic) this.m_Image).set_color(color);
      ((Behaviour) this.m_Image).set_enabled(true);
    }
  }

  public void Play()
  {
    if (this.m_StartAnimation)
      return;
    this.m_StartAnimation = true;
    this.m_Time = 0.0f;
    this.m_Factor = 0.0f;
  }

  public void Stop()
  {
    if (!this.m_StartAnimation)
      return;
    this.m_StartAnimation = false;
    this.m_Time = 0.0f;
    this.m_Factor = 0.0f;
    if (!Object.op_Inequality((Object) this.m_Image, (Object) null))
      return;
    ((Behaviour) this.m_Image).set_enabled(false);
  }
}
