﻿using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Exceptions;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler(IMapper mapper, IAsyncRepository<Event> eventRepository) : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly IAsyncRepository<Event> _eventRepository = eventRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _eventRepository.GetByIdAsync(request.EventId) ?? throw new NotFoundException(nameof(Event), request.EventId);
            await _eventRepository.DeleteAsync(eventToDelete);

            return Unit.Value;
        }
    }
}
