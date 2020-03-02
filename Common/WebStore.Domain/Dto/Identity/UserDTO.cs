using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.Dto.Identity
{
    public abstract class UserDTO
    {
        public User User { get; set; }

    }

    public class AddLoginDTO : UserDTO
    {
        public UserLoginInfo UserLoginInfo { get; set; }
    }

    public class PasswordHashDTO : UserDTO
    {
        public string Hash { get; set; }
    }

    public class SetLockOutDTO : UserDTO
    {
        public  DateTimeOffset? LockoutEnd { get; set; }
    }
}
