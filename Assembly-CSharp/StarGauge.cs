// Decompiled with JetBrains decompiler
// Type: StarGauge
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[AddComponentMenu("UI/Star Gauge")]
public class StarGauge : MonoBehaviour
{
  [Description("明るい星の雛形となるゲームオブジェクト")]
  public GameObject StarTemplate;
  [Description("暗い星の雛形となるゲームオブジェクト")]
  public GameObject SlotTemplate;
  private int mValue;
  private int mValueMax;
  private GameObject[] mStars;

  public StarGauge()
  {
    base.\u002Ector();
  }

  public int Value
  {
    get
    {
      return this.mValue;
    }
    set
    {
      value = Mathf.Min(value, this.mValueMax);
      if (this.mValue == value)
        return;
      this.mValue = value;
      ((Behaviour) this).set_enabled(true);
    }
  }

  public int Max
  {
    get
    {
      return this.mValueMax;
    }
    set
    {
      value = Mathf.Max(value, 0);
      if (this.mValueMax == value)
        return;
      this.mValueMax = value;
      ((Behaviour) this).set_enabled(true);
    }
  }

  private void LateUpdate()
  {
    if (this.mStars != null)
    {
      for (int index = 0; index < this.mStars.Length; ++index)
        Object.Destroy((Object) this.mStars[index]);
      this.mStars = (GameObject[]) null;
    }
    if (this.mValueMax > 0)
    {
      this.mStars = new GameObject[this.mValueMax];
      Transform transform = ((Component) this).get_transform();
      if (Object.op_Inequality((Object) this.StarTemplate, (Object) null))
      {
        for (int index = 0; index < this.mValue; ++index)
        {
          this.mStars[index] = (GameObject) Object.Instantiate<GameObject>((M0) this.StarTemplate);
          this.mStars[index].get_transform().SetParent(transform, false);
          this.mStars[index].SetActive(true);
        }
      }
      if (Object.op_Inequality((Object) this.SlotTemplate, (Object) null))
      {
        for (int mValue = this.mValue; mValue < this.mValueMax; ++mValue)
        {
          this.mStars[mValue] = (GameObject) Object.Instantiate<GameObject>((M0) this.SlotTemplate);
          this.mStars[mValue].get_transform().SetParent(transform, false);
          this.mStars[mValue].SetActive(true);
        }
      }
    }
    ((Behaviour) this).set_enabled(false);
  }
}
