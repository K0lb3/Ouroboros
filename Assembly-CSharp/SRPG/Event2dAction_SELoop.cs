// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SELoop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/SE再生(2D)", "SEを再生します", 5592405, 4473992)]
  public class Event2dAction_SELoop : EventAction
  {
    public static List<Event2dAction_SELoop> instances = new List<Event2dAction_SELoop>();
    private static readonly float MinInterval = 0.1f;
    private static readonly int MinCount = 0;
    private static readonly string SECueSheetName = nameof (SE);
    [HideInInspector]
    public float Interval = 1f;
    [HideInInspector]
    public int Count = 1;
    public string SE;
    public bool Loop;
    [HideInInspector]
    public bool async;
    [HideInInspector]
    public string SE_ID;
    private float mTimer;
    private int mCounter;
    private MySound.CueSheetHandle mHandleSE;

    public MySound.CueSheetHandle HandleSE
    {
      get
      {
        return this.mHandleSE;
      }
    }

    public static Event2dAction_SELoop Find(string id)
    {
      for (int index = 0; index < Event2dAction_SELoop.instances.Count; ++index)
      {
        if (Event2dAction_SELoop.instances[index].SE_ID == id)
          return Event2dAction_SELoop.instances[index];
      }
      return (Event2dAction_SELoop) null;
    }

    public override void PreStart()
    {
      this.mHandleSE = MySound.CueSheetHandle.Create(Event2dAction_SELoop.SECueSheetName, MySound.EType.SE, true, true, false, false);
      if (this.mHandleSE == null)
        return;
      this.mHandleSE.CreateDefaultOneShotSource();
      Event2dAction_SELoop.instances.Add(this);
    }

    public override void OnActivate()
    {
      if (this.Loop)
      {
        this.mTimer = 0.0f;
        this.mCounter = this.Count;
        if (!this.async)
          return;
        this.ActivateNext(true);
      }
      else
      {
        if (this.mHandleSE != null)
          this.mHandleSE.PlayDefaultOneShot(this.SE, false, 0.0f, false);
        this.ActivateNext();
      }
    }

    public override void Update()
    {
      this.mTimer -= Time.get_deltaTime();
      if ((double) this.mTimer > 0.0)
        return;
      if (this.mHandleSE != null)
        this.mHandleSE.PlayDefaultOneShot(this.SE, false, 0.0f, false);
      this.mTimer = this.Interval;
      if (this.mCounter == 0 || --this.mCounter > 0)
        return;
      if (this.async)
        this.enabled = false;
      else
        this.ActivateNext();
    }
  }
}
