using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlightApp.Application.Features.Commands;
using FlightApp.Application.Features.Queries.GetUserReservations;
using System.Security.Claims;
using System.Threading.Tasks;
using FlightApp.Application.Features.Commands.ReservationCommand;

namespace FlightApp.WebUi.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IMediator _mediator;

        public ReservationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var query = new GetUserReservationsQuery { UserId = userId };
            var result = await _mediator.Send(query);

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CancelReservation(Guid reservationId)
        {
            var command = new CancelReservationCommand { ReservationId = reservationId };
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "An error occurred while cancelling the reservation.");
            return RedirectToAction("Index");
        }
    }
}
