// Decompiled with JetBrains decompiler
// Type: UIParticleSystem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIParticleSystem : Graphic
{
  public UIParticleSystem.ParticleUpdateMode updateMode;
  public UIParticleSystem.EmitterTypes emitterType;
  public UIParticleSystem.ConeEmitter coneEmitter;
  public UIParticleSystem.SphereEmitter sphereEmitter;
  public UIParticleSystem.BoxEmitter boxEmitter;
  private List<UIParticleSystem.Particle> mParticles;
  private List<UIParticleSystem.Particle> mDeadParticles;
  private float mPrevTime;
  private float mTime;
  public UIParticleSystem.ParticleRenderMode RenderMode;
  public UIParticleSystem.StretchBillboard m_StretchBillboard;
  [UIParticleSystem.Particle]
  public float duration;
  [UIParticleSystem.Particle]
  public float emissionRate;
  [UIParticleSystem.Particle]
  public float gravityMultipler;
  [UIParticleSystem.Particle]
  public bool loop;
  [UIParticleSystem.Particle]
  public int maxParticles;
  [UIParticleSystem.Particle]
  public float playbackSpeed;
  [UIParticleSystem.Particle]
  public UIParticleSystem.ColorRange startColor;
  [UIParticleSystem.Particle]
  public float startDelay;
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startLifetime;
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startRotation;
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startSize;
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange startSpeed;
  [UIParticleSystem.Particle]
  public float emitterRotation;
  [UIParticleSystem.Particle]
  public UIParticleSystem.FloatRange angularVelocity;
  public bool angularVelocityEnable;
  [UIParticleSystem.Particle]
  public AnimationCurve rotationOverLifetime;
  public bool rotationOverLifetimeEnable;
  [UIParticleSystem.Particle]
  public Gradient colorOverLifetime;
  public bool colorOverLifetimeEnable;
  [UIParticleSystem.Particle]
  public AnimationCurve sizeOverLifetime;
  public bool sizeOverLifetimeEnable;
  [UIParticleSystem.Particle]
  public UIParticleSystem.TextureSheetAnimation textureSheetAnimation;
  public bool textureSheetAnimationEnable;
  [UIParticleSystem.Particle]
  public UIParticleSystem.ParticleBurst burst;
  public bool burstEnable;
  [UIParticleSystem.Particle]
  public UIParticleSystem.VelocityOverLifetime velocityOverLifetime;
  public bool velocityOverLifetimeEnable;
  [NonSerialized]
  public bool IsPlaying;
  [NonSerialized]
  public bool emit;
  private float mSpawnCount;

  public UIParticleSystem()
  {
    base.\u002Ector();
  }

  public int particleCount
  {
    get
    {
      return this.mParticles.Count;
    }
  }

  public float PlaybackTime
  {
    get
    {
      return this.mTime;
    }
    set
    {
      this.mTime = value;
    }
  }

  public bool IsAlive()
  {
    if (this.particleCount <= 0 && (double) this.mTime >= (double) this.duration)
      return this.loop;
    return true;
  }

  protected virtual void Start()
  {
    ((UIBehaviour) this).Start();
    this.ResetParticleSystem();
  }

  public virtual bool Raycast(Vector2 sp, Camera eventCamera)
  {
    return false;
  }

  public virtual Texture mainTexture
  {
    get
    {
      if (Object.op_Implicit((Object) this.get_material()))
        return this.get_material().get_mainTexture();
      return base.get_mainTexture();
    }
  }

  public void ResetParticleSystem()
  {
    this.IsPlaying = true;
    this.emit = true;
    this.mTime = -this.startDelay;
    this.mParticles.Clear();
  }

  public void ResumeEmitters()
  {
    foreach (UIParticleSystem componentsInChild in (UIParticleSystem[]) ((Component) this).GetComponentsInChildren<UIParticleSystem>())
      componentsInChild.IsPlaying = true;
  }

  public void PauseEmitters()
  {
    foreach (UIParticleSystem componentsInChild in (UIParticleSystem[]) ((Component) this).GetComponentsInChildren<UIParticleSystem>())
      componentsInChild.IsPlaying = false;
  }

  public void ResetEmitters()
  {
    UIParticleSystem[] componentsInChildren = (UIParticleSystem[]) ((Component) this).GetComponentsInChildren<UIParticleSystem>();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      componentsInChildren[index].ResetParticleSystem();
      componentsInChildren[index].IsPlaying = false;
      componentsInChildren[index].UpdateGeometry();
    }
  }

  public void StopEmitters()
  {
    UIParticleSystem[] componentsInChildren = (UIParticleSystem[]) ((Component) this).GetComponentsInChildren<UIParticleSystem>();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      componentsInChildren[index].emit = false;
      componentsInChildren[index].loop = false;
    }
  }

  private void Update()
  {
    if (!this.IsPlaying)
      return;
    switch (this.updateMode)
    {
      case UIParticleSystem.ParticleUpdateMode.UnscaledTime:
        this.AdvanceTime(Time.get_unscaledDeltaTime());
        break;
      case UIParticleSystem.ParticleUpdateMode.GameTime:
        this.AdvanceTime(Time.get_deltaTime());
        break;
      case UIParticleSystem.ParticleUpdateMode.FixedTime:
        this.AdvanceTime(Time.get_fixedDeltaTime());
        break;
    }
  }

  public void AdvanceTime(float dt)
  {
    dt *= this.playbackSpeed;
    this.mPrevTime = this.mTime;
    this.mTime += dt;
    if ((double) this.mTime >= (double) this.duration)
    {
      if (this.loop)
      {
        this.mTime = 0.0f;
        this.mPrevTime = 0.0f;
      }
      else
        this.mTime = this.duration;
    }
    for (int index = this.mParticles.Count - 1; index >= 0; --index)
    {
      UIParticleSystem.Particle mParticle = this.mParticles[index];
      mParticle.lifetime -= dt;
      if ((double) mParticle.lifetime <= 0.0)
      {
        this.mParticles.RemoveAt(index);
        this.mDeadParticles.Add(mParticle);
      }
      else
      {
        float num1 = (float) (1.0 - (double) mParticle.lifetime / (double) mParticle.startLifetime);
        UIParticleSystem.Particle particle1 = mParticle;
        particle1.position = Vector3.op_Addition(particle1.position, Vector3.op_Multiply(mParticle.velocity, dt));
        mParticle.rotation += mParticle.angularVelocity * dt;
        if ((double) this.gravityMultipler > 0.0)
        {
          UIParticleSystem.Particle particle2 = mParticle;
          particle2.velocity = Vector3.op_Addition(particle2.velocity, Vector3.op_Multiply(Physics.get_gravity(), dt));
        }
        mParticle.visualVelocity = Vector2.op_Implicit(mParticle.velocity);
        if (this.velocityOverLifetimeEnable)
        {
          float num2 = this.velocityOverLifetime.X.Evaluate(num1) * this.velocityOverLifetime.ScaleX;
          float num3 = this.velocityOverLifetime.Y.Evaluate(num1) * this.velocityOverLifetime.ScaleY;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local1 = @mParticle.position;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local1).x = (__Null) ((^local1).x + (double) dt * (double) num2);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local2 = @mParticle.position;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local2).y = (__Null) ((^local2).y + (double) dt * (double) num3);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local3 = @mParticle.visualVelocity;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local3).x = (__Null) ((^local3).x + (double) num2);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local4 = @mParticle.visualVelocity;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local4).y = (__Null) ((^local4).y + (double) num3);
        }
        if (this.rotationOverLifetimeEnable)
          mParticle.rotation = mParticle.startRotation + this.rotationOverLifetime.Evaluate(num1);
      }
    }
    if (!this.emit)
      return;
    if (this.burstEnable)
    {
      for (int index = 0; index < this.burst.Points.Length; ++index)
      {
        if ((double) this.mPrevTime <= (double) this.burst.Points[index].Time && (double) this.burst.Points[index].Time < (double) this.mTime)
          this.mSpawnCount += (float) this.burst.Points[index].Count;
      }
    }
    if (0.0 <= (double) this.mTime && (double) this.mTime < (double) this.duration)
      this.mSpawnCount += this.emissionRate * dt;
    if ((double) this.mSpawnCount >= 1.0)
    {
      int num = Mathf.FloorToInt(this.mSpawnCount);
      for (int index = 0; index < num && this.mParticles.Count < this.maxParticles; ++index)
      {
        UIParticleSystem.Particle particle1;
        if (this.mDeadParticles.Count > 0)
        {
          particle1 = this.mDeadParticles[this.mDeadParticles.Count - 1];
          this.mDeadParticles.RemoveAt(this.mDeadParticles.Count - 1);
        }
        else
          particle1 = new UIParticleSystem.Particle();
        particle1.randomSeed = Random.Range(0, (int) ushort.MaxValue);
        particle1.startLifetime = particle1.lifetime = this.startLifetime.Evaluate();
        particle1.color = Color32.op_Implicit(this.startColor.Evaluate());
        particle1.rotation = particle1.startRotation = this.startRotation.Evaluate();
        particle1.size = this.startSize.Evaluate();
        particle1.angularVelocity = !this.angularVelocityEnable ? 0.0f : this.angularVelocity.Evaluate();
        switch (this.emitterType)
        {
          case UIParticleSystem.EmitterTypes.Cone:
            particle1.velocity = Quaternion.op_Multiply(Quaternion.AngleAxis(this.coneEmitter.Angle.Evaluate() + this.emitterRotation, Vector3.get_forward()), Vector2.op_Implicit(Vector2.get_up()));
            // ISSUE: explicit reference operation
            particle1.position = Vector3.op_Multiply(((Vector3) @particle1.velocity).get_normalized(), this.coneEmitter.Radius.Evaluate());
            if (this.coneEmitter.RandomDirection)
            {
              particle1.velocity = Quaternion.op_Multiply(Quaternion.AngleAxis(Random.get_value() * 360f, Vector3.get_forward()), Vector2.op_Implicit(Vector2.get_up()));
              break;
            }
            break;
          case UIParticleSystem.EmitterTypes.Sphere:
            if (this.sphereEmitter.RandomDirection)
            {
              particle1.velocity = Quaternion.op_Multiply(Quaternion.AngleAxis(Random.get_value() * 360f, Vector3.get_forward()), Vector2.op_Implicit(Vector2.get_up()));
              particle1.position = Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(Random.get_value() * 360f, Vector3.get_forward()), Vector2.op_Implicit(Vector2.get_up())), this.sphereEmitter.Radius.Evaluate());
            }
            else
            {
              particle1.velocity = Quaternion.op_Multiply(Quaternion.AngleAxis(Random.get_value() * 360f, Vector3.get_forward()), Vector2.op_Implicit(Vector2.get_up()));
              // ISSUE: explicit reference operation
              particle1.position = Vector3.op_Multiply(((Vector3) @particle1.velocity).get_normalized(), this.sphereEmitter.Radius.Evaluate());
            }
            if (this.sphereEmitter.Inverse)
            {
              particle1.velocity = Vector3.op_UnaryNegation(particle1.velocity);
              break;
            }
            break;
          case UIParticleSystem.EmitterTypes.Box:
            particle1.velocity = !this.boxEmitter.RandomDirection ? Quaternion.op_Multiply(Quaternion.AngleAxis(this.emitterRotation, Vector3.get_forward()), Vector2.op_Implicit(Vector2.get_up())) : Quaternion.op_Multiply(Quaternion.AngleAxis(Random.get_value() * 360f, Vector3.get_forward()), Vector2.op_Implicit(Vector2.get_up()));
            particle1.position = new Vector3((Random.get_value() - 0.5f) * this.boxEmitter.Width, (Random.get_value() - 0.5f) * this.boxEmitter.Height, 0.0f);
            break;
        }
        UIParticleSystem.Particle particle2 = particle1;
        particle2.velocity = Vector3.op_Multiply(particle2.velocity, this.startSpeed.Evaluate());
        this.mParticles.Add(particle1);
      }
      this.mSpawnCount -= (float) num;
    }
    this.SetVerticesDirty();
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    UIVertex uiVertex = (UIVertex) null;
    Vector3 vector3_1;
    // ISSUE: explicit reference operation
    ((Vector3) @vector3_1).\u002Ector(-1f, 1f, 0.0f);
    Vector3 vector3_2;
    // ISSUE: explicit reference operation
    ((Vector3) @vector3_2).\u002Ector(1f, 1f, 0.0f);
    Vector3 vector3_3;
    // ISSUE: explicit reference operation
    ((Vector3) @vector3_3).\u002Ector(-1f, -1f, 0.0f);
    Vector3 vector3_4;
    // ISSUE: explicit reference operation
    ((Vector3) @vector3_4).\u002Ector(1f, -1f, 0.0f);
    Vector2 vector2_1;
    // ISSUE: explicit reference operation
    ((Vector2) @vector2_1).\u002Ector(0.0f, 1f);
    Vector2 vector2_2;
    // ISSUE: explicit reference operation
    ((Vector2) @vector2_2).\u002Ector(1f, 1f);
    Vector2 vector2_3;
    // ISSUE: explicit reference operation
    ((Vector2) @vector2_3).\u002Ector(0.0f, 0.0f);
    Vector2 vector2_4;
    // ISSUE: explicit reference operation
    ((Vector2) @vector2_4).\u002Ector(1f, 0.0f);
    Vector2 vector2_5;
    // ISSUE: explicit reference operation
    ((Vector2) @vector2_5).\u002Ector(0.0f, 0.0f);
    float num1 = 1f;
    float num2 = 1f;
    float num3 = 1f;
    int num4 = 1;
    int num5 = 0;
    vh.Clear();
    if (this.textureSheetAnimationEnable)
    {
      num1 = 1f / (float) this.textureSheetAnimation.TilesX;
      num2 = 1f / (float) this.textureSheetAnimation.TilesY;
      vector2_2.x = (__Null) (double) (vector2_4.x = (__Null) num1);
      vector2_3.y = (__Null) (double) (vector2_4.y = (__Null) (1f - num2));
      num3 = this.textureSheetAnimation.Animation != UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet ? (float) this.textureSheetAnimation.TilesX : (float) (this.textureSheetAnimation.TilesX * this.textureSheetAnimation.TilesY);
      num4 = Mathf.FloorToInt(num3);
    }
    if (this.RenderMode == UIParticleSystem.ParticleRenderMode.Billboard)
    {
      for (int index = this.mParticles.Count - 1; index >= 0; --index)
      {
        UIParticleSystem.Particle mParticle = this.mParticles[index];
        Quaternion quaternion = Quaternion.AngleAxis(mParticle.rotation, Vector3.get_forward());
        float size = mParticle.size;
        float num6 = (float) (1.0 - (double) mParticle.lifetime / (double) mParticle.startLifetime);
        Color32 color32 = mParticle.color;
        if (this.textureSheetAnimationEnable)
        {
          int num7 = Mathf.FloorToInt(this.textureSheetAnimation.FrameOverTime.Evaluate(num6) * num3) * this.textureSheetAnimation.Cycles % num4;
          vector2_5.x = (__Null) ((double) (num7 % this.textureSheetAnimation.TilesX) * (double) num1);
          vector2_5.y = this.textureSheetAnimation.Animation != UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet ? (__Null) (1.0 - (double) (mParticle.randomSeed % this.textureSheetAnimation.TilesY) * (double) num2) : (__Null) (1.0 - (double) (num7 / this.textureSheetAnimation.TilesY) * (double) num2);
        }
        if (this.sizeOverLifetimeEnable)
          size *= this.sizeOverLifetime.Evaluate(num6);
        if (this.colorOverLifetimeEnable)
          color32 = Color32.op_Implicit(Color.op_Multiply(Color32.op_Implicit(color32), this.colorOverLifetime.Evaluate(num6)));
        uiVertex.position = (__Null) Vector3.op_Addition(mParticle.position, Vector3.op_Multiply(Quaternion.op_Multiply(quaternion, vector3_1), size));
        uiVertex.color = (__Null) color32;
        uiVertex.uv0 = (__Null) Vector2.op_Addition(vector2_5, vector2_1);
        vh.AddVert(uiVertex);
        uiVertex.position = (__Null) Vector3.op_Addition(mParticle.position, Vector3.op_Multiply(Quaternion.op_Multiply(quaternion, vector3_2), size));
        uiVertex.color = (__Null) color32;
        uiVertex.uv0 = (__Null) Vector2.op_Addition(vector2_5, vector2_2);
        vh.AddVert(uiVertex);
        uiVertex.position = (__Null) Vector3.op_Addition(mParticle.position, Vector3.op_Multiply(Quaternion.op_Multiply(quaternion, vector3_4), size));
        uiVertex.color = (__Null) color32;
        uiVertex.uv0 = (__Null) Vector2.op_Addition(vector2_5, vector2_4);
        vh.AddVert(uiVertex);
        uiVertex.position = (__Null) Vector3.op_Addition(mParticle.position, Vector3.op_Multiply(Quaternion.op_Multiply(quaternion, vector3_3), size));
        uiVertex.color = (__Null) color32;
        uiVertex.uv0 = (__Null) Vector2.op_Addition(vector2_5, vector2_3);
        vh.AddVert(uiVertex);
        vh.AddTriangle(num5, num5 + 1, num5 + 2);
        vh.AddTriangle(num5 + 2, num5 + 3, num5);
        num5 += 4;
      }
    }
    else
    {
      for (int index = this.mParticles.Count - 1; index >= 0; --index)
      {
        UIParticleSystem.Particle mParticle = this.mParticles[index];
        float size = mParticle.size;
        float num6 = (float) (1.0 - (double) mParticle.lifetime / (double) mParticle.startLifetime);
        Color32 color32 = mParticle.color;
        if (this.sizeOverLifetimeEnable)
          size *= this.sizeOverLifetime.Evaluate(num6);
        Vector2 vector2_6;
        Vector2 right;
        // ISSUE: explicit reference operation
        if ((double) ((Vector2) @mParticle.visualVelocity).get_sqrMagnitude() > 0.0)
        {
          // ISSUE: explicit reference operation
          float num7 = this.m_StretchBillboard.LengthScale * (float) (1.0 + (double) ((Vector3) @mParticle.velocity).get_magnitude() * (double) this.m_StretchBillboard.SpeedScale);
          // ISSUE: explicit reference operation
          vector2_6 = ((Vector2) @mParticle.visualVelocity).get_normalized();
          // ISSUE: explicit reference operation
          ((Vector2) @right).\u002Ector((float) vector2_6.y * size, (float) -vector2_6.x * size);
          vector2_6 = Vector2.op_Multiply(vector2_6, size * 2f * num7);
        }
        else
        {
          vector2_6 = Vector2.get_up();
          right = Vector2.get_right();
        }
        if (this.textureSheetAnimationEnable)
        {
          int num7 = Mathf.FloorToInt(this.textureSheetAnimation.FrameOverTime.Evaluate(num6) * num3) * this.textureSheetAnimation.Cycles % num4;
          vector2_5.x = (__Null) ((double) (num7 % this.textureSheetAnimation.TilesX) * (double) num1);
          vector2_5.y = this.textureSheetAnimation.Animation != UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet ? (__Null) (1.0 - (double) (mParticle.randomSeed % this.textureSheetAnimation.TilesY) * (double) num2) : (__Null) (1.0 - (double) (num7 / this.textureSheetAnimation.TilesY) * (double) num2);
        }
        if (this.colorOverLifetimeEnable)
          color32 = Color32.op_Implicit(Color.op_Multiply(Color32.op_Implicit(color32), this.colorOverLifetime.Evaluate(num6)));
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector3&) @uiVertex.position).x = mParticle.position.x - right.x;
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector3&) @uiVertex.position).y = mParticle.position.y - right.y;
        uiVertex.color = (__Null) color32;
        uiVertex.uv0 = (__Null) Vector2.op_Addition(vector2_5, vector2_1);
        vh.AddVert(uiVertex);
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector3&) @uiVertex.position).x = mParticle.position.x + right.x;
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector3&) @uiVertex.position).y = mParticle.position.y + right.y;
        uiVertex.color = (__Null) color32;
        uiVertex.uv0 = (__Null) Vector2.op_Addition(vector2_5, vector2_2);
        vh.AddVert(uiVertex);
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector3&) @uiVertex.position).x = mParticle.position.x + right.x - vector2_6.x;
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector3&) @uiVertex.position).y = mParticle.position.y + right.y - vector2_6.y;
        uiVertex.color = (__Null) color32;
        uiVertex.uv0 = (__Null) Vector2.op_Addition(vector2_5, vector2_4);
        vh.AddVert(uiVertex);
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector3&) @uiVertex.position).x = mParticle.position.x - right.x - vector2_6.x;
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector3&) @uiVertex.position).y = mParticle.position.y - right.y - vector2_6.y;
        uiVertex.color = (__Null) color32;
        uiVertex.uv0 = (__Null) Vector2.op_Addition(vector2_5, vector2_3);
        vh.AddVert(uiVertex);
        vh.AddTriangle(num5, num5 + 1, num5 + 2);
        vh.AddTriangle(num5 + 2, num5 + 3, num5);
        num5 += 4;
      }
    }
  }

  public enum ParticleUpdateMode
  {
    UnscaledTime,
    GameTime,
    FixedTime,
  }

  public enum EmitterTypes
  {
    Cone,
    Sphere,
    Box,
  }

  public enum ParticleRenderMode
  {
    Billboard,
    StretchBillboard,
  }

  [Serializable]
  public struct ConeEmitter
  {
    public UIParticleSystem.FloatRange Angle;
    public UIParticleSystem.FloatRange Radius;
    public bool RandomDirection;

    public ConeEmitter(float angleMin, float angleMax)
    {
      this.Angle = new UIParticleSystem.FloatRange(angleMin, angleMax);
      this.Radius = new UIParticleSystem.FloatRange(0.0f, 0.0f);
      this.RandomDirection = false;
    }
  }

  [Serializable]
  public struct SphereEmitter
  {
    public UIParticleSystem.FloatRange Radius;
    public bool Inverse;
    public bool RandomDirection;

    public SphereEmitter(float radiusMin, float radiusMax)
    {
      this.Radius = new UIParticleSystem.FloatRange(radiusMin, radiusMax);
      this.Inverse = false;
      this.RandomDirection = false;
    }
  }

  [Serializable]
  public struct BoxEmitter
  {
    public float Width;
    public float Height;
    public bool RandomDirection;

    public BoxEmitter(float w, float h)
    {
      this.Width = w;
      this.Height = h;
      this.RandomDirection = false;
    }
  }

  public class Particle
  {
    public float angularVelocity;
    public Vector3 axisOfRotation;
    public Color32 color;
    public float lifetime;
    public Vector3 position;
    public int randomSeed;
    public float rotation;
    public float size;
    public float startLifetime;
    public float startRotation;
    public Vector3 velocity;
    public Vector2 visualVelocity;
  }

  [Serializable]
  public struct FloatRange
  {
    public float Min;
    public float Max;

    public FloatRange(float min, float max)
    {
      this.Min = min;
      this.Max = max;
    }

    public float Evaluate()
    {
      return Mathf.Lerp(this.Min, this.Max, Random.get_value());
    }
  }

  [Serializable]
  public struct ColorRange
  {
    public Color Min;
    public Color Max;

    public ColorRange(Color min, Color max)
    {
      this.Min = min;
      this.Max = max;
    }

    public Color Evaluate()
    {
      return Color.Lerp(this.Min, this.Max, Random.get_value());
    }
  }

  [Serializable]
  public struct TextureSheetAnimation
  {
    public int TilesX;
    public int TilesY;
    public AnimationCurve FrameOverTime;
    public UIParticleSystem.TextureSheetAnimation.AnimationRow Animation;
    public int Cycles;

    public TextureSheetAnimation(int tx, int ty)
    {
      this.TilesX = tx;
      this.TilesY = ty;
      this.FrameOverTime = new AnimationCurve(new Keyframe[2]
      {
        new Keyframe(0.0f, 0.0f),
        new Keyframe(1f, 1f)
      });
      this.Animation = UIParticleSystem.TextureSheetAnimation.AnimationRow.WholeSheet;
      this.Cycles = 1;
    }

    public enum AnimationRow
    {
      WholeSheet,
      RandomRow,
    }
  }

  [Serializable]
  public struct ParticleBurstPoint
  {
    public float Time;
    public int Count;
  }

  [Serializable]
  public struct ParticleBurst
  {
    public UIParticleSystem.ParticleBurstPoint[] Points;

    public ParticleBurst(int n)
    {
      this.Points = new UIParticleSystem.ParticleBurstPoint[n];
    }
  }

  [Serializable]
  public struct VelocityOverLifetime
  {
    public AnimationCurve X;
    public AnimationCurve Y;
    public float ScaleX;
    public float ScaleY;

    public VelocityOverLifetime(int n)
    {
      this.X = new AnimationCurve(new Keyframe[2]
      {
        new Keyframe(0.0f, 0.0f),
        new Keyframe(1f, 0.0f)
      });
      this.Y = new AnimationCurve(new Keyframe[2]
      {
        new Keyframe(0.0f, 0.0f),
        new Keyframe(1f, 0.0f)
      });
      this.ScaleX = 1f;
      this.ScaleY = 1f;
    }
  }

  [Serializable]
  public struct StretchBillboard
  {
    public float LengthScale;
    public float SpeedScale;

    public StretchBillboard(float lengthScale, float speedScale)
    {
      this.LengthScale = lengthScale;
      this.SpeedScale = speedScale;
    }
  }

  public class ParticleAttribute : PropertyAttribute
  {
    public ParticleAttribute()
    {
      base.\u002Ector();
    }
  }
}
