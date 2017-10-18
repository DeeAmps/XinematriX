using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using XinematriX.Models.Models;

namespace XinematriX.DataAccess.DBHelper
{
    public class DBHelper
    {
        private static string _conString;

        public DBHelper()
        {
            _conString = "Data Source=xinematrix.database.windows.net;Initial Catalog=XinematriX;User id=Dee;Password=H3ll0w0rld123";
        }

        public  async Task<List<VirtualMoviePolls>> GetMoviePolls()
        {
            try
            {
                using (var con = new SqlConnection(_conString))
                {
                    var results = new List<VirtualMoviePolls>();
                    double sumVotes = 0;
                    var cmd = new SqlCommand("\"GetPolls\"", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    await con.OpenAsync();
                    var reader = cmd.ExecuteReaderAsync().Result;

                    while (await reader.ReadAsync())
                    {
                        results.Add(new VirtualMoviePolls()
                        {
                            MoviePollsId = await reader.GetFieldValueAsync<int>(0),
                            PollQuestion = await reader.GetFieldValueAsync<string>(1),
                            PollOptions = await reader.GetFieldValueAsync<string>(2),
                            Votes = await reader.GetFieldValueAsync<int>(3),
                        });
                    }
                    con.Close();
                    foreach (var item in results)
                    {
                        sumVotes += item.Votes;
                    }
                    foreach (var item in results)
                    {
                        var sum = Convert.ToDouble((item.Votes / sumVotes) * 100);
                        item.PercentageVote = Convert.ToInt32(sum);
                    }
                    return results;
                }
            }
            catch (Exception x)
            {
                throw x;   
            }
            return new List<VirtualMoviePolls>();
        }
    }
}
