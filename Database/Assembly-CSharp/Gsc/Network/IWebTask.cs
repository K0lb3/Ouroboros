// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IWebTask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Tasks;
using System;
using System.Collections;

namespace Gsc.Network
{
  public interface IWebTask : IEnumerator, IWebTaskBase, ITask
  {
    bool handled { get; }

    WebTaskResult Result { get; }

    byte[] error { get; }

    void Retry();

    bool IsAcceptResult(WebTaskResult result);

    bool HasAttributes(WebTaskAttribute attributes);

    WebInternalTask GetInternalTask();

    Type GetRequestType();
  }
}
