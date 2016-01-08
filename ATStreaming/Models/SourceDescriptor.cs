using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATStreaming.Models
{
    public class SourceDescriptor
    {
        public string Market { get; private set; }
        public string Company { get; private set; }
        public string FilePath { get; private set; }

        /// <summary>
        /// Set Market and Company using file name convention: company_marker.extension
        /// </summary>
        /// <param name="filePath"></param>
        public SourceDescriptor(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException();
            }

            FilePath = filePath;
            SetPropertiesUsingFileName(filePath);
        }

        private void SetPropertiesUsingFileName(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var splittedProperties = fileName.Split('_');
            Company = splittedProperties[0];
            Market = splittedProperties[1];
        }
    }
}
