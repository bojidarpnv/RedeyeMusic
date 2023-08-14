namespace RedeyeMusic.Web.ViewModels.User
{
    public class UserViewModel /*: IMapFrom<Artist>, IHaveCustomMappings*/
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string ArtistName { get; set; } = null!;

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Artist, UserViewModel>()
        //        .ForMember(d => d.Email, opt=>opt.MapFrom(s => s.ApplicationUser.Email))
        //        .ForMember(d => d.FullName, opt=> opt.MapFrom(s=>s.ApplicationUser.FirstName + " " + s.ApplicationUser.LastName));
        //}
    }
}
