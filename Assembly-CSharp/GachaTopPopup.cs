// Decompiled with JetBrains decompiler
// Type: GachaTopPopup
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GachaTopPopup : MonoBehaviour
{
  private static readonly string HOST_URL = string.Empty;
  private static readonly string GACHA_DETAIL_TITLE = "sys.TITLE_POPUP_GACHA_DETAIL";
  private static readonly string GACHA_DESCRIPTION_TITLE = "sys.TITLE_POPUP_GACHA_DESCRIPTION";
  public GameObject TextTemplate;
  public GameObject ImageTemplate;
  public GameObject Contents;
  public GameObject Title;
  private GachaTopPopup.PopupType popupType;
  private GachaTopParam mCurrentGachaTopParam;
  private string mCurrentGachaIname;

  public GachaTopPopup()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    if (Object.op_Inequality((Object) this.TextTemplate, (Object) null))
      this.TextTemplate.SetActive(false);
    if (Object.op_Inequality((Object) this.ImageTemplate, (Object) null))
      this.ImageTemplate.SetActive(false);
    if (Object.op_Equality((Object) this.Contents, (Object) null) || Object.op_Equality((Object) this.Title, (Object) null))
      return;
    Text component = (Text) ((Component) this.Title.get_transform().FindChild("Text")).GetComponent<Text>();
    this.popupType = (GachaTopPopup.PopupType) int.Parse(FlowNode_Variable.Get(nameof (GachaTopPopup)));
    string key = this.popupType != GachaTopPopup.PopupType.DETAIL ? GachaTopPopup.GACHA_DESCRIPTION_TITLE : GachaTopPopup.GACHA_DETAIL_TITLE;
    if (Object.op_Inequality((Object) component, (Object) null))
      component.set_text(LocalizedText.Get(key));
    if (this.popupType == GachaTopPopup.PopupType.DETAIL)
    {
      this.mCurrentGachaIname = FlowNode_Variable.Get("GachaDetailSelectIname");
      if (string.IsNullOrEmpty(this.mCurrentGachaIname))
        return;
    }
    this.CreateContents();
  }

  public List<GachaDetailParam> GetGachaDetailData()
  {
    List<GachaDetailParam> gachaDetailParamList = new List<GachaDetailParam>();
    string empty = string.Empty;
    foreach (JSON_GachaDetailParam json in JSONParser.parseJSONArray<JSON_GachaDetailParam>(this.popupType != GachaTopPopup.PopupType.DETAIL ? AssetManager.LoadTextData("Gachas/gacha_description") : AssetManager.LoadTextData("Gachas/gacha_detail")))
    {
      GachaDetailParam gachaDetailParam = new GachaDetailParam();
      if (gachaDetailParam.Deserialize(GameUtility.Config_Language, json))
        gachaDetailParamList.Add(gachaDetailParam);
    }
    return gachaDetailParamList;
  }

  private void CreateContents()
  {
    List<GachaDetailParam> gachaDetailData = this.GetGachaDetailData();
    if (gachaDetailData == null)
      return;
    using (List<GachaDetailParam>.Enumerator enumerator = gachaDetailData.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        GachaDetailParam current = enumerator.Current;
        if (this.popupType != GachaTopPopup.PopupType.DETAIL || !(this.mCurrentGachaIname != current.gname))
        {
          if (current.type == 1)
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.TextTemplate);
            gameObject.get_transform().SetParent(this.Contents.get_transform(), false);
            Text component = (Text) ((Component) gameObject.get_transform().FindChild("Text")).GetComponent<Text>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.set_text(current.text);
            gameObject.SetActive(true);
          }
          if (current.type == 2)
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ImageTemplate);
            gameObject.get_transform().SetParent(this.Contents.get_transform(), false);
            RawImage component = (RawImage) ((Component) gameObject.get_transform().FindChild("Image")).GetComponent<RawImage>();
            string url = GachaTopPopup.HOST_URL + "/images/gacha/" + current.image;
            if (Object.op_Inequality((Object) component, (Object) null))
              this.StartCoroutine(this.GetWWWImage(((Component) component).get_gameObject(), url, current.width, current.height, 0));
            gameObject.SetActive(true);
          }
        }
      }
    }
  }

  [DebuggerHidden]
  private IEnumerator GetWWWImage(GameObject image, string url, int continue_count = 0, int height = 0, int width = 0)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new GachaTopPopup.\u003CGetWWWImage\u003Ec__IteratorB0()
    {
      url = url,
      image = image,
      \u003C\u0024\u003Eurl = url,
      \u003C\u0024\u003Eimage = image
    };
  }

  public enum PopupType
  {
    DETAIL,
    DESCRIPTION,
    ALL,
  }
}
