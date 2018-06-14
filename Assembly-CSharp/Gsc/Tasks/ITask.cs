// Decompiled with JetBrains decompiler
// Type: Gsc.Tasks.ITask
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;

namespace Gsc.Tasks
{
  public interface ITask
  {
    bool isDone { get; }

    void OnStart();

    IEnumerator Run();

    void OnFinish();
  }
}
