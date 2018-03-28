using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NewsBlog.Models.NewsViewModels
{
    public class PostViewModel
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Image")]
        public IFormFile Image { get; set; }

        public string ImagePath { get; set; }
    }
}
