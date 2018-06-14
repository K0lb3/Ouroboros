// Decompiled with JetBrains decompiler
// Type: Gsc.Network.ReturnCallback`3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.Network
{
  public delegate TResult ReturnCallback<TRequest, TResponse, TResult>(TRequest request, TResponse response);
}
