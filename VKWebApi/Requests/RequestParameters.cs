using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VKWebApi
{
    public class RequestParameters
    {
        private const string AccsessToken = "278ce9959116dd1eab37a11b0fc5f9919a73f2a0dad01c3bf058f4effbaec6b86153c43087861307faf09";
        private const string Version = "5.75";

        private readonly Dictionary<string, string> _parameters;
        public RequestParameters()
        {
            _parameters = new Dictionary<string, string>
            {
                ["access_token"] = AccsessToken,
                ["v"] = Version
            };
        }

        public string this[string parameter]
        {
            get => _parameters[parameter];
            set => _parameters[parameter] = value;
        }

        public FormUrlEncodedContent Content => new FormUrlEncodedContent(_parameters);

        public RequestParameters AddField(string field)
        {
            _parameters["fields"] = _parameters.ContainsKey("fields") ? _parameters["fields"] + ", " + field : field;
            return this;
        }

        public RequestParameters AddPhoto()
        {
            AddField("photo_200");
            return this;
        }

        public RequestParameters AddSex()
        {
            AddField("sex");
            return this;
        }

        public RequestParameters AddEducation()
        {
            AddField("education");
            return this;
        }

        public RequestParameters AddNickName()
        {
            AddField("nickname");
            return this;
        }

        public RequestParameters AddAbout()
        {
            AddField("about");
            return this;
        }

        public RequestParameters AddBooks()
        {
            AddField("books");
            return this;
        }

        public RequestParameters AddMusic()
        {
            AddField("music");
            return this;
        }

        public RequestParameters AddInterests()
        {
            AddField("interests");
            return this;
        }

        public RequestParameters AddCounters()
        {
            AddField("counters");
            return this;
        }

        public RequestParameters AddBDate()
        {
            AddField("bdate");
            return this;
        }

        public RequestParameters AddCity()
        {
            AddField("city");
            return this;
        }

        public RequestParameters AddContacts()
        {
            AddField("contacts");
            return this;
        }

        public RequestParameters AddGames()
        {
            AddField("games");
            return this;
        }

        public RequestParameters AddMovies()
        {
            AddField("movies");
            return this;
        }

        public RequestParameters AddTv()
        {
            AddField("tv");
            return this;
        }

    }
}
