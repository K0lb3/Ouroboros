// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.IRefreshTokenTask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using Gsc.Tasks;

namespace Gsc.Auth
{
  public interface IRefreshTokenTask : ITask
  {
    WebTaskResult Result { get; }
  }
}
