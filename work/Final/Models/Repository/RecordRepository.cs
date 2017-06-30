using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YON.Repository
{
    public class RecordRepository
    {
        private const string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UV_DB.mdf;Integrated Security=True";
        public void Create(List<Models.Record> rds)
        {
            var connection = new System.Data.SqlClient.SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            foreach (var rd in rds)
            {
                var command = new System.Data.SqlClient.SqlCommand("", connection);
                command.CommandText = string.Format(@"
INSERT        INTO    Record(SiteName,UVI,PublishTime)
VALUES          (N'{0}',{1},N'{2}')", rd.SiteName, rd.UVI, rd.PublishTime.ToString("yyyy/MM/dd HH:mm"));
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public bool isExist(Models.Record rd)
        {
            var id = rd.SiteName;
            var date_time = rd.PublishTime;
            var connection = new System.Data.SqlClient.SqlConnection();
            connection.ConnectionString = _connectionString;
            //connection.Open();
            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
select count(*) from Record
where SiteName=N'{0}' and PublishTime='{1}' 
", rd.SiteName, rd.PublishTime.ToString("yyyy/MM/dd HH:mm"));
            connection.Open();
            int count_Result = int.Parse(command.ExecuteScalar().ToString());
            //command.ExecuteNonQuery();
            connection.Close();
            return count_Result > 0;
        }

        public List<Models.Record> FindBySiteName(string SiteName)
        {
            var result = new List<Models.Record>();
            var connection = new System.Data.SqlClient.SqlConnection(_connectionString);
            connection.Open();
            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
Select * from Record
Where SiteName=N'{0}'",
SiteName
);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Models.Record item = new Models.Record();
                item.ID = int.Parse(reader["ID"].ToString());
                item.SiteName = (reader["SiteName"].ToString());
                if (!string.IsNullOrEmpty(reader["UVI"].ToString()))
                {
                    item.UVI = int.Parse(reader["UVI"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["PublishTime"].ToString()))
                {
                    item.PublishTime = DateTime.Parse(reader["PublishTime"].ToString());
                }
                result.Add(item);
            }
            connection.Close();
            return result;
        }
    }
}
