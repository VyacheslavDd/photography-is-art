using AlbumApi.Api.Controllers.Albums.RequestModels;
using AlbumApi.Api.Controllers.Albums.ResponseModels;
using AlbumApi.Api.Controllers.Tags.RequestModels;
using AlbumApi.Api.Controllers.Tags.ResponseModels;
using AlbumApi.Dal.Albums.Models;
using AlbumApi.Dal.Tags.Models;
using AlbumApi.Logic.Albums.Models;
using AlbumApi.Logic.Tags.Models;
using AutoMapper;

namespace AlbumApi.Logic.Mappers
{
	public class AlbumApiMapper : Profile
	{
		public AlbumApiMapper()
		{
			CreateMap<AlbumDal, AlbumLogic>().ForMember(al => al.Guid, opt => opt.MapFrom(a => a.Id));
			CreateMap<AlbumTagDal, AlbumTagLogic>().ForMember(atl => atl.Guid, opt => opt.MapFrom(at => at.Id));
			CreateMap<AlbumLogic, GetAlbumResponse>();
			CreateMap<AlbumTagLogic, GetTagResponse>();
			CreateMap<CreateAlbumRequest, AlbumLogic>()
				.ForMember(al => al.CreationDate, opt => opt.MapFrom(r => DateTime.Now.ToShortDateString()))
				.ForMember(al => al.LastRedactedDate, opt => opt.MapFrom(r => DateTime.Now.ToShortDateString()));
			CreateMap<UpdateAlbumRequest, AlbumLogic>()
				.ForMember(al => al.LastRedactedDate, opt => opt.MapFrom(r => DateTime.Now.ToShortDateString()));
			CreateMap<CreateTagRequest, AlbumTagLogic>();
			CreateMap<UpdateTagRequest, AlbumTagLogic>();
			CreateMap<AlbumLogic, AlbumDal>();
			CreateMap<AlbumTagLogic, AlbumTagDal>();
		}
	}
}
