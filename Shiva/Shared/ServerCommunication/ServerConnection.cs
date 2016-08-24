using GnosisControls;
using Shiva.Shared.Data;
using Shiva.Shared.DataControllers;
using Shiva.Shared.Interfaces;
using Shiva.Shared.Utility;
using ShivaWPF3.UtilityWPF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Shiva.Shared.ServerCommunication
{
    public class ServerConnection
    {
        private SqlConnection connection;
        private string connectionString;
        private XElement xSystemRequest;
        private bool connected;

        public bool Connected
        {
            get { return connected; }
        }


        public ServerConnection(
            string userid, 
            string password, 
            string server,
            string systemURL,  
            string database, 
            string deviceType, 
            string hostName,
            IGnosisErrorHandler errorHandler,
            IGnosisIOHelper ioHelper)
        {
            GlobalData.Singleton.IOHelper = ioHelper;
            GlobalData.Singleton.ErrorHandler = errorHandler;

            connectionString = string.Format("user id={0}; password={1};server={2};" +
                                           "Trusted_Connection=no; database={3}; connection timeout=15", 
                                           userid, password, server, database);

            xSystemRequest = new XElement("GnosisSystem");

            XAttribute att = new XAttribute("SystemURL", systemURL);
            xSystemRequest.Add(att);

            XAttribute att1 = new XAttribute("UserName", userid);
            xSystemRequest.Add(att1);

            XAttribute att2 = new XAttribute("DeviceType", deviceType);
            xSystemRequest.Add(att2);

            XAttribute att3 = new XAttribute("HostName", hostName);
            xSystemRequest.Add(att3);

        }

        public bool Connect()
        {
            try
            {
                XElement xSystem;

                //if online
                //connection = new SqlConnection(connectionString);
                //xSystem = GetGnosisSystemXML(xSystemRequest);

                //using (Stream stream = new FileStream(GlobalData.Singleton.IOHelper.GetXMLFilePath("system-august.xml"), FileMode.Create))
                //{
                //    using (StreamWriter sw = new StreamWriter(stream))
                //    {
                //        sw.Write(xSystem.ToString());

                //    }
                //}
                //else 
                using (Stream stream = new FileStream(GlobalData.Singleton.IOHelper.GetXMLFilePath("system-august.xml"), FileMode.Open))
                {
                    xSystem = XElement.Load(stream);

                }

                //end if


                if (xSystem == null)
                {
                    return false;
                }

                GnosisSystem system = (GnosisSystem)GnosisXMLHelper.GnosisDeserializeXML(xSystem);//.LoadGnosisSystem(xSystem);
                GnosisSystemController systemController = new GnosisSystemController(system, xSystem);
                GlobalData.Singleton.SystemController = systemController;

                connected = true;
            }
            catch (SqlException ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                connection = null;

                connected = false;
            }

            return connected;

        }

        public GnosisInstance GetGnosisInstance(XElement xRequest)
        {
            GnosisInstance instance = null;

            if (xRequest.Attribute("Action") == null)
            {
                XAttribute att = new XAttribute("Action", "Search");
                xRequest.Add(att);
            }

            //remove any results content
            xRequest.Descendants("Result").Remove();

            string input = xRequest.ToString();

            //if offline
            if (xRequest.Attribute("EntityID").Value.Equals("2247"))
            {
                using (Stream stream = new FileStream(GlobalData.Singleton.IOHelper.GetXMLFilePath("instance-2247-global.xml"), FileMode.Open))
                {
                    XElement xInstance = XElement.Load(stream);
                    instance = (GnosisInstance)GnosisXMLHelper.GnosisDeserializeXML(xInstance);
                    instance.Content = xInstance;
                }
            }
            else if (xRequest.Attribute("EntityID").Value.Equals("91"))
            {
                using (Stream stream = new FileStream(GlobalData.Singleton.IOHelper.GetXMLFilePath("instance-91.xml"), FileMode.Open))
                {
                    XElement xInstance = XElement.Load(stream);
                    instance = (GnosisInstance)GnosisXMLHelper.GnosisDeserializeXML(xInstance);
                    instance.Content = xInstance;
                }
            }
            else
            {
                //end if
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("gnosis.xml_instance", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("input", input));
                        string res = (string)command.ExecuteScalar();

                        using (StringReader sr = new StringReader(res))
                        {
                            XElement xInstance = XElement.Load(sr);
                            instance = (GnosisInstance)GnosisXMLHelper.GnosisDeserializeXML(xInstance);
                            instance.Content = xInstance;
                        }


                    }
                }
                catch (SqlException ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
                catch (Exception ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
                finally
                {
                    connection.Close();
                }
            //if offline
            }
          //end if
            return instance;

        }

        public XElement GetGnosisEntityXML(XElement entityRequest)
        {
            XElement xEntity = null;
            string input = entityRequest.ToString();

            //if offline
            if (entityRequest.Attribute("EntityID").Value.Equals("2247"))
            {
                using (Stream stream = new FileStream(GlobalData.Singleton.IOHelper.GetXMLFilePath("entity-2247.xml"), FileMode.Open))
                {
                    xEntity = XElement.Load(stream);

                }
            }
            else if (entityRequest.Attribute("EntityID").Value.Equals("91"))
            {
                using (Stream stream = new FileStream(GlobalData.Singleton.IOHelper.GetXMLFilePath("entity-91.xml"), FileMode.Open))
                {
                    xEntity = XElement.Load(stream);

                }
            }
            else
            {
                //end if
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("gnosis.xml_entity", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("input", input));
                        string res = (string)command.ExecuteScalar();

                        using (StringReader sr = new StringReader(res))
                        {
                            xEntity = XElement.Load(sr);
                        }

                    }
                }
                catch (SqlException ex)
                {
                    GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
                }
                finally
                {
                    connection.Close();
                }
            //if offline
            }
            //end if

            return xEntity;
        }

        public XElement GetGnosisSystemXML(XElement systemRequest)
        {
            XElement xSystem = null;
            string input = systemRequest.ToString();

            try
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("gnosis.xml_system", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("input", input));
                    string res = (string)command.ExecuteScalar();

                    using (StringReader sr = new StringReader(res))
                    {
                        xSystem = XElement.Load(sr);
                    }

                }

            }
            catch (SqlException ex)
            {
                GlobalData.Singleton.ErrorHandler.HandleError(ex.Message, ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }

            return xSystem;

        }

      

    }
}
