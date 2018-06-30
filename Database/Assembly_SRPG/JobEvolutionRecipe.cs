namespace SRPG
{
    using System;

    public class JobEvolutionRecipe
    {
        public ItemParam Item;
        public SRPG.RecipeItem RecipeItem;
        public int Amount;
        public int RequiredAmount;

        public JobEvolutionRecipe()
        {
            base..ctor();
            return;
        }
    }
}

