using System.Globalization;
using API.Dto;
using AutoMapper;
using Core.Model;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BaseEntity, BaseEntityDto>();

            CreateMap<string, DateTimeOffset?>().ConvertUsing<StringToDateTimeConverter>();
            //CreateMap<DateTime, string>().ConvertUsing(dt => dt.ToString("u"));

            CreateMap<Company, CompanyToReturnDto>();
            CreateMap<CompanyCreateDto, Company>();

            CreateMap<SiteCreateDto, Site>();
            CreateMap<Site, SiteToReturnDto>();

            CreateMap<Location, LocationToReturnDto>();
            CreateMap<Category, CatetoryToReturnDto>();
            CreateMap<Department, DepartmentToReturnDto>();

            CreateMap<AssetToCreateDto, Asset>();
            // .ForMember(dest=>dest.PurchasedDate, src =>
            // {
            //     src.MapFrom<CreateDateResolver>();
            // });

            CreateMap<AssetToEditDto, Asset>();
            // .ForMember(dest=>dest.PurchasedDate, src =>
            // {
            //     src.MapFrom<EditDateResolver>();
            // });

            CreateMap<Asset, AssetToReturnDto>()
            .ForMember(destination => destination.Status,
                 opt => opt.MapFrom(source => Enum.GetName(typeof(EventStatus), source.Events.Where(x=>x.IsActive).Select(x => x.EventStatus).FirstOrDefault())))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom<AssetImageUrlResolver>());


            CreateMap<EventToUpdateDto,Event>()
            .ForMember(dest=>dest.EventStatus,opt=>opt.MapFrom(src=>(EventStatus)Enum.Parse(typeof(EventStatus),src.Status)));
        }
    }

    public class StringToDateTimeConverter : ITypeConverter<string, DateTimeOffset?>
    {

        public DateTimeOffset? Convert(string source, DateTimeOffset? destination, ResolutionContext context)
        {
            object objDateTime = source;
            DateTimeOffset dateTime;

            if (objDateTime == null)
            {
                return null;
            }

            if (DateTimeOffset.TryParse(objDateTime.ToString(), out dateTime))
            {
                return dateTime;
            }


            // dateTime = DateTime.ParseExact(objDateTime.ToString(), "ddd MMM dd yyyy HH:mm:ss 'GMT+0600 (Bangladesh Standard Time)'",
            //         CultureInfo.InvariantCulture);

            return null;
        }
    }

    public class AssetImageUrlResolver : IValueResolver<Asset, AssetToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public AssetImageUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Asset source, AssetToReturnDto destination, string destMember, ResolutionContext context)
        {
            var apiUrl = _config["ApiUrl"];
            if (!string.IsNullOrEmpty(apiUrl))
            {
                return apiUrl + source.Photos.Where(x => x.IsMain).Select(x => x.Url).FirstOrDefault();
            }

            return null;
        }
    }

    public class CreateDateResolver : IValueResolver<AssetToCreateDto, Asset, DateTimeOffset?>
    {

        public DateTimeOffset? Resolve(AssetToCreateDto source, Asset destination, DateTimeOffset? destMember, ResolutionContext context)
        {
            object objDateTime = source;
            DateTimeOffset? dateTime;

            if (objDateTime == null)
            {
                return default(DateTime);
            }

            // if (DateTime.TryParse(objDateTime.ToString(), out dateTime))
            // {
            //     return dateTime;
            // }


            dateTime = DateTimeOffset.ParseExact(objDateTime.ToString(), "ddd MMM dd yyyy HH:mm:ss 'GMT+0600 (Bangladesh Standard Time)'",
                    CultureInfo.InvariantCulture);

            return dateTime;
        }
    }

    public class EditDateResolver : IValueResolver<AssetToEditDto, Asset, DateTimeOffset?>
    {

        public DateTimeOffset? Resolve(AssetToEditDto source, Asset destination, DateTimeOffset? destMember, ResolutionContext context)
        {
            object objDateTime = source;
            DateTimeOffset dateTime;

            if (objDateTime == null)
            {
                return default(DateTime);
            }

            if (DateTimeOffset.TryParse(objDateTime.ToString(), out dateTime))
            {
                return dateTime;
            }

            return default(DateTimeOffset);
        }
    }
}