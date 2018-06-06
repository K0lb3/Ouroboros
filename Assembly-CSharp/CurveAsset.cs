// Decompiled with JetBrains decompiler
// Type: CurveAsset
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class CurveAsset : ScriptableObject
{
  public CurveAsset.CurveStruct[] Curves;

  public CurveAsset()
  {
    base.\u002Ector();
  }

  public AnimationCurve FindCurve(string name)
  {
    for (int index = this.Curves.Length - 1; index >= 0; --index)
    {
      if (this.Curves[index].Name == name)
        return this.Curves[index].Curve;
    }
    return (AnimationCurve) null;
  }

  [Serializable]
  public struct CurveStruct
  {
    public string Name;
    public AnimationCurve Curve;
  }
}
