// Decompiled with JetBrains decompiler
// Type: FlowNodePersistent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
