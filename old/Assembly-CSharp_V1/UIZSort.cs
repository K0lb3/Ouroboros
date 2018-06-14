// Decompiled with JetBrains decompiler
// Type: UIZSort
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("UI/Z Sort")]
public class UIZSort : MonoBehaviour
{
  private Transform mTransform;

  public UIZSort()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.mTransform = ((Component) this).get_transform();
    this.Update();
  }

  private void Update()
  {
    int childCount = this.mTransform.get_childCount();
    if (childCount <= 0)
      return;
    Transform[] transformArray = new Transform[childCount];
    bool flag = false;
    for (int index = 0; index < childCount; ++index)
      transformArray[index] = this.mTransform.GetChild(index);
    for (int index1 = 0; index1 < childCount; ++index1)
    {
      float z = (float) transformArray[index1].get_position().z;
      int index2 = index1;
      for (int index3 = index1 + 1; index3 < childCount; ++index3)
      {
        if (transformArray[index3].get_position().z > (double) z)
        {
          index2 = index3;
          z = (float) transformArray[index3].get_position().z;
        }
      }
      if (index2 != index1)
      {
        Transform transform = transformArray[index2];
        int index3 = childCount - 1;
        int index4 = childCount - 1;
        for (; index3 >= index1; --index3)
        {
          if (index3 != index2)
          {
            transformArray[index4] = transformArray[index3];
            --index4;
          }
        }
        flag = true;
        transformArray[index1] = transform;
      }
    }
    if (!flag)
      return;
    for (int index = 0; index < childCount; ++index)
      transformArray[index].SetSiblingIndex(index);
  }
}
