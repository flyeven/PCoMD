﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Waf.Applications;
using CalendarSyncPlus.Common.Log;
using CalendarSyncPlus.Services.Interfaces;
using log4net;
using Newtonsoft.Json;

namespace CalendarSyncPlus.Services
{
    /// <summary>
    /// </summary>
    [Export(typeof (IApplicationUpdateService))]
    public class ApplicationUpdateService : IApplicationUpdateService
    {
        /// <summary>
        /// </summary>
        private string _downloadLink;

        /// <summary>
        /// </summary>
        private string _version;

        [ImportingConstructor]
        public ApplicationUpdateService(ApplicationLogger applicationLogger)
        {
            Logger = applicationLogger.GetLogger(GetType());
        }

        public ILog Logger { get; set; }

        #region IApplicationUpdateService Members

        /// <summary>
        /// </summary>
        public string GetLatestReleaseFromServer()
        {
            _version = null;
            _downloadLink = null;
            try
            {
                var obj = GetLatestReleaseTag();
                
                _version = obj.tag_name;

                string body = obj.body;
                if (body.Contains("[link]"))
                {
                    body = body.Split(new[] {"[link]"}, StringSplitOptions.RemoveEmptyEntries).Last();
                    if (body.Contains("(") && body.Contains(")"))
                    {
                        var arrayValues = body.Split(new[] {"(", ")"}, StringSplitOptions.RemoveEmptyEntries);
                        _downloadLink = arrayValues.FirstOrDefault(t => !string.IsNullOrEmpty(t.Trim()));
                    }
                }

                if (_downloadLink == null || string.IsNullOrEmpty(_downloadLink.Trim()))
                {
                    _downloadLink = obj.assets[0].browser_download_url;
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                return exception.Message;
            }
            return null;
        }

        /* [CFL] remove alpha version check
        private bool IsAlpha(string version1, string version2)
        {
            version1 = version1.Contains("-")
                ? version1.Remove(version1.IndexOf("-", StringComparison.InvariantCultureIgnoreCase))
                : version1;
            version2 = version2.Contains("-")
                ? version2.Remove(version2.IndexOf("-", StringComparison.InvariantCultureIgnoreCase))
                : version2;
            var version = new Version(version1.Substring(1));
            if (version < new Version(version2.Substring(1)))
            {
                return true;
            }
            return false;
        }*/

        private dynamic GetLatestReleaseTag()
        {
            //[CFL] own version control
            var request =
                WebRequest.Create(new Uri("https://api.github.com/repos/ThierryLecomte/PCoMD/releases"))
                    as HttpWebRequest;
            //var request =
            //    WebRequest.Create(new Uri("https://api.github.com/repos/ankeshdave/calendarsyncplus/releases/latest"))
            //        as HttpWebRequest;
            request.Method = "GET";
            request.ProtocolVersion = HttpVersion.Version11;
            request.ContentType = "application/json";
            request.ServicePoint.Expect100Continue = false;
            request.UnsafeAuthenticatedConnectionSharing = true;
            request.UserAgent = ApplicationInfo.ProductName;
            request.KeepAlive = false;
            string result = null;
            using (var resp = request.GetResponse() as HttpWebResponse)
            {
                if (resp != null)
                {
                    var reader =
                        new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }
            }
            //[CFL] remove alpha checks
            dynamic obj = JsonConvert.DeserializeObject(result);
            if (obj.Type == Newtonsoft.Json.Linq.JTokenType.Array)
            {
                return obj[0];
            }
            return obj;
        }

        /*private dynamic GetLatestTag()
        {
            var request =
                WebRequest.Create(new Uri("https://api.github.com/repos/ankeshdave/calendarsyncplus/releases"))
                    as HttpWebRequest;
            var request =
                WebRequest.Create(new Uri("https://api.github.com/repos/ThierryLecomte/PCoMD/releases"))
                    as HttpWebRequest;
            request.Method = "GET";
            request.ProtocolVersion = HttpVersion.Version11;
            request.ContentType = "application/json";
            request.ServicePoint.Expect100Continue = false;
            request.UnsafeAuthenticatedConnectionSharing = true;
            request.UserAgent = ApplicationInfo.ProductName;
            request.KeepAlive = false;
            string result = null;
            using (var resp = request.GetResponse() as HttpWebResponse)
            {
                if (resp != null)
                {
                    var reader =
                        new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }
            }
            dynamic obj = JsonConvert.DeserializeObject(result);
            return obj[0];
        }*/

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public bool IsNewVersionAvailable()
        {
            try
            {
                var versionString = _version.Contains("-")
                    ? _version.Remove(_version.IndexOf("-", StringComparison.InvariantCultureIgnoreCase))
                    : _version;
                //[CFL] version check (no 'v' prefix)
                var version = new Version(versionString);
                if (version > new Version(ApplicationInfo.Version))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
            return false;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public string GetNewAvailableVersion()
        {
            /*
            [CFL] remove alpha versions check
            try
            {
                if (_isAlpha && !_version.Contains("-"))
                    return string.Format("{0}-alpha", _version);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }*/
            return _version;
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        public Uri GetDownloadUri()
        {
            //Avoid for a while
            if (_downloadLink == null)
            {
                return null;
            }
            return new Uri(_downloadLink);
        }

        #endregion
    }
}