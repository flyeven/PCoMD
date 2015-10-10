using System;

namespace CalendarSyncPlus.Domain.Models.Preferences
{
    [Serializable]
    public class CalendarSyncSettings : SyncSettings
    {
        private bool _disableDelete;
        private bool _confirmOnDelete;
        private bool _keepLastModifiedVersion;
        /// <summary>
        /// </summary>
        public bool DisableDelete
        {
            get { return _disableDelete; }
            set { SetProperty(ref _disableDelete, value); }
        }

        public bool ConfirmOnDelete
        {
            get { return _confirmOnDelete; }
            set { SetProperty(ref _confirmOnDelete, value); }
        }

        public bool KeepLastModifiedVersion
        {
            get { return _keepLastModifiedVersion; }
            set { SetProperty(ref _keepLastModifiedVersion, value); }
        }

        public static CalendarSyncSettings GetDefault()
        {
            return new CalendarSyncSettings
            {
                SyncRangeType = SyncRangeTypeEnum.SyncRangeInDays,
               // [CFL] sets default to (-1, 30)
                //DaysInFuture = 120,
                //DaysInPast = 120,
                DaysInFuture = 30,
                DaysInPast = 1,
                StartDate = DateTime.Today.AddDays(-(1)),
                EndDate = DateTime.Today.AddDays(30),
            };
        }
    }
}