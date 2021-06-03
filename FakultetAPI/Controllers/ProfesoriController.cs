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
    [RoutePrefix("api/Profesori")]
    public class ProfesoriController : ApiController
    {
        string connectionString;
        SqlConnection db;

        public ProfesoriController()
        {
            connectionString = Connection.conStr;
            db = new SqlConnection(connectionString);
        }


        [Route("GET")]
        [HttpGet]
        public List<Profesor> Citanje()
        {
            SqlCommand command = new SqlCommand("getAllFromProfesori", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            List<Profesor> pList = new List<Profesor>();
            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Profesor prof = new Profesor();
                    prof.PKProfesorId = Convert.ToInt32(reader[0]);
                    prof.Ime = Convert.ToString(reader[1]);
                    prof.Prezime = Convert.ToString(reader[2]);
                    prof.GodRodjenja = Convert.ToInt32(reader[3]);
                    prof.Titula = Convert.ToString(reader[4]);

                    pList.Add(prof);
                }
                for (int i = 0; i < pList.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + pList[i].ToString());
                }
                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            
            return pList;
        }

        [Route("GET/{idProfesora}")]
        [HttpGet]
        public Profesor CitanjePojedinacno(int idProfesora)
        {
            SqlCommand command = new SqlCommand("getProfesorById", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@ProfesorId", SqlDbType.Int).Value = idProfesora;

            Profesor prof = new Profesor();

            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prof.PKProfesorId = Convert.ToInt32(reader[0]);
                    prof.Ime = Convert.ToString(reader[1]);
                    prof.Prezime = Convert.ToString(reader[2]);
                    prof.GodRodjenja = Convert.ToInt32(reader[3]);
                    prof.Titula = Convert.ToString(reader[4]);
                }

                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return prof;
        }

        [Route("POST")]
        [HttpPost]
        public void Unos(Profesor p)
        {

            SqlCommand command = new SqlCommand("insertIntoProfesori", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Ime", SqlDbType.VarChar).Value = p.Ime;
            command.Parameters.Add("@Prezime", SqlDbType.VarChar).Value = p.Prezime;
            command.Parameters.Add("@Godina", SqlDbType.Int).Value = p.GodRodjenja;
            command.Parameters.Add("@Titula", SqlDbType.VarChar).Value = p.Titula;

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
        public void Izmjena(Profesor p)
        {
            SqlCommand command = new SqlCommand("updateProfesori", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Ime", SqlDbType.VarChar).Value = p.Ime;
            command.Parameters.Add("@Prezime", SqlDbType.VarChar).Value = p.Prezime;
            command.Parameters.Add("@Godina", SqlDbType.Int).Value = p.GodRodjenja;
            command.Parameters.Add("@Titula", SqlDbType.VarChar).Value = p.Titula;
            command.Parameters.Add("@IdProfesora", SqlDbType.Int).Value = p.PKProfesorId;


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

        [Route("DELETE/{idProfesora}")]
        [HttpDelete]
        public void Brisanje(int idProfesora)
        {
            SqlCommand command = new SqlCommand("deleteFromProfesori", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IdProfesora", SqlDbType.Int).Value = idProfesora;

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