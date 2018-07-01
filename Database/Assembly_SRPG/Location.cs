// Decompiled with JetBrains decompiler
// Type: SRPG.Location
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class Location
  {
    private const float TIMEOUT = 60f;
    private Location.Result m_Result;
    private float m_Latitude;
    private float m_Longitude;
    private IEnumerator m_Task;
    private Action<Location> m_Success;
    private Action<Location> m_Failed;

    public Vector2 location
    {
      get
      {
        return new Vector2(this.m_Latitude, this.m_Longitude);
      }
    }

    public float latitude
    {
      get
      {
        return this.m_Latitude;
      }
    }

    public float longitude
    {
      get
      {
        return this.m_Longitude;
      }
    }

    public Location.Result result
    {
      get
      {
        return this.m_Result;
      }
    }

    public static bool isGPSEnable
    {
      get
      {
        return false;
      }
    }

    public void Initialize()
    {
      this.m_Result = Location.Result.None;
      this.m_Latitude = 0.0f;
      this.m_Longitude = 0.0f;
      this.m_Success = (Action<Location>) null;
      this.m_Failed = (Action<Location>) null;
      this.m_Task = (IEnumerator) null;
    }

    public void Release()
    {
      this.End();
    }

    public void Update()
    {
      if (this.m_Task == null || this.m_Task.MoveNext())
        return;
      this.End();
    }

    [DebuggerHidden]
    private IEnumerator Coroutine_UpdateLocation()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Location.\u003CCoroutine_UpdateLocation\u003Ec__Iterator8F locationCIterator8F = new Location.\u003CCoroutine_UpdateLocation\u003Ec__Iterator8F();
      return (IEnumerator) locationCIterator8F;
    }

    public void Start(Action<Location> success, Action<Location> failed)
    {
      if (this.m_Task == null)
        ;
    }

    public void Start()
    {
      this.Start((Action<Location>) null, (Action<Location>) null);
    }

    public void End()
    {
    }

    public bool IsBusy()
    {
      return this.m_Task != null;
    }

    private void OnStart()
    {
    }

    private void OnEnd()
    {
    }

    public enum Result
    {
      None,
      Working,
      Success,
      Timeout,
      DeviceUnable,
    }
  }
}
