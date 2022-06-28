using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL
{
  public class ReadClass : IReadClass
  {
    const string connectionString = "server=g708915-w101;database=Fithub;Trusted_Connection=true";
    public IEnumerable<Class> ReadClasses()
    {
      try
      {
        using(SqlConnection sqlConnection=new SqlConnection(connectionString))
        {
          sqlConnection.Open();
          string sqlQuery = "Select ClassID, ClassName,ClassDescription,PhotoURL,classShortDescription from [dbo].[Class]";
          SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
          var result = sqlCommand.ExecuteReader();
          List<Class> classes = new List<Class>();
          Class objClass = null;
          if (result!=null)
          {
            while (result.Read())
            {
              objClass = new Class();
              objClass.ClassID = Convert.ToInt32(result["ClassID"]);
              objClass.ClassName = result["ClassName"].ToString();
              objClass.ClassDescription = result["ClassDescription"].ToString();
              objClass.ClassShortDescription = result["ClassShortDescription"].ToString();
              objClass.PhotoURL = result["PhotoURL"].ToString();
              classes.Add(objClass);
            } 
          }

          return classes;
        }
      }
      catch (Exception e)
      {

        throw;
      }
    }
  }
}
