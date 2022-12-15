using CorsoCoreGabriel.Models.ValueTypes;
using System;
using System.Collections.Generic;

namespace CorsoCoreGabriel.Models.Entities
{
    public partial class Course
    {
        public Course(string title, string author)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new Exception("The course must have a title");
            }
            if (string.IsNullOrEmpty(author))
            {
                throw new Exception("The course must have a author");
            }

            this.Title = title;

            this.Author = author;

            Lessons = new HashSet<Lesson>();
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Author { get; private set; }
        public string Email { get; private set; }
        public double Rating { get; private set; }
        public Money FullPrice { get; private set; }
        public Money CurrentPrice { get; private set; }

        //public string RowVersion { get; private set; }

        public void ChangeTitle(string newTitle)
        {
            if (string.IsNullOrEmpty(newTitle))
            {
                throw new Exception("the course must have a title");
            }

            this.Title = newTitle;
        }


        public void ChangePrices(Money newFullPrice, Money newDiscountPrice)
        {
            if (newFullPrice==null || newDiscountPrice ==null)
            {
                throw new Exception("Prices can't be null");
            }

            if (newFullPrice.Currency != newDiscountPrice.Currency)
            {
                throw new Exception("Currencies don't match");
            }

            if (newFullPrice.Amount < newDiscountPrice.Amount)
            {
                throw new Exception("FullPrice can't be less than the current price");
            }


            this.FullPrice = newFullPrice;

            this.CurrentPrice = newDiscountPrice;

        }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
