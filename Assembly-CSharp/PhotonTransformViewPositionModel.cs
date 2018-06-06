// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewPositionModel
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class PhotonTransformViewPositionModel
{
  public bool TeleportEnabled = true;
  public float TeleportIfDistanceGreaterThan = 3f;
  public PhotonTransformViewPositionModel.InterpolateOptions InterpolateOption = PhotonTransformViewPositionModel.InterpolateOptions.EstimatedSpeed;
  public float InterpolateMoveTowardsSpeed = 1f;
  public float InterpolateLerpSpeed = 1f;
  public float InterpolateMoveTowardsAcceleration = 2f;
  public float InterpolateMoveTowardsDeceleration = 2f;
  public AnimationCurve InterpolateSpeedCurve = new AnimationCurve(new Keyframe[4]{ new Keyframe(-1f, 0.0f, 0.0f, float.PositiveInfinity), new Keyframe(0.0f, 1f, 0.0f, 0.0f), new Keyframe(1f, 1f, 0.0f, 1f), new Keyframe(4f, 4f, 1f, 0.0f) });
  public float ExtrapolateSpeed = 1f;
  public bool ExtrapolateIncludingRoundTripTime = true;
  public int ExtrapolateNumberOfStoredPositions = 1;
  public bool DrawErrorGizmo = true;
  public bool SynchronizeEnabled;
  public PhotonTransformViewPositionModel.ExtrapolateOptions ExtrapolateOption;

  public enum InterpolateOptions
  {
    Disabled,
    FixedSpeed,
    EstimatedSpeed,
    SynchronizeValues,
    Lerp,
  }

  public enum ExtrapolateOptions
  {
    Disabled,
    SynchronizeValues,
    EstimateSpeedAndTurn,
    FixedSpeed,
  }
}
