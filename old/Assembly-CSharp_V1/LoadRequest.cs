// Decompiled with JetBrains decompiler
// Type: LoadRequest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadRequest : IEnumerator
{
  public Coroutine StartCoroutine()
  {
    return AssetManager.Instance.StartCoroutine((IEnumerator) this);
  }

  public virtual Object asset
  {
    get
    {
      return (Object) null;
    }
  }

  public virtual bool isDone
  {
    get
    {
      return true;
    }
  }

  public void Reset()
  {
  }

  public virtual bool MoveNext()
  {
    return false;
  }

  public object Current
  {
    get
    {
      return (object) null;
    }
  }

  public virtual float progress
  {
    get
    {
      return 0.0f;
    }
  }

  public virtual void KeepSourceAlive()
  {
  }

  protected void UntrackTextComponents(Object obj)
  {
    if (!(obj is GameObject))
      return;
    foreach (Text componentsInChild in (Text[]) (obj as GameObject).GetComponentsInChildren<Text>(true))
      FontUpdateTracker.UntrackText(componentsInChild);
  }
}
