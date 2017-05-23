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
        public void Create(Models.Station stations)
        {
            var connection = new System.Data.SqlClient.SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
INSERT        INTO    Station(SiteName, UVI, PublishAgency, County, WGS84Lon,WGS84Lat,PublishTime)
VALUES          (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}')
", stations.SiteName, stations.UVI, stations.PublishAgency, stations.County, stations.WGS84Lon, stations.WGS84Lat, stations.PublishTime);
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
                st_item.UVI = reader["UVI"].ToString();
                st_item.PublishAgency = reader["PublishAgency"].ToString();
                st_item.County = reader["County"].ToString();
                st_item.WGS84Lon = reader["WGS84Lon"].ToString();
                st_item.WGS84Lat = reader["WGS84Lat"].ToString();
                st_item.PublishTime = DateTime.Parse(reader["PublishTime"].ToString());
                result.Add(st_item);
            }
            connection.Close();
            return result;
        }
    }
}
