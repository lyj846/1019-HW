using _1019.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace _1019_hw.Repository
{
    class OpenDataRepository
    {
        public string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\lyj_nb\Desktop\1019 hw\1019_hw\1019_hw\Repository\database.mdf;Integrated Security=True";
                       
            }
            

            set => throw new NotImplementedException();
        }


public void Insert(OpenData item)
{
    var newItem = item;
    var connection = new SqlConnection(ConnectionString);
    connection.Open();

    var command = new SqlCommand("", connection);
    command.CommandText = string.Format(@"INSERT INTO OpenData(資料年度,統計項目,稅目別,資料單位,值)
                                                  VALUES              (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}')
                                                 ", newItem.資料年度, newItem.統計項目, newItem.稅目別, newItem.資料單位, newItem.值);

    command.ExecuteNonQuery();
    connection.Close();
}
    }
}
