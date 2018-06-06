// Decompiled with JetBrains decompiler
// Type: SRPG.HelpButtonItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class HelpButtonItem : MonoBehaviour
  {
    public GameObject HelpMainTemplate;
    public GameObject m_MainWindowBase;
    public GameObject m_HelpMain;

    public HelpButtonItem()
    {
      base.\u002Ector();
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void UpdateParam(int Idx)
    {
      HelpWindow componentInParent = (HelpWindow) ((Component) ((Component) this).get_transform()).GetComponentInParent<HelpWindow>();
      if (Object.op_Equality((Object) componentInParent, (Object) null))
        return;
      int num1 = Idx;
      Transform child = ((Component) this).get_transform().FindChild("Label");
      if (Object.op_Inequality((Object) child, (Object) null))
      {
        LText component = (LText) ((Component) child).GetComponent<LText>();
        if (componentInParent.MiddleHelp)
        {
          string a = LocalizedText.Get("help.MENU_CATE_NAME_" + (object) (componentInParent.SelectMiddleID + 1));
          string s = LocalizedText.Get("help.MENU_NUM");
          if (string.IsNullOrEmpty(s))
            return;
          int num2 = int.Parse(s);
          int num3 = 0;
          for (int index = 0; index < num2; ++index)
          {
            string b = LocalizedText.Get("help.MENU_CATE_" + (object) (index + 1));
            if (string.Equals(a, b))
            {
              num3 = index;
              break;
            }
          }
          component.set_text(LocalizedText.Get("help.MENU_" + (object) (num3 + Idx + 1)));
          num1 = num3 + Idx;
        }
        else
          component.set_text(LocalizedText.Get("help.MENU_CATE_NAME_" + (object) (Idx + 1)));
      }
      Button component1 = (Button) ((Component) ((Component) this).get_transform()).GetComponent<Button>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      Func<int, UnityAction> func = (Func<int, UnityAction>) (n => new UnityAction((object) new HelpButtonItem.\u003CUpdateParam\u003Ec__AnonStorey250() { n = n, \u003C\u003Ef__this = this }, __methodptr(\u003C\u003Em__29B)));
      ((UnityEventBase) component1.get_onClick()).RemoveAllListeners();
      ((UnityEvent) component1.get_onClick()).AddListener(func(num1));
    }

    private void OnSelectMenu(int MenuID)
    {
      HelpWindow componentInParent = (HelpWindow) ((Component) ((Component) this).get_transform()).GetComponentInParent<HelpWindow>();
      if (Object.op_Equality((Object) componentInParent, (Object) null))
        return;
      if (componentInParent.MiddleHelp)
        componentInParent.CreateMainWindow(MenuID);
      else
        componentInParent.UpdateHelpList(true, MenuID);
    }
  }
}
