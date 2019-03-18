using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VKWebApi.Requests;

namespace VKWebApi
{
    class FriendsRequest : Request
    {
        private FriendsRequest(string id)
        {
            RequestParameters["user_id"] = id;
            RequestParameters["order"] = "name";
            RequestParameters["name_case"] = "nom";
        }

        public static IRequestWithoutFields CreateRequest(int id)
        {
            return new FriendsRequest(id.ToString());
        }

        protected override JToken PostProcessing(JToken jToken) => jToken["response"]["items"];

        public override string MethodName { get; set; } = "friends.get";
    }
}
