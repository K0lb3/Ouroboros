// Decompiled with JetBrains decompiler
// Type: PhotonStream
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class PhotonStream
{
  private bool write;
  private Queue<object> writeData;
  private object[] readData;
  internal byte currentItem;

  public PhotonStream(bool write, object[] incomingData)
  {
    this.write = write;
    if (incomingData == null)
      this.writeData = new Queue<object>(10);
    else
      this.readData = incomingData;
  }

  public void SetReadStream(object[] incomingData, byte pos = 0)
  {
    this.readData = incomingData;
    this.currentItem = pos;
    this.write = false;
  }

  internal void ResetWriteStream()
  {
    this.writeData.Clear();
  }

  public bool isWriting
  {
    get
    {
      return this.write;
    }
  }

  public bool isReading
  {
    get
    {
      return !this.write;
    }
  }

  public int Count
  {
    get
    {
      if (this.isWriting)
        return this.writeData.Count;
      return this.readData.Length;
    }
  }

  public object ReceiveNext()
  {
    if (this.write)
    {
      Debug.LogError((object) "Error: you cannot read this stream that you are writing!");
      return (object) null;
    }
    object obj = this.readData[(int) this.currentItem];
    ++this.currentItem;
    return obj;
  }

  public object PeekNext()
  {
    if (!this.write)
      return this.readData[(int) this.currentItem];
    Debug.LogError((object) "Error: you cannot read this stream that you are writing!");
    return (object) null;
  }

  public void SendNext(object obj)
  {
    if (!this.write)
      Debug.LogError((object) "Error: you cannot write/send to this stream that you are reading!");
    else
      this.writeData.Enqueue(obj);
  }

  public object[] ToArray()
  {
    if (this.isWriting)
      return this.writeData.ToArray();
    return this.readData;
  }

  public void Serialize(ref bool myBool)
  {
    if (this.write)
    {
      this.writeData.Enqueue((object) myBool);
    }
    else
    {
      if (this.readData.Length <= (int) this.currentItem)
        return;
      myBool = (bool) this.readData[(int) this.currentItem];
      ++this.currentItem;
    }
  }

  public void Serialize(ref int myInt)
  {
    if (this.write)
    {
      this.writeData.Enqueue((object) myInt);
    }
    else
    {
      if (this.readData.Length <= (int) this.currentItem)
        return;
      myInt = (int) this.readData[(int) this.currentItem];
      ++this.currentItem;
    }
  }

  public void Serialize(ref string value)
  {
    if (this.write)
    {
      this.writeData.Enqueue((object) value);
    }
    else
    {
      if (this.readData.Length <= (int) this.currentItem)
        return;
      value = (string) this.readData[(int) this.currentItem];
      ++this.currentItem;
    }
  }

  public void Serialize(ref char value)
  {
    if (this.write)
    {
      this.writeData.Enqueue((object) value);
    }
    else
    {
      if (this.readData.Length <= (int) this.currentItem)
        return;
      value = (char) this.readData[(int) this.currentItem];
      ++this.currentItem;
    }
  }

  public void Serialize(ref short value)
  {
    if (this.write)
    {
      this.writeData.Enqueue((object) value);
    }
    else
    {
      if (this.readData.Length <= (int) this.currentItem)
        return;
      value = (short) this.readData[(int) this.currentItem];
      ++this.currentItem;
    }
  }

  public void Serialize(ref float obj)
  {
    if (this.write)
    {
      this.writeData.Enqueue((object) obj);
    }
    else
    {
      if (this.readData.Length <= (int) this.currentItem)
        return;
      obj = (float) this.readData[(int) this.currentItem];
      ++this.currentItem;
    }
  }

  public void Serialize(ref PhotonPlayer obj)
  {
    if (this.write)
    {
      this.writeData.Enqueue((object) obj);
    }
    else
    {
      if (this.readData.Length <= (int) this.currentItem)
        return;
      obj = (PhotonPlayer) this.readData[(int) this.currentItem];
      ++this.currentItem;
    }
  }

  public void Serialize(ref Vector3 obj)
  {
    if (this.write)
    {
      this.writeData.Enqueue((object) obj);
    }
    else
    {
      if (this.readData.Length <= (int) this.currentItem)
        return;
      obj = (Vector3) this.readData[(int) this.currentItem];
      ++this.currentItem;
    }
  }

  public void Serialize(ref Vector2 obj)
  {
    if (this.write)
    {
      this.writeData.Enqueue((object) obj);
    }
    else
    {
      if (this.readData.Length <= (int) this.currentItem)
        return;
      obj = (Vector2) this.readData[(int) this.currentItem];
      ++this.currentItem;
    }
  }

  public void Serialize(ref Quaternion obj)
  {
    if (this.write)
    {
      this.writeData.Enqueue((object) obj);
    }
    else
    {
      if (this.readData.Length <= (int) this.currentItem)
        return;
      obj = (Quaternion) this.readData[(int) this.currentItem];
      ++this.currentItem;
    }
  }
}
