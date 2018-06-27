// Decompiled with JetBrains decompiler
// Type: Gsc.Network.IErrorResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.DOM;

namespace Gsc.Network
{
  public interface IErrorResponse : IResponse
  {
    IDocument data { get; }

    string ErrorCode { get; }
  }
}
