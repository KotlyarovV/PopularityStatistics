using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKWebApi.Requests;

namespace VKWebApi
{
    public interface IRequestHandler : IRequest
    {
        IRequestHandler AddPhoto();
        IRequestHandler AddSex();
        IRequestHandler AddEducation();
        IRequestHandler AddNickName();
        IRequestHandler AddAbout();
        IRequestHandler AddBooks();
        IRequestHandler AddMusic();
        IRequestHandler AddInterests();
        IRequestHandler AddCounters();
        IRequestHandler AddBDate();
        IRequestHandler AddCity();
        IRequestHandler AddContacts();
        IRequestHandler AddGames();
        IRequestHandler AddMovies();
        IRequestHandler AddTv();
        IRequest AddAllPossibleFields();
    }
}
