// -----------------------------------------------------------------------
// <copyright file="DatabaseManager.cs" company="-">
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

namespace Tasslehoff.Library.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// DatabaseManager class.
    /// </summary>
    [Serializable]
    [DataContract]
    public class DatabaseManager
    {
        // fields

        /// <summary>
        /// Default database key.
        /// </summary>
        [DataMember(Name = "DefaultDatabaseKey")]
        private string defaultDatabaseKey;

        /// <summary>
        /// Connections
        /// </summary>
        [DataMember(Name = "Connections")]
        private Dictionary<string, DatabaseManagerConnection> connections;

        /// <summary>
        /// Database instances
        /// </summary>
        [NonSerialized]
        [IgnoreDataMember]
        private Dictionary<string, Database> databaseInstances;

        /// <summary>
        /// Queries
        /// </summary>
        [DataMember(Name = "Queries")]
        private Dictionary<string, DatabaseManagerQuery> queries;

        /// <summary>
        /// Query placeholders.
        /// </summary>
        [DataMember(Name = "QueryPlaceholders")]
        private Dictionary<string, string> queryPlaceholders;

        // constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseManager"/> class.
        /// </summary>
        public DatabaseManager()
        {
            this.connections = new Dictionary<string, DatabaseManagerConnection>();
            this.databaseInstances = new Dictionary<string, Database>();
            this.queries = new Dictionary<string, DatabaseManagerQuery>();
            this.queryPlaceholders = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseManager"/> class.
        /// </summary>
        /// <param name="defaultDatabaseKey">Key for default database</param>
        public DatabaseManager(string defaultDatabaseKey)
            : this()
        {
            this.defaultDatabaseKey = defaultDatabaseKey;
        }

        // events

        /// <summary>
        /// Occurs when [on getting a query].
        /// </summary>
        public event EventHandler OnQueryGet;

        // properties

        /// <summary>
        /// Gets or Sets default database key.
        /// </summary>
        [IgnoreDataMember]
        public string DefaultDatabaseKey
        {
            get
            {
                return this.defaultDatabaseKey;
            }

            set
            {
                this.defaultDatabaseKey = value;
            }
        }

        /// <summary>
        /// Gets or Sets connections.
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<string, DatabaseManagerConnection> Connections
        {
            get
            {
                return this.connections;
            }

            set
            {
                this.connections = value;
            }
        }

        /// <summary>
        /// Gets or Sets database instances.
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<string, Database> DatabaseInstances
        {
            get
            {
                return this.databaseInstances;
            }

            set
            {
                this.databaseInstances = value;
            }
        }

        /// <summary>
        /// Gets or Sets queries.
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<string, DatabaseManagerQuery> Queries
        {
            get
            {
                return this.queries;
            }

            set
            {
                this.queries = value;
            }
        }

        /// <summary>
        /// Gets or Sets query placeholders.
        /// </summary>
        [IgnoreDataMember]
        public Dictionary<string, string> QueryPlaceholders
        {
            get
            {
                return this.queryPlaceholders;
            }

            set
            {
                this.queryPlaceholders = value;
            }
        }

        // methods

        /// <summary>
        /// Returns a Database instance which is requested by the database key
        /// </summary>
        /// <param name="databaseKey">Database key</param>
        /// <returns>Database instance</returns>
        public virtual Database GetDatabase(string databaseKey = null)
        {
            if (databaseKey == null)
            {
                databaseKey = this.DefaultDatabaseKey;
            }

            if (!this.DatabaseInstances.ContainsKey(databaseKey))
            {
                DatabaseManagerConnection database = this.Connections[databaseKey];
                this.DatabaseInstances[databaseKey] = new Database(database.Driver, database.ConnectionString);
            }

            return this.DatabaseInstances[databaseKey];
        }

        /// <summary>
        /// Returns a DataQuery instance which is requested by the query key
        /// </summary>
        /// <param name="queryKey">Query key</param>
        /// <param name="replacements">Replacements</param>
        /// <returns>DataQuery instance</returns>
        public virtual DataQuery GetQuery(string queryKey, Dictionary<string, string> replacements = null)
        {
            return this.GetQuery(this.DefaultDatabaseKey, queryKey, replacements);
        }

        /// <summary>
        /// Returns a DataQuery instance which is requested by the query key
        /// </summary>
        /// <param name="databaseKey">Database key</param>
        /// <param name="queryKey">Query key</param>
        /// <param name="replacements">Replacements</param>
        /// <returns>DataQuery instance</returns>
        public virtual DataQuery GetQuery(string databaseKey, string queryKey, Dictionary<string, string> replacements = null)
        {
            return this.GetQuery(this.GetDatabase(databaseKey), queryKey, replacements);
        }

        /// <summary>
        /// Returns a DataQuery instance which is requested by the query key
        /// </summary>
        /// <param name="database">Database</param>
        /// <param name="queryKey">Query key</param>
        /// <param name="replacements">Replacements</param>
        /// <returns>DataQuery instance</returns>
        public virtual DataQuery GetQuery(Database database, string queryKey, Dictionary<string, string> replacements = null)
        {
            if (this.OnQueryGet != null)
            {
                this.OnQueryGet(this, EventArgs.Empty);
            }

            DatabaseManagerQuery query = this.Queries[queryKey];

            DataQuery dataQuery = database.NewQuery()
                                    .SetSqlString(query.SqlCommand)
                                    .AddPlaceholders(this.QueryPlaceholders);

            if (replacements != null)
            {
                dataQuery.AddPlaceholders(replacements);
            }

            return dataQuery;
        }
    }
}
