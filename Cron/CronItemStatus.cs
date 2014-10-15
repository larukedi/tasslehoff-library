﻿// -----------------------------------------------------------------------
// <copyright file="CronItemStatus.cs" company="-">
// Copyright (c) 2013 larukedi (eser@sent.com). All rights reserved.
// </copyright>
// <author>larukedi (http://github.com/larukedi/)</author>
// -----------------------------------------------------------------------

//// This program is free software: you can redistribute it and/or modify
//// it under the terms of the GNU General Public License as published by
//// the Free Software Foundation, either version 3 of the License, or
//// (at your option) any later version.
//// 
//// This program is distributed in the hope that it will be useful,
//// but WITHOUT ANY WARRANTY; without even the implied warranty of
//// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//// GNU General Public License for more details.
////
//// You should have received a copy of the GNU General Public License
//// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Tasslehoff.Library.Cron
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// CronItemStatus enumeration.
    /// </summary>
    [Serializable]
    [DataContract]
    public enum CronItemStatus
    {
        /// <summary>
        /// Not started
        /// </summary>
        [EnumMember]
        NotStarted = 0,

        /// <summary>
        /// Item is running
        /// </summary>
        [EnumMember]
        Running = 1,

        /// <summary>
        /// Item is stopped
        /// </summary>
        [EnumMember]
        Stopped = 2
    }
}
