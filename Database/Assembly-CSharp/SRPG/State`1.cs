// Decompiled with JetBrains decompiler
// Type: SRPG.State`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
