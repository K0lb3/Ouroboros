// Decompiled with JetBrains decompiler
// Type: SliderSound
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Audio/Slider Sound")]
public class SliderSound : MonoBehaviour
{
  public string cueID;
  public float WaitSec;
  private float mValueMin;
  private float mValueMax;
  private float mWait;

  public SliderSound()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
  }

  private void OnEnable()
  {
  }

  private void OnDisable()
  {
  }

  private void Update()
  {
    if ((double) this.mWait <= 0.0)
      return;
    this.mWait -= Time.get_unscaledDeltaTime();
  }

  public void OnValueChanged()
  {
    if ((double) this.mWait > 0.0)
      return;
    this.mWait = this.WaitSec;
    Slider component = (Slider) ((Component) this).get_gameObject().GetComponent<Slider>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    if ((double) this.mValueMin != (double) component.get_minValue() || (double) this.mValueMax != (double) component.get_maxValue())
    {
      this.mValueMin = component.get_minValue();
      this.mValueMax = component.get_maxValue();
    }
    else
      this.Play();
  }

  public void Play()
  {
    MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, 0.0f);
  }
}
