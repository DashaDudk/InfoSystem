using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlTypes;
using System.Text;
using System.Xml.Xsl;
using InfoSystem.AnalysingMethods;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Linq;

namespace InfoSystem
{
    public partial class Form1 : Form
    {
        private const int columnCount = 6, checkBoxCount = 6;
        private int rowCount;
        private const string filePath = "dataBase.xml";

        // Initializing part
        private void InitializeDataGridView()
        {
            string[] headers = { "Faculty", "Chair", "Name", "Surname", "Degree", "Dateofreceiving" };
            rowCount = 10;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnCount = columnCount;
            dataGridView1.RowCount = rowCount;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderText = headers[col.Index];
            }
        }
        private void InitializeComboBox(dataBase dataBaseClass)
        {
            //Заповнення ComboBox-ів без повтор. елементів 
            foreach (dataBaseFaculty DBF in dataBaseClass.faculty)
            {
                facultyComboBox.Items.Add(DBF.FACULTY);
            }
            foreach (dataBaseFaculty DBF in dataBaseClass.faculty)
            {
                foreach (dataBaseFacultyChair DBFC in DBF.chair)
                {
                    chairComboBox.Items.Add(DBFC.CHAIR);
                }
            }
            // Створюються пусті списки рядків 
            List<string> nameList = new List<string>();
            List<string> surnameList = new List<string>();
            List<string> degreeList = new List<string>();
            List<string> dateofreceivingList = new List<string>();
            // Заповнюю відповідні списки
            foreach (dataBaseFaculty DBF in dataBaseClass.faculty)
            {
                foreach (dataBaseFacultyChair DBFC in DBF.chair)
                {
                    foreach (dataBaseFacultyChairResearcher DBFCR in DBFC.researcher)
                    {
                        nameList.Add(DBFCR.name);
                        surnameList.Add(DBFCR.surname);
                        degreeList.Add(DBFCR.degree);
                        dateofreceivingList.Add(DBFCR.dateofreceiving);
                    }
                }
            }
            // Видаляю з листів дублюючі елементи
            var newNameList = new HashSet<string>(nameList).ToList();
            var newSurnameList = new HashSet<string>(surnameList).ToList();
            var newDegreeList = new HashSet<string>(degreeList).ToList();
            var newDateofreceivingList = new HashSet<string>(dateofreceivingList).ToList();
           
            // Заповнюю ComboBox-си
            foreach (string el in newNameList)
            {
                nameComboBox.Items.Add(el);
            }
            foreach (string el in newSurnameList)
            {
                surnameComboBox.Items.Add(el);
            }
            foreach (string el in newDegreeList)
            {
                degreeComboBox.Items.Add(el);
            }
            foreach (string el in newDateofreceivingList)
            {
                dateofreceivingComboBox.Items.Add(el);
            }
        }
        private void InitializeRadioButton()
        {
            DomRadioButton.Checked = true;
        }

        public Form1()
        {
            InitializeComponent();
            InitializeRadioButton();
            dataBase dataBaseClass = (dataBase)new XmlSerializer(typeof(dataBase)).Deserialize(new StreamReader(filePath));
            this.FormClosing += Form1_FormClosing; 
            if (dataBaseClass != null && dataBaseClass.faculty != null)
            {
                InitializeComboBox(dataBaseClass);
            }
            else
            {
                MessageBox.Show("Помилка завантаження бази даних.");
            }
            InitializeDataGridView();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Ви дійсно хочете вийти?", "Підтвердження виходу", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true; 
            }
        }
        
        // Restriction methods 
        private bool[] checkBoxCheck()
        {
            var checkBoxes = new bool[checkBoxCount];
            checkBoxes[0] = checkBoxFaculty.Checked ? true : false;
            checkBoxes[1] = checkBoxChair.Checked ? true : false;
            checkBoxes[2] = checkBoxName.Checked ? true : false;
            checkBoxes[3] = checkBoxSurname.Checked ? true : false;
            checkBoxes[4] = checkBoxDegree.Checked ? true : false;
            checkBoxes[5] = checkBoxDateofreceiving.Checked ? true : false;
            return checkBoxes;
        }
        private string[] Restriction(bool[] checkBoxes)
        {
            var restrictions = new string[checkBoxCount];
            restrictions[0] = checkBoxes[0] == true ? (facultyComboBox.SelectedItem != null ? facultyComboBox.SelectedItem.ToString() : String.Empty) : string.Empty;
            restrictions[1] = checkBoxes[1] == true ? (chairComboBox.SelectedItem != null ? chairComboBox.SelectedItem.ToString() : String.Empty) : string.Empty;
            restrictions[2] = checkBoxes[2] == true ? (nameComboBox.SelectedItem != null ? nameComboBox.SelectedItem.ToString() : String.Empty) : string.Empty;
            restrictions[3] = checkBoxes[3] == true ? (surnameComboBox.SelectedItem != null ? surnameComboBox.SelectedItem.ToString() : String.Empty) : string.Empty;
            restrictions[4] = checkBoxes[4] == true ? (degreeComboBox.SelectedItem != null ? degreeComboBox.SelectedItem.ToString() : String.Empty) : string.Empty;
            restrictions[5] = checkBoxes[5] == true ? (dateofreceivingComboBox.SelectedItem != null ? dateofreceivingComboBox.SelectedItem.ToString() : String.Empty) : string.Empty;
            return restrictions;
        }

        // Analising (output) methods 
        private void AnaliseFile(string[] restrictions)
        {
            if (File.Exists(filePath))
            {
                IAnalysing Analysing;

                // Визначаю об'єкт Analysing
                if (DomRadioButton.Checked) 
                    Analysing = new XmlDOMAnalysing();
                else if (SaxRadioButton.Checked)
                    Analysing = new XmlSAXAnalysing();
                else
                    Analysing = new XmlLinqAnalysing();

                //Викликаю відповідний метод
                Analysing.AnalisingMethod(restrictions, filePath, dataGridView1);
            }
            else
                MessageBox.Show("FilePath_ERROR");
        }

        // Form's elements 
        private void searchButton_Click(object sender, EventArgs e)
        {
            // Updating our grid
            dataGridView1.Columns.Clear();
            InitializeDataGridView();
            // Cheacking all the restrictions
            var checkBoxes = checkBoxCheck();
            var restrictions = Restriction(checkBoxes);
            
            AnaliseFile(restrictions);
        }
        private void transformToHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            if (File.Exists(filePath))
            {
                xslt.Load("dataBaseXSL.xsl");
                xslt.Transform("dataBase.xml", "dataBase.html");
            }
            else
                MessageBox.Show("FilePath_ERROR");
        }
    }
}