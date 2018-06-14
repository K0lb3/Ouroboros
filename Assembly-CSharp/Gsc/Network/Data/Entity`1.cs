// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Data.Entity`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace Gsc.Network.Data
{
  public abstract class Entity<T> : IEntity, IObject where T : Entity<T>
  {
    private uint ver;

    IEntity IEntity.Clone()
    {
      return (IEntity) this.Clone();
    }

    public string pk { get; protected set; }

    public abstract void Update();

    public abstract void ResolveRefs();

    public T Clone()
    {
      return (T) this.MemberwiseClone();
    }

    protected bool IsUpdatedOnce()
    {
      bool flag = (int) this.ver != (int) EntityRepository.ver;
      this.ver = EntityRepository.ver;
      return flag;
    }
  }
}
