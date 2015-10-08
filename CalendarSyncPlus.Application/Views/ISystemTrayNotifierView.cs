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
//  *      Created On:     20-02-2015 2:05 PM
//  *      Modified On:    20-02-2015 2:05 PM
//  *      FileName:       ISystemTrayNotifierView.cs
//  * 
//  *****************************************************************************/

#endregion

using System.Waf.Applications;

namespace CalendarSyncPlus.Application.Views
{
    public interface ISystemTrayNotifierView : IView
    {
        void ShowCustomBalloon();
        void ShowCustomBalloon(int timeoutInMilliseconds);
        void CloseBalloon();
        void Quit();
    }
}