// Decompiled with JetBrains decompiler
// Type: SRPG.State`1
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class State<T>
  {
    public T self;

    public virtual void Begin(T self)
    {
    }

    public virtual void Update(T self)
    {
    }

    public virtual void End(T self)
    {
    }

    public virtual void Command(T self, string cmd)
    {
    }
  }
}
