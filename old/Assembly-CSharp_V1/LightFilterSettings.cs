// Decompiled with JetBrains decompiler
// Type: LightFilterSettings
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("")]
[ExecuteInEditMode]
public class LightFilterSettings : MonoBehaviour
{
  public float AOSampleRadius;
  public float AOExponent;
  public float AOStrength;
  public Gradient AOGradient;
  public bool UseAmbientOcclusion;

  public LightFilterSettings()
  {
    base.\u002Ector();
  }

  public static LightFilterSettings Current
  {
    get
    {
      foreach (LightFilterSettings lightFilterSettings in (LightFilterSettings[]) Object.FindObjectsOfType<LightFilterSettings>())
      {
        if (((Component) lightFilterSettings).get_gameObject().get_activeInHierarchy())
          return lightFilterSettings;
      }
      return (LightFilterSettings) new GameObject(nameof (LightFilterSettings)).AddComponent<LightFilterSettings>();
    }
  }

  private void Awake()
  {
    ((Object) ((Component) this).get_transform()).set_hideFlags((HideFlags) 2);
    ((Behaviour) this).set_enabled(false);
    ((Component) this).set_tag("EditorOnly");
    ((Object) ((Component) this).get_gameObject()).set_hideFlags((HideFlags) 1);
    if (this.AOGradient != null && this.AOGradient.get_colorKeys().Length > 1)
      return;
    GradientColorKey[] gradientColorKeyArray = new GradientColorKey[2]{ new GradientColorKey(Color.get_black(), 0.0f), new GradientColorKey(Color.get_white(), 1f) };
    GradientAlphaKey[] gradientAlphaKeyArray = new GradientAlphaKey[2]{ new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(1f, 1f) };
    this.AOGradient = new Gradient();
    this.AOGradient.SetKeys(gradientColorKeyArray, gradientAlphaKeyArray);
  }
}
