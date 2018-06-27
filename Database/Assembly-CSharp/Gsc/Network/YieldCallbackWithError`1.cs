// Decompiled with JetBrains decompiler
// Type: Gsc.Network.YieldCallbackWithError`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;

namespace Gsc.Network
{
  public delegate IEnumerator YieldCallbackWithError<TResponse>(TResponse response, IErrorResponse error);
}
