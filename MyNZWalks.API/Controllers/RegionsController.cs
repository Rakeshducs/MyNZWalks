using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNZWalks.API.Data;
using MyNZWalks.API.Models.Domain;
using MyNZWalks.API.Models.DTO;

namespace MyNZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext dbContext;
        public RegionsController(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                var regionDto = new RegionDto
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                };
                regionsDto.Add(regionDto);
            }

            return Ok(regionsDto);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Region
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetById),
                new { id = regionDomainModel.Id },
                new RegionDto
                {
                    Id = regionDomainModel.Id,
                    Name = regionDomainModel.Name,
                    Code = regionDomainModel.Code,
                    RegionImageUrl = regionDomainModel.RegionImageUrl
                });
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var existingRegion = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (existingRegion == null)
            {
                return NotFound();
            }
            existingRegion.Name = updateRegionRequestDto.Name;
            existingRegion.Code = updateRegionRequestDto.Code;
            existingRegion.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            dbContext.SaveChanges();
            var regionDto = new RegionDto
            {
                Id = existingRegion.Id,
                Name = existingRegion.Name,
                Code = existingRegion.Code,
                RegionImageUrl = existingRegion.RegionImageUrl
            };
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var existingRegion = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (existingRegion == null)
            {
                return NotFound();
            }
            dbContext.Regions.Remove(existingRegion);
            dbContext.SaveChanges();
            var regionDto = new RegionDto
            {
                Id = existingRegion.Id,
                Name = existingRegion.Name,
                Code = existingRegion.Code,
                RegionImageUrl = existingRegion.RegionImageUrl
            };
            return Ok(regionDto);
        }
    }
}
