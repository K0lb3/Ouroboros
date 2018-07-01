// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GachaHistoryItem : MonoBehaviour
  {
    [SerializeField]
    private GameObject UnitIconObj;
    [SerializeField]
    private GameObject ItemIconObj;
    [SerializeField]
    private GameObject ArtifactIconObj;
    [SerializeField]
    private GameObject ConceptCardIconObj;
    [SerializeField]
    private GameObject TitleText;
    [SerializeField]
    private Transform ListParent;
    private List<GameObject> mItems;

    public GachaHistoryItem()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.UnitIconObj, (Object) null))
        this.UnitIconObj.SetActive(false);
      if (Object.op_Inequality((Object) this.ItemIconObj, (Object) null))
        this.ItemIconObj.SetActive(false);
      if (Object.op_Inequality((Object) this.ArtifactIconObj, (Object) null))
        this.ArtifactIconObj.SetActive(false);
      if (Object.op_Inequality((Object) this.ConceptCardIconObj, (Object) null))
        this.ConceptCardIconObj.SetActive(false);
      if (Object.op_Equality((Object) this.ListParent, (Object) null))
        DebugUtility.LogError("ListParentが設定されていません");
      else
        this.Initalize();
    }

    private void Update()
    {
    }

    private void Initalize()
    {
      GachaHistoryItemData dataOfClass = DataSource.FindDataOfClass<GachaHistoryItemData>(((Component) this).get_gameObject(), (GachaHistoryItemData) null);
      if (dataOfClass == null)
      {
        DebugUtility.LogError("履歴が存在しません");
      }
      else
      {
        for (int index = dataOfClass.historys.Length - 1; index >= 0; --index)
        {
          GachaHistoryData history = dataOfClass.historys[index];
          if (history != null)
          {
            GameObject gameObject1 = (GameObject) null;
            bool flag = false;
            if (history.type == GachaDropData.Type.Unit)
            {
              gameObject1 = (GameObject) Object.Instantiate<GameObject>((M0) this.UnitIconObj);
              if (Object.op_Inequality((Object) gameObject1, (Object) null))
              {
                gameObject1.get_transform().SetParent(this.ListParent, false);
                gameObject1.SetActive(true);
                DataSource.Bind<UnitData>(gameObject1, GachaHistoryWindow.CreateUnitData(history.unit));
                flag = history.isNew;
                this.mItems.Add(gameObject1);
                gameObject1.get_transform().SetAsFirstSibling();
              }
            }
            else if (history.type == GachaDropData.Type.Item)
            {
              gameObject1 = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemIconObj);
              if (Object.op_Inequality((Object) gameObject1, (Object) null))
              {
                gameObject1.get_transform().SetParent(this.ListParent, false);
                gameObject1.SetActive(true);
                DataSource.Bind<ItemData>(gameObject1, GachaHistoryWindow.CreateItemData(history.item, history.num));
                flag = history.isNew;
                this.mItems.Add(gameObject1);
                gameObject1.get_transform().SetAsFirstSibling();
              }
            }
            else if (history.type == GachaDropData.Type.Artifact)
            {
              gameObject1 = (GameObject) Object.Instantiate<GameObject>((M0) this.ArtifactIconObj);
              if (Object.op_Inequality((Object) gameObject1, (Object) null))
              {
                gameObject1.get_transform().SetParent(this.ListParent, false);
                gameObject1.SetActive(true);
                DataSource.Bind<ArtifactData>(gameObject1, GachaHistoryWindow.CreateArtifactData(history.artifact, history.rarity));
                flag = history.isNew;
                this.mItems.Add(gameObject1);
                gameObject1.get_transform().SetAsFirstSibling();
              }
            }
            else if (history.type == GachaDropData.Type.ConceptCard)
            {
              gameObject1 = (GameObject) Object.Instantiate<GameObject>((M0) this.ConceptCardIconObj);
              if (Object.op_Inequality((Object) gameObject1, (Object) null))
              {
                gameObject1.get_transform().SetParent(this.ListParent, false);
                gameObject1.SetActive(true);
                ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(history.conceptcard.iname);
                ConceptCardIcon component = (ConceptCardIcon) gameObject1.GetComponent<ConceptCardIcon>();
                if (Object.op_Inequality((Object) component, (Object) null))
                  component.Setup(cardDataForDisplay);
                flag = history.isNew;
                this.mItems.Add(gameObject1);
                gameObject1.get_transform().SetAsFirstSibling();
              }
            }
            if (Object.op_Inequality((Object) gameObject1, (Object) null))
            {
              SerializeValueBehaviour component = (SerializeValueBehaviour) gameObject1.GetComponent<SerializeValueBehaviour>();
              if (Object.op_Inequality((Object) component, (Object) null))
              {
                GameObject gameObject2 = component.list.GetGameObject("new");
                if (Object.op_Inequality((Object) gameObject2, (Object) null))
                  gameObject2.SetActive(flag);
              }
            }
          }
        }
        if (Object.op_Inequality((Object) this.TitleText, (Object) null))
        {
          Text component = (Text) this.TitleText.GetComponent<Text>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            string str = LocalizedText.Get("sys.TEXT_GACHA_HISTORY_FOOTER", (object) dataOfClass.GetDropAt().ToString("yyyy/MM/dd HH:mm:ss"), (object) dataOfClass.gachaTitle);
            component.set_text(str);
          }
        }
        GameParameter.UpdateAll(((Component) this).get_gameObject());
      }
    }
  }
}
