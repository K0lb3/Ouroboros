// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewScaleControl
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class PhotonTransformViewScaleControl
{
  private Vector3 m_NetworkScale = Vector3.get_one();
  private PhotonTransformViewScaleModel m_Model;

  public PhotonTransformViewScaleControl(PhotonTransformViewScaleModel model)
  {
    this.m_Model = model;
  }

  public Vector3 GetNetworkScale()
  {
    return this.m_NetworkScale;
  }

  public Vector3 GetScale(Vector3 currentScale)
  {
    switch (this.m_Model.InterpolateOption)
    {
      case PhotonTransformViewScaleModel.InterpolateOptions.MoveTowards:
        return Vector3.MoveTowards(currentScale, this.m_NetworkScale, this.m_Model.InterpolateMoveTowardsSpeed * Time.get_deltaTime());
      case PhotonTransformViewScaleModel.InterpolateOptions.Lerp:
        return Vector3.Lerp(currentScale, this.m_NetworkScale, this.m_Model.InterpolateLerpSpeed * Time.get_deltaTime());
      default:
        return this.m_NetworkScale;
    }
  }

  public void OnPhotonSerializeView(Vector3 currentScale, PhotonStream stream, PhotonMessageInfo info)
  {
    if (!this.m_Model.SynchronizeEnabled)
      return;
    if (stream.isWriting)
    {
      stream.SendNext((object) currentScale);
      this.m_NetworkScale = currentScale;
    }
    else
      this.m_NetworkScale = (Vector3) stream.ReceiveNext();
  }
}
