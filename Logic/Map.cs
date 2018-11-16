using Beskyddare.Entities;
using Beskyddare.Utility;
using Beskyddare.XML;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beskyddare.Logic
{
    /// <summary>
    /// A map in the Game.
    /// </summary>
    public class Map : IExposeData
    {
        public static int currentMapId = 0;

        public string name;

        private Node rootNode;

        public List<IMapInteractible> interactibles = new List<IMapInteractible>();

        public Player player;

        public Node Root
        {
            get
            {
                return rootNode;
            }
        }

        public Map(Node root)
        {
            rootNode = root;

            name = "Map" + currentMapId;
            currentMapId++;
        }

        public void RegisterInteractible(IMapInteractible interactible)
        {
            if(!interactibles.Contains(interactible))
            {
                interactibles.Add(interactible);
                interactible.InteractibleRegistered(this);
                Log.Message($"Added interactible '{interactible.Name}' to map '{name}'");
            }
                
        }

        public void DeregisterInteractible(IMapInteractible interactible)
        {
            if (interactibles.Contains(interactible))
            {
                interactibles.Remove(interactible);
                interactible.InteractibleDeregistered(this);
            }
        }

        public void ExposeData(XMLScribe scribe)
        {
            
        }
    }
}
