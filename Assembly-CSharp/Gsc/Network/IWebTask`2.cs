// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IWebTask`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Tasks;
using System.Collections;

namespace Gsc.Network
{
  public interface IWebTask<TRequest, TResponse> : IEnumerator, IWebTaskBase, IWebTask, ITask, IWebTask<TResponse> where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
  {
    TRequest Request { get; }
  }
}
