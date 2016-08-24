using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

using Shiva.Shared.Events;
using Shiva.Shared.Data;
using GnosisControls;
using System.Xml;
using System.Reflection;
using Shiva.Shared.Interfaces;

namespace Shiva.Shared.Utility
{
    public class GnosisXMLHelper
    {
        public static XElement GnosisBuildXML(object gnosisObject)
        {
            XElement element = new XElement(gnosisObject.GetType().Name);

            var propertyInfo = gnosisObject.GetType().GetProperties();

            //attributes
            var properties = propertyInfo.Where(prop => prop.IsDefined(typeof(GnosisPropertyAttribute), false));

            foreach (var property in properties)
            {
                string name = PropertyNameConverter.GetXMLPropertyName(property.Name);
                if (name == null)
                {
                    name = property.Name;
                }

                if (property.GetValue(gnosisObject) != null)
                {
                    XAttribute att = new XAttribute(name, property.GetValue(gnosisObject));
                    element.Add(att);
                }
            }

            //single childs
            var childs = propertyInfo.Where(prop => prop.IsDefined(typeof(GnosisChildAttribute), false));

            foreach (var child in childs)
            {
                XElement childElement = GnosisBuildXML(child);
                element.Add(childElement);
            }

            //lists
            var lists = propertyInfo.Where(prop => prop.IsDefined(typeof(GnosisCollectionAttribute), false));

            foreach (var list in lists)
            {
                if (list.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var items = list.GetValue(gnosisObject);
                    List<object> itemList = (items as IEnumerable<object>).Cast<object>().ToList();
                    foreach (var item in itemList)
                    {
                        XElement childElement = GnosisBuildXML(item);
                        element.Add(childElement);
                    }
                }
            }

            return element;

        }

        public static object GnosisDeserializeXML(XElement baseElement)
        {
            string className = "GnosisControls." + baseElement.Name.LocalName;
            var obj = Activator.CreateInstance(null, className);
            object baseObject = obj.Unwrap();

            //attributes
            foreach (var xAttribute in baseElement.Attributes())
            {
                string shivaName = PropertyNameConverter.GetShivaPropertyName(xAttribute.Name.ToString());
                if (shivaName == null)
                {
                    shivaName = xAttribute.Name.ToString();
                }

                PropertyInfo propInfo = baseObject.GetType().GetProperty(shivaName);
                //if (propInfo == null)
                //{
                //    var props = baseObject.GetType().GetProperties();
                //}
                //propInfo.SetValue(baseObject, attribute.Value);
                object val;
                if (propInfo.PropertyType == typeof(bool) &&  xAttribute.Value.Equals("0"))
                {
                    val = false;
                }
                else if (propInfo.PropertyType == typeof(bool) && xAttribute.Value.Equals("1"))
                {
                    val = true;
                }
                else
                {
                    val = Convert.ChangeType(xAttribute.Value, propInfo.PropertyType);
                }

                propInfo.SetValue(baseObject, val, null);
            }

            //elements
            //content of GnosisInstance, GnosisDataCache is unknown so is not deserialized
            if (!(baseObject is GnosisInstance || baseObject is GnosisDataCache))
            {
                foreach (var element in baseElement.Elements())
                {
                    var child = GnosisDeserializeXML(element);
                    ((IGnosisObject)baseObject).GnosisAddChild((IGnosisObject)child);
                }
            }

            //if (baseObject is GnosisEntity)
            //{
            //    int i = 1;
            //}

            return baseObject;

        }

        public static void SaveParentWindow(GnosisParentWindow parentWindow)
        {
            //try
            //{
            //    XmlSerializer serializer = new XmlSerializer(typeof(GnosisParentWindow)) ;
            //    using (TextWriter writer = GlobalData.Singleton.IOHelper.GetTextWriter())
            //    {
            //        serializer.Serialize(writer, parentWindow);
                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //   GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.InnerException.Message);
            //}
        }

        public static void SaveGnosisDocFrame(GnosisDocumentFrame frame)
        {
            //try
            //{
            //    XmlSerializer serializer = new XmlSerializer(typeof(GnosisDocumentFrame));
            //    using (TextWriter writer = GlobalData.Singleton.IOHelper.GetTextWriter())
            //    {
            //        serializer.Serialize(writer, frame);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            //}
        }

        public static GnosisEntity LoadGnosisEntity(XElement xEntity)
        {
            return (GnosisEntity)GnosisDeserializeXML(xEntity);
            //XmlSerializer serializer = new XmlSerializer(typeof(GnosisEntity));
            //GnosisEntity entity = null;
            
            //using (StringReader sr = new StringReader(xmlString))
            //{
            //    try
            //    {
            //        entity = (GnosisEntity)serializer.Deserialize(sr);
            //    }
            //    catch (Exception ex)
            //    {
            //        GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            //    }
            //}

            //return entity;
        }

        //public static GnosisInstance LoadGnosisInstace(XElement xInstance)
        //{
        //    GnosisInstance instance = (GnosisInstance)GnosisDeserializeXML(xInstance);

        //}

        //public static GnosisInstance LoadGnosisInstance(string xmlString)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(GnosisInstance));
        //    GnosisInstance instance = null;

        //    using (StringReader sr = new StringReader(xmlString))
        //    {
        //        try
        //        {
        //            instance = (GnosisInstance)serializer.Deserialize(sr);
        //        }
        //        catch (Exception ex)
        //        {
        //            GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
        //        }
        //    }

        //    using (StringReader sr = new StringReader(xmlString))
        //    {
        //        try
        //        {
        //            XElement xInstance = XElement.Load(sr);
        //            instance.Content = xInstance;
        //        }
        //        catch (Exception ex)
        //        {
        //            GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
        //        }
        //    }

        //        return instance;
        //}

        public static GnosisInstance LoadGnosisInstance(XmlReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GnosisInstance));
            GnosisInstance instance = null;

            try
            {
                instance = (GnosisInstance)serializer.Deserialize(reader);
                XElement xInstance = XElement.Load(reader);
                instance.Content = xInstance;
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return instance;
            
        }

        //public static GnosisSystem LoadGnosisSystem(string name)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(GnosisSystem));
        //    GnosisSystem system = null;

        //    using (StreamReader sr = GlobalData.Singleton.IOHelper.GetStreamReader(name))
        //    {
        //        //try
        //        //{
        //            system = (GnosisSystem)serializer.Deserialize(sr);
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    GlobalData.Instance.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
        //        //}
        //    }

        //    return system;
        //}

        public static GnosisSystem LoadGnosisSystem(XElement xSystem)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GnosisSystem));
            GnosisSystem system = null;

            try
            {
                using (StringReader sr = new StringReader(xSystem.ToString()))
                {
                    system = (GnosisSystem)serializer.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            return system;
        }


        //public static GnosisFrame LoadGnosisFrame(StreamReader sr)
        //{
        //    XmlSerializer serializer = new XmlSerializer(typeof(GnosisFrame));
        //    GnosisFrame frame = null;

        //    try
        //    {
        //        frame = (GnosisFrame)serializer.Deserialize(sr);
        //    }
        //    catch (Exception ex)
        //    {
        //        GlobalData.Instance.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
        //    }

        //    return frame;
        //}



        public static GnosisConnection LoadGnosisConnection(StreamReader sr)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GnosisConnection));
            GnosisConnection con = null;

            try
            {
                con = (GnosisConnection)serializer.Deserialize(sr);
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            sr.Close();
            return con;
        }

        public static GnosisParentWindow LoadGnosisParentWindow(StreamReader sr)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GnosisParentWindow));
            GnosisParentWindow parentWindow = null;

            try
            {
                parentWindow = (GnosisParentWindow)serializer.Deserialize(sr);
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            sr.Close();
            return parentWindow;
        }

        public static GnosisNavigatorFrame LoadGnosisNavFrame(StreamReader sr)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GnosisNavigatorFrame));
            GnosisNavigatorFrame navFrame = null;

            try
            {
                navFrame = (GnosisNavigatorFrame)serializer.Deserialize(sr);
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }

            sr.Close();
            return navFrame;
        }

        public static GnosisDocumentFrame LoadGnosisDocFrame(StreamReader sr)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GnosisDocumentFrame));
            GnosisDocumentFrame docFrame = null;

            try
            {
                docFrame = (GnosisDocumentFrame)serializer.Deserialize(sr);
            }
            catch (Exception ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.InnerException.Message);
            }

            sr.Close();
            return docFrame;

        }


        //public static string GetString(string dataFileName, string path, string attributeName)
        //{
        //    string strVal = "";

        //    using (Stream stream = GlobalData.Singleton.IOHelper.GetXMLStream(dataFileName))
        //    {
        //        XElement xElement = XElement.Load(stream);
        //        IEnumerable val = (IEnumerable)xElement.XPathEvaluate("//" + path + "/@" + attributeName);
        //        try
        //        {
        //            strVal = val.Cast<XAttribute>().FirstOrDefault().Value;
        //        }
        //        catch (Exception)
        //        {
        //            GlobalData.Singleton.ErrorHandler.HandleError("Attribute not found: " + attributeName + ", path=" + path + ", document=" + dataFileName, "GnosisXMLHelper.GetString");
        //        }
        //    }

        //    return strVal;
        //}


        //public static bool GetBool(string dataFileName, string path, string attributeName)
        //{
        //    string strBool = "";

        //    using (Stream stream = GlobalData.Singleton.IOHelper.GetXMLStream(dataFileName))
        //    {
        //        XElement xElement = XElement.Load(stream);
        //        IEnumerable val = (IEnumerable)xElement.XPathEvaluate("//" + path + "/@" + attributeName);
        //        try
        //        {
        //            strBool = val.Cast<XAttribute>().FirstOrDefault().Value;
        //        }
        //        catch
        //        {
        //            GlobalData.Singleton.ErrorHandler.HandleError("Attribute not found: " + attributeName + ", path=" + path + ", document=" + dataFileName, "GnosisXMLHelper.GetBool");
        //        }
        //    }
            
        //    if (strBool.Equals("1")) return true;

        //    return false;
        //}

        //public static IEnumerable<XElement> GetRows(string dataFileName, string elementName)
        //{
        //    IEnumerable<XElement> rows = null;

        //    using (Stream dataStream = GlobalData.Singleton.IOHelper.GetXMLStream(dataFileName))
        //    {
        //        XElement instance = XElement.Load(dataStream);
        //        rows = instance.Descendants(elementName);

        //    }

        //    return rows;
        //}


    }
}

