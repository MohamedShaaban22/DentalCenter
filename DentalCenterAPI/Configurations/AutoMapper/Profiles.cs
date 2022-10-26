using AutoMapper;
using DentalCenterAPI.Models.Blog.Basic;
using DentalCenterAPI.Models.Blog.Business;
using DentalCenterAPI.Models.BlogDetails.Basic;
using DentalCenterAPI.Models.BlogDetails.Business;
using DentalCenterAPI.Models.Branch.Basic;
using DentalCenterAPI.Models.Common;
using DentalCenterAPI.Models.FamousSection.Basic;
using DentalCenterAPI.Models.FamousSection.Business;
using DentalCenterAPI.Models.News.Basic;
using DentalCenterAPI.Models.News.Business;
using DentalCenterAPI.Models.NewsImages.Basic;
using DentalCenterAPI.Models.NewsImages.Business;

namespace DentalCenterAPI.Configurations.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            //Branch
            CreateMap<BranchBasicModel, DataBusinessModel>()
                .ForMember(x => x.ID, opt => opt.MapFrom(src => src.BranchID)).ReverseMap();

            //News
            CreateMap<NewsBasicModel, NewsBusinessModel>().ReverseMap();
            CreateMap<NewsBasicModel, HomeNewsBusinessModel>()
                .ForMember(x => x.Date, opt => opt.MapFrom(src => (src.InsertDate != null) ? src.InsertDate.Value.ToString("dd/MM/yyyy") : ""))
                .ReverseMap();

            //News
            CreateMap<NewsImagesBasicModel, NewsImagesBusinessModel>().ReverseMap();

            //Blog
            CreateMap<BlogBasicModel, BlogBusinessModel>().ReverseMap();

            //BlogDetails
            CreateMap<BlogDetailsBasicModel, BlogDetailsBusinessModel>().ReverseMap();

            //FamousSectionImage
            CreateMap<FamousSectionImageBasicModel, FamousSectionImageBusinessModel>().ReverseMap();

            //FamousSection
            CreateMap<FamousSectionBasicModel, FamousSectionBusinessModel>().ReverseMap();
        }
    }
}