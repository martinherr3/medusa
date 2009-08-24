using System.Reflection;

namespace Suricato.Reflection
{
    public class Reflector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object PropertySet(object obj, string propertyName, object value)
        {
            PropertyInfo pi;

            if (obj.GetType().BaseType.GetProperty(propertyName) != null)
            {
                pi = obj.GetType().BaseType.GetProperty(propertyName);
            }
            else
            {
                pi = obj.GetType().GetProperty(propertyName);
            }

            if (pi != null)
            {
                pi.SetValue(obj, value, null);
            }
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object PropertyGet(object obj, string propertyName)
        {
            PropertyInfo pi;

            if (obj.GetType().BaseType.GetProperty(propertyName) != null)
            {
                pi = obj.GetType().BaseType.GetProperty(propertyName);
            }
            else
            {
                pi = obj.GetType().GetProperty(propertyName);
            }

            if (pi != null)
            {
                return pi.GetValue(obj, null);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static object MethodGet(object obj, string methodName)
        {
            MethodInfo mi;

            if (obj.GetType().BaseType.GetMethod(methodName) != null)
            {
                mi = obj.GetType().BaseType.GetMethod(methodName);
            }
            else
            {
                mi = obj.GetType().GetMethod(methodName);
            }

            if (mi != null)
            {
                return mi.Invoke(obj, null);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static object Get(object obj,string methodName)
        {
            MethodInfo mi = obj.GetType().GetMethod(methodName);

            if (mi != null)
                return MethodGet(obj, methodName);
            else
                return PropertyGet(obj, methodName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object FieldGet(object obj,string fieldName)
        {
            FieldInfo fi = obj.GetType().GetField(fieldName, (BindingFlags.GetField | BindingFlags.NonPublic) |
                                                             (BindingFlags.GetField | BindingFlags.Static) |
                                                             (BindingFlags.GetField | BindingFlags.Public) |
                                                             (BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance));

            return fi.GetValue(obj);
        }
    }
}