using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YON.Repository
{
    public class StationRepository
    {
        private const string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UV_DB.mdf;Integrated Security=True";

        //private const string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Work\Web\web_program\work\work03\ConsoleYON\App_Data\UV_DB.mdf;Integrated Security=True";
        public void Create(List<Models.Station> stations) // Models.Station stations
        {
            var connection = new System.Data.SqlClient.SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            foreach (var station in stations)
            {
                var command = new System.Data.SqlClient.SqlCommand("", connection);
                command.CommandText = string.Format(@"
INSERT        INTO    Station(SiteName, UVI, PublishAgency, County, WGS84Lon,WGS84Lat,PublishTime)
VALUES          (N'{0}',{1},N'{2}',N'{3}',N'{4}',N'{5}',N'{6}')
", station.SiteName, station.UVI, station.PublishAgency, station.County, station.WGS84Lon, station.WGS84Lat, station.PublishTime.ToString("yyyy/MM/dd HH:mm"));
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void UpdateLastRecord(Models.Station station)
        {
            var connection = new System.Data.SqlClient.SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
UPDATE [dbo].[Station]
   SET 
       [LastRecordTime] ='{0}'
      ,[LastRecordUVI] ={1}
 WHERE [SiteName] = N'{2}'
", station.LastRecordTime.ToString("yyyy/MM/dd HH:mm"), station.LastRecordUVI, station.SiteName);
            Console.WriteLine(string.Format("{0} = {1}", station.SiteName, station.LastRecordUVI));
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Models.Station station)
        {
            var connection = new System.Data.SqlClient.SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();


            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
UPDATE [dbo].[Station]
   SET 
       [PublishAgency]=N'{0}'
      ,[County]=N'{1}'
      ,[WGS84Lat]=N'{2}'
 WHERE [SiteName] = N'{3}'
", station.PublishAgency, station.County, station.WGS84Lat, station.SiteName);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public List<Models.Station> FindAllSt()
        {
            var result = new List<Models.Station>();
            var connection = new System.Data.SqlClient.SqlConnection(_connectionString);
            connection.Open();
            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = @"Select * from Station";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Models.Station st_item = new Models.Station();
                st_item.SiteName = reader["SiteName"].ToString();
                st_item.UVI = int.Parse(reader["UVI"].ToString());
                st_item.PublishAgency = reader["PublishAgency"].ToString();
                st_item.County = reader["County"].ToString();
                st_item.WGS84Lon = reader["WGS84Lon"].ToString();
                st_item.WGS84Lat = reader["WGS84Lat"].ToString();
                st_item.PublishTime = DateTime.Parse(reader["PublishTime"].ToString());
                if (!string.IsNullOrEmpty(reader["LastRecordTime"].ToString()))
                {
                    st_item.LastRecordTime = DateTime.Parse(reader["LastRecordTime"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["LastRecordUVI"].ToString()))
                {
                    st_item.LastRecordUVI = int.Parse(reader["LastRecordUVI"].ToString());
                }
                result.Add(st_item);
            }
            connection.Close();
            return result;
        }

        public Models.Station FindBySiteName(string st_name)
        {
            Models.Station result = null;
            var connection = new System.Data.SqlClient.SqlConnection(_connectionString);
            connection.Open();
            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
Select * from Station
Where SiteName='{0}'",
st_name);
            var reader = command.ExecuteReader();
            var list = new List<YON.Models.Station>();
            while (reader.Read())
            {
                Models.Station item = new Models.Station();
                item.SiteName = reader["SiteName"].ToString();
                item.UVI = int.Parse(reader["UVI"].ToString());
                item.PublishAgency = reader["PublishAgency"].ToString();
                item.County = reader["County"].ToString();
                item.WGS84Lon = reader["WGS84Lon"].ToString();
                item.WGS84Lat = reader["WGS84Lat"].ToString();
                item.PublishTime = DateTime.Parse(reader["PublishTime"].ToString());
                if (!string.IsNullOrEmpty(reader["LastRecordTime"].ToString()))
                {
                    item.LastRecordTime = DateTime.Parse(reader["LastRecordTime"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["LastRecordWaterLevel"].ToString()))
                {
                    item.LastRecordUVI = int.Parse(reader["LastRecordUVI"].ToString());
                }

                list.Add(item);
            }
            connection.Close();
            if (list.Count == 1)
                result = list.Single();

            return result;
        }
    }
}
