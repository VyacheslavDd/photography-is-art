using AlbumApi.Api.Controllers.Albums.RequestModels;
using AlbumApi.Api.Controllers.Albums.ResponseModels;
using AlbumApi.Api.Controllers.Tags.RequestModels;
using AlbumApi.Api.Controllers.Tags.ResponseModels;
using AlbumApi.Logic.Albums.Interfaces;
using AlbumApi.Logic.Albums.Models;
using AlbumApi.Logic.Tags.Interfaces;
using AlbumApi.Logic.Tags.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlbumApi.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TagsController : ControllerBase
	{
		private readonly IAlbumTagService _albumTagService;
		private readonly IMapper _mapper;

		public TagsController(IAlbumTagService albumTagService, IMapper mapper)
		{
			_albumTagService = albumTagService;
			_mapper = mapper;
		}

		[HttpGet]
		[Route("all")]
		[ProducesResponseType(typeof(GetTagResponse), 200)]
		///<summary>
		///Получение всех тегов, без включения альбомов для каждого из них
		///</summary>
		public async Task<IActionResult> GetAllAsync()
		{
			var data = await _albumTagService.GetAllAsync();
			var mapped = _mapper.Map<List<GetTagResponse>>(data);
			return Ok(mapped);
		}

		[HttpGet]
		[Route("{guid}")]
		[ProducesResponseType(typeof(GetTagResponse), 200)]
		///<summary>
		///Получение определённого тега, включая альбомы, принадлежащие данному тегу
		///</summary>
		public async Task<IActionResult> GetByGuidAsync([FromRoute] Guid guid)
		{
			var tag = await _albumTagService.GetByGuidAsync(guid);
			var mappedTag = _mapper.Map<GetTagResponse>(tag);
			//mappedTag.Albums = _mapper.Map<List<GetAlbumResponse>>(tag.Albums);
			return Ok(mappedTag);
		}

		[HttpPost]
		[Route("create")]
		[ProducesResponseType(typeof(CreateTagResponse), 201)]
		public async Task<IActionResult> AddAsync([FromBody] CreateTagRequest request)
		{
			var logicModel = _mapper.Map<AlbumTagLogic>(request);
			var modelGuid = await _albumTagService.AddAsync(logicModel);
			return Ok(new CreateTagResponse()
			{
				Guid = modelGuid
			});
		}

		[HttpPut]
		[Route("{guid}/update")]
		[ProducesResponseType(200)]
		public async Task<IActionResult> UpdateAsync([FromRoute] Guid guid, [FromBody] UpdateTagRequest request)
		{
			var logicModel = _mapper.Map<AlbumTagLogic>(request);
			await _albumTagService.UpdateAsync(guid, logicModel);
			return Ok();
		}

		[HttpDelete]
		[Route("{guid}/delete")]
		[ProducesResponseType(200)]
		public async Task<IActionResult> DeleteAsync([FromRoute] Guid guid)
		{
			await _albumTagService.DeleteAsync(guid);
			return Ok();
		}
	}
}
