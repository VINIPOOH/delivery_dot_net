using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace TestSalamandrBag.Util
{
    public static class ReflectionUtil
    {
        public static Object GetFieldValue(Object obj, String fieldName)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            return fieldInfo.GetValue(obj);
        }
        
        public static void SetFieldValue(Object obj, String fieldName, Object value)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            fieldInfo.SetValue(obj,value);
        } 
    }
}