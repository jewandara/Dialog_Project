using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;
//using OfficeOpenXml;

namespace DIALOGGSM
{
    public partial class DialogExcelForm : Form
    {
        public DialogExcelForm()
        {
            InitializeComponent();
        }

        private void DialogExcelForm_Load(object sender, EventArgs e)
        {

        }


        String savePath;
        //DialogWaitForm alert;

        private void button1_Click(object sender, EventArgs e)
        {
            savePath = "";
            try
            {
                if (savePath == "")
                {
                    string dummyFileName = "Dialog";
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.FileName = dummyFileName;
                    sf.Filter = "Microsoft Office xls (*.xls*)|*.xls*";
                    if (sf.ShowDialog() == DialogResult.OK)
                    { savePath = sf.FileName + ".xls"; }
                }
                if (!File.Exists(savePath)) { 
                    //saveExcel(savePath); 
                    if (backgroundWorker1.IsBusy != true)
                    {
                        backgroundWorker1.RunWorkerAsync();
                    }
                }
                else 
                {
                    if (backgroundWorker1.IsBusy != true)
                    {
                        backgroundWorker1.RunWorkerAsync();
                    }
                    //MessageBox.Show("The file path " + savePath + " already exists. Rename the saving file and try again.", "File already exists", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                    //return; 
                }
            }
            catch (Exception) { }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //try
            //{
                BackgroundWorker worker = sender as BackgroundWorker;
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                //int presentage = 0;
                for (int i = 1; i <= 100; i++)
                {
                    for (int j = 1; j <= 100; j++)
                    {
                        xlWorkSheet.Cells[i, j] = "test : " + i.ToString() + "," + j.ToString();

                    }
                }
                xlWorkBook.SaveAs(savePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                MessageBox.Show("The Excel file created successfully", "Saving Excel File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                releaseObject(xlApp);
            //}
            //catch (Exception) { MessageBox.Show("The file path " + savePath + " already exists. Rename the saving file and try again.", "File already exists", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
























        private void saveExcel(String savePath) 
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                xlWorkSheet.Cells[1, 1] = "File Name";
                xlWorkSheet.Cells[1, 1] = "File Name";
                xlWorkSheet.Cells[1, 2] = "Sheet 1 content";
                xlWorkSheet.Cells[1, 3] = "Sheet 1 content";
                xlWorkSheet.Cells[1, 4] = "Sheet 1 content";
                xlWorkSheet.Cells[2, 1] = "Content";
                xlWorkSheet.Cells[2, 2] = "Sheet 1 content";
                xlWorkSheet.Cells[2, 3] = "Sheet 1 content";
                xlWorkSheet.Cells[2, 4] = "Sheet 1 content";
                xlWorkSheet.Cells[3, 1] = "Date";
                xlWorkSheet.Cells[3, 2] = "Sheet 1 content";
                xlWorkSheet.Cells[3, 3] = "Sheet 1 content";
                xlWorkSheet.Cells[3, 4] = "Sheet 1 content";


                xlWorkBook.SaveAs(savePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                MessageBox.Show("The Excel file created successfully", "Saving Excel File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                releaseObject(xlApp);
            }
            catch (Exception) { MessageBox.Show("The file path " + savePath + " already exists. Rename the saving file and try again.", "File already exists", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
        }







        private void button2_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                }
                catch (Exception) { }
            }
            label2.Text = size.ToString();
            label1.Text = result.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String path = "";
            String TextFileDta = "hellowwwwwwwwwwwwwww";
            try
            {
                if (path == "")
                {
                    string dummyFileName = "Dialog";
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.FileName = dummyFileName;
                    sf.Filter = "Google Earth Kml (*.kml*)|*.kml*";

                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        path = sf.FileName + ".kml";
                    }
                }
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        tw.Close();
                    }
                    using (StreamWriter outfile = new StreamWriter(path))
                    {
                        outfile.Write(TextFileDta);
                        MessageBox.Show("The kml file created successfully", "Google Kml File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    var result = MessageBox.Show("The file path " + path + " already exists. Are you sure you want to save this ?", "File already exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        using (StreamWriter outfile = new StreamWriter(path))
                        {
                            outfile.Write(TextFileDta);
                            MessageBox.Show("The kml file created successfully", "Google Kml File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                label1.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                label1.Text = "Error: " + e.Error.Message;
            }
            else
            {
                label1.Text = "Done!";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
   
            //Create an Emplyee DataTable
            System.Data.DataTable employeeTable = new System.Data.DataTable("Employee");
            employeeTable.Columns.Add("Employee ID");
            employeeTable.Columns.Add("Employee Name");
            employeeTable.Rows.Add("1", "ABC");
            employeeTable.Rows.Add("2", "DEF");
            employeeTable.Rows.Add("3", "PQR");
            employeeTable.Rows.Add("4", "XYZ");

            //Create a Department Table
            System.Data.DataTable departmentTable = new System.Data.DataTable("Department");
            departmentTable.Columns.Add("Department ID");
            departmentTable.Columns.Add("Department Name");
            departmentTable.Rows.Add("1", "IT");
            departmentTable.Rows.Add("2", "HR");
            departmentTable.Rows.Add("3", "Finance");

            //Create a DataSet with the existing DataTables
            System.Data.DataSet ds = new System.Data.DataSet("Organization");
            ds.Tables.Add(employeeTable);
            ds.Tables.Add(departmentTable);

            ExportDataSetToExcel(ds);
        }


        private void ExportDataSetToExcel(DataSet ds)
        {
            //Creae an Excel application instance
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            //Create an Excel workbook instance and open it from the predefined location
            Microsoft.Office.Interop.Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(@"D:\test.xls");

            foreach (System.Data.DataTable table in ds.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name
                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }
            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //try
            //{

  

            System.Data.OleDb.OleDbConnection MyConnection;
            System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
            MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='D:\\test.xls';Extended Properties=Excel 8.0;");
            MyConnection.Open();
            myCommand.Connection = MyConnection;

            myCommand.CommandText = "INSERT INTO [Department$A1:C1] VALUES ('','','uuuuuuuuuuu')";
            myCommand.ExecuteNonQuery();
            myCommand.CommandText = "INSERT INTO [Department$B7:D7] VALUES ('','sdfgsdfs','uuuuuuuuuuu')";
            myCommand.ExecuteNonQuery();

            MyConnection.Close();
 







            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }





    }
}
