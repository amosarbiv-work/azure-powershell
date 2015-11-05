﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.Azure.Commands.Intune
{
    using System;
    using System.Management.Automation;
    using Management.Intune;
    using Management.Intune.Models;

    /// <summary>
    /// A cmdlet that creates a new iOS intune policy azure resource.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureRmIntuneiOSMAMPolicy", SupportsShouldProcess = true), OutputType(typeof(IOSMAMPolicy))]
    public sealed class NewIntuneiOSMAMPolicyCmdlet : IntuneBaseCmdlet
    {
        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The policy friendly name.")]
        [ValidateNotNullOrEmpty]
        public string FriendlyName { get; set; }

        /// <summary>
        /// The description of the policy
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "The policy description.")]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        /// <summary>
        /// The AppSharingFromLevel of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates whether the application is allowed to receive information shared by other applications.  Information can be restricted to no applications, only managed applications, or be allowed from all applications.")]
        [Alias("AppSharingFromLevel"), ValidateNotNullOrEmpty, ValidateSet("none", "policyManagedApps","allApps"), PSDefaultValue(Value = AppSharingType.none)]
        public AppSharingType AllowDataTransferToApps { get; set; }

        /// <summary>
        /// The AppSharingToLevel of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates whether the application is allowed to share information with other applications.  Information can be shared with no applications, only managed applications, or shared to all applications.")]
        [Alias("AppSharingToLevel"), ValidateNotNullOrEmpty, ValidateSet("none", "policyManagedApps", "allApps"), PSDefaultValue(Value = AppSharingType.none)]
        public AppSharingType AllowDataTransferFromApps{ get; set; }

        /// <summary>
        /// The Authentication of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates whether corporate credentials are required to access the application.")]
        [ValidateNotNullOrEmpty, ValidateSet("required","notRequired"), PSDefaultValue(Value = ChoiceType.required)]
        public ChoiceType Authentication { get; set; }

        /// <summary>
        /// The ClipboardSharingLevel of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates whether to restrict cut, copy and paste with other applications.")]
        [Alias("ClipboardSharingLevel"), ValidateNotNullOrEmpty, ValidateSet("blocked", "policyManagedApps", "policyManagedAppsWithPasteIn", "allApps"), PSDefaultValue(Value = ClipboardSharingLevelType.blocked)]
        public ClipboardSharingLevelType ClipboardSharing{ get; set; }

        /// <summary>
        /// The DataBackup of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates whether to prevent iTunes and iCloud backups.")]
        [ValidateNotNullOrEmpty, ValidateSet("allow", "block"), PSDefaultValue(Value = FilterType.allow)]
        public FilterType DataBackup { get; set; }

        /// <summary>
        /// The FileSharingSaveAs of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates whether to prevent ‘Save As’ from the application.")]
        [ValidateNotNullOrEmpty, ValidateSet("allow", "block"), PSDefaultValue(Value = FilterType.allow)]
        public FilterType FileSharingSaveAs { get; set; }

        /// <summary>
        /// The Pin of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates whether simple PIN is required to access the application.")]
        [ValidateNotNullOrEmpty, ValidateSet("required", "notRequired"), PSDefaultValue(Value = ChoiceType.required)]
        public ChoiceType Pin { get; set;}

        /// <summary>
        /// The PinNumRetry  of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "When a simple PIN is required to access the application, this indicates the number of attempts before a PIN reset.")]
        [Alias("PinNumRetry"), ValidateNotNullOrEmpty, ValidateRange(0, 200)]
        public int? PinRetries { get; set; }

        /// <summary>
        /// The DeviceCompliance  of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates whether managed applications are blocked from running on rooted or jailbroken devices.")]
        [ValidateNotNullOrEmpty, ValidateSet("enable","disable"), PSDefaultValue(Value = OptionType.enable)]
        public OptionType DeviceCompliance { get; set; }

        /// <summary>
        /// The ManagedBrowser of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates whether web content from the application is forced to run in the managed browser.")]
        [ValidateNotNullOrEmpty, ValidateSet("required", "notRequired"), PSDefaultValue(Value = ChoiceType.required)]
        public ChoiceType ManagedBrowser { get; set; }

        /// <summary>
        /// The AccessRecheckOfflineTimeout of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates how long to wait in minutes before re-checking access requirements on the device if the device is offline.")]
        [Alias("AccessRecheckOfflineTimeout"), ValidateNotNullOrEmpty]
        public int? RecheckAccessOfflineGracePeriodMinutes { get; set; }

        /// <summary>
        /// The AccessRecheckOnlineTimeout of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates how long to wait in minutes before re-checking access requirements on the device.")]
        [Alias("AccessRecheckOnlineTimeout")]
        public int? RecheckAccessTimeoutMinutes { get; set; }        
       
        /// <summary>
        /// The OfflineWipeTimeout of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates the number of days a device must be offline before application data is automatically wiped.")]
        [Alias("OfflineWipeTimeout")]
        public int? OfflineWipeIntervalDays { get; set; }

        /// <summary>
        /// The FileEncryptionLevel of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates the level of encryption for application data.")]
        [ValidateNotNullOrEmpty, PSDefaultValue(Value = DeviceLockType.deviceLocked)]
        public DeviceLockType FileEncryptionLevel { get; set; }

        /// <summary>
        /// The TouchId of the policy
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Indicates whether fingerprints are allowed instead of PIN to access the application.")]
        [Alias("TouchId"), ValidateNotNullOrEmpty, PSDefaultValue(Value = OptionType.enable, Help ="Defaults to 'enable'")]
        public OptionType AllowFingerprint { get; set; }
        
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true, HelpMessage = "Don't ask for confirmation.")]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Executes the cmdlet.
        /// </summary>
        protected override void ProcessRecord()
        {
            InitializeDefaultValuesForParams();

            var policyId = Guid.NewGuid().ToString();
            ValidateNumericParameters();
            this.ConfirmAction(
                this.Force,
                "Are you sure you want to create a new iOS policy:" + this.FriendlyName,
                "Creating the iOS policy resource.",
                policyId,
                () =>
                {
                    var policyObj = this.IntuneClient.Ios.CreateOrUpdateMAMPolicy(this.AsuHostName, policyId, PrepareIOSPolicyBody());
                    this.WriteObject(policyObj);
                });
 
        }

        /// <summary>
        /// Initialize the number & string parameters if the value is not provided as part of commandlet execution..
        /// Looks like Parameter PSDefaultValue() does not initialize integers & string params
        /// </summary>
        private void InitializeDefaultValuesForParams()
        {
            this.PinRetries = this.PinRetries ?? IntuneConstants.DefaultPinRetries;
            this.RecheckAccessOfflineGracePeriodMinutes = this.RecheckAccessOfflineGracePeriodMinutes ?? IntuneConstants.DefaultRecheckAccessOfflineGraceperiodMinutes;
            this.RecheckAccessTimeoutMinutes = this.RecheckAccessTimeoutMinutes ?? IntuneConstants.DefaultRecheckAccessTimeoutMinutes;
            this.OfflineWipeIntervalDays = this.OfflineWipeIntervalDays ?? IntuneConstants.DefaultOfflineWipeIntervalDays;
            this.Description = this.Description ?? "New iOS Policy";
        }

        /// <summary>
        /// Verify that numeric parameters are non negative
        /// </summary>
        private void ValidateNumericParameters()
        {
            NumericParameterValueChecker.CheckIfNegativeAndThrowException(
                new System.Collections.Generic.Dictionary<string, int>
                {
                    {"PinRetries", PinRetries.Value},
                    {"RecheckAccessOfflineGracePeriodMinutes", this.RecheckAccessOfflineGracePeriodMinutes.Value},
                    {"RecheckAccessTimeoutMinutes", this.RecheckAccessTimeoutMinutes.Value},
                    {"OfflineWipeIntervalDays", this.OfflineWipeIntervalDays.Value }
                });            
        }

        /// <summary>
        /// Prepares iOS Policy body for the new policy request
        /// </summary>
        /// <returns>policy request body</returns>
        private IOSMAMPolicy PrepareIOSPolicyBody()
        {
            var policyBody = new IOSMAMPolicy() {
                FriendlyName = this.FriendlyName,
                Description = this.Description,
                AppSharingFromLevel = this.AllowDataTransferToApps.ToString(),
                AppSharingToLevel = this.AllowDataTransferFromApps.ToString(),
                Authentication = this.Authentication.ToString(),
                ClipboardSharingLevel = this.ClipboardSharing.ToString(),
                DataBackup = this.DataBackup.ToString(),
                FileSharingSaveAs = this.FileSharingSaveAs.ToString(),
                Pin = this.Pin.ToString(),
                PinNumRetry = this.PinRetries,
                DeviceCompliance = this.DeviceCompliance.ToString(),
                ManagedBrowser = this.ManagedBrowser.ToString(),
                //AccessRecheckOfflineTimeout = XmlConvert.ToString(TimeSpan.FromMinutes(this.RecheckAccessOfflineGracePeriodMinutes.Value)),
                //AccessRecheckOnlineTimeout = XmlConvert.ToString(TimeSpan.FromMinutes(this.RecheckAccessTimeoutMinutes.Value)),
                //OfflineWipeTimeout = XmlConvert.ToString(TimeSpan.FromDays(this.OfflineWipeIntervalDays.Value)),
                AccessRecheckOfflineTimeout = TimeSpan.FromMinutes(this.RecheckAccessOfflineGracePeriodMinutes.Value),
                AccessRecheckOnlineTimeout = TimeSpan.FromMinutes(this.RecheckAccessTimeoutMinutes.Value),
                OfflineWipeTimeout = TimeSpan.FromDays(this.OfflineWipeIntervalDays.Value),
                FileEncryptionLevel = this.FileEncryptionLevel.ToString(),
                TouchId = this.AllowFingerprint.ToString()
            };            
            
            return policyBody;
        }        
    }
}