using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VKWebApi.Requests
{
    public interface IRequest
    {
        Task<JToken> MakeRequest();
    }
}
