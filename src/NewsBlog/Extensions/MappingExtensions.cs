using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsBlog.Domain;
using NewsBlog.Models.NewsViewModels;

namespace NewsBlog.Extensions
{
    using Microsoft.AspNetCore.Identity;

    using NewsBlog.Models;

    public static class MappingExtensions
    {
        public static PostViewModel ToModel(this Post entity)
        {
            var model =
                new PostViewModel()
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Content = entity.Text,
                    ImagePath = entity.ImagePath
                };

            return model;
        }

        public static Post ToEntity(this PostViewModel model)
        {
            var entity =
                new Post()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Text = model.Content,
                    ImagePath = model.ImagePath
                };

            return entity;
        }
    }
}
