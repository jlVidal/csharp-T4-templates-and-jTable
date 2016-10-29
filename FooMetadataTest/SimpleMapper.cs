using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FooMetadataTest
{
    public static class SimpleDbBasedMapper<T>
        where T : class
    {
        private static readonly Dictionary<string, PropertyInfo> _propertyMap;

        static SimpleDbBasedMapper()
        {
            // At this point we can convert each
            // property name to lower case so we avoid 
            // creating a new string more than once.
            _propertyMap =
                typeof(T)
                .GetProperties()
                .ToDictionary(
                    p => p.Name.ToLower(),
                    p => p
                );
        }

        public static T Map(ExpandoObject source, T destination)
        {
            // Might as well take care of null references early.
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");

            MapProcess(source, destination);

            return destination;
        }

        public static T Map(IEnumerable<KeyValuePair<string,object>> source, T destination)
        {
            // Might as well take care of null references early.
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");

            MapProcess(source, destination);

            return destination;
        }

        private static void MapProcess(IEnumerable<KeyValuePair<string, object>> source, T destination)
        {
            // By iterating the KeyValuePair<string, object> of
            // source we can avoid manually searching the keys of
            // source as we see in your original code.
            foreach (var kv in source)
            {
                PropertyInfo p;
                if (_propertyMap.TryGetValue(kv.Key.ToLower(), out p))
                {
                    var propType = p.PropertyType;
                    if (kv.Value == null)
                    {
                        if (!propType.IsByRef && propType.Name != "Nullable`1")
                        {
                            // Throw if type is a value type 
                            // but not Nullable<>
                            throw new ArgumentException("not nullable");
                        }
                    }
                    else if (Convert.IsDBNull(kv.Value))
                    {
                        p.SetValue(destination, null, null);
                        continue;
                    }
                    else if (propType.IsInstanceOfType(kv.Value))
                    {
                        p.SetValue(destination, kv.Value, null);
                        continue;
                    }
                    if (kv.Value.GetType() != propType)
                    {
                        p.SetValue(destination, Convert.ChangeType(kv.Value, propType), null);
                        continue;
                    }
                    p.SetValue(destination, kv.Value, null);
                }
            }
        }
    }
}
