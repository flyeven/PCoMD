﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarSyncPlus.Domain.Models.Preferences
{
    /// <summary>
    /// Contact sync profile
    /// </summary>
    [Serializable]
    public class ContactSyncProfile : SyncProfile
    {
        private ContactSyncSettings _syncSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        public ContactSyncProfile()
        {
            Name = "Default Contact Profile";
            GoogleSettings = new GoogleSettings();
            ExchangeServerSettings = new ExchangeServerSettings();
            OutlookSettings = new OutlookSettings();
            IsSyncEnabled = true;
            IsDefault = true;
        }

        public ContactSyncSettings SyncSettings
        {
            get { return _syncSettings; }
            set { SetProperty(ref _syncSettings, value); }
        }

        public static ContactSyncProfile GetDefaultSyncProfile()
        {
            var syncProfile = new ContactSyncProfile()
            {
                SyncSettings = ContactSyncSettings.GetDefault(),
                OutlookSettings =
                {
                    OutlookOptions = OutlookOptionsEnum.OutlookDesktop |
                                        OutlookOptionsEnum.DefaultProfile |
                                     OutlookOptionsEnum.DefaultMailBoxCalendar
                },
                SyncDirection = SyncDirectionEnum.OutlookGoogleOneWay,
                SyncFrequency = new IntervalSyncFrequency { Hours = 1, Minutes = 0, StartTime = DateTime.Now }
            };
            syncProfile.SetSourceDestTypes();
            return syncProfile;
        }
    }
}
