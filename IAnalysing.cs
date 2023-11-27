using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoSystem
{
    interface IAnalysing
    {
        void AnalisingMethod(string[] restrictions, string filePath, DataGridView dataGridView1);
    }
}