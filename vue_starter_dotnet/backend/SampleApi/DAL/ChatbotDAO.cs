﻿using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.DAL
{
    ///<Summary>
    /// Gets the answer
    ///</Summary>
    public class ChatbotDAO : IChatbotDAO
    {
        public string connectionString;

        public ChatbotDAO(string connectionString)
        {

            this.connectionString = connectionString;
        }

        public string GetKeyword(string userInput)
        {
            string userInputToLower = userInput.ToLower();
            string matchingKeyword= "unknown";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select keyword from users ORDER BY len(keyword) desc", conn);
                    //cmd.Parameters.AddWithValue("@keyword", userInput);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string keyword = Convert.ToString(reader["keyword"]).ToLower();
                        if (userInput.ToLower().Contains(keyword))
                        {
                            matchingKeyword = keyword;
                            break;
                        }
                    }
                }
            }

            catch (SqlException ex)
            {

            }
            return matchingKeyword;
        }
        public string GetBotResponse(string keyword)
        {
            string response="";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {


                    conn.Open();


                    SqlCommand cmd = new SqlCommand("Select * from users where keyword = @keyword; ", conn);
                    cmd.Parameters.AddWithValue("@keyword", keyword);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        response = Convert.ToString(reader["response"]);
                    }
                }
            }

            catch (SqlException ex)
            {

            }

            return response;
        }
        public string GetQuote()
        {
            string randomQuote= "This quote is inspirational.";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 quote FROM motivationalquotes ORDER BY NEWID()", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        randomQuote = Convert.ToString(reader["quote"]);
                    }
                }
            }

            catch (SqlException ex)
            {

            }

            return randomQuote;
            
        }

        public string GetInterviewQuestion()
        {
            string randomQuestion = "Oops...looks like we're out of interview questions.";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP 1 question FROM interview_questions ORDER BY NEWID()", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        randomQuestion = Convert.ToString(reader["question"]);
                    }
                }
            }

            catch (SqlException ex)
            {

            }

            return randomQuestion;

        }

    }
}
