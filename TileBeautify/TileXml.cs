using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace TileBeautify {
    public class TileXml {
        private readonly XmlDocument xmlDoc = new XmlDocument();//xml文档
        public string ExeName {get; set;}//exe文件名
        public string ExeFolder {get;set;}//exe文件夹
        public string XmlPath {get;set;}//xml文件路径
        public bool MightHasPic {get;private set; }//可能存在路径判断不存在的可缩放图片
        public string ShowNameOnSquare150x150Logo {get;set;}//是否显示文字
        public string Square150x150Logo {get;set;}//图片文件路径
        public string Square70x70Logo {get;set;}
        public string Square44x44Logo {get;set;}
        public string ForegroundText {get;set;}//文字颜色
        public string BackgroundColor {get;set;}//背景颜色

        public TileXml(string exePath) {
            ExeName = Path.GetFileNameWithoutExtension(exePath);
            ExeFolder = Path.GetDirectoryName(exePath) + "\\";
            XmlPath = ExeFolder + ExeName + ".VisualElementsManifest.xml";
            if (File.Exists(XmlPath)) {
                xmlDoc.Load(XmlPath);
                ShowNameOnSquare150x150Logo = ReadXml("ShowNameOnSquare150x150Logo");
                ForegroundText = ReadXml("ForegroundText");
                BackgroundColor = ReadXml("BackgroundColor");
                string str1 = ReadXml("Square150x150Logo");
                string str2 = ReadXml("Square70x70Logo");
                string str3 = ReadXml("Square44x44Logo");
                if (str1 != null || str2 != null || str3 != null) MightHasPic = true;
                else MightHasPic = false;
                Square150x150Logo = ExeFolder + str1;
                Square70x70Logo = ExeFolder + str2;
                Square44x44Logo = ExeFolder + str3;
            }
        }

        //写入xml文件
        public void WriteXml(string filePath) {
            XElement doc = new XElement("VisualElements");
            doc.Add(new XAttribute("ShowNameOnSquare150x150Logo", ShowNameOnSquare150x150Logo));

            //这里用传入的string.Empty与传出的null相区分
            if (Square150x150Logo != string.Empty) {
                doc.Add(new XAttribute("Square150x150Logo", Square150x150Logo));
            }
            if (Square70x70Logo != string.Empty) {
                doc.Add(new XAttribute("Square70x70Logo", Square70x70Logo));
            }
            if (Square44x44Logo != string.Empty) {
                doc.Add(new XAttribute("Square44x44Logo", Square44x44Logo));
            }
            doc.Add(new XAttribute("ForegroundText", ForegroundText));
            doc.Add(new XAttribute("BackgroundColor", BackgroundColor));
            XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
            XElement root = new XElement("Application");
            root.Add(new XAttribute(XNamespace.Xmlns + "xns", ns));
            root.Add(doc);
            root.Save(filePath);
        }

        //读取xml文件
        public string ReadXml(string attributes) {
            XmlNode xmlNode = xmlDoc.DocumentElement.SelectSingleNode("/Application/VisualElements");
            string result;
            try {
                result = xmlNode.Attributes[attributes].Value;
            }
            catch (Exception) {
                result = null;
            }
            return result;
        }
    }
}