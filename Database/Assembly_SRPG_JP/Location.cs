// Decompiled with JetBrains decompiler
// Type: SRPG.Location
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
        return Input.get_location().get_isEnabledByUser();
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
      return (IEnumerator) new Location.\u003CCoroutine_UpdateLocation\u003Ec__Iterator7B()
      {
        \u003C\u003Ef__this = this
      };
    }

    public void Start(Action<Location> success, Action<Location> failed)
    {
      if (this.m_Task != null)
        return;
      this.m_Success = success;
      this.m_Failed = failed;
      this.m_Latitude = this.m_Longitude = 0.0f;
      this.m_Task = this.Coroutine_UpdateLocation();
      this.OnStart();
    }

    public void Start()
    {
      this.Start((Action<Location>) null, (Action<Location>) null);
    }

    public void End()
    {
      if (this.m_Task == null)
        return;
      this.OnEnd();
      if (this.m_Result == Location.Result.Success)
      {
        if (this.m_Success != null)
          this.m_Success(this);
      }
      else
      {
        if (this.m_Result == Location.Result.Working)
        {
          Input.get_location().Stop();
          this.m_Latitude = this.m_Longitude = 0.0f;
        }
        if (this.m_Failed != null)
          this.m_Failed(this);
      }
      this.m_Success = (Action<Location>) null;
      this.m_Failed = (Action<Location>) null;
      this.m_Task = (IEnumerator) null;
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
