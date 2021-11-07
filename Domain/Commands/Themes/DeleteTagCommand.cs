﻿using MediatR;

namespace Domain.Commands.Themes
{
	public class DeleteTagCommand : IRequest<bool>
	{
		public int TagId { get; set; }
	}
}
