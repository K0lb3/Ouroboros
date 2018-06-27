// Decompiled with JetBrains decompiler
// Type: Gsc.Network.WebTaskBundle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Gsc.Network
{
  public class WebTaskBundle : IEnumerator, IEnumerable, IEnumerable<IWebTask>
  {
    private readonly List<IWebTask> Tasks;

    public WebTaskBundle()
    {
      this.Tasks = new List<IWebTask>();
    }

    public WebTaskBundle(IEnumerable<IWebTask> tasks)
    {
      this.Tasks = new List<IWebTask>(tasks);
    }

    public WebTaskBundle(List<IWebTask> tasks)
    {
      this.Tasks = tasks;
    }

    public event Action OnFinish;

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public bool isDone { get; protected set; }

    public T Add<T>(T task) where T : IWebTask
    {
      this.Tasks.Add((IWebTask) task);
      return task;
    }

    public void Retry()
    {
      for (int index = 0; index < this.Tasks.Count; ++index)
        this.Tasks[index].Retry();
    }

    public bool HasResult(WebTaskResult result)
    {
      for (int index = 0; index < this.Tasks.Count; ++index)
      {
        if (this.Tasks[index].Result == result)
          return true;
      }
      return false;
    }

    public IEnumerator<IWebTask> GetEnumerator()
    {
      return (IEnumerator<IWebTask>) this.Tasks.GetEnumerator();
    }

    public object Current
    {
      get
      {
        return (object) null;
      }
    }

    public void Reset()
    {
    }

    public bool MoveNext()
    {
      if (this.isDone)
        return false;
      for (int index = 0; index < this.Tasks.Count; ++index)
      {
        if (!this.Tasks[index].isDone)
          return true;
      }
      this.isDone = true;
      if (this.OnFinish != null)
        this.OnFinish();
      return false;
    }
  }
}
