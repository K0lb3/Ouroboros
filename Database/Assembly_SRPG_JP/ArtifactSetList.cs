// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSetList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "アビリティ詳細が開く", FlowNode.PinTypes.Output, 0)]
  public class ArtifactSetList : MonoBehaviour, IFlowInterface
  {
    private const int OUTPUT_ABILITY_DETAIL_OPEN = 100;
    [SerializeField]
    [HeaderBar("▼セット効果のリストの親")]
    private RectTransform m_SetListRoot;
    [HeaderBar("▼セット効果のアイテムのテンプレート")]
    [SerializeField]
    private GameObject m_SetListItemTemplate;
    private ArtifactParam m_ArtifactParam;
    private static ArtifactParam s_SelectedArtifactParam;

    public ArtifactSetList()
    {
      base.\u002Ector();
    }

    public static void SetSelectedArtifactParam(ArtifactParam artifactParam)
    {
      ArtifactSetList.s_SelectedArtifactParam = artifactParam;
    }

    private void Start()
    {
      GameUtility.SetGameObjectActive(this.m_SetListItemTemplate, false);
      this.m_ArtifactParam = ArtifactSetList.s_SelectedArtifactParam;
      this.CreateListItem(MonoSingleton<GameManager>.Instance.MasterParam.FindAllSkillAbilityDeriveDataWithArtifact(this.m_ArtifactParam.iname));
    }

    private void CreateListItem(List<SkillAbilityDeriveData> skillAbilityDeriveData)
    {
      using (List<SkillAbilityDeriveData>.Enumerator enumerator = skillAbilityDeriveData.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SkillAbilityDeriveData current = enumerator.Current;
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.m_SetListItemTemplate);
          ((SkillAbilityDeriveListItem) gameObject.GetComponentInChildren<SkillAbilityDeriveListItem>()).Setup(current);
          gameObject.SetActive(true);
          gameObject.get_transform().SetParent((Transform) this.m_SetListRoot, false);
          DataSource.Bind<SkillAbilityDeriveParam>(gameObject, current.m_SkillAbilityDeriveParam);
        }
      }
    }

    public void Activated(int pinID)
    {
    }

    public void OnAbilityDetailOpen(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
