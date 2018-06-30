namespace SRPG
{
    using System;

    [Serializable]
    public class JSON_RecipeParam
    {
        public string iname;
        public int cost;
        public string mat1;
        public string mat2;
        public string mat3;
        public string mat4;
        public string mat5;
        public int num1;
        public int num2;
        public int num3;
        public int num4;
        public int num5;

        public JSON_RecipeParam()
        {
            base..ctor();
            return;
        }
    }
}

