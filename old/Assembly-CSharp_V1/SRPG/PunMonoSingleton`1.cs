// Decompiled with JetBrains decompiler
// Type: SRPG.PunMonoSingleton`1
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using UnityEngine;

namespace SRPG
{
  public abstract class PunMonoSingleton<T> : PunBehaviour where T : PunBehaviour
  {
    private static T instance_;

    public static T Instance
    {
      get
      {
        if (Object.op_Equality((Object) (object) PunMonoSingleton<T>.instance_, (Object) null))
        {
          System.Type type = typeof (T);
          PunMonoSingleton<T>.instance_ = Object.FindObjectOfType(type) as T;
          if (Object.op_Equality((Object) (object) PunMonoSingleton<T>.instance_, (Object) null))
          {
            GameObject gameObject = new GameObject(type.ToString(), new System.Type[1]{ type });
            if (!Object.op_Equality((Object) gameObject, (Object) null))
              ;
            ((Object) gameObject.get_transform()).set_name(type.Name);
            PunMonoSingleton<T>.instance_ = (T) gameObject.GetComponent<T>();
            if (Object.op_Equality((Object) (object) PunMonoSingleton<T>.instance_, (Object) null))
              Object.Destroy((Object) gameObject);
          }
        }
        return PunMonoSingleton<T>.instance_;
      }
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) (object) PunMonoSingleton<T>.Instance, (Object) this))
        Object.Destroy((Object) this);
      else
        this.Initialize();
    }

    protected virtual void Initialize()
    {
    }

    protected virtual void Release()
    {
    }

    private void OnDestroy()
    {
      this.Release();
      PunMonoSingleton<T>.instance_ = (T) null;
    }

    private void OnApplicationQuit()
    {
      this.Release();
      PunMonoSingleton<T>.instance_ = (T) null;
    }
  }
}
