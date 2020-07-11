namespace MemorySystemApp.Services.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using MemorySystemApp.Data;
    using MemorySystemApp.Data.Models;
    using MemorySystemApp.Models.pictures;

    using Microsoft.EntityFrameworkCore;

    public class PicturesService : IPicturesService
    {
        private readonly MemorySystemDbContext db;

        public PicturesService(MemorySystemDbContext db)
        {
            this.db = db;
        }

        public bool Create(PictureRequestModel model, string userId)
        {
            if (model == null)
            {
                throw new NullReferenceException(nameof(model));
            }

            var category = this.db.Categories.FirstOrDefault(x => x.Type == model.Type);
            if (category == null)
            {
                return false;
            }

            var entity = Mapper.Map<Picture>(model);
            entity.OwnerId = userId;

            this.db.Pictures.Add(entity);
            this.db.CategoryPictures.Add(
                new CategoryPicture
                {
                    CategoryId = category.Id,
                    PictureId = entity.Id
                });

            db.SaveChanges();

            return true;
        }

        //public async Task<Result<IEnumerable<PictureModel>>> GetAll()
        //{
        //    var user = this.db.Users.Include(p => p.Pictures).FirstOrDefault(u => u.Id == userId);
        //    if (user == null)
        //    {
        //        return Result<IEnumerable<PictureModel>>.Failure(new[] { "User not found" });
        //    }

        //    var pictures = Mapper.Map<IEnumerable<PictureModel>>(user.Pictures);

        //    var pictureIds = pictures.Select(p => p.Id);

        //    var likesForPicures = await db.Likes
        //        .Where(l => l.UserId == currentUserId && pictureIds.Contains(l.PictureId))
        //        .ToListAsync();

        //    foreach (var picture in pictures)
        //    {
        //        picture.IsLikedFromCurrentUser = likesForPicures.Any(lp => lp.UserId == currentUserId && lp.PictureId == picture.Id);
        //    }

        //    return Result<IEnumerable<PictureModel>>.SuccessWith(pictures);
        //}

        public async Task<Result<IEnumerable<PictureModel>>> GetOwnPictures(string userId)
        {
            var user = this.db.Users.Include(p => p.Pictures).FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return Result<IEnumerable<PictureModel>>.Error("User not found");
            }

            var pictures = Mapper.Map<IEnumerable<PictureModel>>(user.Pictures);

            var pictureIds = pictures.Select(p => p.Id);

            var likesForPicures = await db.Likes
                .Where(l => l.UserId == userId && pictureIds.Contains(l.PictureId))
                .ToListAsync();

            foreach (var picture in pictures)
            {
                picture.IsLikedFromCurrentUser = likesForPicures.Any(lp => lp.PictureId == picture.Id);
            }

            return Result<IEnumerable<PictureModel>>.Success(pictures);
        }

        public async Task<Result<IEnumerable<PictureModel>>> GetUserPictures(string currentUserId, string userId)
        {
            // check for both users

            var user = this.db.Users.Include(p => p.Pictures).FirstOrDefault(u => u.Id == currentUserId);
            if (user == null)
            {
                return Result<IEnumerable<PictureModel>>.Error("User not found");
            }

            var pictures = Mapper.Map<IEnumerable<PictureModel>>(user.Pictures);

            var pictureIds = pictures.Select(p => p.Id);

            var likesForPicures = await db.Likes
                .Where(l => l.UserId == userId && pictureIds.Contains(l.PictureId))
                .ToListAsync();

            foreach (var picture in pictures)
            {
                picture.IsLikedFromCurrentUser = likesForPicures.Any(lp => lp.PictureId == picture.Id);
            }

            return Result<IEnumerable<PictureModel>>.Success(pictures);
        }
    }
}
