﻿#region File Header

// /******************************************************************************
//  * 
//  *      Copyright (C) Ankesh Dave 2015 All Rights Reserved. Confidential
//  * 
//  ******************************************************************************
//  * 
//  *      Project:        CalendarSyncPlus
//  *      SubProject:     CalendarSyncPlus.Application
//  *      Author:         Dave, Ankesh
//  *      Created On:     04-02-2015 2:18 PM
//  *      Modified On:    04-02-2015 2:39 PM
//  *      FileName:       SettingsProvider.cs
//  * 
//  *****************************************************************************/

#endregion

#region Imports

using System.ComponentModel.Composition;
using CalendarSyncPlus.Domain.Models;
using CalendarSyncPlus.Domain.Models.Preferences;
using CalendarSyncPlus.Services.Interfaces;

#endregion

namespace CalendarSyncPlus.Services
{
    [Export(typeof (ISettingsProvider))]
    public class SettingsProvider : ISettingsProvider
    {
        #region Constructors

        [ImportingConstructor]
        public SettingsProvider(ISettingsSerializationService settingsSerializationService)
        {
            SettingsSerializationService = settingsSerializationService;
        }

        #endregion

        #region Properties

        public ISettingsSerializationService SettingsSerializationService { get; set; }

        #endregion

        #region ISettingsProvider Members

        public Settings GetSettings()
        {
            return SettingsSerializationService.DeserializeSettings();
        }

        #endregion
    }
}