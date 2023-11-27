using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace InfoSystem.AnalysingMethods
{
    internal class XmlDOMAnalysing : IAnalysing
    {
        public void AnalisingMethod(string[] restrictions, string filePath, DataGridView dataGridView1)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);
            // Знаходжу усі моди, що відповідають відповідним restrictions і записую їх у researchers
            string[] res3 = { "faculty[@FACULTY", "chair[@CHAIR", "[name", "[surname", "[degree", "[dateofreceiving" };
            var path = string.Empty; // стрінг, що містить шлях до researcher, що відповідає усім параметрам
            int fc = 2;
            for (int i = 0; i < fc; i++)
            {
                path += (restrictions[i] != string.Empty ? "//" + res3[i] + " = '" + restrictions[i] + "']" : "");
            }
            path += "//researcher";
            for (int i = fc; i < res3.Length; i++)
            {
                path += (restrictions[i] != string.Empty ? res3[i] + " = '" + restrictions[i] + "']" : "");
            }
            var researchers = xml.SelectNodes(path);
            // Заповнюю табличку
            int col = 0, row = 0;
            foreach (XmlNode researcher in researchers)
            {
                dataGridView1[col++, row].Value = researcher.ParentNode.ParentNode.Attributes.GetNamedItem("FACULTY").Value;
                dataGridView1[col++, row].Value = researcher.ParentNode.Attributes.GetNamedItem("CHAIR").Value;
                dataGridView1[col++, row].Value = researcher.ChildNodes[0].InnerText;
                dataGridView1[col++, row].Value = researcher.ChildNodes[1].InnerText;
                dataGridView1[col++, row].Value = researcher.ChildNodes[2].InnerText;
                dataGridView1[col++, row].Value = researcher.ChildNodes[3].InnerText;

                if (dataGridView1.RowCount - row == 1)
                {
                    dataGridView1.RowCount +=10;
                }
                ++row;
                col = 0;
            }
        }
    }
}
