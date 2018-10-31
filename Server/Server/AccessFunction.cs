using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Server
{
    public static class AccessFunction
    {
        private static OleDbConnection dbConnection;
        #region open&close
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
                return;
            }
        }

        public static void AccessCloseAll()
        {
            AccessFunction.dbConnection.Close();
        }
        #endregion

        #region GetRoom
        public static DataTable GetTables()
        {
            string selectCommandText = "SELECT * FROM Table_room";
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            DataTable dataTable = new DataTable();
            oleDbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public static DataTable GetHistoryTables(string tablename)
        {
            string selectCommandText;
            if (tablename.Equals("history_table_0"))
                selectCommandText = "SELECT * FROM history_table_0";
            else if (tablename.Equals("history_table_1"))
                selectCommandText = "SELECT * FROM history_table_1";
            else if (tablename.Equals("history_table_2"))
                selectCommandText = "SELECT * FROM history_table_2";
            else
                return null;
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            DataTable dataTable = new DataTable();
            oleDbDataAdapter.Fill(dataTable);
            return dataTable;
        }
        //private static object privateObjectLock = new object();
        public static DataTable GetClientProtocol(string roomname)
        {
            //lock (privateObjectLock)
            {
                string selectCommandText = "SELECT * FROM Table_room WHERE ID_room='" + roomname + "'";
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
                DataTable dataTable = new DataTable();
                oleDbDataAdapter.Fill(dataTable);
                return dataTable;
            }
        }

        public static DataTable GetTerminalProtocol(string roomname)
        {
            string selectCommandText = "SELECT * FROM Reservatel_room WHERE ID_room='" + roomname + "'";
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            DataTable dataTable = new DataTable();
            oleDbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public static DataTable GetClientHistory(string roomname,string page,string tablename)
        {
            string selectCommandText = "SELECT * FROM " + tablename + " WHERE room_name ='" + roomname + "' AND room_page = '" + page + "'"; ;
            /*
            if (tablename.Equals("history_table_0"))
                selectCommandText = "SELECT * FROM history_table_0 WHERE room_name ='" + roomname + "' AND room_page = '" + page + "'";
            else if (tablename.Equals("history_table_1"))
                selectCommandText = "SELECT * FROM history_table_1 WHERE room_name ='" + roomname + "' AND room_page = '" + page + "'";
            else if (tablename.Equals("history_table_2"))
                selectCommandText = "SELECT * FROM history_table_2 WHERE room_name ='" + roomname + "' AND room_page = '" + page + "'";
            else
                return null;
             */ 
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            DataTable dataTable = new DataTable();
            oleDbDataAdapter.Fill(dataTable);
            return dataTable;
        }
        #endregion

        #region DeleteRoom
        public static void DeleteClientProtocol(string roomname)
        {
            string selectCommandText = "SELECT * FROM Table_room WHERE ID_room ='" + roomname + "'";
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

        public static void DeleteTerminalProtocol(string roomname,string stime,string etime)
        {
            string selectCommandText = "SELECT * FROM Reservatel_room WHERE ID_room ='" + roomname + "' AND room_startime = '" + stime + "'";
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

        public static void DeleteClientHistory(string datetime,string tablename)
        {
            string selectCommandText = selectCommandText = "SELECT * FROM " + tablename + " WHERE room_datetime='" + datetime + "'"; ;
            /*
            if (tablename.Equals("history_table_0"))
                selectCommandText = "SELECT * FROM history_table_0 WHERE room_datetime='" + datetime + "'";
            else if (tablename.Equals("history_table_1"))
                selectCommandText = "SELECT * FROM history_table_1 WHERE room_datetime='" + datetime + "'";
            else if (tablename.Equals("history_table_2"))
                selectCommandText = "SELECT * FROM history_table_2 WHERE room_datetime='" + datetime + "'";
            else
                return;
             */ 
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
        #endregion

        #region Insert
        public static void InsertClientProtocol(string[] str,string tablename)
        {
            string selectCommandText = "SELECT * FROM " + tablename;
            OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, AccessFunction.dbConnection);
            OleDbCommandBuilder oleDbCommandBuilder = new OleDbCommandBuilder(oleDbDataAdapter);
            DataSet dataSet = new DataSet();
            oleDbDataAdapter.Fill(dataSet);
            OleDbCommandBuilder oleDbCommandBuilder2 = new OleDbCommandBuilder(oleDbDataAdapter);
            oleDbDataAdapter.UpdateCommand = oleDbCommandBuilder2.GetUpdateCommand();
            int count = dataSet.Tables[0].Columns.Count;
            if (tablename != "Table_room")
                count = count - 1;
            DataRow dataRow = dataSet.Tables[0].NewRow();
            for (int i = 0; i < count; i++)
            {
                dataRow[i] = str[i];
            }
            dataSet.Tables[0].Rows.Add(dataRow);
            oleDbDataAdapter.Update(dataSet);
        }
        #endregion
    }
}
