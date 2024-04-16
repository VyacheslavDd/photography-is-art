using AlbumApi.Logic.Saga.Models;
using MassTransit;

namespace AlbumApi.Logic.Saga.States
{
	public class AlbumIdentitySagaStateMachine : MassTransitStateMachine<AlbumIdentitySagaState>
	{
		public State Proceeding {  get; set; }
		public State Success {  get; set; }
		public State Failure { get; set; }

		public Event<CreateAlbumSagaRequest> AlbumCreationRequested { get; set; }
		public Event<CreateAlbumSagaResponse> AlbumCreated { get; set; }
		public Event <CreatingAlbumFailed> AlbumCreationFailed { get; set; }

		public AlbumIdentitySagaStateMachine()
		{
			InstanceState(state => state.CurrentState);

			Event(() => AlbumCreationRequested, correlator => correlator.CorrelateById(context => context.Message.UserId));
			Event(() => AlbumCreated, correlator => correlator.CorrelateById(context => context.Message.UserId));
			Event(() => AlbumCreationFailed, correlator => correlator.CorrelateById(context => context.Message.UserId));

			Initially(
			When(AlbumCreationRequested)
				.Then(context =>
				{
					context.Instance.AlbumId = context.Message.AlbumId;
					context.Instance.UserId = context.Message.UserId;
				})
				.Publish(ctx => new CreateAlbumSagaRequest() { AlbumId = ctx.Instance.AlbumId, UserId = ctx.Instance.UserId })
				.TransitionTo(Proceeding)
			);

			During(Proceeding,
				When(AlbumCreationFailed)
					.Then(context => throw new Exception(context.Message.Message))
					.TransitionTo(Failure)
			);

			During(Proceeding,
				When(AlbumCreated)
					.TransitionTo(Success)
			);

			SetCompletedWhenFinalized();
		}
	}
}