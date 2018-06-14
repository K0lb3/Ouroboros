// Decompiled with JetBrains decompiler
// Type: PhotonTransformViewRotationControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class PhotonTransformViewRotationControl
{
  private PhotonTransformViewRotationModel m_Model;
  private Quaternion m_NetworkRotation;

  public PhotonTransformViewRotationControl(PhotonTransformViewRotationModel model)
  {
    this.m_Model = model;
  }

  public Quaternion GetNetworkRotation()
  {
    return this.m_NetworkRotation;
  }

  public Quaternion GetRotation(Quaternion currentRotation)
  {
    switch (this.m_Model.InterpolateOption)
    {
      case PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards:
        return Quaternion.RotateTowards(currentRotation, this.m_NetworkRotation, this.m_Model.InterpolateRotateTowardsSpeed * Time.get_deltaTime());
      case PhotonTransformViewRotationModel.InterpolateOptions.Lerp:
        return Quaternion.Lerp(currentRotation, this.m_NetworkRotation, this.m_Model.InterpolateLerpSpeed * Time.get_deltaTime());
      default:
        return this.m_NetworkRotation;
    }
  }

  public void OnPhotonSerializeView(Quaternion currentRotation, PhotonStream stream, PhotonMessageInfo info)
  {
    if (!this.m_Model.SynchronizeEnabled)
      return;
    if (stream.isWriting)
    {
      stream.SendNext((object) currentRotation);
      this.m_NetworkRotation = currentRotation;
    }
    else
      this.m_NetworkRotation = (Quaternion) stream.ReceiveNext();
  }
}
