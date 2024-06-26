﻿using System.ComponentModel.DataAnnotations;

namespace ConDigest.API.Models.Domain
{
    public class ProductItem
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public double ProductPrice { get; set; }

        public int Stock { get; set; }

        public Guid CategoryId { get; set; }

        public Guid RestaurantId { get; set; }


        // Navigation properties
        public Category Category { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
