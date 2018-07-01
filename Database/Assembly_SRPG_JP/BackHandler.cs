// Decompiled with JetBrains decompiler
// Type: SRPG.BackHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Implicit((Object) instance) && !instance.IsControlBattleUI(SceneBattle.eMaskBattleUI.BACK_KEY))
        return;
      for (int count = BackHandler.mHdls.Count; 0 < count; --count)
      {
        BackHandler mHdl = BackHandler.mHdls[count - 1];
        if (!Object.op_Equality((Object) null, (Object) mHdl))
        {
          ButtonEvent component1 = (ButtonEvent) ((Component) mHdl).get_gameObject().GetComponent<ButtonEvent>();
          if (Object.op_Inequality((Object) component1, (Object) null))
          {
            GraphicRaycaster componentInParent = (GraphicRaycaster) ((Component) mHdl).get_gameObject().GetComponentInParent<GraphicRaycaster>();
            if (!Object.op_Equality((Object) componentInParent, (Object) null) && ((Behaviour) componentInParent).get_enabled())
            {
              Graphic component2 = (Graphic) ((Component) mHdl).get_gameObject().GetComponent<Graphic>();
              if (!Object.op_Equality((Object) component2, (Object) null) && ((Behaviour) component2).get_enabled() && component2.get_raycastTarget())
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
                  PointerEventData data = new PointerEventData(EventSystem.get_current());
                  data.set_position(Vector2.op_Implicit(((Component) mHdl).get_gameObject().get_transform().get_position()));
                  data.set_clickCount(1);
                  component1.OnPointerClick(data);
                  break;
                }
              }
            }
          }
          else
          {
            Button component2 = (Button) ((Component) mHdl).get_gameObject().GetComponent<Button>();
            if (Object.op_Inequality((Object) null, (Object) component2))
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
                  component2.OnPointerClick(pointerEventData);
                  break;
                }
              }
            }
          }
        }
      }
    }
  }
}
