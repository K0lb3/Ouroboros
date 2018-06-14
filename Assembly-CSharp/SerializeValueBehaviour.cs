// Decompiled with JetBrains decompiler
// Type: SerializeValueBehaviour
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SerializeValueBehaviour : MonoBehaviour
{
  [SerializeField]
  private SerializeValueList m_List;

  public SerializeValueBehaviour()
  {
    base.\u002Ector();
  }

  public SerializeValueList list
  {
    get
    {
      return this.m_List;
    }
  }

  private void Awake()
  {
    this.m_List.Initialize();
  }
}
