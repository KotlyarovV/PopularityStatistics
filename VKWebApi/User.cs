using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VKWebApi
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string PhotoLink { get; set; }
        public int Sex { get; set; }
        public string Education { get; set; }
        public string NickName { get; set; }
        public string About { get; set; }
        public string Books { get; set; }
        public string Music { get; set; }
        public string Interests { get; set; }
        public string BirthDateString { private get; set; }
        public string City { get; set; }
        public string Games { get; set; }
        public string Movies { get; set; }
        public string Tv { get; set; }

        public bool DateIsValid => (BirthDateString != null) && BirthDateString.Count(symbol => symbol == '.') == 2;
        public DateTime BirthDate => DateTime.Parse(BirthDateString);
        public int Age
        {
            get
            {
                var currentYear = (DateTime.Now.Month > BirthDate.Month ||
                                   (DateTime.Now.Month == BirthDate.Month && DateTime.Now.Day >= BirthDate.Day))
                    ? 1 : 0;
                return DateTime.Now.Year - BirthDate.Year - 1 + currentYear;
            }
        }

        public static string GetValue(JToken jToken, string value)
        {
            if (jToken[value] == null || jToken[value].ToString() == "")
            {
                return null;
            }

            return jToken[value].ToString();
        }

        public static User ParseJson(JToken jToken)
        {
            if (int.TryParse(jToken.ToString(), out var id))
            {
                return new User { Id = id };
            }

            if (jToken["id"] == null)
            {
                return null;
            }

            var sex = 0;
            if (jToken["sex"] != null)
            {
                sex = int.Parse(jToken["sex"].ToString());
            }

            return new User
            {
                About = GetValue(jToken, "about"),
                BirthDateString = GetValue(jToken, "bdate"),
                Books = GetValue(jToken, "books"),
                Education = GetValue(jToken, "education"),
                City = GetValue(jToken, "city"),
                FirstName = GetValue(jToken, "first_name"),
                Games = GetValue(jToken, "games"),
                HomePhone = GetValue(jToken, "home_phone"),
                Id = int.Parse(jToken["id"].ToString()),
                Interests = GetValue(jToken, "interests"),
                LastName = GetValue(jToken, "last_name"),
                MobilePhone = GetValue(jToken, "mobile_phone"),
                PhotoLink = GetValue(jToken, "photo_200"),
                NickName = GetValue(jToken, "nickname"),
                Music = GetValue(jToken, "music"),
                Movies = GetValue(jToken, "movies"),
                Tv = GetValue(jToken, "tv"),
                Sex = sex
            };
        }
    }
}
