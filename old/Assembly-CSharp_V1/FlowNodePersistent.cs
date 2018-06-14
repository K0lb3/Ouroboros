// Decompiled with JetBrains decompiler
// Type: FlowNodePersistent
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public abstract class FlowNodePersistent : FlowNode
{
  protected override void Awake()
  {
    base.Awake();
    ((Behaviour) this).set_enabled(true);
  }
}
