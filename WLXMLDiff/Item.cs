using System.Xml.Serialization;

namespace WLXMLDiff
{
    [XmlRoot("ITEM"), XmlType("ITEM")]
    public class Item
    {
        [XmlElement("ITEMTYPE")]
        public string ItemType { get; set; }
        [XmlElement("ITEMID")]
        public string ItemId { get; set; }
        [XmlElement("COLOR")]
        public long Color { get; set; }
        [XmlElement("MINQTY")]
        public long MinQty { get; set; }
    }
}
