// -----------------------------------------------------------------------
// <copyright file="WebServiceEndpoint.cs" company="-">
// Copyright (c) 2014 Eser Ozvataf (eser@sent.com). All rights reserved.
// Web: http://eser.ozvataf.com/ GitHub: http://github.com/larukedi
// </copyright>
// <author>Eser Ozvataf (eser@sent.com)</author>
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

namespace Tasslehoff.Library.WebServices
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// WebServiceEndpoint class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class WebServiceEndpoint
    {
        // fields

        /// <summary>
        /// The name
        /// </summary>
        [DataMember(Name = "Name")]
        private string name;

        /// <summary>
        /// The type
        /// </summary>
        [DataMember(Name = "Type")]
        private Type type;

        // constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebServiceEndpoint" /> class.
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="type">The type</param>
        public WebServiceEndpoint(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }

        // properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [IgnoreDataMember]
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [IgnoreDataMember]
        public Type Type
        {
            get
            {
                return this.type;
            }
        }
    }
}
