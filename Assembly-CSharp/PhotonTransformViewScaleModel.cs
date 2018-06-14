// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewScaleModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

[Serializable]
public class PhotonTransformViewScaleModel
{
  public float InterpolateMoveTowardsSpeed = 1f;
  public bool SynchronizeEnabled;
  public PhotonTransformViewScaleModel.InterpolateOptions InterpolateOption;
  public float InterpolateLerpSpeed;

  public enum InterpolateOptions
  {
    Disabled,
    MoveTowards,
    Lerp,
  }
}
