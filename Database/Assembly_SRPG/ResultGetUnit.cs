namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ResultGetUnit : MonoBehaviour
    {
        public GameObject GoGetUnitAnim;
        public GameObject GoGetUnitDetail;
        public RawImage ImgUnit;

        public ResultGetUnit()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            GameManager manager;
            SceneBattle battle;
            string str;
            ItemParam param;
            UnitParam param2;
            Animator animator;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_002F;
            }
            if (battle.IsGetFirstClearItem != null)
            {
                goto Label_0030;
            }
        Label_002F:
            return;
        Label_0030:
            if (this.GoGetUnitAnim == null)
            {
                goto Label_00EF;
            }
            str = battle.FirstClearItemId;
            param = manager.GetItemParam(str);
            if (param == null)
            {
                goto Label_00EF;
            }
            if (param.type != 0x10)
            {
                goto Label_00EF;
            }
            param2 = manager.GetUnitParam(str);
            if (param2 == null)
            {
                goto Label_00EF;
            }
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
            DataSource.Bind<UnitParam>(base.get_gameObject(), param2);
            if (this.ImgUnit == null)
            {
                goto Label_00B6;
            }
            manager.ApplyTextureAsync(this.ImgUnit, AssetPath.UnitImage(param2, param2.GetJobId(0)));
        Label_00B6:
            GameParameter.UpdateAll(base.get_gameObject());
            animator = this.GoGetUnitAnim.GetComponent<Animator>();
            if (animator == null)
            {
                goto Label_00EF;
            }
            animator.SetInteger("rariry", param2.rare + 1);
        Label_00EF:
            return;
        }
    }
}

