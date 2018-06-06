// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [ExecuteInEditMode]
  public abstract class EventAction : ScriptableObject
  {
    [NonSerialized]
    public EventScript.Sequence Sequence;
    private bool mEnabled;
    [HideInInspector]
    public bool Skip;
    [NonSerialized]
    public EventAction NextAction;
    public static bool IsPreloading;

    protected EventAction()
    {
      base.\u002Ector();
    }

    public bool enabled
    {
      set
      {
        if (this.mEnabled == value)
          return;
        this.mEnabled = value;
        if (this.mEnabled)
          this.OnActivate();
        else
          this.OnInactivate();
      }
      get
      {
        return this.mEnabled;
      }
    }

    protected void ActivateNext()
    {
      this.ActivateNext(false);
    }

    protected void ActivateNext(bool keepActive)
    {
      this.enabled = keepActive;
      for (EventAction nextAction = this.NextAction; Object.op_Inequality((Object) nextAction, (Object) null); nextAction = nextAction.NextAction)
      {
        if (!nextAction.Skip)
        {
          nextAction.enabled = true;
          break;
        }
      }
    }

    public virtual void OnActivate()
    {
    }

    public virtual void OnInactivate()
    {
    }

    public virtual void Update()
    {
    }

    protected virtual void OnDestroy()
    {
    }

    public virtual bool Forward()
    {
      return false;
    }

    public virtual void SkipImmediate()
    {
    }

    public virtual bool IsPreloadAssets
    {
      get
      {
        return false;
      }
    }

    public virtual bool ReplaySkipButtonEnable()
    {
      return true;
    }

    [DebuggerHidden]
    public virtual IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      EventAction.\u003CPreloadAssets\u003Ec__Iterator60 assetsCIterator60 = new EventAction.\u003CPreloadAssets\u003Ec__Iterator60();
      return (IEnumerator) assetsCIterator60;
    }

    public virtual void PreStart()
    {
    }

    protected Canvas ActiveCanvas
    {
      get
      {
        return EventScript.Canvas;
      }
    }

    public static GameObject FindActor(string actorID)
    {
      if (string.IsNullOrEmpty(actorID))
        return (GameObject) null;
      TacticsUnitController byUnitId;
      if (Object.op_Inequality((Object) (byUnitId = TacticsUnitController.FindByUnitID(actorID)), (Object) null))
        return ((Component) byUnitId).get_gameObject();
      TacticsUnitController byUniqueName;
      if (Object.op_Inequality((Object) (byUniqueName = TacticsUnitController.FindByUniqueName(actorID)), (Object) null))
        return ((Component) byUniqueName).get_gameObject();
      return GameObjectID.FindGameObject(actorID);
    }

    protected static Vector3 PointToWorld(IntVector2 pt)
    {
      Vector3 position;
      // ISSUE: explicit reference operation
      ((Vector3) @position).\u002Ector((float) pt.x + 0.5f, 0.0f, (float) pt.y + 0.5f);
      position.y = GameUtility.RaycastGround(position).y;
      return position;
    }

    public static bool IsLoading
    {
      get
      {
        return EventAction.IsPreloading;
      }
    }
  }
}
