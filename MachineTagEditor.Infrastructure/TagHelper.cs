using MCM.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MachineTagEditor.Infrastructure
{
    public class TagHelper
    {
        public static TagEnum getEnumfromTag(DataTag tag)
        {

            TagEnum te = null;
            Type t = typeof(DataTag);
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var propInfo in propInfos)
            {
                if (propInfo.PropertyType == typeof(TagEnum))
                {
                    te = (TagEnum)propInfo.GetValue(tag);
                }
            }


            return te;
        }

        public static List<EnumerationContainer> getContainersfromEnum(TagEnum te)
        {
            var EnumerationValues = new List<EnumerationContainer>(); 
            if (te != null)
            {
                foreach (var entry in te.Table)
                {
                    EnumerationValues.Add(new EnumerationContainer(entry.Key.ToString(), entry.Value.ElementAt(0).ToString().Replace('[', ' ').Replace(']', ' ').Replace(',', ' ')));
                }
            }

            return EnumerationValues;
        }

    }
}
