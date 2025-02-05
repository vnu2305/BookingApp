﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum UserState
    {
        NotAuthorized,
        SelectingAction,

        // Booking creating process states
        SelectingCountry,
        SelectingCity,
        SelectingOffice,
        SelectingBookingType,
        SelectingBookingRecurringType,
        SelectingParkingPlace,
        SelectingSpecificWorkPlace,
        FinishingBooking,

        // SpecifyingWorkPlace
            // Map
        SpecifyingWorkPlaceSelectingExactMap,
        SpecifyingWorkPlaceSelectingMapFloor,
        SpecifyingWorkPlaceSelectingMapAttributes,
            // WorkPlace
        SpecifyingWorkPlaceSelectingExactWorkPlace,
        SpecifyingWorkPlaceSelectingWorkPlace,
        SpecifyingWorkPlaceSelectingWorkPlaceAttibutes,

        // Booking setting date states
            // One-day
        SelectingBookingDate,
            // Continuous
        SelectingBookingStartDate,
        SelectingBookingEndDate,
            // Recurring
        SelectingRecurringDays,


        ReviewingMyBookings,
        ReviewingBookingInfo,

        // Booking editing process states
        EditingBookingType,
        EditingBookingDate,
        EditingWorkPlace,
        EditingFloor,
        EditingOffice,
        EditingParkingPlace,
    }
}
