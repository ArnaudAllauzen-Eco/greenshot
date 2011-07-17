﻿/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2011  Thomas Braun, Jens Klingen, Robin Krom
 * 
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using GreenshotPlugin.Core;

namespace GreenshotConfluencePlugin {
	/// <summary>
	/// Description of ConfluenceConfiguration.
	/// </summary>
	[IniSection("Confluence", Description="Greenshot Confluence Plugin configuration")]
	public class ConfluenceConfiguration : IniSection {
		public const string DEFAULT_POSTFIX = "/rpc/soap-axis/confluenceservice-v1?wsdl";
		public const string DEFAULT_PREFIX = "http://";
		private const string DEFAULT_URL = DEFAULT_PREFIX + "confluence" + DEFAULT_POSTFIX;

		[IniProperty("Url", Description="Url to Confluence system, including wsdl.", DefaultValue=DEFAULT_URL)]
		public string Url;
		[IniProperty("Timeout", Description="Session timeout in minutes", DefaultValue="30")]
		public int Timeout;

		[IniProperty("UploadFormat", Description="What file type to use for uploading", DefaultValue="png")]
		public OutputFormat UploadFormat;
		[IniProperty("UploadJpegQuality", Description="JPEG file save quality in %.", DefaultValue="80")]
		public int UploadJpegQuality;

		/// <summary>
		/// A form for username/password
		/// </summary>
		/// <returns>bool true if OK was pressed, false if cancel</returns>
        public bool ShowConfigDialog() {
			SettingsForm settingsForm = new SettingsForm();
        	settingsForm.Url = Url;
        	settingsForm.UploadFormat = UploadFormat.ToString();
        	DialogResult result = settingsForm.ShowDialog();
        	if (result == DialogResult.OK) {
        		if (!settingsForm.Url.Equals(Url) || !settingsForm.UploadFormat.Equals(UploadFormat.ToString())) {
            		Url = settingsForm.Url;
            		UploadFormat = (OutputFormat)Enum.Parse(typeof(OutputFormat), settingsForm.UploadFormat);
            	}
           		IniConfig.Save();
        		return true;
        	}
        	return false;
        }
	}
}
