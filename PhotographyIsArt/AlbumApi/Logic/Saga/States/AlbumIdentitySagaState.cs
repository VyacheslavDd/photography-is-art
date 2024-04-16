using MassTransit;

namespace AlbumApi.Logic.Saga.States
{
	public class AlbumIdentitySagaState : SagaStateMachineInstance
	{
		public  Guid CorrelationId {  get; set; }
		public  State CurrentState { get; set; }
		public Guid AlbumId { get; set; }
		public Guid UserId { get; set; }
	}
}
