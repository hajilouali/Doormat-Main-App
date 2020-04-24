using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;



namespace DoormatSite.Tools
{
    public static class xml
    {
        public static string loadline(string Route)
        {


            string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\GetFolderPath\\Content.xml";


            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            string result = xmlDocument.SelectSingleNode("/Root/" + Route).InnerText;
            return result;
        }

        public static bool SavetoXml(string Route, string text)
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                        + "\\GetFolderPath\\Content.xml";
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                xmlDocument.SelectSingleNode("/Root/" + Route).InnerText = text;
                xmlDocument.Save(path);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}