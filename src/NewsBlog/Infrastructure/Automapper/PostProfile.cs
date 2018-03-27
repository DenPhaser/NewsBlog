namespace NewsBlog.Infrastructure.Automapper
{
    using AutoMapper;

    using NewsBlog.Domain;
    using NewsBlog.Models.NewsViewModels;

    public class PostProfile : Profile
    {
        public PostProfile()
        {
            this.CreateMap<Post, PostViewModel>()
                .ForMember(dm => dm.Content, mo => mo.MapFrom(sm => sm.Text));
            this.CreateMap<PostViewModel, Post>()
                .ForMember(dm => dm.Text, mo => mo.MapFrom(sm => sm.Content))
                .ForMember(dm => dm.User, mo => mo.Ignore())
                .ForMember(dm => dm.UserId, mo => mo.Ignore());
        }
    }
}
