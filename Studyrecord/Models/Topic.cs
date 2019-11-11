using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Studyrecord.Models
{
    public partial class Topic
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Estimatedtime { get; set; }
        public decimal? TimeSpent { get; set; }
        public string Source { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Start { get; set; }
        public bool? InProgress { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CompletionDate { get; set; }
    }
}
