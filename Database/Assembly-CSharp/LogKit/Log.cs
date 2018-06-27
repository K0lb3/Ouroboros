// Decompiled with JetBrains decompiler
// Type: LogKit.Log
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace LogKit
{
  public struct Log
  {
    public Guid ID;
    public string Tag;
    public DateTime Date;
    public LogLevel LogLevel;
    public UserInfo UserInfo;
  }
}
