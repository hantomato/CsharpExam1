using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpExam1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        MemberResult memberResult = new MemberResult();
            memberResult.DisplayName = "nmj";
            memberResult.BbmId = 1234567;
            memberResult.IsConnected = true;
            //memberResult.MyByte = 11;
            //memberResult.MyChar = 'k';
            //memberResult.MyDouble = 123456789123456789;
            //memberResult.MyFloat = 123456.12465f;
            //memberResult.MyShort = 5544;

            object[] parametersArray = new object[] { };
            Type ty = memberResult.GetType();
            {
                MethodInfo mi = ty.GetMethod("get_" + "DisplayName");
                string temp = (string)mi.Invoke(memberResult, parametersArray);

                //Console.WriteLine("Name : " + mi.ReturnType.Name);
                //Console.WriteLine("FullName : " + mi.ReturnType.FullName);
                Console.WriteLine("Type : " + mi.ReturnType.FullName + ", value : " + temp);
            }
            {
                MethodInfo mi = ty.GetMethod("get_" + "BbmId");
                object temp = (object)mi.Invoke(memberResult, parametersArray);
                Console.WriteLine("Type : " + mi.ReturnType.FullName + ", value : " + temp);
            }

            {
                MethodInfo mi = ty.GetMethod("get_" + "MyBool");
                bool temp = (bool)mi.Invoke(memberResult, parametersArray);
                Console.WriteLine("Type : " + mi.ReturnType.FullName + ", value : " + temp);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<String, Object> dic = new Dictionary<String, Object>();
            dic.Add("userId", "neowiztomato@neowiz.com");
            dic.Add("accessToken", "1737402746");

            String jsonString = Json.Serialize(dic);
            Console.WriteLine("jsonString :" + jsonString);
            // jsonString :{"userId":"neowiztomato@neowiz.com","userAge":40,"AccessToken":"1737402746"}
            Dictionary<string, object> dicJson = (Dictionary<string, object>)Json.Deserialize(jsonString);
            foreach (KeyValuePair<string, object> kvp in dicJson)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                Console.WriteLine(getGetMethodName(kvp.Key));

            }
        }




        private void button3_Click(object sender, EventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    

        private void button5_Click(object sender, EventArgs e)
        {

            FriendResult aa = new FriendResult();
            Type tt = aa.GetType();

            List<Friend> listFriend = new List<Friend>();
            Type p1 = listFriend.GetType();
            Type[] p2 = listFriend.GetType().GetGenericArguments();







            //Dictionary<String, Object> dic = new Dictionary<String, Object>();
            //dic.Add("bbmId", 10001);
            //dic.Add("displayName", "nmj");
            //dic.Add("isConnected", true);
            //Dictionary<String, Object> accTo = new Dictionary<String, Object>();
            //accTo.Add("token", "mytoken_abcd");
            //accTo.Add("expireTime", 10000);
            //dic.Add("accessToken", accTo);

            ////List<string> friend = new List<string>();
            ////friend.Add("hello");
            ////friend.Add("world");
            ////friend.Add("kangnam");
            ////dic.Add("friends", friend);

            //List<Payment> paymentTemp = new List<Payment>();
            //Payment p1 = new Payment();
            //p1.PayId = 22;
            //p1.Currency = 1000;
            //Payment p2 = new Payment();
            //p2.PayId = 26;
            //p2.Currency = 1200;
            //paymentTemp.Add(p1);
            //paymentTemp.Add(p2);
            //dic.Add("payments", paymentTemp);

            //object ooomm = paymentTemp;

            //String sssm = paymentTemp.GetType().ToString();

            //String strJsonString = Json.Serialize(dic);
            //// "{\"age\":40,\"bbmId\":10001,\"displayName\":\"nmj\",\"isConnected\":true,\"accessToken\":{\"token\":\"mytoken_abcd\",\"expireTime\":10000}}"
            ////String orgString   "{\"age\":40,\"bbmId\":10001,\"displayName\":\"nmj\",\"isConnected\":true,\"accessToken\":{\"token\":\"mytoken_abcd\",\"expireTime\":10000},\"payments\":[\"CsharpExam1.Payment\",\"CsharpExam1.Payment\"]}";
            //Console.WriteLine("strJsonString : " + strJsonString);
            //String strJsonString2 = FBJson.Serialize(dic);
            //Console.WriteLine("strJsonString2 : " + strJsonString2);

            //            var vv = FBJson.Deserialize(strJsonString2);

            String strJsonString = "{\"serverUrl\":\"this is url\",\"list\":[{\"userId\":\"1111\",\"displayName\":\"neowiztomato\"},{\"userId\":\"2222\",\"displayName\":\"iinuforever\"},{\"userId\":\"3333\",\"displayName\":\"nomoonjin\"}]}";
          //String strJsonString = "{\"serverUrl\":\"this is url\",\"list\":[{\"user_id\":\"1111\",\"display_name\":\"neowiztomato\",\"avatar_url\":\"http://daum.net\"},{\"user_id\":\"2222\",\"display_name\":\"iinuforever\",\"avatar_url\":\"http://google.net\"},{\"user_id\":\"3333\",\"display_name\":\"nomoonjin\",\"avatar_url\":\"http://naver.net\"}]}";
            FriendResult deserializedProduct = JsonConvert.DeserializeObject<FriendResult>(strJsonString);


            FriendResult ret = Deserialize<FriendResult>(strJsonString);
            int jj;
            jj = 33;
        }

        public static T Deserialize<T>(String jsonString)
        {
            Dictionary<String, Object> dicJson = (Dictionary<String, Object>)Json.Deserialize(jsonString);

            String serverUrl = dicJson["serverUrl"] as string;
            Object itemObject = dicJson["list"];
            List<Object> list = (List<Object>)dicJson["list"];
            Dictionary<String, Object> friend1 = (Dictionary<String, Object>)list[0];
            Dictionary<String, Object> friend2 = (Dictionary<String, Object>)list[1];
            Dictionary<String, Object> friend3 = (Dictionary<String, Object>)list[2];
            //Dictionary<String, Friend[]> itemList = dicJson["list"] as Dictionary<String, Friend[]>>;


            T ret = newInstance<T>();
            setMember(ret, dicJson);
            return ret;
        }

        public static void setMember(Object src, Dictionary<String, Object> param)
        {
            // 
            Type srcType = src.GetType();
            object[] emptyParam = new object[] { };
            String setMethodName;
            foreach (KeyValuePair<string, object> kvp in param)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);

                setMethodName = getSetMethodName(kvp.Key);
                MethodInfo miSetMethod = srcType.GetMethod(setMethodName);
                Console.WriteLine(setMethodName);
                Console.WriteLine("Value.GetType() : " + kvp.Value.GetType());

                if (miSetMethod != null)
                {
                    if (typeof(Dictionary<String, Object>).Equals(kvp.Value.GetType()))
                    {
                        // new instance some object
                        String getMethodName = getGetMethodName(kvp.Key);
                        MethodInfo miGetMethod = srcType.GetMethod(getMethodName);
                        object subMember = newInstance(miGetMethod.ReturnType);

                        // set object to body
                        miSetMethod.Invoke(src, new object[] { subMember });
                        setMember(subMember, kvp.Value as Dictionary<String, Object>);
                    }
                    else if (typeof(List<Object>).Equals(kvp.Value.GetType()))
                    {
                        // new Instance of list
                        String getMethodName = getGetMethodName(kvp.Key);
                        MethodInfo miGetMethod = srcType.GetMethod(getMethodName);
                        object listObject = newInstance(miGetMethod.ReturnType);

                        // set listObject to body
                        miSetMethod.Invoke(src, new object[] { listObject });

                        foreach (Object itemParam in kvp.Value as List<Object>)
                        {
                            // new Instance of list item
                            Type itemType = listObject.GetType().GetGenericArguments()[0];
                            object newItem = newInstance(itemType);

                            // set list item to body
                            MethodInfo miListAdd7 = listObject.GetType().GetMethod("Add");
                            miListAdd7.Invoke(listObject, new object[] { newItem });

                            // set list item 
                            setMember(newItem, itemParam as Dictionary<String, Object>);
                        }
;
                    }
                    else {
                        miSetMethod.Invoke(src, new object[] { kvp.Value });
                    }
                }
            }

            Console.WriteLine("src : " + src.ToString());
        }

        public static object newInstance(Type type)
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

        public static T newInstance<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
        public static T newInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { });
        }

        public static string getGetMethodName(string methodName)
        {
            if (String.IsNullOrEmpty(methodName))
            {
                return "";
            }


                return "get_" + methodName.Substring(0, 1).ToUpper() + methodName.Substring(1);
        }

        public static string getSetMethodName(string methodName)
        {
            if (!String.IsNullOrEmpty(methodName))
                return "set_" + methodName.Substring(0, 1).ToUpper() + methodName.Substring(1);
            return "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //\"my_friend\":{\"UserId\":\"1111\",\"DisplayName\":\"neowiztomato\"}
            // PasCal
            //String strJsonStringUnderscore = "{\"serverUrl\":\"this is url\",\"list\":[{\"user_id\":\"1111\",\"display_name\":\"neowiztomato\",\"avatar_url\":\"http://daum.net\"},{\"user_id\":\"2222\",\"display_name\":\"iinuforever\",\"avatar_url\":\"http://google.net\"},{\"user_id\":\"3333\",\"display_name\":\"nomoonjin\",\"avatar_url\":\"http://naver.net\"}]}";
            //String strJsonStringPascal = "{\"ServerUrl\":\"this is url\",\"List\":[{\"UserId\":\"1111\",\"DisplayName\":\"neowiztomato\"},{\"UserId\":\"2222\",\"DisplayName\":\"iinuforever\"},{\"UserId\":\"3333\",\"DisplayName\":\"nomoonjin\"}]}";
            //String strJsonStringCamelcase = "{\"serverUrl\":\"this is url\",\"list\":[{\"userId\":\"1111\",\"displayName\":\"neowiztomato\"},{\"userId\":\"2222\",\"displayName\":\"iinuforever\"},{\"userId\":\"3333\",\"displayName\":\"nomoonjin\"}]}";

            //String strJsonStringUnderscore = "{\"my_friend\":{\"user_id\":\"7777\",\"display_name\":\"hantomato\"},\"serverUrl\":\"this is url\",\"list\":[{\"user_id\":\"1111\",\"display_name\":\"neowiztomato\",\"avatar_url\":\"http://daum.net\"},{\"user_id\":\"2222\",\"display_name\":\"iinuforever\",\"avatar_url\":\"http://google.net\"},{\"user_id\":\"3333\",\"display_name\":\"nomoonjin\",\"avatar_url\":\"http://naver.net\"}]}";
            //String strJsonStringPascal = "{\"MyFriend\":{\"UserId\":\"7777\",\"DisplayName\":\"hantomato\"},\"ServerUrl\":\"this is url\",\"List\":[{\"UserId\":\"1111\",\"DisplayName\":\"neowiztomato\"},{\"UserId\":\"2222\",\"DisplayName\":\"iinuforever\"},{\"UserId\":\"3333\",\"DisplayName\":\"nomoonjin\"}]}";
            String strJsonStringCamelcase = "{\"myFriend\":{\"userId\":\"7777\",\"displayName\":\"hantomato\"},\"serverUrl\":\"this is url\",\"list\":[{\"userId\":\"1111\",\"displayName\":\"neowiztomato\"},{\"userId\":\"2222\",\"displayName\":\"iinuforever\"},{\"userId\":\"3333\",\"displayName\":\"nomoonjin\"}]}";


            String strJsonStringCamelcase2 = "{\"myFriend\":null,\"serverUrl\":\"this is url\",\"list\":[{\"userId\":\"1111\",\"displayName\":\"neowiztomato\"},{\"userId\":\"2222\",\"displayName\":\"iinuforever\"},{\"userId\":\"3333\",\"displayName\":\"nomoonjin\"}]}";
            String strJsonStringCamelcase3 = "{\"myFriend\":null,\"serverUrl\":\"\",\"list\":[{\"userId\":\"1111\",\"displayName\":\"neowiztomato\"},{\"userId\":\"2222\",\"displayName\":\"iinuforever\"},{\"userId\":\"3333\",\"displayName\":\"nomoonjin\"}]}";
            String strJsonStringCamelcase4 = "{\"myFriend\":null,\"serverUrl\":null,\"list\":[{\"userId\":\"1111\",\"displayName\":\"neowiztomato\"},{\"userId\":\"2222\",\"displayName\":\"iinuforever\"},{\"userId\":\"3333\",\"displayName\":\"nomoonjin\"}]}";

            String strJsonStringCamelcase5 = "{\"myFriend\":null,\"serverUrl\":null,\"list\":[]}";
            String strJsonStringCamelcase6 = "{\"myFriend\":null,\"serverUrl\":null,\"list\":null}";

            //            String strJsonStringCamelcase = "{\"serverUrl\":\"this is url\",\"list\":[{\"userId\":\"1111\",\"displayName\":\"neowiztomato\"},{\"userId\":\"2222\",\"displayName\":\"iinuforever\"},{\"userId\":\"3333\",\"displayName\":\"nomoonjin\"}]}";


            //FriendResult deserializedProduct = JsonConvert.DeserializeObject<FriendResult>(strJsonStringCamelcase);

            JsonDeserialize jd = new JsonDeserialize();

            //jd.FieldNamingPolicy = JsonDeserialize.FieldNamingPolicyEnum.UNDERSCORE;
            //FriendResult ret1 = jd.Deserialize<FriendResult>(strJsonStringUnderscore);

            //jd.FieldNamingPolicy = JsonDeserialize.FieldNamingPolicyEnum.PASCALCASE;
            //FriendResult ret2 = jd.Deserialize<FriendResult>(strJsonStringPascal);

            FriendResult fr = new FriendResult();
            if (fr.ServerUrl == null)
            {
                Type ObjType = GetObjectTypeUsingName(fr.GetType(), "get_ServerUrl");
                object subObject = NewInstance(ObjType);

                int aa;
                aa = 33;
            }

            //if (fr.MyFriend == null)
            //{
            //    Type ObjType = GetObjectTypeUsingName(fr.GetType(), "get_MyFriend");
            //    object subObject = NewInstance(ObjType);

            //    int aa;
            //    aa = 33;

            //}


            jd.FieldNamingPolicy = JsonDeserialize.FieldNamingPolicyEnum.CAMELCASE;
            //FriendResult ret3 = jd.Deserialize<FriendResult>(strJsonStringCamelcase);

            FriendResult ret1 = jd.Deserialize<FriendResult>(strJsonStringCamelcase);
            //FriendResult ret2 = jd.Deserialize<FriendResult>(strJsonStringCamelcase2);
            //FriendResult ret3 = jd.Deserialize<FriendResult>(strJsonStringCamelcase3);
            FriendResult ret4 = jd.Deserialize<FriendResult>(strJsonStringCamelcase4);
            FriendResult ret5 = jd.Deserialize<FriendResult>(strJsonStringCamelcase5);
            FriendResult ret6 = jd.Deserialize<FriendResult>(strJsonStringCamelcase6);

            int jj;
            jj = 33;

            //String sm1 = ToUnderscoreCase("serverUrl");
            //String sm2 = ToUnderscoreCase("userId");
            //String sm3 = ToUnderscoreCase("displayNameValue");
            //Console.WriteLine("ret : " + ret);

            //Friend friend = new Friend();
            //String test = "";
            //Type typeFriend = friend.GetType();
            //Type typeTest = test.GetType();
            //jj = 33;

            
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

        private Type GetObjectTypeUsingName(Type srcType, string keyName)
        {
            String getMethodName = keyName;
            MethodInfo miGetMethod = srcType.GetMethod(getMethodName);
            return miGetMethod.ReturnType;
        }

        
    }
}
