
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalendarSyncPlus.Domain.Models;
using CalendarSyncPlus.Domain.Wrappers;

namespace CalendarSyncPlus.Services.Calendars.Interfaces
{
    public interface ICalendarService
    {
        string CalendarServiceName { get; }

        Task<AppointmentsWrapper> DeleteCalendarEvents(List<Appointment> calendarAppointments,
            IDictionary<string, object> calendarSpecificData);

        Task<AppointmentsWrapper> GetCalendarEventsInRangeAsync(DateTime startDate, DateTime endDate,
            IDictionary<string, object> calendarSpecificData);

        // [CFL] remove the 'addDescription' & 'addAttendees' options
        Task<AppointmentsWrapper> AddCalendarEvents(List<Appointment> calendarAppointments, 
            bool addReminder, bool attendeesToDescription,IDictionary<string, object> calendarSpecificData);

        void CheckCalendarSpecificData(IDictionary<string, object> calendarSpecificData);


        // [CFL] remove the 'addDescription' & 'addAttendees' options
        Task<AppointmentsWrapper> UpdateCalendarEvents(List<Appointment> calendarAppointments, 
            bool addReminder, bool attendeesToDescription, IDictionary<string, object> calendarSpecificData);

        Task<bool> ClearCalendar(IDictionary<string, object> calendarSpecificData);

        Task<bool> ResetCalendarEntries(IDictionary<string, object> calendarSpecificData);
    }
}