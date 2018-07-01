// Decompiled with JetBrains decompiler
// Type: SRPG.NotifyListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
