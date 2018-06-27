// Decompiled with JetBrains decompiler
// Type: Gsc.Network.Data.EntityRepository
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Gsc.Components;
using Gsc.Core;
using Gsc.DOM;
using Gsc.DOM.MiniJSON;
using System;
using System.Collections.Generic;

namespace Gsc.Network.Data
{
  public class EntityRepository
  {
    private static readonly EntityRepository repository = new EntityRepository();
    private readonly Dictionary<Type, EntityRepository.ISimpleRepository> entityRepositories = new Dictionary<Type, EntityRepository.ISimpleRepository>();

    private EntityRepository()
    {
    }

    public static T Get<T>(string key) where T : Entity<T>
    {
      T obj;
      if (EntityRepository.repository.GetRepository<T>().PublicList.TryGetValue(key, out obj))
        return obj;
      return (T) null;
    }

    public static EntityList<T> GetAll<T>() where T : Entity<T>
    {
      return EntityRepository.repository.GetRepository<T>().PublicList;
    }

    public static void Subscribe<T>(NotificationObserver<EntityNotification<T>> observer, T sender = null) where T : Entity<T>
    {
      object sender1 = (object) null;
      if ((object) sender != null)
        sender1 = EntityRepository.repository.GetRepository<T>().GetSender(sender.pk);
      NotificationCenter.Instance.AddObserver<EntityNotification<T>>(observer, sender1);
    }

    public static void AllClear()
    {
      using (Dictionary<Type, EntityRepository.ISimpleRepository>.ValueCollection.Enumerator enumerator = EntityRepository.repository.entityRepositories.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.AllClear();
      }
    }

    public static void CacheClear()
    {
      using (Dictionary<Type, EntityRepository.ISimpleRepository>.ValueCollection.Enumerator enumerator = EntityRepository.repository.entityRepositories.Values.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.CacheClear();
      }
    }

    public static uint ver { get; private set; }

    private EntityRepository.SimpleRepository<T> GetRepository<T>() where T : Entity<T>
    {
      return (EntityRepository.SimpleRepository<T>) this.GetRepository(typeof (T));
    }

    private EntityRepository.ISimpleRepository GetRepository(Type type)
    {
      EntityRepository.ISimpleRepository instance;
      if (!this.entityRepositories.TryGetValue(type, out instance))
      {
        instance = AssemblySupport.CreateInstance<EntityRepository.ISimpleRepository>("Gsc.Network.Data.EntityRepository+SimpleRepository`1[[" + type.AssemblyQualifiedName + "]]");
        this.entityRepositories.Add(type, instance);
      }
      return instance;
    }

    public static void Update(Gsc.DOM.IObject root)
    {
      IValue obj1;
      if (root.TryGetValue("update_models", out obj1))
      {
        foreach (IMember member in (IEnumerable<IMember>) obj1.GetObject())
        {
          Type type = AssemblySupport.GetType(string.Format("Gsc.Data.Model.{0}", (object) member.Name));
          if ((object) type != null)
          {
            AssemblySupport.MethodInfo constructor1 = AssemblySupport.GetConstructor(type, new Type[1]{ typeof (Gsc.DOM.IObject) });
            IValue obj2;
            if (constructor1 != null)
            {
              EntityRepository.ISimpleRepository repository = EntityRepository.repository.GetRepository(constructor1.Type);
              foreach (IValue obj3 in (IEnumerable<IValue>) member.Value.GetArray())
              {
                bool flag = obj3.GetObject().TryGetValue("_is_mine", out obj2) && obj2.ToBool();
                repository.Push(constructor1.CreateInstance<IEntity>((object) obj3.GetObject()), (flag ? 1 : 0) != 0);
              }
            }
            else
            {
              AssemblySupport.MethodInfo constructor2 = AssemblySupport.GetConstructor(type, new Type[1]{ typeof (Dictionary<string, object>) });
              EntityRepository.ISimpleRepository repository = EntityRepository.repository.GetRepository(constructor2.Type);
              foreach (IValue node in (IEnumerable<IValue>) member.Value.GetArray())
              {
                bool flag = node.GetObject().TryGetValue("_is_mine", out obj2) && obj2.ToBool();
                repository.Push(constructor2.CreateInstance<IEntity>(Json.Deserialize(node)), (flag ? 1 : 0) != 0);
              }
            }
          }
        }
      }
      if (root.TryGetValue("remove_models", out obj1))
      {
        foreach (IMember member in (IEnumerable<IMember>) obj1.GetObject())
        {
          Type type = AssemblySupport.GetType(string.Format("Gsc.Data.Model.{0}", (object) member.Name));
          if ((object) type != null)
          {
            EntityRepository.ISimpleRepository repository = EntityRepository.repository.GetRepository(type);
            foreach (IValue obj2 in (IEnumerable<IValue>) member.Value.GetArray())
              repository.Remove(obj2.ToString());
          }
        }
      }
      if ((int) ++EntityRepository.ver != 0)
        return;
      EntityRepository.ver = 1U;
    }

    private interface ISimpleRepository
    {
      void Push(IEntity value, bool isPermanent);

      void Remove(string key);

      void AllClear();

      void CacheClear();
    }

    private class SimpleRepository<T> : EntityRepository.ISimpleRepository where T : Entity<T>
    {
      private readonly SortedList<string, T> entityList = new SortedList<string, T>();
      private readonly SortedList<string, T> permanentEntitityList = new SortedList<string, T>();
      private SortedList<string, object> senderList = new SortedList<string, object>();
      public readonly EntityList<T> PublicList;

      public SimpleRepository()
      {
        this.PublicList = new EntityList<T>(this.entityList);
      }

      void EntityRepository.ISimpleRepository.Push(IEntity value, bool isPermanent)
      {
        this.Push((T) value, isPermanent);
      }

      public void Push(T value, bool isPermanent)
      {
        T obj1;
        object sender;
        if (this.entityList.TryGetValue(value.pk, out obj1))
        {
          sender = this.senderList[value.pk];
        }
        else
        {
          object obj2 = new object();
          this.senderList[value.pk] = obj2;
          sender = obj2;
        }
        this.entityList[value.pk] = value;
        if (isPermanent)
          this.permanentEntitityList[value.pk] = value;
        NotificationCenter.Instance.Post<EntityNotification<T>>(new EntityNotification<T>(value, EntityNotificationType.Update), sender);
      }

      public void Remove(string key)
      {
        T entity;
        if (this.entityList.TryGetValue(key, out entity))
        {
          object sender = this.senderList[key];
          this.entityList.Remove(key);
          this.senderList.Remove(key);
          NotificationCenter.Instance.Post<EntityNotification<T>>(new EntityNotification<T>(entity, EntityNotificationType.Remove), sender);
          NotificationCenter.Instance.RemoveObserversWithSender<EntityNotification<T>>(sender);
        }
        this.permanentEntitityList.Remove(key);
      }

      public object GetSender(string key)
      {
        object obj;
        this.senderList.TryGetValue(key, out obj);
        return obj;
      }

      public void AllClear()
      {
        IList<T> values = this.entityList.Values;
        for (int index = 0; index < values.Count; ++index)
          NotificationCenter.Instance.RemoveObserversWithSender<EntityNotification<T>>(this.senderList[values[index].pk]);
        this.entityList.Clear();
        this.senderList.Clear();
        this.permanentEntitityList.Clear();
      }

      public void CacheClear()
      {
        IList<T> values1 = this.entityList.Values;
        for (int index = 0; index < values1.Count; ++index)
        {
          T obj = values1[index];
          if (!this.permanentEntitityList.ContainsKey(obj.pk))
            NotificationCenter.Instance.RemoveObserversWithSender<EntityNotification<T>>(this.senderList[obj.pk]);
        }
        this.entityList.Clear();
        SortedList<string, object> sortedList = new SortedList<string, object>();
        IList<T> values2 = this.permanentEntitityList.Values;
        for (int index = 0; index < values2.Count; ++index)
        {
          T obj = values2[index];
          this.entityList.Add(obj.pk, obj);
          sortedList.Add(obj.pk, this.senderList[obj.pk]);
        }
        this.senderList.Clear();
        this.senderList = sortedList;
      }
    }
  }
}
