using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VKWebApi.Requests;

namespace VKWebApi
{
    public abstract class Request : IRequestWithoutFields, IRequest, IRequestHandler
    {
        protected const string ApiLink = "https://api.vk.com/method/";
        protected readonly HttpClient Client = new HttpClient();
        protected readonly RequestParameters RequestParameters;

        protected abstract JToken PostProcessing(JToken jToken);

        protected Request()
        {
            RequestParameters = new RequestParameters();
        }

        public virtual async Task<JToken> MakeRequest()
        {
            var url = ApiLink + MethodName;
            var response = await Client.PostAsync(url, RequestParameters.Content);
            var result = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(result);

            return PostProcessing(json);
        }

        public abstract string MethodName { get; set; }
        public IRequest CreateRequestWithoutFields()
        {
            return this;
        }

        public IRequestHandler CreateFields()
        {
            return this;
        }

        public IRequestHandler AddPhoto()
        {
            RequestParameters.AddPhoto();
            return this;
        }

        public IRequestHandler AddSex()
        {
            RequestParameters.AddSex();
            return this;
        }

        public IRequestHandler AddEducation()
        {
            RequestParameters.AddEducation();
            return this;
        }

        public IRequestHandler AddNickName()
        {
            RequestParameters.AddNickName();
            return this;
        }

        public IRequestHandler AddAbout()
        {
            RequestParameters.AddAbout();
            return this;
        }

        public IRequestHandler AddBooks()
        {
            RequestParameters.AddBooks();
            return this;
        }

        public IRequestHandler AddMusic()
        {
            RequestParameters.AddMusic();
            return this;
        }

        public IRequestHandler AddInterests()
        {
            RequestParameters.AddInterests();
            return this;
        }

        public IRequestHandler AddCounters()
        {
            RequestParameters.AddCounters();
            return this;
        }

        public IRequestHandler AddBDate()
        {
            RequestParameters.AddBDate();
            return this;
        }

        public IRequestHandler AddCity()
        {
            RequestParameters.AddCity();
            return this;
        }

        public IRequestHandler AddContacts()
        {
            RequestParameters.AddContacts();
            return this;
        }

        public IRequestHandler AddGames()
        {
            RequestParameters.AddGames();
            return this;
        }

        public IRequestHandler AddMovies()
        {
            RequestParameters.AddMovies();
            return this;
        }

        public IRequestHandler AddTv()
        {
            RequestParameters.AddTv();
            return this;
        }

        public IRequest AddAllPossibleFields()
        {
            return this
                .AddAbout()
                .AddBooks()
                .AddBDate()
                .AddCity()
                .AddContacts()
                .AddCounters()
                .AddEducation()
                .AddGames()
                .AddInterests()
                .AddMovies()
                .AddTv()
                .AddMusic()
                .AddNickName()
                .AddPhoto()
                .AddSex();
        }
    }


}
