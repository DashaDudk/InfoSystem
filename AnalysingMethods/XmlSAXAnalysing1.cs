using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace InfoSystem.AnalysingMethods
{
    internal class XmlSAXAnalysing : IAnalysing
    {
        public void AnalisingMethod(string[] restrictions, string filePath, DataGridView dataGridView1)
        {
            int columnCount = dataGridView1.ColumnCount;
            int rowCount = dataGridView1.RowCount;
            var stringbuffer = new string[columnCount];
            var xmlReader = new XmlTextReader(filePath);
            int col = 0, row = 0;
            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xmlReader.Name == "faculty")
                        {
                            if ((xmlReader.GetAttribute(0) == (restrictions[0] == String.Empty ? xmlReader.GetAttribute(0) : restrictions[0])))
                            {
                                col = 0;
                                stringbuffer[col++] = xmlReader.GetAttribute(0);
                            }
                            else
                            {
                                xmlReader.Read();
                                while (xmlReader.Name != "faculty")
                                {
                                    xmlReader.Read();
                                }
                            }
                        }
                        else if (xmlReader.Name == "chair")
                        {
                            col = 1;
                            if ((xmlReader.GetAttribute(0) == (restrictions[1] == String.Empty ? xmlReader.GetAttribute(0) : restrictions[1])))
                            {
                                stringbuffer[col++] = xmlReader.GetAttribute(0);
                            }
                            else
                            {
                                xmlReader.Read();
                                while (xmlReader.Name != "chair")
                                {
                                    xmlReader.Read();
                                }
                            }
                        }
                        else if (xmlReader.Name == "researcher")
                        {
                            col = 2;
                            while (xmlReader.Read())
                            {
                                if (xmlReader.Name == "researcher")
                                    break;
                                else if (xmlReader.NodeType == XmlNodeType.Text)
                                {
                                    if (xmlReader.Value == (restrictions[col] == String.Empty ? xmlReader.Value : restrictions[col]))
                                    {
                                        stringbuffer[col++] = xmlReader.Value;
                                    }
                                    else
                                    {
                                        while (xmlReader.Name != "researcher")
                                        {
                                            xmlReader.Read();
                                        }
                                        break;

                                    }
                                    if (col == columnCount)
                                    {
                                        int i = 0;
                                        foreach (string cell in stringbuffer)
                                        {
                                            dataGridView1[i++, row].Value = cell;
                                        }
                                        if (rowCount - row == 1)
                                        {
                                            rowCount += 10;
                                            dataGridView1.RowCount = rowCount;
                                        }
                                        ++row;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            xmlReader.Close();
        }
    }
}