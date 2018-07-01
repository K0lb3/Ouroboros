// Decompiled with JetBrains decompiler
// Type: SRPG.TowerFeatureDescriptionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "次のページボタン", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "前のページボタン", FlowNode.PinTypes.Input, 2)]
  public class TowerFeatureDescriptionDetail : MonoBehaviour, IFlowInterface
  {
    private const int PIN_NEXT_BUTTON = 1;
    private const int PIN_PREV_BUTTON = 2;
    [SerializeField]
    private ImageArray ImageData;
    [SerializeField]
    private Button PrevButton;
    [SerializeField]
    private Button NextButton;
    [SerializeField]
    private GameObject ParentPageIcon;
    [SerializeField]
    private GameObject TemplatePageIcon;
    private List<Toggle> mToggleIconList;

    public TowerFeatureDescriptionDetail()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.ImageData, (Object) null) && this.ImageData.Images.Length == 0)
      {
        Debug.LogError((object) "ImageData not data.");
      }
      else
      {
        this.ImageData.ImageIndex = 0;
        if (this.ImageData.Images.Length == 1)
        {
          ((Component) this.NextButton).get_gameObject().SetActive(false);
          ((Component) this.PrevButton).get_gameObject().SetActive(false);
        }
        else
          ((Selectable) this.PrevButton).set_interactable(false);
        this.TemplatePageIcon.SetActive(false);
        for (int index = 0; index < this.ImageData.Images.Length; ++index)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.TemplatePageIcon);
          Vector2 vector2 = Vector2.op_Implicit(gameObject.get_transform().get_localScale());
          gameObject.get_transform().SetParent(this.ParentPageIcon.get_transform());
          gameObject.get_transform().set_localScale(Vector2.op_Implicit(vector2));
          gameObject.get_gameObject().SetActive(true);
          ((Object) gameObject).set_name(((Object) this.TemplatePageIcon).get_name() + (index + 1).ToString());
          this.mToggleIconList.Add((Toggle) gameObject.GetComponent<Toggle>());
        }
        this.mToggleIconList[0].set_isOn(true);
      }
    }

    private void Update()
    {
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (this.ImageData.ImageIndex >= this.ImageData.Images.Length - 1)
            break;
          ++this.ImageData.ImageIndex;
          this.SetButtonInteractable();
          this.SetTobbleIcon();
          break;
        case 2:
          if (this.ImageData.ImageIndex <= 0)
            break;
          --this.ImageData.ImageIndex;
          this.SetButtonInteractable();
          this.SetTobbleIcon();
          break;
      }
    }

    private void SetButtonInteractable()
    {
      if (this.ImageData.Images.Length == 1)
        return;
      if (this.ImageData.ImageIndex == this.ImageData.Images.Length - 1)
      {
        ((Selectable) this.NextButton).set_interactable(false);
        ((Selectable) this.PrevButton).set_interactable(true);
      }
      else
      {
        if (this.ImageData.ImageIndex != 0)
          return;
        ((Selectable) this.NextButton).set_interactable(true);
        ((Selectable) this.PrevButton).set_interactable(false);
      }
    }

    private void SetTobbleIcon()
    {
      for (int index = 0; index < this.mToggleIconList.Count; ++index)
      {
        if (index == this.ImageData.ImageIndex)
          this.mToggleIconList[index].set_isOn(true);
        else
          this.mToggleIconList[index].set_isOn(false);
      }
    }
  }
}
