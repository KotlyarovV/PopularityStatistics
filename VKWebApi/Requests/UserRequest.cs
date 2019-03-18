using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VKWebApi.Requests;

namespace VKWebApi.Requests
{
    public class UserRequest : Request
    {        
        private UserRequest(string user)
        {
            RequestParameters["user_ids"] = user;
        }

        public static IRequestWithoutFields CreateRequest(string id)
        {
            return new UserRequest(id);
        }

        protected override JToken PostProcessing(JToken jToken) => jToken["response"].First;

        public override string MethodName { get; set; } = "users.get";
    }
}
