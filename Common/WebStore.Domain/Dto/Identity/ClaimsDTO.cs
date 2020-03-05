using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WebStore.Domain.Dto.Identity
{
    public abstract class ClaimsDTO : UserDTO
    {
        public IEnumerable<Claim> Claims { get; set; }
    }
    public class AddClaimDTO : ClaimsDTO { }
    public class RemoveClaimDTO : ClaimsDTO { }

    public class ReplaceClaimDTO : UserDTO
    {
        public  Claim Claim { get; set; }
        public Claim NewClaim { get; set; }
    }
    
}
