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
INSERT        INTO    stations(ID , LocationAddress , ObservatoryName , LocationByTWD67 , CreateTime)
VALUES          ('{0}' , '{1}' , '{2}' , '{3}' , '{4}')"
, st.ID, st.LocationAddress, st.ObservatoryName, st.LocationByTWD67, st.CreateTime);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
