using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Client
{
    public static class AccessFunction
    {
        private static OleDbConnection dbConnection;

        public static void AccessOpen()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\DataRoom.mdb";
            AccessFunction.dbConnection = new OleDbConnection(connectionString);
            try
            {
                AccessFunction.dbConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void AccessCloseAll()
        {
            AccessFunction.dbConnection.Close();
        }

        public static DataTable GetTables()
        {
            string selectCommandText = "SELECT * FROM room_table";
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            DataTable dataTable = new DataTable();
            oleDbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public static DataTable GetClientProtocol(string roomid)
        {
            string selectCommandText = "SELECT * FROM room_table WHERE room_ID='" + roomid + "'";
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            DataTable dataTable = new DataTable();
            oleDbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public static DataTable SearchClientProtocol(string roomid ,string who)
        {
            string selectCommandText = "SELECT * FROM room_table WHERE room_ID ='" + roomid + "' AND room_customer = '" + who + "'";
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            DataTable dataTable = new DataTable();
            oleDbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public static void DeleteClientProtocol(string roomid)
        {
            string selectCommandText = "SELECT * FROM room_table WHERE room_ID ='" + roomid + "'";
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            OleDbCommandBuilder oleDbCommandBuilder = new OleDbCommandBuilder(oleDbDataAdapter);
            DataTable dataTable = new DataTable();
            if (oleDbDataAdapter.Fill(dataTable) > 0)
            {
                dataTable.Rows[0].Delete();
                oleDbDataAdapter.DeleteCommand = oleDbCommandBuilder.GetDeleteCommand();
                oleDbDataAdapter.Update(dataTable);
            }
        }

        public static void DeleteTerminalProtocol(string roomname, string stime)
        {
            string selectCommandText = "SELECT * FROM room_table WHERE ID_room ='" + roomname + "' AND room_customer = '" + stime + "'";
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            OleDbCommandBuilder oleDbCommandBuilder = new OleDbCommandBuilder(oleDbDataAdapter);
            DataTable dataTable = new DataTable();
            //oleDbDataAdapter.Fill(dataTable);
            if (oleDbDataAdapter.Fill(dataTable) > 0)
            {
                dataTable.Rows[0].Delete();
                oleDbDataAdapter.DeleteCommand = oleDbCommandBuilder.GetDeleteCommand();
                oleDbDataAdapter.Update(dataTable);
            }
        }


        public static void InsertClientProtocol(string[] str)
        {
            string selectCommandText = "SELECT * FROM room_table";
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            OleDbCommandBuilder oleDbCommandBuilder = new OleDbCommandBuilder(oleDbDataAdapter);
            DataSet dataSet = new DataSet();
            oleDbDataAdapter.Fill(dataSet);
            OleDbCommandBuilder oleDbCommandBuilder2 = new OleDbCommandBuilder(oleDbDataAdapter);
            oleDbDataAdapter.UpdateCommand = oleDbCommandBuilder2.GetUpdateCommand();
            int count = dataSet.Tables[0].Columns.Count;
            DataRow dataRow = dataSet.Tables[0].NewRow();
            for (int i = 0; i < count - 1; i++)
            {
                dataRow[i] = str[i];
            }
            dataSet.Tables[0].Rows.Add(dataRow);
            oleDbDataAdapter.Update(dataSet);
        }
    }
}
