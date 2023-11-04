using System.Globalization;
using System.Net.Http.Headers;
using API.Dto;
using API.Helper;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core;
using Core.Interfaces;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AssetsController : BaseApiController
    {
        private readonly IGenericRepository<Asset> _assetRepo;
        private readonly IGenericRepository<Event> _eventRepo;
        private readonly IGenericRepository<Photo> _photoRepo;
        private readonly IMapper _mapper;
        private readonly IAssetService _assetService;
        private readonly IConfiguration _config;

        public AssetsController(
            IGenericRepository<Asset> assetRepo,
            IGenericRepository<Event> eventRepo,
            IGenericRepository<Photo> photoRepo,
            IMapper mapper,
            IAssetService assetService,
            IConfiguration config)
        {
            _photoRepo = photoRepo;
            _mapper = mapper;
            _assetService = assetService;
            _config = config;
            _assetRepo = assetRepo;
            _eventRepo = eventRepo;
        }

        [HttpGet]
        public async Task<ActionResult<AssetToReturnDto>> GetAssets([FromQuery] PaginationParams param)
        {
            var assets = _assetRepo.GetQueryAble().Include(x => x.Photos)
            .Include(x => x.Events).AsQueryable();

            if (!string.IsNullOrEmpty(param.Search))
            {
                assets = assets.Where(a => a.Name.Contains(param.Search) ||
                                    a.AssetTagId.ToString().Contains(param.Search) ||
                                    a.Brand.Contains(param.Search) ||
                                    a.SerialNo.Contains(param.Search));
            }

            var nullActivity = await _assetRepo.GetByExpressionAsync(x => x.Id == 1);

            var result = Result<Asset>.CreatePagination(assets, param.PageIndex, param.PageSize);

            var assetToReturn = _mapper.Map<IReadOnlyList<Asset>, IReadOnlyList<AssetToReturnDto>>(result.Values);

            return Ok(new Pagination<AssetToReturnDto>(result.Pagination.PageSize, result.Pagination.PageIndex, result.Pagination.Total, assetToReturn));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetToReturnDto>> GetAsset(int id)
        {
            //var result = await _assetRepo.GetByIdAsync(id);

            var assetWithEventandPhotos = await _assetRepo.GetQueryAble()
            .Include(x => x.Events.Where(x => x.AssetId == id && x.IsActive == true))
            .Include(x => x.Photos.Where(x => x.AssetId == id && x.IsMain == true)).Where(x => x.Id == id).FirstOrDefaultAsync();
            //.ProjectTo<AssetToReturnDto>(_mapper.ConfigurationProvider).Where(x => x.Id == id).FirstOrDefaultAsync();

            var assetToReturn = _mapper.Map<Asset, AssetToReturnDto>(assetWithEventandPhotos);

            return Ok(assetToReturn);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<AssetToReturnDto>> AddAsset([FromForm] AssetToCreateDto assetDto)
        {
            var OrgFileName = "";
            var photoUrl = "";

            if (assetDto.PurchasedDate != "null")
            {
                assetDto.PurchasedDate = DateTime.ParseExact(assetDto.PurchasedDate.ToString(), "ddd MMM dd yyyy HH:mm:ss 'GMT+0600 (Bangladesh Standard Time)'",
                    CultureInfo.InvariantCulture).ToString();
            }

            if (assetDto.WarrantyExpired != "null")
            {
                assetDto.WarrantyExpired = DateTime.ParseExact(assetDto.WarrantyExpired.ToString(), "ddd MMM dd yyyy HH:mm:ss 'GMT+0600 (Bangladesh Standard Time)'",
                    CultureInfo.InvariantCulture).ToString();
            }

            //var assetMap = _mapper.Map<AssetToCreateDto, Asset>(assetDto);

            //var asset = await _assetRepo.Add(assetMap);

            // if (await _assetRepo.SaveChangesAsync() <= 0)
            // {
            //     return BadRequest("Somethings went wrong");
            // }


            //if files found
            if (assetDto.File != null)
            {

                var folderName = Path.Combine("assetsimages", "images");
                var pathToSave = Path.Combine(_config["RootFilePath"], "");

                foreach (var file in assetDto.File)
                {
                    OrgFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim().ToString();


                    string fileExtension = Path.GetExtension(OrgFileName);
                    string fileNamePart = OrgFileName.Replace(fileExtension, "");

                    OrgFileName = (fileNamePart + fileExtension).Replace("\"", "");

                    var filePath = Path.Combine(_config["RootFilePath"], OrgFileName);

                    // var photo = new Photo
                    // {
                    //     Url = "content/" + OrgFileName,
                    //     IsMain = true,
                    //     Asset = asset
                    // };

                    // var assetPhotos = await _photoRepo.FindListAllAsync(x => x.AssetId == asset.Id);

                    // if (assetPhotos.Count == 0)
                    // {
                    //     photo.IsMain = true;
                    // }

                    //var photoToSave = await _photoRepo.Add(photo);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    photoUrl = "content/" + OrgFileName;


                }



                //if (await _photoRepo.SaveChangesAsync() <= 0) return BadRequest("Photo cannot saved");

            }

            var asset = _mapper.Map<AssetToCreateDto, Asset>(assetDto);




            var assetFromDb = await _assetService.CreateAsset(asset, photoUrl);

            var assetwithDetails = await _assetRepo.GetQueryAble()
                       .Include(x => x.Photos).Where(x => x.Id == assetFromDb.Id).FirstOrDefaultAsync();

            var assetToReturn = _mapper.Map<Asset, AssetToReturnDto>(assetwithDetails);

            return Ok(assetToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AssetToReturnDto>> UpdateAsset(int id, [FromBody] AssetToEditDto assetDto)
        {

            if (id > 0)
            {
                var assetFromDb = await _assetRepo.GetByIdAsync(id);

                if (assetFromDb == null) return BadRequest("Site Doesnot Found");
                assetDto.Id = assetFromDb.Id;
                assetDto.CompanyId = 1;
                _mapper.Map(assetDto, assetFromDb);

                //var company = _mapper.Map<CompanyCreateDto,Company>(companyDto);

                //_companyRepo.Update(company);

                if (await _assetRepo.SaveChangesAsync() <= 0)
                {
                    return BadRequest("Somethings went wrong");
                }
            }


            // var assetReturn = await _assetRepo.GetByIdAsync(id);

            // if (assetReturn == null) return BadRequest("Asset Doesnot Found");

            // var assetToReturn = _mapper.Map<Asset, AssetToReturnDto>(assetReturn);

            var assetWithPhotos = await _assetRepo.GetQueryAble()
            .Include(x => x.Events.Where(x => x.AssetId == id && x.IsActive == true))
            .Include(x => x.Photos.Where(x => x.AssetId == id && x.IsMain == true)).Where(x => x.Id == id).FirstOrDefaultAsync();

            var assetToReturn = _mapper.Map<Asset, AssetToReturnDto>(assetWithPhotos);

            return Ok(assetToReturn);
        }


        [HttpPut("event/{id}")]
        public async Task<ActionResult<AssetToReturnDto>> LostAsset(int id, [FromBody] EventToUpdateDto eventDto)
        {


            var assetFromDb = await _assetRepo.GetByIdAsync(id);

            if (assetFromDb == null) return BadRequest("Asset Doesnot Found");


            var assetEvents = await _eventRepo.FindListAllAsync(x => x.AssetId == id);

            var currentStatus = assetEvents.FirstOrDefault(x => x.IsActive);

            currentStatus.IsActive = false;

            _eventRepo.Update(currentStatus);

            var newEvent = _mapper.Map<EventToUpdateDto, Event>(eventDto);

            newEvent.IsActive = true;
            newEvent.Asset = assetFromDb;
            // var newEvent = new Event
            // {
            //     EventStatus = EventStatus.Lost,
            //     IsActive = true,
            //     EventDate = DateTime.ParseExact(eventDto.LostDate.ToString(), "ddd MMM dd yyyy HH:mm:ss 'GMT+0600 (Bangladesh Standard Time)'",
            //         CultureInfo.InvariantCulture),
            //     Asset = assetFromDb,
            //     Note = eventDto.Notes

            // };

            var eventToSave = _eventRepo.Add(newEvent);



            if (await _eventRepo.SaveChangesAsync() <= 0)
            {
                return BadRequest("Somethings went wrong");
            }

            var assetWithPhotos = await _assetRepo.GetQueryAble()
            .Include(x => x.Events.Where(x => x.AssetId == id && x.IsActive == true))
            .Include(x => x.Photos.Where(x => x.AssetId == id && x.IsMain == true)).Where(x => x.Id == id).FirstOrDefaultAsync();

            var assetToReturn = _mapper.Map<Asset, AssetToReturnDto>(assetWithPhotos);

            return Ok(assetToReturn);
        }

        [HttpPut("found/{id}")]
        public async Task<ActionResult<AssetToReturnDto>> FoundAsset(int id, [FromBody] EventToUpdateDto eventDto)
        {


            var assetFromDb = await _assetRepo.GetByIdAsync(id);

            if (assetFromDb == null) return BadRequest("Asset Doesnot Found");


            var assetEvents = await _eventRepo.FindListAllAsync(x => x.AssetId == id);

            var currentStatus = assetEvents.FirstOrDefault(x => x.IsActive);

            currentStatus.IsActive = false;

            _eventRepo.Update(currentStatus);

            var newEvent = _mapper.Map<EventToUpdateDto, Event>(eventDto);

            newEvent.IsActive = true;
            newEvent.EventStatus = EventStatus.Available;
            newEvent.Asset = assetFromDb;
            // var newEvent = new Event
            // {
            //     EventStatus = EventStatus.Lost,
            //     IsActive = true,
            //     EventDate = DateTime.ParseExact(eventDto.LostDate.ToString(), "ddd MMM dd yyyy HH:mm:ss 'GMT+0600 (Bangladesh Standard Time)'",
            //         CultureInfo.InvariantCulture),
            //     Asset = assetFromDb,
            //     Note = eventDto.Notes

            // };

            var eventToSave = _eventRepo.Add(newEvent);



            if (await _eventRepo.SaveChangesAsync() <= 0)
            {
                return BadRequest("Somethings went wrong");
            }

            var assetWithPhotos = await _assetRepo.GetQueryAble()
            .Include(x => x.Events.Where(x => x.AssetId == id && x.IsActive == true))
            .Include(x => x.Photos.Where(x => x.AssetId == id && x.IsMain == true)).Where(x => x.Id == id).FirstOrDefaultAsync();

            var assetToReturn = _mapper.Map<Asset, AssetToReturnDto>(assetWithPhotos);

            return Ok(assetToReturn);
        }

    }
}