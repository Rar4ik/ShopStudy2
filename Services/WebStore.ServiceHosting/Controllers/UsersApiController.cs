using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Dto.Identity;
using WebStore.Domain.Entities.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Identity.Users)]
    [Produces("application/json")]
    [ApiController]
    public class UsersApiController : Controller
    {
        private readonly ILogger<UsersApiController> _Logger;
        private readonly UserStore<User, Role, WebStoreContext> _UserStore;
        public UsersApiController(WebStoreContext db, ILogger<UsersApiController> Logger)
        {
            _Logger = Logger;
            _UserStore = new UserStore<User, Role, WebStoreContext>(db);
        }
        [HttpGet("AllUsers")]
        public async Task<IEnumerable<User>> GetAllUsers() => await _UserStore.Users.ToArrayAsync();

        #region Users

        /// <summary>
        /// Gets User's Id
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <returns>User's Id</returns>
        [HttpPost("UserId")]
        public async Task<string> GetUserIdAsync([FromBody] User user) => await _UserStore.GetUserIdAsync(user);

        /// <summary>
        /// Gets User's Name
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <returns>User's Name</returns>
        [HttpPost("UserName")]
        public async Task<string> GetUserNameAsync([FromBody] User user) => await _UserStore.GetUserNameAsync(user);

        /// <summary>
        /// Sets user's name 
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <param name="name">Name that will be set</param>
        /// <param name="Logger">Logs name changes </param>
        /// <returns>A user with new name</returns>
        [HttpPost("UserName/{name}")]
        public async Task SetUserNameAsync([FromBody] User user, string name, [FromServices] ILogger<UsersApiController> Logger)
        {
            Logger.LogInformation("Изменение имени пользователя {0} на {1}", user.Id, name);
            await _UserStore.SetUserNameAsync(user, name);
            await _UserStore.UpdateAsync(user);
        }

        /// <summary>
        /// Gets user's name in upper case 
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <returns>A name with capital letters</returns>
        [HttpPost("NormalUserName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody] User user) => await _UserStore.GetNormalizedUserNameAsync(user);

        /// <summary>
        /// Sets user's name in upper case
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <param name="name">A name which will be set</param>
        /// <returns>A name with capital letters</returns>
        [HttpPost("NormalUserName/{name}")]
        public async Task SetNormalizedUserNameAsync([FromBody] User user, string name)
        {
            _Logger.LogInformation("Изменение нормализованного имени пользователя {0} на {1}", user.Id, name);
            await _UserStore.SetNormalizedUserNameAsync(user, name);
            await _UserStore.UpdateAsync(user);
        }

        /// <summary>
        /// Create a User
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <returns>Creation status</returns>
        [HttpPost("User")]
        public async Task<bool> CreateAsync([FromBody] User user) => (await _UserStore.CreateAsync(user)).Succeeded;

        /// <summary>
        /// Update user's information
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <returns>Status of update</returns>
        [HttpPut("User")]
        public async Task<bool> UpdateAsync([FromBody] User user) => (await _UserStore.UpdateAsync(user)).Succeeded;

        /// <summary>
        /// Delete user's information
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <returns>Status of delete</returns>
        [HttpPost("User/Delete")]
        public async Task<bool> DeleteAsync([FromBody] User user) => (await _UserStore.DeleteAsync(user)).Succeeded;

        /// <summary>
        /// Gets a user by his Id
        /// </summary>
        /// <param name="id">Id of a user</param>
        /// <returns>User object</returns>
        [HttpGet("User/Find/{id}")]
        public async Task<User> FindByIdAsync(string id) => await _UserStore.FindByIdAsync(id);

        /// <summary>
        /// Gets a user by his Name
        /// </summary>
        /// <param name="name">Name of a user</param>
        /// <returns>User object</returns>
        [HttpGet("User/Normal/{name}")]
        public async Task<User> FindByNameAsync(string name) => await _UserStore.FindByNameAsync(name);

        /// <summary>
        /// Adds a user to a role
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <param name="role">Object Role which will be implied</param>
        /// <param name="db">Object of Database Context with FromService attribute</param>
        /// <returns>Saves a user to a new role into Database Context</returns>
        [HttpPost("Role/{role}")]
        public async Task AddToRoleAsync([FromBody] User user, string role, [FromServices] WebStoreContext db)
        {
            await _UserStore.AddToRoleAsync(user, role);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a user from a role group
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <param name="role">Object Role which will be deleted</param>
        /// <param name="db">Object of Database Context with FromService attribute</param>
        /// <returns>Deletes a user from the role group in Database Context</returns>
        [HttpPost("Role/Delete/{role}")]
        public async Task RemoveFromRoleAsync([FromBody] User user, string role, [FromServices] WebStoreContext db)
        {
            await _UserStore.RemoveFromRoleAsync(user, role);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Gets user's role
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <returns>A role of a user</returns>
        [HttpPost("Roles")]
        public async Task<IList<string>> GetRolesAsync([FromBody] User user) => await _UserStore.GetRolesAsync(user);

        /// <summary>
        /// Checks whether a user is in a role 
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <param name="role">Role's name</param>
        /// <returns>Bool status of containing a user in role group</returns>
        [HttpPost("InRole/{role}")]
        public async Task<bool> IsInRoleAsync([FromBody] User user, string role) => await _UserStore.IsInRoleAsync(user, role);

        /// <summary>
        /// Gets all users of the role 
        /// </summary>
        /// <param name="role">Role's name</param>
        /// <returns>List of the users which are in the role</returns>
        [HttpGet("UsersInRole/{role}")]
        public async Task<IList<User>> GetUsersInRoleAsync(string role) => await _UserStore.GetUsersInRoleAsync(role);

        /// <summary>
        /// Gets password Hash
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <returns>String Password Hash</returns>
        [HttpPost("GetPasswordHash")]
        public async Task<string> GetPasswordHashAsync([FromBody] User user) => await _UserStore.GetPasswordHashAsync(user);

        /// <summary>
        /// Sets password Hash
        /// </summary>
        /// <param name="hash">Object Hash required with FromBody attribute</param>
        /// <returns>Sets Password Hash in Hash object</returns>
        [HttpPost("SetPasswordHash")]
        public async Task<string> SetPasswordHashAsync([FromBody] PasswordHashDTO hash)
        {
            await _UserStore.SetPasswordHashAsync(hash.User, hash.Hash);
            await _UserStore.UpdateAsync(hash.User);
            return hash.User.PasswordHash;
        }

        /// <summary>
        /// Checks whether a user has a password
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <returns>Bool user's password</returns>
        [HttpPost("HasPassword")]
        public async Task<bool> HasPasswordAsync([FromBody] User user) => await _UserStore.HasPasswordAsync(user);


        #endregion

        #region Claims

        /// <summary>
        /// Gets Claims from User object
        /// </summary>
        /// <param name="user">Object User required with FromBody attribute</param>
        /// <returns></returns>
        [HttpPost("GetClaims")]
        public async Task<IList<Claim>> GetClaimsAsync([FromBody] User user) => await _UserStore.GetClaimsAsync(user);

        
        [HttpPost("AddClaims")]
        public async Task AddClaimsAsync([FromBody] AddClaimDTO ClaimInfo, [FromServices] WebStoreContext db)
        {
            await _UserStore.AddClaimsAsync(ClaimInfo.User, ClaimInfo.Claims);
            await db.SaveChangesAsync();
        }

        [HttpPost("ReplaceClaim")]
        public async Task ReplaceClaimAsync([FromBody] ReplaceClaimDTO ClaimInfo, [FromServices] WebStoreContext db)
        {
            await _UserStore.ReplaceClaimAsync(ClaimInfo.User, ClaimInfo.Claim, ClaimInfo.NewClaim);
            await db.SaveChangesAsync();
        }

        [HttpPost("RemoveClaim")]
        public async Task RemoveClaimsAsync([FromBody] RemoveClaimDTO ClaimInfo, [FromServices] WebStoreContext db)
        {
            await _UserStore.RemoveClaimsAsync(ClaimInfo.User, ClaimInfo.Claims);
            await db.SaveChangesAsync();
        }

        [HttpPost("GetUsersForClaim")]
        public async Task<IList<User>> GetUsersForClaimAsync([FromBody] Claim claim) =>
            await _UserStore.GetUsersForClaimAsync(claim);

        #endregion

        #region TwoFactor

        [HttpPost("GetTwoFactorEnabled")]
        public async Task<bool> GetTwoFactorEnabledAsync([FromBody] User user) =>
            await _UserStore.GetTwoFactorEnabledAsync(user);

        [HttpPost("SetTwoFactor/{enable}")]
        public async Task SetTwoFactorEnabledAsync([FromBody] User user, bool enable)
        {
            await _UserStore.SetTwoFactorEnabledAsync(user, enable);
            await _UserStore.UpdateAsync(user);
        }

        #endregion

        #region Email/Phone

        [HttpPost("GetEmail")]
        public async Task<string> GetEmailAsync([FromBody] User user) => await _UserStore.GetEmailAsync(user);

        [HttpPost("SetEmail/{email}")]
        public async Task SetEmailAsync([FromBody] User user, string email)
        {
            await _UserStore.SetEmailAsync(user, email);
            await _UserStore.UpdateAsync(user);
        }

        [HttpPost("GetEmailConfirmed")]
        public async Task<bool> GetEmailConfirmedAsync([FromBody] User user) => await _UserStore.GetEmailConfirmedAsync(user);

        [HttpPost("SetEmailConfirmed/{enable}")]
        public async Task SetEmailConfirmedAsync([FromBody] User user, bool enable)
        {
            await _UserStore.SetEmailConfirmedAsync(user, enable);
            await _UserStore.UpdateAsync(user);
        }

        [HttpGet("UserFindByEmail/{email}")]
        public async Task<User> FindByEmailAsync(string email) => await _UserStore.FindByEmailAsync(email);

        [HttpPost("GetNormalizedEmail")]
        public async Task<string> GetNormalizedEmailAsync([FromBody] User user) => await _UserStore.GetNormalizedEmailAsync(user);

        [HttpPost("SetNormalizedEmail/{email?}")]
        public async Task SetNormalizedEmailAsync([FromBody] User user, string email)
        {
            await _UserStore.SetNormalizedEmailAsync(user, email);
            await _UserStore.UpdateAsync(user);
        }

        [HttpPost("GetPhoneNumber")]
        public async Task<string> GetPhoneNumberAsync([FromBody] User user) => await _UserStore.GetPhoneNumberAsync(user);

        [HttpPost("SetPhoneNumber/{phone}")]
        public async Task SetPhoneNumberAsync([FromBody] User user, string phone)
        {
            await _UserStore.SetPhoneNumberAsync(user, phone);
            await _UserStore.UpdateAsync(user);
        }

        [HttpPost("GetPhoneNumberConfirmed")]
        public async Task<bool> GetPhoneNumberConfirmedAsync([FromBody] User user) =>
            await _UserStore.GetPhoneNumberConfirmedAsync(user);

        [HttpPost("SetPhoneNumberConfirmed/{confirmed}")]
        public async Task SetPhoneNumberConfirmedAsync([FromBody] User user, bool confirmed)
        {
            await _UserStore.SetPhoneNumberConfirmedAsync(user, confirmed);
            await _UserStore.UpdateAsync(user);
        }

        #endregion

        #region Login/Lockout

        [HttpPost("AddLogin")]
        public async Task AddLoginAsync([FromBody] AddLoginDTO login, [FromServices] WebStoreContext db)
        {
            await _UserStore.AddLoginAsync(login.User, login.UserLoginInfo);
            await db.SaveChangesAsync();
        }

        [HttpPost("RemoveLogin/{LoginProvider}/{ProviderKey}")]
        public async Task RemoveLoginAsync([FromBody] User user, string LoginProvider, string ProviderKey, [FromServices] WebStoreContext db)
        {
            await _UserStore.RemoveLoginAsync(user, LoginProvider, ProviderKey);
            await db.SaveChangesAsync();
        }

        [HttpPost("GetLogins")]
        public async Task<IList<UserLoginInfo>> GetLoginsAsync([FromBody] User user) =>
            await _UserStore.GetLoginsAsync(user);

        [HttpGet("User/FindByLogin/{LoginProvider}/{ProviderKey}")]
        public async Task<User> FindByLoginAsync(string LoginProvider, string ProviderKey) =>
            await _UserStore.FindByLoginAsync(LoginProvider, ProviderKey);

        [HttpPost("GetLockoutEndDate")]
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync([FromBody] User user) =>
            await _UserStore.GetLockoutEndDateAsync(user);

        [HttpPost("SetLockoutEndDate")]
        public async Task SetLockoutEndDateAsync([FromBody] SetLockOutDTO LockoutInfo)
        {
            await _UserStore.SetLockoutEndDateAsync(LockoutInfo.User, LockoutInfo.LockoutEnd);
            await _UserStore.UpdateAsync(LockoutInfo.User);
        }

        [HttpPost("IncrementAccessFailedCount")]
        public async Task<int> IncrementAccessFailedCountAsync([FromBody] User user)
        {
            var count = await _UserStore.IncrementAccessFailedCountAsync(user);
            await _UserStore.UpdateAsync(user);
            return count;
        }

        [HttpPost("ResetAccessFailedCount")]
        public async Task ResetAccessFailedCountAsync([FromBody] User user)
        {
            await _UserStore.ResetAccessFailedCountAsync(user);
            await _UserStore.UpdateAsync(user);
        }

        [HttpPost("GetAccessFailedCount")]
        public async Task<int> GetAccessFailedCountAsync([FromBody] User user) => await _UserStore.GetAccessFailedCountAsync(user);

        [HttpPost("GetLockoutEnabled")]
        public async Task<bool> GetLockoutEnabledAsync([FromBody] User user) => await _UserStore.GetLockoutEnabledAsync(user);

        [HttpPost("SetLockoutEnabled/{enable}")]
        public async Task SetLockoutEnabledAsync([FromBody] User user, bool enable)
        {
            await _UserStore.SetLockoutEnabledAsync(user, enable);
            await _UserStore.UpdateAsync(user);
        }

        #endregion
    }
}
