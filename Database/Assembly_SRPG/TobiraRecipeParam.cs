namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class TobiraRecipeParam
    {
        private string mRecipeIname;
        private int mLevel;
        private int mCost;
        private int mUnitPieceNum;
        private int mElementNum;
        private int mUnlockElementNum;
        private int mUnlockBirthNum;
        private List<TobiraRecipeMaterialParam> mMaterials;

        public TobiraRecipeParam()
        {
            this.mMaterials = new List<TobiraRecipeMaterialParam>();
            base..ctor();
            return;
        }

        public void Deserialize(JSON_TobiraRecipeParam json)
        {
            int num;
            TobiraRecipeMaterialParam param;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mRecipeIname = json.recipe_iname;
            this.mLevel = json.tobira_lv;
            this.mCost = json.cost;
            this.mUnitPieceNum = json.unit_piece_num;
            this.mElementNum = json.piece_elem_num;
            this.mUnlockElementNum = json.unlock_elem_num;
            this.mUnlockBirthNum = json.unlock_birth_num;
            this.mMaterials.Clear();
            if (json.mats == null)
            {
                goto Label_00AA;
            }
            num = 0;
            goto Label_009C;
        Label_0078:
            param = new TobiraRecipeMaterialParam();
            param.Deserialize(json.mats[num]);
            this.mMaterials.Add(param);
            num += 1;
        Label_009C:
            if (num < ((int) json.mats.Length))
            {
                goto Label_0078;
            }
        Label_00AA:
            return;
        }

        public string RecipeIname
        {
            get
            {
                return this.mRecipeIname;
            }
        }

        public int Level
        {
            get
            {
                return this.mLevel;
            }
        }

        public int Cost
        {
            get
            {
                return this.mCost;
            }
        }

        public int UnitPieceNum
        {
            get
            {
                return this.mUnitPieceNum;
            }
        }

        public int ElementNum
        {
            get
            {
                return this.mElementNum;
            }
        }

        public int UnlockElementNum
        {
            get
            {
                return this.mUnlockElementNum;
            }
        }

        public int UnlockBirthNum
        {
            get
            {
                return this.mUnlockBirthNum;
            }
        }

        public TobiraRecipeMaterialParam[] Materials
        {
            get
            {
                return this.mMaterials.ToArray();
            }
        }
    }
}

