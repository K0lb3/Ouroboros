// Decompiled with JetBrains decompiler
// Type: Gsc.App.NetworkHelper.BlockRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Network;

namespace Gsc.App.NetworkHelper
{
  public static class BlockRequest
  {
    public static BlockRequest<TRequest, TResponse> Create<TRequest, TResponse>(IRequest<TRequest, TResponse> request) where TRequest : IRequest<TRequest, TResponse> where TResponse : IResponse<TResponse>
    {
      return new BlockRequest<TRequest, TResponse>(WebInternalTask.Create<TRequest, TResponse>(request));
    }
  }
}
