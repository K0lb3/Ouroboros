// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped : MonoBehaviour, ScrollListSetUp
  {
    private int m_Max;
    private int[] m_CategoryNum;

    public ScrollClamped()
    {
      base.\u002Ector();
    }

    public void Start()
    {
      string s1 = LocalizedText.Get("help.MENU_L_NUM");
      if (string.IsNullOrEmpty(s1))
        return;
      int length = int.Parse(s1);
      string s2 = LocalizedText.Get("help.MENU_NUM");
      if (string.IsNullOrEmpty(s2))
        return;
      int num1 = int.Parse(s2);
      this.m_CategoryNum = new int[length];
      int num2 = 0;
      for (int index = 0; index < length; ++index)
      {
        string b = LocalizedText.Get("help.MENU_CATE_NAME_" + (object) (index + 1));
        int num3 = 0;
        for (; num2 < num1 && string.Equals(LocalizedText.Get("help.MENU_CATE_" + (object) (num2 + 1)), b); ++num2)
          ++num3;
        this.m_CategoryNum[index] = num3;
      }
    }

    public void OnSetUpItems()
    {
      HelpWindow componentInParent = (HelpWindow) ((Component) ((Component) this).get_transform()).GetComponentInParent<HelpWindow>();
      if (Object.op_Equality((Object) componentInParent, (Object) null))
        return;
      ScrollListController component1 = (ScrollListController) ((Component) this).GetComponent<ScrollListController>();
      ScrollListController.OnItemPositionChange onItemUpdate = component1.OnItemUpdate;
      ScrollClamped scrollClamped = this;
      // ISSUE: virtual method pointer
      UnityAction<int, GameObject> unityAction = new UnityAction<int, GameObject>((object) scrollClamped, __vmethodptr(scrollClamped, OnUpdateItems));
      onItemUpdate.AddListener(unityAction);
      ((ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>()).set_movementType((ScrollRect.MovementType) 2);
      RectTransform component2 = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.get_sizeDelta();
      if (componentInParent.MiddleHelp)
      {
        this.m_Max = this.m_CategoryNum[componentInParent.SelectMiddleID];
      }
      else
      {
        string s = LocalizedText.Get("help.MENU_L_NUM");
        if (string.IsNullOrEmpty(s))
          return;
        this.m_Max = int.Parse(s);
      }
      sizeDelta.y = (__Null) ((double) component1.ItemScale * 1.20000004768372 * (double) this.m_Max);
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
        obj.SendMessage("UpdateParam", (object) idx);
      }
    }
  }
}
