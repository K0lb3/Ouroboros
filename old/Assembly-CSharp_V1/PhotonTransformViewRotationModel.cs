// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewRotationModel
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
