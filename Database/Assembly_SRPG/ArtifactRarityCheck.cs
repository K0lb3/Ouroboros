namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(2, "キャンセルを選択", 0, 2), Pin(100, "ウィンドウを閉じる", 1, 100), Pin(1, "売却を選択", 0, 1), Pin(0, "分解を選択", 0, 0)]
    public class ArtifactRarityCheck : MonoBehaviour, IFlowInterface
    {
        private const int INPUT_DECOMPOSE = 0;
        private const int INPUT_SELL = 1;
        private const int INPUT_CANCEL = 2;
        private const int OUTPUT_CLOSE_ACTION = 100;
        [SerializeField]
        private GameObject mArtifactTemplate;
        [SerializeField]
        private RectTransform mArtifactObjectParent;
        [SerializeField]
        private LText mLText;
        [SerializeField]
        private GameObject mButtonDecompose;
        [SerializeField]
        private GameObject mButtonSell;
        private Type mType;
        public OnArtifactRarityCheckDecideEvent OnDecideEvent;
        public OnArtifactRarityCheckCancelEvent OnCancelEvent;
        private GameObject mArgGo;
        private List<ArtifactData> mArtifactDataList;
        private int mRarity;

        public ArtifactRarityCheck()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_002C;

                case 2:
                    goto Label_003F;
            }
            goto Label_0052;
        Label_0019:
            this.OnDecide();
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_0057;
        Label_002C:
            this.OnDecide();
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_0057;
        Label_003F:
            this.OnCancel();
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_0057;
        Label_0052:;
        Label_0057:
            return;
        }

        private void OnCancel()
        {
            if (this.OnCancelEvent == null)
            {
                goto Label_001C;
            }
            this.OnCancelEvent(this.mArgGo);
        Label_001C:
            return;
        }

        private void OnDecide()
        {
            if (this.OnDecideEvent == null)
            {
                goto Label_001C;
            }
            this.OnDecideEvent(this.mArgGo);
        Label_001C:
            return;
        }

        public void Setup(Type type, GameObject arg_go, ArtifactData[] artifact_data, int rarity)
        {
            this.mType = type;
            this.mArgGo = arg_go;
            this.mArtifactDataList = new List<ArtifactData>(artifact_data);
            this.mRarity = rarity;
            return;
        }

        private unsafe void Start()
        {
            ArtifactData data;
            List<ArtifactData>.Enumerator enumerator;
            GameObject obj2;
            Transform transform;
            if ((this.mArgGo == null) != null)
            {
                goto Label_0038;
            }
            if (this.mArtifactDataList == null)
            {
                goto Label_0038;
            }
            if (this.mType == null)
            {
                goto Label_0038;
            }
            if ((this.mLText == null) == null)
            {
                goto Label_0039;
            }
        Label_0038:
            return;
        Label_0039:
            if ((this.mButtonDecompose != null) == null)
            {
                goto Label_0067;
            }
            if (this.mType != 2)
            {
                goto Label_0067;
            }
            this.mButtonSell.SetActive(0);
            goto Label_0090;
        Label_0067:
            if ((this.mButtonSell != null) == null)
            {
                goto Label_0090;
            }
            if (this.mType != 1)
            {
                goto Label_0090;
            }
            this.mButtonDecompose.SetActive(0);
        Label_0090:
            this.mLText.set_text(string.Format(LocalizedText.Get(this.mLText.get_text()), &this.mRarity.ToString()));
            this.mArtifactTemplate.SetActive(0);
            enumerator = this.mArtifactDataList.GetEnumerator();
        Label_00D3:
            try
            {
                goto Label_0126;
            Label_00D8:
                data = &enumerator.Current;
                if ((data.Rarity + 1) < this.mRarity)
                {
                    goto Label_0126;
                }
                obj2 = Object.Instantiate<GameObject>(this.mArtifactTemplate);
                obj2.get_transform().SetParent(this.mArtifactObjectParent, 0);
                DataSource.Bind<ArtifactData>(obj2, data);
                obj2.SetActive(1);
            Label_0126:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00D8;
                }
                goto Label_0143;
            }
            finally
            {
            Label_0137:
                ((List<ArtifactData>.Enumerator) enumerator).Dispose();
            }
        Label_0143:
            return;
        }

        public delegate void OnArtifactRarityCheckCancelEvent(GameObject go);

        public delegate void OnArtifactRarityCheckDecideEvent(GameObject go);

        public enum Type
        {
            NONE,
            SELL,
            DECOMPOSE
        }
    }
}

