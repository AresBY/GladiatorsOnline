using Gladiators.Data.Entities;

namespace Gladiators.Business.Factories
{
    public class FighterFactory
    {
        public FighterFactory()
        {
        }

        public Fighter Create(PlayersSlave slave)
        {
            if (slave == null)
                throw new ArgumentNullException(nameof(slave));

            return new Fighter
            {
                Id = slave.Id,
                Name = slave.Name,
                Damage = slave.Strength * 2,
                HP = slave.Stamina * 10,
                HPMax = slave.Stamina * 10,

                Dodge = slave.Dexterity * 5,
                AntiDodge = (int)Math.Round(slave.Dexterity * 5 + slave.Intuition * 2.5),
                Critical = slave.Intuition * 5,
                AntiCritical = slave.Intuition * 5,

                Wins = slave.Wins
            };
        }
    }
}
