// Decompiled with JetBrains decompiler
// Type: SceneRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;

public abstract class SceneRequest : IEnumerator
{
  public abstract bool ActivateScene();

  public abstract bool IsActivated { get; }

  public abstract bool isDone { get; }

  public abstract bool canBeActivated { get; }

  public virtual void Reset()
  {
  }

  public abstract bool MoveNext();

  public abstract object Current { get; }

  public virtual float progress
  {
    get
    {
      return 0.0f;
    }
  }

  public abstract bool isAdditive { get; }
}
