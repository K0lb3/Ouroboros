// Decompiled with JetBrains decompiler
// Type: PhotonTransformView
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (PhotonView))]
[AddComponentMenu("Photon Networking/Photon Transform View")]
public class PhotonTransformView : MonoBehaviour, IPunObservable
{
  [SerializeField]
  private PhotonTransformViewPositionModel m_PositionModel;
  [SerializeField]
  private PhotonTransformViewRotationModel m_RotationModel;
  [SerializeField]
  private PhotonTransformViewScaleModel m_ScaleModel;
  private PhotonTransformViewPositionControl m_PositionControl;
  private PhotonTransformViewRotationControl m_RotationControl;
  private PhotonTransformViewScaleControl m_ScaleControl;
  private PhotonView m_PhotonView;
  private bool m_ReceivedNetworkUpdate;
  private bool m_firstTake;

  public PhotonTransformView()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    this.m_PhotonView = (PhotonView) ((Component) this).GetComponent<PhotonView>();
    this.m_PositionControl = new PhotonTransformViewPositionControl(this.m_PositionModel);
    this.m_RotationControl = new PhotonTransformViewRotationControl(this.m_RotationModel);
    this.m_ScaleControl = new PhotonTransformViewScaleControl(this.m_ScaleModel);
  }

  private void OnEnable()
  {
    this.m_firstTake = true;
  }

  private void Update()
  {
    if (Object.op_Equality((Object) this.m_PhotonView, (Object) null) || this.m_PhotonView.isMine || !PhotonNetwork.connected)
      return;
    this.UpdatePosition();
    this.UpdateRotation();
    this.UpdateScale();
  }

  private void UpdatePosition()
  {
    if (!this.m_PositionModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
      return;
    ((Component) this).get_transform().set_localPosition(this.m_PositionControl.UpdatePosition(((Component) this).get_transform().get_localPosition()));
  }

  private void UpdateRotation()
  {
    if (!this.m_RotationModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
      return;
    ((Component) this).get_transform().set_localRotation(this.m_RotationControl.GetRotation(((Component) this).get_transform().get_localRotation()));
  }

  private void UpdateScale()
  {
    if (!this.m_ScaleModel.SynchronizeEnabled || !this.m_ReceivedNetworkUpdate)
      return;
    ((Component) this).get_transform().set_localScale(this.m_ScaleControl.GetScale(((Component) this).get_transform().get_localScale()));
  }

  public void SetSynchronizedValues(Vector3 speed, float turnSpeed)
  {
    this.m_PositionControl.SetSynchronizedValues(speed, turnSpeed);
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    this.m_PositionControl.OnPhotonSerializeView(((Component) this).get_transform().get_localPosition(), stream, info);
    this.m_RotationControl.OnPhotonSerializeView(((Component) this).get_transform().get_localRotation(), stream, info);
    this.m_ScaleControl.OnPhotonSerializeView(((Component) this).get_transform().get_localScale(), stream, info);
    if (!this.m_PhotonView.isMine && this.m_PositionModel.DrawErrorGizmo)
      this.DoDrawEstimatedPositionError();
    if (!stream.isReading)
      return;
    this.m_ReceivedNetworkUpdate = true;
    if (!this.m_firstTake)
      return;
    this.m_firstTake = false;
    if (this.m_PositionModel.SynchronizeEnabled)
      ((Component) this).get_transform().set_localPosition(this.m_PositionControl.GetNetworkPosition());
    if (this.m_RotationModel.SynchronizeEnabled)
      ((Component) this).get_transform().set_localRotation(this.m_RotationControl.GetNetworkRotation());
    if (!this.m_ScaleModel.SynchronizeEnabled)
      return;
    ((Component) this).get_transform().set_localScale(this.m_ScaleControl.GetNetworkScale());
  }

  private void DoDrawEstimatedPositionError()
  {
    Vector3 vector3 = this.m_PositionControl.GetNetworkPosition();
    if (Object.op_Inequality((Object) ((Component) this).get_transform().get_parent(), (Object) null))
      vector3 = Vector3.op_Addition(((Component) this).get_transform().get_parent().get_position(), vector3);
    Debug.DrawLine(vector3, ((Component) this).get_transform().get_position(), Color.get_red(), 2f);
    Debug.DrawLine(((Component) this).get_transform().get_position(), Vector3.op_Addition(((Component) this).get_transform().get_position(), Vector3.get_up()), Color.get_green(), 2f);
    Debug.DrawLine(vector3, Vector3.op_Addition(vector3, Vector3.get_up()), Color.get_red(), 2f);
  }
}
