using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace CsharpExam1
{
    /// <summary>
    /// This can deserialize JSON string. (cannot serialize)
    /// It support following field naming policies.
    ///  - under_score
    ///  - camelCase
    ///  - PascalCase
    /// </summary>
    /// 
    class JsonDeserialize
    {
        public enum FieldNamingPolicyEnum { UNDERSCORE, CAMELCASE, PASCALCASE }

        public JsonDeserialize()
        {
            FieldNamingPolicy = FieldNamingPolicyEnum.UNDERSCORE;
        }

        public JsonDeserialize(FieldNamingPolicyEnum policy)
        {
            FieldNamingPolicy = policy;
        }

        public FieldNamingPolicyEnum FieldNamingPolicy { set; get;  }

        public T Deserialize<T>(String jsonString)
        {
            Dictionary<String, Object> dicJson = (Dictionary<String, Object>)Json.Deserialize(jsonString);
            T newObject = NewInstance<T>();
            SetFields(newObject, dicJson);
            return newObject;
        }

        private void SetFields(Object body, Dictionary<String, Object> param)
        {
            Type srcType = body.GetType();
            object[] emptyParam = new object[] { };
            String setMethodName;
            foreach (KeyValuePair<string, object> kvp in param)
            {
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                setMethodName = GetSetMethodName(kvp.Key);
                MethodInfo miSetMethod = srcType.GetMethod(setMethodName);

                if (miSetMethod != null && kvp.Value != null)
                {
                    //Console.WriteLine("Value.GetType() : " + kvp.Value.GetType());

                    if (typeof(Dictionary<String, Object>).Equals(kvp.Value.GetType()))
                    {
                        // new instance some object
                        Type ObjType = GetObjectTypeUsingName(srcType, kvp.Key);
                        object subObject = NewInstance(ObjType);

                        // set object to body
                        miSetMethod.Invoke(body, new object[] { subObject });
                        SetFields(subObject, kvp.Value as Dictionary<String, Object>);
                    }
                    else if (typeof(List<Object>).Equals(kvp.Value.GetType()))
                    {
                        // new Instance of list
                        Type ObjType = GetObjectTypeUsingName(srcType, kvp.Key);
                        object listObject = NewInstance(ObjType);

                        // set listObject to body
                        miSetMethod.Invoke(body, new object[] { listObject });

                        foreach (Object itemParam in kvp.Value as List<Object>)
                        {
                            // new Instance of list item
                            Type itemType = listObject.GetType().GetGenericArguments()[0];
                            object newItem = NewInstance(itemType);

                            // set list item to body
                            MethodInfo miListAdd7 = listObject.GetType().GetMethod("Add");
                            miListAdd7.Invoke(listObject, new object[] { newItem });

                            // set list item 
                            SetFields(newItem, itemParam as Dictionary<String, Object>);
                        }
;                   }
                    else {
                        miSetMethod.Invoke(body, new object[] { kvp.Value });
                    }
                }
            }
        }

        private object NewInstance(Type type)
        {
            try
            {
                return type.GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch
            {
                return null;
            }
        }

        private T NewInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { });
        }

        private Type GetObjectTypeUsingName(Type srcType, string keyName)
        {
            String getMethodName = GetGetMethodName(keyName);
            MethodInfo miGetMethod = srcType.GetMethod(getMethodName);
            return miGetMethod.ReturnType;
        }

        private string GetGetMethodName(string methodName)
        {
            if (!String.IsNullOrEmpty(methodName))
            {
                switch (FieldNamingPolicy)
                {
                    case FieldNamingPolicyEnum.UNDERSCORE:
                        return "get_" + UnderscoreToCamelcase(methodName);
                    case FieldNamingPolicyEnum.CAMELCASE:
                        return "get_" + methodName.Substring(0, 1).ToUpper() + methodName.Substring(1);
                    case FieldNamingPolicyEnum.PASCALCASE:
                        return "get_" + methodName;
                }
            }
            return "";
        }

        private string GetSetMethodName(string methodName)
        {
            if (!String.IsNullOrEmpty(methodName))
            {
                switch (FieldNamingPolicy)
                {
                    case FieldNamingPolicyEnum.UNDERSCORE:
                        return "set_" + UnderscoreToCamelcase(methodName);
                    case FieldNamingPolicyEnum.CAMELCASE:
                        return "set_" + methodName.Substring(0, 1).ToUpper() + methodName.Substring(1);
                    case FieldNamingPolicyEnum.PASCALCASE:
                        return "set_" + methodName;
                }
            }
            return "";
        }

        private string UnderscoreToCamelcase(String src)
        {
            string[] temp = src.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            var words = new StringBuilder();
            foreach (string item in temp)
            {
                words.Append(item.Substring(0, 1).ToUpper() + item.Substring(1));
            }
            return words.ToString();
        }

    }
}
