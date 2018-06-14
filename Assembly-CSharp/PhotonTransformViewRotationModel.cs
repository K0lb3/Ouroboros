// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewRotationModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

[Serializable]
public class PhotonTransformViewRotationModel
{
  public PhotonTransformViewRotationModel.InterpolateOptions InterpolateOption = PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards;
  public float InterpolateRotateTowardsSpeed = 180f;
  public float InterpolateLerpSpeed = 5f;
  public bool SynchronizeEnabled;

  public enum InterpolateOptions
  {
    Disabled,
    RotateTowards,
    Lerp,
  }
}
