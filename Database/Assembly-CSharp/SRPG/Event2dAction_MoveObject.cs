// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MoveObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("オブジェクト/パン(2D)", "パン", 5592405, 4473992)]
  public class Event2dAction_MoveObject : EventAction
  {
    public Vector3 MoveFrom;
    public Vector3 MoveTo;
    public float MoveTime;
    public bool Async;
    private Vector3 FromPosition;
    private Vector3 ToPosition;

    public override void PreStart()
    {
    }

    public override void OnActivate()
    {
    }

    public override void Update()
    {
    }
  }
}
