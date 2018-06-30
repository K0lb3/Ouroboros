namespace SRPG
{
    using System;

    public class RecipeItemParameter
    {
        public ItemParam Item;
        public SRPG.RecipeItem RecipeItem;
        public int Amount;
        public int RequiredAmount;

        public RecipeItemParameter()
        {
            base..ctor();
            return;
        }
    }
}

