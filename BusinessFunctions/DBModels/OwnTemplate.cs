﻿using System;

namespace Hypnofrog.DBModels
{
    public class OwnTemplate
    {
        public int OwnTemplateId { get; set; }
        public string HtmlRealize { get; set; }
        public DateTime CreationTime { get; set; }
        public int? PageId { get; set; }
        public string UserName { get; set; }
    }
}