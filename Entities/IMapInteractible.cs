using Beskyddare.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Entities
{
    /// <summary>
    /// Entities which can interact with each other. E.g deal and take damage.
    /// </summary>
    public interface IMapInteractible
    {
        //Interactibility
        /// <summary>
        /// The faction this interactible belong to.
        /// </summary>
        Faction Faction { get; }
        string Name { get; }

        //Functions
        void SetupInteractible();

        //Signalling
        void InteractibleRegistered(Map map);
        void InteractibleDeregistered(Map map);
    }
}
