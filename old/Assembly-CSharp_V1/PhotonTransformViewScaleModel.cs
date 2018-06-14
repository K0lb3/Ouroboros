// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewScaleModel
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
