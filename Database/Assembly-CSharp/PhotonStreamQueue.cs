// Decompiled with JetBrains decompiler
// Type: PhotonStreamQueue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PhotonStreamQueue
{
  private int m_ObjectsPerSample = -1;
  private float m_LastSampleTime = float.NegativeInfinity;
  private int m_LastFrameCount = -1;
  private int m_NextObjectIndex = -1;
  private List<object> m_Objects = new List<object>();
  private int m_SampleRate;
  private int m_SampleCount;
  private bool m_IsWriting;

  public PhotonStreamQueue(int sampleRate)
  {
    this.m_SampleRate = sampleRate;
  }

  private void BeginWritePackage()
  {
    if ((double) Time.get_realtimeSinceStartup() < (double) this.m_LastSampleTime + 1.0 / (double) this.m_SampleRate)
    {
      this.m_IsWriting = false;
    }
    else
    {
      if (this.m_SampleCount == 1)
        this.m_ObjectsPerSample = this.m_Objects.Count;
      else if (this.m_SampleCount > 1 && this.m_Objects.Count / this.m_SampleCount != this.m_ObjectsPerSample)
      {
        Debug.LogWarning((object) "The number of objects sent via a PhotonStreamQueue has to be the same each frame");
        Debug.LogWarning((object) ("Objects in List: " + (object) this.m_Objects.Count + " / Sample Count: " + (object) this.m_SampleCount + " = " + (object) (this.m_Objects.Count / this.m_SampleCount) + " != " + (object) this.m_ObjectsPerSample));
      }
      this.m_IsWriting = true;
      ++this.m_SampleCount;
      this.m_LastSampleTime = Time.get_realtimeSinceStartup();
    }
  }

  public void Reset()
  {
    this.m_SampleCount = 0;
    this.m_ObjectsPerSample = -1;
    this.m_LastSampleTime = float.NegativeInfinity;
    this.m_LastFrameCount = -1;
    this.m_Objects.Clear();
  }

  public void SendNext(object obj)
  {
    if (Time.get_frameCount() != this.m_LastFrameCount)
      this.BeginWritePackage();
    this.m_LastFrameCount = Time.get_frameCount();
    if (!this.m_IsWriting)
      return;
    this.m_Objects.Add(obj);
  }

  public bool HasQueuedObjects()
  {
    return this.m_NextObjectIndex != -1;
  }

  public object ReceiveNext()
  {
    if (this.m_NextObjectIndex == -1)
      return (object) null;
    if (this.m_NextObjectIndex >= this.m_Objects.Count)
      this.m_NextObjectIndex -= this.m_ObjectsPerSample;
    return this.m_Objects[this.m_NextObjectIndex++];
  }

  public void Serialize(PhotonStream stream)
  {
    if (this.m_Objects.Count > 0 && this.m_ObjectsPerSample < 0)
      this.m_ObjectsPerSample = this.m_Objects.Count;
    stream.SendNext((object) this.m_SampleCount);
    stream.SendNext((object) this.m_ObjectsPerSample);
    for (int index = 0; index < this.m_Objects.Count; ++index)
      stream.SendNext(this.m_Objects[index]);
    this.m_Objects.Clear();
    this.m_SampleCount = 0;
  }

  public void Deserialize(PhotonStream stream)
  {
    this.m_Objects.Clear();
    this.m_SampleCount = (int) stream.ReceiveNext();
    this.m_ObjectsPerSample = (int) stream.ReceiveNext();
    for (int index = 0; index < this.m_SampleCount * this.m_ObjectsPerSample; ++index)
      this.m_Objects.Add(stream.ReceiveNext());
    if (this.m_Objects.Count > 0)
      this.m_NextObjectIndex = 0;
    else
      this.m_NextObjectIndex = -1;
  }
}
