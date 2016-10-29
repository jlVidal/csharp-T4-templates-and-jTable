using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminLogic.Util
{
    public static class TypeExtensions
    {
        public static int ConvertToInt32OrDefault(this object obj)
        {
            if (obj is int)
                return (int) obj;

            if (obj == null)
                return 0;

            if (obj is string)
            {
                int newValue;
                Int32.TryParse((string) obj, out newValue);
                return newValue;
            }

            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
