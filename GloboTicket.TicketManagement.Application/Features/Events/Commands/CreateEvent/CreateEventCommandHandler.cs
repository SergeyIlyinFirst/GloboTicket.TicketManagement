﻿using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Models.Mail;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService, ILogger<CreateEventCommandHandler> logger) : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository = eventRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailService = emailService;
        private readonly ILogger<CreateEventCommandHandler> _logger = logger;

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @event = _mapper.Map<Event>(request);


            @event = await _eventRepository.AddAsync(@event);

            var email = new Email() { To = "gill@snowball.be", Body = $"A new event was created: {request}", Subject = "A new event was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
            }

            return @event.EventId;
        }
    }
}