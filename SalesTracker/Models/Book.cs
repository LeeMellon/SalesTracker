﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTracker.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public decimal Ammount { get; set; }
        public decimal PreviousTotal { get; set; }
        public decimal CurrentTotal { get; set; }

        public override bool Equals(System.Object otherBook)
        {
            if (!(otherBook is Book))
            {
                return false;
            }
            else
            {
                Book newBook = (Book)otherBook;
                return this.BookId.Equals(newBook.BookId);
            }
        }

        public override int GetHashCode()
        {
            return this.BookId.GetHashCode();
        }
    }
}
