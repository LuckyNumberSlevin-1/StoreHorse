using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DATA.Dto;
using Store.DATA.Models;

namespace Store.DATA.Converters
{
    public static class UserConverter
    {
        public static User Convert(UserDto item) =>
            new User
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname
            };

        public static UserDto Convert(User item)
        {
            if (item == null)
                return null;

            return new UserDto
            {
                Id = item.Id,
                Name = item.Name,
                Surname = item.Surname
            };
        }
        public static List<User> Convert(List<UserDto> items) =>
           items.Select(c => Convert(c)).ToList();

        public static List<UserDto> Convert(List<User> items) =>
            items.Select(c => Convert(c)).ToList();
    }
}
