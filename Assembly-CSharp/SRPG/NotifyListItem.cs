// Decompiled with JetBrains decompiler
// Type: SRPG.NotifyListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class NotifyListItem : MonoBehaviour
  {
    public Text Message;
    [NonSerialized]
    public float Lifetime;
    [NonSerialized]
    public float Height;

    public NotifyListItem()
    {
      base.\u002Ector();
    }
  }
}
