// Decompiled with JetBrains decompiler
// Type: SRPG.GachaHistoryItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
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
        Dictionary<string, List<ArtifactData>> dictionary = new Dictionary<string, List<ArtifactData>>();
        for (int index = dataOfClass.historys.Length - 1; index >= 0; --index)
        {
          GachaHistoryData history = dataOfClass.historys[index];
          if (history != null)
          {
            if (history.type == GachaDropData.Type.Unit)
            {
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.UnitIconObj);
              if (Object.op_Inequality((Object) gameObject, (Object) null))
              {
                gameObject.get_transform().SetParent(this.ListParent, false);
                gameObject.SetActive(true);
                DataSource.Bind<UnitData>(gameObject, GachaHistoryWindow.CreateUnitData(history.unit));
                NewBadgeParam data = new NewBadgeParam(true, history.isNew, NewBadgeType.Unit);
                DataSource.Bind<NewBadgeParam>(gameObject, data);
                this.mItems.Add(gameObject);
                gameObject.get_transform().SetAsFirstSibling();
              }
            }
            else if (history.type == GachaDropData.Type.Item)
            {
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemIconObj);
              if (Object.op_Inequality((Object) gameObject, (Object) null))
              {
                gameObject.get_transform().SetParent(this.ListParent, false);
                gameObject.SetActive(true);
                DataSource.Bind<ItemData>(gameObject, GachaHistoryWindow.CreateItemData(history.item, history.num));
                NewBadgeParam data = new NewBadgeParam(true, history.isNew, NewBadgeType.Item);
                DataSource.Bind<NewBadgeParam>(gameObject, data);
                this.mItems.Add(gameObject);
                gameObject.get_transform().SetAsFirstSibling();
              }
            }
            else if (history.type == GachaDropData.Type.Artifact)
            {
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ArtifactIconObj);
              if (Object.op_Inequality((Object) gameObject, (Object) null))
              {
                gameObject.get_transform().SetParent(this.ListParent, false);
                gameObject.SetActive(true);
                DataSource.Bind<ArtifactData>(gameObject, GachaHistoryWindow.CreateArtifactData(history.artifact, history.rarity));
                bool isnew = false;
                if (dictionary.ContainsKey(history.artifact.iname))
                {
                  if (dictionary[history.artifact.iname].Count > 0)
                    dictionary[history.artifact.iname].RemoveAt(0);
                  isnew = dictionary[history.artifact.iname].Count <= 0;
                }
                else
                {
                  List<ArtifactData> artifactsByArtifactId = MonoSingleton<GameManager>.Instance.Player.FindArtifactsByArtifactID(history.artifact.iname);
                  if (artifactsByArtifactId != null && artifactsByArtifactId.Count > 0)
                  {
                    artifactsByArtifactId.RemoveAt(0);
                    dictionary.Add(history.artifact.iname, artifactsByArtifactId);
                    isnew = artifactsByArtifactId.Count <= 0;
                  }
                }
                NewBadgeParam data = new NewBadgeParam(true, isnew, NewBadgeType.Artifact);
                DataSource.Bind<NewBadgeParam>(gameObject, data);
                this.mItems.Add(gameObject);
                gameObject.get_transform().SetAsFirstSibling();
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
