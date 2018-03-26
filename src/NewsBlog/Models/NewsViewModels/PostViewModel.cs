using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsBlog.Models.NewsViewModels
{
    public class PostViewModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        [Required]
        [Display(Name = "Image")]
        public byte[] Image { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}
