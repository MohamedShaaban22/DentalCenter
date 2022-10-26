using DentalCenterAPI.Repository.Admin;
using DentalCenterAPI.Repository.Blog;
using DentalCenterAPI.Repository.BlogDetails;
using DentalCenterAPI.Repository.Branch;
using DentalCenterAPI.Repository.CommonQuestion;
using DentalCenterAPI.Repository.Doctor;
using DentalCenterAPI.Repository.DoctorPatient;
using DentalCenterAPI.Repository.Gallery;
using DentalCenterAPI.Repository.HappyPatient;
using DentalCenterAPI.Repository.News;
using DentalCenterAPI.Repository.NewsImage;
using DentalCenterAPI.Repository.ServiceImage;
using DentalCenterAPI.Repository.Subscribtion;
using DentalCenterAPI.Repository.HomePage;
using DentalCenterAPI.Services.Admin;
using DentalCenterAPI.Services.Blog;
using DentalCenterAPI.Services.BlogDetails;
using DentalCenterAPI.Services.Branch;
using DentalCenterAPI.Services.CommonQuestion;
using DentalCenterAPI.Services.Doctor;
using DentalCenterAPI.Services.DoctorPatient;
using DentalCenterAPI.Services.File;
using DentalCenterAPI.Services.Gallery;
using DentalCenterAPI.Services.HappyPatient;
using DentalCenterAPI.Services.News;
using DentalCenterAPI.Services.NewsImage;
using DentalCenterAPI.Services.ServiceImage;
using DentalCenterAPI.Services.Subscribtion;
using DentalCenterAPI.Services.HomePage;
using Microsoft.Extensions.DependencyInjection;
using DentalCenterAPI.Repository.HomeCounter;
using DentalCenterAPI.Services.HomeCounter;
using DentalCenterAPI.Services.Service;
using DentalCenterAPI.Repository.Service;
using DentalCenterAPI.Services.Appointment;
using DentalCenterAPI.Repository.Appointment;
using DentalCenterAPI.Repository.FamousSection;
using DentalCenterAPI.Services.FamousSection;
using DentalCenterAPI.Services.FamousSectionImage;
using DentalCenterAPI.Repository.FamousSectionImage;

namespace DentalCenterAPI.Configurations.Resolvers
{
    public class DependencyResolver
    {
        public static void Resolve(IServiceCollection services)
        {
            //File
            services.AddScoped<IFileService, FileService>();

            //Branch
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<BranchRepository>();

            //Admin
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<AdminRepository>();

            //HappyPatient
            services.AddScoped<IHappyPatientService, HappyPatientService>();
            services.AddScoped<HappyPatientRepository>();

            //Subscribtion 
            services.AddScoped<ISubscribtionService, SubscribtionService>();
            services.AddScoped<SubscribtionRepository>();

            //Gallery 
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<GalleryRepository>();

            //ServiceImages 
            services.AddScoped<IServiceImageService, ServiceImageService>();
            services.AddScoped<ServiceImageRepository>();

            //News 
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<NewsRepository>();

            //NewsImages 
            services.AddScoped<INewsImageService, NewsImageService>();
            services.AddScoped<NewsImageRepository>();

            //Blog 
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<BlogRepository>();

            //Blog Details 
            services.AddScoped<IBlogDetailsService, BlogDetailsService>();
            services.AddScoped<BlogDetailsRepository>();

            //Common Questions
            services.AddScoped<ICommonQuestionService, CommonQuestionService>();
            services.AddScoped<CommonQuestionRepository>();

            //Doctor
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<DoctorRepository>();

            //Doctor Patient
            services.AddScoped<IDoctorPatientService, DoctorPatientService>();
            services.AddScoped<DoctorPatientRepository>();

            //HomePage
            services.AddScoped<IHomePageService, HomePageService>();
            services.AddScoped<HomePageRepository>();

            //Home Counter
            services.AddScoped<IHomeCounterService, HomeCounterService>();
            services.AddScoped<HomeCounterRepository>();

            //Service
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ServiceRepository>();

            //Appointment
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<AppointmentRepository>();

            //FamousSection
            services.AddScoped<IFamousSectionService, FamousSectionService>();
            services.AddScoped<FamousSectionRepository>();

            //FamousSectionImage
            services.AddScoped<IFamousSectionImageService, FamousSectionImageService>();
            services.AddScoped<FamousSectionImageRepository>();
        }
    }
}