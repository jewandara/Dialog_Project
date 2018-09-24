using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Security;


 namespace MAXFORD.Max_Account.App_Config{

     //----------   PRIVITE CLASS SQLMAXSEVER   ----------//
     class SQLMAXSever
     {
         //----------   PRIVITE CLASS SQLMAXSEVER - error message   ----------//
         private string SqlMaxError;

         //----------   PRIVITE CLASS SQLMAXSEVER - call stored procedure   ----------//
         private void MaxSeverSqlInsertData(object[] MaxObjects, string CommandString)
         {
             string connetionString = null;
             SqlConnection cnn ;
             connetionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\ZionxMaxDatabaseNet16.mdf;Integrated Security=True;User Instance=True";
             cnn = new SqlConnection(connetionString);
             SqlCommand SQLCmd;
             int a = MaxObjects.Length;
             object[] abc = new object[a];

             try { 
                 cnn.Open();
                 SqlCommand MaxCommand = new SqlCommand();
                 MaxCommand.Connection = cnn;
                 MaxCommand.CommandText = CommandString;
                 MaxCommand.CommandType = CommandType.StoredProcedure;
                 SqlCommandBuilder.DeriveParameters(MaxCommand);
                     for (int i = 0; i < MaxCommand.Parameters.Count - 1; i++)
                     {
                         abc[i] = MaxCommand.Parameters[i + 1].ParameterName.ToString();
                     }
                     SQLCmd = new SqlCommand(CommandString, cnn);
                     for (int i = 0; i < MaxObjects.Length; i++)
                     {
                         SqlParameter MaxSqlPerameter = new SqlParameter();
                         MaxSqlPerameter.ParameterName = abc[i].ToString();
                         MaxSqlPerameter.Value = MaxObjects[i];
                         SQLCmd.Parameters.Add(MaxSqlPerameter);
                     }
                     SQLCmd.CommandType = CommandType.StoredProcedure;
                     SQLCmd.ExecuteNonQuery();
                     SqlMaxError = "Good";
                     cnn.Close();
             }
             //----------   PRIVITE CLASS SQLMAXSEVER - catch error   ----------//
             catch (Exception ex)
             {
                 SqlMaxError = "SQL Error ( Data Insert ) - " + ex;
             }
         }


         //----------   PRIVITE CLASS SQLMAXSEVER - public insert data function    ----------//
         public string MaxInsertData(object[] MaxObjects, string CommandString)
         {
             MaxSeverSqlInsertData(MaxObjects, CommandString);
             return SqlMaxError;
         }







         //----------   PRIVITE CLASS SQLMAXSEVER - call select or view   ----------//
         private DataSet MaxSeverSqlViewData(string MaxSQLCommand)
         {
             string connetionString = null;
             SqlConnection cnn;
             connetionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\ZionxMaxDatabaseNet16.mdf;Integrated Security=True;User Instance=True";
             cnn = new SqlConnection(connetionString);
             SqlDataAdapter da = new SqlDataAdapter();
             DataSet MaxDS = new DataSet();

             try
             {
                 cnn.Open();
                 //DataTable dt = new DataTable();
                 da.SelectCommand = new SqlCommand(MaxSQLCommand, cnn);
                 da.Fill(MaxDS, "MP");
                    //dt = MaxDS.Tables["MP"];
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    abc[dr] = dr["MPageBody"].ToString();
                    //}
                    //NewDSet = ActDataBaseText("dbo.SP_ACTBANK_ViewComboBoxBranch")
                    //   object[] abc;
                    //foreach (DataTable dtt in dt.TableName)
                    //{
                    // abc[] = dr["MPageBody"].ToString();
                    //}
                    //vb
                    //For Each row As DataRow In NewDSet.Tables(0).Rows()
                    //    ComboBox6.Items.Add(row.Item(0))
                    //Next row
                 cnn.Close();
             }
             //----------   PRIVITE CLASS SQLMAXSEVER - catch error   ----------//
             catch (Exception ex)
             {
                 SqlMaxError = "SQL Error ( Data View ) - " + ex;
             }
             return MaxDS;
         }


         //----------   PRIVITE CLASS SQLMAXSEVER - public view data function    ----------//
         public DataSet MaxViewData(string MaxSQLCommand)
         {
             DataSet MaxDS = new DataSet();
             MaxDS = MaxSeverSqlViewData(MaxSQLCommand);
             return MaxDS;
         }



     }










        //----------   PUBLIC CLASS CONFIG   ----------//
        public class config {

            SQLMAXSever MaConection = new SQLMAXSever();
            DataSet MaxDS = new DataSet();
            string ErrorM = "";

            //----------   PUBLIC CLASS CONFIG - insert data   ----------//
            public string MaxInsert(object[] MaxObjects, string CommandString)
            {
                ErrorM = MaConection.MaxInsertData(MaxObjects, CommandString);
                return ErrorM;
            }

            //----------   PUBLIC CLASS CONFIG - view data   ----------//
            public DataSet MaxView(string MaxSQLCommandx)
            {
                MaxDS = MaConection.MaxViewData(MaxSQLCommandx);
                return MaxDS;
            }
        }










}
