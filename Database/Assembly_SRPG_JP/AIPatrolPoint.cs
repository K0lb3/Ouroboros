// Decompiled with JetBrains decompiler
// Type: SRPG.AIPatrolPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class AIPatrolPoint
  {
    public int x;
    public int y;
    public int length;

    public void CopyTo(AIPatrolPoint dst)
    {
      dst.x = this.x;
      dst.y = this.y;
      dst.length = this.length;
    }
  }
}
