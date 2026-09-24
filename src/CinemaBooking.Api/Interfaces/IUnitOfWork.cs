using CinemaBooking.Api.Models;

namespace CinemaBooking.Api.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}