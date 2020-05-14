using System.IO;
using System.Xml;

namespace Tribalwars.UI.DeffRequester.Util
{
    public class Config
    {
        // we create a singleton here
        static Config _current;
        public static Config Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new Config();
                }
                return _current;
            }
        }
        public static string SzConfigFile;
        private readonly XmlDocument _xmlDoc;
        private readonly XmlNode _settingsNode;
        public Config()
        {

            _xmlDoc = new XmlDocument();
            if (File.Exists(SzConfigFile))
            {
                _xmlDoc.Load(SzConfigFile);
            }
            else
            {
                _xmlDoc.LoadXml("<Settings></Settings>");
            }
            _settingsNode = _xmlDoc.SelectSingleNode("//Settings");
            SaveDefault();
        }
        public string CodeGenerationTemplate
        {
            get
            {
                XmlNode oneNode = XmlUtil.EnsureElement(_settingsNode, "CodeGenerationTemplate");
                if (string.IsNullOrWhiteSpace(oneNode.InnerText))
                {
                    oneNode.InnerText = "{Counter}. [b]Dorf:[/b] [coord]{Coords}[/coord] - ab {ArrivalFirstInc} - {IncCount} - {RequestedDeff}";
                }
                return oneNode.InnerText;
            }
            set
            {
                XmlNode oneNode = XmlUtil.EnsureElement(_settingsNode, "CodeGenerationTemplate");
                oneNode.InnerText = value;
                Save();
            }
        }
        public void Save()
        {
            _xmlDoc.Save(SzConfigFile);
        }
        public void SaveDefault()
        {
            var dummy = this.CodeGenerationTemplate;
            this.CodeGenerationTemplate = dummy;
            Save();
        }
    }
}
