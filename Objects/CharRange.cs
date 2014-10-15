﻿// -----------------------------------------------------------------------
// <copyright file="CharRange.cs" company="-">
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

namespace Tasslehoff.Library.Objects
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// CharRange class.
    /// </summary>
    [DataContract]
    public class CharRange : ICloneable, IEnumerable, ISerializable
    {
        // fields

        /// <summary>
        /// The start char
        /// </summary>
        [DataMember]
        private readonly char startChar;

        /// <summary>
        /// The end char
        /// </summary>
        [DataMember]
        private readonly char endChar;

        // constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CharRange" /> class.
        /// </summary>
        /// <param name="startChar">The start char</param>
        /// <param name="endChar">The end char</param>
        public CharRange(char startChar, char endChar)
        {
            this.startChar = startChar;
            this.endChar = endChar;
        }

        /// <summary>
        /// Constructor for serialization interface
        /// </summary>
        /// <param name="info">info</param>
        /// <param name="context">context</param>
        protected CharRange(SerializationInfo info, StreamingContext context)
        {
            this.startChar = info.GetChar("startChar");
            this.endChar = info.GetChar("endChar");
        }

        // properties

        /// <summary>
        /// Gets the start char.
        /// </summary>
        [IgnoreDataMember]
        public char StartChar
        {
            get
            {
                return this.startChar;
            }
        }

        /// <summary>
        /// Gets the end char.
        /// </summary>
        [IgnoreDataMember]
        public char EndChar
        {
            get
            {
                return this.endChar;
            }
        }

        // methods

        /// <summary>
        /// Gets the range table.
        /// </summary>
        /// <returns>Set of chars</returns>
        public char[] GetRangeTable()
        {
            int startChar = (int)this.startChar;
            int endChar = (int)this.endChar;
            char[] table = new char[endChar - startChar + 1];

            for (int i = 0; i < table.Length; i++)
            {
                table[i] = (char)(startChar + i);
            }

            return table;
        }

        /// <summary>
        /// Gets the range charset.
        /// </summary>
        /// <returns>Set of chars</returns>
        public string GetRangeCharset()
        {
            int startChar = (int)this.startChar;
            int endChar = (int)this.endChar;
            int length = endChar - startChar;
            string charset = string.Empty;

            for (int i = 0; i <= length; i++)
            {
                charset += (char)(startChar + i);
            }

            return charset;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            int startChar = (int)this.startChar;
            int endChar = (int)this.endChar;
            int length = endChar - startChar;

            for (int i = 0; i <= length; i++)
            {
                yield return (char)(startChar + i);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("startChar", this.startChar);
            info.AddValue("endChar", this.endChar);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            this.GetObjectData(info, context);
        }
    }
}