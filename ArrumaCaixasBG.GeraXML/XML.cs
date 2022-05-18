using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ArrumaCaixasBG.GeraXML;

// using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Instance));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Instance)serializer.Deserialize(reader);
// }

[XmlRoot(ElementName = "Point")]
public class Point
{

    [XmlAttribute(AttributeName = "X")]
    public int X { get; set; }

    [XmlAttribute(AttributeName = "Y")]
    public int Y { get; set; }

    [XmlAttribute(AttributeName = "Z")]
    public int Z { get; set; }
}

[XmlRoot(ElementName = "Cube")]
public class Cube
{

    [XmlElement(ElementName = "Point")]
    public Point Point { get; set; }

    [XmlAttribute(AttributeName = "Length")]
    public int Length { get; set; }

    [XmlAttribute(AttributeName = "Width")]
    public int Width { get; set; }

    [XmlAttribute(AttributeName = "Height")]
    public int Height { get; set; }
}

[XmlRoot(ElementName = "Container")]
public class Container
{

    [XmlElement(ElementName = "Cube")]
    public Cube Cube { get; set; }

    [XmlAttribute(AttributeName = "ID")]
    public int ID { get; set; }
}

[XmlRoot(ElementName = "Containers")]
public class Containers
{

    [XmlElement(ElementName = "Container")]
    public List<Container> Container { get; set; }
}

[XmlRoot(ElementName = "Cubes")]
public class Cubes
{

    [XmlElement(ElementName = "Cube")]
    public Cube Cube { get; set; }
}

[XmlRoot(ElementName = "Components")]
public class Components
{

    [XmlElement(ElementName = "Cubes")]
    public Cubes Cubes { get; set; }
}

[XmlRoot(ElementName = "Piece")]
public class Piece
{

    [XmlElement(ElementName = "Components")]
    public Components Components { get; set; }

    [XmlAttribute(AttributeName = "ID")]
    public int ID { get; set; }

    [XmlAttribute(AttributeName = "ForbiddenOrientations")]
    public string ForbiddenOrientations { get; set; }

    [XmlAttribute(AttributeName = "Material")]
    public string Material { get; set; }

    [XmlAttribute(AttributeName = "Stackable")]
    public bool Stackable { get; set; }
}

[XmlRoot(ElementName = "Pieces")]
public class Pieces
{

    [XmlElement(ElementName = "Piece")]
    public List<Piece> Piece { get; set; }
}

[XmlRoot(ElementName = "Instance")]
public class Instance
{

    [XmlElement(ElementName = "Containers")]
    public Containers Containers { get; set; }

    [XmlElement(ElementName = "Pieces")]
    public Pieces Pieces { get; set; }

    [XmlElement(ElementName = "Solutions")]
    public object Solutions { get; set; }

    [XmlAttribute(AttributeName = "Version")]
    public string Version { get; set; }

    [XmlAttribute(AttributeName = "Name")]
    public string Name { get; set; }
}

