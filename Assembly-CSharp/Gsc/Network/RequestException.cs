// Decompiled with JetBrains decompiler
// Type: Gsc.Network.RequestException
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.Serialization;

namespace Gsc.Network
{
  [Serializable]
  public class RequestException : Exception
  {
    public RequestException()
    {
    }

    public RequestException(string message)
      : base(message)
    {
    }

    public RequestException(string message, Exception inner)
      : base(message, inner)
    {
    }

    protected RequestException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
