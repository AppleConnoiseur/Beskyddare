using Beskyddare.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Entities
{
    public class Enemy_Bipedal : Bipedal
    {
        public override void SetupInteractible()
        {
            base.SetupInteractible();

            faction = Game.Instance.enemyFaction;
        }
    }
}
