using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InfoSystem.AnalysingMethods
{
    internal class XmlLinqAnalysing : IAnalysing
    {
        public void AnalisingMethod(string[] restrictions, string filePath, DataGridView dataGridView1)
        {
            int rowCount = dataGridView1.RowCount;
            XDocument xdoc = XDocument.Load(filePath);
             // За допомогою Linq зберігаю у list myResearchers усіх дослідників, що відповідають фільтрам
            var myResearchers =
                (
                from myFaculty in xdoc.Descendants("faculty")
                where (myFaculty.Attribute("FACULTY").Value == (restrictions[0] == string.Empty ? myFaculty.Attribute("FACULTY").Value : restrictions[0]))
                from myChair in myFaculty.Descendants("chair")
                where (myChair.Attribute("CHAIR").Value == (restrictions[1] == string.Empty ? myChair.Attribute("CHAIR").Value : restrictions[1]))
                from myResear in myChair.Descendants("researcher")
                where ((myResear.Elements("name").Single().Value == (restrictions[2] == String.Empty ? myResear.Elements("name").Single().Value : restrictions[2])) 
                        && (myResear.Elements("surname").Single().Value == (restrictions[3] == String.Empty ? myResear.Elements("surname").Single().Value : restrictions[3])) 
                        && (myResear.Elements("degree").Single().Value == (restrictions[4] == String.Empty ? myResear.Elements("degree").Single().Value : restrictions[4])) 
                        && (myResear.Elements("dateofreceiving").Single().Value == (restrictions[5] == String.Empty ? myResear.Elements("dateofreceiving").Single().Value : restrictions[5])))
                select new
                {
                    _facul = (string)myFaculty.Attribute("FACULTY").Value,
                    _chair = (string)myChair.Attribute("CHAIR").Value,
                    _name = (string)myResear.Elements("name").Single().Value,
                    _surname = (string)myResear.Elements("surname").Single().Value,
                    _degree = (string)myResear.Elements("degree").Single().Value,
                    _dateofreceiving = (string)myResear.Elements("dateofreceiving").Single().Value
                }
                ).ToList();
            // Виводжу свій лист 
            int col = 0, row = 0;
            foreach (var resear in myResearchers)
            {
                dataGridView1[col++, row].Value = resear._facul;
                dataGridView1[col++, row].Value = resear._chair;
                dataGridView1[col++, row].Value = resear._name;
                dataGridView1[col++, row].Value = resear._surname;
                dataGridView1[col++, row].Value = resear._degree;
                dataGridView1[col++, row].Value = resear._dateofreceiving;
                if (rowCount - row == 1)
                {
                    rowCount += 10;
                    dataGridView1.RowCount = rowCount;
                }
                ++row;
                col = 0;
            }
        }
    }
}