using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work_0317_1
{
    class db_repository
    {
        private const string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=water;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public void CreateStation(station st)
        {
            var connection = new System.Data.SqlClient.SqlConnection(_connectionString);
            connection.Open();
            var command = new System.Data.SqlClient.SqlCommand("", connection);

            command.CommandText = string.Format(@"
INSERT        INTO    stations(SiteName , UVI , PublishAgency , County , WGS84Lon,WGS84Lat,PublishTime)
VALUES          (N'{0}' , N'{1}' ,N'{2}' , N'{3}' , N'{4}', N'{5}', N'{6}')"
, st.SiteName, st.UVI, st.PublishAgency, st.County, st.WGS84Lon, st.WGS84Lat, st.PublishTime);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
