// Decompiled with JetBrains decompiler
// Type: LogKit.BufferPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace LogKit
{
  public class BufferPool
  {
    private readonly int mBufferSize;
    private readonly List<Buffer> mBuffers;

    public BufferPool(int poolSize, int bufferSize)
    {
      this.mBufferSize = bufferSize;
      this.mBuffers = new List<Buffer>(poolSize);
      for (int index = 0; index < poolSize; ++index)
        this.mBuffers.Add(new Buffer(bufferSize));
    }

    public Buffer Get()
    {
      for (int index = 0; index < this.mBuffers.Count; ++index)
      {
        Buffer mBuffer = this.mBuffers[index];
        if (!mBuffer.IsAcquired && mBuffer.Count < this.mBufferSize)
        {
          mBuffer.Acquire();
          return mBuffer;
        }
      }
      return (Buffer) null;
    }
  }
}
