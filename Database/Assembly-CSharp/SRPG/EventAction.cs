// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
      for (EventAction nextAction = this.NextAction; UnityEngine.Object.op_Inequality((UnityEngine.Object) nextAction, (UnityEngine.Object) null); nextAction = nextAction.NextAction)
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

    public virtual void GoToEndState()
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
      EventAction.\u003CPreloadAssets\u003Ec__Iterator3E assetsCIterator3E = new EventAction.\u003CPreloadAssets\u003Ec__Iterator3E();
      return (IEnumerator) assetsCIterator3E;
    }

    public virtual void PreStart()
    {
    }

    public virtual string[] GetUnManagedAssetListData()
    {
      return (string[]) null;
    }

    public static string[] GetUnManagedStreamAssets(string[] pair, bool isBGM = false)
    {
      if (pair != null)
      {
        if (isBGM)
        {
          string s = pair[0];
          if (s.IndexOf("BGM_") != -1)
            s = pair[0].Replace("BGM_", string.Empty);
          int result = -1;
          if (int.TryParse(s, out result) && result >= EventAction_BGM.DEMO_BGM_ST && result <= EventAction_BGM.DEMO_BGM_ED)
            return new string[2]{ "sound/BGM_" + s + ".awb", "sound/BGM_" + s + ".acb" };
        }
        else
        {
          if (pair[0].IndexOf("VO_") != -1)
            return (string[]) null;
          return new string[2]{ "sound/" + pair[0] + ".awb", "sound/" + pair[0] + ".acb" };
        }
      }
      return (string[]) null;
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) (byUnitId = TacticsUnitController.FindByUnitID(actorID)), (UnityEngine.Object) null))
        return ((Component) byUnitId).get_gameObject();
      TacticsUnitController byUniqueName;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) (byUniqueName = TacticsUnitController.FindByUniqueName(actorID)), (UnityEngine.Object) null))
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

    public static string[] GetStreamResources(string[] pair)
    {
      if (pair == null)
        return new string[0];
      if (pair[0].IndexOf("VO_") != 0)
        return new string[0];
      return new string[2]{ pair[0] + ".awb", pair[0] + ".acb" };
    }

    public static bool IsUnManagedAssets(string name, bool isBGM = false)
    {
      if (string.IsNullOrEmpty(name))
        return false;
      if (isBGM)
      {
        string s = name;
        if (s.IndexOf("BGM_") != -1)
          s = name.Replace("BGM_", string.Empty);
        int result = -1;
        return int.TryParse(s, out result) && result >= EventAction_BGM.DEMO_BGM_ST && result <= EventAction_BGM.DEMO_BGM_ED;
      }
      return name.IndexOf("VO_") == -1;
    }
  }
}
