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
    [RoutePrefix("api/Predmeti")]
    public class PredmetiController : ApiController
    {
        string connectionString;
        SqlConnection db;

        public PredmetiController()
        {
            connectionString = Connection.conStr;
            db = new SqlConnection(connectionString);
        }

        [Route("GET")]
        [HttpGet]

        public List<Predmet> Citanje()
        {
            SqlCommand command = new SqlCommand("getAllFromPredmeti", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            List<Predmet> pList = new List<Predmet>();
            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Predmet pr = new Predmet();
                    pr.PKPredmetId = Convert.ToInt32(reader[0]);
                    pr.Naziv = Convert.ToString(reader[1]);
                    pr.Godina = Convert.ToInt32(reader[2]);
                    pr.Ime = Convert.ToString(reader[3]);
                    pr.Prezime = Convert.ToString(reader[4]);

                    pList.Add(pr);
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


        [Route("GET/{idPredmeta}")]
        [HttpGet]
        public Predmet CitanjePojedinacno(int idPredmeta)
        {
            SqlCommand command = new SqlCommand("getPredmetById", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@PredmetId", SqlDbType.Int).Value = idPredmeta;

            Predmet pr = new Predmet();

            try
            {
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pr.PKPredmetId = Convert.ToInt32(reader[0]);
                    pr.Naziv = Convert.ToString(reader[1]);
                    pr.Godina = Convert.ToInt32(reader[2]);
                    pr.Ime = Convert.ToString(reader[3]);
                    pr.Prezime = Convert.ToString(reader[4]);
                }

                reader.Close();
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pr;
        }
        
        [Route("POST")]
        [HttpPost]
        public void Unos(Predmet p)
        {

            SqlCommand command = new SqlCommand("insertIntoPredmeti", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Naziv", SqlDbType.VarChar).Value = p.Naziv;
            command.Parameters.Add("@Godina", SqlDbType.Int).Value = p.Godina;
            command.Parameters.Add("@ProfesorId", SqlDbType.Int).Value = p.FKProfesorId;

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
        public void Izmjena(Predmet p)
        {
            SqlCommand command = new SqlCommand("updateToPredmeti", db)
            {
                CommandType = CommandType.StoredProcedure
            };
            
            command.Parameters.Add("@Naziv", SqlDbType.VarChar).Value = p.Naziv;
            command.Parameters.Add("@Godina", SqlDbType.Int).Value = p.Godina;
            command.Parameters.Add("@ProfesorId", SqlDbType.Int).Value = p.FKProfesorId;
            command.Parameters.Add("@IdPredmeta", SqlDbType.Int).Value = p.PKPredmetId;


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

        [Route("DELETE/{idPredmeta}")]
        [HttpDelete]
        public void Brisanje(int idPredmeta)
        {
            SqlCommand command = new SqlCommand("deleteFromPredmeti", db)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@IdPredmeta", SqlDbType.Int).Value = idPredmeta;

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