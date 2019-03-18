using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VKWebApi.Requests;

namespace VKWebApi
{
    public class DataCollector
    {
        private int requestCount = 3;

        public DataCollector(int requestCount = 3)
        {
            this.requestCount = requestCount;
        }
        
        private async Task<IEnumerable<User>>  GetFriends(int id)
        {
            return await Task.Run(
                () => FriendsRequest
                .CreateRequest(id)
                .CreateFields()
                .AddAllPossibleFields()
                .MakeRequest()
                .Result
                .Select(User.ParseJson)
                );
        }

        private async Task<User> GetUser(string identificator)
        {
            return await Task.Run(() =>
            {
                var requestResult = UserRequest
                    .CreateRequest(identificator)
                    .CreateFields()
                    .AddAllPossibleFields()
                    .MakeRequest()
                    .Result;
                return User.ParseJson(requestResult);
            });
           
        }


        public async Task<List<User>> GetUsersBfs(string startId)
        {
            var users = new List<User>();
            var usersStack = new Stack<int>();
            var visitedUsers = new HashSet<int>();

            var userFirst = await GetUser(startId);
            users.Add(userFirst);
            visitedUsers.Add(users.First().Id);
            usersStack.Push(users.First().Id);

            for (var i = 0; i < requestCount; i++)
            {
                var id = usersStack.Pop();
                var downloadedUsers = await GetFriends(id);
                users.AddRange(downloadedUsers);
                foreach (var user in downloadedUsers)
                {
                    if (!visitedUsers.Contains(user.Id))
                    {
                        usersStack.Push(user.Id);
                    } 
                }
                visitedUsers.Add(id);
            }

            return users;
        }
    }
}
