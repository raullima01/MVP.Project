namespace MVP.Project.Infra.CrossCutting.Identity.API
{
    public class AppJwtSettings
    {
        public string SecretKey { get; set; }
        public int Expiration { get; set; } = 1;
        public string Issuer { get; set; } = "MvpProject.Api";
        public string Audience { get; set; } = "Api";
    }
}