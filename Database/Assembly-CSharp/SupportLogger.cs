// Decompiled with JetBrains decompiler
// Type: SupportLogger
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
