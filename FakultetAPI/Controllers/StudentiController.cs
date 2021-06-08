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

        [Route("GET/{idStudenta}")]
        [HttpGet]
        public Student CitanjePojedinacno(int idStudenta)
        {
            SqlCommand command = new SqlCommand("getStudentById", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@StudentId", SqlDbType.Int).Value = idStudenta;

            Student student = new Student();

            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    student.PKStudentId = Convert.ToInt32(reader[0]);
                    student.Ime = Convert.ToString(reader[1]);
                    student.Prezime = Convert.ToString(reader[2]);
                    student.BrIndexa = Convert.ToInt32(reader[3]);
                }

                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return student;
        }

        [Route("POST")]
        [HttpPost]
        public void Unos(Student s)
        {

            SqlCommand command = new SqlCommand("insertIntoStudenti", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Ime", SqlDbType.VarChar).Value = s.Ime;
            command.Parameters.Add("@Prezime", SqlDbType.VarChar).Value = s.Prezime;
            command.Parameters.Add("@Broj", SqlDbType.Int).Value = s.BrIndexa;

            try
            {
                db.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            db.Close();
        }

        [Route("PUT")]
        [HttpPut]
        public void Izmjena(Student s)
        {
            SqlCommand command = new SqlCommand("updateToStudenti", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Ime", SqlDbType.VarChar).Value = s.Ime;
            command.Parameters.Add("@Prezime", SqlDbType.VarChar).Value = s.Prezime;
            command.Parameters.Add("@Broj", SqlDbType.Int).Value = s.BrIndexa;
            command.Parameters.Add("@IdStudenta", SqlDbType.Int).Value = s.PKStudentId;


            try
            {
                db.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            db.Close();
        }

        [Route("DELETE/{idStudenta}")]
        [HttpDelete]
        public void Brisanje(int idStudenta)
        {
            SqlCommand command = new SqlCommand("deleteFromStudenti", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IdStudenta", SqlDbType.Int).Value = idStudenta;

            try
            {
                db.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            db.Close();
        }
    }
}