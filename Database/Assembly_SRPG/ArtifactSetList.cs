namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Pin(100, "アビリティ詳細が開く", 1, 0)]
    public class ArtifactSetList : MonoBehaviour, IFlowInterface
    {
        private const int OUTPUT_ABILITY_DETAIL_OPEN = 100;
        [SerializeField, HeaderBar("▼セット効果のリストの親")]
        private RectTransform m_SetListRoot;
        [HeaderBar("▼セット効果のアイテムのテンプレート"), SerializeField]
        private GameObject m_SetListItemTemplate;
        private ArtifactParam m_ArtifactParam;
        private static ArtifactParam s_SelectedArtifactParam;

        public ArtifactSetList()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private unsafe void CreateListItem(List<SkillAbilityDeriveData> skillAbilityDeriveData)
        {
            SkillAbilityDeriveData data;
            List<SkillAbilityDeriveData>.Enumerator enumerator;
            GameObject obj2;
            SkillAbilityDeriveListItem item;
            enumerator = skillAbilityDeriveData.GetEnumerator();
        Label_0007:
            try
            {
                goto Label_0053;
            Label_000C:
                data = &enumerator.Current;
                obj2 = Object.Instantiate<GameObject>(this.m_SetListItemTemplate);
                obj2.GetComponentInChildren<SkillAbilityDeriveListItem>().Setup(data);
                obj2.SetActive(1);
                obj2.get_transform().SetParent(this.m_SetListRoot, 0);
                DataSource.Bind<SkillAbilityDeriveParam>(obj2, data.m_SkillAbilityDeriveParam);
            Label_0053:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_000C;
                }
                goto Label_0070;
            }
            finally
            {
            Label_0064:
                ((List<SkillAbilityDeriveData>.Enumerator) enumerator).Dispose();
            }
        Label_0070:
            return;
        }

        public void OnAbilityDetailOpen(GameObject go)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        public static void SetSelectedArtifactParam(ArtifactParam artifactParam)
        {
            s_SelectedArtifactParam = artifactParam;
            return;
        }

        private void Start()
        {
            string str;
            List<SkillAbilityDeriveData> list;
            GameUtility.SetGameObjectActive(this.m_SetListItemTemplate, 0);
            this.m_ArtifactParam = s_SelectedArtifactParam;
            str = this.m_ArtifactParam.iname;
            list = MonoSingleton<GameManager>.Instance.MasterParam.FindAllSkillAbilityDeriveDataWithArtifact(str);
            this.CreateListItem(list);
            return;
        }
    }
}

