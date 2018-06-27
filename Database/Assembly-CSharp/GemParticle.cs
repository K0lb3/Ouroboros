// Decompiled with JetBrains decompiler
// Type: GemParticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class GemParticle : MonoBehaviour
{
  public GemParticle Prefab;
  [NonSerialized]
  public Transform TargetObject;
  [NonSerialized]
  public Vector3 TargetOffset;
  public float Speed;
  public float Damping;
  public float Delay;
  public float Accel;
  private float mSpeed;
  private float mDamping;
  private float mDelay;
  private float mAccel;
  private GemParticle.UpdateEvent mOnUpdate;

  public GemParticle()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.Reset();
  }

  public void Reset()
  {
    this.mDelay = this.Delay * Random.Range(0.9f, 1.1f);
    this.mSpeed = this.Speed * Random.Range(0.9f, 1.1f);
    this.mDamping = this.Damping * Random.Range(0.9f, 1.1f);
    this.mAccel = this.Accel * Random.Range(0.9f, 1.1f);
    this.mOnUpdate = new GemParticle.UpdateEvent(this.MoveToMidPoint);
    ParticleSystem[] componentsInChildren1 = (ParticleSystem[]) ((Component) this).get_gameObject().GetComponentsInChildren<ParticleSystem>(true);
    for (int index = 0; index < componentsInChildren1.Length; ++index)
    {
      ParticleSystem.EmissionModule emission = componentsInChildren1[index].get_emission();
      // ISSUE: explicit reference operation
      ((ParticleSystem.EmissionModule) @emission).set_enabled(true);
    }
    if (!Object.op_Inequality((Object) this.Prefab, (Object) null))
      return;
    ParticleSystem[] componentsInChildren2 = (ParticleSystem[]) ((Component) this.Prefab).GetComponentsInChildren<ParticleSystem>(true);
    for (int index = 0; index < componentsInChildren2.Length && index < componentsInChildren1.Length; ++index)
      componentsInChildren1[index].set_loop(componentsInChildren2[index].get_loop());
  }

  private void MoveToMidPoint()
  {
    this.mSpeed = Mathf.Max(this.mSpeed - this.mDamping * Time.get_deltaTime(), 0.0f);
    this.mDelay -= Time.get_deltaTime();
    if ((double) this.mDelay > 0.0)
      return;
    this.mOnUpdate = new GemParticle.UpdateEvent(this.MoveToGoal);
  }

  private void MoveToGoal()
  {
    this.mSpeed += this.mAccel * Time.get_deltaTime();
    Vector3 vector3 = Vector3.op_Subtraction(Vector3.op_Addition(this.TargetObject.get_position(), this.TargetOffset), ((Component) this).get_transform().get_position());
    float num = this.mSpeed * Time.get_deltaTime();
    if ((double) Vector3.Dot(vector3, vector3) <= (double) num * (double) num)
    {
      GameUtility.StopEmitters(((Component) this).get_gameObject());
      this.mSpeed = 0.0f;
      this.mOnUpdate = new GemParticle.UpdateEvent(this.DelayedDeactivate);
    }
    else
      ((Component) this).get_transform().set_rotation(Quaternion.LookRotation(vector3));
  }

  private void DelayedDeactivate()
  {
    foreach (ParticleSystem componentsInChild in (ParticleSystem[]) ((Component) this).GetComponentsInChildren<ParticleSystem>())
    {
      if (componentsInChild.get_particleCount() > 0)
        return;
    }
    this.mOnUpdate = (GemParticle.UpdateEvent) null;
    ((Component) this).get_gameObject().SetActive(false);
  }

  private void Update()
  {
    if (this.mOnUpdate == null)
      return;
    this.mOnUpdate();
    Transform transform1 = ((Component) this).get_transform();
    Transform transform2 = transform1;
    transform2.set_position(Vector3.op_Addition(transform2.get_position(), Vector3.op_Multiply(Vector3.op_Multiply(transform1.get_forward(), this.mSpeed), Time.get_deltaTime())));
  }

  private delegate void UpdateEvent();
}
