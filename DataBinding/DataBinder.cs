using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beskyddare.DataBinding;
using Beskyddare.Utility;

namespace Beskyddare
{
    public class DataBinder : Node
    {
        public override void _Ready()
        {
            //Do all possible bindings.
            Node parent = GetParent();
            if (GetChildCount() <= 0)
            {
                Log.Error($"Got no child node to get binding parent name from for parent node='{parent.Name}'!");
                return;
            }

            Node childNameNode = GetChild(0);
            string parentNameNode = childNameNode.Name;

            IEnumerable<NodeDataBinding> validBindings = DataBindingDatabase.Bindings.Where(binding => binding.parentNode == parentNameNode);
            if(validBindings != null)
            {
                foreach(NodeDataBinding binding in validBindings)
                {
                    object foundNodeObject = parent.GetChildren().FirstOrDefault(child => child is Node childNode && childNode.Name == binding.targetNode);
                    if(foundNodeObject is Node foundChild)
                    {
                        binding.Apply(foundChild);
                    }
                    else
                    {
                        Log.Error($"Failed to apply binding='{binding.parentNode}' to target node='{binding.targetNode}'!");
                    }
                }
            }
            else
            {
                Log.Warning($"Found no valid bindings for '{parentNameNode}'.");
            }
        }
    }
}
