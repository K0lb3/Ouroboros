// Decompiled with JetBrains decompiler
// Type: SRPG.BackHandler
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class BackHandler : MonoBehaviour
  {
    private static List<BackHandler> mHdls = new List<BackHandler>();

    public BackHandler()
    {
      base.\u002Ector();
    }

    private void OnEnable()
    {
      BackHandler.mHdls.Add(this);
    }

    private void OnDisable()
    {
      BackHandler.mHdls.Remove(this);
    }

    public static void Invoke()
    {
      for (int count = BackHandler.mHdls.Count; 0 < count; --count)
      {
        BackHandler mHdl = BackHandler.mHdls[count - 1];
        if (!Object.op_Equality((Object) null, (Object) mHdl))
        {
          Button component = (Button) ((Component) mHdl).get_gameObject().GetComponent<Button>();
          if (Object.op_Inequality((Object) null, (Object) component))
          {
            GraphicRaycaster componentInParent = (GraphicRaycaster) ((Component) mHdl).get_gameObject().GetComponentInParent<GraphicRaycaster>();
            if (!Object.op_Equality((Object) null, (Object) componentInParent) && ((Behaviour) componentInParent).get_enabled())
            {
              CanvasGroup[] componentsInParent = (CanvasGroup[]) ((Component) mHdl).GetComponentsInParent<CanvasGroup>();
              bool flag = false;
              for (int index = 0; index < componentsInParent.Length; ++index)
              {
                if (!componentsInParent[index].get_blocksRaycasts())
                {
                  flag = true;
                  break;
                }
                if (componentsInParent[index].get_ignoreParentGroups())
                  break;
              }
              if (!flag)
              {
                PointerEventData pointerEventData = new PointerEventData(EventSystem.get_current());
                pointerEventData.set_position(Vector2.op_Implicit(((Component) mHdl).get_gameObject().get_transform().get_position()));
                pointerEventData.set_clickCount(1);
                component.OnPointerClick(pointerEventData);
                break;
              }
            }
          }
        }
      }
    }
  }
}
