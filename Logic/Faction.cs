using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Logic
{
    public class Faction
    {
        public string name = "defaultFactionName";

        public HashSet<Faction> EnemiesWith = new HashSet<Faction>();
        public HashSet<Faction> AlliesWith = new HashSet<Faction>();

        public bool AreEnemiesWith(Faction otherFaction)
        {
            return EnemiesWith.Contains(otherFaction);
        }

        public bool AreAlliesWith(Faction otherFaction)
        {
            return AlliesWith.Contains(otherFaction);
        }

        public bool CanBeAttacked(Faction otherFaction)
        {
            return AreEnemiesWith(otherFaction) || !AreAlliesWith(otherFaction);
        }

        public void AddEnemy(Faction otherFaction)
        {
            AddEnemyInt(this, otherFaction);
            AddEnemyInt(otherFaction, this);
        }

        private void AddEnemyInt(Faction firstFaction, Faction secondFaction)
        {
            firstFaction.EnemiesWith.Add(secondFaction);
            if (firstFaction.AlliesWith.Contains(secondFaction))
            {
                firstFaction.AlliesWith.Remove(secondFaction);
            }
        }

        public void AddAlly(Faction otherFaction)
        {
            AddAllyInt(this, otherFaction);
            AddAllyInt(otherFaction, this);
        }

        private void AddAllyInt(Faction firstFaction, Faction secondFaction)
        {
            firstFaction.AlliesWith.Add(secondFaction);
            if (firstFaction.EnemiesWith.Contains(secondFaction))
            {
                firstFaction.EnemiesWith.Remove(secondFaction);
            }
        }
    }
}
