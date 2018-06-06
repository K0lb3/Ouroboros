// Decompiled with JetBrains decompiler
// Type: SupportLogger
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SupportLogger : MonoBehaviour
{
  public bool LogTrafficStats;

  public SupportLogger()
  {
    base.\u002Ector();
  }

  public void Start()
  {
    if (!Object.op_Equality((Object) GameObject.Find("PunSupportLogger"), (Object) null))
      return;
    GameObject gameObject = new GameObject("PunSupportLogger");
    Object.DontDestroyOnLoad((Object) gameObject);
    ((SupportLogging) gameObject.AddComponent<SupportLogging>()).LogTrafficStats = this.LogTrafficStats;
  }
}
