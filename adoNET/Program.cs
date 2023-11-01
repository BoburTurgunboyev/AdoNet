using Sharprompt;
using System.Data.SqlClient;

namespace adoNET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContextAdoNet.GetByGeneric("StudyCenterDB","userTable","where userId = 1;");

            DataContextAdoNet.InsertGeneric("StudyCenterDB","Persons", "('Bahriddin','Abdusalomov',17,1),('Temurbek','Abdurashidov',17,2)");           

            DataContextAdoNet.DeleteGeneric("AdoNET", "userTable", "where UserId = 2003");
        }
    }
}