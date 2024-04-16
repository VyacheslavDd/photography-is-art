
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MassTransit;

namespace AlbumApi.Logic.Saga.States
{
	public class AlbumIdentitySagaStateMap : SagaClassMap<AlbumIdentitySagaState>
	{
		protected override void Configure(EntityTypeBuilder<AlbumIdentitySagaState> entity, ModelBuilder model)
		{
			base.Configure(entity, model);
			entity.Property(prop => prop.CurrentState);
		}
	}
}
