using System.Collections.Generic;

namespace MVP.Project.Infra.CrossCutting.Identity.Models
{
    public class UserToken
    {
        public dynamic Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
}
