﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace AmenityExpress
{
    internal class DBConnector
    {

        private static string connstr;
        private static OracleConnection conn;
        private static OracleCommand cmd = new OracleCommand();
        public DBConnector()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var settings = config.AppSettings.Settings;
            connstr = "data source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = " + settings["host"].Value + ")(PORT = " + settings["port"].Value + ")))(CONNECT_DATA =(SERVICE_NAME = " + settings["sid"].Value + ")));USER ID=" + settings["id"].Value + ";PASSWORD=" + settings["pwd"].Value + ";";
            conn = new OracleConnection(connstr);
        }

        public static void DML_NON_QUERY(string sql) //insert, delete, update
        {
            cmd.Connection = conn;
            cmd.CommandText = sql;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery(); //반환값 x ==>void
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }



        }
        public static DataSet DML_QUERY(string sql)
        {
            cmd.Connection = conn;
            cmd.CommandText = sql;
            DataSet ds = new DataSet();

            try
            {
                conn.Open();
                using (OracleDataAdapter reader = new OracleDataAdapter(cmd))
                {
                    reader.Fill(ds);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
    }

}
