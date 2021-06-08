using FakultetAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FakultetAPI.Controllers
{
        [RoutePrefix("api/Studenti")]
    public class StudentiController : ApiController
    {
            string connectionString;
            SqlConnection db;

            public StudentiController()
            {
                connectionString = Connection.conStr;
                db = new SqlConnection(connectionString);
            }


            [Route("GET")]
            [HttpGet]
            public List<Student> Citanje()
            {
                SqlCommand command = new SqlCommand("getAllFromStudenti", db)
                {
                    CommandType = CommandType.StoredProcedure
                };

                List<Student> sList = new List<Student>();
                try
                {
                    db.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Student student = new Student();
                        student.PKStudentId = Convert.ToInt32(reader[0]);
                        student.Ime = Convert.ToString(reader[1]);
                        student.Prezime = Convert.ToString(reader[2]);
                        student.BrIndexa = Convert.ToInt32(reader[3]);
                       
                        sList.Add(student);
                    }
                    reader.Close();
                    db.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

                return sList;
            }
        }
}