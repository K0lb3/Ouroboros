// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_VersusTowerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_VersusTowerInfo : MonoBehaviour, ScrollListSetUp
  {
    private readonly int MARGIN;
    public float Space;
    private int m_Max;

    public ScrollClamped_VersusTowerInfo()
    {
      base.\u002Ector();
    }

    public void Start()
    {
    }

    public void OnSetUpItems()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      VersusTowerParam[] versusTowerParam = instance.GetVersusTowerParam();
      if (versusTowerParam != null)
      {
        for (int index = 0; index < versusTowerParam.Length; ++index)
        {
          if (string.Equals((string) versusTowerParam[index].VersusTowerID, instance.VersusTowerMatchName))
            ++this.m_Max;
        }
        this.m_Max += this.MARGIN;
      }
      ScrollListController component1 = (ScrollListController) ((Component) this).GetComponent<ScrollListController>();
      ScrollListController.OnItemPositionChange onItemUpdate = component1.OnItemUpdate;
      ScrollClamped_VersusTowerInfo clampedVersusTowerInfo = this;
      // ISSUE: virtual method pointer
      UnityAction<int, GameObject> unityAction = new UnityAction<int, GameObject>((object) clampedVersusTowerInfo, __vmethodptr(clampedVersusTowerInfo, OnUpdateItems));
      onItemUpdate.AddListener(unityAction);
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_movementType((ScrollRect.MovementType) 2);
      RectTransform component2 = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.get_sizeDelta();
      sizeDelta.y = (__Null) ((double) component1.ItemScale * (double) this.Space * (double) this.m_Max);
      component2.set_sizeDelta(sizeDelta);
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (idx < 0 || idx >= this.m_Max)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        VersusTowerFloor component = (VersusTowerFloor) obj.GetComponent<VersusTowerFloor>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.Refresh(idx, this.m_Max - this.MARGIN);
      }
    }
  }
}
