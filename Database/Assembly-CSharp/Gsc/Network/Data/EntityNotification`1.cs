// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Data.EntityNotification`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Components;

namespace Gsc.Network.Data
{
  public class EntityNotification<T> : INotification where T : Gsc.Network.Data.Entity<T>
  {
    public readonly T Entity;
    public readonly EntityNotificationType NotificationType;

    public EntityNotification(T entity, EntityNotificationType type)
    {
      this.Entity = entity;
      this.NotificationType = type;
    }
  }
}
