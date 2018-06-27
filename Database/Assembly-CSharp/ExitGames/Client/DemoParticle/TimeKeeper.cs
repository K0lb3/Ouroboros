// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.DemoParticle.TimeKeeper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace ExitGames.Client.DemoParticle
{
  public class TimeKeeper
  {
    private int lastExecutionTime = Environment.TickCount;
    private bool shouldExecute;

    public TimeKeeper(int interval)
    {
      this.IsEnabled = true;
      this.Interval = interval;
    }

    public int Interval { get; set; }

    public bool IsEnabled { get; set; }

    public bool ShouldExecute
    {
      get
      {
        if (!this.IsEnabled)
          return false;
        if (!this.shouldExecute)
          return Environment.TickCount - this.lastExecutionTime > this.Interval;
        return true;
      }
      set
      {
        this.shouldExecute = value;
      }
    }

    public void Reset()
    {
      this.shouldExecute = false;
      this.lastExecutionTime = Environment.TickCount;
    }
  }
}
