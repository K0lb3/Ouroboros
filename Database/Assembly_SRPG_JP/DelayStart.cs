// Decompiled with JetBrains decompiler
// Type: SRPG.DelayStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class DelayStart : MonoBehaviour
  {
    public float ActivateInterval;
    private List<GameObject> mChildren;
    private int mActivateIndex;
    private float mInterval;

    public DelayStart()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      Transform transform = ((Component) this).get_transform();
      int childCount = transform.get_childCount();
      for (int index = 0; index < childCount; ++index)
      {
        this.mChildren.Add(((Component) transform.GetChild(index)).get_gameObject());
        this.mChildren[index].SetActive(false);
      }
      this.mInterval = 0.0f;
    }

    private void Update()
    {
      if (this.mActivateIndex < this.mChildren.Count)
      {
        this.mInterval -= Time.get_deltaTime();
        if ((double) this.mInterval > 0.0)
          return;
        this.mChildren[this.mActivateIndex++].SetActive(true);
        this.mInterval = this.ActivateInterval;
      }
      else
      {
        for (int index = 0; index < this.mChildren.Count; ++index)
        {
          if (Object.op_Inequality((Object) this.mChildren[index], (Object) null))
            return;
        }
        Object.Destroy((Object) ((Component) this).get_gameObject());
      }
    }
  }
}
