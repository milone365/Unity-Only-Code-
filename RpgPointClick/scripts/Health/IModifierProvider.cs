using System.Collections.Generic;

namespace rpg.Stato
{
    public interface IModifierProvider
    {
        IEnumerable<float> GetAdditiveModifiers(Stats stat);
        IEnumerable<float> GetPercentageModifiers(Stats stat);
    }
}