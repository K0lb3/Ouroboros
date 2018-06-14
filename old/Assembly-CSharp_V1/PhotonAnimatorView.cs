// Decompiled with JetBrains decompiler
// Type: PhotonAnimatorView
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (PhotonView))]
[AddComponentMenu("Photon Networking/Photon Animator View")]
public class PhotonAnimatorView : MonoBehaviour, IPunObservable
{
  private Animator m_Animator;
  private PhotonStreamQueue m_StreamQueue;
  [SerializeField]
  [HideInInspector]
  private bool ShowLayerWeightsInspector;
  [HideInInspector]
  [SerializeField]
  private bool ShowParameterInspector;
  [SerializeField]
  [HideInInspector]
  private List<PhotonAnimatorView.SynchronizedParameter> m_SynchronizeParameters;
  [HideInInspector]
  [SerializeField]
  private List<PhotonAnimatorView.SynchronizedLayer> m_SynchronizeLayers;
  private Vector3 m_ReceiverPosition;
  private float m_LastDeserializeTime;
  private bool m_WasSynchronizeTypeChanged;
  private PhotonView m_PhotonView;
  private List<string> m_raisedDiscreteTriggersCache;

  public PhotonAnimatorView()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    this.m_PhotonView = (PhotonView) ((Component) this).GetComponent<PhotonView>();
    this.m_StreamQueue = new PhotonStreamQueue(120);
    this.m_Animator = (Animator) ((Component) this).GetComponent<Animator>();
  }

  private void Update()
  {
    if (this.m_Animator.get_applyRootMotion() && !this.m_PhotonView.isMine && PhotonNetwork.connected)
      this.m_Animator.set_applyRootMotion(false);
    if (!PhotonNetwork.inRoom || PhotonNetwork.room.PlayerCount <= 1)
      this.m_StreamQueue.Reset();
    else if (this.m_PhotonView.isMine)
    {
      this.SerializeDataContinuously();
      this.CacheDiscreteTriggers();
    }
    else
      this.DeserializeDataContinuously();
  }

  public void CacheDiscreteTriggers()
  {
    for (int index = 0; index < this.m_SynchronizeParameters.Count; ++index)
    {
      PhotonAnimatorView.SynchronizedParameter synchronizeParameter = this.m_SynchronizeParameters[index];
      if (synchronizeParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete && synchronizeParameter.Type == PhotonAnimatorView.ParameterType.Trigger && (this.m_Animator.GetBool(synchronizeParameter.Name) && synchronizeParameter.Type == PhotonAnimatorView.ParameterType.Trigger))
      {
        this.m_raisedDiscreteTriggersCache.Add(synchronizeParameter.Name);
        break;
      }
    }
  }

  public bool DoesLayerSynchronizeTypeExist(int layerIndex)
  {
    return this.m_SynchronizeLayers.FindIndex((Predicate<PhotonAnimatorView.SynchronizedLayer>) (item => item.LayerIndex == layerIndex)) != -1;
  }

  public bool DoesParameterSynchronizeTypeExist(string name)
  {
    return this.m_SynchronizeParameters.FindIndex((Predicate<PhotonAnimatorView.SynchronizedParameter>) (item => item.Name == name)) != -1;
  }

  public List<PhotonAnimatorView.SynchronizedLayer> GetSynchronizedLayers()
  {
    return this.m_SynchronizeLayers;
  }

  public List<PhotonAnimatorView.SynchronizedParameter> GetSynchronizedParameters()
  {
    return this.m_SynchronizeParameters;
  }

  public PhotonAnimatorView.SynchronizeType GetLayerSynchronizeType(int layerIndex)
  {
    int index = this.m_SynchronizeLayers.FindIndex((Predicate<PhotonAnimatorView.SynchronizedLayer>) (item => item.LayerIndex == layerIndex));
    if (index == -1)
      return PhotonAnimatorView.SynchronizeType.Disabled;
    return this.m_SynchronizeLayers[index].SynchronizeType;
  }

  public PhotonAnimatorView.SynchronizeType GetParameterSynchronizeType(string name)
  {
    int index = this.m_SynchronizeParameters.FindIndex((Predicate<PhotonAnimatorView.SynchronizedParameter>) (item => item.Name == name));
    if (index == -1)
      return PhotonAnimatorView.SynchronizeType.Disabled;
    return this.m_SynchronizeParameters[index].SynchronizeType;
  }

  public void SetLayerSynchronized(int layerIndex, PhotonAnimatorView.SynchronizeType synchronizeType)
  {
    if (Application.get_isPlaying())
      this.m_WasSynchronizeTypeChanged = true;
    int index = this.m_SynchronizeLayers.FindIndex((Predicate<PhotonAnimatorView.SynchronizedLayer>) (item => item.LayerIndex == layerIndex));
    if (index == -1)
      this.m_SynchronizeLayers.Add(new PhotonAnimatorView.SynchronizedLayer()
      {
        LayerIndex = layerIndex,
        SynchronizeType = synchronizeType
      });
    else
      this.m_SynchronizeLayers[index].SynchronizeType = synchronizeType;
  }

  public void SetParameterSynchronized(string name, PhotonAnimatorView.ParameterType type, PhotonAnimatorView.SynchronizeType synchronizeType)
  {
    if (Application.get_isPlaying())
      this.m_WasSynchronizeTypeChanged = true;
    int index = this.m_SynchronizeParameters.FindIndex((Predicate<PhotonAnimatorView.SynchronizedParameter>) (item => item.Name == name));
    if (index == -1)
      this.m_SynchronizeParameters.Add(new PhotonAnimatorView.SynchronizedParameter()
      {
        Name = name,
        Type = type,
        SynchronizeType = synchronizeType
      });
    else
      this.m_SynchronizeParameters[index].SynchronizeType = synchronizeType;
  }

  private void SerializeDataContinuously()
  {
    if (Object.op_Equality((Object) this.m_Animator, (Object) null))
      return;
    for (int index = 0; index < this.m_SynchronizeLayers.Count; ++index)
    {
      if (this.m_SynchronizeLayers[index].SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
        this.m_StreamQueue.SendNext((object) this.m_Animator.GetLayerWeight(this.m_SynchronizeLayers[index].LayerIndex));
    }
    for (int index = 0; index < this.m_SynchronizeParameters.Count; ++index)
    {
      PhotonAnimatorView.SynchronizedParameter synchronizeParameter = this.m_SynchronizeParameters[index];
      if (synchronizeParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
      {
        PhotonAnimatorView.ParameterType type = synchronizeParameter.Type;
        switch (type)
        {
          case PhotonAnimatorView.ParameterType.Float:
            this.m_StreamQueue.SendNext((object) this.m_Animator.GetFloat(synchronizeParameter.Name));
            continue;
          case PhotonAnimatorView.ParameterType.Int:
            this.m_StreamQueue.SendNext((object) this.m_Animator.GetInteger(synchronizeParameter.Name));
            continue;
          case PhotonAnimatorView.ParameterType.Bool:
            this.m_StreamQueue.SendNext((object) this.m_Animator.GetBool(synchronizeParameter.Name));
            continue;
          default:
            if (type == PhotonAnimatorView.ParameterType.Trigger)
            {
              this.m_StreamQueue.SendNext((object) this.m_Animator.GetBool(synchronizeParameter.Name));
              continue;
            }
            continue;
        }
      }
    }
  }

  private void DeserializeDataContinuously()
  {
    if (!this.m_StreamQueue.HasQueuedObjects())
      return;
    for (int index = 0; index < this.m_SynchronizeLayers.Count; ++index)
    {
      if (this.m_SynchronizeLayers[index].SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
        this.m_Animator.SetLayerWeight(this.m_SynchronizeLayers[index].LayerIndex, (float) this.m_StreamQueue.ReceiveNext());
    }
    for (int index = 0; index < this.m_SynchronizeParameters.Count; ++index)
    {
      PhotonAnimatorView.SynchronizedParameter synchronizeParameter = this.m_SynchronizeParameters[index];
      if (synchronizeParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Continuous)
      {
        PhotonAnimatorView.ParameterType type = synchronizeParameter.Type;
        switch (type)
        {
          case PhotonAnimatorView.ParameterType.Float:
            this.m_Animator.SetFloat(synchronizeParameter.Name, (float) this.m_StreamQueue.ReceiveNext());
            continue;
          case PhotonAnimatorView.ParameterType.Int:
            this.m_Animator.SetInteger(synchronizeParameter.Name, (int) this.m_StreamQueue.ReceiveNext());
            continue;
          case PhotonAnimatorView.ParameterType.Bool:
            this.m_Animator.SetBool(synchronizeParameter.Name, (bool) this.m_StreamQueue.ReceiveNext());
            continue;
          default:
            if (type == PhotonAnimatorView.ParameterType.Trigger)
            {
              this.m_Animator.SetBool(synchronizeParameter.Name, (bool) this.m_StreamQueue.ReceiveNext());
              continue;
            }
            continue;
        }
      }
    }
  }

  private void SerializeDataDiscretly(PhotonStream stream)
  {
    for (int index = 0; index < this.m_SynchronizeLayers.Count; ++index)
    {
      if (this.m_SynchronizeLayers[index].SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
        stream.SendNext((object) this.m_Animator.GetLayerWeight(this.m_SynchronizeLayers[index].LayerIndex));
    }
    for (int index = 0; index < this.m_SynchronizeParameters.Count; ++index)
    {
      PhotonAnimatorView.SynchronizedParameter synchronizeParameter = this.m_SynchronizeParameters[index];
      if (synchronizeParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
      {
        PhotonAnimatorView.ParameterType type = synchronizeParameter.Type;
        switch (type)
        {
          case PhotonAnimatorView.ParameterType.Float:
            stream.SendNext((object) this.m_Animator.GetFloat(synchronizeParameter.Name));
            continue;
          case PhotonAnimatorView.ParameterType.Int:
            stream.SendNext((object) this.m_Animator.GetInteger(synchronizeParameter.Name));
            continue;
          case PhotonAnimatorView.ParameterType.Bool:
            stream.SendNext((object) this.m_Animator.GetBool(synchronizeParameter.Name));
            continue;
          default:
            if (type == PhotonAnimatorView.ParameterType.Trigger)
            {
              stream.SendNext((object) this.m_raisedDiscreteTriggersCache.Contains(synchronizeParameter.Name));
              continue;
            }
            continue;
        }
      }
    }
    this.m_raisedDiscreteTriggersCache.Clear();
  }

  private void DeserializeDataDiscretly(PhotonStream stream)
  {
    for (int index = 0; index < this.m_SynchronizeLayers.Count; ++index)
    {
      if (this.m_SynchronizeLayers[index].SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
        this.m_Animator.SetLayerWeight(this.m_SynchronizeLayers[index].LayerIndex, (float) stream.ReceiveNext());
    }
    for (int index = 0; index < this.m_SynchronizeParameters.Count; ++index)
    {
      PhotonAnimatorView.SynchronizedParameter synchronizeParameter = this.m_SynchronizeParameters[index];
      if (synchronizeParameter.SynchronizeType == PhotonAnimatorView.SynchronizeType.Discrete)
      {
        PhotonAnimatorView.ParameterType type = synchronizeParameter.Type;
        switch (type)
        {
          case PhotonAnimatorView.ParameterType.Float:
            if (!(stream.PeekNext() is float))
              return;
            this.m_Animator.SetFloat(synchronizeParameter.Name, (float) stream.ReceiveNext());
            continue;
          case PhotonAnimatorView.ParameterType.Int:
            if (!(stream.PeekNext() is int))
              return;
            this.m_Animator.SetInteger(synchronizeParameter.Name, (int) stream.ReceiveNext());
            continue;
          case PhotonAnimatorView.ParameterType.Bool:
            if (!(stream.PeekNext() is bool))
              return;
            this.m_Animator.SetBool(synchronizeParameter.Name, (bool) stream.ReceiveNext());
            continue;
          default:
            if (type == PhotonAnimatorView.ParameterType.Trigger)
            {
              if (!(stream.PeekNext() is bool))
                return;
              if ((bool) stream.ReceiveNext())
              {
                this.m_Animator.SetTrigger(synchronizeParameter.Name);
                continue;
              }
              continue;
            }
            continue;
        }
      }
    }
  }

  private void SerializeSynchronizationTypeState(PhotonStream stream)
  {
    byte[] numArray = new byte[this.m_SynchronizeLayers.Count + this.m_SynchronizeParameters.Count];
    for (int index = 0; index < this.m_SynchronizeLayers.Count; ++index)
      numArray[index] = (byte) this.m_SynchronizeLayers[index].SynchronizeType;
    for (int index = 0; index < this.m_SynchronizeParameters.Count; ++index)
      numArray[this.m_SynchronizeLayers.Count + index] = (byte) this.m_SynchronizeParameters[index].SynchronizeType;
    stream.SendNext((object) numArray);
  }

  private void DeserializeSynchronizationTypeState(PhotonStream stream)
  {
    byte[] next = (byte[]) stream.ReceiveNext();
    for (int index = 0; index < this.m_SynchronizeLayers.Count; ++index)
      this.m_SynchronizeLayers[index].SynchronizeType = (PhotonAnimatorView.SynchronizeType) next[index];
    for (int index = 0; index < this.m_SynchronizeParameters.Count; ++index)
      this.m_SynchronizeParameters[index].SynchronizeType = (PhotonAnimatorView.SynchronizeType) next[this.m_SynchronizeLayers.Count + index];
  }

  public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
  {
    if (Object.op_Equality((Object) this.m_Animator, (Object) null))
      return;
    if (stream.isWriting)
    {
      if (this.m_WasSynchronizeTypeChanged)
      {
        this.m_StreamQueue.Reset();
        this.SerializeSynchronizationTypeState(stream);
        this.m_WasSynchronizeTypeChanged = false;
      }
      this.m_StreamQueue.Serialize(stream);
      this.SerializeDataDiscretly(stream);
    }
    else
    {
      if (stream.PeekNext() is byte[])
        this.DeserializeSynchronizationTypeState(stream);
      this.m_StreamQueue.Deserialize(stream);
      this.DeserializeDataDiscretly(stream);
    }
  }

  public enum ParameterType
  {
    Float = 1,
    Int = 3,
    Bool = 4,
    Trigger = 9,
  }

  public enum SynchronizeType
  {
    Disabled,
    Discrete,
    Continuous,
  }

  [Serializable]
  public class SynchronizedParameter
  {
    public PhotonAnimatorView.ParameterType Type;
    public PhotonAnimatorView.SynchronizeType SynchronizeType;
    public string Name;
  }

  [Serializable]
  public class SynchronizedLayer
  {
    public PhotonAnimatorView.SynchronizeType SynchronizeType;
    public int LayerIndex;
  }
}
