namespace Linterhub.Engine.Linters.jshint
{
    using System.Xml;
    using System.Xml.Serialization;
    using System.Collections.Generic;

    public class File
    {
        [XmlAttribute("name")]
        public string FilePath { get; set; }

        [XmlElement("error")]
        public List<Error> ErrorsList { get; set; }
    }
}