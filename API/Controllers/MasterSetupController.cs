using API.Dto;
using API.Helper;
using AutoMapper;
using Core;
using Core.Interfaces;
using Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MasterSetupController : BaseApiController
    {
        private readonly IGenericRepository<Company> _companyRepo;
        private readonly IGenericRepository<Site> _siteRepo;
        private readonly IGenericRepository<Location> _locationRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<Department> _deptRepo;
        private readonly IMapper _mapper;

        public MasterSetupController(
            IGenericRepository<Company> companyRepo,
            IGenericRepository<Site> siteRepo,
            IGenericRepository<Location> locationRepo,
            IGenericRepository<Category> categoryRepo,
            IGenericRepository<Department> deptRepo,
            IMapper mapper)
        {
            _deptRepo = deptRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _locationRepo = locationRepo;
            _siteRepo = siteRepo;
            _companyRepo = companyRepo;
        }


        [HttpGet("companies")]
        public async Task<ActionResult<CompanyToReturnDto>> GetCompany([FromQuery] PaginationParams param)
        {
            var companies = _companyRepo.GetQueryAble();

             if(!string.IsNullOrEmpty(param.Search))
                {
                    companies = companies.Where(a => a.CompanyName.Contains(param.Search) ||
                                        a.Address.Contains(param.Search) ||
                                        a.City.Contains(param.Search) ||
                                        a.Mobile.Contains(param.Search));
                }

            var nullActivity = await _siteRepo.GetByExpressionAsync(x=>x.Id == 1);

            var result = Result<Company>.CreatePagination(companies, param.PageIndex, param.PageSize);

            var companyToReturn = _mapper.Map<IReadOnlyList<Company>,IReadOnlyList<CompanyToReturnDto>>(result.Values);

            return Ok(new Pagination<CompanyToReturnDto>(result.Pagination.PageSize,result.Pagination.PageIndex,result.Pagination.Total,companyToReturn));
        }

        [HttpGet("company/{id}")]
        public async Task<ActionResult<CompanyToReturnDto>> GetCompany(int id)
        {
            var result = await _companyRepo.GetByIdAsync(id);
            

            var companyToReturn = _mapper.Map<Company,CompanyToReturnDto>(result);

            return Ok(companyToReturn);
        }

        [HttpPost("update-company")]
        public async Task<ActionResult<CompanyToReturnDto>> CreateProduct(CompanyCreateDto companyDto)
        {

           if(companyDto.Id > 0)
           {
                var companyFromDb = await _companyRepo.GetByIdAsync(companyDto.Id);

                 if(companyFromDb==null) return BadRequest("Compnay Doesnot Found");

                var company = _mapper.Map<CompanyCreateDto,Company>(companyDto);

                _companyRepo.Update(company);

                if(await _companyRepo.SaveChangesAsync() <= 0)
                {
                    return BadRequest("Somethings went wrong");
                }
           }


            var companyReturn = await _companyRepo.GetByIdAsync(companyDto.Id);

            if(companyReturn==null) return BadRequest("Compnay Doesnot Found");

            var companyToReturn = _mapper.Map<Company,CompanyToReturnDto>(companyReturn);
            companyToReturn.Status = companyDto.Status;
            return Ok(companyToReturn);
        } 

        [HttpPut("update-company/{id}")]
        public async Task<ActionResult<CompanyToReturnDto>> UpdateCompany(int id,[FromBody]CompanyCreateDto companyDto)
        {

           if(id > 0)
           {
                var companyFromDb = await _companyRepo.GetByIdAsync(id);

                 if(companyFromDb==null) return BadRequest("Compnay Doesnot Found");
                companyDto.Id = companyFromDb.Id;
                 _mapper.Map(companyDto, companyFromDb);

                //var company = _mapper.Map<CompanyCreateDto,Company>(companyDto);

                //_companyRepo.Update(company);

                if(await _companyRepo.SaveChangesAsync() <= 0)
                {
                    return BadRequest("Somethings went wrong");
                }
           }


            var companyReturn = await _companyRepo.GetByIdAsync(id);

            if(companyReturn==null) return BadRequest("Compnay Doesnot Found");

            var companyToReturn = _mapper.Map<Company,CompanyToReturnDto>(companyReturn);
            companyToReturn.Status = companyDto.Status;
            return Ok(companyToReturn);
        } 

        [HttpGet("sites")]
        public async Task<ActionResult<Pagination<SiteToReturnDto>>> GetSites([FromQuery] PaginationParams param)
        {
            

            var sites = _siteRepo.GetQueryAble();

             if(!string.IsNullOrEmpty(param.Search))
                {
                    sites = sites.Where(a => a.Name.Contains(param.Search) ||
                                        a.Address.Contains(param.Search) ||
                                        a.City.Contains(param.Search) ||
                                        a.CopmanyId.ToString().Contains(param.Search) ||
                                        a.Mobile.Contains(param.Search));
                }

            var nullActivity = await _siteRepo.GetByExpressionAsync(x=>x.Id == 1);

            var result = Result<Site>.CreatePagination(sites, param.PageIndex, param.PageSize);

            var sitesToReturn = _mapper.Map<IReadOnlyList<Site>,IReadOnlyList<SiteToReturnDto>>(result.Values);

            return Ok(new Pagination<SiteToReturnDto>(result.Pagination.PageSize,result.Pagination.PageIndex,result.Pagination.Total,sitesToReturn));
        }

        [HttpGet("site/{id}")]
        public async Task<ActionResult<CompanyToReturnDto>> GetSite(int id)
        {
            var result = await _siteRepo.GetByIdAsync(id);
            

            var companyToReturn = _mapper.Map<Site,SiteToReturnDto>(result);

            return Ok(companyToReturn);
        }

        [HttpPost("add-site")]
        public async Task<ActionResult<SiteToReturnDto>> AddSite([FromBody]SiteCreateDto siteDto)
        {

                var siteMap = _mapper.Map<SiteCreateDto,Site>(siteDto);

                var site = await _siteRepo.Add(siteMap);

                if(await _siteRepo.SaveChangesAsync() <= 0)
                {
                    return BadRequest("Somethings went wrong");
                }
        
            var siteToReturn = _mapper.Map<Site,SiteToReturnDto>(site);
            return Ok(siteToReturn);
        } 

        [HttpPut("update-site/{id}")]
        public async Task<ActionResult<SiteToReturnDto>> UpdateSite(int id,[FromBody]SiteCreateDto siteDto)
        {

           if(id > 0)
           {
                var siteFromDb = await _siteRepo.GetByIdAsync(id);

                 if(siteFromDb==null) return BadRequest("Site Doesnot Found");
                siteDto.Id = siteFromDb.Id;
                siteDto.CopmanyId = 1;
                 _mapper.Map(siteDto, siteFromDb);

                //var company = _mapper.Map<CompanyCreateDto,Company>(companyDto);

                //_companyRepo.Update(company);

                if(await _siteRepo.SaveChangesAsync() <= 0)
                {
                    return BadRequest("Somethings went wrong");
                }
           }


            var siteReturn = await _siteRepo.GetByIdAsync(id);

            if(siteReturn==null) return BadRequest("Site Doesnot Found");

            var siteToReturn = _mapper.Map<Site,SiteToReturnDto>(siteReturn);
            return Ok(siteToReturn);
        } 

        [HttpDelete("delete-site/{id}")]
        public async Task<ActionResult<bool>> DeleteSite(int id)
        {
            var siteReturn = await _siteRepo.GetByIdAsync(id);

            if(siteReturn==null) return BadRequest("Site Doesnot Found");

           _siteRepo.Delete(siteReturn);

           return await _siteRepo.SaveChangesAsync() > 0;
        }

        [HttpGet("locations")]
        public async Task<ActionResult<Pagination<LocationToReturnDto>>> GetLocations([FromQuery] PaginationParams param)
        {
            

            var locations = _locationRepo.GetQueryAble();

            if(!string.IsNullOrEmpty(param.Search))
                {
                    locations = locations.Where(a => a.Name.Contains(param.Search) ||
                                        a.Description.Contains(param.Search) ||
                                        a.Site.Name.Contains(param.Search));
                }

            var nullActivity = await _siteRepo.GetByExpressionAsync(x=>x.Id == 1);

            var result = Result<Location>.CreatePagination(locations, param.PageIndex, param.PageSize);

            var locationToReturn = _mapper.Map<IReadOnlyList<Location>,IReadOnlyList<LocationToReturnDto>>(result.Values);

            return Ok(new Pagination<LocationToReturnDto>(result.Pagination.PageSize,result.Pagination.PageIndex,result.Pagination.Total,locationToReturn));
        }

        [HttpGet("categories")]
        public async Task<ActionResult<Pagination<LocationToReturnDto>>> GetCategories([FromQuery] PaginationParams param)
        {
            

            var categories = _categoryRepo.GetQueryAble();

            if(!string.IsNullOrEmpty(param.Search))
                {
                    categories = categories.Where(a => a.Name.Contains(param.Search) ||
                                        a.Description.Contains(param.Search) ||
                                        a.Name.Contains(param.Search));
                }

            var nullActivity = await _siteRepo.GetByExpressionAsync(x=>x.Id == 1);

            var result = Result<Category>.CreatePagination(categories, param.PageIndex, param.PageSize);

            var categoriesToReturn = _mapper.Map<IReadOnlyList<Category>,IReadOnlyList<CatetoryToReturnDto>>(result.Values);

            return Ok(new Pagination<CatetoryToReturnDto>(result.Pagination.PageSize,result.Pagination.PageIndex,result.Pagination.Total,categoriesToReturn));
        }

        [HttpGet("departments")]
        public async Task<ActionResult<Pagination<DepartmentToReturnDto>>> GetDepartments([FromQuery] PaginationParams param)
        {
            

            var departments = _deptRepo.GetQueryAble();

            if(!string.IsNullOrEmpty(param.Search))
                {
                    departments = departments.Where(a => a.Name.Contains(param.Search) ||
                                        a.Description.Contains(param.Search) ||
                                        a.Name.Contains(param.Search));
                }

            var nullActivity = await _siteRepo.GetByExpressionAsync(x=>x.Id == 1);

            var result = Result<Department>.CreatePagination(departments, param.PageIndex, param.PageSize);

            var departmentsToReturn = _mapper.Map<IReadOnlyList<Department>,IReadOnlyList<DepartmentToReturnDto>>(result.Values);

            return Ok(new Pagination<DepartmentToReturnDto>(result.Pagination.PageSize,result.Pagination.PageIndex,result.Pagination.Total,departmentsToReturn));
        }
    }
}