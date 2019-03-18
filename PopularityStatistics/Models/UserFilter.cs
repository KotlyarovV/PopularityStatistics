using System;
using System.Collections.Generic;
using VKWebApi;

namespace PopularityStatistics.Models
{
    public class UserFilter
    {
        private static readonly Dictionary<FilterEnum, Func<User, string>> DataFilter = new Dictionary<FilterEnum, Func<User, string>>
        {
            [FilterEnum.Books] =  user => user.Books,
            [FilterEnum.Movies] = user => user.Movies,
            [FilterEnum.Music] = user => user.Music,
            [FilterEnum.TV] = user => user.Tv
        };

        public Func<User, string>  FilteredField { get; set; }

        private Predicate<User> AgeConstraints { get; set; }

        private Predicate<User> SexConstraints { get; set; }

        public bool IsSuitable(User user) => FilteredField(user) != null && AgeConstraints(user) && SexConstraints(user);

        private UserFilter()
        {}

        public UserFilter(ParametersModel parameters)
        {
            FilteredField = DataFilter[parameters.Filter];
            if (parameters.AgeIsNotImportant)
            {
                AgeConstraints = user => true;
            }
            else
            {
                AgeConstraints = user => user.DateIsValid && user.Age >= parameters.FromAge && user.Age <= parameters.ToAge;
            }

            if (parameters.Sex == SexEnum.Both)
            {
                SexConstraints = user => true;
            }
            else
            {
                var conditionSex = parameters.Sex == SexEnum.Man ? 2 : 1;
                SexConstraints = user => user.Sex == conditionSex;
            }
        }
    }
}
