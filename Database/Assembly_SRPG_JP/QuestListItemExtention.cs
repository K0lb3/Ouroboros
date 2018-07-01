// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListItemExtention
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class QuestListItemExtention : MonoBehaviour, IGameParameter
  {
    [SerializeField]
    private LayoutElement m_LayoutElement;
    private Vector2 m_InitialLayoutElementMinSize;
    private Vector2 m_InitialLayoutElementPreferredSize;

    public QuestListItemExtention()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      Vector2 vector2_1 = (Vector2) null;
      Vector2 vector2_2 = (Vector2) null;
      vector2_1.x = (__Null) (double) this.m_LayoutElement.get_minWidth();
      vector2_1.y = (__Null) (double) this.m_LayoutElement.get_minHeight();
      vector2_2.x = (__Null) (double) this.m_LayoutElement.get_preferredWidth();
      vector2_2.y = (__Null) (double) this.m_LayoutElement.get_minHeight();
      this.m_InitialLayoutElementMinSize = vector2_1;
      this.m_InitialLayoutElementPreferredSize = vector2_2;
    }

    public void UpdateValue()
    {
      ((Behaviour) this).set_enabled(true);
    }

    private void Update()
    {
      this.m_LayoutElement.set_minHeight(160f);
      bool flag = false;
      bool activeInHierarchy = ((Component) this).get_gameObject().get_activeInHierarchy();
      for (int index = 0; index < ((Component) this).get_transform().get_childCount(); ++index)
        flag |= ((Component) ((Component) this).get_transform().GetChild(index)).get_gameObject().get_activeInHierarchy();
      ((Component) this).get_gameObject().SetActive(flag);
      ((Behaviour) this).set_enabled(false);
      if (flag)
      {
        RectTransform component = (RectTransform) ((Component) this).GetComponent<RectTransform>();
        if (this.m_InitialLayoutElementMinSize.x != 0.0)
          this.m_LayoutElement.set_minWidth((float) component.get_sizeDelta().x);
        else
          this.m_LayoutElement.set_preferredWidth((float) component.get_sizeDelta().x);
        if (this.m_InitialLayoutElementMinSize.y != 0.0)
          this.m_LayoutElement.set_minHeight((float) component.get_sizeDelta().y);
        else
          this.m_LayoutElement.set_preferredHeight((float) component.get_sizeDelta().y);
      }
      else
      {
        this.m_LayoutElement.set_minWidth((float) this.m_InitialLayoutElementMinSize.x);
        this.m_LayoutElement.set_minHeight((float) this.m_InitialLayoutElementMinSize.y);
        this.m_LayoutElement.set_preferredWidth((float) this.m_InitialLayoutElementPreferredSize.x);
        this.m_LayoutElement.set_preferredHeight((float) this.m_InitialLayoutElementPreferredSize.y);
      }
      Debug.Log((object) (activeInHierarchy.ToString() + " => " + (object) flag + " child (" + (object) ((Component) this).get_transform().get_childCount() + ")"));
    }
  }
}
