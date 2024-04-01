using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TriviaAppClean.Models;

namespace TriviaAppClean.Services
{
    public class Questions
    {
        public List<AmericanQuestion> questions { get; set; }
    }

    public class Users
    {
        public List<User> TheUsers { get; set; }
    }


    public class TriviaWebAPIProxy
    {
        private HttpClient client;
        private string baseUri;
        private static Random r = new Random();

        public TriviaWebAPIProxy()
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = "https://script.google.com/macros/s/AKfycbx0wFVKvR8bl3GxOScSRIhSVnYkahEwFeyyx8h9pANqybeEBZEtD0huZZOs7FEJFmtBIw/exec";
        }

        //Login - if email and password are correct User object is returned. otherwise a null will be returned
        public async Task<User> LoginAsync(string email, string pass)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}?action=getUsers");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    Users u = JsonSerializer.Deserialize<Users>(content, options);
                    foreach (User user in u.TheUsers)
                    {
                        if (user.Email == email && user.Password == pass)
                        {
                            user.Questions = new List<AmericanQuestion>();
                            //now read all questions of that user
                            List<AmericanQuestion> questions = await GetAllQuestions();
                            foreach (AmericanQuestion question in questions)
                            {
                                if (question.UserId == user.Id)
                                    user.Questions.Add(question);
                            }

                            return user;
                        }

                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //This method gets from the server a random question if it does not suceed it returns null (no previous login is required)
        public async Task<AmericanQuestion> GetRandomQuestion()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}?action=getQuestions");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    Questions q = JsonSerializer.Deserialize<Questions>(content, options);
                    if (q == null)
                        return null;
                    else return q.questions[r.Next(0, q.questions.Count + 1)];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //This method post new question to the server. A previous login is required! The nick name in the question must match the logged in user!
        //it returns true is succeeded or false otherwise
        public async Task<bool> PostNewQuestion(AmericanQuestion q)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string json = JsonSerializer.Serialize<AmericanQuestion>(q, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}?action=addQuestion", content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        //This method register a new user into the server database. A previous login is NOT required! The nick name and email must be uniqe!
        //it returns true is succeeded or false otherwise
        //questions are ignored upon registering a user and shoul dbe added seperatly.

        public async Task<bool> RegisterUser(User u)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string json = JsonSerializer.Serialize<User>(u, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}?action=addUser", content);
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateQuestionStatus(int id, int status)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}?action=updateQuestionStatus&id={id}&status={status}");

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<AmericanQuestion>> GetAllQuestions()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}?action=getQuestions");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    Questions q = JsonSerializer.Deserialize<Questions>(content, options);
                    if (q == null)
                        return null;
                    else return q.questions;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> UpdateQuestion(AmericanQuestion q)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string json = JsonSerializer.Serialize<AmericanQuestion>(q, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}?action=updateQuestion", content);

                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateUser(User u)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string json = JsonSerializer.Serialize<User>(u, options);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}?action=updateUser", content);

                if (response.IsSuccessStatusCode)
                {

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}?action=getUsers");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    Users u = JsonSerializer.Deserialize<Users>(content, options);
                    if (u == null)
                        return null;
                    else
                    {
                        //now read all questions of that user
                        List<AmericanQuestion> questions = await GetAllQuestions();
                        foreach (User user in u.TheUsers)
                        {
                            user.Questions = new List<AmericanQuestion>();
                            foreach (AmericanQuestion question in questions)
                            {
                                if (user.Id == question.UserId)
                                    user.Questions.Add(question);
                            }
                        }

                    }
                    return u.TheUsers;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public List<Rank> GetRanks()
        {
            List<Rank> ranks = new List<Rank>();

            ranks.Add(new Rank() { Id = 0, Name = "Rookie" });
            ranks.Add(new Rank() { Id = 1, Name = "Master" });
            ranks.Add(new Rank() { Id = 2, Name = "Admin" });

            return ranks;
        }

        public List<QuestionStatus> GetQuestionStatuses()
        {
            List<QuestionStatus> statuses = new List<QuestionStatus>();

            statuses.Add(new QuestionStatus() { Id = 0, Name = "Pending" });
            statuses.Add(new QuestionStatus() { Id = 1, Name = "Approved" });
            statuses.Add(new QuestionStatus() { Id = 2, Name = "Not Approved" });

            return statuses;
        }
    }



}