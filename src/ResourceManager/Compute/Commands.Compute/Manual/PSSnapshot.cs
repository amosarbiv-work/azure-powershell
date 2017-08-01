// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

using Microsoft.Azure.Management.Compute.Models;
using System;
using System.Text.RegularExpressions;

namespace Microsoft.Azure.Commands.Compute.Automation.Models
{
	public partial class PSSnapshot : Snapshot
	{
		[Obsolete("This property is deprecated and will be removed in a future release")]
		public string AccountType
		{
			get
			{
				return Sku.Name.ToString();
			}
			set
			{
				Sku.Name = (StorageAccountTypes)Enum.Parse(typeof(StorageAccountTypes), value);
			}
		}

		[Obsolete("This property is deprecated and will be removed in a future release")]
		public string OwnerId
		{
            get
            {
                return ManagedBy;
            }
            set
            {
                OwnerId = value;
            }
        }
	}
}
