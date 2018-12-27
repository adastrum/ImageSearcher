﻿namespace ImageSearcher.Core.DTO
{
    public class Image
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        public string Secret { get; set; }
        public string Server { get; set; }
        public int Farm { get; set; }
        public string Title { get; set; }
        public bool IsPublic { get; set; }
        public bool IsFriend { get; set; }
        public bool IsFamily { get; set; }
    }
}
