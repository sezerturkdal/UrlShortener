using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using UrlShortener.Data.Interface;
using System.Collections.Generic;
using AutoMapper;
using System.Reflection;

namespace UrlShortener.Data
{
    public class SQLiteDB : ISQLiteDB
    {
        SQLiteConnection con;
        SQLiteDataAdapter da;
        SQLiteCommand cmd;
        DataSet ds;

        public SQLiteDB()
        {
            CreateDb();
            con=new SQLiteConnection("Data Source=UrlDatabase.sqlite;Version=3;");
        }

        private void CreateDb()
        {
            if (!File.Exists("UrlDatabase.sqlite"))
            {
                SQLiteConnection.CreateFile("UrlDatabase.sqlite");

                string sql = @"CREATE TABLE URL(
                               Id INTEGER PRIMARY KEY AUTOINCREMENT ,
                               OriginalUrl         TEXT      NOT NULL,
                               ShortUrl            TEXT      NOT NULL,
                               CreatedDate         TEXT      NOT NULL
                            );";
                con = new SQLiteConnection("Data Source=UrlDatabase.sqlite;Version=3;");
                con.Open();
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public string GetOriginalURL(string url)
        {
            try
            {
                da = new SQLiteDataAdapter("select * from URL where ShortUrl='" + url + "' ", con);
                ds = new DataSet();
                con.Open();
                da.Fill(ds, "URL");
                var dt = ds.Tables["URL"];
                con.Close();

                List<UrlModel> response = new List<UrlModel>();
                foreach (DataRow row in dt.Rows)
                {
                    UrlModel model = CreateItemFromRow<UrlModel>(row);
                    response.Add(model);
                }

                return response?.First().OriginalUrl;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public bool SaveURL(string originalUrl,string shortUrl)
        {
            try
            {
                cmd = new SQLiteCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into URL(OriginalUrl,ShortUrl,CreatedDate) values ('" + originalUrl + "','" + shortUrl + "','" + DateTime.Now + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }catch(Exception ex)
            {
                throw ex;
            }
          
        }

        public bool CheckIsExists(string customUrl)
        {
            try
            {
                da = new SQLiteDataAdapter("select * from URL where ShortUrl='"+ customUrl + "' ", con);
                ds = new DataSet();
                con.Open();
                da.Fill(ds, "URL");
                var dt = ds.Tables["URL"];
                con.Close();

                List<UrlModel> response = new List<UrlModel>();
                foreach (DataRow row in dt.Rows)
                {
                    UrlModel model = CreateItemFromRow<UrlModel>(row);
                    response.Add(model);
                }
                return response.Any();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // function that creates an object from the given data row
        public static T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }

        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                // if exists, set the value
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }
    }
}
