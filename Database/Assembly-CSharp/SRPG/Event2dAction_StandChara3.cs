// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_StandChara3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/配置3(2D)", "立ち絵2を配置します", 5592405, 4473992)]
  public class Event2dAction_StandChara3 : EventAction
  {
    private static readonly string AssetPath = "Event2dAssets/Event2dStand";
    private static readonly Vector2 START_POSITION = new Vector2(-1f, 0.0f);
    private string DummyID = "dummyID";
    public bool NewMaterial = true;
    private const string SHADER_NAME = "UI/Custom/EventStandChara";
    private const string PROPERTYNAME_SCALE_X = "_ScaleX";
    private const string PROPERTYNAME_SCALE_Y = "_ScaleY";
    private const string PROPERTYNAME_OFFSET_X = "_OffsetX";
    private const string PROPERTYNAME_OFFSET_Y = "_OffsetY";
    private const string PROPERTYNAME_FACE_TEX = "_FaceTex";
    public string CharaID;
    public GameObject StandTemplate;
    private GameObject mStandObject;
    private EventStandCharaController2 mEVCharaController;

    public override void PreStart()
    {
      if (this.NewMaterial)
      {
        Shader.DisableKeyword("EVENT_MONOCHROME_ON");
        Shader.DisableKeyword("EVENT_SEPIA_ON");
      }
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mStandObject, (UnityEngine.Object) null))
        return;
      string id = this.DummyID;
      if (!string.IsNullOrEmpty(this.CharaID))
        id = this.CharaID;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) EventStandCharaController2.FindInstances(id), (UnityEngine.Object) null))
      {
        this.mEVCharaController = EventStandCharaController2.FindInstances(id);
        this.mStandObject = ((Component) this.mEVCharaController).get_gameObject();
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mStandObject, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StandTemplate, (UnityEngine.Object) null))
      {
        this.mStandObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.StandTemplate);
        this.mEVCharaController = (EventStandCharaController2) this.mStandObject.GetComponent<EventStandCharaController2>();
        this.mEVCharaController.CharaID = this.CharaID;
        if (this.NewMaterial)
        {
          for (int index = 0; index < this.mEVCharaController.StandCharaList.Length; ++index)
          {
            try
            {
              EventStandChara2 component1 = (EventStandChara2) this.mEVCharaController.StandCharaList[index].GetComponent<EventStandChara2>();
              RectTransform component2 = (RectTransform) component1.BodyObject.GetComponent<RectTransform>();
              RectTransform component3 = (RectTransform) component1.FaceObject.GetComponent<RectTransform>();
              Vector2 vector2_1;
              // ISSUE: explicit reference operation
              ((Vector2) @vector2_1).\u002Ector((float) (component2.get_sizeDelta().x * ((Transform) component2).get_localScale().x), (float) (component2.get_sizeDelta().y * ((Transform) component2).get_localScale().y));
              Vector2 vector2_2;
              // ISSUE: explicit reference operation
              ((Vector2) @vector2_2).\u002Ector((float) (component3.get_sizeDelta().x * ((Transform) component3).get_localScale().x), (float) (component3.get_sizeDelta().y * ((Transform) component3).get_localScale().y));
              Vector2 vector2_3;
              Vector2 vector2_4;
              if (Mathf.Approximately((float) vector2_2.x, 0.0f) || Mathf.Approximately((float) vector2_2.y, 0.0f))
              {
                // ISSUE: explicit reference operation
                ((Vector2) @vector2_3).\u002Ector(0.0f, 0.0f);
                // ISSUE: explicit reference operation
                ((Vector2) @vector2_4).\u002Ector(2f, 2f);
              }
              else
              {
                // ISSUE: explicit reference operation
                ((Vector2) @vector2_3).\u002Ector((float) (vector2_1.x / vector2_2.x), (float) (vector2_1.y / vector2_2.y));
                Vector2 vector2_5;
                // ISSUE: explicit reference operation
                ((Vector2) @vector2_5).\u002Ector((float) (((Transform) component3).get_localPosition().x - component3.get_pivot().x * vector2_2.x), (float) (((Transform) component3).get_localPosition().y - component3.get_pivot().y * vector2_2.y));
                Vector2 vector2_6;
                // ISSUE: explicit reference operation
                ((Vector2) @vector2_6).\u002Ector((float) (((Transform) component2).get_localPosition().x - component2.get_pivot().x * vector2_1.x), (float) (((Transform) component2).get_localPosition().y - component2.get_pivot().y * vector2_1.y));
                Vector2 vector2_7 = Vector2.op_Subtraction(vector2_5, vector2_6);
                // ISSUE: explicit reference operation
                ((Vector2) @vector2_4).\u002Ector((float) (-1.0 * vector2_7.x / vector2_2.x), (float) (-1.0 * vector2_7.y / vector2_2.y));
              }
              Material material = new Material(Shader.Find("UI/Custom/EventStandChara"));
              Texture mainTexture = ((RawImage) component1.FaceObject.GetComponent<RawImage>()).get_mainTexture();
              this.SetMaterialProperty(material, "_FaceTex", mainTexture);
              this.SetMaterialProperty(material, "_ScaleX", (float) vector2_3.x);
              this.SetMaterialProperty(material, "_ScaleY", (float) vector2_3.y);
              this.SetMaterialProperty(material, "_OffsetX", (float) vector2_4.x);
              this.SetMaterialProperty(material, "_OffsetY", (float) vector2_4.y);
              ((Graphic) component1.BodyObject.GetComponent<RawImage>()).set_material(material);
              GameUtility.SetGameObjectActive(component1.FaceObject, false);
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mStandObject, (UnityEngine.Object) null))
        return;
      this.mStandObject.get_transform().SetParent(((Component) this.ActiveCanvas).get_transform(), false);
      this.mStandObject.get_transform().SetAsLastSibling();
      this.mStandObject.get_gameObject().SetActive(false);
      RectTransform component = (RectTransform) this.mStandObject.GetComponent<RectTransform>();
      RectTransform rectTransform = component;
      Vector2 startPosition = Event2dAction_StandChara3.START_POSITION;
      component.set_anchorMax(startPosition);
      Vector2 vector2 = startPosition;
      rectTransform.set_anchorMin(vector2);
    }

    private bool SetMaterialProperty(Material material, string name, float val)
    {
      if (!material.HasProperty(name))
        return false;
      material.SetFloat(name, val);
      return true;
    }

    private bool SetMaterialProperty(Material material, string name, Texture val)
    {
      if (!material.HasProperty(name))
        return false;
      material.SetTexture(name, val);
      return true;
    }

    public override void OnActivate()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mStandObject, (UnityEngine.Object) null) && !this.mStandObject.get_gameObject().get_activeInHierarchy())
        this.mStandObject.get_gameObject().SetActive(true);
      this.mEVCharaController.Open(0.0f);
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mStandObject, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mStandObject.get_gameObject());
    }
  }
}
