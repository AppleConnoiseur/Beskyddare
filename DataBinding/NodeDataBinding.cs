using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Beskyddare.Utility;

namespace Beskyddare.DataBinding
{
    /// <summary>
    /// Represents a binding of data to a class.
    /// </summary>
    public class NodeDataBinding
    {
        /// <summary>
        /// What parent node this binding applies to.
        /// </summary>
        public string parentNode = "";

        /// <summary>
        /// The target node in the parent.
        /// </summary>
        public string targetNode = "";

        /// <summary>
        /// Values of the object to set. Target 'member field' => 'value'
        /// </summary>
        public Dictionary<string, object> values = new Dictionary<string, object>();

        /// <summary>
        /// Applies this binding to the target.
        /// </summary>
        /// <param name="target">Target object.</param>
        public void Apply(object target)
        {
            Type targetType = target.GetType();
            //Log.Message($"Applying data binding to '{target.ToString()}'");
            foreach (KeyValuePair<string, object> pair in values)
            {
                try
                {
                    FieldInfo fieldInfo = targetType.GetField(pair.Key);
                    if(fieldInfo != null)
                    {
                        fieldInfo.SetValue(target, pair.Value);
                        //Log.Message($"Applying value='{pair.Value}' to field='{pair.Key}'");
                    }
                }catch(Exception exception)
                {
                    Log.Error($"Exception occured while tryong to apply a binding in class '{targetType.FullName}' with field '{pair.Key}', with the value of={pair.Value}; Exception='{exception.ToString()}'");
                }
            }
        }
    }
}
