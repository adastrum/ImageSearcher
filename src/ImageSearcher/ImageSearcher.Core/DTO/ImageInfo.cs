using System;
using System.Collections.Generic;

namespace ImageSearcher.Core.DTO
{
    public class ImageInfo : Image
    {
        public ImageInfo()
        {
            Notes = new List<string>();
            Tags = new List<string>();
        }

        public string DateUploaded { get; set; }
        public bool IsFavorite { get; set; }
        public string License { get; set; }
        public string SafetyLevel { get; set; }
        public int Rotation { get; set; }
        public string Description { get; set; }
        public DateTime Posted { get; set; }
        public DateTime Taken { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Views { get; set; }
        public bool CanComment { get; set; }
        public bool CanAddMeta { get; set; }
        public bool CanPublicComment { get; set; }
        public bool CanPublicAddMeta { get; set; }
        public bool CanDownload { get; set; }
        public bool CanBlog { get; set; }
        public bool CanPrint { get; set; }
        public bool CanShare { get; set; }
        public string Comments { get; set; }
        public IEnumerable<string> Notes { get; set; }
        public bool HasPeople { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Url { get; set; }
        public string Media { get; set; }
    }
}