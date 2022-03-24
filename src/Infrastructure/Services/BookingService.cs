﻿using Application.DTOs.BookingDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Validations;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(AddBookingDTO bookingDTO)
        {
            Booking booking = _mapper.Map<Booking>(bookingDTO);
            // var startDb = _unitOfWork.Bookings.Search(x=>x.BookingStart==booking.BookingStart,false).FirstOrDefault();
            // var endDb = _unitOfWork.Bookings.Search(x=>x.BookingStart==booking.BookingEnd,false).FirstOrDefault();
            // if (startDb == null && endDb == null)
            // {
            //     await _unitOfWork.Bookings.AddAsync(booking);
            //     await _unitOfWork.CompleteAsync();
            //     return booking.Id;
            //
            // }
            // else if (BookingValidation.Validate(booking, startDb.BookingStart, endDb.BookingEnd))
            // {
            //     await _unitOfWork.Bookings.AddAsync(booking);
            //     await _unitOfWork.CompleteAsync();
            //     return booking.Id;
            // }
            var workPlace = _unitOfWork.Bookings.Search(x=>x.WorkPlaceId==booking.WorkPlaceId,false);
            if (workPlace.Any())
            {
                if (BookingValidation.Validate(booking))
                {
                    await _unitOfWork.Bookings.AddAsync(booking);
                    await _unitOfWork.CompleteAsync();
                    return booking.Id;
                }
            }
            else
            {
                foreach (var item in workPlace)
                {
                    if (BookingValidation.ValidateBookingDate(booking, item.BookingStart, item.BookingEnd) && BookingValidation.Validate(booking))
                    {
                        await _unitOfWork.Bookings.AddAsync(booking);
                        await _unitOfWork.CompleteAsync();
                        return booking.Id;
                    }
                }
                
            }
            return new Guid();
        }

        public async Task<PagedList<GetBookingDTO>> GetPagedAsync(PagedQueryBase query)
        {
            var bookings = await _unitOfWork.Bookings.GetPagedAsync(query);
            var mapBookings = _mapper.Map<List<GetBookingDTO>>(bookings);
            var bookingsDTO = new PagedList<GetBookingDTO>(mapBookings, bookings.TotalCount, bookings.CurrentPage, bookings.PageSize);
            return bookingsDTO;
        }

        public async Task<GetBookingDTO> GetByIdAsync(Guid Id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(Id);
            return _mapper.Map<GetBookingDTO>(booking);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(Id);
            if (booking == null)
                return false;

            _unitOfWork.Bookings.Remove(booking);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<PagedList<GetBookingDTO>> SearchByUserIdAsync(Guid userId, PagedQueryBase query)
        {
            var bookings = await _unitOfWork.Bookings.GetPagedByUserIdAsync(userId, query);
            var bookingMaps = _mapper.Map<List<GetBookingDTO>>(bookings);
            var bookingsDTO = new PagedList<GetBookingDTO>(bookingMaps, bookings.TotalCount, bookings.CurrentPage, bookings.PageSize);
            return bookingsDTO;
        }

        public async Task<PagedList<GetBookingDTO>> SearchByWorkPlaceIdAsync(Guid workPlaceId, PagedQueryBase query)
        {
            var bookings = await _unitOfWork.Bookings.GetPagedByWorkPlaceIdAsync(workPlaceId, query);
            var bookingMaps = _mapper.Map<List<GetBookingDTO>>(bookings);
            var bookingsDTO = new PagedList<GetBookingDTO>(bookingMaps, bookings.TotalCount, bookings.CurrentPage, bookings.PageSize);
            return bookingsDTO;
        }

        public async Task<GetBookingDTO> UpdateAsync(UpdateBookingDTO bookingDTO)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingDTO.Id);
            if (booking == null)
                return null;

            _mapper.Map(bookingDTO, booking);
            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetBookingDTO>(booking);
        }
    }
}
