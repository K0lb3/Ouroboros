// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IWebTask`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Tasks;
using System.Collections;

namespace Gsc.Network
{
  public interface IWebTask<TResponse> : IEnumerator, IWebTaskBase, IWebTask, ITask where TResponse : IResponse<TResponse>
  {
    TResponse Response { get; }

    IErrorResponse ErrorResponse { get; }
  }
}
