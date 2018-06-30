namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_TobiraRecipeParam
    {
        public string recipe_iname;
        public int tobira_lv;
        public int cost;
        public int unit_piece_num;
        public int piece_elem_num;
        public int unlock_elem_num;
        public int unlock_birth_num;
        public JSON_TobiraRecipeMaterialParam[] mats;

        public JSON_TobiraRecipeParam()
        {
            base..ctor();
            return;
        }
    }
}

