﻿#region File Header

// /******************************************************************************
//  * 
//  *      Copyright (C) Ankesh Dave 2015 All Rights Reserved. Confidential
//  * 
//  ******************************************************************************
//  * 
//  *      Project:        CalendarSyncPlus
//  *      SubProject:     CalendarSyncPlus.Domain
//  *      Author:         Dave, Ankesh
//  *      Created On:     11-02-2015 10:09 AM
//  *      Modified On:    11-02-2015 10:13 AM
//  *      FileName:       OutlookMailBox.cs
//  * 
//  *****************************************************************************/

#endregion

#region Imports

using System;
using System.Collections.Generic;
using System.Waf.Foundation;

#endregion

namespace CalendarSyncPlus.Domain.Models.Preferences
{
    [Serializable]
    public class OutlookMailBox : Model
    {
        public OutlookMailBox()
        {
            _folders = new List<OutlookFolder>();
        }

        #region Fields

        private List<OutlookFolder> _folders;
        private string _entryId;
        private string _storeId;
        private string name;

        #endregion

        #region Properties

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public List<OutlookFolder> Folders
        {
            get { return _folders; }
            set { SetProperty(ref _folders, value); }
        }

        public string EntryId
        {
            get { return _entryId; }
            set { SetProperty(ref _entryId, value); }
        }

        public string StoreId
        {
            get { return _storeId; }
            set { SetProperty(ref _storeId, value); }
        }

        #endregion
    }
}