// Decompiled with JetBrains decompiler
// Type: Gsc.Components.NotificationCenter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gsc.Components
{
  public class NotificationCenter
  {
    private readonly Dictionary<Type, NotificationCenter.ObserverList> observers = new Dictionary<Type, NotificationCenter.ObserverList>();
    private static NotificationCenter _instance;

    public static NotificationCenter Instance
    {
      get
      {
        if (NotificationCenter._instance == null)
          NotificationCenter._instance = new NotificationCenter();
        return NotificationCenter._instance;
      }
    }

    public void AddObserver<TMessage>(NotificationObserver<TMessage> observer) where TMessage : INotification
    {
      this.AddObserver<TMessage>(observer, (object) null);
    }

    public void AddObserver<TMessage>(NotificationObserver<TMessage> observer, object sender) where TMessage : INotification
    {
      NotificationCenter.ObserverList observerList;
      if (!this.observers.TryGetValue(typeof (TMessage), out observerList))
      {
        observerList = new NotificationCenter.ObserverList();
        this.observers.Add(typeof (TMessage), observerList);
      }
      observerList.AddObserver((Delegate) observer, sender);
    }

    public void RemoveObserver<TMessage>(NotificationObserver<TMessage> observer) where TMessage : INotification
    {
      NotificationCenter.ObserverList observerList;
      if (!this.observers.TryGetValue(typeof (TMessage), out observerList))
        return;
      observerList.RemoveObserver((Delegate) observer);
      if (!observerList.isEmpty)
        return;
      this.observers.Remove(typeof (TMessage));
    }

    public void RemoveObserversWithSender<TMessage>(object sender) where TMessage : INotification
    {
      this.RemoveObserversWithSender(typeof (TMessage), sender);
    }

    public void RemoveObserversWithSender(object sender)
    {
      using (Dictionary<Type, NotificationCenter.ObserverList>.Enumerator enumerator = this.observers.GetEnumerator())
      {
        while (enumerator.MoveNext())
          this.RemoveObserversWithSender(enumerator.Current.Key, sender);
      }
    }

    public void RemoveObserversWithSender(Type messageType, object sender)
    {
      NotificationCenter.ObserverList observerList;
      if (!this.observers.TryGetValue(messageType, out observerList))
        return;
      observerList.RemoveObserversWithSender(sender);
      if (!observerList.isEmpty)
        return;
      this.observers.Remove(messageType);
    }

    public void Post<TMessage>(TMessage message, object sender = null) where TMessage : INotification
    {
      NotificationCenter.ObserverList observerList;
      if (!this.observers.TryGetValue(typeof (TMessage), out observerList))
        return;
      observerList.Post<TMessage>(ref message, sender);
      if (!observerList.isEmpty)
        return;
      this.observers.Remove(typeof (TMessage));
    }

    private class Observer
    {
      public Behaviour target;
      public Delegate handler;
      public object sender;
    }

    private class ObserverList
    {
      private static readonly List<NotificationCenter.Observer> deadObservers = new List<NotificationCenter.Observer>();
      private readonly List<NotificationCenter.Observer> aliveObservers = new List<NotificationCenter.Observer>();

      public bool isEmpty
      {
        get
        {
          return this.aliveObservers.Count == 0;
        }
      }

      public void AddObserver(Delegate handler, object sender)
      {
        Behaviour target = handler.Target as Behaviour;
        if (!Object.op_Inequality((Object) target, (Object) null))
          return;
        NotificationCenter.Observer observer = this.aliveObservers.Where<NotificationCenter.Observer>((Func<NotificationCenter.Observer, bool>) (x => x.handler == handler)).FirstOrDefault<NotificationCenter.Observer>();
        if (observer != null)
          observer.sender = sender;
        else
          this.aliveObservers.Add(new NotificationCenter.Observer()
          {
            target = target,
            handler = handler,
            sender = sender
          });
      }

      public void RemoveObserver(Delegate handler)
      {
        if (this.aliveObservers.Count <= 0)
          return;
        for (int index = this.aliveObservers.Count - 1; index >= 0; --index)
        {
          NotificationCenter.Observer aliveObserver = this.aliveObservers[index];
          if (aliveObserver.handler == handler)
            NotificationCenter.ObserverList.deadObservers.Add(aliveObserver);
        }
        this.RemoveDeadObservers();
      }

      public void RemoveObserversWithSender(object sender)
      {
        if (this.aliveObservers.Count <= 0)
          return;
        for (int index = this.aliveObservers.Count - 1; index >= 0; --index)
        {
          NotificationCenter.Observer aliveObserver = this.aliveObservers[index];
          if (aliveObserver.sender != null && object.ReferenceEquals(aliveObserver.sender, sender))
            NotificationCenter.ObserverList.deadObservers.Add(aliveObserver);
        }
        this.RemoveDeadObservers();
      }

      public void Post<TMessage>(ref TMessage message, object sender)
      {
        if (this.aliveObservers.Count <= 0)
          return;
        for (int index = this.aliveObservers.Count - 1; index >= 0; --index)
        {
          NotificationCenter.Observer aliveObserver = this.aliveObservers[index];
          if (Object.op_Equality((Object) aliveObserver.target, (Object) null))
            NotificationCenter.ObserverList.deadObservers.Add(aliveObserver);
          else if (aliveObserver.target.get_enabled() && (aliveObserver.sender == null || object.ReferenceEquals(sender, aliveObserver.sender)))
            ((NotificationObserver<TMessage>) aliveObserver.handler)(message);
        }
        this.RemoveDeadObservers();
      }

      private void RemoveDeadObservers()
      {
        if (NotificationCenter.ObserverList.deadObservers.Count <= 0)
          return;
        if (this.aliveObservers.Count <= NotificationCenter.ObserverList.deadObservers.Count)
        {
          this.aliveObservers.Clear();
        }
        else
        {
          for (int index = NotificationCenter.ObserverList.deadObservers.Count - 1; index >= 0; --index)
            this.aliveObservers.Remove(NotificationCenter.ObserverList.deadObservers[index]);
        }
        NotificationCenter.ObserverList.deadObservers.Clear();
      }
    }
  }
}
