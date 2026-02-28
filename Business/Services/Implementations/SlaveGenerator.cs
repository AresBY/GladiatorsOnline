using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Implementations
{
    public class SlaveGenerator : ISlaveGenerator
    {
        private string[] gladiatorNames = new string[]
    {
        "Marc", "Titus", "Cassius", "Lucius", "Gaius",
        "Maximus", "Aurelius", "Septimus", "Flavius", "Decimus",
        "Quintus", "Valerius", "Publius", "Tiberius", "Servius",
        "Cnaeus", "Appius", "Sextus", "Vibius", "Otho",
        "Drusus", "Crispus", "Fabius", "Domitius", "Marcellus"
    };
        public List<MarketSlave> CreateSlaves(int count, Guid playerId)
        {
            List<MarketSlave> slaves = new List<MarketSlave>();

            Random rnd = new Random();

            for (int i = 0; i < count; i++)
            {
                var slave = new MarketSlave();
                slave.PortraitID = rnd.Next(0, 5);
                slave.Name = gladiatorNames[rnd.Next(0, gladiatorNames.Length)];
                slave.PlayerId = playerId;
                slave.Price = rnd.Next(1, 5);

                int totalPoints = 25;
                for (int j = 0; j < totalPoints; j++)
                {
                    int stat = rnd.Next(0, 4);

                    switch (stat)
                    {
                        case 0: slave.Strength += 4; break;
                        case 1: slave.Dexterity += 4; break;
                        case 2: slave.Intuition += 4; break;
                        case 3: slave.Stamina += 4; break;
                    }
                }
                slaves.Add(slave);
            }
            return slaves;
        }
    }
}
