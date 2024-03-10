using AlbumApi.Api.Controllers.Albums.RequestModels;
using AlbumApi.Api.Controllers.Albums.ResponseModels;
using AlbumApi.Api.Controllers.Tags.ResponseModels;
using AlbumApi.Dal.Filters;
using AlbumApi.Logic.Albums.Interfaces;
using AlbumApi.Logic.Albums.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Dal.Constants;
using WebApiCore.Libs.AlbumUserConnectionService.Interfaces;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Requests;
using WebApiCore.Logic.Base.Interfaces;

namespace AlbumApi.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlbumsController : ControllerBase
	{
		private readonly IAlbumService _albumService;
		private readonly IAlbumUserConnectionService _albumUserConnectionService;
		private readonly IMapper _mapper;

		public AlbumsController(IAlbumService albumService, IMapper mapper, IAlbumUserConnectionService albumUserConnectionService)
		{
			_albumService = albumService;
			_mapper = mapper;
			_albumUserConnectionService = albumUserConnectionService;
		}

		[HttpGet]
		[Route("all")]
		[ProducesResponseType(typeof(GetAlbumResponse), 200)]
		///<summary>
		///Получение всех альбомов, не включая теги к ним
		///</summary>
		public async Task<IActionResult> GetAllAsync([FromQuery] AlbumQueryFilter filter)
		{
			var data = await _albumService.GetAllAsync(filter);
			var mappedData = _mapper.Map<List<GetAlbumResponse>>(data);
			return Ok(mappedData);
		}

		[HttpGet]
		[Route("{guid}")]
		[ProducesResponseType(typeof(GetAlbumResponse), 200)]
		///<summary>
		///Получение определённого альбома, включая имеющиеся теги
		///</summary>
		public async Task<IActionResult> GetByGuidAsync([FromRoute] Guid guid)
		{
			var data = await _albumService.GetByGuidAsync(guid);
			var mappedData = _mapper.Map<GetAlbumResponse>(data);
			mappedData.Tags = _mapper.Map<List<GetTagResponse>>(data.Tags);
			return Ok(mappedData);
		}

		[HttpPost]
		[Route("create")]
		[ProducesResponseType(typeof(CreateAlbumResponse), 201)]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> AddAsync([FromForm] CreateAlbumRequest request)
		{
			await _albumUserConnectionService.CheckUserExistenseAsync(new CheckUserExistenceRequest() { UserId = request.UserId });
			var logicModel = _mapper.Map<AlbumLogic>(request);
			var modelGuid = await _albumService.AddAsync(logicModel, request.SelectedTags, request.AlbumCoverImage, request.AlbumPictures);
			return Ok(new CreateAlbumResponse() {
				Guid = modelGuid
			});
		}

		[HttpPut]
		[Route("{guid}/update")]
		[ProducesResponseType(200)]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> UpdateAsync([FromRoute] Guid guid, [FromForm] UpdateAlbumRequest request)
		{
			var logicModel = _mapper.Map<AlbumLogic>(request);
			await _albumService.UpdateAsync(guid, logicModel, request.SelectedTags, request.AlbumCoverImage, request.AlbumPictures);
			return Ok();
		}

		[HttpDelete]
		[Route("{guid}/delete")]
		[ProducesResponseType(200)]
		public async Task<IActionResult> DeleteAsync([FromRoute] Guid guid)
		{
			await _albumService.DeleteAsync(guid);
			return Ok();
		}
	}
}
