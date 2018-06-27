// Decompiled with JetBrains decompiler
// Type: SRPG.HelpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class HelpWindow : MonoBehaviour
  {
    public static readonly string VAR_NAME_MENU_ID = "HELP_MENU_ID";
    public bool ReferenceFlowVariable;
    public Button BackButton;
    public Button MiddleBackButton;
    private bool bMiddleHelp;
    private int SelMiddleIdx;
    private GameObject[] mHelpMenuButtons;
    public GameObject m_HelpMain;

    public HelpWindow()
    {
      base.\u002Ector();
    }

    public bool MiddleHelp
    {
      get
      {
        return this.bMiddleHelp;
      }
    }

    public int SelectMiddleID
    {
      get
      {
        return this.SelMiddleIdx;
      }
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.BackButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BackButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnCloseMain)));
        ((Text) ((Component) ((Component) this.BackButton).get_transform().FindChild("Text")).GetComponent<LText>()).set_text(LocalizedText.Get("help.BACK_BUTTON"));
      }
      if (Object.op_Inequality((Object) this.MiddleBackButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.MiddleBackButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnBackList)));
        ((Text) ((Component) ((Component) this.MiddleBackButton).get_transform().FindChild("Text")).GetComponent<LText>()).set_text(LocalizedText.Get("help.BACK_BUTTON"));
      }
      string s1 = LocalizedText.Get("help.MENU_NUM");
      if (string.IsNullOrEmpty(s1))
        return;
      this.mHelpMenuButtons = new GameObject[int.Parse(s1)];
      if (!this.ReferenceFlowVariable)
        return;
      string s2 = FlowNode_Variable.Get(HelpWindow.VAR_NAME_MENU_ID);
      if (string.IsNullOrEmpty(s2))
        return;
      int result;
      if (int.TryParse(s2, out result))
        this.CreateMainWindow(result - 1);
      FlowNode_Variable.Set(HelpWindow.VAR_NAME_MENU_ID, string.Empty);
    }

    private void OnDestroy()
    {
      GameUtility.DestroyGameObjects(this.mHelpMenuButtons);
    }

    private void OnCloseMain()
    {
      this.m_HelpMain.SetActive(false);
      if (Object.op_Implicit((Object) this.MiddleBackButton))
        ((Component) this.MiddleBackButton).get_gameObject().SetActive(true);
      ((Component) this.BackButton).get_gameObject().SetActive(false);
    }

    public void OnBackList()
    {
      if (Object.op_Implicit((Object) this.MiddleBackButton))
        ((Component) this.MiddleBackButton).get_gameObject().SetActive(false);
      this.UpdateHelpList(false, 0);
    }

    public void UpdateHelpList(bool flag, int Idx)
    {
      GameObject gameObject = GameObject.Find("View");
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      ScrollListController component1 = (ScrollListController) gameObject.GetComponent<ScrollListController>();
      this.bMiddleHelp = flag;
      this.SelMiddleIdx = Idx;
      component1.UpdateList();
      if (this.MiddleHelp)
        ((Component) this.MiddleBackButton).get_gameObject().SetActive(true);
      Transform child = ((Component) this).get_transform().FindChild("window/header");
      if (!Object.op_Inequality((Object) child, (Object) null))
        return;
      LText component2 = (LText) ((Component) child.FindChild("Text")).GetComponent<LText>();
      if (!Object.op_Inequality((Object) component2, (Object) null))
        return;
      string str = LocalizedText.Get("help.TITLE");
      if (this.MiddleHelp)
        str = str + "-" + LocalizedText.Get("help.MENU_CATE_NAME_" + (object) (Idx + 1));
      if (string.IsNullOrEmpty(str))
        return;
      component2.set_text(str);
    }

    public void CreateMainWindow(int MenuID)
    {
      string s = LocalizedText.Get("help.MENU_NUM");
      if (s == null)
        return;
      int num1 = int.Parse(s);
      if (MenuID < 0 || MenuID >= num1 || Object.op_Equality((Object) this.m_HelpMain, (Object) null))
        return;
      int num2 = MenuID + 1;
      float num3 = 0.0f;
      Transform child1 = this.m_HelpMain.get_transform().FindChild("header");
      if (Object.op_Inequality((Object) child1, (Object) null))
      {
        LText component = (LText) ((Component) child1.FindChild("Text")).GetComponent<LText>();
        string str = LocalizedText.Get("help.MAIN_TITLE_" + (object) num2);
        if (string.IsNullOrEmpty(str) || str == "MAIN_TITLE_" + (object) num2)
          ((Component) child1).get_gameObject().SetActive(false);
        else
          component.set_text(str);
      }
      Transform image1 = this.m_HelpMain.get_transform().Find("page/template/contents/viewport/layout/contents_image");
      if (this.SetImageData(image1, "Image", "Helps/help_image_" + (object) num2))
      {
        LayoutElement component = (LayoutElement) ((Component) image1).GetComponent<LayoutElement>();
        num3 += component.get_minHeight();
      }
      bool flag = false;
      Transform image2 = this.m_HelpMain.get_transform().Find("page/template/contents/viewport/layout/contents_image_small");
      if (flag | this.SetImageData(image2, "Image0", "Helps/help_image_" + (object) num2 + "_0") | this.SetImageData(image2, "Image1", "Helps/help_image_" + (object) num2 + "_1"))
      {
        ((Component) image2).get_gameObject().SetActive(true);
        LayoutElement component = (LayoutElement) ((Component) image2).GetComponent<LayoutElement>();
        num3 += component.get_minHeight();
      }
      Transform child2 = this.m_HelpMain.get_transform().FindChild("page/template/contents/viewport/layout/contents_text");
      if (Object.op_Inequality((Object) child2, (Object) null))
      {
        Transform child3 = child2.FindChild("Text");
        LText component1 = (LText) ((Component) child3).GetComponent<LText>();
        string str1 = LocalizedText.Get("help.MAIN_TEXT_" + (object) num2);
        string str2 = "help.MAIN_TEXT_" + (object) num2;
        LayoutElement component2 = (LayoutElement) ((Component) child2).GetComponent<LayoutElement>();
        if (string.IsNullOrEmpty(str1) || str1 == "MAIN_TEXT_" + (object) num2)
        {
          ((Component) child2).get_gameObject().SetActive(false);
        }
        else
        {
          component1.set_text(str2);
          ((Component) child2).get_gameObject().SetActive(true);
          num3 += component2.get_preferredHeight();
          HelpWindow.HELP_ID helpId = (HelpWindow.HELP_ID) num2;
          RectTransform component3 = (RectTransform) ((Component) child3).GetComponent<RectTransform>();
          Vector2 anchoredPosition = component3.get_anchoredPosition();
          switch (helpId)
          {
            case HelpWindow.HELP_ID.ACTION:
              anchoredPosition.y = (__Null) 150.0;
              component3.set_anchoredPosition(anchoredPosition);
              num3 -= component2.get_preferredHeight();
              break;
            case HelpWindow.HELP_ID.REACTION:
            case HelpWindow.HELP_ID.SUPPORT:
              anchoredPosition.y = (__Null) 250.0;
              component3.set_anchoredPosition(anchoredPosition);
              num3 = component2.get_preferredHeight();
              break;
            case HelpWindow.HELP_ID.SHOP:
              anchoredPosition.y = (__Null) 200.0;
              component3.set_anchoredPosition(anchoredPosition);
              num3 -= component2.get_preferredHeight();
              break;
            default:
              anchoredPosition.y = (__Null) 0.0;
              component3.set_anchoredPosition(anchoredPosition);
              break;
          }
        }
      }
      RectTransform child4 = this.m_HelpMain.get_transform().FindChild("page/template/contents/viewport/layout") as RectTransform;
      Vector2 anchoredPosition1 = child4.get_anchoredPosition();
      Vector2 sizeDelta = child4.get_sizeDelta();
      sizeDelta.y = (__Null) (double) num3;
      child4.set_sizeDelta(sizeDelta);
      anchoredPosition1.y = (__Null) 0.0;
      child4.set_anchoredPosition(anchoredPosition1);
      this.m_HelpMain.SetActive(true);
      if (Object.op_Implicit((Object) this.MiddleBackButton))
        ((Component) this.MiddleBackButton).get_gameObject().SetActive(false);
      ((Component) this.BackButton).get_gameObject().SetActive(true);
    }

    private bool SetImageData(Transform image, string childname, string texname)
    {
      if (Object.op_Equality((Object) image, (Object) null))
        return false;
      Transform child = image.FindChild(childname);
      RawImage component = (RawImage) ((Component) child).GetComponent<RawImage>();
      Texture2D texture2D = AssetManager.Load<Texture2D>(texname);
      if (Object.op_Equality((Object) texture2D, (Object) null) || Object.op_Equality((Object) component, (Object) null))
      {
        ((Component) image).get_gameObject().SetActive(false);
        ((Component) child).get_gameObject().SetActive(false);
        return false;
      }
      component.set_texture((Texture) texture2D);
      ((Component) child).get_gameObject().SetActive(true);
      ((Component) image).get_gameObject().SetActive(true);
      return true;
    }

    private float TextHeight(string text, LText ltext)
    {
      float num = (float) ltext.get_fontSize() * 2f;
      for (int index = 0; index < text.Length; ++index)
      {
        if ((int) text[index] == 10)
          num += (float) ltext.get_fontSize() + ltext.get_lineSpacing();
      }
      return num;
    }

    private enum HELP_ID
    {
      ACTION = 61, // 0x0000003D
      REACTION = 62, // 0x0000003E
      SUPPORT = 63, // 0x0000003F
      SHOP = 96, // 0x00000060
    }
  }
}
