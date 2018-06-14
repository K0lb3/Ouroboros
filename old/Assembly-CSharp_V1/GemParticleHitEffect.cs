// Decompiled with JetBrains decompiler
// Type: GemParticleHitEffect
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class GemParticleHitEffect : MonoBehaviour
{
  private Vector3 mStartPosition;
  public static bool IsEnable;
  public GameObject EffectPrefab;

  public GemParticleHitEffect()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.mStartPosition = ((Component) this).get_transform().get_position();
  }

  private void Update()
  {
    if (!GemParticleHitEffect.IsEnable || Object.op_Equality((Object) this.EffectPrefab, (Object) null))
      return;
    GemParticle component = (GemParticle) ((Component) this).get_gameObject().GetComponent<GemParticle>();
    if (!Object.op_Inequality((Object) component, (Object) null) || !Object.op_Inequality((Object) component.TargetObject, (Object) null))
      return;
    Vector3 vector3_1 = Vector3.op_Addition(component.TargetObject.get_position(), component.TargetOffset);
    Vector3 vector3_2 = Vector3.op_Subtraction(vector3_1, this.mStartPosition);
    // ISSUE: explicit reference operation
    float magnitude = ((Vector3) @vector3_2).get_magnitude();
    Vector3 vector3_3 = Vector3.op_Subtraction(vector3_1, ((Component) this).get_transform().get_position());
    // ISSUE: explicit reference operation
    if (0.200000002980232 <= (double) ((Vector3) @vector3_3).get_magnitude() / (double) magnitude)
      return;
    GameUtility.RequireComponent<OneShotParticle>(Object.Instantiate((Object) this.EffectPrefab, vector3_1, Quaternion.get_identity()) as GameObject);
    GemParticleHitEffect.IsEnable = false;
  }

  private void OnDisable()
  {
    GemParticleHitEffect.IsEnable = false;
    Object.Destroy((Object) this);
  }
}
