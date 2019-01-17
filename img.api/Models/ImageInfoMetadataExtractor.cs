using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using img.data;

namespace img.api.Models
{
    /// <summary>
    /// Current implementation of IImageInfo using MetadataExtractor nuget package
    /// </summary>
    public class MetadataExtractorImageInfo : IImageInfo
    {
        public IEnumerable<ImageInfo> GetImageInfo(string url)
        {
            List<ImageInfo> imageInfo = null;
            System.IO.FileStream fileStream = null;

            try
            {
                string fileName = System.IO.Path.GetTempFileName();

                if (fileName != null)
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(url, fileName);

                    if (System.IO.File.Exists(fileName))
                    {
                        using (fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
                        {
                            IEnumerable<MetadataExtractor.Directory> directories = MetadataExtractor.ImageMetadataReader.ReadMetadata(fileStream);

                            if (directories != null)
                            {
                                imageInfo = new List<ImageInfo>();

                                foreach (var directory in directories)
                                {
                                    foreach (var tag in directory.Tags)
                                    {
                                        imageInfo.Add(new ImageInfo() { Property = tag.Name, Value = tag.Description });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            return imageInfo;
        }
    }
}